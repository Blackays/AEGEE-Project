using AEGEE_Project.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
        User user;
        string strName, imageName;
        string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public RegistrationWindow()
        {
            InitializeComponent();
            user = new User();
            this.DataContext = user;
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
            if (!AgeBox.Text.All(Char.IsDigit))
            {
                MessageBox.Show("Age should be digital");
                return;
            }
            else if (Convert.ToInt32(AgeBox.Text) > 100 || Convert.ToInt32(AgeBox.Text) < 0)
            {
                MessageBox.Show("Age should be less than 100 and bigger than 0");
                return;
            }

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO Users (Name, Surname, Login, Password, Age) VALUES(@name, @surname, @login, @password, @Age)";
                cmd.Parameters.AddWithValue("@name", NameBox.Text);
                cmd.Parameters.AddWithValue("@surname", SurnameBox.Text);
                cmd.Parameters.AddWithValue("@login", LoginBox.Text);
                cmd.Parameters.AddWithValue("@password", PasswordBox.Text);
                cmd.Parameters.AddWithValue("@Age", AgeBox.Text);

                cmd.Connection = con;
                a = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                MessageBox.Show("Added");
                return;
            }

            if (a == 1)
            {
                MessageBox.Show("Added");
            }

            try
            {
                if (imageName != "")
                {
                    //Initialize a file stream to read the image file
                    FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);

                    //Initialize a byte array with size of stream
                    byte[] imgByteArr = new byte[fs.Length];

                    //Read data from the file stream and put into the byte array
                    fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));

                    //Close a file stream
                    fs.Close();

                    using (con)
                    {

                        string sql = "insert into Images(ImageContent) values(@img)";
                        using (SqlCommand cmd2 = new SqlCommand(sql, con))
                        {
                            //Pass byte array into database
                            cmd2.CommandText = "insert into Images(ImageContent) values(@img)";
                            cmd2.Parameters.Add(new SqlParameter("img", imgByteArr));
                            int result = cmd2.ExecuteNonQuery();
                            if (result == 1)
                            {
                                MessageBox.Show("Image added successfully.");
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.Filter =
                    "Image Files (*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if ((bool)dialog.ShowDialog())
                {
                    var bitmap = new BitmapImage(new Uri(dialog.FileName));
                    var image = new Image { Source = bitmap };
                    Canvas.SetLeft(image, 0);
                    Canvas.SetTop(image, 0);
                    canvas.Children.Add(image);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private Image draggedImage;
        private Point mousePosition;

        private void CanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = e.Source as Image;

            if (image != null && canvas.CaptureMouse())
            {
                mousePosition = e.GetPosition(canvas);
                draggedImage = image;
                Panel.SetZIndex(draggedImage, 1); // in case of multiple images
            }
        }

        private void CanvasMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (draggedImage != null)
            {
                canvas.ReleaseMouseCapture();
                Panel.SetZIndex(draggedImage, 0);
                draggedImage = null;
            }
        }
        const double ScaleRate = 1.1;
        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                st.ScaleX *= ScaleRate;
                st.ScaleY *= ScaleRate;
            }
            else
            {
                st.ScaleX /= ScaleRate;
                st.ScaleY /= ScaleRate;
            }
        }

        private void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedImage != null)
            {
                var position = e.GetPosition(canvas);
                var offset = position - mousePosition;
                mousePosition = position;
                Canvas.SetLeft(draggedImage, Canvas.GetLeft(draggedImage) + offset.X);
                Canvas.SetTop(draggedImage, Canvas.GetTop(draggedImage) + offset.Y);
            }
        }

    }
}
