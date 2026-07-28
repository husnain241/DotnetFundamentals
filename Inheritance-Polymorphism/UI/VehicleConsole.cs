using Inheritance_Polymorphism.Enums;
using Inheritance_Polymorphism.Helpers;
using Inheritance_Polymorphism.Model;
using Inheritance_Polymorphism.Services;

namespace Inheritance_Polymorphism.UI
{
	public class VehicleConsoleUI
	{
		private readonly VehicleService _vehicleService;

		public VehicleConsoleUI(VehicleService vehicleService)
		{
			_vehicleService = vehicleService;
		}

		public void Run()
		{
			bool exit = false;

			while (!exit)
			{
				Console.Clear();

				Console.WriteLine("===================================");
				Console.WriteLine("      Vehicle Management System");
				Console.WriteLine("===================================");
				Console.WriteLine("1. Add Vehicle");
				Console.WriteLine("2. View All Vehicles");
				Console.WriteLine("3. Search Vehicle");
				Console.WriteLine("4. Delete Vehicle");
				Console.WriteLine("5. Update Vehicle");
				Console.WriteLine("0. Exit");

				int choice = ConsoleHelper.ReadInt("Select an option: ").Value;

				switch (choice)
				{
					case 1:
						AddVehicle();
						break;

					case 2:
						ViewAllVehicles();
						break;

					case 3:
						SearchVehicle();
						break;

					case 4:
						DeleteVehicle();
						break;

					case 5:
						UpdateVehicle();
						break;

					case 0:
						exit = true;
						break;

					default:
						Console.WriteLine("Invalid option.");
						ConsoleHelper.Pause();
						break;
				}
			}
		}

		private void ViewAllVehicles()
		{
			Console.Clear();

			IEnumerable<Vehicle> vehicles = _vehicleService.GetAllVehicles();

			if (!vehicles.Any())
			{
				Console.WriteLine("No vehicles available.");
				ConsoleHelper.Pause();
				return;
			}

			foreach (Vehicle vehicle in vehicles)
			{
				vehicle.DisplayInfo();
				Console.WriteLine(new string('-', 40));
			}

			ConsoleHelper.Pause();
		}

		private void SearchVehicle()
		{
			Console.Clear();

			int id = ConsoleHelper.ReadInt("Enter Vehicle ID: ").Value;

			Vehicle? vehicle = _vehicleService.SearchVehicle(id);

			if (vehicle == null)
			{
				Console.WriteLine("Vehicle not found.");
			}
			else
			{
				vehicle.DisplayInfo();
			}

			ConsoleHelper.Pause();
		}

		private void DeleteVehicle()
		{
			Console.Clear();

			int id = ConsoleHelper.ReadInt("Enter Vehicle ID: ").Value;

			if (_vehicleService.DeleteVehicle(id))
			{
				Console.WriteLine("Vehicle deleted successfully.");
			}
			else
			{
				Console.WriteLine("Vehicle not found.");
			}

			ConsoleHelper.Pause();
		}

		private void AddVehicle()
		{
			Console.Clear();

			Console.WriteLine("Select Vehicle Type");
			Console.WriteLine("1. Car");
			Console.WriteLine("2. Truck");
			Console.WriteLine("3. Motorcycle");

			int type = ConsoleHelper.ReadInt("Choice: ").Value;

			int id = ConsoleHelper.ReadInt("Vehicle ID: ").Value;
			string brand = ConsoleHelper.ReadString("Brand: ")!;
			string model = ConsoleHelper.ReadString("Model: ")!;
			string color = ConsoleHelper.ReadString("Color: ")!;
			int year = ConsoleHelper.ReadInt("Year: ").Value;

			Vehicle vehicle;

			switch (type)
			{
				case 1:

					Console.Write("Fuel Type (Petrol, Diesel, Electric, Hybrid, CNG): ");

					string? input = Console.ReadLine();

					if (int.TryParse(input, out _))
					{
						Console.WriteLine("Please enter the fuel type name, not a number.");
						ConsoleHelper.Pause();
						return;
					}

					if (!Enum.TryParse(input, true, out FuelType fuelType))
					{
						Console.WriteLine("Invalid Fuel Type.");
						ConsoleHelper.Pause();
						return;
					}

					int doors = ConsoleHelper.ReadInt("Number of Doors: ").Value;

					vehicle = new Car(
						id,
						brand,
						model,
						color,
						year,
						doors,
						fuelType);

					break;

				case 2:

					double capacity = ConsoleHelper.ReadDouble("Load Capacity (Tons): ").Value;

					vehicle = new Truck(
						id,
						brand,
						model,
						color,
						year,
						capacity);

					break;

				case 3:

					bool hasSideCar = ConsoleHelper.ReadBool("Has Side Car (Y/N): ").Value;

					vehicle = new Motorcycle(
						id,
						brand,
						model,
						color,
						year,
						hasSideCar);

					break;

				default:
					Console.WriteLine("Invalid vehicle type.");
					ConsoleHelper.Pause();
					return;
			}

            int? vehicleId = _vehicleService.AddVehicle(vehicle);

            if (vehicleId.HasValue)
            {
                Console.WriteLine($"Vehicle added successfully. ID: {vehicleId}");
            }
            else
            {
                Console.WriteLine("A vehicle with this ID already exists.");
            }

            ConsoleHelper.Pause();
		}

		private void UpdateVehicle()
		{
			Console.Clear();

			int id = ConsoleHelper.ReadInt("Enter Vehicle ID to update: ").Value;

			Vehicle? existingVehicle = _vehicleService.SearchVehicle(id);

			if (existingVehicle == null)
			{
				Console.WriteLine("Vehicle not found.");
				ConsoleHelper.Pause();
				return;
			}

			Console.WriteLine("Leave field empty to keep the current value.");
			Console.WriteLine();

			// Base Properties
			string? brand = ConsoleHelper.ReadString(
				$"Brand ({existingVehicle.Brand}): ",
				InputMode.Optional);

			string? model = ConsoleHelper.ReadString(
				$"Model ({existingVehicle.Model}): ",
				InputMode.Optional);

			string? color = ConsoleHelper.ReadString(
				$"Color ({existingVehicle.Color}): ",
				InputMode.Optional);

			int? year = ConsoleHelper.ReadInt(
				$"Year ({existingVehicle.Year}): ",
				InputMode.Optional);

			if (!string.IsNullOrWhiteSpace(brand))
				existingVehicle.Brand = brand;

			if (!string.IsNullOrWhiteSpace(model))
				existingVehicle.Model = model;

			if (!string.IsNullOrWhiteSpace(color))
				existingVehicle.Color = color;

			if (year.HasValue)
				existingVehicle.Year = year.Value;

			// Car
			if (existingVehicle is Car car)
			{
				int? doors = ConsoleHelper.ReadInt(
					$"Number of Doors ({car.NumberOfDoors}): ",
					InputMode.Optional);

				if (doors.HasValue)
					car.NumberOfDoors = doors.Value;

				Console.WriteLine($"Current Fuel Type: {car.FuelType}");
				Console.Write("New Fuel Type (Petrol, Diesel, Electric, Hybrid, CNG) or press Enter to keep current: ");

				string? input = Console.ReadLine();

				if (!string.IsNullOrWhiteSpace(input))
				{
					if (int.TryParse(input, out _))
					{
						Console.WriteLine("Please enter the fuel type name, not a number.");
					}
					else if (Enum.TryParse(input, true, out FuelType fuelType))
					{
						car.FuelType = fuelType;
					}
					else
					{
						Console.WriteLine("Invalid Fuel Type.");
					}
				}
			}

			// Truck
			else if (existingVehicle is Truck truck)
			{
				double? capacity = ConsoleHelper.ReadDouble(
					$"Load Capacity ({truck.LoadCapacity}): ",
					InputMode.Optional);

				if (capacity.HasValue)
					truck.LoadCapacity = capacity.Value;
			}

			// Motorcycle
			else if (existingVehicle is Motorcycle motorcycle)
			{
				bool? hasSideCar = ConsoleHelper.ReadBool(
					$"Has Side Car ({(motorcycle.HasSideCar ? "Y" : "N")}) (Y/N): ",
					InputMode.Optional);

				if (hasSideCar.HasValue)
					motorcycle.HasSideCar = hasSideCar.Value;
			}

			if (_vehicleService.UpdateVehicle(existingVehicle))
			{
				Console.WriteLine("Vehicle updated successfully.");
			}
			else
			{
				Console.WriteLine("Failed to update vehicle.");
			}

			ConsoleHelper.Pause();
		}
	}
}