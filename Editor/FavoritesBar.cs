using System;
using System.Collections.Generic;
using System.Linq;
using SaintsProject.Editor.Config;
using SaintsProject.Editor.Utils;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SaintsProject.Editor
{
    public sealed class FavoritesBar
    {
        private readonly List<AssetFavorite> _favorites = new List<AssetFavorite>();
        private readonly List<Object> _objects = new List<Object>();
        private readonly List<Object> _dragAssets = new List<Object>();
        private IConfig _source;
        private bool _dirty = true;
        private int _pressed = -1;
        private Vector2 _pressPosition;
        private bool _dragging;
        private int _dropIndex = -1;
        private float _height = 24;

        // Keep the preview model and ordering flow aligned with SaintsHierarchy Legacy.
        private enum RuntimeFavoriteStatus
        {
            Default,
            DragExisted,
            DragNew,
        }

        private sealed class FavoriteDrawingInfo
        {
            public int SavedIndex;
            public AssetFavorite Favorite;
            public Object Asset;
            public RuntimeFavoriteStatus Status;
            public GUIContent Content;
            public Texture Icon;
            public Rect Rect;
        }
        public void Invalidate() => _dirty = true;
        private void Refresh()
        {
            if (!_dirty && ReferenceEquals(_source, ProjectConfigStore.Favorites))
            {
                return;
            }

            _dirty = false;
            _source = ProjectConfigStore.Favorites;
            _pressed = -1;
            _dragging = false;
            _dropIndex = -1;
            _dragAssets.Clear();
            _favorites.Clear();
            _objects.Clear();
            _favorites.AddRange(_source.Favorites);
            foreach (AssetFavorite favorite in _favorites)
            {
                _objects.Add(ProjectConfigStore.ResolveFavorite(favorite));
            }
        }

        public float OnGUI(float width, float maxHeight)
        {
            Refresh();
            Event evt = Event.current;
            if (evt.rawType == EventType.MouseMove || evt.rawType == EventType.MouseDown || evt.rawType == EventType.MouseUp || evt.rawType == EventType.DragUpdated || evt.rawType == EventType.DragPerform || evt.rawType == EventType.DragExited)
            {
                EditorWindow.mouseOverWindow?.Repaint();
            }

            const float rowHeight = 22;
            float available = Mathf.Max(40, width - 24);
            Rect area = new Rect(0, 0, available, _height);
            if (evt.type == EventType.DragUpdated || evt.type == EventType.DragPerform)
            {
                _dragAssets.Clear();
                if (area.Contains(evt.mousePosition))
                {
                    foreach (Object asset in DragAndDrop.objectReferences)
                    {
                        if (asset && EditorUtility.IsPersistent(asset)
                            && !string.IsNullOrEmpty(AssetDatabase.GetAssetPath(asset)) && !_dragAssets.Contains(asset))
                        {
                            _dragAssets.Add(asset);
                        }
                    }
                }
            }
            else if (evt.type == EventType.DragExited || evt.rawType == EventType.MouseUp)
            {
                _dragAssets.Clear();
            }

            List<FavoriteDrawingInfo> existsDrawingInfos = new List<FavoriteDrawingInfo>();
            Dictionary<Object, FavoriteDrawingInfo> existedDragging = new Dictionary<Object, FavoriteDrawingInfo>();
            for (int i = 0; i < _favorites.Count; i++)
            {
                AssetFavorite favorite = _favorites[i];
                Object asset = _objects[i];
                AssetConfig appearance = asset ? ProjectConfigStore.Resolve(AssetDatabase.GetAssetPath(asset)) : default;
                Texture icon = null;
                switch (favorite.iconType)
                {
                    case FavoriteIconType.Default:
                        icon = Util.GetIcon(appearance.icon);
                        if (!icon && asset)
                        {
                            icon = AssetDatabase.GetCachedIcon(AssetDatabase.GetAssetPath(asset));
                        }

                        break;
                    case FavoriteIconType.UnityDefault:
                        if (asset)
                        {
                            icon = AssetDatabase.GetCachedIcon(AssetDatabase.GetAssetPath(asset));
                        }

                        break;
                    case FavoriteIconType.Custom:
                        icon = Util.GetIcon(favorite.icon);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                string label = string.IsNullOrEmpty(favorite.alias) ? (asset ? asset.name : "Missing Asset") : favorite.alias;
                GUIContent content = new GUIContent(label, asset ? AssetDatabase.GetAssetPath(asset) : favorite.guid);
                float itemWidth = Mathf.Min(available - 16, GUI.skin.button.CalcSize(content).x + (icon ? 18 : 0) + 8);
                FavoriteDrawingInfo info = new FavoriteDrawingInfo
                {
                    SavedIndex = i,
                    Favorite = favorite,
                    Asset = asset,
                    Status = RuntimeFavoriteStatus.Default,
                    Content = content,
                    Icon = icon,
                    Rect = new Rect(0, 0, itemWidth, rowHeight - 2),
                };
                if (asset && _dragAssets.Contains(asset))
                {
                    existedDragging[asset] = info;
                    continue;
                }

                existsDrawingInfos.Add(info);
            }

            List<FavoriteDrawingInfo> draggingDrawingInfos = new List<FavoriteDrawingInfo>();
            foreach (Object asset in _dragAssets)
            {
                bool exists = existedDragging.TryGetValue(asset, out FavoriteDrawingInfo info);
                if (!exists)
                {
                    string path = AssetDatabase.GetAssetPath(asset);
                    Texture icon = Util.GetIcon(ProjectConfigStore.Resolve(path).icon);
                    if (!icon)
                    {
                        icon = AssetDatabase.GetCachedIcon(path);
                    }

                    GUIContent content = new GUIContent(asset.name, path);
                    float itemWidth = Mathf.Min(available - 16, GUI.skin.button.CalcSize(content).x + (icon ? 18 : 0) + 8);
                    info = new FavoriteDrawingInfo
                    {
                        SavedIndex = -1,
                        Asset = asset,
                        Content = content,
                        Icon = icon,
                        Rect = new Rect(0, 0, itemWidth, rowHeight - 2),
                    };
                }

                info.Status = exists ? RuntimeFavoriteStatus.DragExisted : RuntimeFavoriteStatus.DragNew;
                draggingDrawingInfos.Add(info);
            }

            CalcRelativePos(existsDrawingInfos.Concat(draggingDrawingInfos), available, rowHeight);
            _dropIndex = -1;
            List<FavoriteDrawingInfo> favoriteDrawingInfos;
            if (draggingDrawingInfos.Count > 0)
            {
                Vector2 mousePos = evt.mousePosition;
                favoriteDrawingInfos = new List<FavoriteDrawingInfo>(existsDrawingInfos.Count + draggingDrawingInfos.Count);
                bool inserted = false;
                foreach (FavoriteDrawingInfo favoriteDrawingInfo in existsDrawingInfos)
                {
                    Rect useRect = favoriteDrawingInfo.Rect;
                    if (!inserted && useRect.Contains(mousePos))
                    {
                        bool isPre = Mathf.InverseLerp(useRect.x, useRect.xMax, mousePos.x) < .4f;
                        if (isPre)
                        {
                            favoriteDrawingInfos.AddRange(draggingDrawingInfos);
                            favoriteDrawingInfos.Add(favoriteDrawingInfo);
                        }
                        else
                        {
                            favoriteDrawingInfos.Add(favoriteDrawingInfo);
                            favoriteDrawingInfos.AddRange(draggingDrawingInfos);
                        }

                        // AddFavorites accepts an index in the saved list, before removing dragged entries.
                        _dropIndex = favoriteDrawingInfo.SavedIndex + (isPre ? 0 : 1);
                        inserted = true;
                    }
                    else
                    {
                        favoriteDrawingInfos.Add(favoriteDrawingInfo);
                    }
                }

                if (!inserted)
                {
                    favoriteDrawingInfos.AddRange(draggingDrawingInfos);
                    _dropIndex = _favorites.Count;
                }
            }
            else
            {
                favoriteDrawingInfos = existsDrawingInfos;
            }

            float contentHeight = CalcRelativePos(favoriteDrawingInfos, available, rowHeight);
            // Match SaintsHierarchy: wrap favorites into rows and let the toolbar grow to fit them.
            float height = Mathf.Max(rowHeight + 2, contentHeight);
            _height = height;
            area.height = height;
            foreach (FavoriteDrawingInfo favoriteDrawingInfo in favoriteDrawingInfos)
            {
                int i = favoriteDrawingInfo.SavedIndex;
                Rect rect = favoriteDrawingInfo.Rect;
                AssetFavorite favorite = favoriteDrawingInfo.Favorite;
                Object asset = favoriteDrawingInfo.Asset;
                bool placeholder = favoriteDrawingInfo.Status != RuntimeFavoriteStatus.Default;
                bool hover = rect.Contains(evt.mousePosition);
                if (evt.type == EventType.Repaint)
                {
                    Color old = GUI.backgroundColor;
                    AssetConfig appearance = asset ? ProjectConfigStore.Resolve(AssetDatabase.GetAssetPath(asset)) : default;
                    // ReSharper disable once ConvertIfStatementToSwitchStatement
                    if (placeholder)
                    {
                        GUI.backgroundColor = favoriteDrawingInfo.Status == RuntimeFavoriteStatus.DragExisted ? Color.cyan : Color.green;
                    }
                    else if (favorite.colorType == FavoriteColorType.CustomColor)
                    {
                        GUI.backgroundColor = favorite.color;
                    }
                    else if (favorite.colorType == FavoriteColorType.Default && appearance.hasColor)
                    {
                        GUI.backgroundColor = appearance.color;
                    }

                    GUI.skin.button.Draw(rect, GUIContent.none, hover, !placeholder && _pressed == i, false, false);
                    GUI.backgroundColor = old;
                    Rect labelRect = new Rect(rect.x + 4, rect.y, rect.width - 8, rect.height);
                    if (favoriteDrawingInfo.Icon)
                    {
                        GUI.DrawTexture(new Rect(labelRect.x, rect.y + 2, 16, 16), favoriteDrawingInfo.Icon, ScaleMode.ScaleToFit, true);
                        labelRect.xMin += 18;
                    }

                    GUI.Label(labelRect, favoriteDrawingInfo.Content, EditorStyles.label);
                }

                if (placeholder)
                {
                    continue;
                }

                if (hover && (evt.type == EventType.ContextClick || (evt.type == EventType.MouseDown && evt.button == 0 && evt.alt)))
                {
                    PopupWindow.Show(rect, new FavoriteConfigPopup(favorite));
                    evt.Use();
                }

                if (hover && evt.type == EventType.MouseDown && evt.button == 0)
                {
                    _pressed = i;
                    _pressPosition = evt.mousePosition;
                    _dragging = false;
                    evt.Use();
                }

                if (_pressed == i && !_dragging && evt.type == EventType.MouseDrag && asset && (evt.mousePosition - _pressPosition).sqrMagnitude > 25)
                {
                    DragAndDrop.PrepareStartDrag();
                    DragAndDrop.objectReferences = new[]
                    {
                        asset,
                    };
                    DragAndDrop.StartDrag(asset.name);
                    _dragging = true;
                    evt.Use();
                }

                // ReSharper disable once InvertIf
                if (_pressed == i && evt.type == EventType.MouseUp && evt.button == 0)
                {
                    if (hover && !_dragging)
                    {
                        if (asset)
                        {
                            EditorGUIUtility.PingObject(asset);
                            if (ProjectConfigStore.Active.FavoriteClickToInspect)
                            {
                                Selection.activeObject = asset;
                            }
                        }
                    }

                    _pressed = -1;
                    _dragging = false;
                    evt.Use();
                }
            }

            if (evt.type == EventType.DragUpdated || evt.type == EventType.DragPerform)
            {
                if (_dragAssets.Count > 0)
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();
                        ProjectConfigStore.AddFavorites(_dragAssets, _dropIndex);
                        _dragAssets.Clear();
                        _pressed = -1;
                        _dragging = false;
                        _dropIndex = -1;
                    }

                    evt.Use();
                }
            }

            if (evt.type == EventType.DragExited)
            {
                _pressed = -1;
                _dragging = false;
                _dropIndex = -1;
            }

            if (evt.type == EventType.Repaint)
            {
                GUI.DrawTexture(new Rect(width - 16, 0, 16, 16), Util.GetIcon("fav.png"), ScaleMode.ScaleToFit, true);
            }

            return height;
        }

        public void DrawDragLabel()
        {
            if (Event.current.type == EventType.Repaint && _dragAssets.Count > 0)
            {
                GUIContent content = new GUIContent(string.Join("\n", _dragAssets.ConvertAll(asset => asset.name)));
                Vector2 size = GUI.skin.label.CalcSize(content);
                GUI.Label(new Rect(Event.current.mousePosition + new Vector2(10, 10), size), content);
            }
        }

        private static float CalcRelativePos(IEnumerable<FavoriteDrawingInfo> infos, float available, float rowHeight)
        {
            float x = 2, y = 2;
            foreach (FavoriteDrawingInfo info in infos)
            {
                if (x > 2 && x + info.Rect.width > available - 16)
                {
                    x = 2;
                    y += rowHeight;
                }

                info.Rect.position = new Vector2(x, y);
                x += info.Rect.width + 2;
            }

            return y + rowHeight;
        }

    }
}
