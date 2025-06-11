using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BNF.Scriptables;
using Unity.VisualScripting;
using UnityEngine;

namespace BNF
{
    public class Nia : Singleton<Nia>
    {
        // string version = "0.0.1";
        // public string Version
        // {
        //     get { return version; }
        // }



        List<Subroutine> subroutines = new List<Subroutine>();

        string code = "nia";

        VersionTag version;
        public VersionTag Version
        {
            get{ return version; }
        }


        protected override void Awake()
        {
            base.Awake();

        
            Init();

        }

        // Start is called before the first frame update
        void Start()
        {
         
        }



        void Init()
        {
            // Init Nia version
            if (SaveManager.TryGetCachedValue(code, out var data))
            {
                //version = data;
                version = VersionTag.Parse(data);
            }
            else
            {
                version = VersionTag.Parse("0.0.1");
            }

            // Init subroutines
            InitSubroutines();
        }
        
        void CreateSubroutineModule(SubroutineAsset asset, string data)
        {
            Debug.Log($"Creating subroutine {asset.Name} with version {(string.IsNullOrEmpty(data) ? asset.MinVersion : data)}");

        }

        void InitSubroutines()
        {
            var subAssets = Resources.LoadAll<SubroutineAsset>(SubroutineAsset.ResourceFolder).ToList();
            foreach (var s in subAssets)
            {
                if (SaveManager.TryGetCachedValue(s.Code, out string data))
                {
                    Debug.Log($"Loaded {s.Code} from save file: {data}");
                }
                
                CreateSubroutineModule(s, data);
            }

        }


    }
}