using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class QuickDataManager : MonoBehaviour
{
    [MenuItem("Quick Data/Create Data")]
    public static void CreateTextSettings()
    {
        const string name = "Data";
        var path = GenerateStandardPath(Selection.activeObject, name);
        var asset = ScriptableObject.CreateInstance<QuickDataScriptableObject>();
        ProjectWindowUtil.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
    }

    private static string GenerateStandardPath(Object atAsset, string name)
    {
        var dir = AssetDatabase.GetAssetPath(atAsset);

        if (dir.Length == 0)
            // Selection must not be an asset.
            dir = "QuickData";
        else if (!Directory.Exists(dir))
            // Selection must be a file asset.
            dir = Path.GetDirectoryName(dir);

        return AssetDatabase.GenerateUniqueAssetPath(dir + "/" + name + ".asset");
    }
}