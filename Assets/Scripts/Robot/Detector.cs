using UnityEngine;
using UnityEngine.Events;

namespace Gmtk.Robot
{
    public class Detector : MonoBehaviour
    {
        public UnityEvent<Collider> TriggerEnterEvt { get; } = new();
        public UnityEvent<Collider> TriggerExitEvt { get; } = new();

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnterEvt.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerEnterEvt.Invoke(other);
        }
    }
}
