using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



//Вспомогательные формы для каждой таблички:

namespace WindowsFormsApp1
{
    // хранит данные с таблици, также реализует методы для модификации данных
    public class ClientRepository
    {
        private SqlConnection oSqlCon;
        private SqlCommand oSqlCom;
        private SqlDataAdapter oSqlDtAdptr;
        private SqlConnectionStringBuilder oConStringBuilder;
        // список для хранинения данных о клиентах
        public List<Client> Clients = new List<Client>();
        // конструктор для инициализации строки подключения
        public ClientRepository()
        {
            oConStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "VDBS",
                InitialCatalog = "Tourist Routes",
                UserID = "dbuser",
                Password = "dbuser"
            };
            oSqlCon = new SqlConnection();
            oSqlCon.ConnectionString = oConStringBuilder.ConnectionString;
        }
        // считивает таблицу Client с базы данных
        public DataTable FillDataSet()
        {
            try
            {
                // проверка, не открыто ли ранее соединение 
                if (oSqlCon.State != ConnectionState.Open)
                {
                    oSqlCon.Open();
                }
                var ds = new DataTable();
                using (oSqlCom = new SqlCommand())
                {
                    oSqlCom.Connection = oSqlCon;
                    // передать команду
                    oSqlCom.CommandText = "select * from Client";
                    using (oSqlDtAdptr = new SqlDataAdapter())
                    {
                        oSqlDtAdptr.SelectCommand = oSqlCom;
                        oSqlDtAdptr.Fill(ds);
                        // очистка прежнего списка
                        Clients.Clear();
                        // заполнение списка новыми данными
                        initialiseList(ds);
                        return ds;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (oSqlCon.State == ConnectionState.Open)
                {
                    oSqlCon.Close();
                }
            }
        }
        // метод для заполнения списка clients данными из бд.
        private void initialiseList(DataTable dt)
        {
            foreach(DataRow drow in dt.Rows)
            {
                Client cl = new Client();
                cl.IdClient = Convert.ToInt32(drow["Id_Client"]);
                cl.Name = drow["Name"].ToString();
                cl.Surname = drow["Surame"].ToString();
                Clients.Add(cl);
            }
        }
        // добавление строки в таблицу Client
        public int Insert(string name, string surname)
        {
            try
            {
                if (oSqlCon.State != ConnectionState.Open)
                {
                    oSqlCon.Open();
                }
                using (oSqlCom = new SqlCommand())
                {
                    oSqlCom.Connection = oSqlCon;
                    oSqlCom.CommandText = "insert into Client Values(@Name, @Surname)";
                    oSqlCom.Parameters.AddWithValue("@Name", name);
                    oSqlCom.Parameters.AddWithValue("@Surname", surname);
                    return oSqlCom.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (oSqlCon.State == ConnectionState.Open)
                {
                    oSqlCon.Close();
                }
            }
        }
        //редактирование строчки при одном или нескольких переданных параметров
        //допускается передача пустых строк, только у те которые не должны быть измененые.
        public int Update(int id, string name, string surname)
        {
            try
            {
                // check whether the connection is not open  
                if (oSqlCon.State != ConnectionState.Open)
                {
                    oSqlCon.Open();
                }
                using (oSqlCom = new SqlCommand())
                {
                    oSqlCom.Connection = oSqlCon;
                    if (name != string.Empty && surname != string.Empty)
                    {
                        oSqlCom.CommandText = "Update Client set Name = @name, Surame = @Surname where Id_Client = @id";
                        oSqlCom.Parameters.AddWithValue("@id", id);
                        oSqlCom.Parameters.AddWithValue("@Name", name);
                        oSqlCom.Parameters.AddWithValue("@Surname", surname);
                    }
                    else
                    {
                        if (name == string.Empty)
                        {
                            oSqlCom.CommandText = "Update Client set Surame = @Surname where Id_Client = @id";
                            oSqlCom.Parameters.AddWithValue("@id", id);
                            oSqlCom.Parameters.AddWithValue("@Surname", surname);
                        }
                        else
                        {
                            oSqlCom.CommandText = "Update Client set Name = @name where Id_Client = @id";
                            oSqlCom.Parameters.AddWithValue("@id", id);
                            oSqlCom.Parameters.AddWithValue("@Name", name);
                        }
                    }
                    return oSqlCom.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (oSqlCon.State == ConnectionState.Open)
                {
                    oSqlCon.Close();
                }
            }
        }
        // удаление строк из табл.
        public int Delete(int id)
        {
            try
            {
                if (oSqlCon.State != ConnectionState.Open)
                {
                    oSqlCon.Open();
                }
                using (oSqlCom = new SqlCommand())
                {
                    oSqlCom.Connection = oSqlCon;
                    oSqlCom.CommandText = "delete from Client where Id_Client = @id";
                    oSqlCom.Parameters.AddWithValue("@id", id);
                    return oSqlCom.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (oSqlCon.State == ConnectionState.Open)
                {
                    oSqlCon.Close();
                }
            }
        }
    }
}
