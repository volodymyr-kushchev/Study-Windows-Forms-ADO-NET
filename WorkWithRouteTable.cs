using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    // форма для работы с таблицей Route
    public partial class WorkWithRouteTable : Form
    {
        RouteRepository rt;
        public WorkWithRouteTable(RouteRepository rtRep)
        {
            InitializeComponent();
            rt = rtRep;
        }
        // добавление нового маршрута
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            DateTime start = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime finish = Convert.ToDateTime(dateTimePicker2.Text);
            try
            {
                int res = rt.Insert(name, start, finish);
                MessageBox.Show("Inserted " + res.ToString() + " rows");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // редактирование данных о маршруте
        private void button2_Click(object sender, EventArgs e)
        {
            DateTime start = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime finish = Convert.ToDateTime(dateTimePicker2.Text);
            try
            {
                int id = Convert.ToInt32(textBox1.Text);
                int res = rt.Update(id, start, finish);
                MessageBox.Show("Edited " + res.ToString() + " rows");
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
        // удаление стрк из таблицы
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox1.Text);
                int res = rt.Delete(id);
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
