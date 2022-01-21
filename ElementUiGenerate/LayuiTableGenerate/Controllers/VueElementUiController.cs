using System;
using System.Collections.Generic;
using System.Linq;
using LayuiTableGenerate.Assemblys;
using LayuiTableGenerate.Classes;
using LayuiTableGenerate.Enum;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LayuiTableGenerate.Controllers
{
    public class VueElementUiController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePageCode(string columns, string tableName)
        {
            tableName = tableName.Substring(0, 1).ToLower() + tableName.Substring(1);
            var settingList = JsonConvert.DeserializeObject<List<column>>(columns);
            var searchForm = "";
            var addNewDataForm = "";
            var tableQueryParams = "{";
            var tableRequestParams = "{";
            var tableColumns = "";
            var headerCellStyle = "{background:\"#eef1f6\",color:\"#606266\"}";
            var editGiveValue = "";
            var optionData = "";
            var optionList = new List<string>();

            foreach (var item in settingList)
            {
                //搜索的时候
                if (item.SearchForm)
                {
                    tableQueryParams += item.ColumnTitle.ToFirstLetterLower() + ":''," + "\r\n";
                    if (item.InputType == FormInputType.Date.ToString())
                    {
                        var dateForm = new DateModule(tableName + "Query." + item.ColumnTitle.ToFirstLetterLower());
                        searchForm += dateForm.Value + "\r\n";
                    }

                    if (item.InputType == FormInputType.Input.ToString())
                    {
                        var inputForm = new InputModule(tableName + "Query." + item.ColumnTitle.ToFirstLetterLower(),
                            item.ColumnDes);
                        searchForm += inputForm.Value + "\r\n";
                    }

                    if (item.InputType == FormInputType.Option.ToString() || string.IsNullOrEmpty(item.InputType) )
                    {
                        var optionName = tableName + "Query" + item.ColumnTitle.ToFirstLetterLower() + "Options";
                        var optionForm = new OptionModule(tableName + "Query." + item.ColumnTitle.ToFirstLetterLower(),
                            optionName, item.ColumnDes);
                        searchForm += optionForm.Value + "\r\n";
                        var existOption = optionList.Where(x => x == optionName).FirstOrDefault();
                        if (existOption == null)
                        {
                            optionData += optionName + ":''," + "\r\n";
                            optionList.Add(optionName);
                        }
                    }
                }

                //新增的时候
                if (item.InputForm)
                {
                    tableRequestParams += item.ColumnTitle.ToFirstLetterLower() + ":''," + "\r\n";
                    editGiveValue += "this." + tableName + "Request." + item.ColumnTitle.ToFirstLetterLower() +
                                     "=row." + item.ColumnTitle.ToFirstLetterLower() + ";" + "\r\n";
                    if (item.InputType == FormInputType.Date.ToString())
                    {
                        var dateForm = new DateModule(tableName + "Request." + item.ColumnTitle.ToFirstLetterLower(),
                            item.ColumnDes);
                        addNewDataForm += dateForm.Value + "\r\n";
                    }

                    if (item.InputType == FormInputType.Input.ToString())
                    {
                        var inputForm = new InputModule(tableName + "Request." + item.ColumnTitle.ToFirstLetterLower(),
                            item.ColumnDes, item.ColumnDes);
                        addNewDataForm += inputForm.Value + "\r\n";
                        ;
                    }

                    if (item.InputType == FormInputType.Option.ToString())
                    {
                        var optionName = tableName + "Query" + item.ColumnTitle.ToFirstLetterLower() + "Options";
                        var optionForm =
                            new OptionModule(tableName + "Request." + item.ColumnTitle.ToFirstLetterLower(), optionName,
                                item.ColumnDes, item.ColumnDes);

                        var existOption = optionList.Where(x => x == optionName).FirstOrDefault();
                        if (existOption == null)
                        {
                            optionData += optionName + ":''," + "\r\n";
                            optionList.Add(optionName);
                        }

                        addNewDataForm += optionForm.Value.ToFirstLetterLower() + "\r\n";
                    }
                }

                //显示在表格中
                if (item.ShowInTable)
                {
                    var column = new ColumnModule(item.ColumnTitle.ToFirstLetterLower(), item.ColumnDes);
                    tableColumns += column.Value.ToFirstLetterLower() + "\r\n";
                    ;
                }
            }

            tableQueryParams += "pageIndex:1,pageSize:10 " + "\r\n" + "}";
            tableRequestParams = tableRequestParams.TrimEnd(',') + "\r\n" + "}";

            // html = html.Replace("{searchForm}", searchForm);
            // html = html.Replace("{addNewDataForm}", addNewDataForm);
            // html = html.Replace("{tableQueryParams}", tableQueryParams);
            // html = html.Replace("{tableRequestParams}", tableRequestParams);
            // html = html.Replace("{tableColumns}", tableColumns);
            // html = html.Replace("{tableName}", tableName);
            // html = html.Replace("{headerCellStyle}", headerCellStyle);
            // html = html.Replace("{editGiveValue}", editGiveValue);
            // html = html.Replace("{optionData}", optionData);
            var html = $@"
<template>
<div id='app' v-cloak>
        <el-container v-loading.fullscreen.lock='fullscreenLoading'>
            <el-header style='height:auto'>
                <el-form :inline='true' @keyup.enter.native='getTableList'>
                   {searchForm}
                    <el-form-item>
                        <el-button size='small' type='primary' icon='el-icon-search' @click='getTableList'>查询</el-button>
                    </el-form-item>
                </el-form>
            </el-header>
            <el-row :gutter='10' style='margin-left:0px'>
                <el-button-group>
                    <el-button type='primary' @click='showAdd{tableName}Layer=true' icon='el-icon-plus'>新增</el-button>
                </el-button-group>
            </el-row>
            <el-card class='box-card' style='margin-top:10px'>
                <template>
                    <el-table id='mainTable' :height='mainTableHeight' :data='{tableName}Table.data' v-loading='{tableName}TableLoading' style='width: 100%' border element-loading-background='rgba(255, 255, 255, 1)' :header-cell-style='{headerCellStyle}' element-loading-text='Loading' element-loading-spinner='el-icon-loading'>
                                                      {tableColumns}
                        <el-table-column fixed='right' show-summary width='240' label='操作' style='margin:5px'>
                            <template slot-scope='scope'>
                                <el-tooltip class='item' effect='dark' content='编辑' placement='top-end'>
                                    <el-button size='mini' type='success' @click='edit{tableName}(scope.row)'><i class='el-icon-edit-outline'></i></el-button>
                                </el-tooltip>
                                <el-tooltip class='item' effect='dark' content='删除' placement='top-end'>
                                    <el-button size='mini' type='danger' @click='delete{tableName}(scope.row)'><i class='el-icon-delete'></i></el-button>
                                </el-tooltip>
                            </template>
                        </el-table-column>
                    </el-table>
                    <el-pagination style='margin-top:10px' :page-size='{tableName}Query.pageSize' @current-change='handleCurrentChange'  @size-change='handleSizeChange' :current-page.sync='{tableName}Query.pageIndex' background layout='total,sizes,prev, pager, next, jumper' :total='{tableName}Table.total'></el-pagination>
                </template>
            </el-card>
        </el-container>
        <div>
            <el-dialog title='编辑或新增' :visible.sync='showAdd{tableName}Layer'>
                <el-form label-width='80px' :inline='false' :model='{tableName}Request' ref='add{tableName}Layer'>
                   {addNewDataForm}
                </el-form>
                <div slot='footer' class='dialog-footer'>
                    <el-button type='primary' @click=" + "\"submitAdd{tableName}Form('add{tableName}Layer')\"" +
                       @$">确 认</el-button>
                    <el-button @click='closeAdd{tableName}Layer'>取 消</el-button>
                </div>
            </el-dialog>
        </div>
    </div>
</template>

<script>
export default {{
    name: '{tableName}',
    data() {{
        return {{
        fullscreenLoading: false,
               {tableName}TableLoading: false,
                mainTableHeight: 0,
               {tableName}Table: {{
                    data: [],
                    total: 1
                }},
               {tableName}Query:{tableQueryParams},
               {tableName}Request:{tableRequestParams},
               {optionData}
                showAdd{tableName}Layer: false,
                pickerOptions: {{
                    shortcuts: [
                        {{
                            text: '今天',
                            onClick(picker) {{
                                const end = new Date();
                                const start = new Date();
                                start.setTime(start.getTime());
                                picker.$emit('pick', [start, end]);
                            }}
                        }},
                        {{
                            text: '最近一周',
                            onClick(picker) {{
                                const end = new Date();
                                const start = new Date();
                                start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                                picker.$emit('pick', [start, end]);
                            }}
                        }}, {{
                            text: '这个月',
                            onClick(picker) {{
                                const end = new Date();
                                const start = new Date(new Date().getFullYear(), new Date().getMonth(), 1);
                                picker.$emit('pick', [start, end]);
                            }}
                        }},
                        {{
                            text: '上一个月',
                            onClick(picker) {{
                                const start = new Date(new Date().getFullYear(), new Date().getMonth() - 1, 1);
                                const end = new Date(new Date().getFullYear(), new Date().getMonth(), 1);
                                end.setDate(end.getDate() - 1);
                                picker.$emit('pick', [start, end]);
                            }}
                        }},
                        {{
                            text: '今年',
                            onClick(picker) {{
                                const start = new Date(new Date().getFullYear(), 0, 1);
                                const end = new Date();
                                picker.$emit('pick', [start, end]);
                            }}
                        }},
                    ]
                }}
        }};
    }},
    mounted() {{
                this.getTableList();
                this.fetTableHeight();
            }}, 
    methods: {{
                resetHeight(){{
                    return new Promise((resolve, reject) => {{
                        this.mainTableHeight = 0;
                        resolve();
                  }})
                }},
                fetTableHeight(){{
                    this.resetHeight().then(res => {{
                        this.mainTableHeight = window.innerHeight - this.getElementTop(document.getElementById('mainTable')) - 60;
                  }})
                }},
                handleCurrentChange(val) {{
                    this.{tableName}Query.pageIndex = val;
                    this.getTableList();
                }},
                handleSizeChange(val) {{
                    this.{tableName}Query.pageSize = val;
                    this.getTableList();
                }},
                closeAdd{tableName}Layer(){{
                    this.{tableName}Request.id = 0;
                    this.showAdd{tableName}Layer = false;
                }},
                delete{tableName}(row) {{
                    this.$confirm('确认删除?', '提示', {{
                        confirmButtonText: '确定',
                        cancelButtonText: '取消',
                        type: 'warning'
                  }}).then(() => {{
                        ajax.post('/Home/Delete{tableName}', JSON.stringify(this.{tableName}Request)).then((res) => {{
                            this.{tableName}TableLoading = false;
                            if (res.bodyText.includes('Error')) {{
                                console.log(res);
                                this.$message.error(res.bodyText);
                            }}
                            else {{
                                this.showAdd{tableName}Layer = false;
                                this.$message({{
                                    message: '成功',
                                    type: 'success'
                              }});
                            }}
                        }}, (res) => {{
                      }});
                  }}).catch(() => {{
                        this.$message({{
                            type: 'info',
                            message: '已取消删除'
                      }});
                  }});
                }},
                edit{tableName}(row) {{
                    this.showAdd{tableName}Layer = true;
                   {editGiveValue}
                }},
                submitAdd{tableName}Form(){{
                    ajax.post('/home/AddOrUpdate{tableName}', JSON.stringify(this.{tableName}Request)).then((res) => {{
                        this.{tableName}TableLoading = false;
                        if (res.bodyText.includes('Error')) {{
                            console.log(res);
                            this.$message.error(res.bodyText);
                        }}
                        else {{
                            this.showAdd{tableName}Layer = false;
                            this.$message({{
                                message: '成功',
                                type: 'success'
                          }});
                        }}
                    }}, (res) => {{
                  }});
                }},
               getElementTop(element){{
                    var actualTop = element.offsetTop;
                    var current = element.offsetParent;

                    while (current !== null) {{
                        actualTop += current.offsetTop;
                        current = current.offsetParent;
                    }}
                    return actualTop;
               }},
               getTableList(){{
                    this.{tableName}TableLoading = true;
                    ajax.post('/home/Get{tableName}List', JSON.stringify(this.{tableName}Query)).then((res) => {{
                        this.{tableName}TableLoading = false;
                        if (res.bodyText.includes('Error')) {{
                            console.log(res);
                            this.$message.error(res.bodyText);
                        }}
                        else {{
                            this.{tableName}Table.data = res.data.data;
                            this.{tableName}Table.total = res.data.count;
                        }}
                        this.{tableName}TableLoading = false;
                    }}, (res) => {{
                  }});
                }}
            }}



}}

</script>

 <style>
.el-table thead,
.el-table {{
    color: #333;
}}
.el-header {{
    padding: 0 0px;
}}

.el-form-item--mini.el-form-item,
.el-form-item--small.el-form-item {{
    margin-bottom: 10px;
}}

.el-dialog__header {{
    border-bottom: 1px solid #ebebeb;
    background-color: #f8f8f8;
}}

.el-card__body {{
    padding: 10px;
}}

[v-cloak] {{
    display: none;
}}
</style>
";

            return Json(html);
        }
    }
}