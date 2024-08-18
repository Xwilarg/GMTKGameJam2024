using Gmtk.Map;
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
        private Transform _buildArea;
        public Transform BuildArea => _buildArea;

        private Dictionary<TargetColor, List<Dispencer>> _colorTargets = new();

        private void Awake()
        {
            Instance = this;
            foreach (var color in Enum.GetValues(typeof(TargetColor)).Cast<TargetColor>())
            {
                _colorTargets.Add(color, new());
            }
        }

        public void Register(TargetColor targetColor, Dispencer t)
        {
            _colorTargets[targetColor].Add(t);
        }

        public Dispencer GetClosest(TargetColor color, Vector3 me)
            => _colorTargets[color].OrderBy(x => Vector3.Distance(x.transform.position, me)).First();
    }
}
