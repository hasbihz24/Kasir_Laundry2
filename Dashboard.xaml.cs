using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kasir_Laundry2
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        MYDB db = new MYDB();
        public static Dashboard instance;
        public Dashboard()
        {
            InitializeComponent();
            instance = this;
            MYDB db = new MYDB();
            int row;
            int i = 0;
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(
                "select * from `pesanan`", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            row = table.Rows.Count;
            int[] ID_PEL = new int[10];
            int[] ID_PESAN = new int[10];
            string[] nama = new string[10];
            string[] alamat = new string[10];
            string[] noTelp = new string[10];
            int[] jumlahB = new int[10];
            char[] Bc = new char[10];
            char[] selimut = new char[10];
            int[] ID_PAKET = new int[10];
            int[] harga = new int[10];
            string[] tgl = new string[10];
            string[] status = new string[10];


            string que = "SELECT pelanggan.ID_pelanggan, pelanggan.Nama, pelanggan.Alamat, pelanggan.No_Telp, pesanan.ID_Pesanan, pesanan.Jumlah_Baju, pesanan.Bed_Cover, pesanan.Selimut, pesanan.Tgl_Pemesanan, pesanan.ID_Paket, pesanan.Harga, pesanan.Status FROM `pelanggan` LEFT JOIN `pesanan` ON pelanggan.ID_pelanggan = pesanan.ID_pelanggan";
            MySqlCommand comand2 = new MySqlCommand(que, db.getConnection());
            db.openConnection();
            MySqlDataReader reader = comand2.ExecuteReader();
            while (reader.Read())
            {
                ID_PEL[i] = reader.GetInt32(0);
                nama[i] = reader.GetString(1);
                alamat[i] = reader.GetString(2);
                noTelp[i] = reader.GetString(3);
                ID_PESAN[i] = reader.GetInt32(4);
                jumlahB[i] = reader.GetInt32(5);
                Bc[i] = reader.GetChar(6);
                selimut[i] = reader.GetChar(7);
                tgl[i] = reader.GetString(8);
                ID_PAKET[i] = reader.GetInt32(9);
                harga[i] = reader.GetInt32(10);
                status[i] = reader.GetString(11);
                i++;


            }

            List<Data_pesanan> users = new List<Data_pesanan>();
            for (i = 0; i < row; i++)
            {
                users.Add(new Data_pesanan()
                {
                    id_pesan = ID_PESAN[i],
                    nama = nama[i],
                    no_telp = noTelp[i],
                    alamat = alamat[i],
                    Harga = harga[i],
                    Status = status[i],
                    tgl_pesan = tgl[i]
                });
            }
            listPesan.ItemsSource = users;


        }

        public void listPesan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListView)sender;
            var country = (Data_pesanan)item.SelectedItem;
            MYDB db = new MYDB();
            int row;
            int i = 0;
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(
                "select * from `pesanan`", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            row = table.Rows.Count;
            int ID_PEL = 0;
            int ID_PESAN = 0;
            string nama = country.nama;
            string alamat = "";
            string noTelp = "";
            int jumlahB = 0;
            char Bc = ' ';
            char selimut = ' ';
            int ID_PAKET = 0;
            int harga = 0;
            string tgl = "";
            string status = "";


            MySqlCommand comand2 = new MySqlCommand("SELECT pelanggan.ID_pelanggan, pelanggan.Nama, pelanggan.Alamat, pelanggan.No_Telp, pesanan.ID_Pesanan, pesanan.Jumlah_Baju, pesanan.Bed_Cover, pesanan.Selimut, pesanan.Tgl_Pemesanan, pesanan.ID_Paket, pesanan.Harga, pesanan.Status FROM `pelanggan` LEFT JOIN `pesanan` ON pelanggan.ID_pelanggan = pesanan.ID_pelanggan WHERE pelanggan.Nama LIKE @nama", db.getConnection());
            comand2.Parameters.Add("@nama", MySqlDbType.VarChar).Value = nama;
            db.openConnection();
            MySqlDataReader reader = comand2.ExecuteReader();
            while (reader.Read())
            {
                ID_PEL = reader.GetInt32(0);
                nama = reader.GetString(1);
                alamat = reader.GetString(2);
                noTelp = reader.GetString(3);
                ID_PESAN = reader.GetInt32(4);
                jumlahB = reader.GetInt32(5);
                Bc = reader.GetChar(6);
                selimut = reader.GetChar(7);
                tgl = reader.GetString(8);
                ID_PAKET = reader.GetInt32(9);
                harga = reader.GetInt32(10);
                status = reader.GetString(11);
                
                i++;


            }
            txtkg.Content = jumlahB.ToString();
            txtpaket.Content = ID_PAKET.ToString();
            txtselimut.Content = selimut.ToString();
            txttotal.Content = harga.ToString();
            tbed.Content = Bc.ToString();
            txtid_pesan.Content = ID_PESAN.ToString();
            ID_Pel.Text = ID_PEL.ToString();
            txtNama.Text = nama;
            txtAlamat.Text = alamat;          
            txtNo_Telp.Text = noTelp.ToString();
            statustxt.Text = status.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pesan pesan = new Pesan();
            Visibility = Visibility.Hidden;
            pesan.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ID_Pel.Text == "" || txtNama.Text == "" || txtAlamat.Text == "" || txtNo_Telp.Text == "")
            {
                MessageBox.Show("Empty Field Detected",
                   "Empty Field", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int id_pel = Int32.Parse(ID_Pel.Text);
                string nama = txtNama.Text;
                string alamat = txtAlamat.Text;
                string telp = txtNo_Telp.Text;
                string status = statustxt.Text;
                
                edit_data edit_Data = new edit_data(nama, status);
                Visibility = Visibility.Hidden;
                edit_Data.Show();
                
            }
        }

        public void passingData(string Nama, string Status)
        {
            Nama = txtNama.Text;
            Status = statustxt.Text;
        }


       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int id_pesan = Int32.Parse(txtid_pesan.Content.ToString());
            DataTable dt2 = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand comand = new MySqlCommand(
            "UPDATE `pesanan` SET  `Status` = 'Selesai' WHERE `pesanan`.`ID_Pesanan` = @id_pesan", db.getConnection());
            comand.Parameters.Add("@id_pesan", MySqlDbType.Int32).Value = id_pesan;
            adapter.SelectCommand = comand;
            adapter.Fill(dt2);
            this.Close();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
            Pesan pesan = new Pesan();
            pesan.Close();
            MainWindow mw = new MainWindow();
            mw.Close();
            Registrasi registrasi = new Registrasi();
            registrasi.Close();

        }

        
    }

    public class Data_pesanan
    {
        public int id_pesan { get; set; }
        public string nama { get; set; }
        public string no_telp { get; set; }
        public string alamat { get; set; }
        public int Harga { get; set; }
        public string Status { get; set; }
        public string tgl_pesan { get; set; }


    }
    public class data_Passing
    {
        public string Nama { get; set; }
        public string Status { get; set; }

    }

    
}
