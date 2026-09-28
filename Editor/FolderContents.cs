using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SaintsProject.Editor
{
    /// <summary>Direct folder contents, indexed once per asset database change.</summary>
    [InitializeOnLoad]
    internal static class FolderContents
    {
        internal sealed class Summary
        {
            public string AutoIcon = "";
            public readonly List<string> Icons = new List<string>();
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

            string common = null;
            bool mixed = false;
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

                string icon = IconName(type, path);
                if (common == null)
                {
                    common = icon;
                }
                else if (common != icon)
                {
                    mixed = true;
                }

                if (!string.IsNullOrEmpty(icon) && !result.Icons.Contains(icon))
                {
                    result.Icons.Add(icon);
                }
            }

            if (!mixed)
            {
                result.AutoIcon = common ?? "";
            }

            if (result.Icons.Contains(CsScriptIcon) || result.Icons.Contains(CsScriptIconOld))
            {
                result.Icons.Remove("AssemblyDefinitionAsset Icon");
                result.Icons.Remove("d_AssemblyDefinitionAsset Icon");
            }

            if (result.Icons.Contains(ShaderIcon) || result.Icons.Contains(ShaderIconOld))
            {
                result.Icons.Remove("ShaderInclude Icon");
                result.Icons.Remove("d_ShaderInclude Icon");
            }

            result.Icons.Sort((a, b) =>
            {
                int ai = Array.IndexOf(Order, a);
                int bi = Array.IndexOf(Order, b);
                ai = ai < 0 ? Order.Length : ai;
                bi = bi < 0 ? Order.Length : bi;
                return ai == bi ? string.CompareOrdinal(a, b) : ai.CompareTo(bi);
            });
            return result;
        }

        private static string IconName(Type type, string path)
        {
            if (typeof(Texture).IsAssignableFrom(type))
            {
                return TextureIcon;
            }

            if (type == typeof(GameObject))
            {
                return PrefabIcon;
            }

            string extension = Path.GetExtension(path);
            if (type == typeof(MonoScript) || extension == ".asmdef" || extension == ".asmref")
            {
                return CsScriptIcon;
            }

            if (typeof(ScriptableObject).IsAssignableFrom(type))
            {
                return ScriptableObjectIcon;
            }

            if (type == typeof(DefaultAsset) || type == typeof(TextAsset))
            {
                return "";
            }

            Texture2D icon = AssetPreview.GetMiniTypeThumbnail(type);
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!icon)
            {
                return "";
            }

            // return icon.name.StartsWith("d_", StringComparison.Ordinal) ? icon.name.Substring(2) : icon.name;
            return icon.name;
        }
    }
}
