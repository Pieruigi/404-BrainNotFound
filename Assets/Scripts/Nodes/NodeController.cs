using System.Collections;
using System.Collections.Generic;
using BNF.Scriptables;
using UnityEngine;

namespace BNF
{

    public abstract class NodeController : MonoBehaviour
    {

        NodeAsset asset;
        int level;

        bool running = false;
        

        // Update is called once per frame
        protected virtual void Update()
        {
            if (!running) return;
            DoUpdate();
        }

        

        public virtual void Init(NodeAsset asset, int level)
        {
            this.asset = asset;
            this.level = level;
        }

        public virtual void StartNode()
        {
            running = true;
        }

        public virtual void DoUpdate()
        {

        }
        
    }
}