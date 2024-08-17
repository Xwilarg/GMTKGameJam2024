using Gmtk.Prop;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gmtk.Robot.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rb;
        private Vector2 _mov;

        private IInteractable _interactionTarget;

        private const float Speed = 10f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            var detector = GetComponentInChildren<Detector>();
            detector.TriggerEnterEvt.AddListener((coll) =>
            {
                if (coll.TryGetComponent<IInteractable>(out var comp))
                {
                    _interactionTarget = comp;
                }
            });
            detector.TriggerExitEvt.AddListener((coll) =>
            {
                if (coll == (object)_interactionTarget)
                {
                    _interactionTarget = null;
                }
            });
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = new(_mov.x * Speed, _rb.linearVelocity.y, _mov.y * Speed);
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