using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNF.Scriptables
{
    public abstract class NodeAsset : ScriptableObject
    {
        public const string ResourceFolder = "Nodes";

        [SerializeField]
        string _name;
        public string Name
        {
            get { return name; }
        }

        [SerializeField]
        int minLevel;
        public int MinLevel
        {
            get{ return minLevel; }
        }

        [SerializeField]
        int minNunber;
        public int MinNumber
        {
            get{ return minNunber; }
        }

        [SerializeField]
        int maxNumber;
        public int MaxNumber
        {
            get{ return maxNumber; }
        }

        [SerializeField]
        GameObject nodeObject;
        public GameObject NodeObject
        {
            get{ return nodeObject; }
        }

    }
}