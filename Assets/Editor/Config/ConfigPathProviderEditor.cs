using Assets.CubeRain.CodeBase.Infrastructure.Configs.Interface;
using Assets.CubeRain.CodeBase.Infrastructure.ResourcesLoading;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConfigPathProvider))]
public class ConfigPathProviderEditor : Editor
{
    private const string ButtonText = "Scan for config paths";
    private const string AssetTypeFilter = "t: ScriptableObject";
    private const string ResourcesDirectoryPath = "Assets/Resources";
    private const string AssetFileExtension = ".asset";

    private ConfigPathProvider _target;

    private List<string> _discoveredPaths = new();
    private List<string> _discoveredTypeNames = new();

    private void OnEnable()
    {
        _target = target as ConfigPathProvider;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawScanButton();
    }

    private void DrawScanButton()
    {
        if (GUILayout.Button(ButtonText))
        {
            HandleScanButton();
        }
    }

    private void HandleScanButton()
    {
        _discoveredPaths.Clear();
        _discoveredTypeNames.Clear();

        if (ScanForConfigAssets(CollectAssetPaths()))
        {
            _target.SetPaths(_discoveredPaths);
            _target.SetTypeNames(_discoveredTypeNames);

            EditorUtility.SetDirty(_target);
            AssetDatabase.SaveAssets();
        }
    }

    private List<string> CollectAssetPaths()
    {
        List<string> paths = new();

        string[] guids = AssetDatabase.FindAssets(AssetTypeFilter, new[] { ResourcesDirectoryPath });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            paths.Add(path);
        }

        return paths;
    }

    private bool ScanForConfigAssets(List<string> paths)
    {
        foreach (string path in paths)
        {
            ScriptableObject asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            string assetConstantPart = ResourcesDirectoryPath + '/';

            if (asset != null && asset is IConfig)
            {
                string relativePath = path.Replace(assetConstantPart, "").Replace(AssetFileExtension, "");

                _discoveredPaths.Add(relativePath);
                _discoveredTypeNames.Add(asset.GetType().FullName);
            }
        }

        return _discoveredPaths.Count > 0 && _discoveredTypeNames.Count > 0;
    }
}
