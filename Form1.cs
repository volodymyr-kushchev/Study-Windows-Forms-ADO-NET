using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ClientRepository cl = new ClientRepository();
        RouteRepository rt = new RouteRepository();
        BookingRepository bk = new BookingRepository();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            ShowDB();
            UpDateComboBoxes();

        }
        //обнвление данных выпадающих списков
        private void UpDateComboBoxes()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            // заполнение выпадающих списков.
            comboBox1.Items.Add("Client");
            comboBox1.Items.Add("Booking");
            comboBox1.Items.Add("Routes");
            foreach (Route route in rt.Routes)
                comboBox2.Items.Add(route.IdRoute);
            foreach (Client client in cl.Clients)
            {
                comboBox3.Items.Add(client.Name);
                comboBox4.Items.Add(client.Name);
            }
        }
        //функция для заполнения View данными из таблиц базы данных
        private void ShowDB()
        {
            dataGridView1.DataSource = cl.FillDataSet();
            dataGridView3.DataSource = rt.FillDataSet();
            dataGridView2.DataSource = bk.FillDataSet();
        }
        //вызов хранимой процедуры для подсчета количества проданых туров
        //за определенный промежуток времени
        private void button1_Click(object sender, EventArgs e)
        {
            string first = dateTimePicker1.Value.ToShortDateString().ToString();
            string second = dateTimePicker2.Value.ToShortDateString().ToString();
            first = ConvertToDate(first);
            second = ConvertToDate(second);
            dataGridView4.DataSource = bk.FillResultTours(first, second);

        }
        //преобразует строку с датой для дальнейшей работы процедуры.
        //пример: ->2019.01.02 convert to -> 02-01-2019
        public string ConvertToDate(string Date)
        {
            string[] arr = Date.Split('.');
            string res = arr[2] + "-" + arr[1] + "-" + arr[0];
            return res;
        }
        //вызов функции для расчета стоимости тура
        private void button2_Click(object sender, EventArgs e)
        {
            int idtour = Convert.ToInt32(comboBox2.SelectedItem);
            dataGridView4.DataSource = bk.CostTours(idtour);
        }
        //вызов функции для вывода списка отелей
        private void button3_Click(object sender, EventArgs e)
        {
            string name = comboBox3.SelectedItem.ToString();
            dataGridView4.DataSource = rt.ListHotels(name);

        }
        //вызов хранимки для подсчета суммы проданых туров
        private void button4_Click(object sender, EventArgs e)
        {
            string first = dateTimePicker3.Value.ToShortDateString().ToString();
            string second = dateTimePicker4.Value.ToShortDateString().ToString();
            first = ConvertToDate(first);
            second = ConvertToDate(second);
            dataGridView4.DataSource = bk.SoldTours(first, second);
        }
        //вызов функции для составления расписания отдыха
        private void button5_Click(object sender, EventArgs e)
        {
            string name = comboBox4.SelectedItem.ToString();
            dataGridView4.DataSource = bk.Schedule(name);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //выбор таблички для дальнейшей модификации данных в ней
        private void button6_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Client":
                    {
                        WorkWithClientTable form = new WorkWithClientTable(cl);
                        form.ShowDialog();
                        break;
                    }
                case "Booking":
                    {
                        WorkWithBookingTable form = new WorkWithBookingTable(bk);
                        form.ShowDialog();
                        break;
                    }
                case "Routes":
                    {
                        WorkWithRouteTable form = new WorkWithRouteTable(rt);
                        form.ShowDialog();
                        break;
                    }
            }
            // обновление данных на форме.
            UpDateComboBoxes();
            ShowDB();
        }
    }
}
