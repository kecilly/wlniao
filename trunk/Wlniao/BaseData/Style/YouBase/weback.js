if (!Array.prototype.indexOf)
{
    Array.prototype.indexOf = function(elt /*, from*/)
    {
        var len = this.length;

        var from = Number(arguments[1]) || 0;
        from = (from < 0)
            ? Math.ceil(from)
            : Math.floor(from);
        if (from < 0)
            from += len;

        for (; from < len; from++)
        {
            if (from in this && this[from] === elt)
                return from;
        }
        return -1;
    };
}


(function(window,$){

    /**
     * 加载多个json文件
     * @param reqs 请求的多个文件数组格式类似：
     *          [
     *              {
     *                  url : '',
     *                  data : '',
     *                  key : ''
     *              }
     *          ]
     * @param success
     * @param fail
     */
    $.getMultiJSON = function(reqs,success,fail){
        var count = 0;
        var result = {};

        if($.isArray(reqs)){
            count = reqs.length;

            $.each(reqs,function(index,item){
                $.getJSON(item.url + '?&callback=?',item.data,function(r){
                    count--;
                    if(r['ret']===0){
                        result[item.key] = r['data'];
                        if(typeof item.success==='function'){
                            item.success.call(this,r['data']);
                        }
                    }
                    fn_ok();
                }).fail(fn_err);
            });
        }
        else{
            fn_err();
        }

        function fn_ok(e){
            if(count<=0){
                if(typeof success === 'function'){
                    success.call(this,result);
                }
            }
        }

        function fn_err(e){
            count--;
            if(typeof fail === 'function'){
                fail.call(this,e);
            }
        }
    };


})(window,jQuery);

(function () {
    var onBridgeReady = function () {

        $(document).trigger('bridgeready');

        var $body = $('body'), appId = '',
            title = $body.attr('weiba-title'),
            imgUrl = $body.attr('weiba-icon'),
            link = $body.attr('weiba-link'),
            desc = $body.attr('weiba-desc') || link;
        if (!setForward()) {
            $(document).bind('weibachanged', function () {
                setForward();
            });
        }
    };
    if (document.addEventListener) {
        document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
    } else if (document.attachEvent) {
        document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
        document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
    }

    function setForward() {
        var $body = $('body'), appId = '',
            title = $body.attr('weiba-title'),
            imgUrl = $body.attr('weiba-icon'),
            link = $body.attr('weiba-link'),
            desc = $body.attr('weiba-desc') || link;
        if (title && link) {
            WeixinJSBridge.on('menu:share:appmessage', function (argv) {
                WeixinJSBridge.invoke('sendAppMessage', {
                    //'appid': 'kczxs88',
                    'img_url': imgUrl?imgUrl:undefined,
                    'link': link,
                    'desc': desc?desc:undefined,
                    'title': title
                }, function (res) {
                    if (res && res['err_msg'] && res['err_msg'].indexOf('confirm') > -1) {
                        $(document).trigger('wx_sendmessage_confirm');
                    }
                });
            });
            WeixinJSBridge.on('menu:share:timeline', function (argv) {
                $(document).trigger('wx_timeline_before');

                WeixinJSBridge.invoke('shareTimeline', {
                    'img_url': imgUrl?imgUrl:undefined,
                    'link': link,
                    'desc': desc?desc:undefined,
                    'title': title
                }, function (res) {
                    //貌似目前没有简报
                });
            });
            /*
             WeixinJSBridge.on('menu:share:weibo', function (argv) {
             WeixinJSBridge.invoke('shareWeibo', {
             'content': title + desc,
             'url': link
             }, function (res) {

             });
             });
             */
            return true;
        }
        else {
            return false;
        }
    }

})();
