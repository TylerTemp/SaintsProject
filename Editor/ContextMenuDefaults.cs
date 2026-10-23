using System;
using System.Collections.Generic;

namespace SaintsProject.Editor
{
    internal static class ContextMenuDefaults
    {
        // Original Unity menu path -> icon resource path. Unavailable paths are ignored.
        internal static readonly Dictionary<string, string> DefaultIcons = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            // Asset actions.
            { "Assets/Add to Assistant", "d_AISparkle Icon" },
            { "Assets/Copy Path", "Copy" },
            { "Assets/Create UPM Package...", "d_Package Manager" },
            { "Assets/Delete", "d_TreeEditor.Trash" },
            { "Assets/Export Asset Package...", "d_SaveAs" },
            { "Assets/Export UPM Package...", "d_SaveAs" },
            { "Assets/Extract Material SubAsset", "d_Material Icon" },
            { "Assets/Find References In Project", "d_Search Icon" },
            { "Assets/Find References In Scene", "d_Search Icon" },
            { "Assets/Generate", "d_AISparkle Icon" },
            { "Assets/Import Asset Package...", "d_Import" },
            { "Assets/Import New Asset...", "d_Import" },
            { "Assets/Open", "d_Open" },
            { "Assets/Open C# Project", "d_cs Script Icon" },
            { "Assets/Open Scene Additive", "d_Open" },
            { "Assets/Properties...", "d_Settings Icon" },
            { "Assets/Refresh", "d_Refresh" },
            { "Assets/Reimport", "d_Refresh" },
            { "Assets/Reimport All", "d_Refresh" },
            { "Assets/Rename", "d_editicon.sml" },
            { "Assets/Search Generated Assets", "d_Search Icon" },
            { "Assets/Select Dependencies", "d_UnityEditor.FindDependencies" },
            { "Assets/Show in Explorer", "d_FolderOpened Icon" },
            { "Assets/Update UXML Schema", "d_Refresh" },
            { "Assets/View in Import Activity Window", "d_BuildAnalysisWindow" },
            { "Assets/View in Package Manager", "d_Package Manager" },

            // General assets.
            { "Assets/Create", "d_CreateAddNew" },
            { "Assets/Create/Custom Font", "d_Font Icon" },
            { "Assets/Create/Folder", "d_Folder Icon" },
            { "Assets/Create/Folder with Selection", "d_FolderOpened Icon" },
            { "Assets/Create/GUI Skin", "d_GUISkin Icon" },
            { "Assets/Create/Input Actions", "Packages/com.unity.inputsystem/InputSystem/Editor/Icons/InputActionAsset.png" },
            { "Assets/Create/Material", "d_Material Icon" },
            { "Assets/Create/MonoBehaviour Script", "d_cs Script Icon" },
            { "Assets/Create/Physics Material", "d_PhysicsMaterial Icon" },
            { "Assets/Create/Prefab Variant", "d_PrefabVariant Icon" },

            // 2D.
            { "Assets/Create/2D", "d_Sprite Icon" },
            { "Assets/Create/2D/Generate Sprite", "d_AISparkle Icon" },
            { "Assets/Create/2D/Physics (Core)", "d_Profiler.Physics2D" },
            { "Assets/Create/2D/Physics (Core)/PhysicsCore Settings 2D", "d_PhysicsCoreSettings2D Icon" },
            { "Assets/Create/2D/Physics Material 2D", "d_PhysicsMaterial2D Icon" },
            { "Assets/Create/2D/Sprite Atlas", "d_SpriteAtlas Icon" },
            { "Assets/Create/2D/Sprites", "d_Sprite Icon" },
            { "Assets/Create/2D/Sprites/9-Sliced", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/9Sliced.png" },
            { "Assets/Create/2D/Sprites/Capsule", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Capsule.png" },
            { "Assets/Create/2D/Sprites/Circle", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Circle.png" },
            { "Assets/Create/2D/Sprites/Hexagon Flat Top", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonFlatTop.png" },
            { "Assets/Create/2D/Sprites/Hexagon Point Top", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonPointTop.png" },
            { "Assets/Create/2D/Sprites/Isometric Diamond", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/IsometricDiamond.png" },
            { "Assets/Create/2D/Sprites/Square", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Square.png" },
            { "Assets/Create/2D/Sprites/Triangle", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Triangle.png" },

            // 3D.
            { "Assets/Create/3D", "d_PreMatCube@2x" },
            { "Assets/Create/3D/Generate 3D Object", "d_AISparkle Icon" },

            // Addressables.
            { "Assets/Create/Addressables", "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Addressables Window/d_AddressablesWindow.png" },
            { "Assets/Create/Addressables/Auto Group Generator/Settings", "d_Settings Icon" },
            { "Assets/Create/Addressables/Content Builders", "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png" },
            { "Assets/Create/Addressables/Content Builders/Default Build Schema Driven", "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png" },
            { "Assets/Create/Addressables/Content Builders/Default Build Script", "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png" },
            { "Assets/Create/Addressables/Content Builders/Use Asset Database (fastest)", "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png" },
            { "Assets/Create/Addressables/Content Builders/Use Existing Build (requires built groups)", "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png" },
            { "Assets/Create/Addressables/Custom Build", "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png" },
            { "Assets/Create/Addressables/Custom Build/Custom Location Build Script", "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png" },
            { "Assets/Create/Addressables/Group Templates", "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Content Directory/d_ContentDirectory.png" },
            { "Assets/Create/Addressables/Group Templates/Blank Group Template", "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Content Directory/d_ContentDirectory.png" },

            // Animation.
            { "Assets/Create/Animation", "d_Animation Icon" },
            { "Assets/Create/Animation/Animation Clip", "d_AnimationClip Icon" },
            { "Assets/Create/Animation/Animator Controller", "d_AnimatorController Icon" },
            { "Assets/Create/Animation/Animator Override Controller", "d_AnimatorOverrideController Icon" },
            { "Assets/Create/Animation/Avatar Mask", "d_AvatarMask Icon" },
            { "Assets/Create/Animation/Generate Animation Clip", "d_AISparkle Icon" },

            // App UI.
            { "Assets/Create/App UI", "d_Canvas Icon" },
            { "Assets/Create/App UI/App UI Settings", "d_Settings Icon" },
            { "Assets/Create/App UI/Icons Style Sheet Asset", "d_StyleSheet Icon" },
            { "Assets/Create/App UI/Navigation Graph", "d_Navigation" },

            // Audio.
            { "Assets/Create/Audio", "d_AudioClip Icon" },
            { "Assets/Create/Audio/Audio Mixer", "d_AudioMixerController Icon" },
            { "Assets/Create/Audio/Audio Random Container", "d_AudioRandomContainer Icon" },
            { "Assets/Create/Audio/Generate Audio Clip", "d_AISparkle Icon" },

            // Debug.
            { "Assets/Create/Debug/Saints Hierarchy Config", "d_Settings Icon" },

            // Netcode.
            { "Assets/Create/Netcode", "d_NetworkManager Icon" },
            { "Assets/Create/Netcode/Network Prefabs List", "d_Prefab Icon" },

            // Plugins.
            { "Assets/Create/Plugins", "d_Assembly Icon" },
            { "Assets/Create/Plugins/Android Library", "d_BuildSettings.Android" },

            // Rendering.
            { "Assets/Create/Rendering", "d_RenderPipelineAsset Icon" },
            { "Assets/Create/Rendering/Custom Render Texture", "d_RenderTexture Icon" },
            { "Assets/Create/Rendering/Device Filters", "d_Filter Icon" },
            { "Assets/Create/Rendering/Device Filters/D3D12 Device Filter", "d_Filter Icon" },
            { "Assets/Create/Rendering/Device Filters/Vulkan Device Filter", "d_Filter Icon" },
            { "Assets/Create/Rendering/Device Filters/WebGPU Device Filter", "d_Filter Icon" },
            { "Assets/Create/Rendering/Generate Cubemap", "d_AISparkle Icon" },
            { "Assets/Create/Rendering/Generate Material", "d_AISparkle Icon" },
            { "Assets/Create/Rendering/Generate Sprite", "d_AISparkle Icon" },
            { "Assets/Create/Rendering/Generate Texture 2D", "d_AISparkle Icon" },
            { "Assets/Create/Rendering/Legacy Cubemap", "d_Cubemap Icon" },
            { "Assets/Create/Rendering/Lens Flare (SRP)", "Packages/com.unity.render-pipelines.core/Editor/PostProcessing/LensFlareResource/Flare128.png" },
            { "Assets/Create/Rendering/Lighting Settings", "d_LightingSettings Icon" },
            { "Assets/Create/Rendering/Lightmap Parameters", "d_LightmapParameters Icon" },
            { "Assets/Create/Rendering/Material", "d_Material Icon" },
            { "Assets/Create/Rendering/Material Variant", "d_MaterialVariant Icon" },
            { "Assets/Create/Rendering/My URP Universal Renderer", "d_RenderPipelineAsset Icon" },
            { "Assets/Create/Rendering/Render Texture", "d_RenderTexture Icon" },
            { "Assets/Create/Rendering/URP 2D Renderer", "d_RenderPipelineAsset Icon" },
            { "Assets/Create/Rendering/URP Asset (with 2D Renderer)", "d_RenderPipelineAsset Icon" },
            { "Assets/Create/Rendering/URP Asset (with Universal Renderer)", "d_RenderPipelineAsset Icon" },
            { "Assets/Create/Rendering/URP Post-process Data", "d_Settings Icon" },
            { "Assets/Create/Rendering/URP Universal Renderer", "d_RenderPipelineAsset Icon" },
            { "Assets/Create/Rendering/Volume Profile", "Packages/com.unity.render-pipelines.core/Editor/Icons/MipLevels/d_VolumeProfile@32.png" },

            // Saints Project.
            { "Assets/Saints Project", "d_FolderFavorite Icon" },
            { "Assets/Saints Project/Add Favorite", "d_Favorite Icon" },
            { "Assets/Saints Project/Edit Appearance", "d_editicon.sml" },

            // Scene.
            { "Assets/Create/Scene", "d_SceneAsset Icon" },
            { "Assets/Create/Scene/Prefab", "d_Prefab Icon" },
            { "Assets/Create/Scene/Prefab Variant", "d_PrefabVariant Icon" },
            { "Assets/Create/Scene/Scene", "d_SceneAsset Icon" },
            { "Assets/Create/Scene/Scene Template", "SceneTemplateAsset Icon" },
            { "Assets/Create/Scene/Scene Template From Scene", "SceneTemplateAsset Icon" },
            { "Assets/Create/Scene/Scene Template Pipeline", "d_cs Script Icon" },

            // Scripting.
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

            // Shader.
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

            // Shader Graph.
            { "Assets/Create/Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/Blank Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/BuiltIn", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/BuiltIn/Canvas Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/BuiltIn/Lit Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/BuiltIn/Unlit Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/Custom Render Texture", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/From Template...", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/Sub Graph", "d_SubgraphAsset" },
            { "Assets/Create/Shader Graph/URP", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/URP/Canvas Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/URP/Decal Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/URP/Fullscreen Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/URP/Lit Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/URP/Six Way Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/URP/Sprite Custom Lit Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/URP/Sprite Lit Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/URP/Sprite Unlit Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/URP/Terrain Lit Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/URP/UI Shader Graph", "d_GraphAsset" },
            { "Assets/Create/Shader Graph/URP/Unlit Shader Graph", "d_GraphAsset" },

            // Spine.
            { "Assets/Create/Spine", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/icon-spine.png" },
            { "Assets/Create/Spine/Animation Reference Asset", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/icon-animation.png" },
            { "Assets/Create/Spine/EventData Reference Asset", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/icon-event.png" },
            { "Assets/Create/Spine/SkeletonData Asset", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/SkeletonDataAsset Icon.png" },
            { "Assets/Create/Spine/SkeletonData Modifiers/Blend Mode Materials", "d_Material Icon" },
            { "Assets/Create/Spine/Spine Atlas Asset", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/AtlasAsset Icon.png" },
            { "Assets/Create/Spine/Spine SpriteAtlas Asset", "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/AtlasAsset Icon.png" },

            // Terrain.
            { "Assets/Create/Terrain", "d_Terrain Icon" },
            { "Assets/Create/Terrain/Brush", "d_TerrainInspector.TerrainToolSplat" },
            { "Assets/Create/Terrain/Generate Terrain Layer", "d_AISparkle Icon" },
            { "Assets/Create/Terrain/Terrain Layer", "d_Terrain Icon" },

            // Testing.
            { "Assets/Create/Testing", "TestStopwatch" },
            { "Assets/Create/Testing/C# Test Script", "d_cs Script Icon" },
            { "Assets/Create/Testing/Tests Assembly Folder", "d_AssemblyDefinitionAsset Icon" },

            // TextMeshPro.
            { "Assets/Create/TextMeshPro", "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" },
            { "Assets/Create/TextMeshPro/Color Gradient", "d_ColorPicker.CycleColor" },
            { "Assets/Create/TextMeshPro/Font Asset", "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" },
            { "Assets/Create/TextMeshPro/Font Asset/Bitmap", "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" },
            { "Assets/Create/TextMeshPro/Font Asset/Color", "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" },
            { "Assets/Create/TextMeshPro/Font Asset/Font Asset Variant", "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" },
            { "Assets/Create/TextMeshPro/Font Asset/SDF", "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" },
            { "Assets/Create/TextMeshPro/Sprite Asset", "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Sprite Asset Icon.psd" },
            { "Assets/Create/TextMeshPro/Style Sheet", "d_StyleSheet Icon" },

            // Timeline.
            { "Assets/Create/Timeline", "d_TimelineAsset Icon" },
            { "Assets/Create/Timeline/Signal", "d_SignalAsset Icon" },
            { "Assets/Create/Timeline/Timeline", "d_TimelineAsset Icon" },

            // UI Toolkit.
            { "Assets/Create/UI Toolkit", "d_Canvas Icon" },
            { "Assets/Create/UI Toolkit/Default Runtime Theme File", "d_ThemeStyleSheet Icon" },
            { "Assets/Create/UI Toolkit/Editor Window", "d_UnityEditor.InspectorWindow" },
            { "Assets/Create/UI Toolkit/Filter Function Definition", "d_Filter Icon" },
            { "Assets/Create/UI Toolkit/Panel Settings Asset", "d_PanelSettings Icon" },
            { "Assets/Create/UI Toolkit/Style Sheet (USS)", "d_StyleSheet Icon" },
            { "Assets/Create/UI Toolkit/TSS Theme File", "d_ThemeStyleSheet Icon" },
            { "Assets/Create/UI Toolkit/Text", "d_FontAsset Icon" },
            { "Assets/Create/UI Toolkit/Text/Color Gradient", "d_ColorPicker.CycleColor" },
            { "Assets/Create/UI Toolkit/Text/Font Asset", "d_FontAsset Icon" },
            { "Assets/Create/UI Toolkit/Text/Font Asset/Bitmap", "d_FontAsset Icon" },
            { "Assets/Create/UI Toolkit/Text/Font Asset/Color", "d_FontAsset Icon" },
            { "Assets/Create/UI Toolkit/Text/Font Asset/SDF", "d_FontAsset Icon" },
            { "Assets/Create/UI Toolkit/Text/Panel Text Settings", "d_PanelTextSettings Icon" },
            { "Assets/Create/UI Toolkit/Text/Sprite Asset", "d_SpriteAtlas Icon" },
            { "Assets/Create/UI Toolkit/Text/Text StyleSheet", "d_StyleSheet Icon" },
            { "Assets/Create/UI Toolkit/UI Animation Clip", "d_UIAnimationClip Icon" },
            { "Assets/Create/UI Toolkit/UI Document (UXML)", "d_VisualTreeAsset Icon" },

            // Visual Scripting.
            { "Assets/Create/Visual Scripting", "Packages/com.unity.visualscripting/Editor/VisualScripting.Core/IconMap/FlowGraph@32x.png" },
            { "Assets/Create/Visual Scripting/Script Graph", "Packages/com.unity.visualscripting/Editor/VisualScripting.Core/IconMap/FlowGraph@32x.png" },
            { "Assets/Create/Visual Scripting/State Graph", "Packages/com.unity.visualscripting/Editor/VisualScripting.Core/IconMap/State@32x.png" },

            // Wwise.
            { "Assets/Wwise", "wwise-logo.png" },
        };
    }
}
