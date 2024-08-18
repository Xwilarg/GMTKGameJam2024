using Gmtk.Manager;
using Gmtk.Prop;
using Gmtk.Robot;
using Gmtk.SO.Part;
using UnityEngine;

namespace Gmtk.Map
{
    public class Dispencer : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private ConstructionPart _prefab;

        [SerializeField]
        private APartInfo _part;

        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private TargetColor _target;

        public bool CanInteract => true;

        public int ID => gameObject.GetInstanceID();

        private ConstructionPart _currentObject;

        private void Awake()
        {
            Spawn();
        }

        private void Start()
        {
            var mr = GetComponent<MeshRenderer>();
            var mats = mr.materials;
            mats[1] = ResourcesManager.Instance.GetMat(_target);
            mr.materials = mats;
        }

        private void Spawn()
        {
            var go = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
            _currentObject = go.GetComponent<ConstructionPart>();
            _currentObject.TargetPart = _part;
        }

        public void Interact(ARobot robot)
        {
            if (robot.TryCarry(_currentObject))
            {
                Spawn();
            }
        }
    }
}
