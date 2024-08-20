using Gmtk.Map;
using Gmtk.Robot.AI;
using Gmtk.VN;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

namespace Gmtk.Manager
{
    public class AIManager : MonoBehaviour
    {
        public static AIManager Instance { private set; get; }


        [SerializeField]
        private Transform _gameEndPos;

        [SerializeField]
        private NavMeshSurface[] _surfaces;

        public int[] SurfaceIds => _surfaces.Select(x => x.agentTypeID).ToArray();

        public Vector3 GameEndPos => _gameEndPos.position;

        private Dictionary<TargetColor, List<Dispenser>> _colorTargets = new();

        private List<AIController> _ais = new();

        private int RobotCount;
        public bool DidWonObjective { private set; get; }

        private void Awake()
        {
            Instance = this;
            foreach (var color in Enum.GetValues(typeof(TargetColor)).Cast<TargetColor>())
            {
                _colorTargets.Add(color, new());
            }
        }

        public void Unregister(AIController ai)
        {
            _ais.Remove(ai);
        }
        public void Register(AIController ai)
        {
            _ais.Add(ai);
        }

        public void ReduceRobotCount()
        {
            RobotCount--;
            if (RobotCount == 0)
            {
                GameManager.Instance.StartNextRound();
            }
        }

        public int BuiltAiCount => _ais.Count(x => !x.IsBeingConstructed);
        public int BuiltCatCount => _ais.Count(x => !x.IsBeingConstructed && x.Hands.TargetJob == Job.Cat);
        public int BuiltBuilderCount => _ais.Count(x => !x.IsBeingConstructed && x.Hands.TargetJob == Job.Builder);

        private bool CheckObjective()
        {
            Debug.Log($"Objective requires {VNManager.Instance.Objective} of {string.Join(", ", VNManager.Instance.PossibleBots)}");

            return _ais.Count(x => !x.IsBeingConstructed && VNManager.Instance.PossibleBots.Select(y => VNManager.Instance.GetJob(y)).Contains(x.Hands.TargetJob)) >= VNManager.Instance.Objective;
        }

        public void EndRound()
        {
            RobotCount = BuiltAiCount;
            DidWonObjective = VNManager.Instance.Progress == TutorialProgress.Game ? CheckObjective() : true;
            foreach (var ai in _ais)
            {
                ai.SetTarget();
            }
            _ais.RemoveAll(x => !x.IsBeingConstructed);
        }

        public void Register(TargetColor targetColor, Dispenser t)
        {
            _colorTargets[targetColor].Add(t);
        }

        public Dispenser GetClosest(TargetColor color, Vector3 me)
            => _colorTargets[color].OrderBy(x => Vector3.Distance(x.transform.position, me)).First();
    }
}
