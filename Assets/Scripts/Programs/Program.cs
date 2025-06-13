using System.Collections;
using System.Collections.Generic;
using BNF.Scriptables;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace BNF
{
    public enum ProgramState { Blocked, Ready, Running, Paused }

    public abstract class Program : MonoBehaviour
    {
        public delegate void OnCreatedDelegate(Program program);
        public static OnCreatedDelegate OnCreated;

        

        ProgramState state = ProgramState.Ready;

        ProgramAsset asset;


        int level;




        // Start is called before the first frame update
        void Start()
        {
            OnCreated?.Invoke(this);
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

        public void StartProgram()
        {
            if (state != ProgramState.Ready) return;
            state = ProgramState.Running;
        }

        public void StopProgram()
        {
            if (state != ProgramState.Running) return;
            state = ProgramState.Ready;
        }

        public void BlockProgram()
        {
            if (state == ProgramState.Blocked) return;
            state = ProgramState.Blocked;
        }

        public void UnblockProgram()
        {
            if (state != ProgramState.Blocked) return;
            state = ProgramState.Ready;
        }

        public void PauseProgram()
        {
            if (state != ProgramState.Running) return;
            state = ProgramState.Paused;
        }
    }
}