using System;
using System.Collections.Generic;
using System.Reflection;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private GarageLogicManager m_Garage;
        private const string k_BackToMenu = "#";

        private enum eUserChoises
        {
            EnterNewVehicle = 1,
            ViewCarsFilter,
            ChangeStatus,
            FillAirPressure,
            FillFuel,
            FillElectric,
            DisplayData,
            Exit
        }

        private int printMenuAndGetChoise()
        {
            string stringUserChoise;
            int intUserChoise;

            Console.WriteLine("**********************************************************");
            Console.WriteLine("1. Enter a new Vehicle in the garage.");
            Console.WriteLine("2. View cars in garage with / without filter.");
            Console.WriteLine("3. Change status of vehicle that is already in the garage.");
            Console.WriteLine("4. Fill air pressure into maximum vehicle wheels.");
            Console.WriteLine("5. Fill fuel into a regular vehicle.");
            Console.WriteLine("6. Fill battery into a electronic vehicle.");
            Console.WriteLine("7. Display data of a vehicle that is in the garage.");
            Console.WriteLine("8. Exit.");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("Please enter youre choise: ");
            stringUserChoise = Console.ReadLine();
            while (!int.TryParse(stringUserChoise, out intUserChoise) || intUserChoise > Enum.GetValues(typeof(eUserChoises)).Length || intUserChoise < 1)
            {
                Console.WriteLine("Invalid input! try again");
                stringUserChoise = Console.ReadLine();
            }

            return intUserChoise;
        }

        private void enterVehicleToGarage()
        {
            Vehicle vehicle;
            string licensePlate, vehicleTypeString, ownerName, phoneNumberString;
            int vehicleTypeNumber, phoneNumberInt;

            Console.WriteLine("Please enter the license plate:");
            licensePlate = Console.ReadLine();
            while (licensePlate.Length == 0)
            {
                Console.WriteLine("license plate can't be empty, try again");
                licensePlate = Console.ReadLine();
            }
            if (m_Garage.CheakIfAlreadyExist(licensePlate))
            {
                Console.WriteLine("This vehicle is aleardy in the garage, changing the status to 'In Proccess' ");
                m_Garage.RestartStatus(licensePlate);
            }
            else
            {
                Console.WriteLine("Please enter your vehicle Type (the number):");
                displayEnumValues(typeof(eVehicleTypes));
                vehicleTypeString = Console.ReadLine();
                while(!int.TryParse(vehicleTypeString, out vehicleTypeNumber) || !m_Garage.IsVehicleTypeOk(vehicleTypeNumber))
                {
                    Console.WriteLine("Invalid input, insert a vaild number");
                    vehicleTypeString = Console.ReadLine();
                }

                vehicle = m_Garage.CreateVehicle(vehicleTypeNumber);
                Console.WriteLine("Please enter owner's name:");
                ownerName = Console.ReadLine();
                while (ownerName.Length == 0)
                {
                    Console.WriteLine("owner's name can't be empty, try again");
                    ownerName = Console.ReadLine();
                }

                Console.WriteLine("Please enter phone number:");
                phoneNumberString = Console.ReadLine();
                while(!int.TryParse(phoneNumberString, out phoneNumberInt) || phoneNumberString.Length == 0)
                {
                    Console.WriteLine("Wrong phone number, try again");
                    phoneNumberString = Console.ReadLine();
                }

                m_Garage.AddToGarage(licensePlate, ownerName, phoneNumberString, vehicle);
                enterVehicleDetails(vehicle, licensePlate);
                Console.WriteLine("The vehicle was successfuly added into the garage");
            }
        }

        private void enterVhecile(string i_LicensePlate) 
        {
            string fillTogeterString, manufacturer = string.Empty, modelName;
            Vehicle vehicle =  m_Garage.GetVehicleByLicensePlate(i_LicensePlate);
            int numberOfWheels = vehicle.NumberOfWheels();
            float airPressureFloat = 0f;
            bool fillAllTogeter;

            vehicle.LicensePlate = i_LicensePlate;
            Console.WriteLine("Please enter the name of the model:");
            modelName = Console.ReadLine();
            while (modelName.Length == 0)
            {
                Console.WriteLine("model name can't be empty, try again");
                modelName = Console.ReadLine();
            } 

            vehicle.ModelName = modelName;
            Console.WriteLine("Please enter '1' if you want to fill all your wheels at once or '0' if you want to fill each one separately");
            fillTogeterString = Console.ReadLine();
            while (fillTogeterString != "0" && fillTogeterString != "1")
            {
                Console.WriteLine("Invalid input, try again");
                fillTogeterString = Console.ReadLine();
            }

            fillAllTogeter = fillTogeterString == "1" ? true : false;
            for (int i = 0; i < numberOfWheels; i++)
            {
                if (!fillAllTogeter || i == 0)
                {
                    airPressureFloat = getWheelDataFromUser(vehicle.GetMaxAirPressure(), ref manufacturer);
                }
                try
                {
                    vehicle.FillAirPressueByIndex(airPressureFloat, i, manufacturer);
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message.ToString());
                }
            }
        }

        private float getWheelDataFromUser(float i_MaxAirPressure, ref string io_Manufacturer)
        {
            string airPressureString;
            float airPressureFloat;

            Console.WriteLine("Please enter the name of the manufacturerel of your wheels:");
            io_Manufacturer = Console.ReadLine();
            Console.WriteLine("Please enter air pressue:");
            airPressureString = Console.ReadLine();
            while (!float.TryParse(airPressureString,out airPressureFloat) || airPressureFloat > i_MaxAirPressure)
            {
                Console.WriteLine("Invalid input, try again");
                airPressureString = Console.ReadLine();
            }

            return airPressureFloat;
        }

        private void enterVehicleDetails(Vehicle i_Vehicle, string i_LicensePlate)
        {
            string userInput;
            object enumValue, convertedValue;
            bool isValidInput;
            int userEnumValueNumber;

            enterVhecile(i_LicensePlate);
            foreach (PropertyInfo property in i_Vehicle.GetType().GetProperties())
            {
                if (property.CanWrite && property.Name != "LicensePlate" && property.Name != "ModelName")
                {
                    isValidInput = false;
                    while (!isValidInput)
                    {
                        try
                        {
                            Console.WriteLine($"Please enter {property.Name}:");
                            if (property.PropertyType == typeof(bool))
                            {
                                Console.WriteLine("'0' for no and '1' for yes");
                                userInput = Console.ReadLine();
                                if (userInput != "1" && userInput != "0")
                                {
                                    throw new FormatException("not '0' or '1'");
                                }

                                convertedValue = userInput == "1" ? true : false;
                                property.SetValue(i_Vehicle, convertedValue);
                                isValidInput = true;

                            }
                            else if (property.PropertyType.IsEnum)
                            {
                                Console.WriteLine("Please enter the number:");
                                displayEnumValues(property.PropertyType);
                                userInput = Console.ReadLine();
                                if (int.TryParse(userInput, out userEnumValueNumber) && Enum.IsDefined(property.PropertyType, userEnumValueNumber))
                                {
                                    enumValue = Enum.Parse(property.PropertyType, userInput);
                                    property.SetValue(i_Vehicle, enumValue);
                                    isValidInput = true;
                                }
                                else
                                {
                                    throw new ArgumentException("Invalid value, you need to enter a number");
                                }
                            }
                            else
                            {
                                userInput = Console.ReadLine();
                                convertedValue = Convert.ChangeType(userInput, property.PropertyType);
                                if (property.Name == "CurnetEnergy")
                                {
                                    m_Garage.CheckIfEnergyInsertedIsOK(i_LicensePlate, userInput);
                                    property.SetValue(i_Vehicle, convertedValue);
                                    m_Garage.UpdateEnergyLeft(i_LicensePlate);
                                }

                                property.SetValue(i_Vehicle, convertedValue);
                                isValidInput = true;
                            }
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine($"Invalid input: {exception.Message}, please try again.");
                        }
                    }
                }
            }
        }

        private void filterVehicle()
        {
            string filterChoise;
            List<string> licensePlatesFilter;
            bool validInput = false;
            int filterChoiseInt;

            while (!validInput)
            {
                try
                {
                    Console.WriteLine("Please enter what status of vehicles you want to see (the number) or press '#' to go back to menu:");
                    displayEnumValues(typeof(eStatus));
                    Console.WriteLine((Enum.GetValues(typeof(eStatus)).Length + 1) + ". All");
                    filterChoise = Console.ReadLine();
                    if (filterChoise == k_BackToMenu)
                    {
                        break;
                    }
                    else if (!int.TryParse(filterChoise, out filterChoiseInt) || filterChoiseInt < 1 || filterChoiseInt > (Enum.GetValues(typeof(eStatus)).Length) + 1)
                    {
                        throw new FormatException("Invalid input, try again");
                    }
                    else if (filterChoiseInt == (Enum.GetValues(typeof(eStatus)).Length + 1))
                    {
                        filterChoise = "All";
                    }
                    else
                    {
                        filterChoise = ((eStatus)filterChoiseInt).ToString();
                    }

                    licensePlatesFilter = m_Garage.GetGarageLicensePlates(filterChoise);
                    if (licensePlatesFilter.Count != 0)
                    {
                        Console.WriteLine("The vehicle license plates are:");
                        foreach (string licensePlate in licensePlatesFilter)
                        {
                            Console.WriteLine($"{licensePlate}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are not cars with the status '{0}' in the garage", filterChoise);
                    }

                    validInput = true;

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void fillMaxAirPressureIntoWheels()
        {
            bool validInput = false;
            string licensePlate;

            while (!validInput)
            {
                Console.WriteLine("Please enter the license plate or press '#' to go back to menu:");
                licensePlate = Console.ReadLine();
                if (licensePlate == k_BackToMenu)
                {
                    break;
                }
                try
                {
                    m_Garage.FillToMaximumAllWheels(licensePlate);
                    Console.WriteLine("The fill of the air pressure was suuccessful");
                    m_Garage.RestartStatus(licensePlate);
                    validInput = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void fillBattery()
        {
            string licensePlate = string.Empty, amountToFillString;
            float amountToFillFloat;
            bool validInput = false;

            while (!validInput)
            {
                try
                {
                    Console.WriteLine("Please enter number of lisence plate or press '#' to go back to menu:: ");
                    licensePlate = Console.ReadLine();
                    if ( licensePlate == k_BackToMenu)
                    {
                        break;
                    }

                    m_Garage.CheckIfElectric(licensePlate);
                    Console.WriteLine("Please enter the amount of minutues you want to fill  or press '#' to go back to menu:");
                    amountToFillString = Console.ReadLine();
                    if (amountToFillString == k_BackToMenu)
                    {
                        break;
                    }
                    if (!float.TryParse(amountToFillString, out amountToFillFloat) || amountToFillFloat < 0)
                    {
                       throw new FormatException("Invalid input, you need to enter a float number, try again");
                    }

                    amountToFillFloat /= 60;
                    m_Garage.FillElecric(licensePlate, amountToFillFloat);
                    Console.WriteLine("The fill of the battery was suuccessful");
                    m_Garage.RestartStatus(licensePlate);
                    validInput = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void fillFuel()
        {
            string licensePlate, stringAmountToFill, fuelTypeString;
            float floatAmountToFill;
            bool validInput = false;
            int fuelTypeInt;

            while (!validInput)
            {
                try
                {
                    Console.WriteLine("Please enter number of lisence plate or press '#' to go back to menu: ");
                    licensePlate = Console.ReadLine();
                    if (licensePlate == k_BackToMenu)
                    {
                        break;
                    }

                    m_Garage.CheckIfFueled(licensePlate);
                    Console.WriteLine("Please enter your fuel type or press '#' to go back to menu: ");
                    displayEnumValues(typeof(eFuelType));
                    fuelTypeString = Console.ReadLine();
                    if (fuelTypeString == k_BackToMenu)
                    {
                        break;
                    }
                    if (!int.TryParse(fuelTypeString, out fuelTypeInt) && Enum.IsDefined(typeof(eFuelType), fuelTypeInt))
                    {
                        throw new ArgumentException("Invalid value, you need to enter a number");
                    }
                    fuelTypeString = ((eFuelType)fuelTypeInt).ToString();
                    m_Garage.CheckIfFuelTypeOk(licensePlate, fuelTypeString);
                    Console.WriteLine("Please enter how much would you like to fill or press '#' to go back to menu: ");
                    stringAmountToFill = Console.ReadLine();
                    if (stringAmountToFill == k_BackToMenu)
                    {
                        break;
                    }

                    floatAmountToFill = m_Garage.CheckIfEnergyInsertedIsOK(licensePlate, stringAmountToFill);
                    m_Garage.FillFuel(licensePlate, fuelTypeString, floatAmountToFill);
                    Console.WriteLine("The fill of the fuel was suuccessful");
                    m_Garage.RestartStatus(licensePlate);
                    validInput = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void changeStatus()
        {
            string lisencePlate = string.Empty, newStatus;
            bool validInput = false;
            int newStatusInt;

            while (!validInput)
            {
                try
                {
                    Console.WriteLine("Please enter number of lisence plate or press '#' to go back to menu: ");
                    lisencePlate = Console.ReadLine();
                    if (lisencePlate == k_BackToMenu)
                    {
                        break;
                    }

                    Console.WriteLine("Please enter new status or press '#' to go back to menu:: ");
                    displayEnumValues(typeof(eStatus));
                    newStatus = Console.ReadLine();
                    if (newStatus == k_BackToMenu)
                    {
                        break;
                    }
                    if (!int.TryParse(newStatus, out newStatusInt) || newStatusInt < 1 || newStatusInt > Enum.GetValues(typeof(eStatus)).Length)
                    {
                        throw new FormatException("Invalid input, try again");
                    }

                    newStatus = ((eStatus)newStatusInt).ToString();
                    m_Garage.ChangeStatus(lisencePlate, newStatus);
                    Console.WriteLine("The status was suuccessfuly changed");
                    validInput = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void displayData()
        {
            string data, licensePlate;
            bool validInput = false;

            while (!validInput)
            {
                try
                {
                    Console.WriteLine("Please enter the license plate or press '#' to go back to menu:");
                    licensePlate = Console.ReadLine();
                    if (licensePlate == k_BackToMenu)
                    {
                        break;
                    }

                    data = m_Garage.getVehicleDetails(licensePlate);
                    Console.WriteLine(data);
                    validInput = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private bool doUserChoise(int i_UserChoise)
        {
            eUserChoises userChoise = (eUserChoises)i_UserChoise;
            bool stay = true;

            switch (userChoise)
            {
                case eUserChoises.EnterNewVehicle:
                    enterVehicleToGarage();
                    break;
                case eUserChoises.ViewCarsFilter:
                    filterVehicle();
                    break;
                case eUserChoises.ChangeStatus:
                    changeStatus();
                    break;
                case eUserChoises.FillAirPressure:
                    fillMaxAirPressureIntoWheels();
                    break;
                case eUserChoises.FillFuel:
                    fillFuel();
                    break;
                case eUserChoises.FillElectric:
                    fillBattery();
                    break;
                case eUserChoises.DisplayData:
                    displayData();
                    break;
                case eUserChoises.Exit:
                    stay = false;
                    break;
            }

            return stay;
        }
            
        private void displayEnumValues(Type i_EnumType)
        {
            foreach (var value in Enum.GetValues(i_EnumType))
            {
                Console.WriteLine($"{(int)value}. {value}");
            }
        }

        public void Run()
        {
            int userChoise;
            bool stay = true;

            m_Garage = new GarageLogicManager();
            while (stay)
            {
                userChoise = printMenuAndGetChoise();
                stay = doUserChoise(userChoise);
            }
        }
    }



}

