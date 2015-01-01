using UnityEngine;

namespace ImpossibleInnovations
{
    public class ModuleIIHydrogenConverter : PartModule
    {
        [KSPField(guiActive = true, guiActiveEditor = false, guiName = "Electric Consumption", isPersistant = false)]
        public double converterElectricConsumption;

        [KSPField(guiActive = true, guiActiveEditor = false, guiName = "Current Mode", isPersistant = true)]
        public string mode = "Idle";       

        #region Setting Guis
        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Idle")]
        private void setToIdle()
        {
            mode = "Idle";
            converterElectricConsumption = 0;
        }

        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Hydrogen -> Deuterium")]
        private void setToDeuterium()
        {
            mode = "Hydrogen -> Deuterium";
            converterElectricConsumption = 20;
        }

        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Hydrogen -> Tritium")]
        private void setToTritium()
        {
            mode = "Hydrogen -> Tritium";
            converterElectricConsumption = 40;
        }

        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Deuterium -> Hydrogen")]
        private void setToHydrogenFromDeuterium()
        {
            mode = "Deuterium -> Hydrogen";
            converterElectricConsumption = -15;
        }

        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Tritium -> Hydrogen")]
        private void setToHydrogenFromTritium()
        {
            mode = "Tritium -> Hydrogen";
            converterElectricConsumption = -30;
        }
        #endregion

        #region Action Groups
        [KSPAction("Idle")]
        public void actionSetToIdle(KSPActionParam param)
        {
            setToIdle();
        }

         [KSPAction("Hyrdrogen -> Deuterium")]
        public void actionSetToDeuterium(KSPActionParam param)
        {
            setToDeuterium();
        }

         [KSPAction("Hydrogen -> Tritium")]
        public void actionSetToTritium(KSPActionParam param)
        {
            setToTritium();
        }

         [KSPAction("Deuterium -> Hydrogen")]
        public void actionSetToHydrogenFromDeuterium(KSPActionParam param)
        {
            setToHydrogenFromDeuterium();
        }

         [KSPAction("Tritium -> Hydrogen")]
         public void actionSetToHydrogenFromTritium(KSPActionParam param)
        {
            setToHydrogenFromTritium();
        }
        #endregion

        public void FixedUpdate()
        {
            if (mode == "Hydrogen -> Deuterium" && II_Utilities.GetShipResourceAmount(vessel, "Hydrogen") > 0 && II_Utilities.GetShipResourceAmount(vessel, "Deuterium") != II_Utilities.GetShipResourceMaxAmount(vessel, "Deuterium"))
            {
                part.RequestResource("Hydrogen", TimeWarp.fixedDeltaTime * 2);
                part.RequestResource("Deuterium", TimeWarp.fixedDeltaTime * -2);
                part.RequestResource("ElectricCharge", TimeWarp.fixedDeltaTime * converterElectricConsumption);
            }

            if (mode == "Hydrogen -> Tritium" && II_Utilities.GetShipResourceAmount(vessel, "Hydrogen") > 0 && II_Utilities.GetShipResourceAmount(vessel, "Tritium") != II_Utilities.GetShipResourceMaxAmount(vessel, "Tritium"))
            {
                part.RequestResource("Hydrogen", TimeWarp.fixedDeltaTime * 2);
                part.RequestResource("Tritium", TimeWarp.fixedDeltaTime * -2);
                part.RequestResource("ElectricCharge", TimeWarp.fixedDeltaTime * converterElectricConsumption);
            }

            if (mode == "Deuterium -> Hydrogen" && II_Utilities.GetShipResourceAmount(vessel, "Deuterium") > 0 && II_Utilities.GetShipResourceAmount(vessel, "Hydrogen") != II_Utilities.GetShipResourceMaxAmount(vessel, "Hydrogen") && II_Utilities.GetShipResourceAmount(vessel, "ElectricCharge") != II_Utilities.GetShipResourceMaxAmount(vessel, "ElectricCharge"))
            {
                part.RequestResource("Deuterium", TimeWarp.fixedDeltaTime * 3);
                part.RequestResource("Hydrogen", TimeWarp.fixedDeltaTime * -2);
                part.RequestResource("ElectricCharge", TimeWarp.fixedDeltaTime * converterElectricConsumption);
            }

            if (mode == "Tritium -> Hydrogen" && II_Utilities.GetShipResourceAmount(vessel, "Tritium") > 0 && II_Utilities.GetShipResourceAmount(vessel, "Hydrogen") != II_Utilities.GetShipResourceMaxAmount(vessel, "Hydrogen") && II_Utilities.GetShipResourceAmount(vessel, "ElectricCharge") != II_Utilities.GetShipResourceMaxAmount(vessel, "ElectricCharge"))
            {
                part.RequestResource("Tritium", TimeWarp.fixedDeltaTime * 3);
                part.RequestResource("Hydrogen", TimeWarp.fixedDeltaTime * -2);
                part.RequestResource("ElectricCharge", TimeWarp.fixedDeltaTime * converterElectricConsumption);
            }           
        }
    }
}
