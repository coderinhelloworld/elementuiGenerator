using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LayuiTableGenerate.Models;
using LayuiTableGenerate.Repository;
using LayuiTableGenerate.Classes;
using Newtonsoft.Json;
using static LayuiTableGenerate.Models.LayuiPage;
using LayuiTableGenerate.Assemblys;
using LayuiTableGenerate.Enum;

namespace LayuiTableGenerate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            var n = new DateModule("test");
            var page = new LayuiPage
            {
                Body = "",
            };
            return View();
        }
        public IActionResult Test2()
        {

            var list = GetDataBaseHelper.GetMenuList();
            var page = new LayuiPage
            {
                Body = "",
            };
            return View();
        }
        public IActionResult Test()
        {

            var list = GetDataBaseHelper.GetMenuList();
            var page = new LayuiPage
            {
                Body = "",
            };
            return View();
        }

        public IActionResult ElementUITemplate()
        {

            var list = GetDataBaseHelper.GetMenuList();
            var page = new LayuiPage
            {
                Body = "",
            };
            return View();
        }
        enum SearchType{
            Input=1,
            Date=2,
            Option=3
        }


        public IActionResult CreateTable2(string columns,string tableName)
        {
            var settingList = JsonConvert.DeserializeObject<List<column>>(columns);
            var n = 2;
            var html = @"<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat='server'>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
    <title></title>
    <link crossorigin='anonymous' integrity='sha512-WYzwsHxfl8+qirF4V9ZyFULDBwxvm1kuCHlSd1F57JwALyQMxeq7j1blj4Y7pmP9r0tVMk7GYUI5xLDB7QIZCA==' href='https://lib.baomitu.com/element-ui/2.15.3/theme-chalk/index.min.css' rel='stylesheet'>
    <style>
        .el-table thead, .el-table {
            color: #333;
        }

        .el-header {
            padding: 0 0px;
        }

        .el-form-item--mini.el-form-item, .el-form-item--small.el-form-item {
            margin-bottom: 10px;
        }

        .el-dialog__header {
            border-bottom: 1px solid #ebebeb;
            background-color: #F8F8F8;
        }

        .el-card__body {
            padding: 10px;
        }

        [v-cloak] {
            display: none;
        }
    </style>
</head>
<body>
    <div id='app' v-cloak>
        <el-container v-loading.fullscreen.lock='fullscreenLoading'>
            <el-header style='height:auto'>
                <el-form :inline='true' @@keyup.enter.native='getTableList'>
                    {{searchForm}}
                    <el-form-item>
                        <el-button size='small' type='primary' icon='el-icon-search' @@click='getTableList'>查询</el-button>
                    </el-form-item>
                </el-form>
            </el-header>
            <el-row :gutter='10' style='margin-left:0px'>
                <el-button-group>
                    <el-button type='primary' @@click='showAdd{{tableName}}Layer=true' icon='el-icon-plus'>新增</el-button>
                </el-button-group>
            </el-row>
            <el-card class='box-card' style='margin-top:10px'>
                <template>
                    <el-table :data='{{tableName}}Table.data' v-loading='{{tableName}}TableLoading' style='width: 100%' border element-loading-background='rgba(255, 255, 255, 1)' :header-cell-style='{{headerCellStyle}}' element-loading-text='Loading' element-loading-spinner='el-icon-loading'>
                                                       {{tableColumns}}
                        <el-table-column fixed='right' show-summary width='240' label='操作' style='margin:5px'>
                            <template slot-scope='scope'>
                                <el-tooltip class='item' effect='dark' content='编辑' placement='top-end'>
                                    <el-button size='mini' type='success' @@click='edit{{tableName}}(scope.row)'><i class='el-icon-edit-outline'></i></el-button>
                                </el-tooltip>
                                <el-tooltip class='item' effect='dark' content='删除' placement='top-end'>
                                    <el-button size='mini' type='danger' @@click='delete{{tableName}}(scope.row)'><i class='el-icon-delete'></i></el-button>
                                </el-tooltip>
                            </template>
                        </el-table-column>
                    </el-table>
                    <el-pagination style='margin-top:10px' :page-size='{{tableName}}Query.pageSize' @@current-change='handleCurrentChange' :current-page.sync='{{tableName}}Query.pageIndex' background layout='total,prev, pager, next' :total='{{tableName}}Table.total'></el-pagination>
                </template>
            </el-card>
        </el-container>
        <div>
            <el-dialog title='编辑或新增' :visible.sync='showAdd{{tableName}}Layer'>
                <el-form label-width='80px' :inline='false' :model='{{tableName}}Request' ref='add{{tableName}}Layer'>
                    {{addNewDataForm}}
                </el-form>
                <div slot='footer' class='dialog-footer'>
                    <el-button type='primary' @@click="+ "\"submitAdd{{tableName}}Form('add{{tableName}}Layer')\"" + @">确 认</el-button>
                    <el-button @@click='closeAdd{{tableName}}Layer'>取 消</el-button>
                </div>
            </el-dialog>
        </div>
    </div>
    <script crossorigin='anonymous' integrity='sha512-pSyYzOKCLD2xoGM1GwkeHbdXgMRVsSqQaaUoHskx/HF09POwvow2VfVEdARIYwdeFLbu+2FCOTRYuiyeGxXkEg==' src='https://lib.baomitu.com/vue/2.6.14/vue.js'></script>
    <script crossorigin='anonymous' integrity='sha512-+mrl1yz9SfyUGXnOiA1ZinMC+NAtmqccFL49hjdNljqBYqopcgRO7LLRd+rK+kmc9+4FG75bBkEhjBaiV3jj8w==' src='https://lib.baomitu.com/element-ui/2.15.3/index.js'></script>
    <script crossorigin='anonymous' integrity='sha512-SzBVU+C0eJl+4BIg29X72SYgYNUI2bNVVUFfG9eg1g2jOMvBy3w2movesVS0u+IslOgFyRxuVrMrSHcnLPLN+Q==' src='https://lib.baomitu.com/vue-resource/1.5.3/vue-resource.min.js'></script>
    <script>
        Vue.prototype.$ELEMENT = { size: 'small', zIndex: 3000 };
        var ajax = Vue.http;
        var app = new Vue({
            el: '#app'
            , data: {
                fullscreenLoading: false,
                {{tableName}}TableLoading: false,
                {{tableName}}Table: {
                    data: [],
                    total: 1
                },
                {{tableName}}Query: {{tableQueryParams}},
                {{tableName}}Request: {{tableRequestParams}},
                showAdd{{tableName}}Layer: false,
                pickerOptions: {
                    shortcuts: [
                        {
                            text: '今天',
                            onClick(picker) {
                                const end = new Date();
                                const start = new Date();
                                start.setTime(start.getTime());
                                picker.$emit('pick', [start, end]);
                            }
                        },
                        {
                            text: '最近一周',
                            onClick(picker) {
                                const end = new Date();
                                const start = new Date();
                                start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                                picker.$emit('pick', [start, end]);
                            }
                        }, {
                            text: '这个月',
                            onClick(picker) {
                                const end = new Date();
                                const start = new Date(new Date().getFullYear(), new Date().getMonth(), 1);
                                picker.$emit('pick', [start, end]);
                            }
                        },
                        {
                            text: '上一个月',
                            onClick(picker) {
                                const start = new Date(new Date().getFullYear(), new Date().getMonth() - 1, 1);
                                const end = new Date(new Date().getFullYear(), new Date().getMonth(), 1);
                                end.setDate(end.getDate() - 1);
                                picker.$emit('pick', [start, end]);
                            }
                        },
                        {
                            text: '今年',
                            onClick(picker) {
                                const start = new Date(new Date().getFullYear(), 0, 1);
                                const end = new Date();
                                picker.$emit('pick', [start, end]);
                            }
                        },
                    ]
                },
            }
            , mounted() {
                this.getTableList();
            }
            , methods: {
                handleCurrentChange(val) {
                    this.{{tableName}}Query.pageIndex = val;
                    this.getTableList();
                },
                closeAdd{{tableName}}Layer() {
                    this.{{tableName}}Request.id = 0;
                    this.showAdd{{tableName}}Layer = false;
                },
                delete{{tableName}}(row) {
                    this.$confirm('确认删除?', '提示', {
                        confirmButtonText: '确定',
                        cancelButtonText: '取消',
                        type: 'warning'
                    }).then(() => {
                        ajax.post('/Home/Delete{{tableName}}', JSON.stringify(this.{{tableName}}Request)).then((res) => {
                            this.{{tableName}}TableLoading = false;
                            if (res.bodyText.includes('Error')) {
                                console.log(res);
                                this.$message.error(res.bodyText);
                            }
                            else {
                                this.showAdd{{tableName}}Layer = false;
                                this.$message({
                                    message: '成功',
                                    type: 'success'
                                });
                            }
                        }, (res) => {
                        });
                    }).catch(() => {
                        this.$message({
                            type: 'info',
                            message: '已取消删除'
                        });
                    });
                },
                edit{{tableName}}(row) {
                    this.showAdd{{tableName}}Layer = true;
                    {{editGiveValue}}
                },
                submitAdd{{tableName}}Form() {
                    ajax.post('/home/AddOrUpdate{{tableName}}', JSON.stringify(this.{{tableName}}Request)).then((res) => {
                        this.{{tableName}}TableLoading = false;
                        if (res.bodyText.includes('Error')) {
                            console.log(res);
                            this.$message.error(res.bodyText);
                        }
                        else {
                            this.showAdd{{tableName}}Layer = false;
                            this.$message({
                                message: '成功',
                                type: 'success'
                            });
                        }
                    }, (res) => {
                    });
                },
                getTableList() {
                    this.{{tableName}}TableLoading = true;
                    ajax.post('/home/Get{{tableName}}List', JSON.stringify(this.{{tableName}}Query)).then((res) => {
                        this.{{tableName}}TableLoading = false;
                        if (res.bodyText.includes('Error')) {
                            console.log(res);
                            this.$message.error(res.bodyText);
                        }
                        else {
                            this.{{tableName}}Table.data = res.data.data;
                            this.{{tableName}}Table.total = res.data.count;
                        }
                        this.{{tableName}}TableLoading = false;
                    }, (res) => {
                    });
                }
            }
            , updated() {
            }
        });
    </script>
</body>
</html>
";


            var searchForm = "";
            var addNewDataForm = "";
            var tableQueryParams = "{";
            var tableRequestParams = "{";
            var tableColumns = "";
            var headerCellStyle = "{background:\"#eef1f6\",color:\"#606266\"}";
            var editGiveValue = "";

            foreach (var item in settingList)
            {
                //搜索的时候
                if (item.SearchForm)
                {
                    tableQueryParams += item.ColumnTitle+":'',";
                    if (item.InputType== FormInputType.Date.ToString())
                    {
                        var dateForm=new DateModule(tableName+"Query."+ item.ColumnTitle);
                        searchForm +=dateForm.Value + "\r\n"; 
                    }
                    if (item.InputType == FormInputType.Input.ToString())
                    {
                        var inputForm = new InputModule(tableName + "Query." + item.ColumnTitle, item.ColumnDes);
                        searchForm += inputForm.Value + "\r\n"; 
                    }
                    if (item.InputType == FormInputType.Option.ToString())
                    {
                        var optionForm = new OptionModule(tableName + "Query." + item.ColumnTitle,tableName+"Query"+item.ColumnTitle+"Options", item.ColumnDes);
                        searchForm += optionForm.Value + "\r\n"; 
                    }
                }
                //新增的时候
                if (item.InputForm)
                {
                    tableRequestParams += item.ColumnTitle + ":'',";
                    editGiveValue += "this."+tableName+"Request."+item.ColumnTitle+"=row."+item.ColumnTitle+";"+ "\r\n";
                    if (item.InputType == FormInputType.Date.ToString())
                    {
                        var dateForm = new DateModule(tableName + "Request." + item.ColumnTitle,item.ColumnDes);
                        addNewDataForm += dateForm.Value+"\r\n";
                    }
                    if (item.InputType == FormInputType.Input.ToString())
                    {
                        var inputForm = new InputModule(tableName + "Request." + item.ColumnTitle, item.ColumnDes, item.ColumnDes);
                        addNewDataForm += inputForm.Value + "\r\n"; ;
                    }
                    if (item.InputType == FormInputType.Option.ToString())
                    {
                        var optionForm = new OptionModule(tableName + "Request." + item.ColumnTitle, tableName + "Query" + item.ColumnTitle + "Options", item.ColumnDes,item.ColumnDes);
                        addNewDataForm += optionForm.Value + "\r\n"; ;
                    }
                }

                //显示在表格中
                if (item.ShowInTable)
                {
                    var column = new ColumnModule(item.ColumnTitle,item.ColumnDes);
                    tableColumns += column.Value + "\r\n"; ;
                }


            }


            tableQueryParams = tableQueryParams.TrimEnd(',') + "}";
            tableRequestParams = tableRequestParams.TrimEnd(',') + "}";

            html= html.Replace("{{searchForm}}", searchForm);
            html= html.Replace("{{addNewDataForm}}", addNewDataForm);
            html= html.Replace("{{tableQueryParams}}", tableQueryParams);
            html= html.Replace("{{tableRequestParams}}", tableRequestParams);
            html = html.Replace("{{tableColumns}}", tableColumns);
            html =html.Replace("{{tableName}}", tableName);
            html =html.Replace("{{headerCellStyle}}", headerCellStyle);
            html =html.Replace("{{editGiveValue}}", editGiveValue);
      



            return Json(html);

        }


        //创建Layui表单
        public IActionResult createTable(int dbType, string dbCon, string dbTable)
        {


            var columnList = GetDataBaseHelper.GetColumnList(dbType, dbCon, dbTable);
            var formItem = "";
            string colsFields = "";

            foreach (var item in columnList)
            {



                formItem += @"<div class='layui-form-item'>
                                <label class='layui-form-label'>" + (item.ColumnDes == "" ? item.ColumnTitle : item.ColumnDes) + @"</label>
                                <div class='layui-input-block'>
                                    <input type='text' name='" + item.ColumnTitle + @"' placeholder='请输入' required lay-verify='required' autocomplete='off' class='layui-input'>
                                </div>
                             </div>";


            };

            foreach (var item in columnList)
            {
                colsFields = colsFields + ", { field: '" + item.ColumnTitle + "', title: '" + (item.ColumnDes == "" ? item.ColumnTitle : item.ColumnDes) + "' }\n";
            }


            var tableName = $"{dbTable}Table";
            var tableParamsLayer = $"{dbTable}ParamsLayer";
            var tableInputForm = $"{dbTable}InputForm";
            var submitFormButton = $"{dbTable}SubmitFormButton";
            var sttingLayer = $"{dbTable}SettingLayer";

            var html = @"<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />

    <link href='~/lib/layui/css/layui.css' rel='stylesheet' />

    <script src='~/lib/jquery/dist/jquery.js'></script>

    <script src='~/lib/layui/layui.js'></script>
    <script src='~/js/xm-select.js'></script>
</head>
<body style='padding: 20px'>

    <fieldset class='layui-elem-field'>
        <legend>搜索</legend>
        <div class='layui-field-box'>
            <form id='dataSerarch' class='layui-form' lay-filter='dataSerarch'>
                <div class='layui-form-item'>

                    <div class='layui-inline'>
                        <label class='layui-form-label'>搜索内容</label>
                        <div class='layui-input-inline'>
                            <input type='text' name='searchKey' id='searchKey' placeholder='' autocomplete='off' class='layui-input'>
                        </div>
                    </div>

                    <div class='layui-inline searchData'>
                        <button class='layui-btn' type='button' lay-submit lay-filter='dataSerarch' data-type='reload' function='query'><i class='layui-icon'></i>查询</button>
                    </div>
                </div>
            </form>
        </div>
    </fieldset>

    <script type='text/html' id='headToolBar'>
        <div class='layui-btn-container'>
            <button class='layui-btn layui-btn-sm' lay-event='add'><i class='layui-icon'>&#xe624</i>新增</button>
        </div>
    </script>
    <div class='col-md-12'>
        <table id='" + tableName + @"' lay-filter='" + tableName + @"'></table>
    </div>

    <script type='text/html' id='endToolBar'>
        <a class='layui-btn layui-btn-xs' lay-event='edit'>编辑</a>
        <a class='layui-btn layui-btn-danger layui-btn-xs' lay-event='del'>删除</a>
    </script>

    <div id='" + tableParamsLayer + @"' style='display: none; padding: 20px'>
        <form id='" + tableInputForm + @"' class='layui-form' lay-filter='" + tableInputForm + @"'>" +
            formItem +
            @"<div class='layui-form-item'>
                <div class='layui-input-block'>
                    <button class='layui-btn' lay-submit lay-filter='" + submitFormButton + @"'>提交</button>
                    <button type='reset' id='resetButton' class='layui-btn layui-btn-primary resetButton'>重置</button>
                </div>
            </div>
            <div class='layui-form-item' style='display: none'>
                <div class='layui-input-block'>
                    <input type='text' id='Id' name='Id' value='0' autocomplete='off' class='layui-input'>
                </div>
            </div>
        </form>
    </div>
    <script>
        layui.use(['form', 'table'], function () {
            var form = layui.form, table = layui.table;
            table.render({
                elem: '#" + tableName + @"'
                , height: 600
                , url: '/" + dbTable + @"/List'
                , cellMinWidth: 80
                , page: true
                , limit: 10
                , cols: [[
                    { type: 'checkbox' }
" +
                    colsFields +
                    @", { fixed: 'right', align: 'center', toolbar: '#endToolBar' }
                ]]
                , toolbar: '#headToolBar'
            });
            table.on('toolbar(" + tableName + @")', function (obj) {
                switch (obj.event) {
                    case 'add':
                        " + sttingLayer + @"();
                        break;
                };
            });
            table.on('tool(" + tableName + @")', function (obj) {
                var data = obj.data;
                switch (obj.event) {
                    case 'edit':
                        " + sttingLayer + @"();
                        layui.form.val('" + tableInputForm + @"', {
                            Id: data.Id,
                            CategoryName: data.CategoryName,
                            SortNo: data.SortNo,
                        }); break;
                    case 'del':
                        layer.confirm('真的删除行么', function (index) {
                            obj.del();
                            layer.close(index);
                            $.ajax({
                                url: '/" + dbTable + @"/Delelte',
                                data: data,
                                success: function (res) {
                                    layer.msg('保存成功', { icon: 1 });
                                    table.reload('" + tableName + @"');
                                },
                                error: function () {
                                    layer.msg('失败', { icon: 2 });
                                    layer.close(loading);
                                }
                            })
                        }); break;
                    default: break;
                }
            });
            form.on('submit(" + submitFormButton + @")', function (data) {
                var loading = layer.load(0);
                $.ajax({
                    url: '/" + dbTable + @"/Set',
                    data: data.field,
                    success: function (res) {                     
                        layer.close(settingLayerIndex);
                        layer.msg('保存成功', { icon: 1 });
                        layui.table.reload('" + tableName + @"');
                        $('#" + tableInputForm + @"')[0].reset();
                        layui.form.render();
                        layer.close(loading);                        
                    },
                    error: function() {
                        layer.msg('失败', { icon: 2 });
                        layer.close(loading);
                    }                    
                })
                return false;
            });
            form.on('submit(dataSerarch)', function (data) {
                var loading = layer.load(0);
                table.reload('" + tableName + @"', {
                    page: {curr: 1  }
                    , where: {searchKey: data.field }
                }, 'data');
                layer.close(loading);      
                return false;
            });

        });
        var settingLayerIndex;
        function " + sttingLayer + @"() {
            settingLayerIndex = layer.open({
                area: '1024px',
                type: 1,
                shade: [0.8, '#393D49'],
                title: '类别' + '设置',
                content: $('#" + tableParamsLayer + @"'),
                cancel: function () {
                    $('#" + tableInputForm + @"')[0].reset();
                    layui.form.render();
                }
                , btn: false
            });
        }
    </script>
</body>
</html>
";
            return Json(html);

        }
        //获取数据库表名
        public IActionResult DbTableList(int dbType, string dbCon, string dbTable)
        {
            var list = GetDataBaseHelper.GetDataBaseTable(dbCon, dbType);
            return Json(list);


        }

        public IActionResult Template()
        {

            return View();
        }
        public IActionResult KnowledgeTypeList()
        {
            var list = GetDataBaseHelper.GetKnowledgeTypeList();
            return Json(new { code = 0, msg = "", count = 20, data = list });
        } 

        public class Query
        {

            public string datePeriod { get; set; }
            public int pageSize { get; set; }
            public int pageIndex { get; set; }
            public string  shopMarket { get; set; }
        }
        [HttpPost]
        public IActionResult GetMenuList([FromBody]Query data)
        {
            var list = GetDataBaseHelper.GetMenuList();
            return Json(new { code = 0, msg = "", count = 20, data = list });
        }

        public class Request
        {

            public int id { get; set; }
            public string title { get; set; }
        }
        [HttpPost]
        public IActionResult AddOrUpdateMenu([FromBody] Request Request)
        {
            var list = GetDataBaseHelper.GetMenuList();
            return Json(new { code = 0, msg = "", count = 20, data = list });
        }       
        [HttpPost]
        public IActionResult DeleteMenu([FromBody] Request Request)
        {
            var list = GetDataBaseHelper.GetMenuList();
            return Json(new { code = 0, msg = "", count = 20, data = list });
        }

        public IActionResult SetKnowledgeType(Knowledgetype knowledgetype)
        {

            var list = GetDataBaseHelper.SetKnowledgeTypeList(knowledgetype);
            return Json(list);
        }
        public IActionResult DelelteKnowledgeType(Knowledgetype knowledgetype)
        {
            GetDataBaseHelper.DelelteKnowledgeType(knowledgetype);
            return Json("");
        }

        public IActionResult GetTableName(int dbType, string dbCon, string dbTable)
        {
            var columnList = GetDataBaseHelper.GetColumnList(dbType, dbCon, dbTable);
            return Json(new { code = 0, msg = "", count = 10, data = columnList });
        }
    }
}
