using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace SaintsProject.Editor.Config
{
    [FilePath("Library/PersonalProjectConfig.asset", FilePathAttribute.Location.ProjectFolder)]
    public class PersonalProjectConfig : ScriptableSingleton<PersonalProjectConfig>, IConfig
    {
        public bool personalEnabled;
        private void OnEnable()
        {
            hideFlags &= ~(HideFlags.NotEditable | HideFlags.HideInInspector);
        }

        [field: SerializeField]
        public bool Disabled { get; set; }

        [field: SerializeField]
        public bool BackgroundStrip { get; set; } = true;

        [field: SerializeField]
        public bool MinimalMode { get; set; }

        [field: SerializeField]
        public bool IndentGuides { get; set; } = true;

        [field: SerializeField]
        public bool AutoIcons { get; set; } = true;

        [field: SerializeField]
        public bool ContentMinimap { get; set; } = true;

        [field: SerializeField]
        public bool DisableFavorites { get; set; }

        [field: SerializeField]
        public bool FavoriteClickToInspect { get; set; } = true;

        [field: SerializeField]
        public bool SaveFavoritesToProjectConfig { get; set; }

        [field: SerializeField]
        public List<AssetConfig> Assets { get; private set; } = new List<AssetConfig>();

        [field: SerializeField]
        public List<AssetFavorite> Favorites { get; private set; } = new List<AssetFavorite>();

        public void SaveToDisk() => Save(true);
    }
}
