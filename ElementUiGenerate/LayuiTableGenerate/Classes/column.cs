using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayuiTableGenerate.Classes
{
    public class column
    {
        public string ColumnTitle { get; set; }

        public string ColumnDes { get; set; }
        public bool ShowInTable { get; set; }



        public bool  InputForm { get; set; } = false;

        public bool SearchForm { get; set; } = false;
        public string InputType { get; set; }
        public string TableName { get; set; }
    }
}
