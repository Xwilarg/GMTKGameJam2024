using Gmtk.Manager;
using Gmtk.Robot.AI;
using UnityEngine;

namespace Gmtk.Map
{
    public class RobotEndArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (GameManager.Instance.DidRoundEnd && other.TryGetComponent<AIController>(out var robot))
            {
                AIManager.Instance.ReduceRobotCount();
                Destroy(robot.gameObject);
            }
        }
    }
}
