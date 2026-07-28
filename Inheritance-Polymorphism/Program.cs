using Inheritance_Polymorphism.Data;
using Inheritance_Polymorphism.Services;
using Inheritance_Polymorphism.UI;

VehicleService vehicleService = new VehicleService();

VehicleSeeder.Seed(vehicleService);

VehicleConsoleUI ui = new VehicleConsoleUI(vehicleService);

ui.Run();