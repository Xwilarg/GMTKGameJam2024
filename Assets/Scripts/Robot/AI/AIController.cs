using Gmtk.Manager;
using Gmtk.SO;
using UnityEngine.AI;

namespace Gmtk.Robot.AI
{
    public class AIController : ARobot
    {
        private bool _isBeingConstructed = true;
        public bool IsBeingConstructed
        {
            private get => _isBeingConstructed;
            set
            {
                _isBeingConstructed = value;

                if (!_isBeingConstructed)
                {
                    SetTarget();
                }
            }
        }

        public CPUInfo AiCPU { set; get; }

        private NavMeshAgent _agent;

        private void SetTarget()
        {
            if (Carrying)
            {
                _agent.SetDestination(AIManager.Instance.BuildArea.position);
            }
            else
            {
                _agent.SetDestination(AIManager.Instance.GetClosest(AiCPU.AI switch
                {
                    AIBehavior.GrabRedStation => TargetColor.Red,
                    AIBehavior.GrabGreenStation => TargetColor.Green,
                    AIBehavior.GrabBlueStation => TargetColor.Blue,
                    _ => throw new System.NotImplementedException()
                }, transform.position).position);
            }
        }

        protected override void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }
}
