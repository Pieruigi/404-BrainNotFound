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
        [SerializeField]
        Transform subroutinesContainer;

        float level = 0;
        public float Level
        {
            get{ return level; }
        }


        List<Subroutine> subroutines = new List<Subroutine>();

        string code = "nia";

       

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
                level = float.Parse(data);
            }
            else
            {
                level = VersionUtility.MinVersion;
            }

            // Init subroutines
            InitSubroutines();
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
                
                Debug.Log($"Creating subroutine {s.Name} with version {(string.IsNullOrEmpty(data) ? s.MinLevel : data)}");

                // Create a new empty object
                GameObject obj = Instantiate(s.GamePrefab, subroutinesContainer);
                obj.GetComponent<Subroutine>().Init(s, data);
            }

        }


    }
}