using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNF.Scriptables
{
    public class AntiSpywareAsset : ProgramAsset
    {
        [SerializeField]
        float[] detectionSpeedLevels = new float[] { 0.003f, 0.004f, 0.005f, 0.006f, 0.007f, 0.008f };
        public float[] DetectionSpeedLevels
        {
            get{ return detectionSpeedLevels; }
        }
    }
}