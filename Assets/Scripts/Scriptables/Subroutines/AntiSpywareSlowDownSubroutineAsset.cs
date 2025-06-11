using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNF.Scriptables
{
    public class AntiSpywareSlowDownSubroutineAsset : SubroutineAsset
    {
        [SerializeField]
        float[] detectionSpeedFactorLevels = new float[] { 1.0f, 0.9f, 0.8f, 0.7f, 0.6f, 0.5f };

        public float[] DetectionSpeedFactorLevels
        {
            get{ return detectionSpeedFactorLevels; }
        }

    }
}