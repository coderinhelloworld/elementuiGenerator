using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LayuiTableGenerate.Classes;

namespace LayuiTableGenerate.Assemblys
{
    public class ColumnModule
    {
        public ColumnModule(string modelName, string showName)
        {

            _modelName = modelName;
            _showName = showName;
        }
        private string _modelName;
        private string _showName;
        private string _value;
        public string Value
        {
            get
            {
                _value = @"<el-table-column :show-overflow-tooltip='true' prop='"+ _modelName.ToFirstLetterLower() + "' label='"+ _showName + "'></el-table-column>";
                return _value;

            }
        }
    }
}
