using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Car> cars = new List<Car>();
    static List<Truck> trucks = new List<Truck>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("===== Quản lý xe =====");
            Console.WriteLine("1. Thêm xe ô tô");
            Console.WriteLine("2. Thêm xe tải");
            Console.WriteLine("3. Xuất danh sách xe");
            Console.WriteLine("4. Tìm xe ô tô có số chỗ ngồi nhiều nhất");
            Console.WriteLine("5. Sắp xếp danh sách xe tải theo trọng tải");
            Console.WriteLine("6. Xuất danh sách các biển số xe đẹp");
            Console.WriteLine("7. Tính số tiền đăng kiểm định kỳ của từng xe");
            Console.WriteLine("8. Tính thời gian đăng kiểm định kỳ của từng xe sắp tới");
            Console.WriteLine("9. Tính tổng số tiền đã đăng kiểm");
            Console.WriteLine("0. Thoát");

            Console.Write("Nhập lựa chọn của bạn: ");
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
                    SortTrucksByPayload();
                    break;
                case 6:
                    DisplayBeautifulLicensePlates();
                    break;
                case 7:
                    CalculateInspectionFee();
                    break;
                case 8:
                    CalculateNextInspectionDate();
                    break;
                case 9:
                    CalculateTotalInspectionFees();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                    break;
            }
        }
    }

    static void AddCar()
    {
        Console.Write("Nhập ngày sản xuất (yyyy-MM-dd): ");
        DateTime manufactureDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Nhập biển số xe (vd: 62B6-88888): ");
        string licensePlate = Console.ReadLine();
        Console.Write("Nhập số chỗ ngồi: ");
        int seats = int.Parse(Console.ReadLine());
        Console.Write("Có đăng ký kinh doanh vận tải (true/false): ");
        bool isBusiness = bool.Parse(Console.ReadLine());

        Car car = new Car(manufactureDate, licensePlate, seats, isBusiness);
        cars.Add(car);
        Console.WriteLine("Thêm xe ô tô thành công!");
    }

    static void AddTruck()
    {
        Console.Write("Nhập ngày sản xuất (yyyy-MM-dd): ");
        DateTime manufactureDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Nhập biển số xe (vd: 30C3-12345): ");
        string licensePlate = Console.ReadLine();
        Console.Write("Nhập trọng tải (tấn): ");
        double payload = double.Parse(Console.ReadLine());

        Truck truck = new Truck(manufactureDate, licensePlate, payload);
        trucks.Add(truck);
        Console.WriteLine("Thêm xe tải thành công!");
    }

    static void DisplayAllVehicles()
    {
        Console.WriteLine("===== Danh sách xe ô tô =====");
        foreach (var car in cars)
        {
            Console.WriteLine(car.ToString());
        }

        Console.WriteLine("===== Danh sách xe tải =====");
        foreach (var truck in trucks)
        {
            Console.WriteLine(truck.ToString());
        }
    }

    static void FindCarWithMostSeats()
    {
        var carWithMostSeats = cars.OrderByDescending(c => c.Seats).FirstOrDefault();
        if (carWithMostSeats != null)
        {
            Console.WriteLine($"Xe ô tô có số chỗ ngồi nhiều nhất: {carWithMostSeats.ToString()}");
        }
        else
        {
            Console.WriteLine("Không có xe ô tô nào trong danh sách.");
        }
    }

    static void SortTrucksByPayload()
    {
        var sortedTrucks = trucks.OrderBy(t => t.Payload).ToList();
        Console.WriteLine("===== Danh sách xe tải sắp xếp theo trọng tải tăng dần =====");
        foreach (var truck in sortedTrucks)
        {
            Console.WriteLine(truck.ToString());
        }
    }

    static void DisplayBeautifulLicensePlates()
    {
        var beautifulLicensePlates = cars.Where(c => IsBeautifulLicensePlate(c.LicensePlate))
                                          .Select(c => c.LicensePlate)
                                          .ToList();
        Console.WriteLine("===== Danh sách các biển số xe đẹp =====");
        foreach (var licensePlate in beautifulLicensePlates)
        {
            Console.WriteLine(licensePlate);
        }
    }

    static void CalculateInspectionFee()
    {
        double totalFee = 0;

        foreach (var car in cars)
        {
            int inspectionInterval = car.CalculateInspectionInterval();
            double inspectionFee = car.CalculateInspectionFee();

            totalFee += inspectionFee;

            Console.WriteLine($"Biển số: {car.LicensePlate}, Thời gian đăng kiểm: {inspectionInterval} tháng, Phí đăng kiểm: {inspectionFee} VND");
        }

        foreach (var truck in trucks)
        {
            int inspectionInterval = truck.CalculateInspectionInterval();
            double inspectionFee = truck.CalculateInspectionFee();

            totalFee += inspectionFee;

            Console.WriteLine($"Biển số: {truck.LicensePlate}, Thời gian đăng kiểm: {inspectionInterval} tháng, Phí đăng kiểm: {inspectionFee} VND");
        }

        Console.WriteLine($"Tổng phí đăng kiểm: {totalFee} VND");
    }

    static void CalculateNextInspectionDate()
    {
        Console.Write("Nhập biển số xe: ");
        string licensePlate = Console.ReadLine();

        var car = cars.FirstOrDefault(c => c.LicensePlate == licensePlate);
        var truck = trucks.FirstOrDefault(t => t.LicensePlate == licensePlate);

        if (car != null)
        {
            int nextInspectionInterval = car.CalculateInspectionInterval();
            DateTime nextInspectionDate = DateTime.Now.AddMonths(nextInspectionInterval);

            Console.WriteLine($"Xe ô tô có biển số {licensePlate}, Thời gian đăng kiểm tiếp theo: {nextInspectionDate.ToString("yyyy-MM-dd")}");
        }
        else if (truck != null)
        {
            int nextInspectionInterval = truck.CalculateInspectionInterval();
            DateTime nextInspectionDate = DateTime.Now.AddMonths(nextInspectionInterval);

            Console.WriteLine($"Xe tải có biển số {licensePlate}, Thời gian đăng kiểm tiếp theo: {nextInspectionDate.ToString("yyyy-MM-dd")}");
        }
        else
        {
            Console.WriteLine("Không tìm thấy xe với biển số đã nhập.");
        }
    }

    static void CalculateTotalInspectionFees()
    {
        double totalFee = 0;

        foreach (var car in cars)
        {
            double inspectionFee = car.CalculateInspectionFee();
            totalFee += inspectionFee;
        }

        foreach (var truck in trucks)
        {
            double inspectionFee = truck.CalculateInspectionFee();
            totalFee += inspectionFee;
        }

        Console.WriteLine($"Tổng số tiền đã đăng kiểm: {totalFee} VND");
    }

    static bool IsBeautifulLicensePlate(string licensePlate)
    {
        string lastFiveDigits = licensePlate.Substring(6, 5);
        return lastFiveDigits.Distinct().Count() <= 2;
    }
}

class Vehicle
{
    public DateTime ManufactureDate { get; }
    public string LicensePlate { get; }

    public Vehicle(DateTime manufactureDate, string licensePlate)
    {
        ManufactureDate = manufactureDate;
        LicensePlate = licensePlate;
    }

    public virtual int CalculateInspectionInterval()
    {
        return 6;
    }

    public override string ToString()
    {
        return $"Biển số: {LicensePlate}, Ngày sản xuất: {ManufactureDate.ToString("yyyy-MM-dd")}";
    }
}

class Car : Vehicle
{
    public int Seats { get; }
    public bool IsBusiness { get; }

    public Car(DateTime manufactureDate, string licensePlate, int seats, bool isBusiness)
        : base(manufactureDate, licensePlate)
    {
        Seats = seats;
        IsBusiness = isBusiness;
    }

    public override int CalculateInspectionInterval()
    {
        int yearsSinceManufacture = DateTime.Now.Year - ManufactureDate.Year;
        int inspectionInterval = 6;  // Default interval for cars over 7 years

        if (yearsSinceManufacture <= 7)
        {
            if (Seats <= 10)
            {
                inspectionInterval = IsBusiness ? 12 : 24;
            }
            else
            {
                inspectionInterval = 12;
            }
        }

        return inspectionInterval;
    }

    public double CalculateInspectionFee()
    {
        double fee = Seats <= 10 ? 240000 : 320000;
        return fee;
    }

    public override string ToString()
    {
        return base.ToString() + $", Số chỗ: {Seats}, Đăng ký kinh doanh: {IsBusiness}";
    }
}

class Truck : Vehicle
{
    public double Payload { get; }

    public Truck(DateTime manufactureDate, string licensePlate, double payload)
        : base(manufactureDate, licensePlate)
    {
        Payload = payload;
    }

    public override int CalculateInspectionInterval()
    {
        int yearsSinceManufacture = DateTime.Now.Year - ManufactureDate.Year;
        int inspectionInterval = yearsSinceManufacture <= 20 ? 6 : 3;
        return inspectionInterval;
    }

    public double CalculateInspectionFee()
    {
        double fee;
        if (Payload > 20)
        {
            fee = 560000;
        }
        else if (Payload >= 7)
        {
            fee = 350000;
        }
        else
        {
            fee = 320000;
        }
        return fee;
    }

    public override string ToString()
    {
        return base.ToString() + $", Trọng tải: {Payload} tấn";
    }
}
