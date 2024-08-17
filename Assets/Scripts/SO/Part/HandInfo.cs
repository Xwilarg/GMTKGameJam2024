using Gmtk.SO.Part;
using UnityEngine;

namespace Gmtk.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/HandInfo", fileName = "HandInfo")]
    public class HandInfo : APartInfo
    {
        public bool CanGrab;
    }
}