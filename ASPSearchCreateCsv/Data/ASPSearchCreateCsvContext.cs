using System.Data.Entity;

namespace ASPSearchCreateCsv.Data
{
    public class ASPSearchCreateCsvContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public ASPSearchCreateCsvContext() : base("name=ASPSearchCreateCsvContext")
        {
        }

        public System.Data.Entity.DbSet<ASPSearchCreateCsv.Models.CSVData> CSVDatas { get; set; }
    }
}
