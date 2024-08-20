using UnityEngine;

namespace Gmtk.Manager
{
    public class BGMManager : MonoBehaviour
    {
        public static BGMManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void UpdateBGM()
        {
            // TODO: Fill this
        }
    }
}
