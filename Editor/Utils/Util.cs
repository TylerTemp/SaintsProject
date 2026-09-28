using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SaintsProject.Editor.Utils
{
    public static class Util
    {
        private const string ResourceRoot = "Packages/today.comes.saintsproject/Editor/Editor Default Resources/SaintsFolders/";
        private static readonly Dictionary<string, Texture2D> Icons = new Dictionary<string, Texture2D>();
        public static void ClearCache() => Icons.Clear();
        public static string NormalizeIcon(string icon)
        {
            if (string.IsNullOrEmpty(icon))
            {
                return "";
            }

            string guid = AssetDatabase.AssetPathToGUID(icon);
            return string.IsNullOrEmpty(guid) ? icon : guid;
        }

        public static T LoadResource<T>(string path)
            where T : Object
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            if (typeof(T) == typeof(Texture2D) && Icons.TryGetValue(path, out Texture2D cached))
            {
                return cached as T;
            }

            string assetPath = AssetDatabase.GUIDToAssetPath(path);
            T asset = AssetDatabase.LoadAssetAtPath<T>(string.IsNullOrEmpty(assetPath) ? path : assetPath);
            if (!asset)
            {
                asset = AssetDatabase.LoadAssetAtPath<T>("Assets/Editor Default Resources/SaintsProject/" + path);
            }

            if (!asset)
            {
                asset = AssetDatabase.LoadAssetAtPath<T>(ResourceRoot + path);
            }

            if (!asset)
            {
                asset = EditorGUIUtility.Load(path) as T;
            }

            if (!asset && typeof(T) == typeof(Texture2D))
            {
                // IconContent resolves built-in editor icons which FindTexture cannot find.
                bool logging = Debug.unityLogger.logEnabled;
                try
                {
                    Debug.unityLogger.logEnabled = false;
                    asset = EditorGUIUtility.IconContent(path)?.image as T;
                }
                catch (System.Exception)
                {
                    // Icon availability differs across Unity versions.
                }
                finally
                {
                    Debug.unityLogger.logEnabled = logging;
                }
            }

            if (typeof(T) == typeof(Texture2D))
            {
                Icons[path] = asset as Texture2D;
            }

            return asset;
        }

        public static Texture2D GetIcon(string path) => LoadResource<Texture2D>(path);
    }
}
