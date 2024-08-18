using Gmtk.Manager;
using Gmtk.Prop;
using Gmtk.Robot;
using Gmtk.SO.Part;
using UnityEngine;

namespace Gmtk.Map
{
    public class Dispenser : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private ConstructionPart _prefab;

        [SerializeField]
        private Transform _spawnPoint;
        public Transform SpawnPoint => _spawnPoint;

        [SerializeField]
        private TargetColor _target;

        private APartInfo _part;

        public bool CanInteract => true;

        public int ID => gameObject.GetInstanceID();

        private ConstructionPart _currentObject;

        public void SetPart(APartInfo part)
        {
            _part = part;
            if (_currentObject != null)
            {
                Destroy(_currentObject.gameObject);
            }
            Spawn();
        }

        private void Start()
        {
            var mr = GetComponent<MeshRenderer>();
            var mats = mr.materials;
            mats[1] = ResourcesManager.Instance.GetMat(_target);
            mr.materials = mats;

            AIManager.Instance.Register(_target, this);
        }

        private void Spawn()
        {
            var go = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
            _currentObject = go.GetComponent<ConstructionPart>();
            _currentObject.TargetPart = _part;
        }

        public void Interact(ARobot robot)
        {
            if (_currentObject != null && robot.TryCarry(_currentObject))
            {
                Spawn();
            }
        }
    }
}
