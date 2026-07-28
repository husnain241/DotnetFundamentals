
namespace Inheritance_Polymorphism.Model
{
	public abstract class Vehicle
	{
		private int _vehicleId;
		private string _brand = string.Empty;
		private string _model = string.Empty;
		private string _color = string.Empty;
		private int _year;

		public int VehicleId
		{
			get => _vehicleId;
			set
			{
				if (value > 0)
				{
					_vehicleId = value;
				}
			}
		}

		public string Brand
		{
			get => _brand;
			set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					_brand = value.Trim();
				}
			}
		}

		public string Model
		{
			get => _model;
			set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					_model = value.Trim();
				}
			}
		}

		public string Color
		{
			get => _color;
			set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					_color = value.Trim();
				}
			}
		}

		public int Year
		{
			get => _year;
			set
			{
				if (value >= 1886 && value <= DateTime.Now.Year)
				{
					_year = value;
				}
			}
		}

		protected Vehicle(
		int vehicleId,
		string brand,
		string model,
		string color,
		int year)
		{
			VehicleId = vehicleId;
			Brand = brand;
			Model = model;
			Color = color;
			Year = year;
		}

		public virtual void DisplayInfo()
		{
			Console.WriteLine($"ID    : {VehicleId}");
			Console.WriteLine($"Brand : {Brand}");
			Console.WriteLine($"Model : {Model}");
			Console.WriteLine($"Color : {Color}");
			Console.WriteLine($"Year  : {Year}");
		}

		public virtual void StartEngine()
		{
			Console.WriteLine($"{Brand} {Model} engine started.");
		}
	}
}