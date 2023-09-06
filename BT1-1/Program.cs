using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<XEOTO> xeoto = new List<XEOTO>();
    static List<XETAI> xetai = new List<XETAI>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("===== Quan ly xe =====");
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
            int chon = int.Parse(Console.ReadLine());

            switch (chon)
            {
                case 1:
                    themxeoto();
                    break;
                case 2:
                    themxetai();
                    break;
                case 3:
                    xuattatcaxe();
                    break;
                case 4:
                    xecochongoilonnhat();
                    break;
                case 5:
                    xapsepxetaitheotrongtai();
                    break;
                case 6:
                    xuatbiensoxedep();
                    break;
                case 7:
                    sotiendangkiemtungxehientai();
                    break;
                case 8:
                    sothoigiandangkiemtungxesaptoi();
                    break;
                case 9:
                    tongsotiendadangkiem();
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

    static void themxeoto()
    {
        Console.Write("Nhap ngay san xuat (yyyy-MM-dd): ");
        DateTime ngaysanxuat = DateTime.Parse(Console.ReadLine());
        Console.Write("Nhap bien so xe (vd: 62B6-67519): ");
        string biensoxe = Console.ReadLine();
        Console.Write("Nhap so cho ngoi: ");
        int chongoi = int.Parse(Console.ReadLine());
        Console.Write("Co dang ky kinh doanh van tai (true/false): ");
        bool doanhnghiep = bool.Parse(Console.ReadLine());

        XEOTO xeoto1 = new XEOTO(ngaysanxuat, biensoxe, chongoi, doanhnghiep);
        xeoto.Add(xeoto1);
        Console.WriteLine("Them xe o to thanh cong!");
    }

    static void themxetai()
    {
        Console.Write("Nhap ngay san xuat (yyyy-MM-dd): ");
        DateTime ngaysanxuat = DateTime.Parse(Console.ReadLine());
        Console.Write("Nhap bien so xe (vd: 30C3-12345): ");
        string biensoxe = Console.ReadLine();
        Console.Write("Nhap trong tai (tan): ");
        double trongtai = double.Parse(Console.ReadLine());

        XETAI xetai1 = new XETAI(ngaysanxuat, biensoxe, trongtai);
        xetai.Add(xetai1);
        Console.WriteLine("Them xe tai thanh cong!");
    }

    static void xuattatcaxe()
    {
        Console.WriteLine("===== Danh sach xe o to =====");
        foreach (var xeoto1 in xeoto)
        {
            Console.WriteLine(xeoto1.ToString());
        }

        Console.WriteLine("===== Danh sach xe tai =====");
        foreach (var xetai1 in xetai)
        {
            Console.WriteLine(xetai1.ToString());
        }
    }

    static void xecochongoilonnhat()
    {
        var sochongoi = xeoto.OrderByDescending(c => c.CHONGOI).FirstOrDefault();
        if (sochongoi != null)
        {
            Console.WriteLine($"Xe o to co so cho ngoi nhieu nhat: {sochongoi.ToString()}");
        }
        else
        {
            Console.WriteLine("Khong co xe o to nao trong danh sach.");
        }
    }

    static void xapsepxetaitheotrongtai()
    {
        var sotrongtai = xetai.OrderBy(t => t.TRONGTAI).ToList();
        Console.WriteLine("===== Danh sach xe tai sap xep theo trong tai tang dan =====");
        foreach (var xetai1 in sotrongtai)
        {
            Console.WriteLine(xetai1.ToString());
        }
    }

    static void xuatbiensoxedep()
    {
        var biensoxedep = xeoto.Where(c => timbiensoxedep(c.BIENSOXE)).Select(c => c.BIENSOXE).ToList();
        var biensoxedep1 = xetai.Where(b => timbiensoxedep(b.BIENSOXE)).Select(b => b.BIENSOXE).ToList();

        Console.WriteLine("===== Danh sach cac bien so xe đep =====");
        foreach (var biensoxe in biensoxedep.Concat(biensoxedep1))
        {
            Console.WriteLine(biensoxe);
        }
    }

    static void sotiendangkiemtungxehientai()
    {

        foreach (var xeoto1 in xeoto)
        {
            int khoanthoigiandangkiem = xeoto1.thoigiankiemtra();
            double phikiemtra = xeoto1.sotiendangkiemtungxehientai();


            Console.WriteLine($"Bien so: {xeoto1.BIENSOXE}, Thoi gian đang kiem: {khoanthoigiandangkiem} thang, Phi đang kiem: {phikiemtra} VND");
        }

        foreach (var xetai1 in xetai)
        {
            int khoanthoigiandangkiem = xetai1.thoigiankiemtra();
            double phikiemtra = xetai1.sotiendangkiemtungxehientai();


            Console.WriteLine($"Bien so: {xetai1.BIENSOXE}, Thoi gian đang kiem: {khoanthoigiandangkiem} thang, Phi đang kiem: {phikiemtra} VND");
        }

    }

    static void sothoigiandangkiemtungxesaptoi()
    {
        foreach (var phuongtien in xeoto.Concat<phuongtien>(xetai))
        {
            int thoigiankiemtra = phuongtien.thoigiankiemtra();
            DateTime ngaySx = phuongtien.ngaykiemtra;
            DateTime ngayHienTai = DateTime.Now;

            while (ngaySx < ngayHienTai)
            {
                ngaySx = ngaySx.AddMonths(thoigiankiemtra);
            }

            Console.WriteLine($"Xe co bien so {phuongtien.BIENSOXE}, Thoi gian đang kiem tiep theo: {ngaySx:yyyy-MM-dd}");
        }
    }

    static void tongsotiendadangkiem()
    {
        double tongphidangkiem = 0;

        foreach (var xeoto1 in xeoto)
        {
            int thoigiankiemtra = xeoto1.thoigiankiemtraTHDB();
            double phidangkiemtra = xeoto1.sotiendangkiemtungxehientai();

            double tienDangKiem = 0;
            DateTime ngaySx = xeoto1.ngaykiemtra;
            DateTime ngayHienTai = DateTime.Now;
            int dem = 0,dem1=0;

            while (ngaySx < ngayHienTai)
            {
                if (thoigiankiemtra > 13) 
                {
                    ++dem1;
                }
                    else
                    {
                        ++dem;
                    }
                if (dem > 7)
                {
                    thoigiankiemtra = 6;
                }
                if (dem1 > 4)
                {
                    thoigiankiemtra = 6;
                }
                if (ngaySx != xeoto1.ngaykiemtra)
                {
                    tienDangKiem += phidangkiemtra;
                }
                ngaySx = ngaySx.AddMonths(thoigiankiemtra);

            }
            Console.WriteLine($"Xe o to co bien so {xeoto1.BIENSOXE} , Tong phi đang kiem: {tienDangKiem}");
            tongphidangkiem += tienDangKiem;
        }

        foreach (var xetai1 in xetai)
        {
            int thoigiankiemtra = 6;
            double phidangkiemtra = xetai1.sotiendangkiemtungxehientai();
            int dem2 = 0;

            double tienDangKiem = 0;
            DateTime ngaySx = xetai1.ngaykiemtra;
            DateTime ngayHienTai = DateTime.Now;

            while (ngaySx < ngayHienTai)
            {
                if (thoigiankiemtra > 4)
                {
                    ++dem2;
                }
                if (dem2 > 40)
                {
                    thoigiankiemtra = 3;
                }
                if (ngaySx != xetai1.ngaykiemtra)
                {
                    tienDangKiem += phidangkiemtra;
                }
                ngaySx = ngaySx.AddMonths(thoigiankiemtra);
            }
            Console.WriteLine($"Xe tai co bien so {xetai1.BIENSOXE}, Tong phi đang kiem: {tienDangKiem}");
            tongphidangkiem += tienDangKiem;
        }

        Console.WriteLine($"Tong so tien đã đăng kiem: {tongphidangkiem} VND");
    }

    static bool timbiensoxedep(string biensoxe)
    {
        string namsocuoi = biensoxe.Substring(5, 5);
        return namsocuoi.Distinct().Count() <= 2;
    }
}
class phuongtien
{
    public DateTime ngaykiemtra { get; }
    public string BIENSOXE { get; }

    public phuongtien(DateTime ngaysanxuat, string biensoxe)
    {
        ngaykiemtra = ngaysanxuat;
        BIENSOXE = biensoxe;
    }

    public virtual int thoigiankiemtra()
    {
        return 6;
    }

    public override string ToString()
    {
        return $"Bien so: {BIENSOXE}, Ngay san xuat: {ngaykiemtra.ToString("yyyy-MM-dd")}";
    }
}
class XEOTO : phuongtien
{
    public int CHONGOI { get; }
    public bool ladoanhnghiep { get; }

    public XEOTO(DateTime ngaysanxuat, string biensoxe, int chongoi, bool doanhnghiep)
        : base(ngaysanxuat, biensoxe)
    {
        CHONGOI = chongoi;
        ladoanhnghiep = doanhnghiep;
    }

    public override int thoigiankiemtra()
    {
        int tuoicuaxe = DateTime.Now.Year - ngaykiemtra.Year;
        int khoanthoigiandangkiem = 6;

        if (tuoicuaxe <= 7)
        {
            if (CHONGOI <= 9)
            {
                khoanthoigiandangkiem = ladoanhnghiep ? 12 : 24;
            }
            else
            {
                khoanthoigiandangkiem = 12;
            }
        }

        return khoanthoigiandangkiem;
    }

    public double sotiendangkiemtungxehientai()
    {
        double phidangkiem = CHONGOI <= 10 ? 240000 : 320000;
        return phidangkiem;
    }

    public override string ToString()
    {
        return base.ToString() + $", So cho: {CHONGOI}, Đang ky kinh doanh: {ladoanhnghiep}";
    }
}
class XETAI : phuongtien
{
    public double TRONGTAI { get; }

    public XETAI(DateTime ngaysanxuat, string biensoxe, double trongtai) : base(ngaysanxuat, biensoxe)
    {
        TRONGTAI = trongtai;
    }

    public override int thoigiankiemtra()
    {
        int tuoicuaxe = DateTime.Now.Year - ngaykiemtra.Year;
        int khoanthoigiandangkiem = tuoicuaxe <= 20 ? 6 : 3;
        return khoanthoigiandangkiem;
    }

    public double sotiendangkiemtungxehientai()
    {
        double phidangkiem;
        if (TRONGTAI > 20)
        {
            phidangkiem = 560000;
        }
        else if (TRONGTAI >= 7)
        {
            phidangkiem = 350000;
        }
        else
        {
            phidangkiem = 320000;
        }
        return phidangkiem;
    }

    public override string ToString()
    {
        return base.ToString() + $", Trong tai: {TRONGTAI} tan";
    }
}
