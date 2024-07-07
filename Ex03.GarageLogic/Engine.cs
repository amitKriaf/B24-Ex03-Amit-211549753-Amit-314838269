using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Engine
    {
        private float m_MaxEnergy;
        private float m_CurrnetEnergy;

        public Engine(float i_MaxEnergy)
        {
            m_MaxEnergy = i_MaxEnergy;
            m_CurrnetEnergy = 0;
        }

        public float MaxEnergy
        {
            get
            { 
            return m_MaxEnergy;
            }
            set
            {
                m_MaxEnergy = value;
            }
        }

        public float CurnetEnergy
        {
            get
            {
                return m_CurrnetEnergy;
            }
            set
            {
                if (value <= m_MaxEnergy && value >= 0)
                {
                    m_CurrnetEnergy = value;
                }
                else
                {
                    throw new ArgumentException("invalid amount of energy");
                }
            }
        }

        public override string ToString()
        {
            StringBuilder fuelVehicleDetails = new StringBuilder();

            fuelVehicleDetails.Append("Max Energy: ");
            fuelVehicleDetails.AppendLine(m_MaxEnergy.ToString());
            fuelVehicleDetails.Append("Current Energy: ");
            fuelVehicleDetails.AppendLine(m_MaxEnergy.ToString());

            return fuelVehicleDetails.ToString();
        }
    }
}
