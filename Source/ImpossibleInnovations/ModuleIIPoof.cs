using UnityEngine;

namespace ImpossibleInnovations
{
    public class ModuleIIPoof : PartModule
    {
        [KSPField(guiActive = true, guiActiveEditor = false, guiName = "Saftey", isPersistant = true)]
        public string poofSaftey = "On";

        public void poofSafteyOn()
        {
            poofSaftey = "On";
            Events["toggleSaftey"].guiName = "Turn Saftey Off";
        }

        public void poofSafteyOff()
        {
            poofSaftey = "Off";
            Events["toggleSaftey"].guiName = "Turn Saftey On";
        }

        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Turn Saftey Off")]
        public void toggleSaftey()
        {
            if (poofSaftey == "On")
            {
                poofSafteyOff();
            }
            else
            {
                poofSafteyOn();
            }
        }

        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Poof!")]
        public void poof()
        {
            if (poofSaftey == "Off") this.part.explode();
        }

        [KSPAction("Toggle Saftey")]
        public void actionToggleSaftey(KSPActionParam param)
        {
            toggleSaftey();
        }

        [KSPAction("Saftey On")]
        public void actionSafteyOn(KSPActionParam param)
        {
            poofSafteyOn();
        }

        [KSPAction("Saftey Off")]
        public void actionSafteyOff(KSPActionParam param)
        {
            poofSafteyOff();
        }

        [KSPAction("Poof!")]
        public void actionPoof(KSPActionParam param)
        {
            poof();
        }
    }
}
