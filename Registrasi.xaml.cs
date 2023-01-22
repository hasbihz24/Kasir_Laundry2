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
    /// Interaction logic for Registrasi.xaml
    /// </summary>
    public partial class Registrasi : Window
    {
        MYDB db = new MYDB();
        public Registrasi()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            string username = txtUser.Text;
            string pass = txtPass.Password;
            string conPass = txtConpass.Password;
            string tgl = txtTgl.Text;
            string alamat = txtAlamat.Text;
            char jk;
            if (username == "" || pass == "" || conPass == "" || tgl == "" || alamat == "")
            {
                MessageBox.Show("Empty Field Detected",
                        "Empty Field", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else{
                if (rbLaki.IsChecked == false && rbPerem.IsChecked == false)
                {
                    MessageBox.Show("Empty Field Detected",
                        "Empty Field", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (pass == conPass)
                    {
                        if (rbLaki.IsChecked == true)
                        {
                            jk = 'L';
                            Input_Pegawai(username, pass, tgl, alamat, jk);
                            txtUser.Text = "";
                            txtPass.Password = "";
                            txtConpass.Password = "";
                            txtTgl.Text = "";
                            txtAlamat.Text = "";
                        }
                        else
                        {
                            jk = 'p';
                            Input_Pegawai(username, pass, tgl, alamat, jk);
                            txtUser.Text = "";
                            txtPass.Password = "";
                            txtConpass.Password = "";
                            txtTgl.Text = "";
                            txtAlamat.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password Incorrect",
                        "Password Incorrect", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
           
            

           

        }

        public void Input_Pegawai(string username, string pass, string tgl, string alamat, char jk)
        {
            int row;
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(
                "select * from `akun_pegawai`", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            row = table.Rows.Count;
            row++;
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            MySqlCommand comand2 = new MySqlCommand(
            "INSERT INTO `akun_pegawai` (`ID_Pegawai`, `Nama`, `Jenis_Kelamin`, `Alamat`, `Username`, `Password`, `Tgl_Lahir`) VALUES(@id, @name, @jk, @alamat, @user, @pass, @tgl)", db.getConnection());
            comand2.Parameters.Add("@id", MySqlDbType.VarChar).Value = row;
            comand2.Parameters.Add("@name", MySqlDbType.VarChar).Value = username;
            comand2.Parameters.Add("@jk", MySqlDbType.VarChar).Value = jk;
            comand2.Parameters.Add("@alamat", MySqlDbType.VarChar).Value = alamat;
            comand2.Parameters.Add("@user", MySqlDbType.VarChar).Value = username;
            comand2.Parameters.Add("@pass", MySqlDbType.VarChar).Value = pass;
            comand2.Parameters.Add("@tgl", MySqlDbType.VarChar).Value = tgl;
            adapter1.SelectCommand = comand2;
            adapter1.Fill(dt);
            MessageBox.Show("Data Succesfully Add",
                        "Data Succesfully Add", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txtUser.Text = "";
            txtPass.Password = "";
            txtConpass.Password = "";
            txtTgl.Text = "";
            txtAlamat.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            Visibility = Visibility.Hidden;
            MW.Show();
        }
    }
}
