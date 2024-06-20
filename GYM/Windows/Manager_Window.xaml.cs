using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
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

namespace GYM.Windows
{
    public partial class Manager_Window : Window
    {
        public Manager_Window()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow x = new MainWindow();
            x.Show();
            Close();
        }

        private void LoadData()
        {
            string connectionString = "DataSource=db.db";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();


                string sqlExpression = "SELECT * FROM Devices";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                List<Device> devices = new List<Device>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Device device = new Device
                        {
                            id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            FAQ = reader.GetString(2),
                            Availability = reader.GetString(3),

                        };
                        devices.Add(device);
                    }
                }

                DataGrid_Devices.ItemsSource = devices;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new Device_Add().Show();
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Device selectedItem = (Device)DataGrid_Devices.SelectedItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Выберите Тренажер");
            }
            else
            {
                try
                {
                    using (var connection = new SqliteConnection("Data Source=db.db"))
                    {
                        connection.Open();

                        string query = "DELETE FROM Devices WHERE id = @id";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", selectedItem.id);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Тренажер успешно удален");

                    DeleteDevice();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении тренажера: " + ex.Message);
                }
            }
        }

        private void DeleteDevice()
        {
            try
            {
                List<Device> devices = new List<Device>();

                using (var connection = new SqliteConnection("Data Source=db.db"))
                {
                    connection.Open();

                    string query = "SELECT id, Name, FAQ, Availability FROM Devices";
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            devices.Add(new Device
                            {
                                id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                FAQ = reader.GetString(2),
                                Availability = reader.GetString(3)
                            });
                        }
                    }
                }

                DataGrid_Devices.ItemsSource = devices;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке сотрудников: " + ex.Message);
            }
        }
    }
}

public class Device
{
    public int id { get; set; }
    public string Name { get; set; }
    public string FAQ { get; set; }
    public string Availability { get; set; }
}
