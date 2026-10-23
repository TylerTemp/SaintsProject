using System;
using System.Collections.Generic;

namespace SaintsProject.Editor
{
    internal static class ContextMenuDefaults
    {
        // Try candidates in order; missing editor/package resources fall through.
        // Prefer pro icons when light and dark variants exist.
        // Original Unity menu path -> ordered icon resource candidates. Unavailable paths are ignored.
        internal static readonly Dictionary<string, string[]> DefaultIcons = new Dictionary<string, string[]>(StringComparer.Ordinal)
        {
            // Asset actions.
            { "Assets/Export As Asset Package...", new[] { "d_SaveAs" } },
            { "Assets/Export As UPM Package...", new[] { "d_SaveAs" } },
            // Legacy Unity menu paths
            { "Assets/Import Package", new[] { "d_Import" } },
            { "Assets/Import Package/Custom Package...", new[] { "d_Import" } },
            { "Assets/Export Package...", new[] { "d_SaveAs" } },
            { "Assets/Add to Assistant", new[] { "d_AISparkle Icon" } },
            { "Assets/Copy Path", new[] { "Copy", "d_TreeEditor.Duplicate" } },
            { "Assets/Create UPM Package...", new[] { "d_Package Manager" } },
            { "Assets/Delete", new[] { "d_TreeEditor.Trash" } },
            { "Assets/Export Asset Package...", new[] { "d_SaveAs" } },
            { "Assets/Export UPM Package...", new[] { "d_SaveAs" } },
            { "Assets/Extract Material SubAsset", new[] { "d_Material Icon" } },
            { "Assets/Find References In Project", new[] { "d_Search Icon" } },
            { "Assets/Find References In Scene", new[] { "d_Search Icon" } },
            { "Assets/Generate", new[] { "d_AISparkle Icon" } },
            { "Assets/Import Asset Package...", new[] { "d_Import" } },
            { "Assets/Import New Asset...", new[] { "d_Import" } },
            { "Assets/Open", new[] { "d_Open", "d_FolderOpened Icon" } },
            { "Assets/Open C# Project", new[] { "d_cs Script Icon" } },
            { "Assets/Open Scene Additive", new[] { "d_Open", "d_FolderOpened Icon" } },
            { "Assets/Properties...", new[] { "d_Settings Icon" } },
            { "Assets/Refresh", new[] { "d_Refresh" } },
            { "Assets/Reimport", new[] { "d_Refresh" } },
            { "Assets/Reimport All", new[] { "d_Refresh" } },
            { "Assets/Rename", new[] { "d_editicon.sml" } },
            { "Assets/Search Generated Assets", new[] { "d_Search Icon" } },
            { "Assets/Select Dependencies", new[] { "d_UnityEditor.FindDependencies" } },
            { "Assets/Show in Explorer", new[] { "d_FolderOpened Icon" } },
            { "Assets/Update UXML Schema", new[] { "d_Refresh" } },
            { "Assets/View in Import Activity Window", new[] { "d_BuildAnalysisWindow", "d_Import" } },
            { "Assets/View in Package Manager", new[] { "d_Package Manager" } },
            // End: Legacy Unity menu paths

            // General assets.
            // Legacy Unity menu paths
            { "Assets/Create/C# Script", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Physic Material", new[] { "d_PhysicsMaterial Icon", "d_PhysicMaterial Icon" } },
            { "Assets/Create", new[] { "d_CreateAddNew" } },
            { "Assets/Create/Custom Font", new[] { "d_Font Icon" } },
            { "Assets/Create/Folder", new[] { "d_Folder Icon" } },
            { "Assets/Create/Folder with Selection", new[] { "d_FolderOpened Icon" } },
            { "Assets/Create/GUI Skin", new[] { "d_GUISkin Icon" } },
            { "Assets/Create/Input Actions", new[] { "Packages/com.unity.inputsystem/InputSystem/Editor/Icons/InputActionAsset.png" } },
            { "Assets/Create/Material", new[] { "d_Material Icon" } },
            { "Assets/Create/MonoBehaviour Script", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Physics Material", new[] { "d_PhysicsMaterial Icon", "d_PhysicMaterial Icon" } },
            { "Assets/Create/Prefab Variant", new[] { "d_PrefabVariant Icon" } },
            // End: Legacy Unity menu paths

            // 2D.
            { "Assets/Create/2D/Physics LowLevel Settings", new[] { "d_PhysicsCoreSettings2D Icon", "d_PhysicsLowLevelSettings2D Icon", "d_Settings Icon" } },
            // Legacy Unity menu paths
            { "Assets/Create/2D/Sprites/Hexagon Flat-Top", new[] { "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonFlatTop.png" } },
            { "Assets/Create/2D/Sprites/Hexagon Pointed-Top", new[] { "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonPointTop.png", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonPointedTop.png" } },
            { "Assets/Create/2D", new[] { "d_Sprite Icon" } },
            { "Assets/Create/2D/Generate Sprite", new[] { "d_AISparkle Icon" } },
            { "Assets/Create/2D/Physics (Core)", new[] { "d_Profiler.Physics2D" } },
            { "Assets/Create/2D/Physics (Core)/PhysicsCore Settings 2D", new[] { "d_PhysicsCoreSettings2D Icon", "d_PhysicsLowLevelSettings2D Icon", "d_Settings Icon" } },
            { "Assets/Create/2D/Physics Material 2D", new[] { "d_PhysicsMaterial2D Icon" } },
            { "Assets/Create/2D/Sprite Atlas", new[] { "d_SpriteAtlas Icon" } },
            { "Assets/Create/2D/Sprites", new[] { "d_Sprite Icon" } },
            { "Assets/Create/2D/Sprites/9-Sliced", new[] { "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/9Sliced.png" } },
            { "Assets/Create/2D/Sprites/Capsule", new[] { "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Capsule.png" } },
            { "Assets/Create/2D/Sprites/Circle", new[] { "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Circle.png" } },
            { "Assets/Create/2D/Sprites/Hexagon Flat Top", new[] { "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonFlatTop.png" } },
            { "Assets/Create/2D/Sprites/Hexagon Point Top", new[] { "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonPointTop.png", "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/HexagonPointedTop.png" } },
            { "Assets/Create/2D/Sprites/Isometric Diamond", new[] { "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/IsometricDiamond.png" } },
            { "Assets/Create/2D/Sprites/Square", new[] { "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Square.png" } },
            { "Assets/Create/2D/Sprites/Triangle", new[] { "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/Triangle.png" } },
            // End: Legacy Unity menu paths

            // 3D.
            { "Assets/Create/3D", new[] { "d_PreMatCube@2x" } },
            { "Assets/Create/3D/Generate 3D Object", new[] { "d_AISparkle Icon" } },

            // Addressables.
            { "Assets/Create/Addressables", new[] { "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Addressables Window/d_AddressablesWindow.png", "Packages/com.unity.addressables/Editor/Icons/AddressableAssetsIconY1756Basic.png" } },
            { "Assets/Create/Addressables/Auto Group Generator/Settings", new[] { "d_Settings Icon" } },
            { "Assets/Create/Addressables/Content Builders", new[] { "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png", "d_Package Manager" } },
            { "Assets/Create/Addressables/Content Builders/Default Build Schema Driven", new[] { "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png", "d_Package Manager" } },
            { "Assets/Create/Addressables/Content Builders/Default Build Script", new[] { "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png", "d_Package Manager" } },
            { "Assets/Create/Addressables/Content Builders/Use Asset Database (fastest)", new[] { "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png", "d_Package Manager" } },
            { "Assets/Create/Addressables/Content Builders/Use Existing Build (requires built groups)", new[] { "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png", "d_Package Manager" } },
            { "Assets/Create/Addressables/Custom Build", new[] { "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png", "d_Package Manager" } },
            { "Assets/Create/Addressables/Custom Build/Custom Location Build Script", new[] { "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Asset Bundle/d_AssetBundle.png", "d_Package Manager" } },
            { "Assets/Create/Addressables/Group Templates", new[] { "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Content Directory/d_ContentDirectory.png", "d_Folder Icon" } },
            { "Assets/Create/Addressables/Group Templates/Blank Group Template", new[] { "Packages/com.unity.addressables/Editor/Icons/Groups Window/Dark Theme/Content Directory/d_ContentDirectory.png", "d_Folder Icon" } },

            // Animation.
            // Legacy Unity menu paths
            { "Assets/Create/Animator Controller", new[] { "d_AnimatorController Icon" } },
            { "Assets/Create/Animator Override Controller", new[] { "d_AnimatorOverrideController Icon" } },
            { "Assets/Create/Avatar Mask", new[] { "d_AvatarMask Icon" } },
            { "Assets/Create/Animation", new[] { "d_Animation Icon" } },
            { "Assets/Create/Animation/Animation Clip", new[] { "d_AnimationClip Icon" } },
            { "Assets/Create/Animation/Animator Controller", new[] { "d_AnimatorController Icon" } },
            { "Assets/Create/Animation/Animator Override Controller", new[] { "d_AnimatorOverrideController Icon" } },
            { "Assets/Create/Animation/Avatar Mask", new[] { "d_AvatarMask Icon" } },
            { "Assets/Create/Animation/Generate Animation Clip", new[] { "d_AISparkle Icon" } },
            // End: Legacy Unity menu paths

            // App UI.
            { "Assets/Create/App UI", new[] { "d_Canvas Icon" } },
            { "Assets/Create/App UI/App UI Settings", new[] { "d_Settings Icon" } },
            { "Assets/Create/App UI/Icons Style Sheet Asset", new[] { "d_StyleSheet Icon" } },
            { "Assets/Create/App UI/Navigation Graph", new[] { "d_Navigation" } },

            // Audio.
            // Legacy Unity menu paths
            { "Assets/Create/Audio Mixer", new[] { "d_AudioMixerController Icon" } },
            { "Assets/Create/Audio", new[] { "d_AudioClip Icon" } },
            { "Assets/Create/Audio/Audio Mixer", new[] { "d_AudioMixerController Icon" } },
            { "Assets/Create/Audio/Audio Random Container", new[] { "d_AudioRandomContainer Icon" } },
            { "Assets/Create/Audio/Generate Audio Clip", new[] { "d_AISparkle Icon" } },
            // End: Legacy Unity menu paths

            // Netcode.
            { "Assets/Create/Netcode", new[] { "d_NetworkManager Icon" } },
            { "Assets/Create/Netcode/Network Prefabs List", new[] { "d_Prefab Icon" } },

            // Plugins.
            { "Assets/Create/Plugins", new[] { "d_Assembly Icon" } },
            { "Assets/Create/Plugins/Android Library", new[] { "d_BuildSettings.Android" } },

            // Rendering.
            { "Assets/Create/D3D12 Settings", new[] { "d_Filter Icon" } },
            { "Assets/Create/D3D12 Settings/Device Filter", new[] { "d_Filter Icon" } },
            { "Assets/Create/Vulkan Settings", new[] { "d_Filter Icon" } },
            { "Assets/Create/Vulkan Settings/Device Filter", new[] { "d_Filter Icon" } },
            { "Assets/Create/Rendering/Environment Library (Look Dev)", new[] { "d_Cubemap Icon" } },
            { "Assets/Create/Rendering/Lens Flare", new[] { "d_Flare Icon" } },
            // Legacy Unity menu paths
            { "Assets/Create/Custom Render Texture", new[] { "d_RenderTexture Icon" } },
            { "Assets/Create/Lens Flare", new[] { "d_Flare Icon" } },
            { "Assets/Create/Lens Flare (SRP)", new[] { "Packages/com.unity.render-pipelines.core/Editor/PostProcessing/LensFlareResource/Flare128.png" } },
            { "Assets/Create/Lighting Settings", new[] { "d_LightingSettings Icon" } },
            { "Assets/Create/Lightmap Parameters", new[] { "d_LightmapParameters Icon" } },
            { "Assets/Create/Material Variant", new[] { "d_MaterialVariant Icon" } },
            { "Assets/Create/Render Texture", new[] { "d_RenderTexture Icon" } },
            { "Assets/Create/Volume Profile", new[] { "Packages/com.unity.render-pipelines.core/Editor/Icons/MipLevels/d_VolumeProfile@32.png", "Packages/com.unity.render-pipelines.core/Editor/Volume/Icons/VolumeProfileAsset.png" } },
            { "Assets/Create/Legacy", new[] { "d_Cubemap Icon" } },
            { "Assets/Create/Legacy/Cubemap", new[] { "d_Cubemap Icon" } },
            { "Assets/Create/Rendering", new[] { "d_RenderPipelineAsset Icon", "d_ScriptableObject Icon" } },
            { "Assets/Create/Rendering/Custom Render Texture", new[] { "d_RenderTexture Icon" } },
            { "Assets/Create/Rendering/Device Filters", new[] { "d_Filter Icon" } },
            { "Assets/Create/Rendering/Device Filters/D3D12 Device Filter", new[] { "d_Filter Icon" } },
            { "Assets/Create/Rendering/Device Filters/Vulkan Device Filter", new[] { "d_Filter Icon" } },
            { "Assets/Create/Rendering/Device Filters/WebGPU Device Filter", new[] { "d_Filter Icon" } },
            { "Assets/Create/Rendering/Generate Cubemap", new[] { "d_AISparkle Icon" } },
            { "Assets/Create/Rendering/Generate Material", new[] { "d_AISparkle Icon" } },
            { "Assets/Create/Rendering/Generate Sprite", new[] { "d_AISparkle Icon" } },
            { "Assets/Create/Rendering/Generate Texture 2D", new[] { "d_AISparkle Icon" } },
            { "Assets/Create/Rendering/Legacy Cubemap", new[] { "d_Cubemap Icon" } },
            { "Assets/Create/Rendering/Lens Flare (SRP)", new[] { "Packages/com.unity.render-pipelines.core/Editor/PostProcessing/LensFlareResource/Flare128.png" } },
            { "Assets/Create/Rendering/Lighting Settings", new[] { "d_LightingSettings Icon" } },
            { "Assets/Create/Rendering/Lightmap Parameters", new[] { "d_LightmapParameters Icon" } },
            { "Assets/Create/Rendering/Material", new[] { "d_Material Icon" } },
            { "Assets/Create/Rendering/Material Variant", new[] { "d_MaterialVariant Icon" } },
            { "Assets/Create/Rendering/My URP Universal Renderer", new[] { "d_RenderPipelineAsset Icon", "d_ScriptableObject Icon" } },
            { "Assets/Create/Rendering/Render Texture", new[] { "d_RenderTexture Icon" } },
            { "Assets/Create/Rendering/URP 2D Renderer", new[] { "d_RenderPipelineAsset Icon", "d_ScriptableObject Icon" } },
            { "Assets/Create/Rendering/URP Asset (with 2D Renderer)", new[] { "d_RenderPipelineAsset Icon", "d_ScriptableObject Icon" } },
            { "Assets/Create/Rendering/URP Asset (with Universal Renderer)", new[] { "d_RenderPipelineAsset Icon", "d_ScriptableObject Icon" } },
            { "Assets/Create/Rendering/URP Post-process Data", new[] { "d_Settings Icon" } },
            { "Assets/Create/Rendering/URP Universal Renderer", new[] { "d_RenderPipelineAsset Icon", "d_ScriptableObject Icon" } },
            { "Assets/Create/Rendering/Volume Profile", new[] { "Packages/com.unity.render-pipelines.core/Editor/Icons/MipLevels/d_VolumeProfile@32.png", "Packages/com.unity.render-pipelines.core/Editor/Volume/Icons/VolumeProfileAsset.png" } },
            // End: Legacy Unity menu paths

            // Saints Project.
            { "Assets/Saints Project", new[] { "d_FolderFavorite Icon" } },
            { "Assets/Saints Project/Add Favorite", new[] { "d_Favorite Icon" } },
            { "Assets/Saints Project/Edit Appearance", new[] { "d_editicon.sml" } },
            { "Assets/Saints Project/Edit Context Menu", new[] { "d__Menu", "d__Popup", "d_Settings Icon" } },

            // Scene.
            // Legacy Unity menu paths
            { "Assets/Create/Prefab", new[] { "d_Prefab Icon" } },
            { "Assets/Create/Scene Template", new[] { "SceneTemplateAsset Icon" } },
            { "Assets/Create/Scene Template From Scene", new[] { "SceneTemplateAsset Icon" } },
            { "Assets/Create/Scene Template Pipeline", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Scene", new[] { "d_SceneAsset Icon" } },
            { "Assets/Create/Scene/Prefab", new[] { "d_Prefab Icon" } },
            { "Assets/Create/Scene/Prefab Variant", new[] { "d_PrefabVariant Icon" } },
            { "Assets/Create/Scene/Scene", new[] { "d_SceneAsset Icon" } },
            { "Assets/Create/Scene/Scene Template", new[] { "SceneTemplateAsset Icon" } },
            { "Assets/Create/Scene/Scene Template From Scene", new[] { "SceneTemplateAsset Icon" } },
            { "Assets/Create/Scene/Scene Template Pipeline", new[] { "d_cs Script Icon" } },
            // End: Legacy Unity menu paths

            // Scripting.
            // Legacy Unity menu paths
            { "Assets/Create/Assembly Definition", new[] { "d_AssemblyDefinitionAsset Icon" } },
            { "Assets/Create/Assembly Definition Reference", new[] { "d_AssemblyDefinitionReferenceAsset Icon" } },
            { "Assets/Create/Playables", new[] { "d_PlayableDirector Icon" } },
            { "Assets/Create/Playables/Playable Asset C# Script", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Playables/Playable Behaviour C# Script", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Rendering/URP Renderer Feature", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Scripting", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Scripting/Assembly Definition", new[] { "d_AssemblyDefinitionAsset Icon" } },
            { "Assets/Create/Scripting/Assembly Definition Reference", new[] { "d_AssemblyDefinitionReferenceAsset Icon" } },
            { "Assets/Create/Scripting/Empty C# Script", new[] { "dll Script Icon" } },
            { "Assets/Create/Scripting/MonoBehaviour Script", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Scripting/Playables", new[] { "d_PlayableDirector Icon" } },
            { "Assets/Create/Scripting/Playables/Playable Asset Script", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Scripting/Playables/Playable Behaviour Script", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Scripting/ScriptableObject Script", new[] { "d_ScriptableObject Icon" } },
            { "Assets/Create/Scripting/URP Post-process Volume Scripts", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Scripting/URP Renderer Feature Script", new[] { "d_cs Script Icon" } },
            // End: Legacy Unity menu paths

            // Shader.
            { "Assets/Create/Shader/Image Effect Shader", new[] { "d_Shader Icon" } },
            { "Assets/Create/Shader/Standard Surface Shader", new[] { "d_Shader Icon" } },
            { "Assets/Create/Shader/Unlit Shader", new[] { "d_Shader Icon" } },
            // Legacy Unity menu paths
            { "Assets/Create/Shader Variant Collection", new[] { "d_ShaderVariantCollection Icon" } },
            { "Assets/Create/Shader", new[] { "d_Shader Icon" } },
            { "Assets/Create/Shader/Compute Shader", new[] { "d_ComputeShader Icon" } },
            { "Assets/Create/Shader/Custom Render Texture", new[] { "d_Shader Icon" } },
            { "Assets/Create/Shader/Empty HLSL", new[] { "d_ShaderInclude Icon" } },
            { "Assets/Create/Shader/Graphics State Collection", new[] { "d_ShaderVariantCollection Icon" } },
            { "Assets/Create/Shader/Ray Tracing Shader", new[] { "d_RayTracingShader Icon" } },
            { "Assets/Create/Shader/SRP Blit Shader", new[] { "d_Shader Icon" } },
            { "Assets/Create/Shader/Shader Variant Collection", new[] { "d_ShaderVariantCollection Icon" } },
            { "Assets/Create/Shader/URP Unlit Shader", new[] { "d_Shader Icon" } },
            { "Assets/Create/Shader/Unified Ray Tracing Shader", new[] { "d_RayTracingShader Icon" } },
            // End: Legacy Unity menu paths

            // Shader Graph.
            { "Assets/Create/Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/Blank Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/BuiltIn", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/BuiltIn/Canvas Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/BuiltIn/Lit Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/BuiltIn/Unlit Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/Custom Render Texture", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/From Template...", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/Sub Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_subgraph_icon_gray_dark.png", "d_SubgraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_subgraph_icon.png" } },
            { "Assets/Create/Shader Graph/URP", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/URP/Canvas Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/URP/Decal Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/URP/Fullscreen Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/URP/Lit Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/URP/Six Way Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/URP/Sprite Custom Lit Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/URP/Sprite Lit Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/URP/Sprite Unlit Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/URP/Terrain Lit Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/URP/UI Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },
            { "Assets/Create/Shader Graph/URP/Unlit Shader Graph", new[] { "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon_gray_dark.png", "d_GraphAsset", "Packages/com.unity.shadergraph/Editor/Resources/Icons/sg_graph_icon.png" } },

            // Spine.
            { "Assets/Create/Spine", new[] { "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/icon-spine.png" } },
            { "Assets/Create/Spine/Animation Reference Asset", new[] { "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/icon-animation.png" } },
            { "Assets/Create/Spine/EventData Reference Asset", new[] { "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/icon-event.png" } },
            { "Assets/Create/Spine/SkeletonData Asset", new[] { "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/SkeletonDataAsset Icon.png" } },
            { "Assets/Create/Spine/SkeletonData Modifiers/Blend Mode Materials", new[] { "d_Material Icon" } },
            { "Assets/Create/Spine/Spine Atlas Asset", new[] { "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/AtlasAsset Icon.png" } },
            { "Assets/Create/Spine/Spine SpriteAtlas Asset", new[] { "Packages/com.esotericsoftware.spine.spine-unity/Editor/spine-unity/Editor/GUI/AtlasAsset Icon.png" } },

            // Terrain.
            // Legacy Unity menu paths
            { "Assets/Create/Brush", new[] { "d_TerrainInspector.TerrainToolSplat" } },
            { "Assets/Create/Terrain Layer", new[] { "d_Terrain Icon" } },
            { "Assets/Create/Terrain", new[] { "d_Terrain Icon" } },
            { "Assets/Create/Terrain/Brush", new[] { "d_TerrainInspector.TerrainToolSplat" } },
            { "Assets/Create/Terrain/Generate Terrain Layer", new[] { "d_AISparkle Icon" } },
            { "Assets/Create/Terrain/Terrain Layer", new[] { "d_Terrain Icon" } },
            // End: Legacy Unity menu paths

            // Testing.
            { "Assets/Create/Testing", new[] { "TestStopwatch" } },
            { "Assets/Create/Testing/C# Test Script", new[] { "d_cs Script Icon" } },
            { "Assets/Create/Testing/Tests Assembly Folder", new[] { "d_AssemblyDefinitionAsset Icon" } },

            // Text Core.
            { "Assets/Create/Text Core", new[] { "d_FontAsset Icon", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/Text Core/Color Gradient", new[] { "d_ColorPicker.CycleColor" } },
            { "Assets/Create/Text Core/Font Asset", new[] { "d_FontAsset Icon", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/Text Core/Font Asset Variant", new[] { "d_FontAsset Icon", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/Text Core/Font Asset/Bitmap", new[] { "d_FontAsset Icon", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/Text Core/Font Asset/Color", new[] { "d_FontAsset Icon", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/Text Core/Font Asset/SDF", new[] { "d_FontAsset Icon", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/Text Core/Sprite Asset", new[] { "d_SpriteAtlas Icon" } },
            { "Assets/Create/Text Core/Text StyleSheet", new[] { "d_StyleSheet Icon" } },

            // TextMeshPro.
            // Legacy Unity menu paths
            { "Assets/Create/TextMeshPro/Font Asset Variant", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/Text", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/Text/Font Asset", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/Text/Font Asset Variant", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/Text/Sprite Asset", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Sprite Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Sprite Asset Icon.psd" } },
            { "Assets/Create/Text/Text StyleSheet", new[] { "d_StyleSheet Icon" } },
            { "Assets/Create/Text/Color Gradient", new[] { "d_ColorPicker.CycleColor" } },
            { "Assets/Create/TextMeshPro", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/TextMeshPro/Color Gradient", new[] { "d_ColorPicker.CycleColor" } },
            { "Assets/Create/TextMeshPro/Font Asset", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/TextMeshPro/Font Asset/Bitmap", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/TextMeshPro/Font Asset/Color", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/TextMeshPro/Font Asset/Font Asset Variant", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/TextMeshPro/Font Asset/SDF", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Font Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/TextMeshPro/Sprite Asset", new[] { "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Sprite Asset Icon.psd", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Sprite Asset Icon.psd" } },
            { "Assets/Create/TextMeshPro/Style Sheet", new[] { "d_StyleSheet Icon" } },
            // End: Legacy Unity menu paths

            // Timeline.
            // Legacy Unity menu paths
            { "Assets/Create/Signal", new[] { "d_SignalAsset Icon" } },
            { "Assets/Create/Timeline", new[] { "d_TimelineAsset Icon" } },
            { "Assets/Create/Timeline/Signal", new[] { "d_SignalAsset Icon" } },
            { "Assets/Create/Timeline/Timeline", new[] { "d_TimelineAsset Icon" } },
            // End: Legacy Unity menu paths

            // UI Toolkit.
            { "Assets/Create/UI Toolkit/Panel Text Settings", new[] { "d_PanelTextSettings Icon", "d_Settings Icon" } },
            // Legacy Unity menu paths
            { "Assets/Create/UI Toolkit/Style Sheet", new[] { "d_StyleSheet Icon" } },
            { "Assets/Create/UI Toolkit/UI Document", new[] { "d_VisualTreeAsset Icon" } },
            { "Assets/Create/UI Toolkit/Text Settings", new[] { "d_PanelTextSettings Icon", "d_Settings Icon" } },
            { "Assets/Create/UI Toolkit", new[] { "d_Canvas Icon" } },
            { "Assets/Create/UI Toolkit/Default Runtime Theme File", new[] { "d_ThemeStyleSheet Icon" } },
            { "Assets/Create/UI Toolkit/Editor Window", new[] { "d_UnityEditor.InspectorWindow" } },
            { "Assets/Create/UI Toolkit/Filter Function Definition", new[] { "d_Filter Icon" } },
            { "Assets/Create/UI Toolkit/Panel Settings Asset", new[] { "d_PanelSettings Icon" } },
            { "Assets/Create/UI Toolkit/Style Sheet (USS)", new[] { "d_StyleSheet Icon" } },
            { "Assets/Create/UI Toolkit/TSS Theme File", new[] { "d_ThemeStyleSheet Icon" } },
            { "Assets/Create/UI Toolkit/Text", new[] { "d_FontAsset Icon", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/UI Toolkit/Text/Color Gradient", new[] { "d_ColorPicker.CycleColor" } },
            { "Assets/Create/UI Toolkit/Text/Font Asset", new[] { "d_FontAsset Icon", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/UI Toolkit/Text/Font Asset/Bitmap", new[] { "d_FontAsset Icon", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/UI Toolkit/Text/Font Asset/Color", new[] { "d_FontAsset Icon", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/UI Toolkit/Text/Font Asset/SDF", new[] { "d_FontAsset Icon", "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Font Asset Icon.psd" } },
            { "Assets/Create/UI Toolkit/Text/Panel Text Settings", new[] { "d_PanelTextSettings Icon", "d_Settings Icon" } },
            { "Assets/Create/UI Toolkit/Text/Sprite Asset", new[] { "d_SpriteAtlas Icon" } },
            { "Assets/Create/UI Toolkit/Text/Text StyleSheet", new[] { "d_StyleSheet Icon" } },
            { "Assets/Create/UI Toolkit/UI Animation Clip", new[] { "d_UIAnimationClip Icon", "d_AnimationClip Icon" } },
            { "Assets/Create/UI Toolkit/UI Document (UXML)", new[] { "d_VisualTreeAsset Icon" } },
            // End: Legacy Unity menu paths

            // Visual Scripting.
            { "Assets/Create/Visual Scripting", new[] { "Packages/com.unity.visualscripting/Editor/VisualScripting.Core/IconMap/FlowGraph@32x.png" } },
            { "Assets/Create/Visual Scripting/Script Graph", new[] { "Packages/com.unity.visualscripting/Editor/VisualScripting.Core/IconMap/FlowGraph@32x.png" } },
            { "Assets/Create/Visual Scripting/State Graph", new[] { "Packages/com.unity.visualscripting/Editor/VisualScripting.Core/IconMap/State@32x.png" } },

            // Wwise.
            { "Assets/Wwise", new[] { "wwise-logo.png" } },
        };
    }
}
