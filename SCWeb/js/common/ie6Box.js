if (typeof (window.loadjs_obj) == "undefined") {
    window.loadjs_obj = {};
}
window.loadjs_obj["/ie6box.js"] = true;
if (window.top.document.getElementById("ie_box") == null) {
    function xsl_cs() { document.getElementById("ie_box").style.display = "none", __oran_kf_setCookie("isIe6", "true") } var str = ""; str += '<div class="ie_box" id="ie_box" >', str += '<div class="ie_content">', str += '<div class="ie_ts">', str += "由于你正在使用IE6浏览器，帐号正在遭遇严重安全风险，建议你立即升级到以下最新浏览器保护帐号安全，以获得更好的浏览体验。导读：", str += '<a href="http://www.nsw88.com/" target="_blank" title="对浏览器升级">对IE6说再见</a></div>', str += '<div class="ie_down">', str += '<a class="up1" href="http://www.iefans.net/ie8-zhongwenban-guanfang-xiazai/" target="_blank" title="IE浏览器升级官网下载">Internet Explorer</a>', str += '<a class="up2" href="http://firefox.com.cn/download/" target="_blank" title="火狐浏览器官网下载">Firefox</a>', str += '<a class="up3" href="http://www.google.cn/intl/zh-CN/chrome/" target="_blank" title="Google浏览器官方下载">Google</a>', str += '<a class="up4" href="http://se.360.cn/" target="_blank" title="360安全浏览器官网下载">360安全浏览器最新版</a>', str += '<a class="up5" href="http://ie.sogou.com/" target="_blank" title="搜狗高速浏览器官方下载">搜狗高速浏览器</a>', str += '<a class="up6" href="http://liulanqi.baidu.com/" target="_blank" title="百度浏览器官方下载">百度浏览器</a>', str += '<div class="clear"></div></div>', str += '<div class="ie_close"><a href="javascript:void(0)" onclick="xsl_cs()" title="关闭">不再提示</a></div>', str += "</div></div>", str += "", str += "", document.write(str), $(function () { var c, a = !!window.ActiveXObject, b = a && !window.XMLHttpRequest; b && (c = __oran_kf_getCookie("isIe6"), ("" == c || null == c) && (document.getElementById("ie_box").style.display = "block")) });
    xsl_cs();
    top.$("body").html("").append(
    [
'<style>',
'.ie_box{ height:83px; background: url("ie_update.png") repeat-x left top; overflow:hidden; font-size:12px; color:#917b50; z-index: 999;position:absolute;top: 0px;left: 0; width:100%; display:none;}',
'.ie_content{ width:910px;  margin:auto; line-height:36px; padding:10px 0px; background:url("ie_update.png") no-repeat left -90px; padding-left:70px; position:relative;  }',
'.ie_down a{  float:left; overflow:hidden; text-decoration:none; padding-left:20px; padding-right:16px; line-height:16px; margin:0px; color:#917b50;}',
'.ie_down a:hover{text-decoration:underline;}',
'.ie_down .up1{ background:url("ie_update.png") no-repeat 0px -170px;}',
'.ie_down .up2{ background:url("ie_update.png") no-repeat 0px -196px;}',
'.ie_down .up3{ background:url("ie_update.png") no-repeat 0px -218px;}',
'.ie_down .up4{ background:url("ie_update.png") no-repeat 0px -240px;}',
'.ie_down .up5{ background:url("ie_update.png") no-repeat 0px -262px;}',
'.ie_down .up6{ background:url("ie_update.png") no-repeat 0px -284px;}',
'.ie_close{ position:absolute; top:10px; right:20px;}',
'.ie_close a{ color:#917b50;}',
'.ie_close a:hover{ color:#f7b79b;}',
'.clear{ clear:both;}',
'</style>'
    ].join('')
    )
} else {
    document.getElementById("ie_box").style.display = "block";
}