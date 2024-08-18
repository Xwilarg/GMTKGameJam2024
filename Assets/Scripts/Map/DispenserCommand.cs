using Gmtk.Prop;
using Gmtk.Robot;
using Gmtk.Robot.Player;
using UnityEngine;

namespace Gmtk.Map
{
    public class DispenserCommand : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Dispenser _target;

        public bool CanInteract => true;

        public int ID => gameObject.GetInstanceID();

        public void Interact(ARobot robot)
        {
            if (robot is PlayerController pc)
            {
                pc.ShowItemSelector(_target);
            }
        }
    }
}
