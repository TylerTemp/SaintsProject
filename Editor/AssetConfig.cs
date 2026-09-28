using System;
using UnityEngine;

namespace SaintsProject.Editor
{
    [Serializable]
    public struct AssetConfig
    {
        public string guid;
        public string icon;
        public bool recursiveIcon;
        public bool hasColor;
        public Color color;
        public bool recursiveColor;
    }

    public enum FavoriteIconType
    {
        Default,
        UnityDefault,
        // None,
        Custom,
    }

    public enum FavoriteColorType
    {
        Default,
        NoColor,
        CustomColor,
    }

    [Serializable]
    public struct AssetFavorite
    {
        public string guid;
        public long localId;
        public string alias;
        public FavoriteIconType iconType;
        public string icon;
        public FavoriteColorType colorType;
        public Color color;
        public bool SameAsset(AssetFavorite other) => guid == other.guid && localId == other.localId;
    }
}
