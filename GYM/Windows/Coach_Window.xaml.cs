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
    public partial class Coach_Window : Window
    {
        public Coach_Window()
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


                string sqlExpression = "SELECT * FROM Recording";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                List<Record> records = new List<Record>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Record record = new Record
                        {
                            id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Coach = reader.GetString(2),
                            Date = reader.GetInt32(3),

                        };
                        records.Add(record);
                    }
                }

                DataGrid_Recording.ItemsSource = records;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new Record_Add().Show();
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Record selectedItem = (Record)DataGrid_Recording.SelectedItem;

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

                        string query = "DELETE FROM Recording WHERE id = @id";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", selectedItem.id);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Запись успешно удалена");

                    DeleteRecord();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении записи: " + ex.Message);
                }
            }
        }

        private void DeleteRecord()
        {
            try
            {
                List<Record> records = new List<Record>();

                using (var connection = new SqliteConnection("Data Source=db.db"))
                {
                    connection.Open();

                    string query = "SELECT id, Name, Coach, Date FROM Recording";
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new Record
                            {
                                id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Coach = reader.GetString(2),
                                Date = reader.GetInt32(3),
                            });
                        }
                    }
                }

                DataGrid_Recording.ItemsSource = records;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке записей: " + ex.Message);
            }
        }
    }
}

public class Record
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Coach { get; set; }
    public int Date { get; set; }
}

