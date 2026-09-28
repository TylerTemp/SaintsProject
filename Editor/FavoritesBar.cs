using System;
using System.Collections.Generic;
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
        private readonly List<Rect> _rects = new List<Rect>();
        private readonly List<GUIContent> _labels = new List<GUIContent>();
        private readonly List<Texture> _icons = new List<Texture>();
        private IConfig _source;
        private bool _dirty = true;
        private Vector2 _scroll;
        private int _pressed = -1;
        private Vector2 _pressPosition;
        private bool _dragging;
        private int _dropIndex = -1;
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
            if (evt.rawType == EventType.MouseMove || evt.rawType == EventType.MouseDown || evt.rawType == EventType.MouseUp || evt.rawType == EventType.DragUpdated || evt.rawType == EventType.DragExited)
            {
                EditorWindow.mouseOverWindow?.Repaint();
            }

            const float rowHeight = 22;
            float available = Mathf.Max(40, width - 24);
            float x = 2, y = 2;
            _rects.Clear();
            _labels.Clear();
            _icons.Clear();
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
                if (x > 2 && x + itemWidth > available - 16)
                {
                    x = 2;
                    y += rowHeight;
                }

                _rects.Add(new Rect(x, y, itemWidth, rowHeight - 2));
                _labels.Add(content);
                _icons.Add(icon);
                x += itemWidth + 2;
            }

            float contentHeight = y + rowHeight;
            float height = Mathf.Clamp(contentHeight, rowHeight + 2, Mathf.Max(rowHeight + 2, maxHeight));
            Rect area = new Rect(0, 0, available, height);
            _scroll = GUI.BeginScrollView(area, _scroll, new Rect(0, 0, available - 16, contentHeight), false, contentHeight > height);
            for (int i = 0; i < _favorites.Count; i++)
            {
                Rect rect = _rects[i];
                AssetFavorite favorite = _favorites[i];
                Object asset = _objects[i];
                bool hover = rect.Contains(evt.mousePosition);
                if (evt.type == EventType.Repaint)
                {
                    Color old = GUI.backgroundColor;
                    AssetConfig appearance = asset ? ProjectConfigStore.Resolve(AssetDatabase.GetAssetPath(asset)) : default;
                    // ReSharper disable once ConvertIfStatementToSwitchStatement
                    if (favorite.colorType == FavoriteColorType.CustomColor)
                    {
                        GUI.backgroundColor = favorite.color;
                    }
                    else if (favorite.colorType == FavoriteColorType.Default && appearance.hasColor)
                    {
                        GUI.backgroundColor = appearance.color;
                    }

                    GUI.skin.button.Draw(rect, GUIContent.none, hover, _pressed == i, false, false);
                    GUI.backgroundColor = old;
                    Rect labelRect = new Rect(rect.x + 4, rect.y, rect.width - 8, rect.height);
                    if (_icons[i])
                    {
                        GUI.DrawTexture(new Rect(labelRect.x, rect.y + 2, 16, 16), _icons[i], ScaleMode.ScaleToFit, true);
                        labelRect.xMin += 18;
                    }

                    GUI.Label(labelRect, _labels[i], EditorStyles.label);
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

            // if (_favorites.Count == 0)
            // {
            //     GUI.Label(new Rect(4, 2, available, rowHeight), "Drag assets here to add favorites", EditorStyles.miniLabel);
            // }

            // Mouse coordinates are local to the scroll view here.
            if (evt.type == EventType.DragUpdated || evt.type == EventType.DragPerform)
            {
                _dropIndex = -1;
                Rect visible = new Rect(_scroll.x, _scroll.y, area.width, area.height);
                if (visible.Contains(evt.mousePosition))
                {
                    List<Object> assets = new List<Object>();
                    foreach (Object asset in DragAndDrop.objectReferences)
                    {
                        if (asset && EditorUtility.IsPersistent(asset) && !string.IsNullOrEmpty(AssetDatabase.GetAssetPath(asset)))
                        {
                            assets.Add(asset);
                        }
                    }

                    if (assets.Count > 0)
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                        int insertion = _favorites.Count;
                        for (int i = 0; i < _rects.Count; i++)
                        {
                            if (evt.mousePosition.y < _rects[i].yMin || (evt.mousePosition.y < _rects[i].yMax && evt.mousePosition.x < _rects[i].center.x))
                            {
                                insertion = i;
                                break;
                            }
                        }

                        _dropIndex = insertion;
                        if (evt.type == EventType.DragPerform)
                        {
                            DragAndDrop.AcceptDrag();
                            ProjectConfigStore.AddFavorites(assets, insertion);
                            _pressed = -1;
                            _dragging = false;
                            _dropIndex = -1;
                        }

                        evt.Use();
                    }
                }
            }

            if (evt.type == EventType.DragExited)
            {
                _pressed = -1;
                _dragging = false;
                _dropIndex = -1;
            }

            if (evt.type == EventType.Repaint && _dropIndex >= 0)
            {
                Rect marker;
                if (_dropIndex < _rects.Count)
                {
                    marker = _rects[_dropIndex];
                }
                else
                {
                    marker = _rects.Count > 0 ? _rects[^1] : new Rect(2, 2, 0, rowHeight - 2);
                }

                float markerX = _dropIndex < _rects.Count
                    ? marker.xMin - 1
                    : marker.xMax + 1;
                EditorGUI.DrawRect(new Rect(markerX, marker.y, 2, marker.height), new Color(.3f, .65f, 1f));
            }

            GUI.EndScrollView();
            if (evt.type == EventType.Repaint)
            {
                GUI.DrawTexture(new Rect(width - 20, 4, 16, 16), Util.GetIcon("fav.png"), ScaleMode.ScaleToFit, true);
            }

            return height;
        }
    }
}
