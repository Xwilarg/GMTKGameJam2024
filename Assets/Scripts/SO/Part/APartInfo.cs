using UnityEngine;

namespace Gmtk.SO.Part
{
    public class APartInfo : ScriptableObject
    {
        public string Name;
        [TextArea]
        public string Description;
        public GameObject GameObject;
    }
}
