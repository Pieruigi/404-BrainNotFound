using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using BNF.Scriptables;
using Unity.VisualScripting;
using UnityEngine;

namespace BNF
{
    public enum NodeType { Enter, Router, AntiSpyware, Firewall, SystemAdmin, DataCenter, Research }

    public class NetBuilder : MonoBehaviour
    {
        class Node
        {
            public NodeType type;

            public NodeAsset asset;

            public GameObject nodeObject;

            public bool northConnection = false; 
            public bool eastConnection = false; 
            public bool southConnection = false; 
            public bool westConnection = false; 


            public Node(NodeType type, NodeAsset asset)
            {
                this.type = type;
                this.asset = asset;
            }

            public override string ToString()
            {
                return $"[Node - Type:{type}, Asset:{asset.name}]";
            }
        }

        int level = 0;

        int minNodes = 18, maxNodes = 21;

        List<Node> nodes;


        void Awake()
        {
            

        }

        // Start is called before the first frame update
        void Start()
        {
            // Choose how many nodes and which ones will be used
            ChooseNodes();

            // Create node objects
            CreateNodeObjects();

            // Arrange nodes 
            ArrangeNodes();


        }

        // Update is called once per frame
        void Update()
        {

        }

        async void __Test()
        {
            // NodeManager.Instance.CreateNode(nodeAssets[0], level);
            // await Task.Delay(3000);
            // NodeManager.Instance.StartNodeAll();
        }

        void ChooseNodes()
        {
            List<NodeAsset> nodeAssets = Resources.LoadAll<NodeAsset>(NodeAsset.ResourceFolder).ToList();

            // How many node?
            int nodeCount = Random.Range(minNodes, maxNodes + 1);

            nodes = new List<Node>();

            // Choose the special nodes you want to add to this run
            foreach (var na in nodeAssets)
            {
                if (na.MinLevel > level)
                    continue;
                if (na.GetType() == typeof(RouterNodeAsset) || na.GetType() == typeof(EnterNodeAsset)) // We add routers at the end to fill the remaining slots
                    continue;
                
                int count = Random.Range(na.MinNumber, na.MaxNumber + 1);
                for (int i = 0; i < count; i++)
                    nodes.Add(new Node(NodeAssetToNodeType(na), na));
            }

            // Fill with router
            int left = nodeCount - nodes.Count;
            NodeAsset ra = nodeAssets.Find(n => n.GetType() == typeof(RouterNodeAsset));
            for (int i = 0; i < left; i++)
                nodes.Add(new Node(NodeType.Router, ra));

            // Add the enter node
            nodes.Add(new Node(NodeType.Enter, nodeAssets.Find(n => n.GetType() == typeof(EnterNodeAsset))));

            DebugNodes();

        }

        void CreateNodeObjects()
        {
            foreach (var node in nodes)
                node.nodeObject = NodeManager.Instance.CreateNode(node.asset, level);
        }

        void ArrangeNodes()
        {
            // Start with the enter node
            var node = nodes.Find(n => n.type == NodeType.Enter);
            // Move the enter node at zero
            node.nodeObject.transform.position = Vector3.zero;
            node.nodeObject.transform.rotation = Quaternion.identity;
        }

        NodeType NodeAssetToNodeType(NodeAsset asset)
        {
            if (asset.GetType() == typeof(FirewallNodeAsset))
                return NodeType.Firewall;
            if (asset.GetType() == typeof(ResearchNodeAsset))
                return NodeType.Research;
            if (asset.GetType() == typeof(DataCenterNodeAsset))
                return NodeType.DataCenter;
            if (asset.GetType() == typeof(AntiSpywareNodeAsset))
                return NodeType.AntiSpyware;
            if (asset.GetType() == typeof(SystemAdminNodeAsset))
                return NodeType.SystemAdmin;
            if (asset.GetType() == typeof(EnterNodeAsset))
                return NodeType.Enter;

            return NodeType.Router;

        }

        

        void DebugNodes()
        {
            Debug.Log($"[Nodes - Count:{nodes.Count}]");
            foreach (var node in nodes)
                Debug.Log(node);
        }

    }
}