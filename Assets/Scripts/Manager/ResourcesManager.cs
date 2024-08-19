using Gmtk.SO.Part;
using Gmtk.VN;
using System.Linq;
using UnityEngine;

namespace Gmtk.Manager
{
    public class ResourcesManager : MonoBehaviour
    {
        public static ResourcesManager Instance { private set; get; }

        [SerializeField]
        private Material _matRed, _matGreen, _matBlue;

        [SerializeField]
        private APartInfo[] _allAvailableParts;

        public APartInfo[] AllAvailableParts => _allAvailableParts.Where(x => VNManager.Instance.Progress >= x.UnlockOn).ToArray();

        private void Awake()
        {
            Instance = this;
        }

        public Material GetMat(TargetColor color)
            => color switch
            {
                TargetColor.Red => _matRed,
                TargetColor.Green => _matGreen,
                TargetColor.Blue => _matBlue,
                _ => throw new System.NotImplementedException()
            };
    }

    public enum TargetColor
    {
        Red,
        Green,
        Blue
    }
}
