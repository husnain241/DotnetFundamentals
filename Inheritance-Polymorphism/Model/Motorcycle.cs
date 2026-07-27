namespace Inheritance_Polymorphism.Model
{

    public class Motorcycle : Vehicle
    {
        private bool _hasSideCar;

        public bool HasSideCar
        {
            get => _hasSideCar;
            set => _hasSideCar = value;
        }

        public Motorcycle(
            int vehicleId,
            string brand,
            string model,
            string color,
            int year,
            bool hasSideCar)
            : base(vehicleId, brand, model, color, year)
        {
            HasSideCar = hasSideCar;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();

            Console.WriteLine($"Side Car : {(HasSideCar ? "Yes" : "No")}");
        }

        public override void StartEngine()
        {
            Console.WriteLine($"The {Brand} {Model} motorcycle engine has started.");
        }
    }
}
