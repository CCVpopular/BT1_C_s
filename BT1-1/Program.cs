using System;
using System.Collections.Generic;
using System.Linq;

class Vehicle
{
    public string LicensePlate { get; set; }
    public DateTime ManufacturingDate { get; set; }
}

class Car : Vehicle
{
    public int NumSeats { get; set; }
    public bool IsTransportationBusiness { get; set; }
}

class Truck : Vehicle
{
    public double LoadCapacity { get; set; }
}

class Program
{
    static List<Vehicle> vehicles = new List<Vehicle>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Car");
            Console.WriteLine("2. Add Truck");
            Console.WriteLine("3. Display All Vehicles");
            Console.WriteLine("4. Find Car with Most Seats");
            Console.WriteLine("5. Sort Trucks by Load Capacity");
            Console.WriteLine("6. Display Beautiful License Plates");
            Console.WriteLine("7. Calculate Inspection Fees");
            Console.WriteLine("8. Calculate Upcoming Inspection Dates");
            Console.WriteLine("9. Calculate Total Inspection Fees");
            Console.WriteLine("0. Exit");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddCar();
                    break;
                case 2:
                    AddTruck();
                    break;
                case 3:
                    DisplayAllVehicles();
                    break;
                case 4:
                    FindCarWithMostSeats();
                    break;
                case 5:
                    SortTrucksByLoadCapacity();
                    break;
                case 6:
                    DisplayBeautifulLicensePlates();
                    break;
                case 7:
                    CalculateInspectionFees();
                    break;
                case 8:
                    CalculateUpcomingInspectionDates();
                    break;
                case 9:
                    CalculateTotalInspectionFees();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose again.");
                    break;
            }
        }
    }

    static void AddCar()
    {
        Console.Write("Enter license plate: ");
        string licensePlate = Console.ReadLine();
        Console.Write("Enter manufacturing date (yyyy-mm-dd): ");
        DateTime manufacturingDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter number of seats: ");
        int numSeats = int.Parse(Console.ReadLine());
        Console.Write("Is for transportation business? (true/false): ");
        bool isTransportationBusiness = bool.Parse(Console.ReadLine());

        Car car = new Car
        {
            LicensePlate = licensePlate,
            ManufacturingDate = manufacturingDate,
            NumSeats = numSeats,
            IsTransportationBusiness = isTransportationBusiness
        };

        vehicles.Add(car);
        Console.WriteLine("Car added successfully.");
    }

    static void AddTruck()
    {
        Console.Write("Enter license plate: ");
        string licensePlate = Console.ReadLine();
        Console.Write("Enter manufacturing date (yyyy-mm-dd): ");
        DateTime manufacturingDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter load capacity (tons): ");
        double loadCapacity = double.Parse(Console.ReadLine());

        Truck truck = new Truck
        {
            LicensePlate = licensePlate,
            ManufacturingDate = manufacturingDate,
            LoadCapacity = loadCapacity
        };

        vehicles.Add(truck);
        Console.WriteLine("Truck added successfully.");
    }

    static void DisplayAllVehicles()
    {
        Console.WriteLine("List of all vehicles:");
        foreach (var vehicle in vehicles)
        {
            Console.WriteLine($"License Plate: {vehicle.LicensePlate}, Manufacturing Date: {vehicle.ManufacturingDate}");
        }
    }

    static void FindCarWithMostSeats()
    {
        var cars = vehicles.OfType<Car>();
        if (cars.Any())
        {
            var carWithMostSeats = cars.OrderByDescending(car => car.NumSeats).FirstOrDefault();
            Console.WriteLine($"Car with most seats: License Plate: {carWithMostSeats.LicensePlate}, Number of Seats: {carWithMostSeats.NumSeats}");
        }
        else
        {
            Console.WriteLine("No cars found.");
        }
    }

    static void SortTrucksByLoadCapacity()
    {
        var trucks = vehicles.OfType<Truck>();
        if (trucks.Any())
        {
            var sortedTrucks = trucks.OrderBy(truck => truck.LoadCapacity);
            Console.WriteLine("Trucks sorted by load capacity:");
            foreach (var truck in sortedTrucks)
            {
                Console.WriteLine($"License Plate: {truck.LicensePlate}, Load Capacity: {truck.LoadCapacity} tons");
            }
        }
        else
        {
            Console.WriteLine("No trucks found.");
        }
    }

    static void DisplayBeautifulLicensePlates()
    {
        var beautifulPlates = vehicles.Where(vehicle => IsBeautifulLicensePlate(vehicle.LicensePlate));
        if (beautifulPlates.Any())
        {
            Console.WriteLine("Beautiful license plates:");
            foreach (var vehicle in beautifulPlates)
            {
                Console.WriteLine($"License Plate: {vehicle.LicensePlate}");
            }
        }
        else
        {
            Console.WriteLine("No beautiful license plates found.");
        }
    }

    static bool IsBeautifulLicensePlate(string licensePlate)
    {
        string[] parts = licensePlate.Split('-');
        string lastPart = parts[1];
        return lastPart.Distinct().Count() >= 4;
    }

    static void CalculateInspectionFees()
    {
        double totalFees = 0;
        foreach (var vehicle in vehicles)
        {
            double fee = CalculateVehicleInspectionFee(vehicle);
            totalFees += fee;
            Console.WriteLine($"License Plate: {vehicle.LicensePlate}, Inspection Fee: {fee} VND");
        }
        Console.WriteLine($"Total Inspection Fees: {totalFees} VND");
    }

    static double CalculateVehicleInspectionFee(Vehicle vehicle)
    {
        if (vehicle is Car car)
        {
            if (DateTime.Now.Year - car.ManufacturingDate.Year <= 7)
            {
                if (car.NumSeats <= 10)
                {
                    return car.IsTransportationBusiness ? 320000 : 240000;
                }
                else
                {
                    return 320000;
                }
            }
            else
            {
                return 60000;
            }
        }
        else if (vehicle is Truck truck)
        {
            if (DateTime.Now.Year - truck.ManufacturingDate.Year <= 20)
            {
                if (truck.LoadCapacity > 20)
                {
                    return 560000;
                }
                else if (truck.LoadCapacity >= 7 && truck.LoadCapacity <= 20)
                {
                    return 350000;
                }
                else
                {
                    return 320000;
                }
            }
            else
            {
                return 30000;
            }
        }
        return 0;
    }

    static void CalculateUpcomingInspectionDates()
    {
        foreach (var vehicle in vehicles)
        {
            DateTime nextInspectionDate = CalculateNextInspectionDate(vehicle);
            Console.WriteLine($"License Plate: {vehicle.LicensePlate}, Next Inspection Date: {nextInspectionDate.ToString("yyyy-MM-dd")}");
        }
    }

    static DateTime CalculateNextInspectionDate(Vehicle vehicle)
    {
        int inspectionIntervalMonths;
        if (vehicle is Car car)
        {
            if (DateTime.Now.Year - car.ManufacturingDate.Year <= 7)
            {
                inspectionIntervalMonths = car.NumSeats <= 10 && car.IsTransportationBusiness ? 12 : 24;
            }
            else
            {
                inspectionIntervalMonths = 6;
            }
        }
        else if (vehicle is Truck truck)
        {
            if (DateTime.Now.Year - truck.ManufacturingDate.Year <= 20)
            {
                inspectionIntervalMonths = truck.LoadCapacity > 20 ? 3 : 6;
            }
            else
            {
                inspectionIntervalMonths = 3;
            }
        }
        else
        {
            inspectionIntervalMonths = 0;
        }

        return vehicle.ManufacturingDate.AddMonths(inspectionIntervalMonths);
    }

    static void CalculateTotalInspectionFees()
    {
        double totalFees = vehicles.Sum(vehicle => CalculateVehicleInspectionFee(vehicle));
        Console.WriteLine($"Total Inspection Fees: {totalFees} VND");
    }
}
