using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        private string m_ModelName;
        private string m_LicensePlate;
        private float m_EnergyLeft;
        private List<Wheel> m_WheelsList;
        private Engine m_Engine;
        protected const string k_FueledEngineType = "Fuel";
        protected const string k_ElecronicEngineType = "Electronic";

        public string ModelName
        {
            set
            {
                m_ModelName = value;
            }
        }

        public string LicensePlate
        {
            get
            {
                return m_LicensePlate;
            }
            set
            {
                m_LicensePlate = value;
            }
        }

        public float CurnetEnergy
        {
            set
            {
                m_Engine.CurnetEnergy = value;
            }
        }

        public Engine VehicleEngine
        {
            get
            {
                return m_Engine;
            }
        }

        public Vehicle(int i_NumberOfWheels, float i_MaxAirPressure)
        {
            m_WheelsList = new List<Wheel>(i_NumberOfWheels);
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_WheelsList.Add(new Wheel(i_MaxAirPressure));
            }
        }

        protected void CreateEngine(string i_EngineType, float i_MaxEnergy, eFuelType i_FuelType = eFuelType.Octan95)
        {
            if (i_EngineType == k_FueledEngineType)
            {
                m_Engine = new FueledVehicle(i_FuelType, i_MaxEnergy);
            }
            else if (i_EngineType == k_ElecronicEngineType)
            {
                m_Engine = new ElectronicVehicle(i_MaxEnergy);
            }
        }

        public float GetMaxAirPressure()
        {
            if (m_WheelsList.Count == 0)
            {
                throw new AggregateException("The wheel is empty");
            }

            return m_WheelsList[0].MaxAirPressure;
        }

        public int NumberOfWheels()
        {
            return m_WheelsList.Count;
        }

        public void UpdateEnergyLeft()
        {
            m_EnergyLeft = (m_Engine.CurnetEnergy / m_Engine.MaxEnergy) * 100;
        }

        public void FillAirPressueByIndex(float i_AirPressureAmount, int i_Index, string i_Manufacturerel) 
        {
            if(i_Index < 0 || i_Index >= m_WheelsList.Count)
            {
                throw new ValueOutOfRangeException(0, m_WheelsList.Count);
            }
            m_WheelsList[i_Index].Manufacturerel = i_Manufacturerel;
            m_WheelsList[i_Index].AddPressure(i_AirPressureAmount);
        }

        public void FillToMaxAirPressure()
        {
            foreach(Wheel wheel in m_WheelsList)
            {
                wheel.FillToMax();
            }
        }

        public float GetMaxEnergy()
        {
            return m_Engine.MaxEnergy;
        }

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();
            int index = 1;

            vehicleDetails.Append("Model Name: ");
            vehicleDetails.AppendLine(m_ModelName);
            foreach(Wheel wheel in m_WheelsList)
            {
                vehicleDetails.Append("Wheel #");
                vehicleDetails.AppendLine(index.ToString());
                vehicleDetails.AppendLine(wheel.ToString());
                index++;
            }

            vehicleDetails.Append("Energy Left: ");
            vehicleDetails.AppendLine(m_EnergyLeft.ToString() + "%");

            return vehicleDetails.ToString();
        }
    }
}
