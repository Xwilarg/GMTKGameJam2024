using Gmtk.SO.Part;
using Gmtk.VN;
using UnityEngine;

namespace Gmtk.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/HandInfo", fileName = "HandInfo")]
    public class HandInfo : APartInfo
    {
        public bool CanGrab;
        public Job TargetJob;
    }
}