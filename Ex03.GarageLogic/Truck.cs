using System.Text;


namespace Ex03.GarageLogic
{
    public sealed class Truck : Vehicle
    {
        private bool m_IsTransportsDangerousMaterials;
        private float m_CargoVolume;
        private const int k_NumOfWheels = 12;
        private const float k_MaxAirPressure = 28f;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_FullTank = 120;

        public bool IsTransportsDangerousMaterials
        {
            set
            {
                m_IsTransportsDangerousMaterials = value;
            }
        }

        public float CargoVolume
        {
            set
            {
                m_CargoVolume = value;
            }
        }

        public Truck() : base(k_NumOfWheels, k_MaxAirPressure) 
        { 
            CreateEngine(k_FueledEngineType, k_FullTank, k_FuelType);
        }

        public override string ToString()
        {
            StringBuilder truckDeatils = new StringBuilder();

            truckDeatils.Append("Is Trasport Dangerous Materials: ");
            truckDeatils.AppendLine(m_IsTransportsDangerousMaterials.ToString());
            truckDeatils.Append("Cargo Volume:");
            truckDeatils.AppendLine(m_CargoVolume.ToString());
            truckDeatils.Append(base.ToString());

            return truckDeatils.ToString();

        }
    }
}
