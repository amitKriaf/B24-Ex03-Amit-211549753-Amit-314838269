using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageVehicle
    {
        private string m_OwnerName;
        private string m_PhoneNumber;
        private eStatus m_Status;
        private Vehicle m_Vehicle;

        public eStatus Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }
        }

        public Vehicle GetVehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public GarageVehicle(string i_OwnerName, string i_PhoneNumber, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_PhoneNumber = i_PhoneNumber;
            m_Status = eStatus.InProcess;
            m_Vehicle = i_Vehicle;
        }

        public override string ToString()
        {
            StringBuilder garageVehicleDeatils = new StringBuilder();

            garageVehicleDeatils.Append("License plate: ");
            garageVehicleDeatils.AppendLine(m_Vehicle.LicensePlate);
            garageVehicleDeatils.Append("Owner: ");
            garageVehicleDeatils.AppendLine(m_OwnerName);
            garageVehicleDeatils.Append("Phone Number: ");
            garageVehicleDeatils.AppendLine(m_PhoneNumber);
            garageVehicleDeatils.Append(m_Vehicle.ToString());

            return garageVehicleDeatils.ToString();
        }
    }
}
