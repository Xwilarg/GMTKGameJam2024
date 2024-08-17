using Gmtk.SO;
using UnityEngine;

namespace Gmtk.Robot
{
    public class ARobot : MonoBehaviour
    {
        private Rigidbody _rb;

        protected HandInfo _hands { set; private get; }
        protected WheelInfo _wheels { set; private get; }

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 dir, float speed)
        {
            _rb.linearVelocity = new(dir.x * speed * _wheels.Speed, _rb.linearVelocity.y, dir.y * speed * _wheels.Speed);
        }
    }
}
