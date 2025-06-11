using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BNF.Scriptables;

namespace BNF.Editor
{
    public class AssetBuilder : MonoBehaviour
    {
        public const string ResourceFolder = "Assets/Resources";

        [MenuItem("Assets/Create/Subroutines/AntiSpywareSlowDownSubroutine")]
        public static void CreateAntiSpywareSlowDownSubroutine()
        {
            AntiSpywareSlowDownSubroutineAsset asset = ScriptableObject.CreateInstance<AntiSpywareSlowDownSubroutineAsset>();

            string name = "AntiSpywareSlowDownSubroutine.asset";

            string folder = System.IO.Path.Combine(ResourceFolder, SubroutineAsset.ResourceFolder);

            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}