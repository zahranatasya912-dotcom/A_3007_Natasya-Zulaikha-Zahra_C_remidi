using System;
using System.Collections.Generic;

namespace PTKwekKwekSejahtera
{
   
    public class KaryawanBebek
    {
        public string IdBebek { get; set; }
        public string NamaBebek { get; set; }
        public int JumlahTugas { get; set; }

        public KaryawanBebek(string idBebek, string namaBebek, int jumlahTugas)
        {
            IdBebek = idBebek;
            NamaBebek = namaBebek;
            JumlahTugas = jumlahTugas;
        }

        public void TampilInfo()
        {
            Console.WriteLine($"ID Bebek             : {IdBebek}");
            Console.WriteLine($"Nama Bebek           : {NamaBebek}");
            Console.WriteLine($"Tugas Diterima       : {JumlahTugas} tugas");
            Console.WriteLine("--------------------------------------------------");
        }
    }

   
    public abstract class Pekerjaan
    {
        
        private string idPekerjaan;
        private string namaPekerjaan;
        private int stokTugas;

        
        public string IdPekerjaan
        {
            get { return idPekerjaan; }
            set { idPekerjaan = value; }
        }

        public string NamaPekerjaan
        {
            get { return namaPekerjaan; }
            set { namaPekerjaan = value; }
        }

        public int StokTugas
        {
            get { return stokTugas; }
            set
            {
                
                if (value >= 0)
                {
                    stokTugas = value;
                }
                else
                {
                    Console.WriteLine("Peringatan: Stok tugas tidak boleh negatif!");
                }
            }
        }

        
        protected Pekerjaan(string idPekerjaan, string namaPekerjaan, int stokTugas)
        {
            IdPekerjaan = idPekerjaan;
            NamaPekerjaan = namaPekerjaan;
            StokTugas = stokTugas;
        }

        
        public void KurangiStok(int jumlah)
        {
            if (jumlah > 0 && jumlah <= stokTugas)
            {
                stokTugas -= jumlah;
            }
            else
            {
                Console.WriteLine("Pengurangan stok gagal: Jumlah tidak valid atau melebihi stok tugas tersedia.");
            }
        }

        
        public abstract double HitungBiaya(int jumlah);
    }

    public class PekerjaanMagang : Pekerjaan
    {
        public PekerjaanMagang(string idPekerjaan, string namaPekerjaan, int stokTugas)
            : base(idPekerjaan, namaPekerjaan, stokTugas)
        {
        }

        public override double HitungBiaya(int jumlah)
        {
            return jumlah * 10000.0;
        }
    }

   
    public class PekerjaanSenior : Pekerjaan
    {
        public string JenisKeahlian { get; set; }

        public PekerjaanSenior(string idPekerjaan, string namaPekerjaan, int stokTugas, string jenisKeahlian)
            : base(idPekerjaan, namaPekerjaan, stokTugas)
        {
            JenisKeahlian = jenisKeahlian;
        }

        
        public override double HitungBiaya(int jumlah)
        {
            return jumlah * 50000.0;
        }
    }

    
    public interface DapatDitugaskan
    {
        void BagikanTugas();
    }

    public class DistribusiTugas : DapatDitugaskan
    {
        public KaryawanBebek KaryawanPenerima { get; set; }
        public Pekerjaan PekerjaanDibagikan { get; set; }
        public int JumlahKirim { get; set; }

        public DistribusiTugas(KaryawanBebek karyawanPenerima, Pekerjaan pekerjaanDibagikan, int jumlahKirim)
        {
            KaryawanPenerima = karyawanPenerima;
            PekerjaanDibagikan = pekerjaanDibagikan;
            JumlahKirim = jumlahKirim;
        }

        public void BagikanTugas()
        {
            Console.WriteLine($"--- Memproses Pembagian Tugas ke Bebek {KaryawanPenerima.NamaBebek} ---");

            if (JumlahKirim > 0 && JumlahKirim <= PekerjaanDibagikan.StokTugas)
            {

                PekerjaanDibagikan.KurangiStok(JumlahKirim);
                KaryawanPenerima.JumlahTugas += JumlahKirim;

                Console.WriteLine($"KWEK! Sebanyak {JumlahKirim} tugas berhasil diberikan kepada Bebek {KaryawanPenerima.NamaBebek}. Selamat bekerja, deadline menanti!");
            }
            else
            {

                Console.WriteLine("Pembagian Tugas GAGAL!");
                if (JumlahKirim <= 0)
                {
                    Console.WriteLine("Alasan: Jumlah tugas yang dibagikan harus lebih besar dari 0.");
                }
                else
                {
                    Console.WriteLine($"Alasan: Jumlah permintaan tugas ({JumlahKirim}) melebihi sisa stok tugas perusahaan ({PekerjaanDibagikan.StokTugas}).");
                }
            }
            Console.WriteLine("--------------------------------------------------");
        }
    }

    public class RuangMeeting
    {
        public string IdRuangan { get; set; }
        public string NamaRuangan { get; set; }
        public int KapasitasBebek { get; set; }

        public RuangMeeting(string idRuangan, string namaRuangan, int kapasitasBebek)
        {
            IdRuangan = idRuangan;
            NamaRuangan = namaRuangan;
            KapasitasBebek = kapasitasBebek;
        }

        public void TampilInfo()
        {
            Console.WriteLine($"ID Ruangan     : {IdRuangan}");
            Console.WriteLine($"Nama Ruangan   : {NamaRuangan}");
            Console.WriteLine($"Kapasitas Bebek: {KapasitasBebek} ekor");
            Console.WriteLine("--------------------------------------------------");
        }
    }

  
    public class KantorBebek
    {
        private List<RuangMeeting> daftarRuangan = new List<RuangMeeting>();

        public void TambahRuangan(string idRuangan, string namaRuangan, int kapasitasBebek)
        {
            
            RuangMeeting ruanganBaru = new RuangMeeting(idRuangan, namaRuangan, kapasitasBebek);
            daftarRuangan.Add(ruanganBaru);
        }

        public void TampilkanRuangan()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("       DAFTAR RUANG MEETING PT KWEK KWEK SEJAHTERA");
            Console.WriteLine("==================================================");
            foreach (var r in daftarRuangan)
            {
                r.TampilInfo();
            }
        }

        public int HitungTotalKapasitas()
        {
            int totalKapasitas = 0;
            foreach (var r in daftarRuangan)
            {
                totalKapasitas += r.KapasitasBebek;
            }
            return totalKapasitas;
        }
    }

  
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== SISTEM MANAJEMEN PT KWEK KWEK SEJAHTERA ===\n");

            
            KaryawanBebek bebek1 = new KaryawanBebek("BBK-001", "Bebek Oyen (CEO)", 5);
            KaryawanBebek bebek2 = new KaryawanBebek("BBK-002", "Bebek Blorok", 2);

            Pekerjaan pekerjaan1 = new PekerjaanMagang("PKJ-001", "Fotokopi & Bikin Kopi Atasan", 50);
            Pekerjaan pekerjaan2 = new PekerjaanSenior("PKJ-002", "Pengembangan Sistem AI Kwek", 20, "Programming");

            Console.WriteLine("[DEMONSTRASI POLYMORPHISM]");
            Pekerjaan[] kumpulanPekerjaan = { pekerjaan1, pekerjaan2 };
            foreach (var p in kumpulanPekerjaan)
            {
                double biaya10Tugas = p.HitungBiaya(10);
                Console.WriteLine($"Jenis Pekerjaan: {p.NamaPekerjaan} | Biaya untuk 10 tugas: Rp{biaya10Tugas:N0}");
            }
            Console.WriteLine();

            Console.WriteLine("[DEMONSTRASI PEMBAGIAN TUGAS]");

            DistribusiTugas distribusi1 = new DistribusiTugas(bebek1, pekerjaan1, 15);
            distribusi1.BagikanTugas();

            DistribusiTugas distribusi2 = new DistribusiTugas(bebek2, pekerjaan2, 30);
            distribusi2.BagikanTugas();

            KantorBebek kantor = new KantorBebek();
            kantor.TambahRuangan("RM-01", "Ruang Rapat Evaluasi Pagi CEO", 25);
            kantor.TambahRuangan("RM-02", "Ruang Santai & Ngedate Bebek", 15);

            kantor.TampilkanRuangan();

            int totalKapasitas = kantor.HitungTotalKapasitas();
            Console.WriteLine($"Total Kapasitas Rapat Seluruh Ruangan: {totalKapasitas} ekor bebek");
            Console.WriteLine("==================================================");

            Console.ReadKey();
        }
    }
}