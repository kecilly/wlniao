<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MPUserInfo.aspx.cs" Inherits="Wlniao.Sys.MPUserInfo" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>我的用户列表</title>
    <link href="/static/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/static/wlniao-style.css" rel="stylesheet" type="text/css" />
    <script src="/static/jquery.js" type="text/javascript"></script>
    <script src="/static/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/static/wln.js" type="text/javascript"></script>
    <style type="text/css">
        label{ width:80px; display:inline-block; text-align:right;}
        .aedit{width:158px; display:inline-block;color:gray; cursor:pointer; background-repeat:no-repeat; background-position:center right;}
        .aedit:hover{color:blue; background-image:url("/static/img/icon-edit.png");}
    </style>
</head>
<body>
    <div class="widget-box" style=" margin:10px;">
            <div class="widget-title">
            <div style=" width:300px; text-align:right; float:right; padding:3px;">
                <a class="btn btn-primary" href="javascript:void(0);" onclick="JAVASCRIPT:reLoadData();" style=" float:right; margin-right:5px;">刷新</a> 
            </div>
            <h5>我的用户列表</h5>
        </div>               
        <table id="dataTable" class="wlntable">
            <tr>
                <td style="width:158px; text-align:center;" filed="NickName" function="onNickName">昵称（备注）</td>
                <td style="width:98px; text-align:center;" filed="MobileNumber">手机号</td>
                <td style="width:40px; text-align:center;" filed="Sex" function="onSex">性别</td>
                <td style="width:40px; text-align:center;" filed="SubscribeTime" function="onSubscribeTime">关注</td>
                <td style="width:68px; text-align:center;" filed="City">城市</td>
                <td style="width:auto; text-align:center;" filed="LastVisitTime">最后来访时间</td>
                <td style="width:120px;" function="onAction">操作</td>
            </tr>
        </table>
        <div id="RemarknameModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="RemarknameModalLabel" aria-hidden="true">
            <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="RemarknameModalLabel">修改用户备注</h3>
            </div>
            <div class="modal-body">
            <label>备注名称：</label><input type="text" id="RemarknameTxt" class="txt grid-4 alpha pin" />
            </div>
            <div class="modal-footer">
            <input type="hidden" id="RemarknameId" value="" />
            <button class="btn" data-dismiss="modal" aria-hidden="true">关闭</button>
            <button class="btn btn-primary" onclick="return RemarknameSave();">确定</button>
            </div>
        </div>
        <div id="sendMsg" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="SendMsgLabel" aria-hidden="true">
            <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="SendMsgLabel">发送消息</h3>
            </div>
            <div class="modal-body" style=" padding:0px;">
            <textarea id="SendMsgText" cols="55" rows="" class="txt content" style="height:98px; width:546px; margin-bottom:0px;"></textarea>
            </div>
            <div class="modal-footer">
            <input type="hidden" id="SendMsgId" value="" />
            <button class="btn" data-dismiss="modal" aria-hidden="true">关闭</button>
            <button class="btn btn-primary" onclick="return SendMsg();">立即发送</button>
            </div>
        </div>
        <div id="editMPUserInfo" class="modal hide fade" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3>编辑用户</h3>
            </div>
            <div class="modal-body">
                <div>
                <label>备注名称：</label><input type="text" id="RemarknameTxt2" class="txt grid-4 alpha pin" />
                </div>
                <div>
                <label>手机号：</label><input type="text" id="MobileNumber" class="txt grid-4 alpha pin" />
                </div>
                <div>
                <label>性别：</label><select id="Sex"><option value="0">未知</option><option value="1">男</option><option value="2">女</option></select>
                </div>
                <div>
                <label>所在省份：</label><input type="text" id="Province" class="txt grid-4 alpha pin" />
                </div>
                <div>
                <label>所在城市：</label><input type="text" id="City" class="txt grid-4 alpha pin" />
                </div>
            </div>
            <div class="modal-footer">
            <input type="hidden" id="editUserId" value="" />
            <button class="btn" data-dismiss="modal" aria-hidden="true">关闭</button>
            <button class="btn btn-primary" onclick="MPUserInfoSave();">确定</button>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function ShowSend(id) {
            $('#SendMsgId').val(id);
            $('#SendMsgText').val('');
            $('#sendMsg').modal();
            setTimeout(function () { $('#SendMsgText').focus(); }, 300);
        }
        function SendMsg() {
            $.getJSON("SendMsg.aspx", { "do": "SendMsg", "Id": $('#SendMsgId').val(), "text": $('#SendMsgText').val() }, function (json) {
                if (json.success) {
                    $('#sendMsg').modal('hide');
                    parent.showTips('Success，消息发送成功!', 4);
                } else {
                    parent.showTips(json.msg, 5);
                }
            });
        }
        function editMPUserInfo(id) {
            $('#editUserId').val('');
            $('#RemarknameTxt2').val('');
            $('#MobileNumber').val('');
            $('#Sex').val('');
            $('#Province').val('');
            $('#City').val('');
            $.getJSON("mpuserinfo.aspx", { "do": "getone", "id": id }, function (json) {
                try {
                    $('#editUserId').val(id);
                    $('#RemarknameTxt2').val(json.RemarkName);
                    $('#MobileNumber').val(json.MobileNumber);
                    $('#Sex').val(json.Sex);
                    $('#Province').val(json.Province);
                    $('#City').val(json.City);
                    $('#editMPUserInfo').modal();
                } catch (e) { }
            });
        }
        function MPUserInfoSave() {
            $.getJSON("mpuserinfo.aspx", { "do": "mpuserinfosave", "Id": $('#editUserId').val()
            , "RemarkName": $('#RemarknameTxt2').val()
            , "MobileNumber": $('#MobileNumber').val()
            , "Sex": $('#Sex').val()
            , "Province": $('#Province').val()
            , "City": $('#City').val()
            }, function (json) {
                if (json.success) {
                    reLoadData();
                    $('#editMPUserInfo').modal('hide');
                    parent.showTips('Success，用户信息编辑成功!', 4);
                } else {
                    parent.showTips(json.msg, 5);
                }
            });
        }
        function editNickname(id, name) {
            $('#RemarknameId').val(id);
            if (name != undefined) {
                $('#RemarknameTxt').val(name);
            }
            $('#RemarknameModal').modal();
        }
        function RemarknameSave() {
            $.getJSON("mpuserinfo.aspx", { "do": "remarkname", "Id": $('#RemarknameId').val(), "Remarkname": $('#RemarknameTxt').val() }, function (json) {
                if (json.success) {
                    reLoadData();
                    $('#RemarknameModal').modal('hide');
                    parent.showTips('Success，用户备注编辑成功!', 4);
                } else {
                    parent.showTips(json.msg, 5);
                }
            });
        }
        wln.onNickName = function (e) {
            str = e.NickName;
            if (e.NickName) {
                if (e.RemarkName) {
                    str = '<a class="aedit" href="javascript:editNickname(\'' + e.Id + '\',\'' + e.RemarkName + '\');"><b>' + e.NickName + '</b>(' + e.RemarkName + ')</a>';
                } else {
                    str = '<a class="aedit" href="javascript:editNickname(\'' + e.Id + '\',\'\');"><b>' + e.NickName + '</b>(立即备注)</a>';
                }
            } else if (e.RemarkName) {
                str = '<a class="aedit" href="javascript:editNickname(\'' + e.Id + '\',\'' + e.RemarkName + '\');">' + e.RemarkName + '</a>';
            } else {
                str = '<a class="aedit" href="javascript:editNickname(\'' + e.Id + '\',\'\');">暂无</a>';
            }
            return str;
        }
        wln.onSex = function (e) {
            if (e.Sex == 1) {
                return '<font color="green">男</font>'
            } else if (e.Sex == 2) {
                return '<font color="green">女</font>'
            } else {
                return '<font color="gray">未知</font>'
            }
        }
        wln.onSubscribeTime = function (e) {
            if (e.SubscribeTime > 0) {
                return '<font color="green">是</font>'
            } else {
                return '<font color="gray">否</font>'
            }
        }
        wln.onAction = function (e) {
            str = '';
            <%if(MPType==2){ %>
            str += '<a href="javascript:ShowSend(\'' + e.Id + '\');">发消息</a>&nbsp;';
            <%} %>
            str += '<a href="javascript:editMPUserInfo(\'' + e.Id + '\');">编辑</a>&nbsp;<a href="javascript:void(\'0\')" onclick="del(\'' + e.Id + '\');">删除</a>';
            return str;
        }
        function reLoadData() {
            wln.loadTable('.wlntable', 'mpuserinfo.aspx', { 'do': 'getlist', 'page': wln.pageIndex, 'rows': 10, random: Math.random() });
        }
        function del(id) {
            if (confirm('注意：\n用户信息删除后无法恢复，需谨慎操作；\n您确定要删除帐号吗？')) {
                $.getJSON("mpuserinfo.aspx", { "do": "del", "id": id }, function (json) {
                    if (json.success) {
                        reLoadData();
                        parent.showTips('Success，用户信息删除成功!', 4);
                    } else {
                        parent.showTips(json.msg, 5);
                    }
                });
            }
        }
        reLoadData();
    </script>
</body>
</html>
