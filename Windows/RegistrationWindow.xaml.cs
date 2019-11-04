using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace AEGEE_Project.Windows
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Users (Name, Surname, Login, Password) VALUES(@name, @surname, @login, @password)";
            cmd.Parameters.AddWithValue("@name", NameBox.Text);
            cmd.Parameters.AddWithValue("@surname", SurnameBox.Text);
            cmd.Parameters.AddWithValue("@login", LoginBox.Text);
            cmd.Parameters.AddWithValue("@password", PasswordBox.Text);
            cmd.Connection = con;
            int a = cmd.ExecuteNonQuery();
            if(a==1)
            {
                MessageBox.Show("Added");
            }
            else { MessageBox.Show("Not Added"); }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
