using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectronicVehicle : Engine
    {
        public ElectronicVehicle(float i_MaxTime) : base(i_MaxTime) { }

        public void AddElectrition(float i_AddElectric)
        {
            if (i_AddElectric + base.CurnetEnergy > base.MaxEnergy)
            {
                throw new ValueOutOfRangeException(0, (base.MaxEnergy - base.CurnetEnergy) * 60);
            }
            base.CurnetEnergy += i_AddElectric;
        }

        public override string ToString()
        {
            StringBuilder electricVehicleDetails = new StringBuilder();

            electricVehicleDetails.Append(base.ToString()); 

            return electricVehicleDetails.ToString();
        }
    }
}
