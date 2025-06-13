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