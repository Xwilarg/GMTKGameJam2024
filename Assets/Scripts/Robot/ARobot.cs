using Gmtk.Prop;
using Gmtk.SO;
using Gmtk.SO.Part;
using UnityEngine;

namespace Gmtk.Robot
{
    public class ARobot : MonoBehaviour
    {
        [SerializeField]
        private Transform _wheelsAnchor, _handsAnchor, _cpuAnchor, _clothesAnchor;

        [SerializeField]
        private Transform _rotTarget;

        private Rigidbody _rb;
        private Detector _detector;

        protected IInteractable _interactionTarget;

        public HandInfo Hands { set; get; }
        public WheelInfo Wheels { set; get; }
        public CPUInfo CPU { set; get; }

        private GameObject _wheelsGo, _handsGo, _cpuGo, _clothesGo;

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
                    ToggleInteract(true);
                }
            });
            _detector.TriggerExitEvt.AddListener((coll) =>
            {
                if (_interactionTarget != null && coll.gameObject.GetInstanceID() == _interactionTarget.ID)
                {
                    _interactionTarget = null;
                    ToggleInteract(false);
                }
            });
        }

        protected virtual void ToggleInteract(bool value)
        { }

        public void AddPart(APartInfo part)
        {
            if (part is CPUInfo cpu)
            {
                if (_cpuGo != null) Destroy(_cpuGo);
                if (_clothesGo != null) Destroy(_clothesGo);

                _cpuGo = Instantiate(part.GameObject, _cpuAnchor.transform);
                if (cpu.OptionalClothes != null)
                {
                    _clothesGo = Instantiate(cpu.OptionalClothes, _clothesAnchor.transform);
                }
                CPU = cpu;
            }
            else if (part is WheelInfo wheels)
            {
                if (_wheelsGo != null) Destroy(_wheelsGo);

                Instantiate(part.GameObject, _wheelsAnchor.transform);
                Wheels = wheels;
            }
            else if (part is HandInfo hands)
            {
                if (_handsGo != null) Destroy(_handsGo);

                Instantiate(part.GameObject, _handsAnchor.transform);
                Hands = hands;
            }
        }

        public bool TryCarry(ConstructionPart part)
        {
            if (Hands.CanGrab && Carrying == null)
            {
                part.transform.parent = _detector.transform;
                part.transform.position = _detector.transform.position;

                Carrying = part;
                return true;
            }
            return false;
        }

        private int Clamp1Int(float value)
        {
            if (value < 0f) return -1;
            if (value > 0f) return 1;
            return 0;
        }

        public void Move(Vector2 dir, float speed)
        {
            _rb.linearVelocity = new(dir.x * speed * Wheels.Speed, _rb.linearVelocity.y, dir.y * speed * Wheels.Speed);

            _rotTarget.LookAt(transform.position + new Vector3(dir.x, 0f, dir.y), Vector3.up);
        }
    }
}
