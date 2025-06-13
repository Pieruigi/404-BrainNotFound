using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BNF.Scriptables;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using UnityEngine.Animations;

namespace BNF
{
    public class NodeManager : Singleton<NodeManager>
    {
        public delegate void OnNodeCreatedDelegate(NodeController nodeController);
        public static OnNodeCreatedDelegate OnNodeCreated;

        
        [SerializeField]
        Transform nodeContainer;

        List<NodeController> nodes = new List<NodeController>();

        protected override void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        void Start()
        {
            //__Test();


        }

        // Update is called once per frame
        void Update()
        {

        }

        // private async void __Test()
        // {
        //     CreateSpywareNode();

        //     await Task.Delay(3000);

        //     Nia.Instance.StartSubroutineAll();
        //     foreach (var node in nodes)
        //         node.StartProgramAll();
        // }

        // GameObject CreateSpywareNode()
        // {
        //     var node = CreateNodeController("Spyware");

        //     ProgramAsset asset = programAssets.Find(a => typeof(AntiSpywareAsset) == a.GetType());
        //     // Create component
        //     var program = node.gameObject.AddComponent<AntiSpyware>();
        //     program.Init(asset, level);

        //     // Add program to node controller
        //     node.AddProgram(program);

        //     return node.gameObject;
        // }

        // NodeController CreateNodeController(string name)
        // {
        //     // Create object
        //     GameObject go = new GameObject(name);

        //     go.transform.parent = programContainer;

        //     // Assign node component
        //     var node = go.AddComponent<NodeController>();

        //     // Add node to list
        //     nodes.Add(node);

        //     return node;
        // }

        public GameObject CreateNode(NodeAsset asset, int level)
        {
            // Instantiate node scene object
            var obj = Instantiate(asset.NodeObject, nodeContainer);

            // Get node controller
            var nc = obj.GetComponent<NodeController>();

            // Initialize node controller
            nc.Init(asset, level);

            // Add node to list
            nodes.Add(nc);

            // Report creation
            OnNodeCreated?.Invoke(nc);

            // Return
            return obj;
        }

        public void StartNodeAll()
        {
            foreach (var node in nodes)
            {
                node.StartNode();
            }
                
        }
    }
}