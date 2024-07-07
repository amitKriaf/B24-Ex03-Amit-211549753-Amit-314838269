using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Factory
    {
        protected const string k_FueledEngineType = "Fuel";
        protected const string k_ElecronicEngineType = "Electronic";

        public static Vehicle CreateVehicle(int i_VehicleType)
        {
            Vehicle vehicle;

            if (!Enum.TryParse(i_VehicleType.ToString(), true, out eVehicleTypes vehicleType))
            {
                throw new ArgumentException("Invalid Vehicle type");
            }

            switch (vehicleType)
            {
                case eVehicleTypes.FuelCar:
                    vehicle = new Car(k_FueledEngineType);
                    break;
                case eVehicleTypes.ElectricCar:
                    vehicle = new Car(k_ElecronicEngineType);
                    break;
                case eVehicleTypes.FuelMotorcycle:
                    vehicle = new Motorcycle(k_FueledEngineType);
                    break;
                case eVehicleTypes.ElectricMotorcycle:
                    vehicle = new Motorcycle(k_ElecronicEngineType);
                    break;
                case eVehicleTypes.Truck:
                    vehicle = new Truck();
                    break;
                default:
                    throw new ArgumentException($"There is no such type like {i_VehicleType.ToString()} in the garage");
            }

            return vehicle;
        }
    }
}
