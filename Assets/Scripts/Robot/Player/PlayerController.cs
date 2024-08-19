using FMOD;
using Gmtk.Manager;
using Gmtk.Map;
using Gmtk.Menu;
using Gmtk.SO;
using Gmtk.VN;
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

        //Sounds//
        private FMOD.Studio.EventInstance robotmovement;
        private bool isMoving = false;  // New variable to track movement

        protected override void Awake()
        {
            base.Awake();

            AddPart(_defaultWheels);
            AddPart(_defaultHands);

            robotmovement = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/sfx_robot_moving");
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.DidRoundEnd) Move(Vector2.zero, 0f);
            else Move(_mov, Speed);

            {
                if (_mov != Vector2.zero && !isMoving)
                {
                    isMoving = true;
                    robotmovement.setParameterByName("Movement", 0);
                    robotmovement.start();
                }
                else if (_mov == Vector2.zero && isMoving)
                {
                    isMoving = false;
                    robotmovement.setParameterByName("Movement", 1);
                    
                }
            }
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
            if (value.phase == InputActionPhase.Started && !VNManager.Instance.IsShowingIntro)
            {
                if (_dispenserItemSelector.gameObject.activeInHierarchy)
                {
                    _dispenserItemSelector.OnConfirm();
                    _dispenserItemSelector.gameObject.SetActive(false);

                    //Sound//
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sfx_option_selected_panel");
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