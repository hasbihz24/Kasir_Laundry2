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
    /// Interaction logic for Pesan.xaml
    /// </summary>
    public partial class Pesan : Window
    {
        MYDB db = new MYDB();
        public Pesan()
        {
            InitializeComponent();
        }

        private void btnTambah_Click(object sender, RoutedEventArgs e)
        {
            int nPaket =0;
            int paket = 0;
            string pakettxt = "";
            if (rbKilat.IsChecked == true)
            {
                paket = 3;
                nPaket = 20000;
                pakettxt = "Kilat";

            }else if(rbEks.IsChecked == true)
            {
                paket = 2;
                nPaket = 12000;
                pakettxt = "Ekspress";
            }
            else if(rbReguler.IsChecked == true)
            {
                paket=1;
                nPaket = 6000;
                pakettxt = "Reguler";
            }
            else
            {
                MessageBox.Show("Empty Field Detected",
                       "Empty Field", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            string selimut = "";
            int nSel = 0;
            if(rbSelY.IsChecked == true)
            {
                selimut = "Y";
                nSel = 5000;

            }else if(rbSelT.IsChecked == true)
            {
                selimut = "T";
                nSel = 0;
            }
            else
            {
                MessageBox.Show("Empty Field Detected",
                       "Empty Field", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            string bed_cover = "";
            int nBed_cover = 0;
            if(rbBCY.IsChecked == true)
            {
                bed_cover = "Y";
                nBed_cover = 6000;
            }else if(rbBCT.IsChecked == true)
            {
                bed_cover = "T";
                nBed_cover=0;
            }
            else
            {
                MessageBox.Show("Empty Field Detected",
                       "Empty Field", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            string name = txtNama.Text;
            string alamat = txtAlamat.Text;
            string no_telp = txtTelp.Text;
            int kg = Int32.Parse(txtKg.Text);
            int total = kg * nPaket + nBed_cover + nSel;
            
            Random rnd = new Random();
            int num = rnd.Next(1000, 9999);
            int num2 = num;
            int num_id = rnd.Next(1000, 9999);

            if (name == "" || alamat == "" || no_telp == "" || total == 0 || kg == 0)
            {
                MessageBox.Show("Empty Field Detected",
                   "Empty Field", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                Input_Pelanggan(name, alamat, no_telp, num);
                Input_Pesanan(paket, kg, bed_cover, selimut, total, num2, num_id);
                MessageBox.Show("Total : " + total,
                       "Data Succesfully Add", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNama.Text = "";
                txtAlamat.Text = "";
                txtTelp.Text = "";
                txtKg.Text = "";
            }   
        }
        public void Input_Pesanan(int paket, int kg, string bed_cover, string selimut, int harga, int row, int id_pesan)
        {
            DateTime dateTime = DateTime.Now;
            string tgl = dateTime.ToString("dd/MM/yyyy");
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            MySqlCommand comand2 = new MySqlCommand(
            "INSERT INTO `pesanan` (`ID_Pesanan`, `Jumlah_Baju`, `Bed_Cover`, `Selimut`, `Tgl_Pemesanan`, `ID_Paket`, `Harga`, `Status`, `ID_pelanggan`) VALUES (@id, @kg, @bed, @selimut, @tgl, @paket, @harga, 'Dicuci', @id_pelanggan)", db.getConnection());
            comand2.Parameters.Add("@id", MySqlDbType.Int32).Value = id_pesan;
            comand2.Parameters.Add("@kg", MySqlDbType.Int32).Value = kg;
            comand2.Parameters.Add("@bed", MySqlDbType.VarChar).Value = bed_cover;
            comand2.Parameters.Add("@selimut", MySqlDbType.VarChar).Value = selimut;
            comand2.Parameters.Add("@tgl", MySqlDbType.VarChar).Value = tgl;
            comand2.Parameters.Add("@paket", MySqlDbType.Int32).Value = paket;
            comand2.Parameters.Add("@harga", MySqlDbType.Int32).Value = harga;
            comand2.Parameters.Add("@id_pelanggan", MySqlDbType.Int32).Value = row;
            adapter1.SelectCommand = comand2;
            adapter1.Fill(dt);
           

        }
        public void Input_Pelanggan(string name, string alamat, string no_telp, int row)
        {
            
            
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            MySqlCommand comand2 = new MySqlCommand(
            "INSERT INTO `pelanggan` (`ID_pelanggan`, `Nama`, `Alamat`, `No_Telp`) VALUES (@id, @nama, @alamat,  @telp)", db.getConnection());
            comand2.Parameters.Add("@id", MySqlDbType.Int32).Value = row;
            comand2.Parameters.Add("@nama", MySqlDbType.VarChar).Value = name;
            comand2.Parameters.Add("@alamat", MySqlDbType.VarChar).Value = alamat;
            comand2.Parameters.Add("@telp", MySqlDbType.VarChar).Value = no_telp;
            adapter1.SelectCommand = comand2;
            adapter1.Fill(dt);
        }

        private void txtKg_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
        public int Select()
        {
            int row;
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(
                "select * from `pesanan`", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            row = table.Rows.Count;
            return row;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dashboard pesan = new Dashboard();
            Visibility = Visibility.Hidden;
            pesan.Show();
        }
    }
}
