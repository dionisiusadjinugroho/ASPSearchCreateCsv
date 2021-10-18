using System;

namespace ASPSearchCreateCsv.Models
{
    public class CSVData
    {
        public Guid ID { get; set; }
        public string Content { get; set; }
        public int MatchedTimes { get; set; }
    }
}