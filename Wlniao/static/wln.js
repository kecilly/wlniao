window.wln = window.wln || {};
var __path = getWlnPath();
var __ele;
window.wln = {
    anthor: "wlniao",
    init: "wln.js",
    ele: __ele,
    path: __path,
    info: function () {
        alert("init from " + this.init);
    },
    pageIndex: 0,
    loadTableId: '',
    loadTableUrl: '',
    loadTableParam: {},
    toPage: function (pageIndex) {
        wln.pageIndex = pageIndex;
        wln.reloadTable();
    },
    reloadTable: function () {
        try {
            wln.loadTableParam.page = wln.pageIndex;
        } catch (e) { }
        wln.doLoad(wln.loadTableId, wln.loadTableUrl, wln.loadTableParam);
    },
    loadTable: function (tid, url, data) {
        wln.loadTableId = tid;
        wln.loadTableUrl = url;
        wln.loadTableParam = data;
        wln.doLoad(wln.loadTableId, wln.loadTableUrl, wln.loadTableParam);
    },
    doLoad: function (tid, url, data) {
        $.getJSON(url, data, function (json) {
            $(tid + " tr:not(:first)").remove();
            if (json.RecordCount > 0) {
                var headrow = $(tid + " tr:first");
                var filedCount = 0;
                var rowsCount = 0;
                $.each(json.Results, function (i, item) {
                    var newrow = headrow.clone();
                    try {
                        $.each(newrow.children("td"), function () {
                            try {
                                var fnName = $(this).attr('function');
                                if (fnName) {
                                    $(this).addClass('cell').html(wln[fnName](item));
                                } else {
                                    $(this).addClass('cell').html(item[$(this).attr('filed')]);
                                }
                            } catch (e) { $(this).html(''); }
                            filedCount = filedCount + 1;
                        });
                    } catch (e) { }
                    rowsCount = rowsCount + 1;
                    newrow.insertAfter(headrow);

                    //for (var filed in item) { //获取全部字段并依次绑定
                    //newrow.children("[filed='" + filed + "']").html(item[filed]).attr('wlntablecell', 'filed');
                    //}

                });
                if (json.PageCount) {
                    var _PageBar = "";
                    if (json.Current == 1) {
                        _PageBar = _PageBar + "<span>首页</span>&nbsp;";
                        _PageBar = _PageBar + "<span>上一页</span>&nbsp;";
                    }
                    else {
                        _PageBar = _PageBar + "<a href=\"javascript:wln.toPage(1);\">首页</a>&nbsp;<a href=\"javascript:wln.toPage(" + (json.Current - 1) + ");\">上一页</a>&nbsp;";
                    }
                    var i = 1;
                    var min = 0;
                    var max = 10;
                    if (json.Current > 5) {
                        max = json.Current + 5;
                    }
                    if (max > json.PageCount) {
                        max = json.PageCount;
                    }
                    min = max - 11;
                    if (min < 0) {
                        min = 0;
                    }
                    for (; min < max; min++) {
                        if ((i + min) == json.Current) {
                            _PageBar = _PageBar + "<em>" + (i + min) + "</em>&nbsp;";
                        }
                        else {
                            _PageBar = _PageBar + "<a href=\"javascript:wln.toPage(" + (i + min) + ");\">" + (i + min) + "</a>&nbsp;";
                        }
                    }
                    if (json.Current == json.PageCount) {
                        _PageBar = _PageBar + "<span>下一页</span>&nbsp;<span>尾页</span>";
                    }
                    else {
                        _PageBar = _PageBar + "<a href=\"javascript:wln.toPage(" + (json.Current + 1) + ");\">下一页</a>&nbsp;<a href=\"javascript:wln.toPage(" + json.PageCount + ");\">尾页</a>";
                    }
                    var pagerBar = document.getElementById('wlnPagebar');
                    if (pagerBar) {
                        $(pagerBar).html(_PageBar);
                    } else {
                        $(tid + " tr:last").parent().append('<tr><td colspan="' + filedCount + '" align="right"><div id=\"wlnPagebar\">' + _PageBar + '</div></td></tr>');
                    }
                }
            }

        });
    }
}
function getWlnPath(js) {
    var scripts = document.getElementsByTagName("script");
    var path = "";
    for (var i = 0, l = scripts.length; i < l; i++) {
        var src = scripts[i].src;
        if (src.indexOf("wln.js") != -1) {
            __ele = scripts[i];
            var ss = src.split("wln.js"); path = ss[0]; break;
        }
    }
    var href = location.href;
    href = href.split("#")[0];
    href = href.split("?")[0];
    var ss = href.split("/");
    ss.length = ss.length - 1;
    href = ss.join("/");
    if (path.indexOf("https:") == -1 && path.indexOf("http:") == -1 && path.indexOf("file:") == -1 && path.indexOf("\/") != 0) {
        path = href + "/" + path;
    }
    return path;
}
function Goto(url) {
    window.location.href = url;
    return false;
}