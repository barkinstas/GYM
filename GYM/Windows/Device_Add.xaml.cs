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
    public partial class Device_Add : Window
    {
        public Device_Add()
        {
            InitializeComponent();
        }

        private void Add_Device(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text) ||
                string.IsNullOrWhiteSpace(FAQ.Text) ||
                string.IsNullOrWhiteSpace(Availability.Text))
            {
                MessageBox.Show("Заполните все поля и убедитесь, что все введено корректно");
            }
            else
            {
                try
                {
                    using (var connection = new SqliteConnection("Data Source=db.db"))
                    {
                        connection.Open();

                        string query = "INSERT INTO Devices (Name, FAQ, Availability) VALUES (@Name, @FAQ, @Availability)";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Name", Name.Text);
                            command.Parameters.AddWithValue("@FAQ", FAQ.Text);
                            command.Parameters.AddWithValue("@Availability", Availability.Text);

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Данные успешно сохранены");

                    new Manager_Window().Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
                }
            }
        }
    }
}
