using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public class BookingRepository
    {
        private SqlConnection oSqlCon;
        private SqlCommand oSqlCom;
        private SqlDataAdapter oSqlDtAdptr;
        private SqlConnectionStringBuilder oConStringBuilder;
        public List<Booking> bookings = new List<Booking>();
        public BookingRepository()
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
                    oSqlCom.CommandText = "select * from Booking";
                    using (oSqlDtAdptr = new SqlDataAdapter())
                    {
                        oSqlDtAdptr.SelectCommand = oSqlCom;
                        oSqlDtAdptr.Fill(ds);
                        // очистка прежнего списка
                        bookings.Clear();
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
            foreach (DataRow drow in dt.Rows)
            {
                Booking bk = new Booking();
                bk.IdBooking = Convert.ToInt32(drow["Id_Booking"]);
                bk.IdRoute = Convert.ToInt32(drow["Id_Route"]);
                bk.IdClient = Convert.ToInt32(drow["Id_Client"]);
                bk.DateOrder = Convert.ToDateTime(drow["DateOrder"]);
                bk.Cost = Convert.ToInt32(drow["Cost"]);
                bookings.Add(bk);
            }
        }
        // вставка данных 
        public int Insert(int idRoute, int idClient, int cost, DateTime dateOrder)
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
                    oSqlCom.CommandText = "insert into Booking Values(@IdRoute, @IdClient, @DateOrder, @Cost)";
                    // входные параметры
                    oSqlCom.Parameters.AddWithValue("@IdRoute", idRoute);
                    oSqlCom.Parameters.AddWithValue("@IdClient", idClient);
                    oSqlCom.Parameters.AddWithValue("@Cost", cost);
                    oSqlCom.Parameters.AddWithValue("@DateOrder", dateOrder);
                    // это вернет количество затронутых строк, выполнив запрос  
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
        //редактирование данных. Принимает номер маршрута стоимость и дату заказа.
        public int Update(int id, int idRoute, int cost, DateTime dateOrder)
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
                    // строка запроса
                    oSqlCom.CommandText = "Update Booking set Id_Route = @idRoute, Cost = @cost, DataOrder = @dateOrder where Id_Booking = @id";
                    oSqlCom.Parameters.AddWithValue("@id", id);
                    oSqlCom.Parameters.AddWithValue("@idRoute", idRoute);
                    oSqlCom.Parameters.AddWithValue("@cost", cost);
                    oSqlCom.Parameters.AddWithValue("@dateOrder", dateOrder);
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
        // удаление строки по id заказа
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
                    oSqlCom.CommandText = "delete from Booking where Id_Booking = @id";
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
        //вызов хранимой процедуры для подсчета количества проданых туров
        //за определенный промежуток времени
        public DataTable FillResultTours(string first, string second)
        {
            try
            {
                if (oSqlCon.State != ConnectionState.Open)
                {
                    oSqlCon.Open();
                }
                var ds = new DataTable();
                using (oSqlCom = new SqlCommand())
                {
                    //установить соединение для команды 
                    oSqlCom.Connection = oSqlCon;
                    oSqlCom.CommandText = "BoughtR";
                    //тип команды хранимая процедура
                    oSqlCom.CommandType = CommandType.StoredProcedure;
                    //передача параметров в хранимую процедуру
                    oSqlCom.Parameters.AddWithValue("@StartD", first);
                    oSqlCom.Parameters.AddWithValue("@EndD", second);
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
        public DataTable CostTours(int NumberTour)
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
                    oSqlCom.CommandText = "select dbo.CostTours(" + NumberTour.ToString() + ")";
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
        public DataTable SoldTours(string first, string second)
        {
            try
            {
                if (oSqlCon.State != ConnectionState.Open)
                {
                    oSqlCon.Open();
                }
                var ds = new DataTable();
                using (oSqlCom = new SqlCommand())
                {
                    //установить соединение для команды 
                    oSqlCom.Connection = oSqlCon;
                    oSqlCom.CommandText = "SoldTours";
                    //тип команды хранимая процедура
                    oSqlCom.CommandType = CommandType.StoredProcedure;
                    //передача параметров в хранимую процедуру
                    oSqlCom.Parameters.AddWithValue("@StartD", first);
                    oSqlCom.Parameters.AddWithValue("@EndD", second);
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
        public DataTable Schedule(string nameClient)
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
                    oSqlCom.CommandText = "select * from Schedule('" + nameClient + "')";
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
