using System;
using System.Collections.Generic;

namespace SaintsProject.Editor
{
    internal static class ContextMenuDefaults
    {
        // Unity 6 is a coarse compatibility boundary for editor and package icons.
        // Null means no suitable legacy icon; the resolver dismisses the prefix.
        private const string AssistantIcon =
#if UNITY_6000_0_OR_NEWER
            "d_AISparkle Icon"
#else
            null
#endif
            ;

        private const string CopyIcon =
#if UNITY_6000_0_OR_NEWER
            "Copy"
#else
            "d_TreeEditor.Duplicate"
#endif
            ;

        private const string OpenIcon =
#if UNITY_6000_0_OR_NEWER
            "d_Open"
#else
            "d_FolderOpened Icon"
#endif
            ;

        private const string ImportActivityIcon =
#if UNITY_6000_0_OR_NEWER
            "d_BuildAnalysisWindow"
#else
            "d_Import"
#endif
            ;

        private const string PhysicsMaterialIcon =
#if UNITY_6000_0_OR_NEWER
            "d_PhysicsMaterial Icon"
#else
            "d_PhysicMaterial Icon"
#endif
            ;

        private const string PhysicsCoreSettings2DIcon =
#if UNITY_6000_0_OR_NEWER
            "d_PhysicsCoreSettings2D Icon"
#else
            "d_Settings Icon"
#endif
            ;

        private const string HexagonPointTopIcon =
#if UNITY_6000_0_OR_NEWER
            "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonPointTop.png"
#else
            "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonPointedTop.png"
#endif
            ;

        private const string AddressablesIcon =
#if UNITY_6000_0_OR_NEWER
            "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Addressables Window/d_AddressablesWindow.png"
#else
            "Packages/com.unity.addressables/Editor/Icons/AddressableAssetsIconY1756Basic.png"
#endif
            ;

        private const string AssetBundleIcon =
#if UNITY_6000_0_OR_NEWER
            "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png"
#else
            "d_Package Manager"
#endif
            ;

        private const string ContentDirectoryIcon =
#if UNITY_6000_0_OR_NEWER
            "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Content Directory/d_ContentDirectory.png"
#else
            "d_Folder Icon"
#endif
            ;

        private const string AudioRandomContainerIcon =
#if UNITY_6000_0_OR_NEWER
            "d_AudioRandomContainer Icon"
#else
            null
#endif
            ;

        private const string RenderPipelineIcon =
#if UNITY_6000_0_OR_NEWER
            "d_RenderPipelineAsset Icon"
#else
            "d_ScriptableObject Icon"
#endif
            ;

        private const string VolumeProfileIcon =
#if UNITY_6000_0_OR_NEWER
            "Packages/com.unity.render-pipelines.core/Editor/Icons/MipLevels/d_VolumeProfile@32.png"
#else
            "Packages/com.unity.render-pipelines.core/Editor/Volume/Icons/VolumeProfileAsset.png"
#endif
            ;

        private const string ShaderGraphIcon =
#if UNITY_6000_0_OR_NEWER
            "d_GraphAsset"
#else
            "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png"
#endif
            ;

        private const string ShaderSubGraphIcon =
#if UNITY_6000_0_OR_NEWER
            "d_SubgraphAsset"
#else
            "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_subgraph_icon.png"
#endif
            ;

        private const string TmpFontIcon =
#if UNITY_6000_0_OR_NEWER
            "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd"
#else
            "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd"
#endif
            ;

        private const string TmpSpriteIcon =
#if UNITY_6000_0_OR_NEWER
            "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Sprite Asset Icon.psd"
#else
            "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Sprite Asset Icon.psd"
#endif
            ;

        private const string FontAssetIcon =
#if UNITY_6000_0_OR_NEWER
            "d_FontAsset Icon"
#else
            "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd"
#endif
            ;

        private const string PanelTextSettingsIcon =
#if UNITY_6000_0_OR_NEWER
            "d_PanelTextSettings Icon"
#else
            "d_Settings Icon"
#endif
            ;

        private const string UIAnimationClipIcon =
#if UNITY_6000_0_OR_NEWER
            "d_UIAnimationClip Icon"
#else
            null
#endif
            ;

        // Original Unity menu path -> icon resource path. Unavailable paths are ignored.
        internal static readonly Dictionary<string, string> DefaultIcons = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            // Asset actions.
            // Legacy Unity menu paths
            { "Assets/Import Package", "d_Import" },
            { "Assets/Import Package/Custom Package...", "d_Import" },
            { "Assets/Export Package...", "d_SaveAs" },
            { "Assets/Add to Assistant", AssistantIcon },
            { "Assets/Copy Path", CopyIcon },
            { "Assets/Create UPM Package...", "d_Package Manager" },
            { "Assets/Delete", "d_TreeEditor.Trash" },
            { "Assets/Export Asset Package...", "d_SaveAs" },
            { "Assets/Export UPM Package...", "d_SaveAs" },
            { "Assets/Extract Material SubAsset", "d_Material Icon" },
            { "Assets/Find References In Project", "d_Search Icon" },
            { "Assets/Find References In Scene", "d_Search Icon" },
            { "Assets/Generate", AssistantIcon },
            { "Assets/Import Asset Package...", "d_Import" },
            { "Assets/Import New Asset...", "d_Import" },
            { "Assets/Open", OpenIcon },
            { "Assets/Open C# Project", "d_cs Script Icon" },
            { "Assets/Open Scene Additive", OpenIcon },
            { "Assets/Properties...", "d_Settings Icon" },
            { "Assets/Refresh", "d_Refresh" },
            { "Assets/Reimport", "d_Refresh" },
            { "Assets/Reimport All", "d_Refresh" },
            { "Assets/Rename", "d_editicon.sml" },
            { "Assets/Search Generated Assets", "d_Search Icon" },
            { "Assets/Select Dependencies", "d_UnityEditor.FindDependencies" },
            { "Assets/Show in Explorer", "d_FolderOpened Icon" },
            { "Assets/Update UXML Schema", "d_Refresh" },
            { "Assets/View in Import Activity Window", ImportActivityIcon },
            { "Assets/View in Package Manager", "d_Package Manager" },
            // End: Legacy Unity menu paths

            // General assets.
            // Legacy Unity menu paths
            { "Assets/Create/C# Script", "d_cs Script Icon" },
            { "Assets/Create/Physic Material", PhysicsMaterialIcon },
            { "Assets/Create", "d_CreateAddNew" },
            { "Assets/Create/Custom Font", "d_Font Icon" },
            { "Assets/Create/Folder", "d_Folder Icon" },
            { "Assets/Create/Folder with Selection", "d_FolderOpened Icon" },
            { "Assets/Create/GUI Skin", "d_GUISkin Icon" },
            { "Assets/Create/Input Actions", "Packages/com.unity.inputsystem/InputSystem/Editor/Icons/InputActionAsset.png" },
            { "Assets/Create/Material", "d_Material Icon" },
            { "Assets/Create/MonoBehaviour Script", "d_cs Script Icon" },
            { "Assets/Create/Physics Material", PhysicsMaterialIcon },
            { "Assets/Create/Prefab Variant", "d_PrefabVariant Icon" },
            // End: Legacy Unity menu paths

            // 2D.
            // Legacy Unity menu paths
            { "Assets/Create/2D/Sprites/Hexagon Flat-Top", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonFlatTop.png" },
            { "Assets/Create/2D/Sprites/Hexagon Pointed-Top", HexagonPointTopIcon },
            { "Assets/Create/2D", "d_Sprite Icon" },
            { "Assets/Create/2D/Generate Sprite", AssistantIcon },
            { "Assets/Create/2D/Physics (Core)", "d_Profiler.Physics2D" },
            { "Assets/Create/2D/Physics (Core)/PhysicsCore Settings 2D", PhysicsCoreSettings2DIcon },
            { "Assets/Create/2D/Physics Material 2D", "d_PhysicsMaterial2D Icon" },
            { "Assets/Create/2D/Sprite Atlas", "d_SpriteAtlas Icon" },
            { "Assets/Create/2D/Sprites", "d_Sprite Icon" },
            { "Assets/Create/2D/Sprites/9-Sliced", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/9Sliced.png" },
            { "Assets/Create/2D/Sprites/Capsule", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Capsule.png" },
            { "Assets/Create/2D/Sprites/Circle", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Circle.png" },
            { "Assets/Create/2D/Sprites/Hexagon Flat Top", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonFlatTop.png" },
            { "Assets/Create/2D/Sprites/Hexagon Point Top", HexagonPointTopIcon },
            { "Assets/Create/2D/Sprites/Isometric Diamond", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/IsometricDiamond.png" },
            { "Assets/Create/2D/Sprites/Square", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Square.png" },
            { "Assets/Create/2D/Sprites/Triangle", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Triangle.png" },
            // End: Legacy Unity menu paths

            // 3D.
            { "Assets/Create/3D", "d_PreMatCube@2x" },
            { "Assets/Create/3D/Generate 3D Object", AssistantIcon },

            // Addressables.
            { "Assets/Create/Addressables", AddressablesIcon },
            { "Assets/Create/Addressables/Auto Group Generator/Settings", "d_Settings Icon" },
            { "Assets/Create/Addressables/Content Builders", AssetBundleIcon },
            { "Assets/Create/Addressables/Content Builders/Default Build Schema Driven", AssetBundleIcon },
            { "Assets/Create/Addressables/Content Builders/Default Build Script", AssetBundleIcon },
            { "Assets/Create/Addressables/Content Builders/Use Asset Database (fastest)", AssetBundleIcon },
            { "Assets/Create/Addressables/Content Builders/Use Existing Build (requires built groups)", AssetBundleIcon },
            { "Assets/Create/Addressables/Custom Build", AssetBundleIcon },
            { "Assets/Create/Addressables/Custom Build/Custom Location Build Script", AssetBundleIcon },
            { "Assets/Create/Addressables/Group Templates", ContentDirectoryIcon },
            { "Assets/Create/Addressables/Group Templates/Blank Group Template", ContentDirectoryIcon },

            // Animation.
            // Legacy Unity menu paths
            { "Assets/Create/Animator Controller", "d_AnimatorController Icon" },
            { "Assets/Create/Animator Override Controller", "d_AnimatorOverrideController Icon" },
            { "Assets/Create/Avatar Mask", "d_AvatarMask Icon" },
            { "Assets/Create/Animation", "d_Animation Icon" },
            { "Assets/Create/Animation/Animation Clip", "d_AnimationClip Icon" },
            { "Assets/Create/Animation/Animator Controller", "d_AnimatorController Icon" },
            { "Assets/Create/Animation/Animator Override Controller", "d_AnimatorOverrideController Icon" },
            { "Assets/Create/Animation/Avatar Mask", "d_AvatarMask Icon" },
            { "Assets/Create/Animation/Generate Animation Clip", AssistantIcon },
            // End: Legacy Unity menu paths

            // App UI.
            { "Assets/Create/App UI", "d_Canvas Icon" },
            { "Assets/Create/App UI/App UI Settings", "d_Settings Icon" },
            { "Assets/Create/App UI/Icons Style Sheet Asset", "d_StyleSheet Icon" },
            { "Assets/Create/App UI/Navigation Graph", "d_Navigation" },

            // Audio.
            // Legacy Unity menu paths
            { "Assets/Create/Audio Mixer", "d_AudioMixerController Icon" },
            { "Assets/Create/Audio", "d_AudioClip Icon" },
            { "Assets/Create/Audio/Audio Mixer", "d_AudioMixerController Icon" },
            { "Assets/Create/Audio/Audio Random Container", AudioRandomContainerIcon },
            { "Assets/Create/Audio/Generate Audio Clip", AssistantIcon },
            // End: Legacy Unity menu paths

            // Debug.
            { "Assets/Create/Debug/Saints Hierarchy Config", "d_Settings Icon" },

            // Netcode.
            { "Assets/Create/Netcode", "d_NetworkManager Icon" },
            { "Assets/Create/Netcode/Network Prefabs List", "d_Prefab Icon" },

            // Plugins.
            { "Assets/Create/Plugins", "d_Assembly Icon" },
            { "Assets/Create/Plugins/Android Library", "d_BuildSettings.Android" },

            // Rendering.
            // Legacy Unity menu paths
            { "Assets/Create/Custom Render Texture", "d_RenderTexture Icon" },
            { "Assets/Create/Lens Flare", "d_Flare Icon" },
            { "Assets/Create/Lens Flare (SRP)", "Packages/com.unity.render-pipelines.core/Editor/PostProcessing/LensFlareResource/Flare128.png" },
            { "Assets/Create/Lighting Settings", "d_LightingSettings Icon" },
            { "Assets/Create/Lightmap Parameters", "d_LightmapParameters Icon" },
            { "Assets/Create/Material Variant", "d_MaterialVariant Icon" },
            { "Assets/Create/Render Texture", "d_RenderTexture Icon" },
            { "Assets/Create/Volume Profile", VolumeProfileIcon },
            { "Assets/Create/Legacy", "d_Cubemap Icon" },
            { "Assets/Create/Legacy/Cubemap", "d_Cubemap Icon" },
            { "Assets/Create/Rendering", RenderPipelineIcon },
            { "Assets/Create/Rendering/Custom Render Texture", "d_RenderTexture Icon" },
            { "Assets/Create/Rendering/Device Filters", "d_Filter Icon" },
            { "Assets/Create/Rendering/Device Filters/D3D12 Device Filter", "d_Filter Icon" },
            { "Assets/Create/Rendering/Device Filters/Vulkan Device Filter", "d_Filter Icon" },
            { "Assets/Create/Rendering/Device Filters/WebGPU Device Filter", "d_Filter Icon" },
            { "Assets/Create/Rendering/Generate Cubemap", AssistantIcon },
            { "Assets/Create/Rendering/Generate Material", AssistantIcon },
            { "Assets/Create/Rendering/Generate Sprite", AssistantIcon },
            { "Assets/Create/Rendering/Generate Texture 2D", AssistantIcon },
            { "Assets/Create/Rendering/Legacy Cubemap", "d_Cubemap Icon" },
            { "Assets/Create/Rendering/Lens Flare (SRP)", "Packages/com.unity.render-pipelines.core/Editor/PostProcessing/LensFlareResource/Flare128.png" },
            { "Assets/Create/Rendering/Lighting Settings", "d_LightingSettings Icon" },
            { "Assets/Create/Rendering/Lightmap Parameters", "d_LightmapParameters Icon" },
            { "Assets/Create/Rendering/Material", "d_Material Icon" },
            { "Assets/Create/Rendering/Material Variant", "d_MaterialVariant Icon" },
            { "Assets/Create/Rendering/My URP Universal Renderer", RenderPipelineIcon },
            { "Assets/Create/Rendering/Render Texture", "d_RenderTexture Icon" },
            { "Assets/Create/Rendering/URP 2D Renderer", RenderPipelineIcon },
            { "Assets/Create/Rendering/URP Asset (with 2D Renderer)", RenderPipelineIcon },
            { "Assets/Create/Rendering/URP Asset (with Universal Renderer)", RenderPipelineIcon },
            { "Assets/Create/Rendering/URP Post-process Data", "d_Settings Icon" },
            { "Assets/Create/Rendering/URP Universal Renderer", RenderPipelineIcon },
            { "Assets/Create/Rendering/Volume Profile", VolumeProfileIcon },
            // End: Legacy Unity menu paths

            // Saints Project.
            { "Assets/Saints Project", "d_FolderFavorite Icon" },
            { "Assets/Saints Project/Add Favorite", "d_Favorite Icon" },
            { "Assets/Saints Project/Edit Appearance", "d_editicon.sml" },

            // Scene.
            // Legacy Unity menu paths
            { "Assets/Create/Prefab", "d_Prefab Icon" },
            { "Assets/Create/Scene Template", "SceneTemplateAsset Icon" },
            { "Assets/Create/Scene Template From Scene", "SceneTemplateAsset Icon" },
            { "Assets/Create/Scene Template Pipeline", "d_cs Script Icon" },
            { "Assets/Create/Scene", "d_SceneAsset Icon" },
            { "Assets/Create/Scene/Prefab", "d_Prefab Icon" },
            { "Assets/Create/Scene/Prefab Variant", "d_PrefabVariant Icon" },
            { "Assets/Create/Scene/Scene", "d_SceneAsset Icon" },
            { "Assets/Create/Scene/Scene Template", "SceneTemplateAsset Icon" },
            { "Assets/Create/Scene/Scene Template From Scene", "SceneTemplateAsset Icon" },
            { "Assets/Create/Scene/Scene Template Pipeline", "d_cs Script Icon" },
            // End: Legacy Unity menu paths

            // Scripting.
            // Legacy Unity menu paths
            { "Assets/Create/Assembly Definition", "d_AssemblyDefinitionAsset Icon" },
            { "Assets/Create/Assembly Definition Reference", "d_AssemblyDefinitionReferenceAsset Icon" },
            { "Assets/Create/Playables", "d_PlayableDirector Icon" },
            { "Assets/Create/Playables/Playable Asset C# Script", "d_cs Script Icon" },
            { "Assets/Create/Playables/Playable Behaviour C# Script", "d_cs Script Icon" },
            { "Assets/Create/Rendering/URP Renderer Feature", "d_cs Script Icon" },
            { "Assets/Create/Scripting", "d_cs Script Icon" },
            { "Assets/Create/Scripting/Assembly Definition", "d_AssemblyDefinitionAsset Icon" },
            { "Assets/Create/Scripting/Assembly Definition Reference", "d_AssemblyDefinitionReferenceAsset Icon" },
            { "Assets/Create/Scripting/Empty C# Script", "dll Script Icon" },
            { "Assets/Create/Scripting/MonoBehaviour Script", "d_cs Script Icon" },
            { "Assets/Create/Scripting/Playables", "d_PlayableDirector Icon" },
            { "Assets/Create/Scripting/Playables/Playable Asset Script", "d_cs Script Icon" },
            { "Assets/Create/Scripting/Playables/Playable Behaviour Script", "d_cs Script Icon" },
            { "Assets/Create/Scripting/ScriptableObject Script", "d_ScriptableObject Icon" },
            { "Assets/Create/Scripting/URP Post-process Volume Scripts", "d_cs Script Icon" },
            { "Assets/Create/Scripting/URP Renderer Feature Script", "d_cs Script Icon" },
            // End: Legacy Unity menu paths

            // Shader.
            // Legacy Unity menu paths
            { "Assets/Create/Shader Variant Collection", "d_ShaderVariantCollection Icon" },
            { "Assets/Create/Shader", "d_Shader Icon" },
            { "Assets/Create/Shader/Compute Shader", "d_ComputeShader Icon" },
            { "Assets/Create/Shader/Custom Render Texture", "d_Shader Icon" },
            { "Assets/Create/Shader/Empty HLSL", "d_ShaderInclude Icon" },
            { "Assets/Create/Shader/Graphics State Collection", "d_ShaderVariantCollection Icon" },
            { "Assets/Create/Shader/Ray Tracing Shader", "d_RayTracingShader Icon" },
            { "Assets/Create/Shader/SRP Blit Shader", "d_Shader Icon" },
            { "Assets/Create/Shader/Shader Variant Collection", "d_ShaderVariantCollection Icon" },
            { "Assets/Create/Shader/URP Unlit Shader", "d_Shader Icon" },
            { "Assets/Create/Shader/Unified Ray Tracing Shader", "d_RayTracingShader Icon" },
            // End: Legacy Unity menu paths

            // Shader Graph.
            { "Assets/Create/Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/Blank Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/BuiltIn", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/BuiltIn/Canvas Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/BuiltIn/Lit Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/BuiltIn/Unlit Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/Custom Render Texture", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/From Template...", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/Sub Graph", ShaderSubGraphIcon },
            { "Assets/Create/Shader Graph/URP", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/URP/Canvas Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/URP/Decal Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/URP/Fullscreen Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/URP/Lit Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/URP/Six Way Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/URP/Sprite Custom Lit Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/URP/Sprite Lit Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/URP/Sprite Unlit Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/URP/Terrain Lit Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/URP/UI Shader Graph", ShaderGraphIcon },
            { "Assets/Create/Shader Graph/URP/Unlit Shader Graph", ShaderGraphIcon },

            // Spine.
            { "Assets/Create/Spine", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/icon-spine.png" },
            { "Assets/Create/Spine/Animation Reference Asset", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/icon-animation.png" },
            { "Assets/Create/Spine/EventData Reference Asset", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/icon-event.png" },
            { "Assets/Create/Spine/SkeletonData Asset", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/SkeletonDataAsset Icon.png" },
            { "Assets/Create/Spine/SkeletonData Modifiers/Blend Mode Materials", "d_Material Icon" },
            { "Assets/Create/Spine/Spine Atlas Asset", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/AtlasAsset Icon.png" },
            { "Assets/Create/Spine/Spine SpriteAtlas Asset", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/AtlasAsset Icon.png" },

            // Terrain.
            // Legacy Unity menu paths
            { "Assets/Create/Brush", "d_TerrainInspector.TerrainToolSplat" },
            { "Assets/Create/Terrain Layer", "d_Terrain Icon" },
            { "Assets/Create/Terrain", "d_Terrain Icon" },
            { "Assets/Create/Terrain/Brush", "d_TerrainInspector.TerrainToolSplat" },
            { "Assets/Create/Terrain/Generate Terrain Layer", AssistantIcon },
            { "Assets/Create/Terrain/Terrain Layer", "d_Terrain Icon" },
            // End: Legacy Unity menu paths

            // Testing.
            { "Assets/Create/Testing", "TestStopwatch" },
            { "Assets/Create/Testing/C# Test Script", "d_cs Script Icon" },
            { "Assets/Create/Testing/Tests Assembly Folder", "d_AssemblyDefinitionAsset Icon" },

            // TextMeshPro.
            // Legacy Unity menu paths
            { "Assets/Create/TextMeshPro/Font Asset Variant", TmpFontIcon },
            { "Assets/Create/Text", TmpFontIcon },
            { "Assets/Create/Text/Font Asset", TmpFontIcon },
            { "Assets/Create/Text/Font Asset Variant", TmpFontIcon },
            { "Assets/Create/Text/Sprite Asset", TmpSpriteIcon },
            { "Assets/Create/Text/Text StyleSheet", "d_StyleSheet Icon" },
            { "Assets/Create/Text/Color Gradient", "d_ColorPicker.CycleColor" },
            { "Assets/Create/TextMeshPro", TmpFontIcon },
            { "Assets/Create/TextMeshPro/Color Gradient", "d_ColorPicker.CycleColor" },
            { "Assets/Create/TextMeshPro/Font Asset", TmpFontIcon },
            { "Assets/Create/TextMeshPro/Font Asset/Bitmap", TmpFontIcon },
            { "Assets/Create/TextMeshPro/Font Asset/Color", TmpFontIcon },
            { "Assets/Create/TextMeshPro/Font Asset/Font Asset Variant", TmpFontIcon },
            { "Assets/Create/TextMeshPro/Font Asset/SDF", TmpFontIcon },
            { "Assets/Create/TextMeshPro/Sprite Asset", TmpSpriteIcon },
            { "Assets/Create/TextMeshPro/Style Sheet", "d_StyleSheet Icon" },
            // End: Legacy Unity menu paths

            // Timeline.
            // Legacy Unity menu paths
            { "Assets/Create/Signal", "d_SignalAsset Icon" },
            { "Assets/Create/Timeline", "d_TimelineAsset Icon" },
            { "Assets/Create/Timeline/Signal", "d_SignalAsset Icon" },
            { "Assets/Create/Timeline/Timeline", "d_TimelineAsset Icon" },
            // End: Legacy Unity menu paths

            // UI Toolkit.
            // Legacy Unity menu paths
            { "Assets/Create/UI Toolkit/Style Sheet", "d_StyleSheet Icon" },
            { "Assets/Create/UI Toolkit/UI Document", "d_VisualTreeAsset Icon" },
            { "Assets/Create/UI Toolkit/Text Settings", PanelTextSettingsIcon },
            { "Assets/Create/UI Toolkit", "d_Canvas Icon" },
            { "Assets/Create/UI Toolkit/Default Runtime Theme File", "d_ThemeStyleSheet Icon" },
            { "Assets/Create/UI Toolkit/Editor Window", "d_UnityEditor.InspectorWindow" },
            { "Assets/Create/UI Toolkit/Filter Function Definition", "d_Filter Icon" },
            { "Assets/Create/UI Toolkit/Panel Settings Asset", "d_PanelSettings Icon" },
            { "Assets/Create/UI Toolkit/Style Sheet (USS)", "d_StyleSheet Icon" },
            { "Assets/Create/UI Toolkit/TSS Theme File", "d_ThemeStyleSheet Icon" },
            { "Assets/Create/UI Toolkit/Text", FontAssetIcon },
            { "Assets/Create/UI Toolkit/Text/Color Gradient", "d_ColorPicker.CycleColor" },
            { "Assets/Create/UI Toolkit/Text/Font Asset", FontAssetIcon },
            { "Assets/Create/UI Toolkit/Text/Font Asset/Bitmap", FontAssetIcon },
            { "Assets/Create/UI Toolkit/Text/Font Asset/Color", FontAssetIcon },
            { "Assets/Create/UI Toolkit/Text/Font Asset/SDF", FontAssetIcon },
            { "Assets/Create/UI Toolkit/Text/Panel Text Settings", PanelTextSettingsIcon },
            { "Assets/Create/UI Toolkit/Text/Sprite Asset", "d_SpriteAtlas Icon" },
            { "Assets/Create/UI Toolkit/Text/Text StyleSheet", "d_StyleSheet Icon" },
            { "Assets/Create/UI Toolkit/UI Animation Clip", UIAnimationClipIcon },
            { "Assets/Create/UI Toolkit/UI Document (UXML)", "d_VisualTreeAsset Icon" },
            // End: Legacy Unity menu paths

            // Visual Scripting.
            { "Assets/Create/Visual Scripting", "Packages/com.unity.visualscripting/Editor/VisualScripting.Core/IconMap/FlowGraph@32x.png" },
            { "Assets/Create/Visual Scripting/Script Graph", "Packages/com.unity.visualscripting/Editor/VisualScripting.Core/IconMap/FlowGraph@32x.png" },
            { "Assets/Create/Visual Scripting/State Graph", "Packages/com.unity.visualscripting/Editor/VisualScripting.Core/IconMap/State@32x.png" },

            // Wwise.
            { "Assets/Wwise", "wwise-logo.png" },
        };
    }
}
