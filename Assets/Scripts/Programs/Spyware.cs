using System.Collections;
using System.Collections.Generic;
using BNF.Scriptables;
using UnityEngine;

namespace BNF
{
    public class Spyware : Program
    {
        float detectionSpeed;

        // Update is called once per frame
        protected override void UpdateRunningState()
        {
            base.UpdateRunningState();


        }


        public override void Init(ProgramAsset asset, int level)
        {
            base.Init(asset, level);

            SpywareAsset sa = asset as SpywareAsset;

            detectionSpeed = sa.DetectionSpeedLevels[Mathf.FloorToInt(level)];
            
        }
    }
}