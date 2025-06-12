using System.Collections;
using System.Collections.Generic;
using BNF.Scriptables;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace BNF
{
    public enum ProgramState { Blocked, Ready, Running }

    public abstract class Program : MonoBehaviour
    {
        ProgramState state = ProgramState.Ready;

        ProgramAsset asset;
        

        int level;

        
        

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            switch (state)
            {
                case ProgramState.Running:
                    UpdateRunningState();
                    break;
                case ProgramState.Blocked:
                    UpdateBlockedState();
                    break;
                case ProgramState.Ready:
                    UpdateReadyState();
                    break;
            }
            
                
        }

        protected virtual void UpdateRunningState() { }

        protected virtual void UpdateBlockedState() { }
        protected virtual void UpdateReadyState() { }

        public virtual void Init(ProgramAsset asset, int level)
        {
            this.asset = asset;
            this.level = level;
            
        }
    }
}