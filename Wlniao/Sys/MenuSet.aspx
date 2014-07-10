<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuSet.aspx.cs" Inherits="Wlniao.Sys.MenuSet" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>自定义菜单设置</title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/static/om-apusic.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/static/operamasks-ui-all.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/static/jquery.json-2.4.min.js"></script>
    <style type="text/css">
body{background-color: #ffffff;}
.left{position: absolute;top:140px;left:20px;width:258px;border-right:1px solid #CCC;overflow-y: auto;}
.right{position: absolute;top:140px;left:280px;width:600px;overflow:auto;}
.tree-menu{
	float: right;
}
.tree-menu span{
	margin-left: 6px;
}
.tree-menu span i{
	cursor: pointer;
}
.icon-plus {
	background-position: -408px -96px;
}
.icon-remove {
	background-position: -312px 0;
}
.icon-edit {
	background-position: -96px -72px;
}
[class^="icon-"], [class*=" icon-"] {
	display: inline-block;
	width: 14px;
	height: 14px;
	line-height: 14px;
	vertical-align: text-top;
	background-image: url("/static/bootstrap/img/glyphicons-halflings.png");
	background-repeat: no-repeat;
	margin-top: 1px;
}
#menu_tree{
	margin-right: 20px;
}
.right iframe{
	height: 100%;
	width: 100%;
	z-index: 20;
	border: 0;
	margin: 0 auto;
	display: block;
	-moz-border-radius: 10px;
	-webkit-border-radius: 10px;
	border-radius: 10px;
}
li{
	line-height: 16px;
}
.om-tree-node a{
	display: inline-block;
	*display: inline;
	*zoom: 1;
	width: 115px;
	overflow: hidden;
	text-overflow: ellipsis;
}
#vip_tip{
	text-align: center;
}
.actions{
	position: absolute;
	bottom: 20px;
	left: 10px;
	width: 268px;
	border-right: 1px solid #CCC;
	height: 60px;
}
.actions .btn{
	position: relative;
	top: 30px;
}

    </style>
</head>
<body>
<div style="padding:0px 12px 0px 12px;">
    <div class="main-title">
        <h3 style="font-family:微软雅黑;">自定义菜单设置</h3>
    </div>
    <div id="top" class="alert alert-info">
        1.使用本功能必须先升级<strong>服务号</strong>并取得<strong>AppId和AppSecret</strong>，然后在【帐号接入信息】中设置。<br />
        2.最多创建<span class="red bold">3 个一级菜单</span>，每个一级菜单下最多可以创建 <span class="red bold">5 个二级菜单</span>，菜单<span
            class="red bold">最多支持两层</span>。<br />
        3.拖动树形菜单再点击<strong>“保存排序”</strong>可以对菜单重排序，但最终只有<strong>“发布”</strong>后才会生效。公众平台限制了每天的发布次数。
    </div>
    <div class="left">
        <ul id="menu_tree">
        </ul>
    </div>
    <div class="right">
        <iframe frameborder="0" id="detail" name="detail" src=""></iframe>
    </div>
    <div class="actions">
        <button class="btn btn-big btn-primary" id="sync">
            发布</button>
        <button class="btn btn-big btn-primary" id="preview" style=" display:none;" >
            下载</button>
        <button class="btn btn-big btn-primary" id="stop">
            停用</button>
        <button class="btn btn-big btn-primary" id="saveOrder">
            保存排序</button>
    </div>
</div>
    <script>
        var $tree;
        var LEVEL_1_COUNT = 3; //一级菜单最大数
        var LEVEL_2_COUNT = 5; //二级子菜单最大数

        $(function () {
            resize();
            $(window).resize(function () {
                resize();
            });

            var treeData = [{
                text: "我的自定义菜单",
                id: 0,
                rootid: 0
            }];
            treeData[0].children = eval('<%=treedata %>'); //{"text":"测试","item_id":4565,"nid":"menu_tree_2","children":[]}

            $tree = $("#menu_tree").omTree({
                dataSource: treeData,
                simpleDataModel: true,
                draggable: true,
                onDrag: function (nodeData, event) {
                    var level = $("#" + nodeData.nid).parents(".om-tree-node").length;
                    if (level == 0) {
                        return false; //根节点不可以拖拽
                    }
                },
                onDrop: function (nodeData, event) {
                    refreshActions();
                },
                onSelect: function () {
                    try {
                        var selNode = $("#menu_tree").omTree("getSelected");
                        var rootid = selNode["rootid"];
                        if (rootid != 0) {
                            $("#detail").attr("src", "MenuInfo.aspx?" + getCommonParam(selNode) + "&item_id=" + selNode.item_id);
                        }
                    } catch (e) { }
                }
            });
            $tree.omTree("expandAll");
            refreshActions();
            initSelect(); //默认选中第一个菜单 

            $tree.delegate(".tree-menu .add", "click", function () {
                var node = getNode(this);
                unselect();
                $("#detail").attr("src", "MenuInfo.aspx?" + getCommonParam(node) + "&add=true");
            });

            $tree.delegate(".tree-menu .edit", "click", function () {
                var selectedNode = $tree.omTree("findByNId", $(this).closest(".om-tree-node").attr("id"));
                $tree.omTree('select', selectedNode);
            });

            $tree.delegate(".tree-menu .del", "click", function () {
                var delNode = getNode(this);
                var selectedNode = $tree.omTree("getSelected");
                var children = delNode.children;
                if (children && children.length > 0) {
                    alert("菜单下边有子菜单，请先逐个删除子菜单！");
                    return;
                }

                if (confirm("请慎重该操作，删除后不可恢复，您真的要删除【" + delNode.text + "】吗？")) {
                    $tree.omTree("remove", delNode);
                    refreshActions();
                    if (selectedNode && delNode.hid == selectedNode.hid) {
                        $("#detail").attr("src", "MenuInfo.aspx");
                    }
                    var treeData = $.toJSON($tree.omTree("getData")[0].children);
                    $.post("menuajax.aspx?do=delMenuItem&itemId=" + delNode.item_id,
				{ treeData: treeData },
				function (result) {
				    if (!result.success) {
				        alert("删除菜单出错！");
				        location.reload();
				    }
				}, "json");
                }
            });

            $("#saveOrder").click(function () {
                var children = $tree.omTree("getData")[0].children;
                if (children && children.length > 0) {
                    for (var i = 0; i < children.length; i++) {
                        var level_one = children[i];
                        if (hasChild(level_one)) {
                            var sub_children = level_one.children;
                            for (var j = 0; j < sub_children.length; j++) {
                                //如果二级菜单下边还有子节点，则说明超过二层，不予保存
                                if (hasChild(sub_children[j])) {
                                    alert("公众平台规定，最多只支持两级菜单。");
                                    return false;
                                }
                            }
                        }
                    }
                }
                $("#saveOrder").attr("disabled", "disabled");
                var treedata = $.toJSON(children);
                $.post("menuajax.aspx?do=saveTreeData", { treeData: treedata }
			 , function (result) {
			     if (result.success) {
			         $("#saveOrder").removeAttr("disabled");
			         alert("保存排序成功！");
			     }
			 }, "json");
            });
            $("#preview").click(function () {
                $("#preview").attr("disabled", "disabled");
                $.post("menuajax.aspx?do=getMenu", {}, function (result) {
                    $("#sync").removeAttr("disabled");
                    if (result.success) {
                        alert("发布成功！");
                    } else {
                        if (result.errormsg) {
                            alert(result.errormsg);
                        }
                    }
                }, "json");
               // parent.showMenuMonitor();
            });
            $("#sync").click(function () {
                $("#sync").attr("disabled", "disabled");
                $.post("menuajax.aspx?do=syncMenu", {}
		 , function (result) {
		     $("#sync").removeAttr("disabled");
		     if (result.success) {
		         alert("发布成功！");
		     } else {
		         if (result.errormsg) {
		             alert(result.errormsg);
		         }
		     }
		 }, "json");
            });
            $("#stop").click(function () {
                $("#stop").attr("disabled", "disabled");
                $.post("menuajax.aspx?do=stopMenu", {}
		 , function (result) {
		     $("#stop").removeAttr("disabled");
		     if (result.success) {
		         alert("停用成功！");
		     } else {
		         if (result.errormsg) {
		             alert(result.errormsg);
		         }
		     }
		 }, "json");
            });
        });
        function hasChild(nodeData) {
            return nodeData.children && nodeData.children.length > 0;
        }
        function initSelect() {
            var data = $tree.omTree("getData");
            var child = data[0].children;
            if (child && child.length > 0) {
                $tree.omTree("select", child[0]);
            } else {
                $("#detail").attr("src", "MenuInfo.aspx");
            }
        }
        function resize() {
            var w = $(window).width(),
	h = $(window).height(),
	th = $("#top").outerHeight(true),
	mh = $(".main-title h3").outerHeight(true);
            $(".right").width(w - $(".left").width() - 40);
            $(".left").height(h - th - mh - 55);
            $(".right").height(h - th - mh - 5);
        }
        function unselect() {
            var selected = $tree.omTree("getSelected");
            if (selected) {
                $tree.omTree("unselect", selected);
            }
        }
        function getCommonParam(node) {
            return "nid=" + node.nid + "&level=" + ($("#" + node.nid).parents(".om-tree-node").length);
        }
        function getNode(target) {
            return $tree.omTree("findByNId", $(target).closest(".om-tree-node").attr("id"));
        }
        function refreshActions() {
            $("#menu_tree .om-tree-node span").each(function (index, span) {
                $(span).prev(".tree-menu").remove(); //先删除再重新创建
                var node = getNode(this);
                var menuHtml = '<div class="tree-menu">';
                var level = $(this).parents(".om-tree-node").length;
                var childCount = $tree.omTree("getChildren", node).length;
                if (level == 1) {
                    if (childCount < LEVEL_1_COUNT) {
                        $(span).find("a").width(130);
                        menuHtml += '<span><i class="icon-plus add" title="添加一级菜单"></i></span>';
                    }
                } else if (level == 2) {
                    if (childCount < LEVEL_2_COUNT) {
                        menuHtml += '<span><i class="icon-plus add" title="添加二级子菜单"></i></span>';
                    }
                    menuHtml += '<span><i class="icon-edit edit" title="编辑菜单"></i></span>';
                    menuHtml += '<span><i class="icon-remove del" title="删除菜单(其下没有子菜单才可以删除)"></i></span>';
                } else {
                    menuHtml += '<span><i class="icon-edit edit" title="编辑菜单"></i></span>';
                    menuHtml += '<span><i class="icon-remove del" title="删除菜单"></i></span>';
                }
                menuHtml += '</div>';
                $(span).before($(menuHtml));
            });
        }
        function addNode(nid, node) {
            var $pnode = $tree.omTree("findByNId", nid);
            $tree.omTree("insert", node, $pnode);
            refreshActions();
        }
        function updateNode(node) {
            $("#" + node.nid).find("a:eq(0)").text(node.text);
        }
        //图文选择回调
        function setSelectedArticle(resId) {
            window.frames["detail"].setSelectedArticle(resId);
        }
    </script>
</body>
</html>
