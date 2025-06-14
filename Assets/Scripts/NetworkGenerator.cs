using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using BNF.Scriptables;
using Unity.Android.Types;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

namespace BNF
{
    public enum NodeType { None, Enter, Router, AntiSpyware, Firewall, SystemAdmin, DataCenter, Research }

    public class NetworkGenerator : MonoBehaviour
    {
        [SerializeField] int minNodes = 20;
        [SerializeField] int maxNodes = 40;
        [SerializeField, Range(0f, 1f)] float loopProbability = 0.2f;

        private List<Node> nodes = new();
        private Dictionary<Vector2Int, int> grid = new();

        [System.Serializable]
        class Node
        {
            [SerializeField]
            public NodeType type;
            

            public NodeAsset asset;
            public GameObject nodeObject;
            public int deep;
            public int[] connections = new int[4]; // N, E, S, W

            public Node()
            {
                this.type = NodeType.None;
                asset = null;
                for (int i = 0; i < connections.Length; i++)
                    connections[i] = -1;
            }

            

            public override string ToString()
            {
                return $"[Node - Type:{type}, Asset:{(asset != null ? asset.name : "null")}]";
            }
        }

        private readonly Vector2Int[] directions = {
            new Vector2Int(0, 1),   // N
            new Vector2Int(1, 0),   // E
            new Vector2Int(0, -1),  // S
            new Vector2Int(-1, 0)   // W
        };

        List<NodeAsset> nodeAssetList;

        void Awake()
        {
            nodeAssetList = Resources.LoadAll<NodeAsset>(NodeAsset.ResourceFolder).ToList();
        }

        void Start()
        {
            GenerateNetwork();
            ChooseNodeTypes();
            CreateNodeObjects();
            
            DebugNodes();
        }

        void DebugNodes()
        {
            Debug.Log("**********************************************************************************************************************");
            foreach (Node node in nodes)
            {
                Debug.Log($"Node:{nodes.IndexOf(node)}, Conns:{node.connections[0]} {node.connections[1]} {node.connections[2]} {node.connections[3]}, Deep:{node.deep}");
            }
            Debug.Log("**********************************************************************************************************************");
        }

        void GenerateNetwork()
        {
            nodes.Clear();
            grid.Clear();

            Queue<Vector2Int> frontier = new();
            CreateNode(Vector2Int.zero);
            frontier.Enqueue(Vector2Int.zero);

            while (nodes.Count < maxNodes && frontier.Count > 0)
            {
                Vector2Int currentPos = frontier.Dequeue();
                int currentIndex = grid[currentPos];
                Node currentNode = nodes[currentIndex];

                List<int> dirs = new List<int> { 0, 1, 2, 3 };
                Shuffle(dirs);

                int maxConnectionsForThisNode = Random.Range(1, 5); // tra 1 e 4
                int connectionsCreated = 0;

                foreach (int dir in dirs)
                {
                    if (nodes.Count >= maxNodes || connectionsCreated >= maxConnectionsForThisNode)
                        break;
                    {
                        if (nodes.Count >= maxNodes) break;

                        Vector2Int nextPos = currentPos + directions[dir];

                        if (grid.ContainsKey(nextPos))
                        {
                            int existingIndex = grid[nextPos];
                            Node existingNode = nodes[existingIndex];

                            if (Random.value < loopProbability &&
                                currentNode.connections[dir] == -1 &&
                                existingNode.connections[Opposite(dir)] == -1)
                            {
                                currentNode.connections[dir] = existingIndex;
                                existingNode.connections[Opposite(dir)] = currentIndex;
                            }

                            continue;
                        }

                        int newIndex = CreateNode(nextPos);
                        currentNode.connections[dir] = newIndex;
                        nodes[newIndex].connections[Opposite(dir)] = currentIndex;
                        frontier.Enqueue(nextPos);
                        connectionsCreated++;
                    }
                }
            }

            if (nodes.Count < minNodes)
            {
                Debug.LogWarning($"Solo {nodes.Count} nodi generati, ma il minimo richiesto è {minNodes}");
            }

            Debug.Log($"Generati {nodes.Count} nodi.");
        }

        int CreateNode(Vector2Int pos)
        {
            Node newNode = new Node();
            nodes.Add(newNode);
            grid[pos] = nodes.Count - 1;
            return nodes.Count - 1;
        }

        int Opposite(int dir)
        {
            return (dir + 2) % 4;
        }

        void Shuffle(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int j = Random.Range(i, list.Count);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        void ChooseNodeTypes()
        {
            
            for (int i=0; i<nodes.Count; i++)
            {
                var node = nodes[i];
                if (i == 0)
                {
                    node.type = NodeType.Enter;
                    node.asset = NodeTypeToNodeAsset(NodeType.Enter);
                }
                else
                {
                    node.type = NodeType.Router;
                    node.asset = NodeTypeToNodeAsset(NodeType.Router);            
                }
            }
        }

        void CreateNodeObjects()
        {
            HashSet<Node> visited = new HashSet<Node>();
            Node node = nodes.Find(n => n.type == NodeType.Enter);
            CreateNodeObject(node, null, visited);
        }

       
        void CreateNodeObject(Node node, Node fromNode, HashSet<Node> visited)
        {
            if (visited.Contains(node)) return;
            visited.Add(node);
            var obj = Instantiate(node.asset.NodeObject);
            node.nodeObject = obj;
            
            Vector3 position = Vector3.zero;
            if(fromNode != null)
            {
                int nodeId = nodes.IndexOf(fromNode);
                var fromConnectionId = node.connections.ToList().FindIndex(c => c == nodeId);
                
                position = fromNode.nodeObject.transform.position + GetOffsetByConnectionId(fromConnectionId);

            }

            obj.transform.position = position;
            obj.transform.rotation = Quaternion.identity;

            for (int i = 0; i < node.connections.Length; i++)
            {
                if (node.connections[i] < 0) continue;

                // Create next node object
                Node nextNode = nodes[node.connections[i]];
                
                CreateNodeObject(nextNode, node, visited);

            }
        }

        Vector3 GetOffsetByConnectionId(int connectionId)
        {
            float distance = 4;
            switch (connectionId)
            {
                case 0:
                    return Vector3.forward * distance;
                case 1:
                    return Vector3.right * distance;
                case 2:
                    return Vector3.back * distance;
                case 3:
                    return Vector3.left * distance;
                default:
                    return Vector3.zero;
            }
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
        
        NodeAsset NodeTypeToNodeAsset(NodeType type)
        {
            switch (type)
            {
                case NodeType.Enter:
                    return nodeAssetList.Find(n => n.GetType() == typeof(EnterNodeAsset));
                case NodeType.AntiSpyware:
                    return nodeAssetList.Find(n => n.GetType() == typeof(AntiSpywareNodeAsset));
                case NodeType.DataCenter:
                    return nodeAssetList.Find(n => n.GetType() == typeof(DataCenterNodeAsset));
                case NodeType.Firewall:
                    return nodeAssetList.Find(n => n.GetType() == typeof(FirewallNodeAsset));
                case NodeType.Research:
                    return nodeAssetList.Find(n => n.GetType() == typeof(ResearchNodeAsset));
                case NodeType.Router:
                    return nodeAssetList.Find(n => n.GetType() == typeof(RouterNodeAsset));
                case NodeType.SystemAdmin:
                    return nodeAssetList.Find(n => n.GetType() == typeof(SystemAdminNodeAsset));
                default:
                    return null;
                        
            }
            
        }
    }
}