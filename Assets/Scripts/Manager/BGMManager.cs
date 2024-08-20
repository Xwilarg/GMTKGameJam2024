using FMOD;
using UnityEngine;

namespace Gmtk.Manager
{
    public class BGMManager : MonoBehaviour
    {
        public static BGMManager Instance { private set; get; }

        private FMOD.Studio.EventInstance gameMusic;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            gameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/gameplay_music");
            gameMusic.start();
            gameMusic.setParameterByName("Music", 0);
        }

        public void UpdateBGM()
        {
            UnityEngine.Debug.Log("Updating BGM parameter to 1");
            gameMusic.setParameterByName("Music", 1);
        }

        public void UpdateBGM2()
        {
            UnityEngine.Debug.Log("Updating BGM parameter to 2");
            gameMusic.setParameterByName("Music", 2);
        }
    }
}
