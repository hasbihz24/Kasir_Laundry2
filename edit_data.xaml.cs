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
    /// Interaction logic for edit_data.xaml
    /// </summary>
    public partial class edit_data : Window
    {

        MYDB db = new MYDB();

        public static edit_data instance;
        public TextBox tb1;
        public edit_data(string nama, string status)
        {
            InitializeComponent();
            instance = this;
            tb1 = txtNama;
            Dashboard dashboard = new Dashboard();
            
            int i =0;
            int ID_PEL = 0;
            int ID_PESAN = 0;
            string alamat = "";
            string noTelp = "";
            int jumlahB = 0;
            char Bc = ' ';
            char selimut = ' ';
            int ID_PAKET = 0;
            int harga = 0;
            string tgl = "";
           

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
            txtNama.Text = nama;
            txtAlamat.Text = alamat;
            txtKg.Text = jumlahB.ToString();
            txtTelp.Text = noTelp.ToString();
            id_pel.Content = ID_PEL.ToString();
            id_pesan.Content = ID_PESAN.ToString();
            tgl_pesan.Content = tgl.ToString();
            lbStatus.Content = status;
            if(ID_PAKET == 1)
            {
                rbReguler.IsChecked = true;
            }else if(ID_PAKET == 2)
            {
                rbKilat.IsChecked = true;
            }
            else
            {
                rbEks.IsChecked = true;
            }
            if (Bc == 'Y' || Bc == 'y')
            {
                rbBCY.IsChecked = true;
            }
            else
            {
                rbBCT.IsChecked = true;
            }
            if(selimut == 'Y' ||selimut == 'y')
            {
                rbSelY.IsChecked = true;
            }
            else
            {
                rbSelT.IsChecked = true;
            }
            db.closeConnection();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string name = txtNama.Text;
            string alamat = txtAlamat.Text;
            string no_telp = txtTelp.Text;
            int kg = Int32.Parse(txtKg.Text);
            int id_pelanggan = Int32.Parse(id_pel.Content.ToString());
            int id_pesanan = Int32.Parse(id_pesan.Content.ToString());
            string tgl_pesanan = tgl_pesan.Content.ToString();
            string status = lbStatus.Content.ToString(); 
            
            if (name == "" || alamat == "" || no_telp == "" ||  kg == 0)
            {
                MessageBox.Show("Empty Field Detected",
                   "Empty Field", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                MessageBoxResult m = MessageBox.Show("Yakin ingin mendelete data ?",
                  "Data Deleted", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (m == MessageBoxResult.Yes)
                {
                    delpesan(id_pesanan);
                    delpelanggan(id_pelanggan);
                    MessageBox.Show("Data Deleted",
                       "Data Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    Dashboard dashboard = new Dashboard();
                    Visibility = Visibility.Hidden;
                    dashboard.Show();
                }
                
            }
        }

        private void btnTambah_Click(object sender, RoutedEventArgs e)
        {
            int nPaket = 0;
            int paket = 0;
            string pakettxt = "";
            if (rbKilat.IsChecked == true)
            {
                paket = 3;
                nPaket = 20000;
                pakettxt = "Kilat";

            }
            else if (rbEks.IsChecked == true)
            {
                paket = 2;
                nPaket = 12000;
                pakettxt = "Ekspress";
            }
            else if (rbReguler.IsChecked == true)
            {
                paket = 1;
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
            if (rbSelY.IsChecked == true)
            {
                selimut = "Y";
                nSel = 5000;

            }
            else if (rbSelT.IsChecked == true)
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
            if (rbBCY.IsChecked == true)
            {
                bed_cover = "Y";
                nBed_cover = 6000;
            }
            else if (rbBCT.IsChecked == true)
            {
                bed_cover = "T";
                nBed_cover = 0;
            }
            else
            {
                MessageBox.Show("Empty Field Detected",
                       "Empty Field", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            string name = txtNama.Text;
            string alamat = txtAlamat.Text;
            string no_telp =txtTelp.Text;
            int kg = Int32.Parse(txtKg.Text);
            int id_pelanggan = Int32.Parse(id_pel.Content.ToString());
            int id_pesanan = Int32.Parse(id_pesan.Content.ToString());
            string tgl_pesanan = tgl_pesan.Content.ToString();
            int total = kg * nPaket + nBed_cover + nSel;
            string status = lbStatus.Content.ToString();

            if (name == "" || alamat == "" || no_telp == "" || total == 0 || kg == 0)
            {
                MessageBox.Show("Empty Field Detected",
                   "Empty Field", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                delpesan(id_pesanan);
                delpelanggan(id_pelanggan);
                Input_Pelanggan(id_pelanggan, name, alamat, no_telp);
                Input_Pesanan(paket, kg, bed_cover, selimut, total, id_pesanan, tgl_pesanan, status, id_pelanggan);
               
                MessageBox.Show("Total : " + total,
                       "Data Succesfully Add", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNama.Text = "";
                txtAlamat.Text = "";
                txtTelp.Text = "";
                txtKg.Text = "";
                Dashboard dashboard = new Dashboard();
                Visibility = Visibility.Hidden;
                dashboard.Show();
            }
            
        }
        public void Input_Pesanan(int paket, int kg, string bed_cover, string selimut, int harga, int id_pesan, string tgl, string status, int id_pelanggan)
        {
           

            
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            MySqlCommand comand2 = new MySqlCommand(
            "INSERT INTO `pesanan` (`ID_Pesanan`, `Jumlah_Baju`, `Bed_Cover`, `Selimut`, `Tgl_Pemesanan`, `ID_Paket`, `Harga`, `Status`, `ID_pelanggan`) VALUES (@id, @kg, @bed, @selimut, @tgl, @paket, @harga, @status, @id_pelanggan)", db.getConnection());
            comand2.Parameters.Add("@id", MySqlDbType.Int32).Value = id_pesan;
            comand2.Parameters.Add("@kg", MySqlDbType.Int32).Value = kg;
            comand2.Parameters.Add("@bed", MySqlDbType.VarChar).Value = bed_cover;
            comand2.Parameters.Add("@selimut", MySqlDbType.VarChar).Value = selimut;
            comand2.Parameters.Add("@tgl", MySqlDbType.VarChar).Value = tgl;
            comand2.Parameters.Add("@paket", MySqlDbType.Int32).Value = paket;
            comand2.Parameters.Add("@harga", MySqlDbType.Int32).Value = harga;
            comand2.Parameters.Add("@status", MySqlDbType.VarChar).Value = status;
            comand2.Parameters.Add("@id_pelanggan", MySqlDbType.Int32).Value = id_pelanggan;
            adapter1.SelectCommand = comand2;
            adapter1.Fill(dt);


        }
        public void Input_Pelanggan(int id_pel, string name, string alamat, string no_telp)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            MySqlCommand comand2 = new MySqlCommand(
            "INSERT INTO `pelanggan` (`ID_pelanggan`, `Nama`, `Alamat`, `No_Telp`) VALUES (@id, @nama, @alamat, @telp)", db.getConnection());
            comand2.Parameters.Add("@id", MySqlDbType.Int32).Value = id_pel;
            comand2.Parameters.Add("@nama", MySqlDbType.VarChar).Value = name;
            comand2.Parameters.Add("@alamat", MySqlDbType.VarChar).Value = alamat;
            comand2.Parameters.Add("@telp", MySqlDbType.VarChar).Value = no_telp;
            adapter1.SelectCommand = comand2;
            adapter1.Fill(dt);
        }
        public void delpelanggan(int id_pel)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            MySqlCommand comand2 = new MySqlCommand(
            "DELETE FROM pelanggan WHERE `pelanggan`.`ID_pelanggan` = @id_pel", db.getConnection());
            comand2.Parameters.Add("@id_pel", MySqlDbType.Int32).Value = id_pel;
            adapter1.SelectCommand = comand2;
            adapter1.Fill(dt);
        }
        public void delpesan(int id_pesan)
        {
            DataTable dt2 = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand comand = new MySqlCommand(
            "DELETE FROM pesanan WHERE `pesanan`.`ID_Pesanan` = @id_pes", db.getConnection());
            comand.Parameters.Add("@id_pes", MySqlDbType.Int32).Value = id_pesan;
            adapter.SelectCommand = comand;
            adapter.Fill(dt2);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            this.Close();
            dashboard.Show();
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
    }
}
