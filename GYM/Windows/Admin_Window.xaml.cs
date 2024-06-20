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
    public partial class Admin_Window : Window
    {
        public Admin_Window()
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


                string sqlExpression = "SELECT * FROM Clients";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                List<Client> clients = new List<Client>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Client client = new Client
                        {
                            id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            PhoneNumber = reader.GetString(2),
                            Subscription = reader.GetInt32(3),
                            Trainer = reader.GetString(4),
                            Attendance = reader.GetString(5)

                        };
                        clients.Add(client);
                    }
                }

                DataGrid_Clients.ItemsSource = clients;
            }

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();


                string sqlExpression = "SELECT * FROM Coaches";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                List<Coach> coachs = new List<Coach>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Coach coach = new Coach
                        {
                            id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Specialization = reader.GetString(2),
                            Schedule = reader.GetInt32(3),

                        };
                        coachs.Add(coach);
                    }
                }

                DataGrid_Coach.ItemsSource = coachs;
            }
        }

        private void Client_Add(object sender, RoutedEventArgs e)
        {
            new Client_Add().Show();
            this.Close();
        }

        private void Client_Delete(object sender, RoutedEventArgs e)
        {
            Client selectedItem = (Client)DataGrid_Clients.SelectedItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника");
            }
            else
            {
                try
                {
                    using (var connection = new SqliteConnection("Data Source=db.db"))
                    {
                        connection.Open();

                        string query = "DELETE FROM Clients WHERE id = @id";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", selectedItem.id);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Сотрудник успешно удален");

                    LoadEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении сотрудника: " + ex.Message);
                }
            }
        }

        private void LoadEmployees()
        {
            try
            {
                List<Client> clients = new List<Client>();

                using (var connection = new SqliteConnection("Data Source=db.db"))
                {
                    connection.Open();

                    string query = "SELECT id, Name, PhoneNumber, Subscription, Trainer, Attendance FROM Clients";
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(new Client
                            {
                                id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                PhoneNumber = reader.GetString(2),
                                Subscription = reader.GetInt32(3),
                                Trainer = reader.GetString(4),
                                Attendance = reader.GetString(5)
                            });
                        }
                    }
                }

                DataGrid_Clients.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке клиентов: " + ex.Message);
            }
        }

        private void Coach_Add(object sender, RoutedEventArgs e)
        {
            new Coach_Add().Show();
            this.Close();
        }

        private void Coach_Delete(object sender, RoutedEventArgs e)
        {
            Coach selectedItem = (Coach)DataGrid_Coach.SelectedItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Выберите Тренера");
            }
            else
            {
                try
                {
                    using (var connection = new SqliteConnection("Data Source=db.db"))
                    {
                        connection.Open();

                        string query = "DELETE FROM Coaches WHERE id = @id";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", selectedItem.id);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Сотрудник успешно удален");

                    DeleteCoach();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении сотрудника: " + ex.Message);
                }
            }
        }

        private void DeleteCoach()
        {
            try
            {
                List<Coach> coaches = new List<Coach>();

                using (var connection = new SqliteConnection("Data Source=db.db"))
                {
                    connection.Open();

                    string query = "SELECT id, Name, Specialization, Schedule FROM Coaches";
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            coaches.Add(new Coach
                            {
                                id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Specialization = reader.GetString(2),
                                Schedule = reader.GetInt32(3),                             
                            });
                        }
                    }
                }

                DataGrid_Coach.ItemsSource = coaches;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке сотрудников: " + ex.Message);
            }
        }
    }
}

public class Client
{
    public int id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public int Subscription { get; set; }
    public string Trainer { get; set; }
    public string Attendance { get; set; }
}

public class Coach
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Specialization { get; set; }
    public int Schedule { get; set; }
}
