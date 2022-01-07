using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Go2Go.Core.Factories
{ 
    public class DataTableFactory
    {
        public static DataTable GetStudyTable()
        {
            var table = new DataTable();
            table.Columns.AddRange(new[] {
                new DataColumn("integerIN", typeof(int)),
            });
            return table;
        }
        public static DataTable GetUserLedgersTable()
        {
            var table = new DataTable();
            table.Columns.AddRange(new[] {
                new DataColumn("Id", typeof(int)),
                new DataColumn("TrxId", typeof(string)),
                new DataColumn("DriverId", typeof(int)),
                new DataColumn("DriverKey", typeof(string)),
                new DataColumn("RefId", typeof(string)),
                new DataColumn("TreansactionType", typeof(int)),
                new DataColumn("Description", typeof(string)),
                new DataColumn("Flag", typeof(int)),
                new DataColumn("Amount", typeof(decimal)),
                new DataColumn("Balance", typeof(decimal)),
                //new DataColumn("TrxDate", typeof(DateTime),),
                
            });
            return table;
        }

    }
}
