if (typeof (window.loadjs_obj) == "undefined") {
    window.loadjs_obj = {};
}
window.loadjs_obj["/edit.js"] = true;
var GetFrmEditor_ = function () {
    if (window.self == window.top) {
        return null;
    }
    if (window.self == window.top.frames["frmEditor"]) {
        return $(window.top.document.body).find("#frmEditor");
    }
    return null;
}

//上传按钮
var _SetUpload = function (options) {
    var def =
    {
        action: 'SaveHttpPostedFile',
        ajax: "handler/upload.ashx",
        buttonID: 'Upload_Img',
        fileName: 'imgFile',
        input: null, //设置保存文件路径
        dir: 'image',
        imgwarp: false,
        types: 'gif,jpg,jpeg,png,bmp', //上传类型
        iframeid: 'upload_iframe_',
        verification: function (fileName) {
            var me = this;
            var fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1);
            var types = ',' + me.options.types + ',';
            if (!fileExtension || types.indexOf(fileExtension) == -1) {
                _alert("请上传" + me.options.types + "类型的文件！");
                return false;
            }
            return true;
        },
        loadedSuccessfully: function () {
            var me = this;
            var offset = me.btn.offset();
            me.formFileUpload.css({ top: offset.top, left: offset.left, width: me.btn.outerWidth(), height: me.btn.outerHeight() });
            $(me.btn).hover(function () {
                var _me = $(this);
                var _offset = _me.offset();
                me.formFileUpload.css({ top: _offset.top, left: _offset.left }).show();
            })
        },
        showImg: function (data) {
            var me = this;
            var url = data.url;
            me.btn = $(me.btn);
            if (url) {
                var ext = url.substr(url.lastIndexOf('.') + 1).toLowerCase();
                if (",ico,jpg,png,gif,bmp,jpeg,".indexOf(ext) > 0) {
                    if (!me.options.imgwarp) {
                        var img = me.btn.siblings("img.show_").show();
                        if (img.length) {
                            img.attr("src", url);
                        } else {
                            me.btn.after('<img src="' + url + '" height="' + me.btn.outerHeight() + 'px" class="show_" style="margin-left:5px;" />');
                            img = me.btn.siblings("img.show_").show();
                        }
                        pictureEnlarge(img);
                    } else {
                        var warp = me.options.imgwarp.jquery ? me.options.imgwarp : me.btn.parent().find(me.options.imgwarp);
                        var img = warp.find("img").show();
                        if (img.length) {
                            img.attr("src", url);
                        } else {
                            warp.append('<img src="' + url + '" height="' + me.btn.outerHeight() + 'px" class="show_"  />');
                            img = warp.find("img").show();
                        }
                        pictureEnlarge(img);
                    }
                } else if (',doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2,'.indexOf(ext) > 0) {
                    if (!me.options.imgwarp) {
                        var a = me.btn.siblings("a.a_upd").show();
                        if (a.length) {
                            a.attr("href", url);
                        } else {
                            me.btn.after('<a class="a_upd" target="_blank" href="' + url + '" style="display: inline-block; padding: 0px 10px; border: 1px solid #dcdcdc;margin-left: 10px;line-height:' + me.btn.height() + 'px">立即下载</a>');
                        }
                    } else {
                        var warp = me.options.imgwarp.jquery ? me.options.imgwarp : me.btn.parent().find(me.options.imgwarp);
                        var a = warp.find("a.a_upd").show();
                        if (a.length) {
                            a.attr("href", url);
                        } else {
                            warp.append('<a class="a_upd" target="_blank" href="' + url + '" style="display: inline-block; padding: 0px 10px; border: 1px solid #dcdcdc;margin-left: 10px;line-height:' + me.btn.height() + 'px">立即下载</a>');
                        }
                    }
                }
            }
        },
        uploadSuccess: function (data, isNoShow) {
            var me = this;
            if (!isNoShow) {
                _alert("上传成功！");
            }
            me.btn = $(me.btn);
            var url = data.url;
            if (url) {
                if (me.options.input) {
                    $j(me.options.input).val(url);
                }
                if (me.options.showImg) {
                    me.options.showImg.call(me, data);
                }
            }
        },
        afterUpload: function (data, isNoShow) {
            var me = this;
            if (data.error === 0) {
                me.options.uploadSuccess.call(me, data, isNoShow);
            } else {
                _alert(data.message ? data.message : "上传失败！");
            }
        },
        afterError: function (data) {
            _alert("上传失败，请稍后重试！\r\n" + data);
        },
        clear: function () {
            var me = this;
            var data = {};
            $(me.formFileUpload).find("[name!='" + me.options.fileName + "']").each(function () {
                data[$(this).attr("name")] = $(this).val();
            })
            me.formFileUpload[0].reset();
            for (var name in data) {
                $(me.formFileUpload).find("[name='" + name + "']").val(data[name]);
            }
        }
    };
    var me = this;
    me.options = options = jQuery.extend(def, options);
    me.options.ajax = me.options.action ? (me.options.ajax + (me.options.ajax.indexOf('?') > 0 ? '&' : '?') + 'action=' + me.options.action) : me.options.ajax;
    me.options.ajax = me.options.dir ? (me.options.ajax + (me.options.ajax.indexOf('?') > 0 ? '&' : '?') + 'dir=' + me.options.dir) : me.options.ajax;
    var html = [
        '<form action="' + me.options.ajax + '" id="' + me.options.buttonID + '_formFileUpload" method="post" enctype="multipart/form-data" target="' + me.options.iframeid + '" style="position:absolute;z-index:100;overflow:hidden;opacity:0;filter:alpha(opacity=0);background:#fff;">',
            '<div style="position:relative">',
                '<input type="file" name="' + me.options.fileName + '" style="font-size:80px;position:absolute;right:0;top:0;opacity:0;_filter:alpha(opacity=0);height: 200px;line-height: 200px;cursor: pointer;" title="点击上传" />',
                (me.options.imgurl ? '<input type="hidden" name="ReplaceFile" value="' + me.options.imgurl + '"/>' : ''),
            '</div>',
            '<input type="hidden" name="dir" value="' + me.options.dir + '"/>',
        '</form>'].join('');

    me.btn = $j(me.options.buttonID);
    if (!me.btn.length) {
        _alert("不存在" + me.options.buttonID);
        return me;
    }
    var upload_iframe_html = '<iframe id="' + me.options.iframeid + '" name="upload_iframe_" style="display:none;"></iframe>';
    me._iframe = $j(options.iframeid);
    if (!me._iframe.length) {
        $(document.body).append(upload_iframe_html);
        me._iframe = $j(options.iframeid);
    }
    me.formFileUpload = $j(me.options.buttonID + "_formFileUpload");
    if (!me.formFileUpload.length) {
        $(document.body).append(html);
        me.formFileUpload = $j(me.options.buttonID + "_formFileUpload").hide();
    }

    me.formFileUpload.mouseout(function () {
        $(this).hide();
    })
    me.formFileUpload.find("input[name='imgFile']").unbind().bind("change", function () {
        var fileName = $(this).val();
        if (fileName) {
            if (me.options.types && options.types !== "*" && me.options.verification && me.options.verification.call(me, fileName) === false) {
                me.options.clear.call(me);
                return me;
            }
            me._iframe.bind('load', function () {
                me._iframe.unbind();
                var doc = window.frames[me.options.iframeid].document;
                var data = null;
                var str = doc.body.innerHTML;
                str = _unescape(str);
                me._iframe.attr("src", "javascript:false");
                try {
                    data = jQuery.parseJSON(str);
                } catch (e) {
                    me.options.afterError.call(me, '<!doctype html><html>' + doc.body.parentNode.innerHTML + '</html>');
                }
                if (data && data.error !== undefined) {
                    me.options.afterUpload.call(me, data, false);
                }
                me._iframe.attr("src", "");
                me.options.clear.call(me);
            });
            me.formFileUpload.submit();
        }
    })
    if (me.options.input) {
        var value = $j(me.options.input).val();
        if (value) {
            me.options.uploadSuccess.call(me, { error: 0, url: value }, true);
        }
    }
    me.options.loadedSuccessfully.call(me);
    return me;
}
//粘贴
function bindChangeHandler(input, fun) {
//    if (input == null) return;
//    if ("onpropertychange" in input) {//IE6、7、8，属性为disable时不会被触发
//        input.onpropertychange = function () {
//            if (window.event.propertyName == "value") {
//                if (fun) {
//                    fun.call(this, window.event);
//                }
//            }
//        }
//    } else {
//        //IE9+ FF opera11+,该事件用js改变值时不会触发，自动下拉框选值时不会触发
//        input.addEventListener("input", fun, false);
//    }
}
function GetPingYing(src, data) {
    //var isXX = '|news|help|agent|product|project|'.indexOf(data.type) == -1;
    var me = $(src);
    loadJs("js/other/ajax.js", function () {
        _PostAjax("getPingYing", {
            title: encodeURIComponent(me.val().trim()),
            hidfile: $j(data.hidfile).val().trim(),
            type: data.type,
            pylength: 10
        }, function (msg) {
            var _data = _Json(msg);
            if (_data.error) {
                $j(data.pyid).val(_data.msg).trigger("keyup").trigger("hasValue");
            } else {
                _alert(_data.msg);
            }
        });
    })
}

//显示剩余字数、已输入字数
var SetEnterWords = function (inputs) {
    $(inputs).each(function () {
        var me = $(this);
        var _tagName = me[0].tagName;
        var div = me.parents("div.seo_r,div.add_r").find(".f_ib_");
        if (div.length == 0) {
            if (_tagName == "INPUT") {
                var span = me.parents("span");
                if (span.length) {
                    span.after('<em class="inp_tips_gray pd010 f_lht27 f_fl f_ib_"></em>');
                    div = span.next();
                }
            } else if (_tagName == "TEXTAREA") {
                var span = me.parents("div.bord_gray");
                if (span.length) {
                    span.after('<div class="f_cb f_mt5 f_lht27"><div class="f_fl f_mr10 f_ib_"></div></div>');
                    window.top.SetHeight();
                    div = span.next();
                }
            }
        }
        if (div.length) {
            me.keyup(function () {
                var _me = $(this);
                var _value = _me.val();
                /*初始化数据*/
                var data = null;
                if (!(data = _me.data("keyup_data"))) {
                    data = {};
                    _me.attr("triggerkeyup", "triggerkeyup");
                    var tagName = _me[0].tagName.toLowerCase();
                    var maxlength = _me.attr("maxlength") || _me.attr("_maxlength") || (tagName == "textarea" ? "200" : "30");
                    maxlength = parseInt(maxlength, 10);
                    maxlength = isNaN(maxlength) ? 30 : maxlength;
                    data.maxlength = maxlength;
                    data.temp = _me.attr("temp") || (tagName == "input" ? "{length}/{max_length}字符" : "你还可以输<span class='color_orange'>{surplus_length}</span>个字符");
                    data.temp = data.temp.replace('{max_length}', data.maxlength);
                    data.div = _me.parent().hasClass("msg_content") ? _me.siblings(".f_ib_").eq(0) : _me.parents("div.seo_r,div.add_r").find(".f_ib_").eq(0);
                    _me.data("keyup_data", data);
                }
                /*初始化数据 end*/
                if (_value == _me.attr("_defvalue")) {
                    return;
                }
                if (_me.attr("id") == "txtTag") {
                    _value = _value.replace(/，/g, ',');
                    _me.val(_value);
                }
                var length = GetUnicodeLenth(_value);
                var surplus_length = data.maxlength - length;
                if (surplus_length < 0) {
                    var _length = surplus_length;
                    var index = -1;
                    var c;
                    //console.log(surplus_length);
                    for (var i = _value.length - 1; i > (data.maxlength - 2) / 2 && _length < 0; i--) {
                        c = _value.charCodeAt(i);
                        if ((c >= 0x0001 && c <= 0x007e) || (0xff60 <= c && c <= 0xff9f)) {
                            _length++;
                        } else {
                            _length += 2;
                        }
                        index = i;
                    }
                    if (index > 0) {
                        //console.log(GetUnicodeLenth(_value));
                        _value = _value.substr(0, index);
                        //console.log(GetUnicodeLenth(_value));
                        _me.val(_value);
                        length = data.maxlength;
                    }
                    surplus_length = 0;
                }
                _me.attr("_defvalue", _value);
                data.div.html(data.temp.replace('{length}', length).replace('{surplus_length}', surplus_length));
            }).keyup();
        }
    });
}

$(function () {
    //双击输入
    $(".click_input").click(function () {
        var me = $(this);
        var input = me.children("input");
        if (!input.length) {
            var value = _escape(me.text());
            if (!me.data("defValue")) {
                me.data("defValue", value);
            }
            me.html('<i style="font-style:normal;text-align:center">' + value + '<i>');
            me.append('<input type="text" style="border:0;text-align:center;width:' + me.width() + 'px;height:' + me.height() + 'px;line-height:' + me.height() + 'px" />');
            me.children("i").text(value).hide();
            me.children("input").val(value).show().unbind("blur").bind("blur", function () {
                var val = $(this).hide().val();
                me.children("i").text(val || me.data("defValue")).show();
            });
        } else {
            var value = me.children("i").hide().text();
            me.children("input").val(value).show();
        }
        me.children("input").focus();
    });

    //创建拼音
    $("input.CreatePY").change(function () {
        var me = $(this);
        var value = $.trim(me.val());
        if (value == "") { if (me.attr("triggerkeyup")) { _alert("请填写标题名称！"); } return; }
        var data = me.data("data") || (me.attr("data-src") ? _Json(me.attr("data-src")) : null) || null;
        if (data) {
            if (!me.data("data")) {
                data.type = data.type || "-";
                data.pyid = data.pyid || "txtFileName";
                data.hidfile = data.hidfile || "hidFileName";
                me.data("data", data);
            }
            if (me.attr("checkfile") === "true") {//只用于验证
                me.attr("checkfile", "false");
            } else {
                var input = $j(data.pyid);
                if (input.length && (input.val().length == 0 || ($j(data.hidfile).val() == "")) || me.attr("_change_") === "true") {//用于更新
                    me.attr("_change_", "false");
                    GetPingYing(me, data);
                }
            }
        }
    })
    $("div.GetPY,a.GetPY").click(function () {
        $("input.CreatePY").attr("_change_", "true").change();
    })

    $("a.CheckFile").click(function () {
        var input = $("input.CreatePY").attr("checkfile", "true").change();
        var data = input.data("data");
        if (data) {
            loadJs("js/other/ajax.js", function () {
                var py = encodeURIComponent($j(data.pyid).val().trim());
                _PostAjax("checkFile", {
                    py: py,
                    hidfile: $j(data.hidfile).val().trim(),
                    type: data.type
                }, function (msg) {
                    var _data = _Json(msg);
                    if (_data.error) {
                        if (_data.msg === "true") {
                            _alert("可以使用", true);
                        } else {
                            _alert("已经存在相同路径！", false, function () {
                                $j(data.pyid).val($j(data.pyid).val() + "_1").trigger("hasValue");
                            });
                        }
                    }
                });
            })
        }
    })

    bindChangeHandler(document.getElementById("txtFileName"), function () {
        $("#txtFileName").change();
    });
    $("#txtFileName").change(function () {
        var me = $(this);
        var input = $("input.CreatePY").attr("checkfile", "true").change();
        var data = input.data("data");
        if (data) {
            loadJs("js/other/ajax.js", function () {
                var py = encodeURIComponent(me.val().trim());
                if (py == "") {
                    input.attr("checkfile", "false").change();
                    return;
                }
                _PostAjax("checkFile", {
                    py: py,
                    hidfile: $j(data.hidfile).val().trim(),
                    type: data.type
                }, function (msg) {
                    var _data = _Json(msg);
                    if (_data.error) {
                        if (_data.msg === "true") {
                            _alert("可以使用", true);
                        } else {
                            _alert("已经存在相同路径！", false, function () {
                                me.val(me.val() + "_1").trigger("hasValue");
                            });
                        }
                    }
                });
            })
        }
    }).keyup(function () {
        var me = $(this);
        if (/[^a-zA-Z0-9\_]/.test(this.value)) {
            this.value = this.value.replace(/[^a-zA-Z0-9\_]/g, '');
        }
    }).each(function () {
        var me = $(this);
        if (me.val() == "") {
            $("input.CreatePY").change();
        }
    });
})

/*选项卡切换*/
$(function () {
    $(window).bind("_resize_", function () {
        $("div.settab_lh18:visible div.f_cb:visible label.add_label").each(function () {
            var me = $(this);
            var lh = parseInt(me.css("line-height"));
            var h = me.height();
            if (!isNaN(lh) && h > 0) {
                var max = h / lh;
                if (max > 1) {
                    me.css("padding-top", 0);
                } else {
                    me.css("padding-top", "5px");
                }
            }
        })
        window.top.SetHeight();
    })
    $(window).resize(function () {
        $(window).trigger("_resize_");
    })
    var isresize = false;
    jQuery.jqtab = function (tabtit, tab_conbox, shijian) {
        $(tab_conbox).children("li").hide();
        $(tabtit).children("li:first").addClass("thistab active").show();
        $(tab_conbox).children("li:first").show();

        var conts = $(tab_conbox).children("[class*='cont']").each(function (index) {
            $(this).addClass("tab_cat_cont").attr("tab_index", index);
        });
        $(tabtit).children("li").unbind(shijian).bind(shijian, function () {
            if ($(this).hasClass("noclick")) { return false; }
            $(this).addClass("thistab active").siblings("li").removeClass("thistab active");
            var activeindex = $(tabtit).children("li").index(this);
            conts.hide().eq(activeindex).show();
            var FrmEditor = GetFrmEditor_();
            if (FrmEditor) {
                FrmEditor.css("height", $(document.body).height() + "px");
            }
            $(window).trigger("_resize_");
            isresize = true;
            return false;
        }).eq(0).trigger(shijian);
    };
    /*调用方法如下：*/
    //选项卡
    $.jqtab(".j_recordCon", ".j_recordCon_c", "click");
    if (isresize === false) {
        $(window).trigger("_resize_");
    }
    //自动排版
    $("span.f_underline").click(function (e) {
        var me = $(this);
        var textarea = me.parents("div.add_typeset").find("textarea[class!='ke-edit-textarea']");
        if (me.hasClass("f_underline_content")) {//编辑器
            var editor = textarea.data("editor");
            if (editor) {
                editor.clickToolbar("typesetting");
            }
        } else {
            if (textarea.length && textarea.val()) {
                var value = textarea.val();
                value = value.replace(/(<[^>]+>)([\s\S]*?)(<\/[^>]+>)/ig, function ($0, $1, $2, $3) {
                    return $2 + "\r\n";
                });
                textarea.val(value);
            }
        }
    })

    $(".advan_btn").unbind("click").click(function () {
        var target = $(this).attr("datasrc") || ".advan_posi";
        if (!target) { return; }
        target = $(target);
        target.is(":hidden") ? target.fadeIn() : target.fadeOut();
    });
});

//删除扩展标签
var delContent = function (src) {
    var me = $(src).parent("li");
    var first = me.parent().children(":first");
    SetContentToInput(first);
    me.remove();
}

//将编辑器的内容保存到input
var SetContentToInput = function (src, isSave) {
    var me = $(src);
    var cur = me.siblings(".cur");
    if (!cur.length && !isSave) {//自己
        return;
    }
    var editor = null;
    var _textarea = me.parents("div.extend_warp").find("textarea[name^='content_']");
    if (_textarea.length && (editor = _textarea.data("editor")) != null) {
        if (!cur.length && isSave) {
            me.find("input[type='hidden']").val(editor.html());
        } else {
            me.addClass("cur");
            cur.removeClass("cur");
            cur.find("input[type='hidden']").val(editor.html());
            editor.html(me.find("input[type='hidden']").val());
            editor.focus();
        }
    } else {
        _alert("编辑器不存在，或网络延迟，请刷新重试，如还有问题请检查您的代码！");
    }
}
//保存所有
var saveAllContent = function () {
    SetContentToInput($(".type_marklist li.cur"), true);
    var separate = $j("hdSeparate").val();
    var lis = $("div.extend_warp ul.type_marklist li:gt(0)");
    var hdAllContent = $j("hdAllContent");
    var hdAllTitle = $j("hdAllTitle");

    var titles = "";
    var allcontent = "";
    lis.each(function () {
        var me = $(this);
        var input = me.children("input");
        allcontent += input.val() + separate;
        titles += me.attr("title") + separate;
    })
    hdAllTitle.val(titles);
    hdAllContent.val(allcontent);
    return true;
}
//产品属性单选
var SetCurrentRadValue = function (id, value) {
    $j(id).val(value);
}
//产品属性多选
var SetCurrentCBLValue = function (src, id, text, value) {
    if (!text) { return; }
    var input = $j(id);
    var val = input.val();
    var _va = ',' + val + ',';
    var _text = ',' + text + ',';
    if (_va.indexOf(_text) > -1) {
        if (!src.checked) {
            val = _va.replace(/,/ig, ',,').replace(_text, '').replace(/,,/ig, ',').replace(/(^,)|(,$)/ig, '');
        }
    } else {
        if (src.checked) {
            val += (val == "" ? "" : ",") + text;
        }
    }
    input.val(val);
}

$(function () {
    //上传
    $(".Upload_Clik").each(function () {
        var me = $(this);
        var data = me.attr("data-src") ? _Json(me.attr("data-src")) || {} : {};
        if (jQuery.isEmptyObject(data)) {
            var imgurl = me.attr("imgurl");
            var ajax = me.attr("ajax");
            var action = me.attr("action");
            var buttonID = me.attr("id");
            var input = me.attr("input");
            var types = me.attr("types");
            var imgwarp = me.attr("imgwarp");
            var dir = me.attr("dir");
            var directory = me.attr("directory");
            var obj = {};
            if (input) {
                obj.input = input;
            }
            if (ajax) {
                obj.ajax = ajax;
            }
            if (action) {
                obj.action = action;
            }
            if (types) {
                obj.types = types;
            }
            if (imgwarp) {
                obj.imgwarp = imgwarp;
            }
            if (dir) {
                obj.dir = dir;
            }
            if (directory) {
                obj.directory = directory;
            }
            if (imgurl) {
                obj.imgurl = imgurl;
            }
            data = obj;
        }
        if (data.imgurl || (data.types && data.types.indexOf('doc') > -1)) {
            if (buttonID) {
                data.buttonID = buttonID;
                new _SetUpload(data);
            }
        } else {
            data.max = data.max || 1;
            Timeout(function () {
                loadJs("js/common/editor.js", function () {
                    new img_gallerywindow(me, data, "upload_clik");
                })
            });
        }
    })
    //已经填写了内容的
    $("div.msg_content").find("input").each(function () {
        if (this.value != "") {
            $(this).parents("div.msg_content").show().prev("div.msg_btn").hide();
        }
    })

    Timeout(function () {
        SetEnterWords("textarea.EnterWords_Decreasing,input.EnterWords");

        var dates = $("input.calendar_tx");
        if (dates.length) {
            Timeout(function () {
                loadJs("/Component/My97DatePicker/WdatePicker.js", function () {
                    dates.attr("readonly", "true").bind("focus", function () {
                        WdatePicker({ el: $(this).attr("id"), dateFmt: $(this).attr("dateFmt") || 'yyyy-MM-dd HH:mm', onpicked: function () { $(this).trigger("hasValue"); } });
                    });
                    dates.next("i.calendar_icon").bind("click", function () {
                        var id = $(this).prev("input").attr("id");
                        if (id) {
                            WdatePicker({ el: id, dateFmt: $(this).attr("dateFmt") || 'yyyy-MM-dd HH:mm', onpicked: function () { $("#" + id).trigger("hasValue"); } });
                        }
                    });
                })
            });
        }
    })

    var url = getPageFilename();
    if (url.indexOf("_edit.aspx") > 0 || window.EditPage === true) {
        var _div_view_btn = $("div.view_btn");
        var _close = _div_view_btn.attr("close") === "true";
        var _reset_icon = _div_view_btn.find("i.reset_icon");
        var fl = _div_view_btn.find("span:eq(0)").hasClass("fl") ? " fl" : "";
        if (_reset_icon.length > 0) {
            _reset_icon.parent().after('<span class="e_btn2 f_ml35 f_csp' + fl + '" onclick="listView();"><i class="back_icon"></i>返 回</span>');
        } else {
            _div_view_btn.append('<span class="e_btn2 f_ml35 f_csp' + fl + '" onclick="listView();"><i class="back_icon"></i>返 回</span>');
        }
        if (_close && window.top == window.self) {
            listView = function () {
                close_();
            }
        }
    }
    $("#btnDel").hide();

    //详细页
    $("div.msg_btn").click(function () {
        var msg_content = $(this).siblings(".msg_content");
        if (msg_content.length) {
            $(this).hide();
            msg_content.fadeIn(500);
        }
    });
});

//获取文字长度，中文字符集按2个长度来计算
var GetUnicodeLenth = function (input) {
    var len = 0;
    if (input) {
        for (var i = 0; i < input.length; i++) {
            if (input.charCodeAt(i) > 127 || input.charCodeAt(i) == 94) {
                len += 2;
            } else {
                len++;
            }
        }
    }
    return len;
};
//外部链接禁止点击选项卡
var initTabCut = function (has) {
    if (has) {
        $(".j_recordCon>li:gt(0)").addClass("noclick");
    } else {
        $(".j_recordCon>li:gt(0)").removeClass("noclick");
    }
};

