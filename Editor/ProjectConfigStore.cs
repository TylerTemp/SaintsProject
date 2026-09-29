using System;
using System.Collections.Generic;
using SaintsProject.Editor.Config;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SaintsProject.Editor
{
    [InitializeOnLoad]
    public static class ProjectConfigStore
    {
        public static event Action Changed;
        public static IConfig Active => PersonalProjectConfig.instance.personalEnabled ? PersonalProjectConfig.instance : SaintsProjectConfig.instance;
        public static IConfig Favorites => Active.SaveFavoritesToProjectConfig ? SaintsProjectConfig.instance : PersonalProjectConfig.instance;

        private static readonly Dictionary<string, AssetConfig> Resolved = new Dictionary<string, AssetConfig>();
        private static readonly Dictionary<string, AssetConfig> Direct = new Dictionary<string, AssetConfig>();
        private static readonly HashSet<IConfig> Modified = new HashSet<IConfig>();
        private static IConfig _indexedConfig;
        static ProjectConfigStore()
        {
            EditorApplication.projectChanged += Invalidate;
            Undo.undoRedoPerformed += OnUndoRedo;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.EnteredPlayMode && state != PlayModeStateChange.EnteredEditMode)
            {
                return;
            }

            // Editor subscriptions survive when domain reload is disabled, but cached
            // asset references and configuration values can change across Play mode.
            FolderContents.Invalidate();
            Invalidate();
        }

        private static void Invalidate()
        {
            _indexedConfig = null;
            Direct.Clear();
            Resolved.Clear();
            Utils.Util.ClearCache();
            Changed?.Invoke();
            EditorApplication.RepaintProjectWindow();
        }

        public static void Edit(IConfig config, string undoName, Action action)
        {
            Undo.RecordObject((Object)config, undoName);
            action();
            EditorUtility.SetDirty((Object)config);
            Modified.Add(config);
            config.SaveToDisk();
            Invalidate();
        }

        private static void OnUndoRedo()
        {
            foreach (IConfig config in Modified)
            {
                config.SaveToDisk();
            }

            Invalidate();
        }

        public static AssetConfig GetDirect(string guid)
        {
            if (!ReferenceEquals(_indexedConfig, Active))
            {
                Direct.Clear();
                Resolved.Clear();
                _indexedConfig = Active;
                foreach (AssetConfig entry in Active.Assets)
                {
                    if (!string.IsNullOrEmpty(entry.guid))
                    {
                        Direct[entry.guid] = entry;
                    }
                }
            }

            return Direct.TryGetValue(guid, out AssetConfig value) ? value : new AssetConfig
            {
                guid = guid,
            };
        }

        public static AssetConfig Resolve(string assetPath)
        {
            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            AssetConfig value = GetDirect(guid);
            if (Resolved.TryGetValue(assetPath, out AssetConfig cached))
            {
                return cached;
            }

            bool needsIcon = string.IsNullOrEmpty(value.icon);
            bool needsColor = !value.hasColor;
            string parent = assetPath;
            while ((needsIcon || needsColor) && parent.LastIndexOf('/') > 0)
            {
                parent = parent[..parent.LastIndexOf('/')];
                AssetConfig inherited = GetDirect(AssetDatabase.AssetPathToGUID(parent));
                if (needsIcon && inherited.recursiveIcon && !string.IsNullOrEmpty(inherited.icon))
                {
                    value.icon = inherited.icon;
                    needsIcon = false;
                }

                // ReSharper disable once InvertIf
                if (needsColor && inherited is { recursiveColor: true, hasColor: true })
                {
                    value.hasColor = true;
                    value.color = inherited.color;
                    needsColor = false;
                }
            }

            Resolved[assetPath] = value;
            return value;
        }

        public static void SetAsset(AssetConfig value)
        {
            if (string.IsNullOrEmpty(value.guid) || string.IsNullOrEmpty(AssetDatabase.GUIDToAssetPath(value.guid)))
            {
                throw new ArgumentException("Asset GUID does not resolve to an existing asset.", nameof(value));
            }

            IConfig config = Active;
            Edit(config, "Edit Project Asset Appearance", () =>
            {
                config.Assets.RemoveAll(each => each.guid == value.guid);
                if (value.hasColor || !string.IsNullOrEmpty(value.icon))
                {
                    config.Assets.Add(value);
                }
            });
        }

        private static AssetFavorite Identify(Object asset)
        {
            if (!asset || !EditorUtility.IsPersistent(asset) || !AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out string guid, out long localId))
            {
                throw new ArgumentException("Favorites must be persistent project assets.", nameof(asset));
            }

            // Main assets follow their GUID even if their importer changes their local ID.
            return new AssetFavorite
            {
                guid = guid,
                localId = AssetDatabase.IsMainAsset(asset) ? 0 : localId,
            };
        }

        public static Object ResolveFavorite(AssetFavorite favorite)
        {
            string path = AssetDatabase.GUIDToAssetPath(favorite.guid);
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            if (favorite.localId == 0)
            {
                return AssetDatabase.LoadMainAssetAtPath(path);
            }

            foreach (Object asset in AssetDatabase.LoadAllAssetsAtPath(path))
            {
                if (asset && AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out string _, out long id) && id == favorite.localId)
                {
                    return asset;
                }
            }

            return null;
        }

        public static void AddFavorite(Object asset, int index = -1)
        {
            AddFavorites(new[] { asset }, index);
        }

        public static void AddFavorites(IReadOnlyList<Object> assets, int index = -1)
        {
            IConfig config = Favorites;
            List<AssetFavorite> incoming = new List<AssetFavorite>();
            foreach (Object asset in assets)
            {
                AssetFavorite favorite = Identify(asset);
                if (incoming.Exists(each => each.SameAsset(favorite)))
                {
                    continue;
                }

                int oldIndex = config.Favorites.FindIndex(each => each.SameAsset(favorite));
                if (oldIndex >= 0)
                {
                    if (index < 0)
                    {
                        continue;
                    }

                    favorite = config.Favorites[oldIndex];
                }

                incoming.Add(favorite);
            }

            if (incoming.Count == 0)
            {
                return;
            }

            int insertion = index < 0 ? config.Favorites.Count : Mathf.Clamp(index, 0, config.Favorites.Count);
            List<AssetFavorite> reordered = new List<AssetFavorite>();
            int removedBeforeInsertion = 0;
            for (int i = 0; i < config.Favorites.Count; i++)
            {
                AssetFavorite favorite = config.Favorites[i];
                if (incoming.Exists(each => each.SameAsset(favorite)))
                {
                    if (i < insertion)
                    {
                        removedBeforeInsertion++;
                    }
                }
                else
                {
                    reordered.Add(favorite);
                }
            }

            reordered.InsertRange(insertion - removedBeforeInsertion, incoming);
            bool unchanged = reordered.Count == config.Favorites.Count;
            for (int i = 0; unchanged && i < reordered.Count; i++)
            {
                unchanged = reordered[i].SameAsset(config.Favorites[i]);
            }

            if (unchanged)
            {
                return;
            }

            Edit(config, "Add or Move Project Favorites", () =>
            {
                config.Favorites.Clear();
                config.Favorites.AddRange(reordered);
            });
        }

        public static void SetFavorite(AssetFavorite favorite)
        {
            IConfig config = Favorites;
            int index = config.Favorites.FindIndex(each => each.SameAsset(favorite));
            if (index < 0)
            {
                return;
            }

            Edit(config, "Edit Project Favorite", () => config.Favorites[index] = favorite);
        }

        public static void RemoveFavorite(AssetFavorite favorite)
        {
            IConfig config = Favorites;
            Edit(config, "Remove Project Favorite", () => config.Favorites.RemoveAll(each => each.SameAsset(favorite)));
        }
    }
}
