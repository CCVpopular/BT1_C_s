using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<Vehicle> vehicles = new List<Vehicle>();

        while (true)
        {
            Console.WriteLine("Chương trình quản lý xe");
            Console.WriteLine("1. Thêm xe ô tô");
            Console.WriteLine("2. Thêm xe tải");
            Console.WriteLine("3. Xuất danh sách xe");
            Console.WriteLine("4. Tìm xe ô tô có số chỗ ngồi nhiều nhất");
            Console.WriteLine("5. Sắp xếp danh sách xe tải theo trọng tải tăng dần");
            Console.WriteLine("6. Xuất danh sách các biển số xe đẹp");
            Console.WriteLine("7. Tính số tiền đăng kiểm định kỳ của từng xe");
            Console.WriteLine("8. Tính thời gian đăng kiểm định kỳ của từng xe sắp tới");
            Console.WriteLine("9. Tính tổng số tiền đã đăng kiểm");
            Console.WriteLine("0. Thoát");
            Console.Write("Nhập lựa chọn: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddCar(vehicles);
                    break;
                case 2:
                    AddTruck(vehicles);
                    break;
                case 3:
                    DisplayVehicles(vehicles);
                    break;
                case 4:
                    FindCarWithMostSeats(vehicles);
                    break;
                case 5:
                    SortTrucksByLoadCapacity(vehicles);
                    break;
                case 6:
                    DisplayBeautifulLicensePlates(vehicles);
                    break;
                case 7:
                    CalculateInspectionCost(vehicles);
                    break;
                case 8:
                    CalculateNextInspectionDate(vehicles);
                    break;
                case 9:
                    CalculateTotalInspectionCost(vehicles);
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Hãy chọn lại.");
                    break;
            }
        }
    }

    static void AddCar(List<Vehicle> vehicles)
    {
        Console.Write("Nhập ngày sản xuất (dd/mm/yyyy): ");
        DateTime productionDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Nhập biển số xe: ");
        string licensePlate = Console.ReadLine();

        Console.Write("Nhập số chỗ ngồi: ");
        int seats = int.Parse(Console.ReadLine());
        Console.Write("Có đăng ký kinh doanh vận tải (yes/no): ");
        bool isRegisteredForTransport = Console.ReadLine().ToLower() == "yes";

        Car car = new Car(productionDate, licensePlate, seats, isRegisteredForTransport);
        vehicles.Add(car);
        Console.WriteLine("Xe ô tô đã được thêm vào danh sách.");
    }

    static void AddTruck(List<Vehicle> vehicles)
    {
        Console.Write("Nhập ngày sản xuất (dd/mm/yyyy): ");
        DateTime productionDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Nhập biển số xe: ");
        string licensePlate = Console.ReadLine();

        Console.Write("Nhập trọng tải (tấn): ");
        double loadCapacity = double.Parse(Console.ReadLine());

        Truck truck = new Truck(productionDate, licensePlate, loadCapacity);
        vehicles.Add(truck);
        Console.WriteLine("Xe tải đã được thêm vào danh sách.");
    }

    static void DisplayVehicles(List<Vehicle> vehicles)
    {
        Console.WriteLine("Danh sách xe:");
        foreach (var vehicle in vehicles)
        {
            Console.WriteLine(vehicle);
        }
    }

    static void FindCarWithMostSeats(List<Vehicle> vehicles)
    {
        var cars = vehicles.OfType<Car>().ToList();
        if (cars.Count > 0)
        {
            var carWithMostSeats = cars.OrderByDescending(c => c.Seats).First();
            Console.WriteLine("Xe ô tô có số chỗ ngồi nhiều nhất:");
            Console.WriteLine(carWithMostSeats);
        }
        else
        {
            Console.WriteLine("Không có xe ô tô trong danh sách.");
        }
    }

    static void SortTrucksByLoadCapacity(List<Vehicle> vehicles)
    {
        var trucks = vehicles.OfType<Truck>().ToList();
        if (trucks.Count > 0)
        {
            var sortedTrucks = trucks.OrderBy(t => t.LoadCapacity).ToList();
            Console.WriteLine("Danh sách xe tải theo trọng tải tăng dần:");
            foreach (var truck in sortedTrucks)
            {
                Console.WriteLine(truck);
            }
        }
        else
        {
            Console.WriteLine("Không có xe tải trong danh sách.");
        }
    }

    static void DisplayBeautifulLicensePlates(List<Vehicle> vehicles)
    {
        var beautifulLicensePlates = vehicles.Where(v => v.HasBeautifulLicensePlate()).ToList();
        if (beautifulLicensePlates.Count > 0)
        {
            Console.WriteLine("Danh sách biển số xe đẹp:");
            foreach (var vehicle in beautifulLicensePlates)
            {
                Console.WriteLine(vehicle.LicensePlate);
            }
        }
        else
        {
            Console.WriteLine("Không có biển số xe đẹp trong danh sách.");
        }
    }

    static void CalculateInspectionCost(List<Vehicle> vehicles)
    {
        Console.Write("Nhập ngày kiểm định (dd/mm/yyyy): ");
        DateTime inspectionDate = DateTime.Parse(Console.ReadLine());

        double totalCost = 0;
        foreach (var vehicle in vehicles)
        {
            totalCost += vehicle.CalculateInspectionCost(inspectionDate);
        }

        Console.WriteLine($"Tổng số tiền đăng kiểm định kỳ là: {totalCost:C}");
    }

    static void CalculateNextInspectionDate(List<Vehicle> vehicles)
    {
        Console.Write("Nhập ngày hiện tại (dd/mm/yyyy): ");
        DateTime currentDate = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Thời gian đăng kiểm định kỳ của từng xe sắp tới:");
        foreach (var vehicle in vehicles)
        {
            DateTime nextInspectionDate = vehicle.CalculateNextInspectionDate(currentDate);
            Console.WriteLine($"{vehicle.LicensePlate}: {nextInspectionDate:dd/MM/yyyy}");
        }
    }

    static void CalculateTotalInspectionCost(List<Vehicle> vehicles)
    {
        double totalCost = 0;
        foreach (var vehicle in vehicles)
        {
            totalCost += vehicle.CalculateInspectionCost(DateTime.Now);
        }

        Console.WriteLine($"Tổng số tiền đã đăng kiểm là: {totalCost:C}");
    }
}

class Vehicle
{
    public DateTime ProductionDate { get; }
    public string LicensePlate { get; }

    public Vehicle(DateTime productionDate, string licensePlate)
    {
        ProductionDate = productionDate;
        LicensePlate = licensePlate;
    }

    public virtual double CalculateInspectionCost(DateTime inspectionDate)
    {
        return 0;
    }

    public virtual DateTime CalculateNextInspectionDate(DateTime currentDate)
    {
        return DateTime.Now;
    }

    public virtual bool HasBeautifulLicensePlate()
    {
        return false;
    }

    public override string ToString()
    {
        return $"Biển số: {LicensePlate}, Ngày sản xuất: {ProductionDate:dd/MM/yyyy}";
    }
}

class Car : Vehicle
{
    public int Seats { get; }
    public bool IsRegisteredForTransport { get; }

    public Car(DateTime productionDate, string licensePlate, int seats, bool isRegisteredForTransport)
        : base(productionDate, licensePlate)
    {
        Seats = seats;
        IsRegisteredForTransport = isRegisteredForTransport;
    }

    public override double CalculateInspectionCost(DateTime inspectionDate)
    {
        double cost = Seats <= 10 ? 240000 : 320000;
        if (IsRegisteredForTransport)
        {
            cost /= 2;
        }
        return cost;
    }

    public override DateTime CalculateNextInspectionDate(DateTime currentDate)
    {
        int yearsSinceProduction = currentDate.Year - ProductionDate.Year;
        int inspectionInterval = yearsSinceProduction <= 7 ? (Seats <= 10 ? 2 : 1) : 0;
        return currentDate.AddMonths(6 * inspectionInterval);
    }

    public override bool HasBeautifulLicensePlate()
    {
        string serialNumber = LicensePlate.Substring(6, 5);
        return serialNumber.Distinct().Count() <= 2;
    }

    public override string ToString()
    {
        return base.ToString() + $", Số chỗ ngồi: {Seats}, Đăng ký kinh doanh: {IsRegisteredForTransport}";
    }
}

class Truck : Vehicle
{
    public double LoadCapacity { get; }

    public Truck(DateTime productionDate, string licensePlate, double loadCapacity)
        : base(productionDate, licensePlate)
    {
        LoadCapacity = loadCapacity;
    }

    public override double CalculateInspectionCost(DateTime inspectionDate)
    {
        if (LoadCapacity > 20)
        {
            return 560000;
        }
        else if (LoadCapacity >= 7)
        {
            return 350000;
        }
        else
        {
            return 320000;
        }
    }

    public override DateTime CalculateNextInspectionDate(DateTime currentDate)
    {
        int yearsSinceProduction = currentDate.Year - ProductionDate.Year;
        int inspectionInterval = yearsSinceProduction <= 20 ? 1 : 0;
        return currentDate.AddMonths(6 * inspectionInterval);
    }

    public override string ToString()
    {
        return base.ToString() + $", Trọng tải: {LoadCapacity} tấn";
    }
}
