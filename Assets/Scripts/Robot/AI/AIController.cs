using Gmtk.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Gmtk.Robot.AI
{
    public class AIController : ARobot
    {
        [SerializeField]
        private TMP_Text _debugText;

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

        private NavMeshAgent _agent;

        protected override void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_isBeingConstructed) return;

#if UNITY_EDITOR
            _debugText.text = $"{_agent.pathStatus}";
#endif
            if (_agent.pathStatus == NavMeshPathStatus.PathComplete && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                SetTarget();
#if UNITY_EDITOR
                _debugText.text = "Changing target";
#endif
            }
#if UNITY_EDITOR
            _debugText.text += $"Interaction: {(_interactionTarget?.CanInteract)?.ToString() ?? "null"}";
#endif
        }

        private void SetTarget()
        {
            if (_interactionTarget != null && _interactionTarget.CanInteract)
            {
                _interactionTarget.Interact(this);
            }

            if (Carrying)
            {
                _agent.SetDestination(AIManager.Instance.BuildArea.position);
            }
            else
            {
                _agent.SetDestination(AIManager.Instance.GetClosest(CPU.AI switch
                {
                    AIBehavior.GrabRedStation => TargetColor.Red,
                    AIBehavior.GrabGreenStation => TargetColor.Green,
                    AIBehavior.GrabBlueStation => TargetColor.Blue,
                    _ => throw new System.NotImplementedException()
                }, transform.position).SpawnPoint.position);
            }
        }
    }
}
