using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace CARS_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConnectToDB();
        }

        public void ConnectToDB()
        {
            const string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=classicmodels;";

            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MessageBox.Show("Connection successful!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
    }
}