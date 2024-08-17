using UnityEngine;
using UnityEngine.InputSystem;

namespace Gmtk.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rb;
        private Vector2 _mov;

        private const float Speed = 10f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = new(_mov.x * Speed, _rb.linearVelocity.y, _mov.y * Speed);
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>();
        }
    }
}