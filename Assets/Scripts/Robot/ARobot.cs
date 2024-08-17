using Gmtk.Prop;
using Gmtk.SO;
using UnityEngine;

namespace Gmtk.Robot
{
    public class ARobot : MonoBehaviour
    {
        private Rigidbody _rb;
        private Detector _detector;

        protected IInteractable _interactionTarget;

        public HandInfo Hands { set; get; }
        public WheelInfo Wheels { set; get; }

        private Vector2 _lastDir = Vector2.up;

        public ConstructionPart Carrying { set; get; }

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _detector = GetComponentInChildren<Detector>();

            _detector.TriggerEnterEvt.AddListener((coll) =>
            {
                if (coll.TryGetComponent<IInteractable>(out var comp))
                {
                    _interactionTarget = comp;
                }
            });
            _detector.TriggerExitEvt.AddListener((coll) =>
            {
                if (coll == (object)_interactionTarget)
                {
                    _interactionTarget = null;
                }
            });

            SetDetectorPos();
        }

        public void TryCarry(ConstructionPart part)
        {
            if (Hands.CanGrab && Carrying == null)
            {
                part.CanInteract = false;
                part.transform.parent = _detector.transform;
                part.transform.position = _detector.transform.position;

                Carrying = part;
            }
        }

        private void SetDetectorPos()
        {
            _detector.transform.position = transform.position + new Vector3(_lastDir.x, 0f, _lastDir.y);
        }

        private int Clamp1Int(float value)
        {
            if (value < 0f) return -1;
            if (value > 0f) return 1;
            return 0;
        }

        public void Move(Vector2 dir, float speed)
        {
            if (dir.magnitude > 0f)
            {
                _lastDir = new(Clamp1Int(dir.x), Clamp1Int(dir.y));
            }

            _rb.linearVelocity = new(dir.x * speed * Wheels.Speed, _rb.linearVelocity.y, dir.y * speed * Wheels.Speed);

            SetDetectorPos();
        }
    }
}
