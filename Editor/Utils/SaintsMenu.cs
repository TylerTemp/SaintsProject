using System;
using SaintsProject.Editor.Config;
using UnityEditor;

namespace SaintsProject.Editor.Utils
{
    public static class SaintsMenu
    {
        private const string MenuRoot =
#if !SAINTSPROJECT_DEBUG
                "Tools/" +
#endif
                "Saints Project/"
        ;

        [MenuItem(MenuRoot + "Edit Config...", false, -103)]
        private static void EditConfig() => ConfigEditWindow.Open();
        private static void Toggle(string label, Action<IConfig> change)
        {
            IConfig config = ProjectConfigStore.Active;
            ProjectConfigStore.Edit(config, label, () => change(config));
        }

        private static bool Check(string label, bool value, bool enabled = true)
        {
            Menu.SetChecked(MenuRoot + label, value);
            return enabled;
        }

        [MenuItem(MenuRoot + "Disable Saints Project", false, -102)]
        private static void Disabled() => Toggle("Disable Saints Project", config => config.Disabled = !config.Disabled);
        [MenuItem(MenuRoot + "Enable Personal Config", false, -100)]
        private static void Personal()
        {
            PersonalProjectConfig config = PersonalProjectConfig.instance;
            ProjectConfigStore.Edit(config, "Toggle Personal Project Config", () => config.personalEnabled = !config.personalEnabled);
        }

        [MenuItem(MenuRoot + "Enable Personal Config", true)]
        private static bool PersonalValidate() => Check("Enable Personal Config", PersonalProjectConfig.instance.personalEnabled);

        [MenuItem(MenuRoot + "Disable Saints Project", true)]
        private static bool DisabledValidate() => Check("Disable Saints Project", ProjectConfigStore.Active.Disabled);
        [MenuItem(MenuRoot + "Background Strip", false, 0)]
        private static void BackgroundStrip() => Toggle("Background Strip", config => config.BackgroundStrip = !config.BackgroundStrip);
        [MenuItem(MenuRoot + "Background Strip", true)]
        private static bool BackgroundStripValidate() => Check("Background Strip", ProjectConfigStore.Active.BackgroundStrip, !ProjectConfigStore.Active.Disabled);
        [MenuItem(MenuRoot + "Minimal Mode", false, 1)]
        private static void MinimalMode() => Toggle("Minimal Mode", config => config.MinimalMode = !config.MinimalMode);
        [MenuItem(MenuRoot + "Minimal Mode", true)]
        private static bool MinimalModeValidate() => Check("Minimal Mode", ProjectConfigStore.Active.MinimalMode, !ProjectConfigStore.Active.Disabled);
        [MenuItem(MenuRoot + "Indent Guides", false, 1)]
        private static void IndentGuides() => Toggle("Indent Guides", config => config.IndentGuides = !config.IndentGuides);
        [MenuItem(MenuRoot + "Indent Guides", true)]
        private static bool IndentGuidesValidate() => Check("Indent Guides", ProjectConfigStore.Active.IndentGuides, !ProjectConfigStore.Active.Disabled);
        [MenuItem(MenuRoot + "Auto Icons", false, 2)]
        private static void AutoIcons() => Toggle("Auto Icons", config => config.AutoIcons = !config.AutoIcons);
        [MenuItem(MenuRoot + "Auto Icons", true)]
        private static bool AutoIconsValidate() => Check("Auto Icons", ProjectConfigStore.Active.AutoIcons, !ProjectConfigStore.Active.Disabled);
        [MenuItem(MenuRoot + "Content Minimap", false, 3)]
        private static void ContentMinimap() => Toggle("Content Minimap", config => config.ContentMinimap = !config.ContentMinimap);
        [MenuItem(MenuRoot + "Content Minimap", true)]
        private static bool ContentMinimapValidate() => Check("Content Minimap", ProjectConfigStore.Active.ContentMinimap, !ProjectConfigStore.Active.Disabled);
        [MenuItem(MenuRoot + "Disable Favorites", false, 20)]
        private static void DisableFavorites() => Toggle("Disable Favorites", config => config.DisableFavorites = !config.DisableFavorites);
        [MenuItem(MenuRoot + "Disable Favorites", true)]
        private static bool DisableFavoritesValidate() => Check("Disable Favorites", ProjectConfigStore.Active.DisableFavorites, !ProjectConfigStore.Active.Disabled);
        [MenuItem(MenuRoot + "Favorite Click To Inspect", false, 21)]
        private static void FavoriteClickToInspect() => Toggle("Favorite Click To Inspect", config => config.FavoriteClickToInspect = !config.FavoriteClickToInspect);
        [MenuItem(MenuRoot + "Favorite Click To Inspect", true)]
        private static bool FavoriteClickToInspectValidate() => Check("Favorite Click To Inspect", ProjectConfigStore.Active.FavoriteClickToInspect, !ProjectConfigStore.Active.Disabled && !ProjectConfigStore.Active.DisableFavorites);
        [MenuItem(MenuRoot + "Save Favorites To Project Config", false, 22)]
        private static void SaveFavoritesToProjectConfig() => Toggle("Save Favorites To Project Config", config => config.SaveFavoritesToProjectConfig = !config.SaveFavoritesToProjectConfig);
        [MenuItem(MenuRoot + "Save Favorites To Project Config", true)]
        private static bool SaveFavoritesToProjectConfigValidate() => Check("Save Favorites To Project Config", ProjectConfigStore.Active.SaveFavoritesToProjectConfig, !ProjectConfigStore.Active.Disabled && !ProjectConfigStore.Active.DisableFavorites);
    }
}
