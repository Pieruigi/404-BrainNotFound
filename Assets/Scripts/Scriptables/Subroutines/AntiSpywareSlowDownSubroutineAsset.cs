using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNF.Scriptables
{
    public class AntiSpywareSlowDownSubroutineAsset : SubroutineAsset
    {
        [SerializeField]
        float slowDownPower = 10; // Percentage

        public float SlowDownPower
        {
            get{ return slowDownPower; }
        }

    }
}