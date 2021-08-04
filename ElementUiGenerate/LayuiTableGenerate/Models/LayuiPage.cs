using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayuiTableGenerate.Models
{
    public class LayuiPage
    {

        public string Page { get => Head + Body + Footer;}
        private string Head
        {
            get { return @"<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />

    <link href='~/lib/layui/css/layui.css' rel='stylesheet' />

    <script src='~/lib/jquery/dist/jquery.js'></script>

    <script src='~/lib/layui/layui.js'></script>
    <script src='~/js/xm-select.js'></script>
</head>
<body style='padding: 20px'>"; }

        }

        public string Body { get; set; }

        private string Footer
        {
            get { return @"</body></html>"; }

        }


    }


    public class Search
    {
        private string _Head;
        private string _Footer;
        private string _Body;
        public string Body { get=> _Body; set=> _Body=this._Head+this.Items + this.Footer; }
        public string Head { get => _Head; set => _Head = @"
    <fieldset class='layui-elem-field'>
        <legend>搜索</legend>
        <div class='layui-field-box'>
            <form id='dataSerarch' class='layui-form' lay-filter='dataSerarch'>
                <div class='layui-form-item'>
"; }
        public string Items { get; set; }
        public string Footer { get=>_Footer; set=>_Footer= @"<div class='layui-inline searchData'>
                        <button class='layui-btn' type='button' lay-submit lay-filter='dataSerarch' data-type='reload' function='query'><i class='layui-icon'></i>查询</button>
                    </div>
                </div>
            </form>
        </div>
    </fieldset>"; }
    }


    public class Script
    {
        private string _Body;
        private string _tableName;
        public string _tableParamsLayer;
        public string _tableInputForm;
        public string _formItem;
        public string _submitFormButton;
        public string _dbTable;
        public string _colsFields;
        public string _sttingLayer;

        public string SttingLayer { get => _sttingLayer; set => _sttingLayer = value; }
        public string ColsFields { get => _colsFields; set => _colsFields = value; }
        public string DbTable { get => _dbTable; set => _dbTable = value; }
        public string SubmitFormButton { get => _submitFormButton; set => _submitFormButton = value; }
        public string TableInputForm { get => _tableInputForm; set => _tableInputForm = value; }
        public string TableName { get=> _tableName; set=>_tableName=value; }
        public string TableParamsLayer { get => _tableParamsLayer; set => _tableParamsLayer = value; }
      

        public string Body { get=>_Body; set=> _Body= @"<script type='text/html' id='headToolBar'>
        <div class='layui-btn-container'>
            <button class='layui-btn layui-btn-sm' lay-event='add'><i class='layui-icon'>&#xe624</i>新增</button>
        </div>
    </script>
    <div class='col-md-12'>
        <table id='" + _tableName + @"' lay-filter='" + _tableName + @"'></table>
    </div>

    <script type='text/html' id='endToolBar'>
        <a class='layui-btn layui-btn-xs' lay-event='edit'>编辑</a>
        <a class='layui-btn layui-btn-danger layui-btn-xs' lay-event='del'>删除</a>
    </script>

    <div id='" + _tableParamsLayer + @"' style='display: none; padding: 20px'>
        <form id='" + _tableInputForm + @"' class='layui-form' lay-filter='" + _tableInputForm + @"'>" +
            _formItem +
            @"<div class='layui-form-item'>
                <div class='layui-input-block'>
                    <button class='layui-btn' lay-submit lay-filter='" + _submitFormButton + @"'>提交</button>
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
                elem: '#" + _tableName + @"'
                , height: 600
                , url: '/" + _dbTable + @"/List'
                , cellMinWidth: 80
                , page: true
                , limit: 10
                , cols: [[
                    { type: 'checkbox' }
" +
                    _colsFields +
                    @", { fixed: 'right', align: 'center', toolbar: '#endToolBar' }
                ]]
                , toolbar: '#headToolBar'
            });
            table.on('toolbar(" + _tableName + @")', function (obj) {
                switch (obj.event) {
                    case 'add':
                        " + _sttingLayer + @"();
                        break;
                };
            });
            table.on('tool(" + _tableName + @")', function (obj) {
                var data = obj.data;
                switch (obj.event) {
                    case 'edit':
                        " + _sttingLayer + @"();
                        layui.form.val('" + _tableInputForm + @"', {
                            Id: data.Id,
                            CategoryName: data.CategoryName,
                            SortNo: data.SortNo,
                        }); break;
                    case 'del':
                        layer.confirm('真的删除行么', function (index) {
                            obj.del();
                            layer.close(index);
                            $.ajax({
                                url: '/" + _dbTable + @"/Delelte',
                                data: data,
                                success: function (res) {
                                    layer.msg('保存成功', { icon: 1 });
                                    table.reload('" + _tableName + @"');
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
            form.on('submit(" + _submitFormButton + @")', function (data) {
                var loading = layer.load(0);
                $.ajax({
                    url: '/" + _dbTable + @"/Set',
                    data: data.field,
                    success: function (res) {                     
                        layer.close(settingLayerIndex);
                        layer.msg('保存成功', { icon: 1 });
                        layui.table.reload('" + _tableName + @"');
                        $('#" + _tableInputForm + @"')[0].reset();
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
                table.reload('" + _tableName + @"', {
                    page: {curr: 1  }
                    , where: {searchKey: data.field }
                }, 'data');
                layer.close(loading);      
                return false;
            });

        });
        var settingLayerIndex;
        function " + _sttingLayer + @"() {
            settingLayerIndex = layer.open({
                area: '1024px',
                type: 1,
                shade: [0.8, '#393D49'],
                title: '类别' + '设置',
                content: $('#" + _tableParamsLayer + @"'),
                cancel: function () {
                    $('#" + _tableInputForm + @"')[0].reset();
                    layui.form.render();
                }
                , btn: false
            });
        }
    </script>"; }  


    }



    public class InputItem
    {
        private string _InputTitle;
        private string _InputName;
        private string _InputBody;


        public string Title
        {
            get { return this._InputTitle; }
            set
            {

                this._InputTitle = value;
                this._InputBody = @" < div class='layui-inline'>
                        <label class='layui-form-label'>" + this._InputTitle + @"</label>
                        <div class='layui-input-inline'>
                            <input type='text' name='" + this._InputName + @" id='" + this._InputName + @"' placeholder='' autocomplete='off' class='layui-input'>
                        </div>
                    </div>";
            }
        }
        public string InputName
        {
            get { return this._InputName; }
            set
            {
                this._InputName = value;
                this._InputBody = @"<div class='layui-inline'>
                        <label class='layui-form-label'>" + this._InputTitle + @"</label>
                        <div class='layui-input-inline'>
                            <input type='text' name='" + this._InputName + @" id='" + this._InputName + @"' placeholder='' autocomplete='off' class='layui-input'>
                        </div>
                    </div>";
            }
        }
        public string InputBody
        {
            get { return this._InputBody; }
        }
    }
}
