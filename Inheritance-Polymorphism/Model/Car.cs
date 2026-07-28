namespace Inheritance_Polymorphism.Model
{

    public class Car : Vehicle
    {
        private int _numberOfDoors;
        private FuelType _fuelType;



        public int NumberOfDoors
        {
            get => _numberOfDoors;
            set
            {
                if (value >= 2 && value <= 6)
                {
                    _numberOfDoors = value;
                }
            }
        }

        public FuelType FuelType
        {
            get => _fuelType;
            set => _fuelType = value;
        }

        public Car(
        int vehicleId,
        string brand,
        string model,
        string color,
        int year,
        int numberOfDoors,
        FuelType fuelType)
        : base(vehicleId, brand, model, color, year)
        {
            NumberOfDoors = numberOfDoors;
            FuelType = fuelType;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();

            Console.WriteLine($"Doors : {NumberOfDoors}");
            Console.WriteLine($"Fuel  : {FuelType}");
        }

        public override void StartEngine()
        {
            Console.WriteLine(
                $"The {Brand} {Model} car engine has started."
            );
        }
    }
}