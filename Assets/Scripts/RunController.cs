using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BNF.Scriptables;
using UnityEngine;
using UnityEngine.Animations;

namespace BNF
{
    public class RunController : Singleton<RunController>
    {

        int level = 0;

        List<ProgramAsset> programAssets;

        protected override void Awake()
        {
            base.Awake();
            programAssets = Resources.LoadAll<ProgramAsset>(ProgramAsset.ResourceFolder).ToList();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void CreateSpywareNode()
        {
            GameObject go = new GameObject("SpywareNode");
            ProgramAsset asset = programAssets.Find(a => typeof(SpywareAsset) == a.GetType());
            
        }
    }
}