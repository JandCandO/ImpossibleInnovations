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
            this.part.force_activate();
            Events["toggleFilter"].guiName = "Turn Collector Off";
        }

        public void filterOff()
        {
            intakeActive = "Inactive";
            this.part.deactivate();
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

        public override void OnInitialize()
        {
            if (intakeActive == "Active")
            {
                this.part.force_activate();
            }
           
            base.OnInitialize();
        }

        public override void OnFixedUpdate()
        {
            this.part.RequestResource("Hydrogen", ((this.part.vessel.atmDensity * -1.4f) - 0.01f) * TimeWarp.fixedDeltaTime);

            base.OnFixedUpdate();
        }
    }
}
