using Inheritance_Polymorphism.Model;
using Inheritance_Polymorphism.DTOs;

namespace Inheritance_Polymorphism.Services
{
    public class VehicleService
    {
        private readonly List<Vehicle> _vehicles;

        public VehicleService()
        {
            _vehicles = new List<Vehicle>();
        }

        // Add Vehicle
        public int? AddVehicle(Vehicle vehicle)
        {
            if (_vehicles.Any(v => v.VehicleId == vehicle.VehicleId))
            {
                return null;
            }

            _vehicles.Add(vehicle);
            return vehicle.VehicleId;
        }

        // View All Vehicles
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return _vehicles;
        }

        // Search Vehicle
        public Vehicle? SearchVehicle(int vehicleId)
        {
            return _vehicles.FirstOrDefault(v => v.VehicleId == vehicleId);
        }

        // Delete Vehicle
        public bool DeleteVehicle(int vehicleId)
        {
            Vehicle? vehicle = SearchVehicle(vehicleId);

            if (vehicle == null)
            {
                return false;
            }

            _vehicles.Remove(vehicle);
            return true;
        }

        public bool UpdateVehicle(VehicleUpdateDto dto)
        {
            Vehicle? vehicle = SearchVehicle(dto.VehicleId);

            if (vehicle == null)
            {
                return false;
            }

            // Base Properties
            if (!string.IsNullOrWhiteSpace(dto.Brand))
            {
                vehicle.Brand = dto.Brand;
            }

            if (!string.IsNullOrWhiteSpace(dto.Model))
            {
                vehicle.Model = dto.Model;
            }

            if (!string.IsNullOrWhiteSpace(dto.Color))
            {
                vehicle.Color = dto.Color;
            }

            if (dto.Year.HasValue)
            {
                vehicle.Year = dto.Year.Value;
            }

            // Car
            if (vehicle is Car car)
            {
                if (dto.NumberOfDoors.HasValue)
                {
                    car.NumberOfDoors = dto.NumberOfDoors.Value;
                }

                if (dto.FuelType.HasValue)
                {
                    car.FuelType = dto.FuelType.Value;
                }
            }

            // Truck
            else if (vehicle is Truck truck)
            {
                if (dto.LoadCapacity.HasValue)
                {
                    truck.LoadCapacity = dto.LoadCapacity.Value;
                }
            }

            // Motorcycle
            else if (vehicle is Motorcycle motorcycle)
            {
                if (dto.HasSideCar.HasValue)
                {
                    motorcycle.HasSideCar = dto.HasSideCar.Value;
                }
            }

            return true;
        }
    }
}