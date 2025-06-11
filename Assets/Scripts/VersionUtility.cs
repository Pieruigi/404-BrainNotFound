using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BNF
{
    public class VersionUtility
    {
        public const string MaxVersion = "9.9.9";
        public const string MinVersion = "0.0.1";



        public static bool CheckVersion(string version)
        {
            string[] mmp = version.Split(".");
            if (mmp.Length != 3)
            {
                Debug.LogError($"VersionUtility.CheckVersion() - Format error, version:{version}");
                return false;
            }


            for (int i = 0; i < mmp.Length; i++)
            {

                if (!int.TryParse(mmp[i], out var v))
                {
                    Debug.LogError($"VersionUtility.CheckVersion() - Format error, version:{version}");
                    return false;
                }

                if (v < 0 || v > 9)
                {
                    Debug.LogError($"VersionUtility.CheckVersion() - Format error, version:{version}");
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// Upgrade a given version depending on the amount of snipped code we are passing in. 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool TryUpgradeVersion(string version, int codeAmount, out string upgradedVersion, out int codeAdded)
        {
            upgradedVersion = version;
            codeAdded = 0;

            if (!CheckVersion(version)) return false;
            if (!VersionCanBeUpgraded(version)) return false;

            // Upgrade version
            string[] mmp = version.Split(".");
            int major = int.Parse(mmp[0]);
            int minor = int.Parse(mmp[1]);
            int patch = int.Parse(mmp[2]);


            while (codeAdded < codeAmount && VersionCanBeUpgraded(upgradedVersion))
            {
                // Add to patch
                codeAdded++;
                patch++;

                if (patch == 10)
                {
                    patch = 0;
                    minor++;
                    if (minor == 10)
                    {
                        minor = 0;
                        major++;
                    }
                }

                upgradedVersion = $"{major}.{minor}.{patch}";

            }



            return true;
        }

        public static bool TryDowngradeVersion(string version, int codeAmount, out string downgradedVersion, out int codeRemoved)
        {
            downgradedVersion = version;
            codeRemoved = 0;

            if (!CheckVersion(version)) return false;
            if (!VersionCanBeDowngraded(version)) return false;

            // Downgrade version
            string[] mmp = version.Split(".");
            int major = int.Parse(mmp[0]);
            int minor = int.Parse(mmp[1]);
            int patch = int.Parse(mmp[2]);

            while (codeRemoved < codeAmount && VersionCanBeDowngraded(downgradedVersion))
            {
                codeRemoved++;
                patch--;

                if (patch < 0)
                {
                    patch = 9;
                    minor--;
                    if (minor < 0)
                    {
                        minor = 9;
                        major--;
                    }
                }

                downgradedVersion = $"{major}.{minor}.{patch}";
            }

            return true;
        }

        public static bool VersionCanBeUpgraded(string version)
        {
            if (!CheckVersion(version)) return false;
            if (MaxVersion.Equals(version)) return false;
            return true;
        }

        public static bool VersionCanBeDowngraded(string version)
        {
            if (!CheckVersion(version)) return false;
            if (MinVersion.Equals(version)) return false;
            return true;
        }

        
        
    }
}