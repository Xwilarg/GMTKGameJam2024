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

        [SerializeField]
        private GameObject _interactionText;

        private Vector2 _mov;

        private const float Speed = 10f;

        protected override void Awake()
        {
            base.Awake();

            AddPart(_defaultWheels);
            AddPart(_defaultHands);
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
            if (value.phase == InputActionPhase.Started && _interactionTarget != null && _interactionTarget.CanInteract)
            {
                _interactionTarget.Interact(this);
                ToggleInteract(false);
            }
        }

        protected override void ToggleInteract(bool value)
        {
            _interactionText.SetActive(value);
        }
    }
}