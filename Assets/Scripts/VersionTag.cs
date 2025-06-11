// using System;
// using UnityEngine;

// [System.Serializable]
// public class VersionTag : IComparable<VersionTag>
// {
//     [SerializeField]

//     int major, minor;


//     public VersionTag(int major, int minor)
//     {
//         this.major = major;
//         this.minor = minor;
        
//     }



//     public static VersionTag Parse(string version)
//     {
//         var parts = version.Split('.');
//         if (parts.Length != 2)
//             throw new FormatException("Invalid version format");

//         return new VersionTag(
//             int.Parse(parts[0]),
//             int.Parse(parts[1])
//         );
//     }

//     public int CompareTo(VersionTag other)
//     {
//         if (major != other.major) return major.CompareTo(other.major);
//         return minor.CompareTo(other.minor);
        
//     }

//     public override string ToString() => $"{major}.{minor}";

//     public override bool Equals(object obj) => obj is VersionTag other && this == other;
//     public override int GetHashCode() => HashCode.Combine(major, minor);

//     public static bool operator ==(VersionTag a, VersionTag b) => a.CompareTo(b) == 0;
//     public static bool operator !=(VersionTag a, VersionTag b) => !(a == b);
//     public static bool operator <(VersionTag a, VersionTag b) => a.CompareTo(b) < 0;
//     public static bool operator <=(VersionTag a, VersionTag b) => a.CompareTo(b) <= 0;
//     public static bool operator >(VersionTag a, VersionTag b) => a.CompareTo(b) > 0;
//     public static bool operator >=(VersionTag a, VersionTag b) => a.CompareTo(b) >= 0;

//     public void Downgrade(int amount)
//     {

//         while (amount > 0 && CanBeDowngraded())
//         {
//             amount--;
//             patch--;

//             if (patch < 0)
//             {
//                 patch = 9;
//                 minor--;
//                 if (minor < 0)
//                 {
//                     minor = 9;
//                     major--;
//                 }
//             }
//         }
//     }

//     public bool CanBeDowngraded()
//     {
//         return !(major == 0 && minor == 1);
//     }

//     public bool CanBeUpgraded()
//     {
//         return !(major == 5 && minor == 0);
//     }
// }