using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenseTypes m_LicenseType;
        private int m_EngineVolume;
        private const int k_NumOfWheels = 2;
        private const float k_MaxAirPressure = 33f;
        private const float k_MaxTime = 2.5f;
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private const float k_FullTank = 5.5f;

        public eLicenseTypes LicenseType
        {
            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineVolume
        {
            set
            {
                m_EngineVolume = value;
            }
        }

        public Motorcycle(string i_EngineType) : base(k_NumOfWheels, k_MaxAirPressure)
        {
            if (i_EngineType == k_FueledEngineType)
            {
                CreateEngine(i_EngineType, k_FullTank, k_FuelType);
            }
            else if (i_EngineType == k_ElecronicEngineType)
            {
                CreateEngine(i_EngineType, k_MaxTime);
            }
        }

        public override string ToString()
        {
            StringBuilder elecricMotorcycleDetails = new StringBuilder();

            elecricMotorcycleDetails.Append("License Type: ");
            elecricMotorcycleDetails.AppendLine(m_LicenseType.ToString());
            elecricMotorcycleDetails.Append(" Engine Volume: ");
            elecricMotorcycleDetails.AppendLine(m_EngineVolume.ToString());
            elecricMotorcycleDetails.Append(base.ToString());

            return elecricMotorcycleDetails.ToString();
        }
    }
}
