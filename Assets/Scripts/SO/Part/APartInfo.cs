using Gmtk.VN;
using UnityEngine;

namespace Gmtk.SO.Part
{
    public class APartInfo : ScriptableObject
    {
        public string Name;
        [TextArea]
        public string Description;
        public Sprite Icon;
        public GameObject GameObject;
        public TutorialProgress UnlockOn;
    }
}
