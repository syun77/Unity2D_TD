#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class MyAssetPostprocessor : AssetPostprocessor
{
    /// <summary>
    /// すべてのアセットのインポートが終了した際に呼び出されます
    /// </summary>
    /// <param name="importedAssets">インポートされたアセットのパス</param>
    /// <param name="deletedAssets">削除されたアセットのパス</param>
    /// <param name="movedAssets">移動したアセットの移動後のパス</param>
    /// <param name="movedFromPath">移動したアセットの移動前のパス</param>
    private static void OnPostprocessAllAssets(
        string[] importedAssets, 
        string[] deletedAssets, 
        string[] movedAssets, 
        string[] movedFromPath)
    {
        foreach (var importedAsset in importedAssets)
        {
            if(IsTmxFile(importedAsset))
            {
                // TMXファイルなので拡張子を*.xmlにしてコピー
                var newAsset = importedAsset.Replace(".tmx", ".xml");
                // 古いXMLは削除
                AssetDatabase.DeleteAsset(newAsset);
                if(AssetDatabase.CopyAsset(importedAsset, newAsset))
                {
                    // コピー実行
                    Debug.Log ("Copy: " + importedAsset + " -> " + newAsset);
                }
            }
        }
    }

    /// TMXファイルかどうか調べる
    static bool IsTmxFile(string str)
    {
        return str.IndexOf(".tmx") > 0;
    }
 
}

#endif
