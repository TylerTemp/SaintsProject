using System.Collections.Generic;

namespace SaintsProject.Editor.Config
{
    public interface IConfig
    {
        bool Disabled { get; set; }

        bool BackgroundStrip { get; set; }

        bool MinimalMode { get; set; }

        bool IndentGuides { get; set; }

        bool AutoIcons { get; set; }

        bool ContentMinimap { get; set; }

        bool DisableFavorites { get; set; }

        bool FavoriteClickToInspect { get; set; }

        bool SaveFavoritesToProjectConfig { get; set; }

        List<AssetConfig> Assets { get; }

        List<AssetFavorite> Favorites { get; }

        void SaveToDisk();
    }
}
