// using System.Collections.Generic;
// using System.Collections.Specialized;
// using System.Linq;
// using BNF.Scriptables;
// using Unity.VisualScripting;
// using Unity.VisualScripting.Antlr3.Runtime;
// using UnityEngine;

// namespace BNF
// {
//     public enum NodeType { None, Enter, Router, AntiSpyware, Firewall, SystemAdmin, DataCenter, Research }

//     public class NetBuilder : MonoBehaviour
//     {
//         [System.Serializable]
//         class Node
//         {
//             [SerializeField]
//             public NodeType type;

//             public NodeAsset asset;

//             public GameObject nodeObject;

//             public int deep;

//             public int[] connections = new int[4]; // North, east, south, west

//             public Node(NodeType type)
//             {
//                 this.type = type;
//                 asset = null;
//                 for (int i = 0; i < connections.Length; i++)
//                     connections[i] = -1;
//             }

//             // public Node(NodeType type, NodeAsset asset)
//             // {
//             //     this.type = type;
//             //     this.asset = asset;
//             //     for (int i = 0; i < connections.Length; i++)
//             //         connections[i] = -1;
//             // }

//             public override string ToString()
//             {
//                 return $"[Node - Type:{type}, Asset:{asset.name}]";
//             }
//         }

//         // [System.Serializable]
//         // class Connection
//         // {
            
//         // }

//         int level = 0;

//         int minNodes = 8, maxNodes = 11;

//         [SerializeField]
//         List<Node> nodes;


        
//         void Awake()
//         {
          
//         }

//         // Start is called before the first frame update
//         void Start()
//         {
//             // Choose how many nodes and which ones will be used
//             //ChooseNodes();

//  Debug.Log("*************************************************************************************************************");

//             // Connections
//             CreateNet();
           
//             foreach (Node n in nodes)
//             {
//                 Debug.Log($"Node {nodes.IndexOf(n)}, N:{n.connections[0]}, E:{n.connections[1]}, S:{n.connections[2]}, W:{n.connections[3]}");
//             }
// Debug.Log("*************************************************************************************************************");
//             // Create node objects
//             // CreateNodeObjects();

            

//             // // Arrange nodes 
//             // ArrangeNodes();


//         }

//         // Update is called once per frame
//         void Update()
//         {

//         }

//         async void __Test()
//         {
//             // NodeManager.Instance.CreateNode(nodeAssets[0], level);
//             // await Task.Delay(3000);
//             // NodeManager.Instance.StartNodeAll();
//         }

//         // void ChooseNodes()
//         // {
//         //     List<NodeAsset> nodeAssets = Resources.LoadAll<NodeAsset>(NodeAsset.ResourceFolder).ToList();

//         //     // How many node?
//         //     int nodeCount = Random.Range(minNodes, maxNodes + 1);

//         //     nodes = new List<Node>();

//         //     // Choose the special nodes you want to add to this run
//         //     foreach (var na in nodeAssets)
//         //     {
//         //         if (na.MinLevel > level)
//         //             continue;
//         //         if (na.GetType() == typeof(RouterNodeAsset) || na.GetType() == typeof(EnterNodeAsset)) // We add routers at the end to fill the remaining slots
//         //             continue;
                
//         //         int count = Random.Range(na.MinNumber, na.MaxNumber + 1);
//         //         for (int i = 0; i < count; i++)
//         //             nodes.Add(new Node(NodeAssetToNodeType(na), na));
//         //     }

//         //     // Fill with router
//         //     int left = nodeCount - nodes.Count;
//         //     NodeAsset ra = nodeAssets.Find(n => n.GetType() == typeof(RouterNodeAsset));
//         //     for (int i = 0; i < left; i++)
//         //         nodes.Add(new Node(NodeType.Router, ra));

//         //     // Add the enter node
//         //     nodes.Add(new Node(NodeType.Enter, nodeAssets.Find(n => n.GetType() == typeof(EnterNodeAsset))));

//         //     DebugNodes();

//         // }

//         void CreateNet()
//         {
//             int count = Random.Range(minNodes, maxNodes + 1);
//             for (int i = 0; i < count; i++)
//             {
//                 nodes.Add(new Node(NodeType.None));
//             }

//             List<Node> remainingList = nodes.FindAll(n => true);
//             // foreach (var node in nodes)
//             //     remainingList.Add(node);

//             //ConnectNode(null, -1, remainingList, deep:0, pendingConnections:0);
//             //Node n = remainingList.Find(n => n.type == NodeType.Enter);
//             Node n = remainingList[0];
//             remainingList.Remove(n);
//             n.deep = 0;
//             ConnectNode(n, remainingList, 1, 0);
            
//         }

//         void ConnectNode(Node node, List<Node> remainingNodes, int deep, int pendingConnections)
//         {   

//             // if (fromNode.connections.ToList().Where(c => c < 0).Count() == 0)
//             //     return;

//             if (remainingNodes.Count == 0)
//                 return;

//             // How many free connections do we have to use?
//             List<int> indices = new List<int>();
//             for (int i = 0; i < node.connections.Length; i++)
//             {
//                 if (node.connections[i] < 0) 
//                     indices.Add(i);
//             }


//             int max = Mathf.Min(indices.Count, remainingNodes.Count - pendingConnections);
            
//             if (max == 0) return;
            
//             int min = 0;
//             if (pendingConnections == 0 && remainingNodes.Count > 0)
//                 min = 1;
            
//             int count = Random.Range(min, max+1);
            
            
//             Debug.Log($"Count:{count}");
//             Debug.Log($"Pending:{pendingConnections}");
//             for (int i = 0; i < count; i++)
//             {
//                 // Get a new node
//                 var toNode = remainingNodes[Random.Range(0, remainingNodes.Count)];
//                 remainingNodes.Remove(toNode);
//                 toNode.deep = deep;

//                 int connIndex = indices[Random.Range(0, indices.Count)];
//                 indices.Remove(connIndex);
//                 node.connections[connIndex] = nodes.IndexOf(toNode);
//                 toNode.connections[(connIndex + 2) % 4] = nodes.IndexOf(node);

//                 Debug.Log($"N:{nodes.IndexOf(node)},C:{connIndex} <-> N:{nodes.IndexOf(toNode)},C:{(connIndex + 2) % 4}");

//                 ConnectNode(toNode, remainingNodes, deep + 1, pendingConnections + count - 1 - i);
//             }


            

            
            

//             // Move ahead with the new node            
            
//         }

//         // void _ConnectNode(Node fromNode, int fromNodeConnIndex, List<Node> remainingNodes, int deep, int pendingConnections)
//         // {
//         //     Debug.Log("-----------------------------------------------------------------------------------------------");
//         //     //Debug.Log($"Creating Node, fromNode {fromNode}, fromNodeConnIndex:{fromNodeConnIndex}, remainingNodes.Count:{remainingNodes.Count}, deep:{deep}, pendingConnections:{pendingConnections}");
//         //     if (fromNode == null)
//         //     {
//         //         // First node
//         //         Node node = remainingNodes.Find(n => n.type == NodeType.Enter);
//         //         remainingNodes.Remove(node);
//         //         node.deep = deep;
//         //         int connCount = Random.Range(node.asset.MinConnections, node.asset.MaxConnections);
//         //         connCount = 1;
//         //         List<int> indices = GetFreeConnectionIndices(node);
//         //         var index = Random.Range(0, indices.Count);


//         //         ConnectNode(node, index, remainingNodes, deep + 1, pendingConnections + 1);

//         //     }
//         //     else
//         //     {

//         //         // Choose another node to connect
//         //         Node node = null;
//         //         if (deep < 2)
//         //         {
                    
//         //             // Router preferred
//         //             node = remainingNodes.Find(n => n.type == NodeType.Router);

//         //         }
//         //         if (node == null)
//         //         {
//         //             List<Node> tmp = new List<Node>();
//         //             if (pendingConnections == 1 && remainingNodes.Count > 1)
//         //                 tmp = remainingNodes.FindAll(n => n.asset.MaxConnections > 1);
//         //             else
//         //                 tmp = remainingNodes.FindAll(n => true);

//         //             node = tmp[Random.Range(0, tmp.Count)];
//         //         }

//         //         Debug.Log($"New node chosen {nodes.IndexOf(node)}:{node.type}");
//         //         remainingNodes.Remove(node);
//         //         node.deep = deep;

//         //         // Update from node connection
//         //         fromNode.connections[fromNodeConnIndex] = nodes.IndexOf(node);
//         //         // Update new node connection
//         //         int connIndex = (fromNodeConnIndex + 2) % 4;
//         //         node.connections[connIndex] = nodes.IndexOf(fromNode);

//         //         Debug.Log($"Connecting {nodes.IndexOf(node)}:{node.type} to {nodes.IndexOf(fromNode)}:{fromNode.type}, connection:{connIndex}<->{fromNodeConnIndex}, remainingNodes.Count:{remainingNodes.Count}, deep:{deep}, pendingConnections:{pendingConnections}");

//         //         // Create new connections
//         //         List<int> indices = GetFreeConnectionIndices(node);
//         //         if (indices.Count == 0) return;
//         //         if (indices.Count >= node.asset.MaxConnections) return;
//         //         Debug.Log($"Free connections:{indices.Count}");
//         //         //int max = Mathf.Max(indices.Count, node.asset.MaxConnections);
                
//         //         int indexCount = Random.Range(node.asset.MinConnections, indices.Count);
//         //         Debug.Log($"This node will have {indexCount} other connection/s");
//         //         for (int i = 0; i < indexCount; i++)
//         //         {
//         //             var index = indices[Random.Range(0, indices.Count)];
//         //             indices.Remove(index);

//         //             ConnectNode(node, index, remainingNodes, deep + 1, pendingConnections + indexCount);
//         //         }

//         //     }

//         // }

//         List<int> GetFreeConnectionIndices(Node node)
//         {
//             List<int> indices = new List<int>();
//             for (int i = 0; i < node.connections.Length; i++)
//             {
//                 //Debug.Log($"AAAAAAAAA nodeConnection:{node.connections[i]} is null {node.connections[i]==null}");
//                 if (node.connections[i] < 0)
//                     indices.Add(i);
//             }
//             return indices;
//         }

//         void CreateNodeObjects()
//         {
//             foreach (var node in nodes)
//                 node.nodeObject = NodeManager.Instance.CreateNode(node.asset, level);
//         }

//         void ArrangeNodes()
//         {
//             // Start with the enter node
//             var node = nodes.Find(n => n.type == NodeType.Enter);
//             // Move the enter node at zero
//             node.nodeObject.transform.position = Vector3.zero;
//             node.nodeObject.transform.rotation = Quaternion.identity;
//         }

//         NodeType NodeAssetToNodeType(NodeAsset asset)
//         {
//             if (asset.GetType() == typeof(FirewallNodeAsset))
//                 return NodeType.Firewall;
//             if (asset.GetType() == typeof(ResearchNodeAsset))
//                 return NodeType.Research;
//             if (asset.GetType() == typeof(DataCenterNodeAsset))
//                 return NodeType.DataCenter;
//             if (asset.GetType() == typeof(AntiSpywareNodeAsset))
//                 return NodeType.AntiSpyware;
//             if (asset.GetType() == typeof(SystemAdminNodeAsset))
//                 return NodeType.SystemAdmin;
//             if (asset.GetType() == typeof(EnterNodeAsset))
//                 return NodeType.Enter;

//             return NodeType.Router;

//         }

        

//         void DebugNodes()
//         {
//             Debug.Log($"[Nodes - Count:{nodes.Count}]");
//             foreach (var node in nodes)
//                 Debug.Log(node);
//         }

//     }
// }