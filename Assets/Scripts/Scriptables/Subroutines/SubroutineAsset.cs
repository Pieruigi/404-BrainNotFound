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
        float minLevel;
        public float MinLevel
        {
            get { return minLevel; }
        }

        [SerializeField]
        GameObject gamePrefab;
        public GameObject GamePrefab
        {
            get { return gamePrefab; }
        }

        [SerializeField]
        string code;
        public string Code
        {
            get{ return code; }
        }

    }
}