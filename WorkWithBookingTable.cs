using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    // форма для работы с таблицей Booking
    public partial class WorkWithBookingTable : Form
    {
        BookingRepository booking;
        public WorkWithBookingTable(BookingRepository bk)
        {
            InitializeComponent();
            booking = bk;
        }
        //создание нового заказа
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dateorder = Convert.ToDateTime(dateTimePicker1.Text);
            try
            {
                int cost = Convert.ToInt32(textBox4.Text);
                int IdClient = Convert.ToInt32(textBox3.Text);
                int IdRoute = Convert.ToInt32(textBox2.Text);
                int res = booking.Insert(IdRoute, IdClient, cost, dateorder);
                MessageBox.Show("Inserted " + res.ToString() + " rows");
            }
            catch (FormatException)
            {
                MessageBox.Show("Id/Cost не может быть символьным значением.\nВведите число пожалуйста");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //редактирование данных в таблице
        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dateorder = Convert.ToDateTime(dateTimePicker1.Text);
            try
            {
                int id = Convert.ToInt32(textBox1.Text);
                int IdRoute = Convert.ToInt32(textBox2.Text);
                int cost = Convert.ToInt32(textBox4.Text);
                int res = booking.Update(id, IdRoute, cost, dateorder);
                MessageBox.Show("Edited " + res.ToString() + " rows");
            }
            catch(FormatException)
            {
                MessageBox.Show("Id не может быть символьным значением.\nПожалуйста, введите число");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //удаление строк из таблицы
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox1.Text);
                int res = booking.Delete(id);
                MessageBox.Show("Deleted " + res.ToString() + " rows");
            }
            catch (FormatException)
            {
                MessageBox.Show("Id не может быть символьным значением.\nПожалуйста, введите число");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
