using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gmtk.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("Main");

            //Sound//

            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sfx_ui_click");
        }
    }
}
