/*
    by suolan

*/

//1.WeiBaUI
(function(window,$){
    window.WBPage = {};

    $(function(){
        /*
        //如果发现没有历史浏览记录，则把当前页面url作为历史记录保存，以便返回按钮一直有效
        alert(window.history.length);
        if(window.history.length<=1){
            var ls = window.localStorage;
            if(ls){
                ls.setItem('default_history',window.location.href);
            }
        }
        */
    });

    $.extend(window.WBPage,{
        /**
         * 返回
         */
        'goBack' : function(){
            if(window.history.length<=1){
                window.location.href = (window.WBPage.Info.home);
            }else{
                window.history.back();
            }
        },

        'getWBData' : function(name){
            return $('body').getWBData(name);
        },
        'show' : function(){
            $('.weiba-page').show();
        },
        'hide' : function(){
            $('.weiba-page').hide();
        },

        'info_init' : function(info){
            $.extend({
                home : '',name : '微信网站'
            },info);
            window.WBPage.Info = {
                'home' : info.url,
                'name' : info.company
            };
        },

        /**
         * 初始化插件
         * @param name 插件名称
         */
        'widget_init' : function(name){
            switch(name){
                case 'banner':
                    $('.weiba-banner').wb_ui_banner();
                    break;
                case 'navbar':
                    $('.weiba-navbar').wb_ui_navbar();
                    break;
                case 'quickpanel':
                    $('.weiba-quickpanel').wb_ui_quickpanel();
                    break;
                case 'easycall':
                    $('.weiba-easycall').wb_ui_easycall();
                    break;
            }
        },
        /**
         * 渲染模板数据
         */
        'tpl_render' : function(data,directive){
            $.each(directive,function(key,dir){
                $(key).render(data,dir);
            });
        }
    });

    //扩展$对象
    $.fn.getWBData = function(name){
        var dataname = 'weiba-' + name;
        return this.attr(dataname);
    };
})(window,jQuery);


//MaskLayer
(function(window,$){
    var $masklayer;
    window.WBPage.MaskLayer = {
        'show' : function(color){
            if(!$masklayer){
                $masklayer = $('<div class="weiba-masklayer"></div>').addClass(color?color:'');
            }
            return $masklayer.hide().appendTo('body').fadeIn();
        },
        'close' : function(){
            if($masklayer){
                $masklayer.fadeOut(function(){
                    $masklayer.off();
                    $masklayer.unbind();
                    $masklayer.remove();
                    $masklayer = null;
                });
            }
        },
        'getZIndex' : function(){
            if($masklayer){
                return $masklayer.css('z-index');
            }
            else{
                return 0;
            }
        }
    };


})(window,jQuery);


//2.navbar
(function(window,$){
    $.fn.wb_ui_navbar = function(){


        var $navBar = this.each(function(){
            $(this).on('tap','.weiba-navbar-item',function(e){
                if($(this).hasClass('quick')){
                    if(WBPage.QuickPanel){
                        if(!WBPage.QuickPanel.isOpened){
                            WBPage.QuickPanel.open();
                        }
                        else{
                            WBPage.QuickPanel.close();
                        }
                    }
                }
                else if($(this).hasClass('easycall')){
                    if($(this).hasClass('easycall-one')){//只有一个easycall按钮，则直接执行链接
                        return;
                    }else{
                        if(WBPage.EasyCall){
                            if(!WBPage.EasyCall.isOpened){
                                WBPage.EasyCall.open();
                            }
                            else{
                                WBPage.EasyCall.close();
                            }
                        }
                    }
                }
                else if($(this).hasClass('home')){
                    return;
                    //if(WBPage.Info){
                    //    window.location.href = WBPage.Info.home;
                    //}
                }
                else if($(this).hasClass('back')){
                    WBPage.goBack();
                    //window.history.back();
                }
                e.preventDefault();
                return false;
            });
        });

        window.WBPage.NavBar = {
            'Dom' : $navBar
        };

        return $navBar;
    };

    function quickpanelclose(e) {
        console.log('close');
        $('body').removeClass('weiba-quickpanel-animate-push');
        $('.weiba-quickpanel').hide();
        $('.weiba-page').unbind('tap.quickpanel',quickpanelclose);
        e.preventDefault();
        return false;
    }
})(window,jQuery);

//3.quickpanel
(function(window,$){
    $.fn.wb_ui_quickpanel = function(action){
        var _transitionEndEvents = 'webkitTransitionEnd oTransitionEnd otransitionend transitionend msTransitionEnd';
        var $quickpanel =  this;
        var $pannel_box;

        if($quickpanel.length>0){
            $pannel_box = $('<div class="weiba-quickpanel-box"><div class="weiba-quickpanel-toolbar"><div class="weiba-quickpanel-toolbar-title">快捷导航</div><div class="weiba-quickpanel-toolbar-close icon-delete"></div></div></div>')
                .append($quickpanel.show()).appendTo('body')
                .on('tap','.weiba-quickpanel-toolbar',function(){
                    window.WBPage.QuickPanel.close();
                });
        }

        window.WBPage.QuickPanel = {
            'isOpened': false,
            'open' : function(){
                window.WBPage.MaskLayer.show('black');
                $pannel_box.css({
                    'z-index' : window.WBPage.MaskLayer.getZIndex()+1,
                    'width': $quickpanel.width(),
                    'top' : 0,
                    'right' : -$quickpanel.width() + 'px'
                    //'height' : $(window).height()
                }).show().animate({
                    'right' : 0
                },function(){
                    window.WBPage.QuickPanel.isOpened = true;
                });
            },
            'close' : function(){
                $pannel_box.css({
                    'top' : 0,
                    'right' : 0
                }).show().animate({
                        'right' : -$quickpanel.width() + 'px'
                    },function(){
                        window.WBPage.QuickPanel.isOpened = false;
                        $pannel_box.hide();
                        window.WBPage.MaskLayer.close();
                    });
            }
        };
        return $quickpanel;
    };
})(window,jQuery);

//4.weiba-easycall
(function(window,$){
    $.fn.wb_ui_easycall = function(){
        this.addClass('child' + this.children('.weiba-easycall-item').each(function(index,item){
            $(item).addClass('no' + index);
        }).length);
        var $easycall =  this.on('tap','.weiba-easycall-item',function(e){
            e.stopPropagation();
            //return false;
        }).on('tap',function(){
            if(!$(this).hasClass('weiba-easycall-item')){
                window.WBPage.EasyCall.close();
            }
        });

        if(this.children('.weiba-easycall-item').length<=0){//隐藏navBar的图标
            $('.weiba-navbar').addClass('easycall-no');
        }
        else if(this.children('.weiba-easycall-item').length==1){//一个的时候navbar直接操作
            $('.weiba-navbar-item.easycall').addClass('easycall-one').attr('href',this.children('.weiba-easycall-item').attr('href'));
        }

        window.WBPage.EasyCall = {
            'isOpened': false,
            'open' : function(){
                window.WBPage.MaskLayer.show('black').on('tap',function(){
                    window.WBPage.EasyCall.close();
                });
                $easycall.css({
                    'z-index' : window.WBPage.MaskLayer.getZIndex()+1
                }).show(function(){
                    window.WBPage.EasyCall.isOpened = true;
                });
            },
            'close' : function(){
                $easycall.hide(function(){
                        window.WBPage.EasyCall.isOpened = false;
                        window.WBPage.MaskLayer.close();
                    });
            }
        };
        return $easycall;
    };
})(window,jQuery);

//4.banner
(function(window,$){
    $.fn.wb_ui_banner = function(){
        return this.each(function(){
            var $this = $(this),
                flganimate = false,
                count = $this.children('.weiba-banner-item').length;

            if (count>1) {
                //1.插入工具条
                var html = '<div class="weiba-banner-toolbar">';
                for (var i=0;i<count;i++) {
                    html+='<span class="weiba-banner-toolbar-item l'+ i +'"></span>';
                }
                html +='</div>';
                
                //2.绑定事件
                var $toolbar =  $(html).appendTo($this);
                $this.on({
                    'swipeleft': function (e) {
                        if (!flganimate && $this.data('__currIndex') < (count - 1)) {
                            e.preventDefault();
                            selectedIndex($this.data('__currIndex')+1);
                        }
                    },
                    'swiperight': function (e) {
                        if (!flganimate && $this.data('__currIndex') > 0) {
                            e.preventDefault();
                            selectedIndex($this.data('__currIndex') - 1);
                        }
                    }
                });
                
                //3.选中第一个
                selectedIndex(0);

                //4.自动播放
                autoPlay();
            }
            else if(count<=0){
                $this.remove();
            }
            
            function selectedIndex(index) {
        		$this.data('__currIndex', index);
        		changeMarginLeft(-$this.data('__currIndex') * 100 + '%');// $this.width());
                $($toolbar.children().removeClass('selected')[index]).addClass('selected');
            }   
            function changeMarginLeft(toLeft) {
                flganimate = true;
                $($this.children().get(0)).animate({
                    'margin-left' : toLeft
                }, 220, 'linear', function () {
                    flganimate = false;
                });
                        
            }
            function autoPlay(){
                var cindex = $this.data('__currIndex');
                cindex++;
                if(cindex>=count){
                    cindex = 0;
                }
                if(!flganimate){
                    selectedIndex(cindex);
                }
                window.setTimeout(autoPlay,3500);
            }
        });
    }; 
})(window,jQuery);


