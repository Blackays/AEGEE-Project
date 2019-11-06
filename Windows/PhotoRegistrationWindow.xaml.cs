using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// <summary>
    /// Interaction logic for PhotoRegistrationWindow.xaml
    /// </summary>
    public partial class PhotoRegistrationWindow : Window
    {
        DataSet ds;
        string strName, imageName;
        string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public PhotoRegistrationWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
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

                    using (SqlConnection conn = new SqlConnection(constr))
                    {

                        conn.Open();
                        string sql = "insert into Images(ImageContent) values(@img)";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            //Pass byte array into database
                            cmd.Parameters.Add(new SqlParameter("img", imgByteArr));
                            int result = cmd.ExecuteNonQuery();
                            if (result == 1)
                            {
                                MessageBox.Show("Image added successfully.");
                                
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

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
                try
                {
                    FileDialog fldlg = new OpenFileDialog();
                    fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                    fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                    fldlg.ShowDialog();
                    {
                        strName = fldlg.SafeFileName;
                        imageName = fldlg.FileName;
                        ImageSourceConverter isc = new ImageSourceConverter();
                        image.SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));
                    }
                    fldlg = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
        }
      
    }
}
