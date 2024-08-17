using Gmtk.Prop;
using Gmtk.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gmtk.Robot.Player
{
    public class PlayerController : ARobot
    {
        [SerializeField]
        private WheelInfo _defaultWheels;

        [SerializeField]
        private HandInfo _defaultHands;

        private Vector2 _mov;

        private const float Speed = 10f;

        protected override void Awake()
        {
            base.Awake();

            _wheels = _defaultWheels;
            _hands = _defaultHands;
        }

        private void FixedUpdate()
        {
            Move(_mov, Speed);
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>();
        }

        public void OnAction(InputAction.CallbackContext value)
        {
            if (value.performed && _interactionTarget != null)
            {
                _interactionTarget.Interact();
            }
        }
    }
}