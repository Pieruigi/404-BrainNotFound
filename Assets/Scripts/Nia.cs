using System;
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

        int level = 0;
        public int Level
        {
            get { return level; }
        }

        int stability = 0;
        public int Stability
        {
            get { return stability; }
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
                string[] s = data.Split("|");
                //version = data;
                level = int.Parse(s[0]);
                stability = int.Parse(s[1]);
            }
            else
            {
                level = VersionUtility.MinLevel;
                stability = VersionUtility.MinStability;
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
                var sub = obj.GetComponent<Subroutine>();
                sub.Init(s, data);
                subroutines.Add(sub);
            }

        }

        Subroutine GetSubroutineByType(Type type)
        {
            foreach (var sub in subroutines)
            {
                if (sub.GetType() == type)
                    return sub;
            }

            return null;
        }

        public float GetAntiSpywareDetectionSpeedFactor()
        {
            var sub = GetSubroutineByType(typeof(AntiSpywareSlowDownSubroutine));
            return (sub as AntiSpywareSlowDownSubroutine).DetectionSpeedFactor;
        }

        public void StartSubroutineAll()
        {
            Debug.Log("TEST - NIA - Starting subroutines...");
            foreach (var subroutine in subroutines)
                subroutine.StartSubroutine();
        }

    }
}