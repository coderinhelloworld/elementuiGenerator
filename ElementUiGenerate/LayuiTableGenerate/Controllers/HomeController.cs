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
        public IActionResult CreateTable2(string columns)
        {
            var settingList = JsonConvert.DeserializeObject<List<column>>(columns);
            var n = 2;
            var html = @"<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat='server'>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
    <title></title>
    <link href='../../css/element-ui.css' rel='stylesheet' />
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
    </style>
</head>
<body>
    <div id='app'>
        <el-container v-loading.fullscreen.lock='fullscreenLoading'>
                 <el-header style='height:auto'>
              <%--查询界面--%>
                <div>
                        <el-form :inline='true'  @keyup.enter.native='getReportStatus(1)'>
                           <el-form-item >
                                 <el-date-picker value-format='yyyy-MM-dd' v-model='query.datePeriod' unlink-panels  type='daterange'  unlink-panels  range-separator='至' start-placeholder='开始日期' end-placeholder='结束日期' :picker-options='pickerOptions'>
                                  </el-date-picker>
                            </el-form-item>
                            <el-form-item  >
                                <el-select  v-model='query.market' placeholder='市场' clearable>
                                    <el-option  v-for='(item,index) in query.shopMarkets' :key='index' :label='item' :value='item'> </el-option>
                                </el-select>
                           </el-form-item>
                             <el-form-item >
                                <el-input   placeholder='文件名' v-model='query.fileName'></el-input>
                              </el-form-item>
                            <el-form-item>
                                <el-button size='small' type='primary' icon='el-icon-search' @click='getReportStatus(1)' >查询</el-button>
                            </el-form-item>
                        </el-form>
                       </div>
                   </el-header>
                  <el-row :gutter='10' style='margin-left:0px'>
                  <el-button  size='small' @click='showDownloadLayer()' plain>下载报告ID</el-button>      
                  </el-row>
          <%--表格界面--%>
            <el-card class='box-card' style='margin-top:10px'>
                 <template>
                              <el-table :data='promotionTable.data' style='width: 100%'  border  element-loading-background = 'rgba(0, 0, 0, 0.1)'  :header-cell-style='{background:'#eef1f6',color:'#606266'}' element-loading-text = 'Loading' element-loading-spinner = 'el-icon-loading'>
                                <el-table-column  :show-overflow-tooltip='true' prop='date' label='修改日期'  width='180' ></el-table-column>
                                <el-table-column  :show-overflow-tooltip='true' prop='name' label='报告状态'  ></el-table-column>
                                <el-table-column  :show-overflow-tooltip='true' prop='address' label='报告状态'  ></el-table-column>
                               <el-table-column      fixed='right' show-summary  width='240'  label='操作' style='margin:5px'  >
                                         <el-tooltip class='item' effect='dark' content='删除' placement='top-end'>
                                                <el-button size='mini' type='danger'  @click='deleteReport(scope.row)'><i class='el-icon-delete'></i></el-button>
                                         </el-tooltip>                              
                               </el-table-column>
                              </el-table>
                              <el-pagination style='margin-top:10px' :page-size ='query.pageSize'  @current-change='handleCurrentChange' :current-page.sync='query.pageIndex' background layout='total,prev, pager, next':total='promotionTable.total'></el-pagination>
                   </template>
              </el-card>
        </el-container>
    </div>
    <script src='../../Scripts/vue.js'></script>
    <script src='../../Scripts/element-ui.js'></script>
    <script src='../../Scripts/vue-resource.min.js'></script>
    <script>
        Vue.prototype.$ELEMENT = { size: 'small', zIndex: 3000 };
        var app = new Vue({
            el: '#app'
            , data: {
                fullscreenLoading: false,
                promotionTable: {
                    data: [{
                        date: '2016-05-02',
                        name: '王小虎',
                        address: '上海市普陀区金沙江路 1518 弄'
                    }, {
                        date: '2016-05-04',
                        name: '王小虎',
                        address: '上海市普陀区金沙江路 1517 弄'
                    }, {
                        date: '2016-05-01',
                        name: '王小虎',
                        address: '上海市普陀区金沙江路 1519 弄'
                    }, {
                        date: '2016-05-03',
                        name: '王小虎',
                        address: '上海市普陀区金沙江路 1516 弄'
                    }],
                    total: 0
                },
                query: {
                    datePeriod: '',
                    pageSize: 10,
                    shopMarkets: [1, 2, 3]
                },
                pickerOptions: {
                    shortcuts: [{
                        text: '最近一周',
                        onClick(picker) {
                            const end = new Date();
                            const start = new Date();
                            start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                            picker.$emit('pick', [start, end]);
                        }
                    }, {
                        text: '上一个月',
                        onClick(picker) {
                            const start = new Date(new Date().getFullYear(), new Date().getMonth() - 1, 1);
                            const end = new Date(new Date().getFullYear(), new Date().getMonth(), 1);
                            end.setDate(end.getDate() - 1);
                            picker.$emit('pick', [start, end]);
                        }
                    }, {
                        text: '这个月',
                        onClick(picker) {
                            const end = new Date();
                            const start = new Date(new Date().getFullYear(), new Date().getMonth(), 1);
                            picker.$emit('pick', [start, end]);
                        }
                    }]
                },
            }
            , mounted() {
            }
            , methods: {
                handleCurrentChange: function () {
                }
            }
            , updated() {
            }
        });
    </script>
</body>
</html>
";











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
