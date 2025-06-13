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
        [System.Serializable]
        class Node
        {
            [SerializeField]
            public NodeType type;

            public NodeAsset asset;

            public GameObject nodeObject;

            public int deep;

            public Node[] connections = new Node[4]; // North, east, south, west


            public Node(NodeType type, NodeAsset asset)
            {
                this.type = type;
                this.asset = asset;
                for (int i = 0; i < connections.Length; i++)
                    connections[i] = null;
            }

            public override string ToString()
            {
                return $"[Node - Type:{type}, Asset:{asset.name}]";
            }
        }

        int level = 0;

        int minNodes = 18, maxNodes = 21;

        [SerializeField]
        List<Node> nodes;


        void Awake()
        {
            

        }

        // Start is called before the first frame update
        void Start()
        {
            // Choose how many nodes and which ones will be used
            ChooseNodes();

            // Connections
            ConnectNodes();

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

        void ConnectNodes()
        {
            List<Node> remainingList = new List<Node>();
            foreach (var node in nodes)
                remainingList.Add(node);
            int deep = 0;
            int connectionIndex = -1;
            int pendingConnections = 0;
            ConnectNode(null, connectionIndex, remainingList, deep, 0);
            
        }

        void ConnectNode(Node fromNode, int fromNodeConnIndex, List<Node> remainingNodes, int deep, int pendingConnections)
        {
            if (fromNode == null)
            {
                // First node
                Node node = remainingNodes.Find(n => n.type == NodeType.Enter);
                remainingNodes.Remove(node);
                node.deep = deep;
                int connCount = Random.Range(node.asset.MinConnections, node.asset.MaxConnections);
                connCount = 1;
                List<int> indices = GetFreeConnectionIndices(node);
                var index = Random.Range(0, indices.Count);
                deep++;
                pendingConnections = 0;
                ConnectNode(node, index, remainingNodes, deep, pendingConnections);

            }
            else
            {
                Debug.Log($"From node {fromNode}, fromConnIndex:{fromNodeConnIndex}, remainingCount:{remainingNodes.Count}, deep:{deep}");
                // Choose another node to connect
                Node node = null;
                if (deep < 2)
                {
                    // Router preferred
                    node = remainingNodes.Find(n => n.type == NodeType.Router);

                }
                if (node == null)
                {
                    List<Node> tmp = new List<Node>();
                    if (pendingConnections == 0)
                        tmp = remainingNodes.FindAll(n => n.asset.MaxConnections > 1);
                    else
                        tmp = remainingNodes.FindAll(n => true);

                    node = tmp[Random.Range(0, tmp.Count)];
                }

                remainingNodes.Remove(node);
                node.deep = deep;

                // Update from node connection
                fromNode.connections[fromNodeConnIndex] = node;
                // Update new node connection
                int connIndex = (fromNodeConnIndex + 2) % 4;
                node.connections[connIndex] = fromNode;

                // Create new connections
                

            }

        }

        List<int> GetFreeConnectionIndices(Node node)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < node.connections.Length; i++)
            {
                //Debug.Log($"AAAAAAAAA nodeConnection:{node.connections[i]} is null {node.connections[i]==null}");
                if (node.connections[i] == null)
                    indices.Add(i);
            }
            return indices;
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