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
            Console.WriteLine("1. Them xe o to");
            Console.WriteLine("2. Them xe tai");
            Console.WriteLine("3. Xuat danh sach xe");
            Console.WriteLine("4. Tim xe o to co cho ngoi nhieu nhat");
            Console.WriteLine("5. Sap xep danh sach xe tai theo trong tai");
            Console.WriteLine("6. Xuat danh sach cac bien so xe dep");
            Console.WriteLine("7. Tinh so tien dang kiem dinh ky cua tung xe");
            Console.WriteLine("8. Tinh thoi gian dang kiem dinh ky cua tung xe sap toi");
            Console.WriteLine("9. Tinh tong so tien da dang kiem");
            Console.WriteLine("0. Thoat");

            Console.Write("Nhap lua chon cua ban: ");
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
                    Console.WriteLine("Lua chon khong hop le,vui long ch.");
                    break;
            }
        }
    }

    static void AddCar()
    {
        Console.Write("Nhap ngay san xuat (yyyy-MM-dd): ");
        DateTime manufactureDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Nhap bien so xe (vd: 62B6-67519): ");
        string licensePlate = Console.ReadLine();
        Console.Write("Nhap so cho ngoi: ");
        int seats = int.Parse(Console.ReadLine());
        Console.Write("Co dang ky kinh doanh van tai (true/false): ");
        bool isBusiness = bool.Parse(Console.ReadLine());

        Car car = new Car(manufactureDate, licensePlate, seats, isBusiness);
        cars.Add(car);
        Console.WriteLine("Them xe o to thanh cong!");
    }

    static void AddTruck()
    {
        Console.Write("Nhap ngay san xuat (yyyy-MM-dd): ");
        DateTime manufactureDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Nhap bien so xe (vd: 30C3-12345): ");
        string licensePlate = Console.ReadLine();
        Console.Write("Nhap trong tai (tấn): ");
        double payload = double.Parse(Console.ReadLine());

        Truck truck = new Truck(manufactureDate, licensePlate, payload);
        trucks.Add(truck);
        Console.WriteLine("Them xe tai thanh cong!");
    }

    static void DisplayAllVehicles()
    {
        Console.WriteLine("===== Danh sach xe o to =====");
        foreach (var car in cars)
        {
            Console.WriteLine(car.ToString());
        }

        Console.WriteLine("===== Danh sach xe tai =====");
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
            Console.WriteLine($"Xe o to co so cho ngoi nhieu nhat: {carWithMostSeats.ToString()}");
        }
        else
        {
            Console.WriteLine("Khong co xe o to nao trong danh sach.");
        }
    }

    static void SortTrucksByPayload()
    {
        var sortedTrucks = trucks.OrderBy(t => t.Payload).ToList();
        Console.WriteLine("===== Danh sach xe tai sap xep theo trong tai tang dan =====");
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
        Console.WriteLine("===== Danh sach cac bien so xe đep =====");
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

            Console.WriteLine($"Bien so: {car.LicensePlate}, Thoi gian đang kiem: {inspectionInterval} thang, Phi đang kiem: {inspectionFee} VND");
        }

        foreach (var truck in trucks)
        {
            int inspectionInterval = truck.CalculateInspectionInterval();
            double inspectionFee = truck.CalculateInspectionFee();

            totalFee += inspectionFee;

            Console.WriteLine($"Bien so: {truck.LicensePlate}, Thoi gian đang kiem: {inspectionInterval} thang, Phi đang kiem: {inspectionFee} VND");
        }

        Console.WriteLine($"Tong phi đang kiem: {totalFee} VND");
    }

    static void CalculateNextInspectionDate()
    {
        Console.Write("Nhap bien so xe: ");
        string licensePlate = Console.ReadLine();

        var car = cars.FirstOrDefault(c => c.LicensePlate == licensePlate);
        var truck = trucks.FirstOrDefault(t => t.LicensePlate == licensePlate);

        if (car != null)
        {
            int nextInspectionInterval = car.CalculateInspectionInterval();
            DateTime nextInspectionDate = DateTime.Now.AddMonths(nextInspectionInterval);

            Console.WriteLine($"Xe o to co bien so {licensePlate}, Thoi gian đang kiem tiep theo: {nextInspectionDate.ToString("yyyy-MM-dd")}");
        }
        else if (truck != null)
        {
            int nextInspectionInterval = truck.CalculateInspectionInterval();
            DateTime nextInspectionDate = DateTime.Now.AddMonths(nextInspectionInterval);

            Console.WriteLine($"Xe tai co bien so {licensePlate}, Thoi gian đang kiem tiep theo: {nextInspectionDate.ToString("yyyy-MM-dd")}");
        }
        else
        {
            Console.WriteLine("Khong tim thay xe voi bien so đa nhap.");
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

        Console.WriteLine($"Tong so tien đa đang kiem: {totalFee} VND");
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
        return $"Bien so: {LicensePlate}, Ngay san xuat: {ManufactureDate.ToString("yyyy-MM-dd")}";
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
        return base.ToString() + $", So cho: {Seats}, Đang ky kinh doanh: {IsBusiness}";
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
        return base.ToString() + $", Trong tai: {Payload} tan";
    }
}
