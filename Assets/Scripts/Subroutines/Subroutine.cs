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
        float minLevel;


        float level;
        public float Level
        {
            get { return level; }
        }

        SubroutineState state = SubroutineState.Locked;

        SubroutineAsset asset;


        public virtual void Init(SubroutineAsset asset, string data = null)
        {
            this.asset = asset;
            minLevel = asset.MinLevel;


            if (!string.IsNullOrEmpty(data))
            {
                level = float.Parse(data);
            }
            else
            {
                level = minLevel;
            }

            if (Nia.Instance.Level >= level)
                state = SubroutineState.Ready;
        }
    }
}