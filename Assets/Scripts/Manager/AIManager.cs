using Gmtk.Map;
using Gmtk.Robot.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gmtk.Manager
{
    public class AIManager : MonoBehaviour
    {
        public static AIManager Instance { private set; get; }


        [SerializeField]
        private Transform _gameEndPos;
        public Vector3 GameEndPos => _gameEndPos.position;

        private Dictionary<TargetColor, List<Dispenser>> _colorTargets = new();

        private List<AIController> _ais = new();

        private int RobotCount;

        private void Awake()
        {
            Instance = this;
            foreach (var color in Enum.GetValues(typeof(TargetColor)).Cast<TargetColor>())
            {
                _colorTargets.Add(color, new());
            }
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

        public void EndRound()
        {
            RobotCount = BuiltAiCount;
            foreach (var ai in _ais)
            {
                ai.SetTarget();
            }
            _ais.Clear();
        }

        public void Register(TargetColor targetColor, Dispenser t)
        {
            _colorTargets[targetColor].Add(t);
        }

        public Dispenser GetClosest(TargetColor color, Vector3 me)
            => _colorTargets[color].OrderBy(x => Vector3.Distance(x.transform.position, me)).First();
    }
}
