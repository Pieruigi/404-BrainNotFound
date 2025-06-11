using System.Collections;
using System.Collections.Generic;
using System.Data;
using BNF.Scriptables;
using UnityEngine;

namespace BNF
{
    public abstract class Subroutine : Singleton<Subroutine>
    {
        VersionTag minVersion;
        

        VersionTag version;
        public VersionTag Version
        {
            get{ return version; }
        }

        bool running = false;
        public bool Running
        {
            get{ return running; }
        }


        public virtual void Init(SubroutineAsset asset, string data = null)
        {
            minVersion = VersionTag.Parse(asset.MinVersion);


            if (string.IsNullOrEmpty(data))
            {
                version = VersionTag.Parse(data);
            }
            else
            {
                version = minVersion;
            }

            // To be sure
            if (version < minVersion)
                version = minVersion;

            if (Nia.Instance.Version <= version)
                running = true;
        }
    }
}