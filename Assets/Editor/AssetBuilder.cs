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

        #region subroutines
        [MenuItem("Assets/Create/404BNF/Subroutines/AntiSpywareSlowDownSubroutine")]
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
        #endregion

        #region nodes
        [MenuItem("Assets/Create/404BNF/Nodes/AntiSpywareNode")]
        public static void CreateSpywareNode()
        {
            AntiSpywareNodeAsset asset = ScriptableObject.CreateInstance<AntiSpywareNodeAsset>();

            string name = "AntiSpywareNode.asset";

            string folder = System.IO.Path.Combine(ResourceFolder, NodeAsset.ResourceFolder);

            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Create/404BNF/Nodes/RouterNode")]
        public static void CreateRouterNode()
        {
            RouterNodeAsset asset = ScriptableObject.CreateInstance<RouterNodeAsset>();

            string name = "RouterNode.asset";

            string folder = System.IO.Path.Combine(ResourceFolder, NodeAsset.ResourceFolder);

            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Create/404BNF/Nodes/FirewallNode")]
        public static void CreateFirewallNode()
        {
            FirewallNodeAsset asset = ScriptableObject.CreateInstance<FirewallNodeAsset>();

            string name = "Firewall.asset";

            string folder = System.IO.Path.Combine(ResourceFolder, NodeAsset.ResourceFolder);

            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Create/404BNF/Nodes/DataCenterNode")]
        public static void CreateDataCenterNode()
        {
            DataCenterNodeAsset asset = ScriptableObject.CreateInstance<DataCenterNodeAsset>();

            string name = "DataCenterNode.asset";

            string folder = System.IO.Path.Combine(ResourceFolder, NodeAsset.ResourceFolder);

            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Create/404BNF/Nodes/ResearchNode")]
        public static void CreateResearchNode()
        {
            ResearchNodeAsset asset = ScriptableObject.CreateInstance<ResearchNodeAsset>();

            string name = "ResearchNode.asset";

            string folder = System.IO.Path.Combine(ResourceFolder, NodeAsset.ResourceFolder);

            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Create/404BNF/Nodes/SystemAdminNode")]
        public static void CreateSystemAdminNode()
        {
            SystemAdminNodeAsset asset = ScriptableObject.CreateInstance<SystemAdminNodeAsset>();

            string name = "SystemAdminNode.asset";

            string folder = System.IO.Path.Combine(ResourceFolder, NodeAsset.ResourceFolder);

            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(asset, System.IO.Path.Combine(folder, name));

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
        
       
    }
#endregion


 
    }