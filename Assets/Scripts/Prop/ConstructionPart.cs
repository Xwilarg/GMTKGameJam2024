﻿using Gmtk.Robot;
using Gmtk.SO.Part;
using UnityEngine;

namespace Gmtk.Prop
{
    public class ConstructionPart : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private APartInfo _targetPart;

        public APartInfo TargetPart => _targetPart;

        public bool CanInteract { set; get; } = true;

        public int ID => gameObject.GetInstanceID();

        public void Interact(ARobot robot)
        {
            robot.TryCarry(this);
        }
    }
}
