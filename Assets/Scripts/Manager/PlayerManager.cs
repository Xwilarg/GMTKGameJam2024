using Gmtk.Robot;
using Gmtk.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gmtk.Manager
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        private CPUInfo[] _cpus;

        private int _index;

        public void OnJoin(PlayerInput player)
        {
            player.GetComponent<ARobot>().AddPart(_cpus[_index % _cpus.Length]);

            _index++;
        }
    }
}
