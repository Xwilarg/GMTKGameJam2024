using UnityEngine;

namespace Gmtk.Manager
{
    public class BGMManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
