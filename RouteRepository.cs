using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public class RouteRepository
    {
        private SqlConnection oSqlCon;
        private SqlCommand oSqlCom;
        private SqlDataAdapter oSqlDtAdptr;
        private SqlConnectionStringBuilder oConStringBuilder;
        public List<Route> Routes = new List<Route>();
        public RouteRepository()
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
        // считивает таблицу Routes с базы данных
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
                    oSqlCom.CommandText = "select * from Routes";
                    using (oSqlDtAdptr = new SqlDataAdapter())
                    {
                        oSqlDtAdptr.SelectCommand = oSqlCom;
                        oSqlDtAdptr.Fill(ds);
                        // очистка прежнего списка
                        Routes.Clear();
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
        // метод для заполнения списка routes данными из бд.
        private void initialiseList(DataTable dt)
        {
            foreach (DataRow drow in dt.Rows)
            {
                Route rt = new Route();
                rt.IdRoute = Convert.ToInt32(drow["Id_Route"]);
                rt.Name = drow["Name"].ToString();
                rt.StartDate = Convert.ToDateTime(drow["StartDate"]);
                rt.FinishDate = Convert.ToDateTime(drow["FinishDate"]);
                Routes.Add(rt);
            }
        }
        // вставка новых строк
        public int Insert(string name, DateTime start, DateTime finish)
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
                    oSqlCom.CommandText = "insert into Routes Values(@Name, @StartDate, @FinishDate)";
                    //input parameters
                    oSqlCom.Parameters.AddWithValue("@Name", name);
                    oSqlCom.Parameters.AddWithValue("@StartDate", start);
                    oSqlCom.Parameters.AddWithValue("@FinishDate", finish);
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
        // редактирование данных в табл.
        public int Update(int id, DateTime start, DateTime finish)
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
                    oSqlCom.CommandText = "Update Routes set StartDate = @start, FinishDate = @finish where Id_Route = @id";
                    oSqlCom.Parameters.AddWithValue("@id", id);
                    oSqlCom.Parameters.AddWithValue("@start", start);
                    oSqlCom.Parameters.AddWithValue("@finish", finish);
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
        // удаление строк из таблицы
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
                    oSqlCom.CommandText = "delete from Routes where Id_Route = @id";
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
        public DataTable ListHotels(string nameClient)
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
                    oSqlCom.CommandText = "select * from listHotel('" + nameClient + "')";
                    using (oSqlDtAdptr = new SqlDataAdapter())
                    {
                        oSqlDtAdptr.SelectCommand = oSqlCom;
                        oSqlDtAdptr.Fill(ds);
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
    }
}
