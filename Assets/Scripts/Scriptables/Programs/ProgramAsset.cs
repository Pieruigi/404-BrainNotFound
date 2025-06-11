using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNF.Scriptables
{
    public abstract class ProgramAsset : ScriptableObject
    {
        public const string ResourceFolder = "Programs";

        [SerializeField]
        string _name;
        public string Name
        {
            get { return _name; }
        }


                

        
    }
}