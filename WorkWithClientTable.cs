using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    // форма для работы с табицей Client
    public partial class WorkWithClientTable : Form
    {
        ClientRepository ClRep;
        public WorkWithClientTable(ClientRepository clr)
        {
            InitializeComponent();
            ClRep = clr;
        }
        private void WorkWithClientTable_Load(object sender, EventArgs e)
        {

        }
        // вставка строк в таблицу
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            string surname = textBox3.Text;
            try
            {
                int res = ClRep.Insert(name, surname);
                MessageBox.Show("Inserted " + res.ToString() + " rows");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // обновление данных в таблице
        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            string surname = textBox3.Text;
            try
            {
                int id = Convert.ToInt32(textBox1.Text);
                int res = ClRep.Update(id, name, surname);
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
        // удаление строк из таблицы
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox1.Text);
                int res = ClRep.Delete(id);
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
