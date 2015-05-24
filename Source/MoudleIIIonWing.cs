using UnityEngine;

namespace ImpossibleInnovations
{
    public class ModuleIIIonWing : PartModule
    {
        [KSPField(guiActive = true, guiActiveEditor = false, guiName = "Electric Consumption", isPersistant = false)]
        public double wingElectricConsumption;

        [KSPField(guiActive = true, guiActiveEditor = false, guiName = "deflectionLiftCoeff", isPersistant = false)]
        public float deflectionLiftCoeff;

        public void FixedUpdate()
        {
            if (part.Modules.Contains("ModuleLiftingSurface"))
            {
                wingElectricConsumption = (vessel.srfSpeed * vessel.atmDensity) / 15; //electricCharge drain dependent on airspeed and atmospheric density. if either is 0, no electricCharge is drained
                
                if (vessel.srfSpeed < 30)
                {
                    wingElectricConsumption = 0;
                }

                if (II_Utilities.GetShipResourceAmount(vessel, "ElectricCharge") >= wingElectricConsumption)
                {
                    part.RequestResource("ElectricCharge", TimeWarp.fixedDeltaTime * wingElectricConsumption);

                    deflectionLiftCoeff = 15f;
                    ((ModuleLiftingSurface)part.Modules["ModuleLiftingSurface"]).deflectionLiftCoeff = deflectionLiftCoeff;
                }
                else
                {
                    wingElectricConsumption = 0;

                    deflectionLiftCoeff = 1.75f;
                    ((ModuleLiftingSurface)part.Modules["ModuleLiftingSurface"]).deflectionLiftCoeff = deflectionLiftCoeff;
                }
            }
        }
    }
}
