using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BNF.Scriptables;
using UnityEngine;

namespace BNF
{
    public enum NodeType { Router, AntiSpyware }

    public class NetBuilder : MonoBehaviour
    {

        int level = 0;

        int minNodes = 12, maxNodes = 15;

        List<NodeAsset> nodeAssets;

        void Awake()
        {
            nodeAssets = Resources.LoadAll<NodeAsset>(NodeAsset.ResourceFolder).ToList();
        }

        // Start is called before the first frame update
        void Start()
        {
            __Test();
            

        }

        // Update is called once per frame
        void Update()
        {

        }

        async void __Test()
        {
            NodeManager.Instance.CreateNode(nodeAssets[0], level);
            await Task.Delay(3000);
            NodeManager.Instance.StartNodeAll();
        }
    }
}