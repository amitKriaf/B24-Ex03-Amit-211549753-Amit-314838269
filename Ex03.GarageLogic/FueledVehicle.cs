using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{   
    public class FueledVehicle : Engine
    {
        private eFuelType m_FuelType;

        public FueledVehicle(eFuelType i_FuelType, float i_MaxFuel) :base(i_MaxFuel)
        {
            m_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        public void AddFuel(float i_AddFuel, eFuelType i_FuelType)
        {
            if (i_FuelType != m_FuelType)
            {
                throw new ArgumentException("The fuel type is not suitable!");
            }
            if (i_AddFuel + base.CurnetEnergy > base.MaxEnergy)
            {
                throw new ValueOutOfRangeException(0, base.MaxEnergy - base.CurnetEnergy);
            }
            base.CurnetEnergy += i_AddFuel;
        }

        public override string ToString()
        {
            StringBuilder fuelVehicleDetails = new StringBuilder();

            fuelVehicleDetails.Append("Fuel Type: ");
            fuelVehicleDetails.AppendLine(m_FuelType.ToString());
            fuelVehicleDetails.Append(base.ToString());

            return fuelVehicleDetails.ToString();
        }
    }
}
