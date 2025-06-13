using System.Collections;
using System.Collections.Generic;
using BNF.Scriptables;
using UnityEngine;

namespace BNF
{
    public class AntiSpyware : Program
    {
        float detectionSpeed;

        float detectionProgress = 0;
        public float DetectionProgress
        {
            get{ return detectionProgress; }
        }

        Program fighter;

        Program blocker;



        // Update is called once per frame
        protected override void UpdateRunningState()
        {
            base.UpdateRunningState();

            if(PlayerController.Instance.Velocity.magnitude > 0)
                detectionProgress += Time.deltaTime * detectionSpeed * Nia.Instance.GetAntiSpywareDetectionSpeedFactor();
        }


        public override void Init(ProgramAsset asset, int level)
        {
            base.Init(asset, level);

            AntiSpywareAsset sa = asset as AntiSpywareAsset;

            detectionSpeed = sa.DetectionSpeedLevels[Mathf.FloorToInt(level)];

            //fighter = new Program()
            //blocker = new Program()

            
        }
    }
}