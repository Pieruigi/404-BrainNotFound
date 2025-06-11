using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNF.Scriptables
{
    public abstract class SubroutineAsset : ScriptableObject
    {
        public const string ResourceFolder = "Subroutines";

        [SerializeField]
        string _name;

        public string Name
        {
            get { return name; }
        }

        [SerializeField]
        string minVersion;
        public string MinVersion
        {
            get { return name; }
        }

        [SerializeField]
        MonoBehaviour component;
        public MonoBehaviour Component
        {
            get { return component; }
        }

        [SerializeField]
        string code;
        public string Code
        {
            get{ return code; }
        }

    }
}