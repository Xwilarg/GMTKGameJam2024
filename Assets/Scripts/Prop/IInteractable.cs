using Gmtk.Robot;

namespace Gmtk.Prop
{
    public interface IInteractable
    {
        public void Interact(ARobot robot);
        public bool CanInteract { get; }

        public int ID { get; }
    }
}
