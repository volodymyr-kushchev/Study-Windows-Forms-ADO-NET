using System;
using System.Data;
using System.Data.SqlClient;
namespace WindowsFormsApp1
{
    // класс для модификации данных в таблице Booking
    public class Booking
    {
       public int IdBooking { set; get; }
       public int IdRoute { set; get; }
       public int IdClient { set; get; }
       public DateTime DateOrder { set; get; }
       public int Cost { set; get; }
    }
}
