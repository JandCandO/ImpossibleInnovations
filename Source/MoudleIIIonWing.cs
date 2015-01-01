using UnityEngine;

namespace ImpossibleInnovations
{
    public class ModuleIIIonWing : PartModule
    {
        const float wingElectricDrain = 5f; //(per second)

        public void FixedUpdate()
        {
            if (II_Utilities.GetShipResourceAmount(vessel,"ElectricCharge") != 0)
            {
                part.RequestResource("ElectricCharge", TimeWarp.fixedDeltaTime * wingElectricDrain);
            }
            else
            {
                part.explode();
            }
        }
    }
}
