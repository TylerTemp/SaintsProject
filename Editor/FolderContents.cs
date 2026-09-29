#if (SAINTSPROJECT_WWISE || WWISE_2024_OR_LATER || WWISE_2023_OR_LATER || WWISE_2022_OR_LATER || WWISE_2021_OR_LATER || WWISE_2020_OR_LATER || WWISE_2019_OR_LATER) && !SAINTSPROJECT_WWISE_DISABLE
#define USE_WWISE
#endif

using System;
using System.Collections.Generic;
using System.IO;
using SaintsProject.Editor.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SaintsProject.Editor
{
    /// <summary>Direct folder contents, indexed once per asset database change.</summary>
    [InitializeOnLoad]
    internal static class FolderContents
    {
        internal class Summary
        {
            public Texture2D AutoIcon;
            public readonly List<Texture2D> Icons = new List<Texture2D>();
        }

        private static readonly Dictionary<string, List<string>> Children = new Dictionary<string, List<string>>(StringComparer.Ordinal);
        private static readonly Dictionary<string, Summary> Cache = new Dictionary<string, Summary>(StringComparer.Ordinal);
        private static bool _indexed;

        private const string PrefabIconOld = "Prefab Icon";
        private const string PrefabIcon = "d_" + PrefabIconOld;
        private const string TextureIconOld = "Texture Icon";
        private const string TextureIcon = "d_" + TextureIconOld;
        private const string ScriptableObjectIconOld = "ScriptableObject Icon";
        private const string ScriptableObjectIcon = "d_" + ScriptableObjectIconOld;
        private const string CsScriptIconOld = "cs Script Icon";
        private const string CsScriptIcon = "d_" + CsScriptIconOld;
        private const string ShaderIconOld = "Shader Icon";
        private const string ShaderIcon = "d_" + ShaderIconOld;

        private static readonly string[] Order =
        {
            "d_SceneAsset Icon",
            "SceneAsset Icon",

            PrefabIcon,
            PrefabIconOld,

            "d_Mesh Icon",
            "Mesh Icon",

            "d_Material Icon",
            "Material Icon",

            "d_Texture Icon",
            "Texture Icon",

            CsScriptIcon,
            CsScriptIconOld,

            ShaderIcon,
            ShaderIconOld,

            "d_ComputeShader Icon",
            "ComputeShader Icon",

            "d_ShaderInclude Icon",
            "ShaderInclude Icon",

            ScriptableObjectIcon,
            ScriptableObjectIconOld,
        };
        static FolderContents()
        {
            EditorApplication.projectChanged += Invalidate;
        }

        internal static void Invalidate()
        {
            _indexed = false;
            Children.Clear();
            Cache.Clear();
        }

        internal static Summary Get(string folder)
        {
            if (Cache.TryGetValue(folder, out Summary cached))
            {
                return cached;
            }

            if (!_indexed)
            {
                foreach (string path in AssetDatabase.GetAllAssetPaths())
                {
                    int slash = path.LastIndexOf('/');
                    if (slash < 0)
                    {
                        continue;
                    }

                    string parent = path[..slash];
                    if (!Children.TryGetValue(parent, out List<string> children))
                    {
                        children = new List<string>();
                        Children.Add(parent, children);
                    }

                    children.Add(path);
                }

                _indexed = true;
            }

            Summary result = new Summary();
            Cache.Add(folder, result);
            if (!Children.TryGetValue(folder, out List<string> files))
            {
                return result;
            }

            Texture2D common = null;
            bool mixed = false;
            bool containsSpineSkeletonData = false;
            bool containsSpineAtlas = false;

            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (string path in files)
            {
                if (AssetDatabase.IsValidFolder(path))
                {
                    continue;
                }

                Type type = AssetDatabase.GetMainAssetTypeAtPath(path);
                if (type == null)
                {
                    continue;
                }

#if SAINTSPROJECT_SPINE_UNITY
                if (typeof(Spine.Unity.SkeletonDataAsset).IsAssignableFrom(type))
                {
                    containsSpineSkeletonData = true;
                }

                if (typeof(Spine.Unity.SpineAtlasAsset).IsAssignableFrom(type))
                {
                    containsSpineAtlas = true;
                }
#endif

                Texture2D icon = GetTypeIcon(type, path);
                if (common == null)
                {
                    common = icon;
                }
                else if (common != icon)
                {
                    mixed = true;
                }

                if (icon != null && !result.Icons.Contains(icon))
                {
                    result.Icons.Add(icon);
                }
            }

            if (mixed)
            {
#if SAINTSPROJECT_SPINE_UNITY
                if (containsSpineAtlas && containsSpineSkeletonData)
                {
                    result.AutoIcon = AssetPreview.GetMiniTypeThumbnail(typeof(Spine.Unity.SkeletonDataAsset));
                }
#endif
            }
            else
            {
                result.AutoIcon = common;
            }

            foreach (Texture2D texture2D in result.Icons.ToArray())
            {
                if (texture2D.name.Contains(CsScriptIcon) || texture2D.name.Contains(CsScriptIconOld))
                {
                    result.Icons.RemoveAll(each =>
                        each.name is "AssemblyDefinitionAsset Icon" or "d_AssemblyDefinitionAsset Icon");
                }

                if (texture2D.name.Contains(ShaderIcon) || texture2D.name.Contains(ShaderIconOld))
                {
                    result.Icons.RemoveAll(each =>
                        each.name is "ShaderInclude Icon" or "d_ShaderInclude Icon");
                }
            }


            result.Icons.Sort((a, b) =>
            {
                int ai = Array.IndexOf(Order, a.name);
                int bi = Array.IndexOf(Order, b.name);
                ai = ai < 0 ? Order.Length : ai;
                bi = bi < 0 ? Order.Length : bi;
                return ai == bi ? string.CompareOrdinal(a.name, b.name) : ai.CompareTo(bi);
            });
            return result;
        }

        private static Texture2D GetTypeIcon(Type type, string path)
        {
            if (typeof(Texture).IsAssignableFrom(type))
            {
                return Util.LoadIconContent(TextureIcon);
            }

            if (type == typeof(GameObject))
            {
                return Util.LoadIconContent(PrefabIcon);
            }

            string extension = Path.GetExtension(path);
            if (type == typeof(MonoScript) || extension == ".asmdef" || extension == ".asmref")
            {
                return Util.LoadIconContent(CsScriptIcon);
            }

            if (type == typeof(VisualTreeAsset) || extension == ".uxml")
            {
                return Util.LoadIconContent("d_VisualTreeAsset Icon");
            }
            if (type == typeof(StyleSheet) || extension == ".uss")
            {
                return Util.LoadIconContent("d_StyleSheet Icon");
            }

#if USE_WWISE
            if (type == typeof(WwiseEventReference))
            {
                return Util.LoadResource<Texture2D>("Wwise/event_nor.png");
            }
            if (type == typeof(WwiseRtpcReference))
            {
                return Util.LoadResource<Texture2D>("Wwise/gameparameter_nor.png");
            }
            if (type == typeof(WwiseBankReference))
            {
                return Util.LoadResource<Texture2D>("Wwise/soundbank_nor.png");
            }
            if (type == typeof(WwiseSwitchReference))
            {
                return Util.LoadResource<Texture2D>("Wwise/switch_nor.png");
            }
            if (type == typeof(WwiseSwitchGroupReference))
            {
                return Util.LoadResource<Texture2D>("Wwise/switchgroup_nor.png");
            }
#endif

            Texture2D icon = AssetPreview.GetMiniTypeThumbnail(type);
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (icon)
            {
                return icon;
            }

            if (typeof(ScriptableObject).IsAssignableFrom(type))
            {
                return Util.LoadIconContent(ScriptableObjectIcon);
            }

            if (type == typeof(DefaultAsset) || type == typeof(TextAsset))
            {
                return null;
            }

            // return icon.name.StartsWith("d_", StringComparison.Ordinal) ? icon.name.Substring(2) : icon.name;
            return null;
        }
    }
}
