using Gmtk.Robot.AI;
using Gmtk.SO.Part;
using UnityEngine;

namespace Gmtk.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/CPUInfo", fileName = "CPUInfo")]
    public class CPUInfo : APartInfo
    {
        public AIBehavior AI;

        public GameObject OptionalClothes;
    }
}