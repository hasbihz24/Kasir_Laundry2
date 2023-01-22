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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kasir_Laundry2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MYDB db = new MYDB();

            string username = txtUser.Text;
            string pass = txtPass.Password;

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(
                "select * from `akun_pegawai` where `Username`=@usn AND `password`=@pass", db.getConnection());
            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = pass;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                Dashboard dh = new Dashboard();
                Visibility = Visibility.Hidden;
                dh.Show();

            }
            else
            {
                if (username.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Your Username",
                        "Empty Username", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (pass.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Your Password",
                        "Empty Password", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password",
                        "Wrong Data", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Registrasi rgt = new Registrasi();
            Visibility = Visibility.Hidden;
            rgt.Show();

        }

        private void txtPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
