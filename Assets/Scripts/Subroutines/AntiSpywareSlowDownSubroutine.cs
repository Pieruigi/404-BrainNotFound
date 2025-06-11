using System.Collections;
using System.Collections.Generic;
using BNF.Scriptables;
using UnityEngine;

namespace BNF
{
    public class AntiSpywareSlowDownSubroutine : Subroutine
    {
        float detectionSpeedFactor;

        public override void Init(SubroutineAsset asset, string data = null)
        {
            base.Init(asset, data);

            detectionSpeedFactor = (asset as AntiSpywareSlowDownSubroutineAsset).DetectionSpeedFactorLevels[Mathf.FloorToInt(Level)];
        }

        
    }
}