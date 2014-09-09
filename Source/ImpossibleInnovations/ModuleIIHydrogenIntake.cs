using UnityEngine;

namespace ImpossibleInnovations
{
    public class ModuleIIHydrogenIntake : PartModule
    {
        [KSPField(guiActive = true, guiActiveEditor = false, guiName = "State", isPersistant = false)]
        public string intakeActive = "Active";

        public void filterOn()
        {
            intakeActive = "Active";
            Events["toggleFilter"].guiName = "Turn Collector Off";
        }

        public void filterOff()
        {
            intakeActive = "Inactive";
            Events["toggleFilter"].guiName = "Turn Collector On";
        }

        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Turn Collector Off")]
        public void toggleFilter()
        {
            if (intakeActive == "Active")
            {
                filterOff();
            }
            else
            {
                filterOn();              
            }
        }        

        [KSPAction("Toggle Collector")]
        public void actionToggleCollector(KSPActionParam param)
        {
            toggleFilter();
        }

        [KSPAction("Turn Collector On")]
        public void actionTurnCollectorOn(KSPActionParam param)
        {
            filterOn();
        }

        [KSPAction("Turn Collector Off")]
        public void actionTurnCollectorOff(KSPActionParam param)
        {
            filterOff();
        }    

        public void FixedUpdate()
        {
            if (intakeActive == "Active")
            {
                part.RequestResource("Hydrogen", ((this.part.vessel.atmDensity * -1.4f) - 0.01f) * TimeWarp.fixedDeltaTime);
            }
        }
    }
}
