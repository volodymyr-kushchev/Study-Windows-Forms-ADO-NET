using System;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    // класс для работы с табличкой Routes
    public class Route
    {
       public int IdRoute { set; get; }
       public string Name { set; get; }
       public DateTime StartDate { set; get; }
       public DateTime FinishDate { set; get; }
    }
}
