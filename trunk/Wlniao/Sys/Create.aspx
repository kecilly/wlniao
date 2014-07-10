﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Wlniao.Sys.Create" %>
<!DOCTYPE html>
<html lang="zh">
<head>
    <title>创建用户</title>
    <style type="text/css">
        @charset"utf-8";body,h1,h2,h3,h4,h5,h6,blockquote,p,pre,dl,dd,ol,ul,caption,th,td,form,fieldset,legend,input,button,textarea,address{margin:0;padding:0;}

        a,a:hover{text-decoration:none;}a,label,:focus{outline:0 none;}
        body{font:12px Tahoma,arial,sans-serif;color:#000;}
        input:-moz-placeholder{color:#ccc;}
        ::-webkit-input-placeholder{color:#ccc;}
        .clearfix:after{display:block;content:"\20";height:0;clear:both;overflow:hidden;visibility:hidden;}
        .clearfix{*zoom:1;}html,body{width:100%;height:100%;}
        body{background:#f2f2f2;}
        .container{margin-right: auto;margin-left: auto;width: 940px;}
        .suc_content{padding-top:60px;height:630px;width:903px;margin:0 auto;}
        .suc_kuang{background:#fdfdfd url(../static/img/p/create_f.gif) left bottom repeat-x;}
        .suc_botm{background:url(../static/img/p/create_b.gif) left bottom no-repeat;height:62px;}
        .sub_bg,.input_kuang,.input_b,.sub_login,.mt_login{-moz-border-radius:3px;-webkit-border-radius:3px;border-radius:3px;}
        .sub_bg,.mt_login,.input_b{-moz-box-shadow:0 1px 1px #f0f0f0;-webkit-box-shadow:0 1px 1px #f0f0f0;box-shadow:0 1px 1px #f0f0f0;}
        .sub_bg{height:36px;display:inline-block;*display:inline;zoom:1;border:1px solid #dadada;font-size:14px;width:132px;cursor:pointer;}
        .input_kuang{display:inline-block;height:20px;padding:8px;font-size:14px;border:1px solid #e3e3e3;border-top:1px solid #ccc;width:252px;background:#fff url(../../img/passport/s_bgs.png) 0 -118px repeat-x;box-shadow:0 1px 1px #fff;}
        .error_put{border:2px solid #f18447;padding:7px;}.sub_login{margin:0 auto;width:130px;border:1px solid #FF7B00;height:40px;background:#FF7B00;cursor:pointer;}
        .no_bg{border:0 none;padding:0;background-color:transparent;cursor:pointer;display:block;}
        .sub_bg input{width:132px;height:36px;}
        .sub_login input{width:130px;height:40px;color:#fff;font:700 16px/40px \5FAE\8F6F\96C5\9ED1,\9ED1\4F53,\6587\6CC9\9A7F\9ED1\4F53;}
        .sub_login .sub_login_a{display:inline-block;_display:inline;*zoom:1;width:130px;height:40px;text-align:center;color:#fff;font:700 16px/40px \5FAE\8F6F\96C5\9ED1,\9ED1\4F53,\6587\6CC9\9A7F\9ED1\4F53;}.mt_login{display:inline-block;*display:inline;zoom:1;height:24px;border:1px solid #dadada;font-size:14px;cursor:pointer;padding-top:14px;width:273px;text-align:center;color:#FF7B00;font-weight:bold;}.pointer{cursor:pointer;}.color_33{color:#333;}.marl_40{margin-left:40px;}.mart_60{margin-top:60px;}.mart_30{margin-top:30px;}.flt_l{float:left;}.flt_ln{float:left;}.long_width{width:326px;}p.intrd{color:#b0b0b0;margin-top:10px;}p.lht{line-height:1.5;}.val_m{vertical-align:middle;}i.val_m{font-style:normal;font-size:14px;}.val_mT{vertical-align:middle;_vertical-align:baseline;}.teln_m{text-align:center;}.por_r{position:relative;}.check_zi{margin:10px 0 0 10px;float:left;height:20px;line-height:20px;}.back_link{margin-top:95px;font-size:14px;color:#FF7B00;display:block;text-decoration:underline;}.register p{font-size:14px;}.change{margin-top:26px;margin-left:140px;}.h4_suc{margin:0 0 10px 30px;padding-top:20px;font-size:22px;font-weight:bold;color:#FF7B00;}.suc_p{padding:0 0 20px 30px;border-bottom:1px dashed #e3e3e3;color:#999;}.p_cor_hui{color:#999;margin:4px 0 0 260px;}.hei_513{height:auto;border:1px solid #dadada;border-bottom:none;}.register{margin:0 auto;width:650px;}.title{padding-top:140px;color:#FF7B00;font-size:30px;}.register p{line-height:22px;}.point_email{border-bottom:1px dashed #C9C9C9;padding:30px 0;}.point_phone{margin:45px 0 0;font-size:14px;}.point_time{padding:25px;color:#999;font-size:14px;}.other{padding:30px 0 0 83px;text-align:left;color:#999;}.action{color:#FF7B00;text-decoration:underline;}.back{display:inline-block;margin-top:55px;color:#FF7B00;text-decoration:underline;}.retrieve_pwd{border-bottom:1px dashed #E3E3E3;padding:30px;height:24px;line-height:24px;color:#FF7B00;text-align:left;font-size:24px;font-weight:400;}.tips{margin-top:98px;font-size:14px;}.txt_input{text-align:center;margin:30px 0 0 0;}.txt_input .mt_login{color:#000;font-weight:normal;margin-left:4px;padding-top:12px;width:110px;}.txt_input .goback{margin-top:30px;}.next_btn{margin-top:60px;width:130px;height:35px;line-height:35px;}.email{margin:30px 0 0 415px;text-align:left;font-size:14px;}.phone{margin:20px 0 0 415px;text-align:left;font-size:14px;}.sure_btn{margin-top:50px;width:130px;height:35px;line-height:35px;}.email_tips{margin-top:142px;font-size:14px;}.orange{text-decoration:underline;color:#FF7B00;font-size:14px;}.new_pwd{margin:60px auto 0;width:510px;}.new_pwd table td{height:38px;line-height:38px;padding-bottom:12px;}.reset_pwd{margin-bottom:20px;}.p_tips{text-align:center;margin-top:110px;font-size:14px;padding-top:10px;}.new_tips{margin:0 0 0 80px;padding:0;text-align:left;height:32px;line-height:16px;}.resetpassword .new_pwd{margin:60px 0 0 100px;}.resetpassword .reset_pwd{width:800px;}.resetpassword .new_tips{margin:0 0 0 150px;}.resetPass_content{margin-top:60px;}.msgTips{width:100%;height:100%;position:absolute;top:0;left:0;}.hide{display:none;}.td1{font-weight:bold;font-size:14px;line-height:1.5;width:80px;}.changePwd .td1{width:120px;text-align:right;}.font_14{font-size:14px;}.new_width{width:290px;}.changeP{margin:0 0 0 120px;}.pwd_p{margin:0 0 20px 84px;font-size:14px;line-height:1.1;}.big_name{font-size:24px;color:#FF7B00;font-weight:normal;line-height:1.1;}.only_tips{margin:10px 10px 0 180px;font-size:12px;color:#999;}.detail{margin:50px 0 0 200px;}.user_info{margin-bottom:40px;}.info{height:25px;line-height:25px;border-bottom:1px dashed #E3E3E3;padding:30px;}.info .left{float:left;display:inline-block;color:#FF7B00;font-size:24px;font-weight:400;}.info .right{float:right;display:inline-block;font-size:12px;}.info .right a{color:#000;}.photo{width:120px;float:left;border:1px solid #dcdcdc;-moz-box-shadow:0 1px 2px #CDCDCD;-webkit-box-shadow:0 1px 2px #CDCDCD;box-shadow:0 1px 2px #CDCDCD;}.photo img{display:block;width:120px;height:120px;}.photo a{color:#FF7B00;font-size:12px;text-decoration:underline;display:block;text-align:center;margin-top:15px;}.mess{float:left;}.mess td{height:14px;padding-bottom:22px;font-size:14px;font-weight:bold;}.mess td.td_l{text-align:right;width:80px;color:#999;font-size:14px;font-weight:normal;}.p_pop{position:relative;display:inline-block;*display:inline;zoom:1;}.pop{position:absolute;left:16px;top:-24px;z-index:10;margin-left:10px;display:none;padding-left:10px;width:400px;}.hover .pop{display:inline-block;*display:inline;zoom:1;}.doubt{width:16px;height:16px;display:inline-block;*display:inline;zoom:1;overflow:hidden;background:url(../../img/passport/icos.png) 0 0 no-repeat;vertical-align:middle;margin-left:10px;cursor:pointer;}.td_lht{line-height:1.5;font-size:12px;font-weight:normal;color:#999;padding:6px 10px;border:1px solid #d8d8d8;display:inline-block;*display:inline;zoom:1;border-radius:5px;background:url(../../img/passport/s_bgs.png) 0 -204px repeat-x;-moz-box-shadow:0 3px 3px #f0f0f0;-webkit-box-shadow:0 3px 3px #f0f0f0;box-shadow:0 3px 3px #f0f0f0;}.lit_tip{display:inline-block;*display:inline;zoom:1;width:12px;height:11px;background:url(../../img/passport/icos.png) 0 -63px no-repeat;overflow:hidden;float:left;top:16px;left:-1px;position:absolute;z-index:11;}.servie{width:564px;}.serve_title{clear:both;margin-bottom:11px;border-bottom:1px solid #FF7B00;padding-bottom:11px;color:#FF7B00;font-weight:bold;}.mess td a{color:#FF7B00;font-size:12px;font-weight:normal;text-decoration:underline;margin-left:20px;}.servie a.mt_login{width:130px;color:#000;font-weight:normal;line-height:36px;padding-top:0;height:36px;}.servie a.mt_login:hover{background:url(../../img/passport/sub_h.png) 0 0 repeat-x;border:1px solid #FF7B00;color:#fff;}.mess td a.mar_no{margin-left:0;}.sub_links{padding-top:10px;}.sub_links li{float:left;height:50px;width:144px;}.sub_links li.the_one{width:132px;}.check_tips{display:none;margin-left:10px;padding-left:20px;vertical-align:middle;height:16px;line-height:16px;background:url(../../img/passport/icosfories.png) 0 0 no-repeat;}.succ_tips{display:none;background:url(../../img/passport/icos.png) 0 -16px no-repeat;margin-left:10px;vertical-align:middle;width:16px;height:16px;line-height:16px;}.imgerror .errortip,.nosame .errortip,.error .errortip,.empty .errortip,.repeat .errortip{border:2px solid #f18447;padding:7px;}.msgTips{width:100%;height:100%;background:#000;opacity:.6;filter:alpha(opacity=60);z-index:5;position:absolute;top:0;left:0;}.msgInfo{padding:30px;margin:0 auto;width:200px;height:30px;color:#000;text-align:center;color:#000;font-size:14px;font-weight:600;background:#fcfcfc;position:absolute;z-index:10;top:50%;left:50%;margin-left:-130px;margin-top:-30px;border:1px solid #9f9f9f;box-shadow:0 0 10px rgba(0,0,0,0.4);}.hide{display:none;}.suc_limit{height:586px;}.hei_444{height:444px;border:1px solid #dadada;border-bottom:0 none;}.left_name{font-size:16px;font-weight:bold;float:left;}.m_name{color:#333;}.m_func{color:#FF7B00;margin-left:10px;}.guide{margin:26px;}.guide ul{width:100%;background:#ebebeb;}.guide li{float:left;color:#666;background:#ebebeb;position:relative;text-align:center;height:30px;line-height:30px;}.li1{width:212px;}.li2,.li3{width:216px;}.li4{width:205px;}.li5{width:430px;}.li6{width:419px;}.guide li span{margin-right:10px;font-weight:bold;font-size:14px;}.guide li em{display:block;position:absolute;top:0;right:0;width:13px;height:30px;}.guide li .em1{background:url(../../img/passport/huijiantou.png) no-repeat;}.guide li.current{background:#fd843f;color:#fff;}.guide li.current span{color:#fff;}.guide li.current .em1{background:url(../../img/passport/jiantou.png) no-repeat;}.guide li.current .em2{background:url(../../img/passport/caijiantou.png) no-repeat;left:-13px;top:0;}a.resent{color:#FF7800;margin-left:20px;text-decoration:underline;}p.sucss{font-size:14px;font-weight:bold;text-align:center;margin:62px 0 52px 0;line-height:16px;}.sucs_div{text-align:center;}.sucs_div .mt_login{color:#000;height:26px;padding-top:10px;font-weight:normal;width:118px;}p.sent_p{font-size:14px;margin:40px 0;text-align:center;line-height:24px;}p.sent_p b{margin-left:10px;}.a_cancel{text-align:center;margin-top:26px;}.a_cancel a{color:#FF7B00;text-decoration:underline;}.mar_k label{display:block;margin-bottom:14px;vertical-align:middle;}.mar_k{margin-top:100px;font-size:14px;}.mar_k2{margin-top:60px;font-size:14px;}.some_tips{text-align:center;margin-bottom:28px;line-height:1.1;}.mar_k label{margin-left:386px;}.mar_k2 label{display:block;margin-left:290px;vertical-align:middle;margin-bottom:6px;}.mar_k .sub_bg{margin:0 auto;display:block;margin-top:50px;}.em_check{text-align:center;margin:-12px 0 26px 0;}.em_check .input_kuang{width:80px;}.em_sub{text-align:center;}.em_sub .sub_login{float:none;margin:0 auto;}.pos_new{padding:30px 0 0 290px;}#vKey,#vKey_text{width:86px;}.goback{margin-top:60px;text-align:center;}.goback a{text-decoration:underline;color:#FF7B00;}.ph_tips{margin-top:70px;text-align:center;font-size:14px;}.email_tip{margin:140px 0 0 0;padding-bottom:40px;text-align:center;font-size:14px;}.checkcode{width:100px;}.checkcode_span{display:inline-block;*display:inline;zoom:1;width:200px;overflow:hidden;vertical-align:middle;}.checkcode_span i{font-style:normal;}.code_error{color:#FF7B00745;}.mes_p{line-height:1.5;color:#333;font-size:14px;font-weight:bold;margin:0 0 30px 40px;}.mes_p a{color:#FF7B00;text-decoration:underline;margin-left:10px;}.mes_p span{margin-right:10px;}.padt_20{padding-top:20px;}.content_p_center{padding-left:66px;line-height:1.5;color:#333;font-size:14px;font-weight:bold;}.content_p{line-height:1.5;color:#333;font-size:14px;font-weight:bold;margin:20px 0 0 180px;}.new_mar{margin:10px 0 20px 180px;}.mar_tp{margin:100px 0 60px 0;font-size:18px;font-weight:bold;color:red;text-align:center;}.new_con{width:320px;margin:0 auto;}.new_lg{margin-left:40px;height:30px;padding-top:10px;font-size:16px;width:130px;}.new_sg{float:left;width:130px;}.appeal-process{padding:40px;}.appeal-process h4{font-size:16px;color:#333;}.appeal-process p{color:#2c2c2c;font-size:14px;line-height:1.5;margin:10px 0;}.appeal-process span{color:#FF7B00;}.appeal-process p.text-indent{text-indent:28px;}.appeal-process-con{padding-left:28px;}.apc-left,.apc-right{float:left;}.apc-right{width:680px;}.cannot-used{margin-top:20px;font-size:14px;}.cannot-used a{color:#FF7B00;text-decoration:underline;}.inputvkey{width:86px;}.usi .mess{margin-left:10px;}.bif .sucs_div .mt_login{width:200px;}.sakc .sucs_div .mt_login{width:200px;}.bia .ph_mar span{width:260px;}.bia .marl_1{padding-left:275px;}.snsu .mess{margin-left:10px;width:430px;}.enchpen .prompt_info{padding-top:0;}.sra .sub_bg{width:180px;}.sra .sub_bg input{width:180px;}.nouse .suc_kuang{background:#fdfdfd url(../../img/passport/con_bg.png) left bottom repeat-x;}.nouse{height:1060px;}.nouse .hei_513{height:974px;}.nouse .apc-right{width:700px;}.nousedever{color:#FF7B00;text-decoration:underline;margin-left:10px;}.pointer{cursor:pointer;}.tooltips{position:relative;vertical-align:2px;display:inline-block;*display:inline;zoom:1;}.tooltips .bubble{position:absolute;left:72px;top:-24px;z-index:10;margin-left:10px;display:none;padding-left:10px;}.hover .bubble{display:inline-block;*display:inline;zoom:1;}.tooltips .doubt{width:70px;height:16px;display:inline-block;background:none;vertical-align:middle;margin-left:10px;cursor:pointer;}.tooltips .bubble-content{line-height:1.5;font-size:12px;font-weight:normal;width:220px;color:#999;padding:6px 10px;border:1px solid #d8d8d8;display:inline-block;border-radius:5px;background:#f4f4f4;-moz-box-shadow:0 3px 3px #f0f0f0,0 -1px 0 #fff inset;-webkit-box-shadow:0 3px 3px #f0f0f0,0 -1px 0 #fff inset;box-shadow:0 3px 3px #f0f0f0,0 -1px 0 #fff inset;}.tooltips .bubble-corner{display:inline-block;width:12px;height:11px;background:url(../../img/passport/icos-new.png) 0 0 no-repeat;overflow:hidden;top:16px;left:-1px;position:absolute;z-index:11;}.suc_content{height:auto;}.account_tips{text-align:left;height:32px;line-height:16px;font-size:14px;margin-left:180px;}dl{margin-bottom:20px;}.dt_l,.dd_r{float:left;height:38px;margin-bottom:12px;vertical-align:middle;}.dt_l{width:170px;padding-right:10px;font-size:14px;font-weight:bold;line-height:38px;text-align:right;}.dd_r{width:580px;}.dd_r_pos{position:relative;}.dd_r input{display:block;float:left;}.dd_r span{float:left;}.dd_r p{font-size:14px;line-height:38px;}.check_tips{margin-top:11px;}.newerror .error_tip,.nosame .no_tips,.error .error_tip,.empty .empty_tip,.repeat .repeat_tip,.succ .succ_tips{display:block;}.error .for_error_tip,.empty .for_empty_tip,.error .littlepop,.empty .littlepop{display:inline-block;*display:inline;zoom:1;}.prompt_info,.validate_info{padding-top:10px;margin-left:10px;}.prompt_info{position:static;}.changP .dt_l{width:210px;}.changP_container{margin:60px auto 0 auto;}.changP_container .dd_r{width:620px;}.changP .account_tips{margin-left:220px;}.resetP .dd_r{width:680px;}.resetp .prompt_info{padding-top:0;}.bindA dl{margin:60px 0 30px 0;}.bindA .dt_l{width:270px;}.bindA .dd_r{width:620px;}.bindA .input_kuang{width:232px;}.bindA .sub_login{margin:0 0 0 280px;display:inline-block;*display:inline;zoom:1;}.reg-account .getCode,.upgrade .getCode{float:left;padding-top:0;height:38px;width:90px;color:#000;font-weight:normal;margin-left:4px;}.reg-account .code-area{width:106px;}.reg-account b,.upgrade b{font-weight:normal;}.regs b,.upus b{font-weight:normal;margin-top:-8px;position:relative;display:block;}.reg-account .dd_r{width:680px;}.reg-account img{cursor:pointer;vertical-align:middle;margin:-10px 10px 0 5px;float:left;}.upgrade .checkcode,.reg-account .checkcode{width:100px;}.reg-account .checkcode_span{padding-top:8px;}.reg-account .radio_quyu{margin:50px 0 20px 180px;}.reg-account .p_cor_hui{margin:4px 0 0 180px;}.reg-account .p_cor_hui a{color:#FF7B00;text-decoration:underline;}.cor_yellow{color:#FF7B00;text-decoration:underline;}.upgrade .noSend,.reg-account .noSend{color:#999;cursor:default;}.forgetpassword .sub_bg{display:block;margin:60px auto 0 auto;}.forget_area{position:relative;width:350px;margin:30px auto 14px auto;}.forget_area .littlepop{right:-12px;}.upgrade .code-area{width:106px;}.upgrade .sub_login{margin:10px 0 0 0;}.login-intro{margin-left:180px;}.vfphone dl{margin-top:70px;}.vfphone .dt_l{width:224px;}.vfphone .dd_r{width:660px;}.vfphone .input_kuang{width:90px;}.vfphone .dd_r_span{line-height:1.5;margin-left:10px;}.vfphone .dd_r_span a{color:#FF7B00;text-decoration:underline;}.vfphone .sub_login{margin-left:234px;display:inline-block;*display:inline;zoom:1;}.vfphone .cannot-used{margin-left:234px;}.em_errorp{clear:both;color:red;font-size:12px;text-align:center;margin-top:10px;}.suc_limit p.sent_p{margin:60px 0 20px 0;}.cps .title{padding-top:100px;font-size:16px;}.upgrade .dd_r,.regs .dd_r{width:720px;}.upus .prompt_info,.regs .prompt_info{padding-top:0;width:260px;}.upus .getCode,.regs .getCode{width:180px;}
    </style>
</head>
<body>
 <div class="container">  
    <div class="suc_content reg-account nojsp">
      <div class="suc_kuang">
        <div class="hei_513">
          <h4 class="h4_suc">注册帐户</h4>
          <p class="suc_p">要使用我们为您提供的服务需要先注册帐户。如果您已拥有帐户，则可<a href="/login.aspx" class="cor_yellow">在此登录</a></p>
          <br>
          <form method="post" action="create.aspx">
	        <dl class="login-dl clearfix">
	              <dt class="dt_l">用户名：</dt>
	              <dd class="dd_r clearfix" id="phoneInner">
                        <input type="text" class="input_kuang short item errortip address" name="username" value="<%=username %>">
                        <%--<input class="mt_login val_m getCode" type="button" value="获取验证码">--%>                
                      </dd>
                  <dt class="dt_l" style=" display:none;">验证码：</dt>
                  <dd class="dd_r clearfix" id="validate_td" style=" display:none;">
                    <input type="text" class="input_kuang code-area errortip item val_m" name="ticket">
                  </dd>
                  <dt class="dt_l">设置密码：</dt>
                  <dd class="dd_r clearfix dd_r_pos" id="pwdTd2">
                    <input type="password" class="input_kuang item val_m errortip password" name="password" value="<%=password %>">
    	          </dd>
                  <dt class="dt_l">确认密码：</dt>
	              <dd class="dd_r clearfix">
                    <input type="password" class="input_kuang val_m item errortip" name="repassword" value="<%=repassword %>">
	              </dd>
	              <dt class="dt_l">&nbsp;</dt>
	              <dd class="dd_r clearfix la_height">
                    <div style="color:red;font-size:14px; margin-bottom:5px;"><%=msg%></div>
                  </dd>
	              <dt class="dt_l">&nbsp;</dt>
	              <dd class="dd_r clearfix la_height">
                    <div class="sub_login flt_l"><input type="submit" class="no_bg" value="立即注册"></div>
                  </dd>
	        </dl>  
          </form>
          <p class="p_cor_hui">点击“立即注册”，即表示您同意并愿意遵守<a href="#" class="cor_yellow" target="_blank">用户协议</a>和<a href="#" class="cor_yellow" target="_blank">隐私政策</a></p>
        </div>
        <div class="suc_botm"></div><!--这部分是卷角效果-->
      </div>
    </div>
 </div>
</body>
</html>