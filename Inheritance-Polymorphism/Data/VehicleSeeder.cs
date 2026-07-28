
using Inheritance_Polymorphism.Services;
using Inheritance_Polymorphism.Model;
namespace Inheritance_Polymorphism.Data

{
    public static class VehicleSeeder
    {
        public static void Seed(VehicleService vehicleService)
        {
            // ==========================
            // Cars
            // ==========================
            vehicleService.AddVehicle(new Car(
                101,
                "Toyota",
                "Corolla",
                "White",
                2022,
                4,
                FuelType.Petrol));

            vehicleService.AddVehicle(new Car(
                102,
                "Tesla",
                "Model 3",
                "Black",
                2024,
                4,
                FuelType.Electric));

            vehicleService.AddVehicle(new Car(
                103,
                "Honda",
                "Civic",
                "Blue",
                2023,
                4,
                FuelType.Hybrid));

            // ==========================
            // Trucks
            // ==========================
            vehicleService.AddVehicle(new Truck(
                201,
                "Volvo",
                "FH16",
                "Silver",
                2021,
                25));

            vehicleService.AddVehicle(new Truck(
                202,
                "Mercedes",
                "Actros",
                "White",
                2022,
                30));

            vehicleService.AddVehicle(new Truck(
                203,
                "Isuzu",
                "N-Series",
                "Blue",
                2020,
                10));

            // ==========================
            // Motorcycles
            // ==========================
            vehicleService.AddVehicle(new Motorcycle(
                301,
                "Honda",
                "CBR 600RR",
                "Red",
                2023,
                false));

            vehicleService.AddVehicle(new Motorcycle(
                302,
                "Harley-Davidson",
                "Street Glide",
                "Black",
                2022,
                true));

            vehicleService.AddVehicle(new Motorcycle(
                303,
                "Yamaha",
                "R15",
                "Blue",
                2024,
                false));
        }
    }
}