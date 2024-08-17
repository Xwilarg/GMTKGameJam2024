using Gmtk.SO;

namespace Gmtk.Robot.AI
{
    public class AIController : ARobot
    {
        public bool IsBeingConstructed { private get; set; } = true;

        public CPUInfo CPU { set; get; }
    }
}
