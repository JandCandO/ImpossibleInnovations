using UnityEngine;

namespace ImpossibleInnovations
{
    public class ModuleIIIonWing : PartModule
    {
        const float wingElectricDrain = 5f; //(per second)

        //I borrowed this code from Karbonite, and made some changes
        private double GetShipResourceAmount(string resName)
        {
            var amount = 0;
            if (vessel != null)
            {
                foreach (var p in vessel.parts)
                {
                    if (p.Resources.Contains(resName))
                    {
                        var res = p.Resources[resName];
                        amount += (int)res.amount;
                    }
                }
            }
            return amount;
        }

        public override void OnInitialize()
        {
            this.part.force_activate();

            base.OnInitialize();
        }

        public override void OnFixedUpdate()
        {
            if (this.part.vessel.atmDensity != 0 && GetShipResourceAmount("ElectricCharge") != 0)
            {
                this.part.RequestResource("ElectricCharge", TimeWarp.fixedDeltaTime * wingElectricDrain);
            }
            else
            {
                this.part.explode();
            }

            base.OnFixedUpdate();
        }
    }
}
