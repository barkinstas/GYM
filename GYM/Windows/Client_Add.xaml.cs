using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GYM.Windows
{
    public partial class Client_Add : Window
    {
        public Client_Add()
        {
            InitializeComponent();
        }

        private void Add_Clients(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(Subscription.Text) ||
                string.IsNullOrWhiteSpace(Trainer.Text) ||
                string.IsNullOrWhiteSpace(Attendance.Text) ||
                !int.TryParse(PhoneNumber.Text, out int phonenumber) ||
                !int.TryParse(Subscription.Text, out int subscription))
            {
                MessageBox.Show("Заполните все поля и убедитесь, что номер введен корректно");
            }
            else
            {
                try
                {
                    using (var connection = new SqliteConnection("Data Source=db.db"))
                    {
                        connection.Open();

                        string query = "INSERT INTO Clients (Name, PhoneNumber, Subscription, Trainer, Attendance) VALUES (@Name, @PhoneNumber, @Subscription, @Trainer, @Attendance)";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Name", Name.Text);
                            command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber.Text);
                            command.Parameters.AddWithValue("@Subscription", Subscription.Text);  
                            command.Parameters.AddWithValue("@Trainer", Trainer.Text);           
                            command.Parameters.AddWithValue("@Attendance", Attendance.Text);

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Данные успешно сохранены");

                    new Admin_Window().Show();
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