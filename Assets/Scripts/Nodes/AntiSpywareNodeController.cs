using System.Collections;
using System.Collections.Generic;
using BNF.Scriptables;
using UnityEngine;

namespace BNF
{
    public class AntiSpywareNodeController : NodeController
    {
        float detectionSpeed;

        float detectionProgress = 0;
        public float DetectionProgress
        {
            get { return detectionProgress; }
        }

        

        public override void Init(NodeAsset asset, int level)
        {
            Debug.Log($"[Node initialization - asset:{asset}, level:{level}");
            base.Init(asset, level);

            AntiSpywareNodeAsset sa = asset as AntiSpywareNodeAsset;

            detectionSpeed = sa.DetectionSpeedLevels[Mathf.FloorToInt(level)];

            // Initialize the fighter program
        }

        public override void DoUpdate()
        {
            base.DoUpdate();
            // Detection progress
            if (PlayerController.Instance.Velocity.magnitude > 0)
                detectionProgress += Time.deltaTime * detectionSpeed * Nia.Instance.GetAntiSpywareDetectionSpeedFactor();
        }
    }
}