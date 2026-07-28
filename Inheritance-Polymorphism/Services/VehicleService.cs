using Inheritance_Polymorphism.Model;

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

        // Update Vehicle
        public bool UpdateVehicle(Vehicle vehicle)
        {
            Vehicle? existingVehicle = SearchVehicle(vehicle.VehicleId);

            if (existingVehicle == null)
            {
                return false;
            }

            int index = _vehicles.IndexOf(existingVehicle);

            _vehicles[index] = vehicle;

            return true;
        }
    }
}