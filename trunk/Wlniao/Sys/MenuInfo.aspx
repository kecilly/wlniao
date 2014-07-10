<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuInfo.aspx.cs" Inherits="Wlniao.Sys.MenuInfo" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/static/om-apusic.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/static/operamasks-ui-all.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/static/jquery.json-2.4.min.js"></script>
    <script type="text/javascript" src="/static/html-helper.js"></script>
    <title>菜单内容设置</title>
    <style>
        body
        {
            padding: 0 20px;
        }
        #saveMenuItem{margin-left:180px;}
    </style>
</head>
<body>
    <div class="main-title">
        <h3><%= title %></h3>
    </div>
    <form class="form-horizontal" id="reply_form">
    <div class="control-group">
        <label class="control-label" for="shopname">
            菜单名称:</label>
        <div class="controls">
            <input type="text" id="menu_text" name="menu_text" style=" width:155px;" />
            <span class="maroon">*</span> <br /><span>二级菜单可输入8个字符！</span>
        </div>
    </div>
    <div id='res_block' class="form-horizontal">
        <div class="control-group">
            <label class="control-label" for="shopname">
                类型:</label>
            <div class="controls">
                <select id="menu_type" onchange="typeselect()" style=" width:128px;">
                    <option value="click" selected="selected">点击事件</option>
                    <option value="view">访问网页</option>
                </select>
                <select id="menu_value" onchange="valueselect()" style=" width:198px;">
                    <option value="" selected="selected">快速选择已有的Key值</option>
                    <%=_KeyWordList %>
                </select>
            </div>
        </div>
        <div id="r_text" class="control-group r-module">
            <label class="control-label" id="eventvalue">
                文本Key值:</label>
            <div class="controls">
                <input type="text" id="menu_key" name="menu_key" value="" style=" width:320px;" />
                </div>
            </div>
        </div>
    <button type="submit" class="btn btn-big btn-primary" id="saveMenuItem">
        保存</button>
    </form>
    <script>
        var item_id = <%= item_id %>;
        $(function () {
            $("#reply_form").validate({
                rules: {
                    menu_text: { required: true, maxlength: 8 }
                },
                messages: {
                    menu_text: { required: "菜单名称不可以为空！", maxlength: "最多只能输入" + 8 + "个字符！" }
                },
                showErrors: function (errorMap, errorList) {
                    if (errorList && errorList.length > 0) {
                        $.each(errorList,
					function (index, obj) {
					    var item = $(obj.element);
					    if (item.is(".cover")) {
					        alert(obj.message);
					    }
					    // 给输入框添加出错样式
					    item.closest(".control-group").addClass('error');
					    item.attr("title", obj.message);
					});
                    } else {
                        var item = $(this.currentElements);
                        item.closest(".control-group").removeClass('error');
                        item.removeAttr("title");
                    }
                },
                submitHandler: function () {
                    var menu_text = $("#menu_text").val();
                    var menu_type = $("#menu_type").val();
                    var menu_key = $("#menu_key").val();
                    var s_menu_text = $.htmlFilter(menu_text);
                    var s_menu_key = $.htmlFilter(menu_key);
                    if (menu_text == "") {
                        alert("请输入按钮名称！");
                        return false;
                    }
//                    if (menu_key == "") {
//                        if(menu_type=='click'){
//                            alert("请输入文本Key值！");
//                        }else{
//                            alert("请输入跳转的Url！");
//                        }
//                        return false;
//                    }
                    if (s_menu_text == "") {
                        alert("含有非法字符！");
                        return false;
                    }
//                    if (s_menu_key == "") {
//                        alert("含有非法字符！");
//                        return false;
//                    }
                    menu_text = s_menu_text;
                    menu_key = s_menu_key;
                    $("#menu_text").val(menu_text);
                    $("#menu_type").val(menu_type);
                    $("#menu_key").val(menu_key);
                    var submitData = {
                        item_id: item_id,
                        menu_text: menu_text,
                        menu_type: menu_type,
                        menu_key: menu_key
                    };
                    $("#saveMenuItem").attr("disabled", "disabled");
                    $.post("menuajax.aspx?do=saveMenuItem", submitData,
					function (data) {
					    $("#saveMenuItem").removeAttr("disabled");
					    if (data.success) {
					        var node = {
					            "text": data.menu_text,
					            "type": data.menu_type,
					            "key": data.menu_key,
					            "item_id": data.itemId
					        };
					        $tree = parent.$("#menu_tree");
					        item_id = data.itemId;
					        if (data.insert) {
					            parent.addNode("<%=nid %>", node);
					            var treedata = $.toJSON($tree.omTree("getData")[0].children);
					            $.post("menuajax.aspx?do=saveTreeData", { treeData: treedata }
								 , function (result) {
								     if (result.success) {
								         alert("成功添加菜单【" + data.menu_text + "】。");
								     }
								 }, "json");
					        } else {
					            var oldNode = $tree.omTree("findNode", "item_id", node.item_id);
					            oldNode.text = node.text;
					            oldNode.type = node.type;
					            oldNode.key = node.key;
					            parent.updateNode(oldNode);
					            var treedata = $.toJSON($tree.omTree("getData")[0].children);
					            $.post("menuajax.aspx?do=saveTreeData", { treeData: treedata }
								 , function (data) {
								     if (data.success) {
								         alert("保存成功！");
								     }
								 }, "json");
					        }
					    } else {
					        alert("保存菜单内容失败");
					    }
					}, "json");
                    return false;
                }
            });
        });
        function typeselect(){
            if($("#menu_type").val()=="click"){
                $("#eventvalue").html('文本Key值:');
                $("#menu_value").show();
            }else{
                $("#menu_value").hide();
                $("#eventvalue").html('跳转的Url:');
            }
        }
        function valueselect(){
            if($("#menu_value").val()){
            $("#menu_key").val($("#menu_value").val());
            }
        }

        // mycustom
        function getObjects(obj, key, val) {
            var objects = [];
            for (var i in obj) {
                if (!obj.hasOwnProperty(i)) continue;
                if (typeof obj[i] == 'object') {
                    objects = objects.concat(getObjects(obj[i], key, val));
                } else if (i == key && obj[key] == val) {
                    objects.push(obj);
                }
            }
            return objects;
        }

        try{
            var treeData = '<%= treeData %>';
            var treeobj = getObjects(eval(treeData), "item_id", <%=item_id %>);
            $("#menu_text").val(treeobj[0].text);
            $("#menu_type").val(treeobj[0].type);
            $("#menu_key").val(treeobj[0].key);
        }catch(e){}

    </script>
</body>
</html>
