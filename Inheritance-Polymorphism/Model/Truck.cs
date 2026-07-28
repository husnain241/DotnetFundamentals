namespace Inheritance_Polymorphism.Model
{

    public class Truck : Vehicle
    {
        private double _loadCapacity;

        public double LoadCapacity
        {
            get => _loadCapacity;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Load capacity must be greater than zero.");
                }

                _loadCapacity = value;
            }
        }

        public Truck(
            int vehicleId,
            string brand,
            string model,
            string color,
            int year,
            double loadCapacity)
            : base(vehicleId, brand, model, color, year)
        {
            LoadCapacity = loadCapacity;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();

            Console.WriteLine($"Load Capacity : {LoadCapacity} Tons");
        }

        public override void StartEngine()
        {
            Console.WriteLine($"The {Brand} {Model} truck engine has started.");
        }
    }
}