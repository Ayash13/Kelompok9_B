using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace database_ktp{

    class Program
    {
        private const string connString = "data source=localhost;database=ktp;user ID=pengguna1;password=P4ssword1";

        static void Main(string[] args)
        {
            int choice = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Lihat data Penduduk");
                Console.WriteLine("2. Tambah data Penduduk");
                Console.WriteLine("3. Ubah data Penduduk");
                Console.WriteLine("4. Hapus data Penduduk");
                Console.WriteLine("0. Keluar");
                Console.Write("Pilih menu: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ReadKTP();
                        break;
                    case 2:
                        InseertKTP();
                        break;
                    case 3:
                        UpdatePenduduk();
                        break;
                    case 4:
                        DeletePenduduk();
                        break;
                    case 0:
                        Console.WriteLine("Keluar dari program...");
                        break;
                    default:
                        Console.WriteLine("Menu tidak valid. Silakan coba lagi.");
                        break;
                }

                Console.WriteLine();
                Console.Write("Tekan sembarang tombol untuk melanjutkan...");
                Console.ReadKey();

            } while (choice != 0);
        }

        private static void ReadKTP()
        {
            Console.WriteLine("Data Penduduk:");
            Console.WriteLine("--------------------------------------------------------");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM Penduduk";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("{0,-10}{1,-50}{2,-50}{3,-50}{4,-20}", "ID Penduduk", "Nama Lengkap", "Tempat Lahir", "Tanggal Lahir", "Jenis Kelamin", "Alamat", "No Telp");
                            while (reader.Read())
                            {
                                Console.WriteLine("{0,-10}{1,-50}{2,-50}{3,-50}{4,-20}", reader["ID_Penduduk"], reader["Nama_Lengkap"], reader["Tempat_lahir"], reader["Tanggal_Lahir"], reader["Jenis_Kelamin"], reader["Alamat"], reader["No_Telp"]);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Terjadi kesalahan: " + ex.Message + " (Error Code: " + ex.Number + ")");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Terjadi kesalahan: " + ex.Message);
                    }
                }
            }
        }

        private static void InseertKTP()
        {
            Console.WriteLine("Tambah Data Penduduk:");
            Console.WriteLine("--------------------------------------------------------");

            Console.Write("ID Penduduk: ");
            string idPen = Console.ReadLine();

            Console.Write("Nama Lengkap: ");
            string Nama_Lengkap = Console.ReadLine();

            Console.Write("Tempat Lahir: ");
            string Tempat_Lahir = Console.ReadLine();

            Console.Write("Tanggal Lahir (yyyy-mm-dd): ");
            DateTime Tanggal_Lahir = DateTime.Parse(Console.ReadLine());

            Console.Write("Jenis Kelamin (L/P): ");
            string Jenis_Kelamin = Console.ReadLine();

            Console.Write("Alamat: ");
            string Alamat = Console.ReadLine();

            Console.Write("No Telp: ");
            string No_Telp = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Penduduk (ID_Penduduk, Nama_Lengkap, Tempat_Lahir, Tanggal_Lahir, Jenis_Kelamin, Alamat, No_Telp) VALUES (@idPen, @Nama_Lengkap, @Tempat_Lahir, @Tanggal_Lahir, @Jenis_Kelamin, @Alamat, @No_Telp )";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPen", idPen);
                    cmd.Parameters.AddWithValue("@Nama_Lengkap", Nama_Lengkap);
                    cmd.Parameters.AddWithValue("@Tempat_Lahir", Tempat_Lahir);
                    cmd.Parameters.AddWithValue("@Tanggal_Lahir", Tanggal_Lahir);
                    cmd.Parameters.AddWithValue("@Jenis_Kelamin", Jenis_Kelamin);
                    cmd.Parameters.AddWithValue("@Alamat", Alamat);
                    cmd.Parameters.AddWithValue("@No_Telp", No_Telp);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine("{0} baris berhasil ditambahkan.", rowsAffected);
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Terjadi kesalahan: " + ex.Message + " (Error Code: " + ex.Number + ")");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Terjadi kesalahan: " + ex.Message);
                    }
                }
            }
        }

        private static void UpdatePenduduk()
        {
            Console.WriteLine("Ubah Data Penduduk:");
            Console.WriteLine("--------------------------------------------------------");

            Console.Write("Masukkan ID Penduduk yang akan diubah: ");
            string idPen = Console.ReadLine();

            Console.Write("Nama baru (kosongkan jika tidak ingin mengubah): ");
            string Nama = Console.ReadLine();

            Console.Write("Tempat Lahir baru (kosongkan jika tidak ingin mengubah): ");
            string Tempat = Console.ReadLine();

            Console.Write("Tanggal Lahir baru (yyyy-mm-dd) (kosongkan jika tidak ingin mengubah): ");
            string Tanggal = Console.ReadLine();

            Console.Write("Jenis Kelamin baru (kosongkan jika tidak ingin mengubah): ");
            string JK = Console.ReadLine();

            Console.Write("Alamat baru(kosongkan jika tidak ingin mengubah): ");
            string Alamat = Console.ReadLine();

            Console.Write("No Telp baru (kosongkan jika tidak ingin mengubah): ");
            string No_telp = Console.ReadLine();

            DateTime? tanggal = null;
            if (!string.IsNullOrEmpty(Tanggal))
            {
                tanggal = DateTime.Parse(Tanggal);
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE Penduduk SET";
                List<string> updateFields = new List<string>();
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(idPen))
                {
                    updateFields.Add("ID_Penduduk = @ID_Penduduk");
                    parameters.Add(new SqlParameter("@ID_Penduduk", idPen));
                }

                if (!string.IsNullOrEmpty(Nama))
                {
                    updateFields.Add("Nama_Lengkap = @Nama_Lengkap");
                    parameters.Add(new SqlParameter("@Nama_Lengkap", Nama));
                }

                if (!string.IsNullOrEmpty(Tempat))
                {
                    updateFields.Add("Tempat_Lahir = @Tempat_Lahir");
                    parameters.Add(new SqlParameter("@Tempat_Lahir", Tempat));
                }

                if (Tanggal != null)
                {
                    updateFields.Add("Tanggal_Lahir = @Tanggal_Lahir");
                    parameters.Add(new SqlParameter("@Tanggal_Lahir", Tanggal));
                }

                if (!string.IsNullOrEmpty(JK))
                {
                    updateFields.Add("Jenis_Kelamin = @Jenis_Kelamin");
                    parameters.Add(new SqlParameter("@Jenis_Kelamin", JK));
                }

                if (!string.IsNullOrEmpty(Alamat))
                {
                    updateFields.Add("Alamat = @Alamat");
                    parameters.Add(new SqlParameter("@Alamat", Alamat));
                }

                if (!string.IsNullOrEmpty(No_telp))
                {
                    updateFields.Add("No_Telp = @No_Telp");
                    parameters.Add(new SqlParameter("@No_Telp", No_telp));
                }

                if (updateFields.Count == 0)
                {
                    Console.WriteLine("Tidak ada data yang diubah.");
                    return;
                }

                query += " " + string.Join(", ", updateFields.ToArray()) + " WHERE ID_Penduduk = @idPen";
                parameters.Add(new SqlParameter("@idPen", idPen));

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine("{0} baris berhasil diubah.", rowsAffected);
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Terjadi kesalahan: " + ex.Message + " (Error Code: " + ex.Number + ")");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Terjadi kesalahan: " + ex.Message);
                    }
                }
            }
        }

        private static void DeletePenduduk()
        {
            Console.WriteLine("Hapus Data Penduduk:");
            Console.WriteLine("--------------------------------------------------------");

            Console.Write("Masukkan ID Penduduk yang akan dihapus: ");
            string idPen = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM Penduduk WHERE ID_Penduduk = @ID_Penduduk";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Penduduk", idPen);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine("{0} baris berhasil dihapus.", rowsAffected);
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Terjadi kesalahan: " + ex.Message + " (Error Code: " + ex.Number + ")");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Terjadi kesalahan: " + ex.Message);
                    }
                }
            }
        }
    }
}