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

        }

        protected override void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }
}
