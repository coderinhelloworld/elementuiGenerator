using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LayuiTableGenerate.Classes;

namespace LayuiTableGenerate.Assemblys
{
    public class DateModule
    {
        public DateModule(string modelName,string frontLabelName="")
        {

            _modelName = modelName;
            _frontLabelName = frontLabelName;
        }
        private string _modelName;
        private string _value;
        private string _frontLabelName;
        public string Value
        {
            get
            {
                _value = @"<el-form-item label='"+ _frontLabelName + @"'>
                        <el-date-picker value-format='yyyy-MM-dd' v-model='" + _modelName.ToFirstLetterLower() + @"' unlink-panels type='daterange' range-separator='至' start-placeholder='开始日期' end-placeholder='结束日期' :picker-options='pickerOptions'>
                        </el-date-picker>
                    </el-form-item>";
                return _value;

            }
        }
    }
}
