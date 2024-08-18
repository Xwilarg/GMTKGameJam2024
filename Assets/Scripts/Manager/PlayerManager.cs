using Gmtk.Robot;
using Gmtk.SO;
using Gmtk.VN;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gmtk.Manager
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { private set; get; }

        [SerializeField]
        private CPUInfo[] _cpus;

        private int _index;

        public bool HasJoin => _index > 0;

        private void Awake()
        {
            Instance = this;
        }

        public void OnJoin(PlayerInput player)
        {
            player.GetComponent<ARobot>().AddPart(_cpus[_index % _cpus.Length]);

            if (_index == 0)
            {
                VNManager.Instance.StartTutorial();
            }

            _index++;
        }
    }
}
