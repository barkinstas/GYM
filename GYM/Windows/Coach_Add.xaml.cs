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
    public partial class Coach_Add : Window
    {
        public Coach_Add()
        {
            InitializeComponent();
        }

        private void Add_Coach(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text) ||
                string.IsNullOrWhiteSpace(Specialization.Text) ||
                string.IsNullOrWhiteSpace(Schedule.Text))
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

                        string query = "INSERT INTO Coaches (Name, Specialization, Schedule) VALUES (@Name, @Specialization, @Schedule)";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Name", Name.Text);
                            command.Parameters.AddWithValue("@Specialization", Specialization.Text);
                            command.Parameters.AddWithValue("@Schedule", Schedule.Text);

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
