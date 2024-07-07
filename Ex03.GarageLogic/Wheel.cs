using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;
        private const float K_MinAirPressure = 0;

        public Wheel(float i_MaxAirPressure)
        {
            m_CurrentAirPressure = 0;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public void AddPressure(float i_AddAirPressure)
        {
            if (i_AddAirPressure + m_CurrentAirPressure > m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(K_MinAirPressure, m_MaxAirPressure - m_CurrentAirPressure);
            }
            m_CurrentAirPressure += i_AddAirPressure;
        }

        public float CuurentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        public string Manufacturerel
        {
            set
            {
                m_Manufacturer = value;
            }
        }

        public void FillToMax()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }

        public override string ToString()
        {
            StringBuilder wheelDeatils = new StringBuilder();

            wheelDeatils.Append("Manufacturer: ");
            wheelDeatils.Append(m_Manufacturer);
            wheelDeatils.Append(" Current Pressure: ");
            wheelDeatils.Append(m_CurrentAirPressure);

            return wheelDeatils.ToString();
        }
    }
}
