if (typeof (window.loadjs_obj) == "undefined") {
    window.loadjs_obj = {};
}
window.loadjs_obj["/common.js"] = true;
var Version = "1.0";
window.loadjs_obj = {};
if (typeof (console) == "undefined" || window.top.debug) {
    var console = function () { };
    console.log = function (msg) {
        if (window.top.debug) {
            var log_ = window.top.document.getElementById("log_");
            if (!log_) {
                log_ = window.top.document.createElement("div");
                log_.setAttribute("id", "log_");
                window.top.document.body.appendChild(log_);

                var input = null;
                if (! -[1, ]) {
                    input = window.top.document.createElement("<input type='button' value='清除记录' />");
                    log_.appendChild(input);
                } else {
                    input = window.top.document.createElement("input");
                    log_.appendChild(input);
                    input.type = "button";
                    input.value = "清除记录";
                }
                if (input) {
                    input.onclick = function () {
                        var divs = $("#log_").find("div").remove();
                    }
                }
            }
            var div = window.top.document.createElement("div");
            div.innerHTML = window.location + "  --->>   log：" + msg + "<br />";
            log_.appendChild(div);
            return true;
        }
    }
}
window.log = function (msg) {
    console.log(msg);
}

var GetFrmEditor = function () {
    if (window.self == window.top) {
        return window;
    }
    return window.top.frames["frmEditor"] || window.top;
}

if (window.self == window.top && !window.top.SetHeight) {
    window.top.SetHeight = function () {}
}
function _extend(child, parent, proto) {
    if (!proto) {
        proto = parent;
        parent = null;
    }
    var childProto;
    if (parent) {
        var fn = function () { };
        fn.prototype = parent.prototype;
        childProto = new fn();
        _each(proto, function (key, val) {
            childProto[key] = val;
        });
    } else {
        childProto = proto;
    }
    childProto.constructor = child;
    child.prototype = childProto;
    child.parent = parent ? parent.prototype : null;
}

function getPageFilename(path) {
    path = path || window.location.pathname;
    var filename = path;
    var pos = -1;
    if ((pos = path.lastIndexOf('/') + 1) > 0) {
        filename = path.substring(pos, path.length);
    }
    return filename;
}
//获取文件实名，如page.aspx则为page,file.exe为file
function getFilenameClip(filename) {
    var pos = filename.lastIndexOf('.');
    var filenameClip = filename.substring(0, pos);
    return filenameClip;
}
function SaveURL(url) {
    var _href = url || (window.location + '');
    _href = _href.substring(_href.lastIndexOf('/') + 1, _href.length);
    _href = _href.replace(/#{2,}/, '');
    $.cookie("nswXpath", "{editmenu:'" + _href + "'}");
}
var Manager = window.location.pathname + ""
if (Manager.lastIndexOf('/') > 0) {
    Manager = Manager.substr(0, Manager.lastIndexOf('/') + 1);
}
function edit(oid,url) {
 
    if (oid!=null) {
        url += "?oid=" + oid;

    }
    window.location = url;
}
var PARENT = window.self.parent || window.top;
var TOP = window.top;
function GetRandomNum(Min, Max) {
    var Range = Max - Min;
    var Rand = Math.random();
    return (Min + Math.round(Rand * Range));
}
var chars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
function generateMixed(n) {
    var res = "";
    for (var i = 0; i < n; i++) {
        var id = Math.ceil(Math.random() * 35);
        res += chars[id];
    }
    return res;
}
//获取参数化
function parse_url(_url, key) {
    var parames = {};
    var arr = _url.split('?');
    if (arr.length > 1) {
        items = arr[1].split("&");
        var arr, name, value;
        for (var i = 0, l = items.length; i < l; i++) {
            arr = items[i].split("=");
            name = arr[0];
            value = arr[1] || '';
            if (name) {
                if (key && key == name) {
                    return decodeURIComponent(value);
                } else {
                    if (value) {
                        parames[name] = decodeURIComponent(value);
                    }
                } 
            }
        }
    }
    if (key) {
        return null;
    }
    return parames;
}
var _Json = function (src_data) {
    try {
        src_data = jQuery.parseJSON(src_data);
        if (src_data) {
            return src_data;
        }
    } catch (e) {
        try {
            src_data = eval("(" + src_data + ")");
        } catch (e) {
            jQuery.error("src-data设置错误，请联系程序立即处理！");
        }
    }
    return null;
}
//字符串去除头尾空格
String.prototype.trim = function (c) {
    if (c == null || c == "") {
        return this.replace(/(^\s*)|(\s*$)/g, "");
    } else {
        var rg = new RegExp("^" + c + "*");
        var str = this.replace(rg, '');
        rg = new RegExp(c);
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
}

String.prototype.ltrim = function () {
    return this.replace(/(^\s*)/g, "");
}

String.prototype.rtrim = function () {
    return this.replace(/(\s*$)/g, "");
}

//设置验证
var SetValidatebox = function (inputs) {
    loadCss("skins/easyui/easyui.css");
    loadJs("js/easyui/jquery.easyui.min.js", function () {
        var message = "";
        window.bodyWidth = $(document.body).width();
        var _w = (window.bodyWidth * 0.82 - 5) * 0.02;
        inputs = $(inputs);
        inputs.each(function () {
            var me = $(this);
            var tips = me.attr("tips") || '';
            var required = me.attr("_required") === "true";
            var tipPosition = me.attr("tipPosition") || "right";
            var validType = me.attr("validtype");
            var invalidMessage = me.attr("invalidMessage");

            var obj = {};
            if (required) {
                obj.required = true;
            }
            if (tips) {
                obj.missingMessage = "请输入" + tips;
            } else {
                var lab = me.parents("div.add_r").prev(".add_label");
                if (lab.length) {
                    tips = lab.text().replace('*', '');
                    obj.missingMessage = "请输入" + tips;
                } else {
                    obj.missingMessage = "该输入项为必输项";
                }
            }

            if (validType) {
                obj.validType = validType;
                if (invalidMessage) {
                    obj.invalidMessage = invalidMessage;
                }
                me.addClass("valid_type");
            }
            if (tipPosition) {
                obj.tipPosition = tipPosition;
            }
            if (tipPosition == "right") {
                var clear_bd = me.parent("span.clear_bd");
                if (clear_bd.length) {
                    var left = clear_bd.offset().left + clear_bd.outerWidth();
                    var mleft = me.offset().left + me.outerWidth();
                    if (left - mleft > 0) {
                        obj.deltaX = left - mleft;
                    } else {
                        obj.deltaX = _w;
                    }
                } else {
                    obj.deltaX = me.attr("deltaX") ? parseInt(me.attr("deltaX")) : 5;
                }
            }
            me.validatebox(obj);
        })
    })
}

var RequiredSelect = "input[_required],select[_required],input[validtype]";
$(function () {
    $("i.red_point").parent().next().find("input[name][type='text']:first").attr("_required", "true");
    var inputs = $(RequiredSelect);
    if (inputs.length) {
        Timeout(function () {
            SetValidatebox(inputs);
        });
    }
    //判断是不是列表页
    var div = $("div.nsw_tools_bar");
    if (div.length == 1) {
        $("#nsw_list_table").before(div.clone());
        $("div.nsw_tools_bar:eq(0)").addClass("top_nsw_tools_bar").css("background", "#fff").find(".e_btn1").hide();
    }
});
//表单提交前验证
//提交表单之前，SubmitFormBefore
var ValidateForm = function (_submit) {
    var _form = $(_submit).parents("form,.div_form");
    var inputs = _form.find(".validatebox-text,.valid_type");
    var SubmitFormBefore = window["SubmitFormBefore"];
    var isTrue = true;
    if (inputs.length) {
        isTrue = _form.form('validate');
        if (!isTrue) {
            var ul = $("ul.addtab_ul");
            //有切换的
            if (ul.length) {
                setTimeout(function () {
                    var div = inputs.filter(".tooltip-f").eq(0).parents("div.tab_cat_cont");
                    if (div.length) {
                        var index = parseInt(div.attr("tab_index"));
                        if (!isNaN(index)) {
                            ul.children().eq(index).trigger("click");
                        }
                    }
                }, 200);
            }
        }
    }
    if (isTrue && SubmitFormBefore && jQuery.isFunction(SubmitFormBefore)) {
        var value = SubmitFormBefore.apply(_submit, arguments);
        if (value === false) {
            isTrue = value;
        }
    }
    return isTrue;
}
var getData = function (data, key, def) {
    if (!data) {
        return def;
    }
    var value = data[key];
    return value ? value : def;
}
var _getJSONNewAjax = function (data) {
    var ajax = getData(data, "ajax", "getDetailed");
    var _data = getData(data, "data", {});
    var callback = getData(data, "callback", function (msg) {
        _alert(msg.Data);
    });
    $.ajax({
        url: "/Manager/Handler/newAjax.ashx?action=" + ajax + "&t" + Math.random(),
        data: _data,
        dataType: "json",
        type: "post",
        success: function (msg) {
            callback(msg);
        }
    })
}
var $j = function (id) {
    return $("#" + id);
}
function _unescape(val) {
    return val.replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&quot;/g, '"').replace(/&amp;/g, '&');
}

var topDoc = window.top.document;

//美化单选，多选，下拉
$(function () {
    //单选框
    var radio_temp = [
        '<span class="f_fl f_db e_radio radio radio_{index}" cur="z_select">',
            '<span class="dn"></span>',
        '</span>'
    ].join('');
    $("div.set_radios>span,td.attribute_radiobuttonlist>span").each(function () {
        var me = $(this);
        var parent = me.parent();
        var childrens = me.children("input");
        childrens.each(function (i) {
            parent.append(radio_temp.replace('{index}', i));
            var bx = parent.find("span.radio_" + i);
            bx.append($(this).next("label"));
            bx.children("span").append($(this));
        })
        me.remove();
    })

    //下拉框
    var select_temp = [
        '<span class="clear_bd s_selt f_fl pt43 f_mr15 s_selt_{index}">',
            '<i class="revise_sub"></i>',
        '</span>'
    ].join('');
    $("div.set_selects,td.attribute_dropdownlist").each(function () {
        var me = $(this);
        var childrens = me.children("select");
        childrens.each(function (i) {
            me.append(select_temp.replace('{index}', i));
            var bx = me.find("span.s_selt_" + i);
            bx.append($(this).addClass("pass_faq"));
        })
    })

    //设置多选
    var checkbox_temp = [
        '<div class="nsw_check_box w80 bx{index}">',
        '    <span class="ck_box mt5">',
        '    </span>',
        '    <label class="ck_text"></label>',
        '</div>'
    ].join('');

    $("div.set_cbs>span,td.attribute_checkboxlist>span").each(function () {
        var me = $(this);
        var parent = me.parent();
        var childrens = me.children("input");
        childrens.each(function (i) {
            parent.append(checkbox_temp.replace('{index}', i));
            var bx = parent.find("div.bx" + i);
            bx.children("label.ck_text").text($(this).next().text());
            bx.children("span.ck_box").append($(this).addClass("dn"));
        });
        me.remove();
    })

    //文本框
    var input_temp = [
        '<span class="clear_bd f_fl p_inp1">',
        '</span>'
    ].join('');
    $("div.set_input,td.attribute_textbox").each(function () {
        var me = $(this);
        var childrens = me.children("input");
        childrens.each(function (i) {
            me.append(input_temp);
            me.find("span.clear_bd").append($(this).addClass("com_input clear_word")).append('<i class="clear_x"></i>');
        });
    })
    //多行文本框
    var textarea_temp = [
        '<span class="clear_bd f_fl p_inp1">',
        '</span>'
    ].join('');

    $("div.set_textarea>span,td.attribute_textarea").each(function () {
        var me = $(this);
        var childrens = me.children("textarea");
        childrens.each(function (i) {
            me.append(textarea_temp);
            me.find("span.clear_bd").css("margin", "8px 0").append($(this).addClass("com_input clear_word").attr("style", "resize:none;height:100px;")).append('<i class="clear_x"></i>');
        });
    })
})

var SetClearWord = function (clear_words) {
    $(clear_words).bind({
        focus: function () {
            if (this.value == "") {
                $(this).css({ "color": "#4f5159", "border-color": "#aaa" });
                $(this).parent(".clear_bd,.search_so,.add_typeset").css("border-color", "#aaa");
            }
        },
        blur: function () {
            if (this.value == "") {
                $(this).css({ "color": "#dcdcdc", "border-color": "#dcdcdc" });
                $(this).parent(".clear_bd,.search_so,.add_typeset").css("border-color", "#dcdcdc");
            }
        }
    }).bind("hasValue", function () {
        if (this.value != "") {
            $(this).css({ "color": "#4f5159", "border-color": "#aaa" });
            $(this).parent(".clear_bd,.search_so,.add_typeset").css("border-color", "#aaa");
        } else {
            $(this).css("color", null).css("border-color", null).parent(".clear_bd,.search_so,.add_typeset").css("border-color", null);
        }
    }).each(function () {
        $(this).trigger("hasValue");
    }).next("i.clear_x").bind("click", function () {
        var me = $(this);
        me.prev("input,textarea").val("").blur();
    });
}

$(function () {
    SetClearWord("input.clear_word,textarea.clear_word,textarea.text_area");
});

/** 
* java String hashCode 的实现 
* @param strKey 
* @return intValue 
*/
function isNull(str) {
    return str == null || str.value == "";
}

function hashCode(strKey) {
    var hash = 0;
    if (!isNull(strKey)) {
        for (var i = 0; i < strKey.length; i++) {
            hash = hash * 31 + strKey.charCodeAt(i);
            hash = intValue(hash);
        }
    }
    return hash;
}

/** 
* 将js页面的number类型转换为java的int类型 
* @param num 
* @return intValue 
*/
function intValue(num) {
    var MAX_VALUE = 0x7fffffff;
    var MIN_VALUE = -0x80000000;
    if (num > MAX_VALUE || num < MIN_VALUE) {
        return num &= 0xFFFFFFFF;
    }
    return num;
}

/*加载js*/
function _isFunction(val) {
    if (!val) {
        return false;
    }
    return Object.prototype.toString.call(val) === '[object Function]';
}
var loadCss = function (src, doc) {
    if (!src) {
        return false;
    }
    doc = doc || document;
    var csss = $(doc).data("csss") || {};
    src = src.toLowerCase();
    if (csss[src]) {
        return true;
    }
    var head = doc.getElementsByTagName('head')[0];
    var file = src.lastIndexOf('/') > 0 ? src.substr(src.lastIndexOf('/')) : src;
    var script = $(head).find("link[href$='" + file + "']");

    if (script.length) {
        return true;
    }

    var fileref = doc.createElement('link');
    fileref.setAttribute("rel", "stylesheet");
    fileref.setAttribute("type", "text/css");
    fileref.setAttribute("href", src + "?v=" + Version);
    fileref.charset = 'utf-8';
    head.appendChild(fileref);

    csss[src] = true;
    $(doc).data("csss", csss);

    return true;
}
var _$ = function (ex, doc) {
    return window.top.$(ex, doc || document);
}
//2015-08-19
var loadJs = function (src, fn, doc) {
    if (!src) { return false; }
    doc = doc || document;
    src = src.toLowerCase();
    var file = src.lastIndexOf('/') > 0 ? src.substr(src.lastIndexOf('/')) : src;
    var win = doc === window.top.document ? window.top : window;
    var Executive = function (fn) {
        if (fn && _isFunction(fn)) {
            setTimeout(function () {
                fn();
            }, 100);
        }
    }
    var _setTimeout = function (file, fn, max) {
        max--;
        if (max < 1) {
            win.loadjs_obj[file] = true;
        }
        setTimeout(function () {
            if (!win.loadjs_obj[file]) {
                _setTimeout(file, fn, max);
            } else {
                Executive(fn);
            }
        }, 500);
    }
    if (win.loadjs_obj[file]) {
        Executive(fn);
        return;
    }
    var jss = $(doc).data("jss") || {};
    if (jss[src]) {
        if (fn && _isFunction(fn)) {
            if (jss[src] == "complete" || win.loadjs_obj[file]) {
                Executive(fn);
            } else {
                _setTimeout(file, fn, 5);
            }
        }
        return true;
    }
    var head = doc.getElementsByTagName('head')[0];
    var script = $(head).find("script[src*='" + file + "']");
    if (script.length) {
        if (fn && _isFunction(fn)) {
            if (script.attr("state") == "loading") {
                _setTimeout(file, fn, 5);
            } else {
                Executive(fn);
            }
        }
        return true;
    }

    var nodeScript = doc.createElement('script');
    nodeScript.type = "text/javascript";
    nodeScript.src = src + "?v=" + Version;
    nodeScript.charset = 'utf-8';

    head.appendChild(nodeScript);

    jss[src] = "loading";
    $(doc).data("jss", jss);

    $(nodeScript).attr("state", "loading");
    nodeScript.onload = nodeScript.onreadystatechange = function () {
        if (nodeScript.ready) {
            return false;
        }
        if (!nodeScript.readyState || nodeScript.readyState === 'loaded' || nodeScript.readyState === 'complete') {
            if (fn && _isFunction(fn)) {
                _setTimeout(file, fn, 2);
            }
            $(nodeScript).attr("state", "complete");
            jss[src] = "complete";
            $(doc).data("jss", jss);
            nodeScript.onload = nodeScript.onreadystatechange = null;
        }
    };
}
function Timeout(fn, tims) {
    tims = tims || 500;
    setTimeout(function () {
        if (fn && _isFunction(fn)) {
            fn();
        }
    }, tims);
}
function _escape(val) {
    return val.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
}

$(function () {
    //单选按钮
    $("span.radio").click(function () {
        var me = $(this);
        var name = $(this).attr("name");
        var cur = me.attr("cur");
        var radio = me.find("input").attr("checked", true);
        var siblings = me.siblings("span.radio" + (name ? "[name='" + name + "']" : ""));
        me.addClass(cur)
        siblings.each(function () {
            var _me = $(this);
            _me.removeClass(cur).attr("checked", false).find("input").attr("checked", false);
        })
        radio.trigger("ev_click");
        //window.onerror('radio.trigger("ev_click") end');
    }).filter(function () {
        var me = $(this);
        return me.hasClass(me.attr("cur"));
    }).click();

    //复选框
    $("span.ck_box").bind("click", function (e) {
        var me = $(this);
        var checkbox = me.find("input");
        var checked = checkbox.attr("checked");
        checkbox.attr("checked", !checked);
        if (!checked) {
            me.addClass("z_select");
        } else {
            me.removeClass("z_select");
        }
        checkbox.trigger("ev_click");
        return false;
    }).next("label.ck_text").bind("click", function () {
        $(this).prev("span.ck_box").trigger("click");
    });

    setCheckeds($("span.ck_box input,span.radio input"));
})

function setCheckeds(checkeds) {
    checkeds = $(checkeds);
    checkeds.filter("[onclick]").each(function () {
        var me = $(this);
        me.bind("ev_click", function () {
            this.onclick.call(this);
            //window.onerror('this.onclick.call(this)');
        });
    })
    var sels = checkeds.selector.split(',');
    for (var a in sels) {
        var v = sels[a];
        if (v) {
            $(v + ":checked").trigger("ev_click").parents("span.ck_box,span.radio").addClass("z_select");
        }
    }
}

//弹出框
$(function () {
    var ups = $(".popUp");
    if (ups.length) {
        Timeout(function () {
            loadJs("js/common/popup.js", function () {
                ups.click(function () {
                    var me = $(this);
                    var popUp = me.data("popup");
                    if (!popUp) {
                        var data = me.attr("data-src") ? _Json(me.attr("data-src")) : null;
                        if (!data) {
                            _alert("请设置data-src");
                        }
                        var popup = data.popup || "NewPopUp";
                        if (popup && (popup in window)) {
                            popUp = window[popup](data);
                            popUp._src = me;
                            me.data("popup", popUp);
                        }
                    } else {
                        popUp.options.show.call(popUp);
                    }
                })
            });
        });
    }

    var editor_warps = $(".editor_warp");
    if (editor_warps.length > 0) {
        Timeout(function () {
            loadJs("js/common/editor.js", function () {
                editor_warps.each(function () {
                    var me = $(this);
                    var data = me.attr("data-src") ? _Json(me.attr("data-src")) : null;
                    if (data && data.editor) {
                        newFunction(data.editor, me, data);
                    }
                });
            });
        });
    }

})

var fadeOut = function (el) {
    if (jQuery.browser.msie) {
        $(el).hide();
    } else {
        $(el).fadeOut();
    }
}

//
var _confirm = function (msg, fn, fn2) {
    var _$ = window.top.$;
    var div = _$(top.document.body).find("div.msg_confirm");
    if (!div.length) {
        var html = [
            '<div class="nsw_dilogbor msg_confirm" style="width:500px;position:fixed; left:50%;top:50%; margin-left:-250px;z-index:999991;">',
	        '    <div class="dialog-titlebar f_cb">',
		    '        <span class="dialog-title">网站操作信息提示</span>',
		    '        <button class="dialog-titlebar-close"></button>',
	        '    </div>',
	        '    <div class="dialog_content">',
		    '        <div class="f_cb nsw_con_padding">',
		    '	        <div class="tips_icon tips_recovery f_fl"></div>',
		    '	        <div class="tips_box f_fl">',
		    '		        <div class="war_text f_lht30 f_fw "></div>',
		    '		        <div class="btn_space f_pdb30 f_mt20 f_cb">',
		    '			        <span class="e_btn1 f_csp "><i class="sure_icon marg12_icon"></i>确 定</span>',
		    '			        <span class="e_btn2 f_ml35 f_csp "><i class="viewCancle_icon"></i>取 消</span>',
		    '		        </div>',
		    '	        </div>',
		    '        </div>',
	        '    </div>',
            '</div>'
        ].join('');
        _$(top.document.body).append(html);
        div = _$(top.document.body).find("div.msg_confirm").hide();
    }
    div.fadeIn();
    div.find("div.war_text").html(msg);
    div.find("span.e_btn2,button.dialog-titlebar-close").unbind("click");
    if (fn2) {
        div.find("span.e_btn2").bind("click", function () {
            fn2();
        })
    }
    div.find("span.e_btn2,button.dialog-titlebar-close").bind("click", function () {
        div.fadeOut();
    })
    div.find("span.e_btn1").unbind("click").bind("click", function () {
        if (fn && _isFunction(fn)) {
            fn.call(null);
        }
        div.fadeOut();
    })
    div.css("margin-top", -(div.height() / 2 + 80) + "px");
}
//回调
var __callback = function (data, popID, update) {
    if (data && popID) {
        var _$ = window.top._$ || window.top.$;
        var div = _$(window.top.document.body).find("#" + popID);
        if (div.length) {
            div.data("_data", { data: data, update: update }).trigger("updateCallback");
        }
    }
}

var _allReplace = function (str, key, value) {
    return str.replace(new RegExp(key, "g"), value);
}

function _each(obj, fn) {
    if (jQuery.isArray(obj)) {
        for (var i = 0, len = obj.length; i < len; i++) {
            if (fn.call(obj[i], i, obj[i]) === false) {
                break;
            }
        }
    } else {
        for (var key in obj) {
            if (obj.hasOwnProperty(key)) {
                if (fn.call(obj[key], key, obj[key]) === false) {
                    break;
                }
            }
        }
    }
}

$(function () {

    $("input.onlyNumber").each(function () {
        var me = $(this), reg = me.attr("reginput") || "\D";
        if (reg == "\D") {
            me.keyup(function () { $(this).val($(this).val().replace(/\D/g, '')); })
            .blur(function () { $(this).val($(this).val().replace(/\D/g, '')); })
            .css("ime-mode", "disabled");
        } else if (reg == "telinfo") {
            me.keyup(function () { $(this).val($(this).val().replace(/[^\d|^-]/g, '')); })
            .blur(function () { $(this).val($(this).val().replace(/[^\d|^-]/g, '')); })
            .css("ime-mode", "disabled");
        } else if (reg = "domain") {
            me.blur(function () {
                var IsURL = function (str_url) {
                    var strRegex = "^((https|http|ftp|rtsp|mms)?://)"
        + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //ftp的user@
        + "(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP形式的URL- 199.194.52.184
        + "|" // 允许IP和DOMAIN（域名）
        + "([0-9a-z_!~*'()-]+\.)*" // 域名- www.
        + "([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // 二级域名
        + "[a-z]{2,6})" // first level domain- .com or .museum
        + "(:[0-9]{1,4})?" // 端口- :80
        + "((/?)|" // a slash isn't required if there is no file name
        + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
                    var re = new RegExp(strRegex);
                    //re.test()
                    if (re.test(str_url)) {
                        return (true);
                    } else {
                        return (false);
                    }
                }, __val = jQuery.trim(me.val());
                if (__val != "" && !IsURL(__val)) {
                    _alert("域名格式错误，请重新填写！");
                    me.focus();
                }
            });
        }
    });
});
/*获取列表勾选中的id*/
var SerializeCheckID = function (isRoot) {
    var ids = "", isone = false;
    $("#nsw_list_table td.td1 input:checked").each(function () {
        if (isRoot && $.trim($(this).val()) == "1") { //如果是1的话跳出当次循环
            return true;
        }
        if (isone) {
            ids += "," + $.trim($(this).val());
        } else {
            ids += $.trim($(this).val());
            isone = true;
        }
    });
    if (/^\d+(,\d+)*$/.test(ids)) {
        return ids;
    }
    return "";
};
var listView = function (url) {
    url = url || "";
    if (!url) {
        var xPath = eval("(" + $.cookie("nswXpath") + ")");
        if (xPath) {
            if (xPath.editmenu.indexOf("?") > -1) {
                var index = xPath.editmenu.indexOf("time=");
                if (index > 0) {
                    xPath.editmenu = xPath.editmenu.replace(/(time=[^#&]*)/, 'time=' + Math.random());
                } else {
                    xPath.editmenu += "&time=" + Math.random();
                }
            } else {
                xPath.editmenu += "?time=" + Math.random();
            }
            url = xPath.editmenu;
        } else {
            url = (window.location + '').toLowerCase();
            if (url.indexOf('?') > 0) {
                url = url.substr(0, url.indexOf('?'));
            }
            if (url.lastIndexOf('_'));
            {
                url = url.substr(0, url.lastIndexOf('_')) + ".aspx";
            }
            url = getPageFilename(url);
            if (_OBJ_[url]) {
                url = _OBJ_[url];
            }
        }
    }
    var frmEditor = top.document.getElementById("frmEditor");
    if (frmEditor) {
        frmEditor.src = url;
    } else {
        window.location = url;
    }
}

var $tv = function (id, value) {
    if (value !== undefined && value!==null) {
        $("#" + id).val(value);
    } else {
        return $("#" + id).val();
    }
}
function shoMsg_2(msg) {
    $(function () {
        _alert(msg, true);
    })
}

//提示
//if (!window.top._alert) {
_alert = function (msg, isSuccess, fn, tims, hd) {
    //alert(msg);
    var _$ = window.top.$;
    hd = hd == null ? true : hd;
    if (isSuccess && _isFunction(isSuccess)) {
        fn = isSuccess;
        isSuccess = false;
    }

    var div = _$(top.document.body).find("div.mask_tips");
    if (!div.length) {
        var html = [
            '<div class="mask_tips">',
            '    <i class="mask_icon mask_war_icon f_fl"></i>',
            '    <span class="mask_text f_fl"></span>',
            '</div>'
        ].join('');
        _$(top.document.body).append(html);
        div = _$(top.document.body).find("div.mask_tips").hide();
    }
    if (isSuccess) {
        div.find("i.mask_icon").attr("class", "mask_icon mask_able_icon f_fl");
    } else {
        div.find("i.mask_icon").attr("class", "mask_icon mask_war_icon f_fl");
    }
    if (window.top._alertCallback && fn == null) {
        fn = window.top._alertCallback;
    }
    var _fn = fn;
    if (fn) {
        _fn = function () {
            window.top._alertCallback = null;
            fn();
        }
    }
    div.find("span.mask_text").html(msg);
    var IsShow = false;
    var Call = function () {
        if (IsShow) {
            return;
        }
        IsShow = true;
        if (_isFunction(_fn)) {
            _fn.call(null);
        }
    }
    div.stop(true, true).fadeIn();
    if (hd) {
        div.unbind("click").bind("click", function () {
            Call();
            div.stop(true, true).fadeOut();
        }).delay(tims || 1500).fadeOut(500, function () {
            Call();
        });
    }
}

if (window.self == window.top.frames["frmEditor"]) {
    $(function () {
        $(window.self).bind("load", function () {
            if (window.top.SetHeight) {
                window.top.SetHeight();
            }
            window.top.$("body,html").animate({ scrollTop: 110 }, 100);
        });
    })
}

OpenBlank = function (url, keyValue) {
    var html = '';
    if (keyValue) {
        for (var i in keyValue) {
            html += '<input type="text" name="' + i + '" value="' + keyValue[i] + '" />';
        }
    }
    $(document.body).append([
    '<form action="' + url + '" target="_blank" method="get" name="blankForm" id="blankForm">' + html,
    '    <input type="submit" />',
    '</form>'
    ].join(''));
    $("#blankForm").submit().remove();
}

function HidePop(id) {
    if (id) {
        window.top.$("#" + id).hide();
    } else {
        var maxZindex = 0;
        var divs = window.top.$("div.nsw_dilogbor:visible");
        divs.each(function () {
            var me = window.top.$(this);
            if (me.css("z-index")) {
                var zindex = parseInt(me.css("z-index"));
                if (!isNaN(zindex) && zindex > maxZindex) {
                    maxZindex = zindex;
                    me.attr("maxZindex", maxZindex);
                }
            }
        });
        divs.filter("[maxZindex='" + maxZindex + "']:first").hide();
    }
}

//点击图片展示效果
var pictureEnlarge = function (img) {
    img = $(img);
    img.unbind("click").bind("click", function () {
        var me = $(this);
        loadCss("skins/fancybox/jquery.fancybox.css", window.top.document);
        loadJs("js/easyui/jquery.fancybox.js", function () {
            window.top.$.fancybox.open(
                [
                    {
                        href: me.attr("src"),
                        title: me.attr("title")
                    }
                ]
            );
        }, window.top.document);
    });
};
//点击图片展示效果
var ShowImg_ = function (img) {
    img = $(img);
    loadCss("skins/fancybox/jquery.fancybox.css", window.top.document);
    loadJs("js/easyui/jquery.fancybox.js", function () {
        window.top.$.fancybox.open([{ href: img.attr("src"), title: img.attr("title")}]);
    }, window.top.document);
}
var close_ = function () {
//    var userAgent = navigator.userAgent;
//    if (userAgent.indexOf("Firefox") != -1 || userAgent.indexOf("Chrome") != -1) {
//        window.location.href = "about:blank";
//    } else {
        window.opener = null;
        window.open("", "_self");
        window.close();
    //}
}

