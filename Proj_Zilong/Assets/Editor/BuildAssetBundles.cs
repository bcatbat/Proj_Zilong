using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
public class BuildAssetBundles {

    [MenuItem("Custom Editor/Build AssetBundles")]
    static void BuildAssetBundlesAll()
    {
        string abDirectory = Application.dataPath + "/StreamingAssets";
        if (!Directory.Exists(abDirectory))
        {
            Directory.CreateDirectory(abDirectory);
        }
        BuildPipeline.BuildAssetBundles(abDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
#endif
