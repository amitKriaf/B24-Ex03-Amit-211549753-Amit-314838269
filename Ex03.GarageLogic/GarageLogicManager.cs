using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GarageLogicManager
    {

        private Dictionary<string, GarageVehicle> m_Vehicles;

        public GarageLogicManager()
        {
            m_Vehicles = new Dictionary<string, GarageVehicle>();
        }

        public Vehicle CreateVehicle(int i_VehicleType)
        {
            Vehicle vehicle = Factory.CreateVehicle(i_VehicleType);

            return vehicle;
        }
        
        private void checkIfInGarage(string i_LicensePlate)
        {
            if (!m_Vehicles.ContainsKey(i_LicensePlate))
            {
                throw new ArgumentException($"License Plate {i_LicensePlate} is not in the garage");
            }
        }
        
        public bool CheakIfAlreadyExist(string i_LicensePlate)
        {
            return m_Vehicles.ContainsKey(i_LicensePlate);
        }

        public void AddToGarage(string i_LicensePlate, string i_OwnerName, string i_PhoneNumber, Vehicle i_Vehicle)
        {
            m_Vehicles.Add(i_LicensePlate, new GarageVehicle(i_OwnerName, i_PhoneNumber, i_Vehicle));
        }

        public List<string> GetGarageLicensePlates(string i_FilterStatus)
        {
            List<string> licensePlatesList = new List<string>();

            if (!Enum.TryParse(i_FilterStatus, true, out eStatus status) &&  i_FilterStatus != "All")
            {
                throw new ArgumentException("Invalid status");
            }

            foreach(string licensePlate in m_Vehicles.Keys)
            {
                if (i_FilterStatus == "All")
                {
                    licensePlatesList.Add(licensePlate);
                }
                else if (status == m_Vehicles[licensePlate].Status)
                {
                    licensePlatesList.Add(licensePlate);
                }
            }

            return licensePlatesList;
        }

        public void ChangeStatus(string i_LicensePlate, string i_NewStatus)
        {
            checkIfInGarage(i_LicensePlate);
            if (!Enum.TryParse(i_NewStatus, true, out eStatus status))
            {
                throw new ArgumentException("Invalid status");
            }

            m_Vehicles[i_LicensePlate].Status = status;
        }

        public void FillToMaximumAllWheels(string i_LicensePlate)
        {
            checkIfInGarage(i_LicensePlate);
            m_Vehicles[i_LicensePlate].GetVehicle.FillToMaxAirPressure();
        }

        public void FillFuel(string i_LicensePlate, string i_Fueltype, float i_AmountToFill)
        {
            checkIfInGarage(i_LicensePlate);
            if (!Enum.TryParse(i_Fueltype, true, out eFuelType fuelType))
            {
                throw new ArgumentException("Invalid fuel type");
            }
            
            if(m_Vehicles[i_LicensePlate].GetVehicle.VehicleEngine is FueledVehicle fueledVehicle)
            {
                fueledVehicle.AddFuel(i_AmountToFill, fuelType);
            }
            else
            {
                throw new ArgumentException("This is not a fueled vehicle");
            }
            m_Vehicles[i_LicensePlate].GetVehicle.UpdateEnergyLeft();
        }

        public void FillElecric(string i_LicensePlate, float i_AmountToFill)
        {
            checkIfInGarage(i_LicensePlate);
            if (m_Vehicles[i_LicensePlate].GetVehicle.VehicleEngine is ElectronicVehicle electronicVehicle)
            {
                electronicVehicle.AddElectrition(i_AmountToFill);
            }
            else
            {
                throw new ArgumentException("This is not an electronic vehicle");
            }
            m_Vehicles[i_LicensePlate].GetVehicle.UpdateEnergyLeft();
        }

        public string getVehicleDetails(string i_LicensePlate)
        {
            checkIfInGarage(i_LicensePlate);
            string details = m_Vehicles[i_LicensePlate].ToString();

            return details;
        }

        public void RestartStatus(string i_LicensePlate)
        {
            ChangeStatus(i_LicensePlate, eStatus.InProcess.ToString());
        }

        public bool IsVehicleTypeOk(int i_VehicleType)
        {
            return i_VehicleType >= 1 && i_VehicleType <= Enum.GetValues(typeof(eVehicleTypes)).Length ;
        }

        public Vehicle GetVehicleByLicensePlate(string i_LicesnePlate)
        {
            checkIfInGarage(i_LicesnePlate);
            return m_Vehicles[i_LicesnePlate].GetVehicle;
        }

        public void CheckIfElectric(string i_LicensePlate)
        {
            checkIfInGarage(i_LicensePlate);
            if (!(m_Vehicles[i_LicensePlate].GetVehicle is ElectronicVehicle))
            {
                throw new ArgumentException("This is not an eletric vehicle!");
            }
        }

        public void CheckIfFueled(string i_LicensePlate)
        {
            checkIfInGarage(i_LicensePlate);
            if (!(m_Vehicles[i_LicensePlate].GetVehicle.VehicleEngine is FueledVehicle))
            {
                throw new ArgumentException("This is not an fueled vehicle!");
            }
        }

        public void CheckIfFuelTypeOk(string i_LicensePlate, string i_FuelType)
        {
            checkIfInGarage(i_LicensePlate);
            if (m_Vehicles[i_LicensePlate].GetVehicle.VehicleEngine is FueledVehicle fueledVehicle)
            {
                if (fueledVehicle.FuelType.ToString() != i_FuelType)
                {
                    throw new ArgumentException("The fuel type is not suitable!");
                }
            }
            else
            {
                throw new ArgumentException("This is not a fuled vehicle!");
            }
        }

        public float CheckIfEnergyInsertedIsOK(string i_LicensePlate, string i_UserInput)
        {
            Vehicle vehicle;
            float energy;

            checkIfInGarage(i_LicensePlate);
            vehicle = m_Vehicles[i_LicensePlate].GetVehicle;
            if (!float.TryParse(i_UserInput, out energy))
            {
                throw new FormatException("Invalid input, you need to enter a float number, try again");
            }
            if (energy < 0f ||energy > vehicle.GetMaxEnergy())
            {
                throw new ArgumentException("amount is incorrect");
            }

            return energy;
        }

        public void UpdateEnergyLeft(string i_LicensePlate)
        {
            checkIfInGarage(i_LicensePlate);
            m_Vehicles[i_LicensePlate].GetVehicle.UpdateEnergyLeft();
        }
    }


}
