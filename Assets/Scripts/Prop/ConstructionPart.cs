using Gmtk.Robot;
using Gmtk.SO.Part;
using UnityEngine;

namespace Gmtk.Prop
{
    public class ConstructionPart : MonoBehaviour
    {
        [SerializeField]
        private APartInfo _targetPart;

        public APartInfo TargetPart
        {
            set => _targetPart = value;
            get => _targetPart;
        }
    }
}
