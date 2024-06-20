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
    public partial class Record_Add : Window
    {
        public Record_Add()
        {
            InitializeComponent();
        }

        private void Add_Record(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text) ||
                string.IsNullOrWhiteSpace(Coach.Text) ||
                string.IsNullOrWhiteSpace(Date.Text))
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

                        string query = "INSERT INTO Recording (Name, Coach, Date) VALUES (@Name, @Coach, @Date)";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Name", Name.Text);
                            command.Parameters.AddWithValue("@Coach", Coach.Text);
                            command.Parameters.AddWithValue("@Date", Date.Text);

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Данные успешно сохранены");

                    new Coach_Window().Show();
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

public class Recrod
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Coach { get; set; }
    public int Date { get; set; }
}