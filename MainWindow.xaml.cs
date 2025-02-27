using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
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
using CARS_WPF.Class;
using MySql.Data.MySqlClient;

namespace CARS_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=classicmodels;";
        MySqlConnection connection;
        ObservableCollection<Product> products = new();
        ObservableCollection<Order> orders = new();
        public MainWindow()
        {
            InitializeComponent();
            ConnectToDB();
            LoadProducts();
            LoadCountries();
            LoadOrders();
        }

        public void ConnectToDB()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                //MessageBox.Show("Connection successful!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        public void LoadProducts()
        {
            try
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM products GROUP BY productCode", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetDouble(7), reader.GetDouble(8)));
                    }
                }
                dtgProducts.ItemsSource = products;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void dtgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = (Product)dtgProducts.SelectedItem;
            if (product != null)
            {
                CheckOrderCount(product.ProductCode);
            }

        }

        public void CheckOrderCount(string productCode)
        {
            const string query = "SELECT COUNT(*) FROM orderdetails WHERE productCode = @productCode";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@productCode", productCode);
            try
            {
                int orderCount = Convert.ToInt32(command.ExecuteScalar());
                if (orderCount > 0)
                {
                    lblOrderCount.Content = $"Orders: {orderCount}";
                }
                else
                {
                    MessageBox.Show("No orders for this product.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCountry = cmbCountries.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedCountry))
            {
                LoadCustomersByCountry(selectedCountry);
            }
        }

        private void LoadCountries()
        {
            try
            {
                MySqlCommand command = new MySqlCommand("SELECT DISTINCT country FROM customers ORDER BY country", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbCountries.Items.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void LoadCustomersByCountry(string country)
        {
            ObservableCollection<Customer> customers = new();
            try
            {
                MySqlCommand command = new MySqlCommand("SELECT customerName, phone, city FROM customers WHERE country = @country", connection);
                command.Parameters.AddWithValue("@country", country);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                    }
                }
                dtgCustomers.ItemsSource = customers;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void LoadOrders()
        {
            try
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM orders", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        orders.Add(new Order(
                            reader.GetInt32(0),
                            reader.GetDateTime(1),
                            reader.GetDateTime(2),
                            reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            reader.GetString(4),
                            reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                            reader.GetInt32(6)
                        ));
                    }

                dtgOrders.ItemsSource = orders;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void dtgOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgOrders.SelectedIndex >= 0)
            {
                lstOrderDetails.Items.Clear();
                ObservableCollection<OrderDetails> orderedProducts = new();

                Order order = orders[dtgOrders.SelectedIndex];

                MySqlCommand command = new MySqlCommand("SELECT products.productName, products.buyPrice " +
                                                        "FROM orders " +
                                                        "INNER JOIN orderdetails ON orders.orderNumber = orderdetails.orderNumber " +
                                                        "INNER JOIN products ON orderdetails.productCode = products.productCode " +
                                                        "WHERE orders.orderNumber = @orderNumber " +
                                                        "ORDER BY products.productName", connection);
                command.Parameters.AddWithValue("@orderNumber", order.Id);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    orderedProducts.Add(new OrderDetails(reader.GetString(0), reader.GetDouble(1)));
                }
                reader.Close();

                foreach (var product in orderedProducts)
                {
                    lstOrderDetails.Items.Add($"{product.productName} - {product.buyPrice}");
                }
            }
        }
    }
}