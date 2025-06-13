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
        int minLevel;


        int level;
        public int Level
        {
            get { return level; }
        }

        int stability;
        public int Stability
        {
            get { return stability; }
        }

        SubroutineState state = SubroutineState.Locked;

        SubroutineAsset asset;


        public virtual void Init(SubroutineAsset asset, string data = null)
        {
            this.asset = asset;
            minLevel = asset.MinLevel;


            if (!string.IsNullOrEmpty(data))
            {
                string[] s = data.Split("|");
                level = int.Parse(s[0]);
                stability = int.Parse(s[1]);
            }
            else
            {
                level = minLevel;
                stability = VersionUtility.MinStability;
            }

            if (Nia.Instance.Level >= level)
                state = SubroutineState.Ready;
        }

        public void StartSubroutine()
        {
            if (state != SubroutineState.Ready) return;
            state = SubroutineState.Running;
        }
    }
}