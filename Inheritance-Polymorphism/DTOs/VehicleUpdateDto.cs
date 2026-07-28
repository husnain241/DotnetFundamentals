using Inheritance_Polymorphism.Enums;

namespace Inheritance_Polymorphism.DTOs
{
    public class VehicleUpdateDto
    {
        // Common Vehicle Properties
        public int VehicleId { get; set; }

        public string? Brand { get; set; }

        public string? Model { get; set; }

        public string? Color { get; set; }

        public int? Year { get; set; }

        // Car Properties
        public int? NumberOfDoors { get; set; }

        public FuelType? FuelType { get; set; }

        // Truck Properties
        public double? LoadCapacity { get; set; }

        // Motorcycle Properties
        public bool? HasSideCar { get; set; }
    }
}