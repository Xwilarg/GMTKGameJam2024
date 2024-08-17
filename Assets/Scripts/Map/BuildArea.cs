using Gmtk.Robot;
using Gmtk.Robot.AI;
using Gmtk.SO;
using UnityEngine;

namespace Gmtk.Map
{
    public class BuildArea : MonoBehaviour
    {
        [SerializeField]
        private GameObject _aiPrefab;

        private AIController _constructing;

        private void Awake()
        {
            Spawn();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ARobot>(out var robot) && robot.Carrying != null)
            {
                var part = robot.Carrying.TargetPart;

                if (part is HandInfo handInfo && _constructing.Hands == null) _constructing.AddPart(handInfo);
                else if (part is WheelInfo wheelInfo && _constructing.Wheels == null) _constructing.AddPart(wheelInfo);
                else if (part is CPUInfo cpuInfo && _constructing.CPU == null) _constructing.AddPart(cpuInfo);

                CheckConstructionStatus();

                Destroy(robot.Carrying.gameObject);
                robot.Carrying = null;
            }
        }

        private void CheckConstructionStatus()
        {
            if (_constructing.Hands != null && _constructing.Wheels != null && _constructing.CPU != null)
            {
                _constructing.IsBeingConstructed = false;
                Spawn();
            }
        }

        private void Spawn()
        {
            var go = Instantiate(_aiPrefab, transform.position, Quaternion.identity);
            _constructing = go.GetComponent<AIController>();
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            GUI.Label(new Rect(5, 5, 100, 100), $"CPU: {_constructing.CPU?.name ?? "None"}\nWheels: {_constructing.Wheels?.name ?? "None"}\nHands: {_constructing.Hands?.name ?? "None"}");
        }
#endif
    }
}
