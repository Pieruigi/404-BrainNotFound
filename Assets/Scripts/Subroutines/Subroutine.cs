using System.Collections;
using System.Collections.Generic;
using System.Data;
using BNF.Scriptables;
using UnityEngine;

namespace BNF
{
    public enum SubroutineState { Locked, Ready, Running, Cooldown }

    public abstract class Subroutine : Singleton<Subroutine>
    {
        float minVersion;


        float version;
        public float Version
        {
            get { return version; }
        }

        SubroutineState state = SubroutineState.Locked;

        SubroutineAsset asset;


        public virtual void Init(SubroutineAsset asset, string data = null)
        {
            this.asset = asset;
            minVersion = asset.MinLevel;


            if (!string.IsNullOrEmpty(data))
            {
                version = float.Parse(data);
            }
            else
            {
                version = minVersion;
            }

            if (Nia.Instance.Version >= version)
                state = SubroutineState.Ready;
        }
    }
}