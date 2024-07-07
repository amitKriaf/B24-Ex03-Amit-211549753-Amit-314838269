using System.Text;


namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColors m_Color;
        private eDoors m_NumOfDoors;
        private const int k_NumOfWheels = 5;
        private const float k_MaxAirPressure = 31f;
        private const float k_MaxTime = 3.5f;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const float k_FullTank = 45;

        public eColors Color
        {
            set
            {
                m_Color = value;
            }
        }

        public eDoors NumOfDoors
        {
            set
            {
                m_NumOfDoors = value;
            }
        }

        public Car(string i_EngineType) : base(k_NumOfWheels, k_MaxAirPressure) 
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
            StringBuilder elecrticCarDetails = new StringBuilder();

            elecrticCarDetails.Append("Color: ");
            elecrticCarDetails.AppendLine(m_Color.ToString());
            elecrticCarDetails.Append("Number Of Doors: ");
            elecrticCarDetails.AppendLine(m_NumOfDoors.ToString());
            elecrticCarDetails.Append(base.ToString());

            return elecrticCarDetails.ToString();
        }
    }
}
