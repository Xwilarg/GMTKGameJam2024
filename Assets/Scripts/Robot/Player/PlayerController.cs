using Gmtk.Manager;
using Gmtk.Map;
using Gmtk.Menu;
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

        [SerializeField]
        private DispenserItemSelector _dispenserItemSelector;

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
            if (GameManager.Instance.DidRoundEnd) Move(Vector2.zero, 0f);
            else Move(_mov, Speed);
        }

        public void ShowItemSelector(Dispenser dispenser)
        {
            _dispenserItemSelector.gameObject.SetActive(true);
            _dispenserItemSelector.Init(dispenser);
            Move(Vector2.zero, 0f);
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            if (_dispenserItemSelector.gameObject.activeInHierarchy)
            {
                if (value.phase == InputActionPhase.Started)
                {
                    var d = value.ReadValue<Vector2>();
                    if (d.x < 0f) _dispenserItemSelector.OnPrev();
                    else if (d.x > 0f) _dispenserItemSelector.OnNext();
                }
            }
            else
            {
                _mov = value.ReadValue<Vector2>();
            }
        }

        public void OnAction(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Started)
            {
                if (_dispenserItemSelector.gameObject.activeInHierarchy)
                {
                    _dispenserItemSelector.OnConfirm();
                    _dispenserItemSelector.gameObject.SetActive(false);
                }
                else if (_interactionTarget != null && _interactionTarget.CanInteract)
                {
                    _interactionTarget.Interact(this);
                    ToggleInteract(false);
                }
            }
        }

        protected override void ToggleInteract(bool value)
        {
            _interactionText.SetActive(value);
        }
    }
}