using System.Collections;
using System.Collections.Generic;
using BNF.Scriptables;
using UnityEngine;
using UnityEngine.Animations;

namespace BNF
{
    public class AntiSpywareSlowDownSubroutine : Subroutine
    {
        float detectionSpeedFactor;
        public float DetectionSpeedFactor
        {
            get{ return detectionSpeedFactor; }
        }

        public override void Init(SubroutineAsset asset, string data = null)
        {
            base.Init(asset, data);

            detectionSpeedFactor = (asset as AntiSpywareSlowDownSubroutineAsset).DetectionSpeedFactorLevels[Mathf.FloorToInt(Level)];

            
        }

        
    }
}