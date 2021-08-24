using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayuiTableGenerate.Assemblys
{
    public class OptionModule
    {
        public OptionModule(string modelName,string optionData,string showName,string frontLabelName="")
        {

            _modelName = modelName;
            _optionData = optionData;
            _showName = showName;
            _frontLabelName = frontLabelName;
        }
        private string _modelName;
        private string _showName;
        private string _optionData;
        private string _value;
        private string _frontLabelName;
        public string Value
        {
            get
            {
                _value = @"<el-form-item label='" + _frontLabelName + @"'>
                        <el-select v-model='" + _modelName +@"' placeholder='"+ _showName + @"' clearable>
                            <el-option v-for='(item,index) in "+ _optionData + @"' :key='index' :label='item' :value='item'> </el-option>
                        </el-select>
                    </el-form-item>";
                return _value;

            }
        }
    }
}
