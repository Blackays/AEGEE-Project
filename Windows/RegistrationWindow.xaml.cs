using AEGEE_Project.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        User Tom;
        public RegistrationWindow()
        {
            InitializeComponent();
            Tom = new User();
            this.DataContext = Tom;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            int a = 1;

            if (PasswordBox.Text.Trim() == "" || RepeatPasswordBox.Text.Trim() == "")
            {            
                MessageBox.Show("One or more input field is empty,Please enter values");
                return;
            }
            if (PasswordBox.Text.Trim() != RepeatPasswordBox.Text.Trim())
            {               
                MessageBox.Show("Please enter equal passwords");
                return;
            }           
            if (!NameBox.Text.All(Char.IsLetter))
            {
                MessageBox.Show("Name should have only letters");
                return;
            }
            if (!SurnameBox.Text.All(Char.IsLetter))
            {
                MessageBox.Show("Surname should have only letters");
                return;
            }

            try
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
                a = cmd.ExecuteNonQuery();
            }
            catch
            {      
                MessageBox.Show("This Login already exist");
                return;
            }

            if (a == 1)
            {
                MessageBox.Show("Added");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
        
    }
}
