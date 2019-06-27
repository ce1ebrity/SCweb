if (typeof (window.loadjs_obj) == "undefined") {
    window.loadjs_obj = {};
}
window.loadjs_obj["/popup.js"] = true;

//避免添加的过多
if (!window.top.debug) {
    if (window.self != window.top) {
        var href = window.location + '';
        href = href.toLowerCase();
        var frmEditorUrl = $(window.top.document.body).find("#frmEditor");
        if (frmEditorUrl.length) {
            frmEditorUrl = frmEditorUrl.attr("src").toLowerCase();
            if (href.indexOf(frmEditorUrl) > 0) {
                $(window.top.document.body).find("div.nsw_dilogbor").remove();
            }
        }
    }
}

var MARK = "";
var URL = (window.location + '').toLowerCase();
if (URL.indexOf("product") > -1) {
    MARK = "product";
} else if (URL.indexOf("project") > -1) {
    MARK = "project";
} else if (URL.indexOf("agent") > -1) {
    MARK = "agent";
} else if (URL.indexOf("news") > -1) {
    MARK = "news";
} else if (URL.indexOf("help") > -1) {
    MARK = "help";
}

var Separate = "\\#\\$\\$\\#";
var _PopUp = function (options, iframes) {
    var def = {
        title: '网站操作信息提示',
        popid: null,
        width: 740,
        height: 550,
        el: TOP.document.body,
        html: null,
        css: "skins/base/dialog.css",
        show: function () {
            var me = this;
            me.pop.show();
        },
        hide: function () {
            var me = this;
            me.pop.hide();
        },
        callback: null,
        btn1_Click: function () {

        },
        btn2_Click: function () {
            this.pop.hide();
        }
    }
    var me = this;
    var top$ = window.top.$ || window.$;
    if (!def.popid) {
        def.popid = "pop_" + generateMixed(25) + (hashCode(options.title || def.title) + '').replace('-', '_');
    }
    var op = me.options = me.op = options = jQuery.extend({}, def, options);

    loadCss(op.css, TOP.document);
    me.doc = top$(TOP.document.body);
    me.pop = me.doc.find("#" + op.popid);
    if (!me.pop.length) {
        var html = [
            '<div class="nsw_dilogbor" style="width:' + op.width + 'px;margin-left:-' + (op.width / 2) + 'px;" id="' + op.popid + '">',
                '<div class="dialog-titlebar f_cb">',
                    '<span class="dialog-title">' + op.title + '</span>',
                    '<button class="dialog-titlebar-close"></button>',
                '</div>',
                '<div class="dialog_content">',
                    (op.html ? op.html : ''),
                '</div>',
            '</div>'
        ].join('');
        me.doc.append(html);
        me.pop = me.doc.find("#" + op.popid).hide();

        me.pop.content = me.pop.find("div.dialog_content");
        me.pop.titlebar = me.pop.find("div.dialog-titlebar");
        me.pop.find("button.dialog-titlebar-close").bind("click", function () {
            me.hide();
        })

        me.setHtml = function (html) {
            me.pop.content.html(html);
        }
        me.setHeight = function (height) {
            height = parseInt(height);
            if (!isNaN(height)) {
                me.pop.height(height);
                me.pop.css("top", "50%");
                me.pop.css("margin-top", "-" + (height / 2) + "px");
            }
        }
        me.hide = function () {
            if (me.op.hide && _isFunction(me.op.hide)) {
                me.op.hide.call(me);
            } else {
                me.pop.hide();
            }
        }
        if (me.options.height) {
            me.setHeight(me.options.height);
        }
        if (iframes) {
            var src = iframes.src;
            me.iframes = iframes;
            var height = iframes.height || op.height - me.pop.titlebar.height() - 30;
            if (isNaN(height)) {
                height = "auto";
            }
            var cssName = iframes.cssName || 'pad1520';
            var width = iframes.width || op.width - 40;
            src += (src.indexOf('?') > 0 ? '&' : '?') + "popid=" + op.popid;
            var iframeID = "iframe_" + op.popid;
            me.pop.content.append('<div class="' + cssName + '"><iframe id="' + iframeID + '" name="' + iframeID + '" src="' + src + '" width="' + width + '" height="' + height + '" frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes"></iframe><div class="clear"></div></div>');
            me.iframe = window.top.frames[iframeID];
            me.pop.data("me", me);

            top$("#" + iframeID).bind("load", function () {
                //alert(1);
                if (height == "auto") {
                    me.setHeight($(me.iframe.document.body).height() + me.pop.titlebar.height() + 30);
                }
                if (iframes.callback && _isFunction(iframes.callback)) {
                    iframes.callback.call(me);
                }
                $(me.iframe.document.body).find("i.cancel_icon,i.viewCancle_icon").parent().bind("click", function (event) {
                    me.options.btn2_Click.call(me);
                })
            })

            //console.log(iframes.updateCallback);
            if (iframes.updateCallback) {
                window.top.$(me.pop).bind("updateCallback", function () {
                    iframes.updateCallback.call(me, me.pop.data("_data"));
                })
            }
            //添加数据时和刷新时调用
            if (iframes.reset && _isFunction(iframes.reset)) {
                $(me.pop).bind("reset", function () {
                    iframes.reset.call(me);
                })
            }
        } else {
            me.pop.find("span.e_btn1").bind("click", function (event) {
                event.stopPropagation();
                me.options.btn1_Click.call(me);
            })
            me.pop.find("span.e_btn2").bind("click", function (event) {
                event.stopPropagation();
                me.options.btn2_Click.call(me);
            })
        }
        if (op.callback && _isFunction(op.callback)) {
            op.callback.call(me);
        }
    }
    if (me.options.show) {
        me.options.show.call(me);
    }
    return me;
}
 //调用
var NewPopUp = function (options, iframes) {
    var pop = new _PopUp(options, iframes);
    pop.show = function () {
        pop.options.show.call(pop)
    };
    return pop;
}
var SelectTagPopUp = function (options) {
    var def = {
        title: '标签提取库',
        html: '<div class="tp1 tp1bg">已选标签：</div>',
        height: 540
    }
    var op = jQuery.extend({}, def, options);
    var iframes = {
        src: "selecttag.aspx",
        height: 430,
        reset: function () {

        },
        callback: function () {
            var me = this;
            var tp1bg = me.pop.content.find("div.tp1bg");
            var max = 10;
            $(me.iframe.document.body).find(".mk_ul span").unbind("click").bind("click", function () {
                var span = $(this);
                if (span.children(".zon").length) {
                    if (tp1bg.children("span").length > max) {
                        span.children(".zon").removeClass("zon");
                    } else {
                        tp1bg.append("<span oid='" + span.attr("oid") + "'>" + span.text().trim() + "</span>");
                    }
                } else {
                    tp1bg.find("span[oid='" + span.attr("oid") + "']").remove();
                }
            }).end().find("span.queding").unbind("click").bind("click", function () {
                var input = $j(me.options.input);
                if (input.length) {
                    var text = "";
                    tp1bg.children("span").each(function () {
                        text += $(this).text().trim() + ",";
                    })
                    text = text.trim(',');
                    if (text.length > 0) {
                        input.val(text).trigger("hasValue").trigger("keyup");
                    }
                }
                me.pop.hide();
            }).end().find("span.quxiao").unbind("click").bind("click", function () {
                tp1bg.children("span").remove();
                me.pop.hide();
            })
        }
    };

    iframes = jQuery.extend(iframes, op.iframes || {});
    return NewPopUp(op, iframes);
}
//批量添加
var BatchAddColumnPopUp = function (options) {
    var def = {
        title: '批量添加分类',
        placeholder: '填写格式：分类之间请使用回车分开，一个分类一行',
        width: 600,
        height: 440,
        saveTitle: "添 加",
        show: function () {
            var me = this;
            me.pop.show();
        },
        callback: function () {
            var me = this;
            var op = me.op;

            var select = $("select.pass_faq[name='cid']:first");
            me.pop.find("span.s_selt").append(select.clone());
            me.pop.select = me.pop.find("span.s_selt select");
            me.text = me.pop.find("textarea.text_area");

            if (op.action) {
                loadJs("js/other/ajax.js", function () {
                    me.pop.find("span.e_btn1").bind("click", function () {
                        if (me.text.val() == "") {
                            _alert("请填写分类！");
                            return;
                        }
                        _PostAjax(op.action, me.pop.find("form").serialize(), function (data) {
                            data = _Json(data);
                            if (data.error) {
                                _alert(data.msg, true, function () {
                                    window.location = window.location;
                                    me.hide();
                                }, 500);
                            } else {
                                _alert(data.msg);
                            }
                        });
                    })
                });
            }
        }
    }
    var op = jQuery.extend({}, def, options);
    var html = [
            '<div class="tp1 tp1bg f_cb">',
			'	<div class="f_fl tp_ico"><i></i>分类列表  （此处可以直接拷贝粘贴word文档里面的产品分类到此处进行批量添加）</div><a class="tp_link f_fl" href="#"></a>',
			'</div>',
            '<div class="f_cb pad1520 extend_sort">',
            '    <form method="post"><div class="bord_gray compos_box">',
		    '        <textarea class="text_area tx1" name="columns" placeholder="' + op.placeholder + '"></textarea>',
            '        <input type="hidden" name="typename" value="' + op.type + '"/>',
		    '        <div class="compos f_cb">',
			'            <span class="f_fl f_underline">自动排版</span>',
		    '        </div>',
	        '    </div>',
	        '    <div class="f_cb f_mt15">',
		    '        <span class="f_fl f_lht27 f_mr10">属性分类</span>',
		    '        <div class="f_cb add_sv">',
			'            <span class="clear_bd s_selt f_fl px215 f_mr15">',
			'	            <i class="revise_sub"></i>',
			'            </span>',
		    '        </div>',
	        '    </div><form>',
	        '    <div class="f_cb f_mt15">',
		    '        <div class="view_btn"><span class="e_btn1 f_csp "><i class="add_icon"></i>' + op.saveTitle + '</span><span class="e_btn2 f_ml35 f_csp "><i class="cancel_icon"></i>取 消</span></div>',
	        '    </div>',
            '</div>'
        ].join('');
    op.html = html;
    op.action = op.action || "BatchAddColumn";

    return NewPopUp(op);
}

/*快速修改*/
var ModificationPopUp = function (options) {
    var src = options.src;
    if (!src) {
        if (MARK) {
            src = MARK + "Modification.aspx";
        }
    }
    var def = {
        title: '资料快速修改',
        width: 700,
        height: 580,
        show: function () {
            var me = this;
            me.pop.css({ "position": "absolute" });
            me.pop.show();
        }
    }
    var op = jQuery.extend(def, options);
    var iframes = {
        src: src,
        height: 530,
        width: 680,
        cssName: "no_css",
        updateCallback: function (_data) {
            var me = this;
            var op = me.options;
            var tr = $(me._src).parents("tr");
            if (_data) {
                tr.find("[class*='update_']").each(function () {
                    var me = $(this);
                    var classs = me.attr("class").trim().split(' ');
                    for (var i = 0; i < classs.length; i++) {
                        if (classs[i].indexOf("update_") > -1) {
                            var class_ = classs[i].trim().replace("update_", "");
                            if (class_ in _data.data) {
                                var value = _data.data[class_];
                                var input = me[0];
                                if ("|INPUT|SELECT|".indexOf(input.tagName) == -1) {
                                    if (input.tagName == "A") {
                                        me.attr("title", value).text(value);
                                    }
                                    input = me.find("input,select");
                                }
                                $(input).val(value);
                            }
                        }
                    }
                })
            }
            me.hide();
        }
    };
    iframes = jQuery.extend(iframes, op.iframes || {});
    if (op.oid) {
        iframes.src = iframes.src + (iframes.src.indexOf('?') > 0 ? "&" : "?") + "oid=" + op.oid;
    }
    if (!iframes.src) {
        alert("ModificationPopUp：-》请设置iframes.src");
        return {};
    }
    return NewPopUp(op, iframes);
}
var addTag = function (options) {
    var def = {
        title: '添加标签',
        width: 500,
        height: 197,
        temp: [
            '<li onclick="SetContentToInput(this)" title="{0}">{0}',
            '    <em class="mark_dele" onclick="delContent(this)"></em><i></i>',
            '    <input type="hidden" class="hdExtendTag" />',
            '</li>'
        ].join(''),
        callback: function () {
            var me = this;
            me.pop.input = me.pop.find("input.com_input");
            if (me.options.input) {
                me.pop._input = $(me.options.input);
            } else {
                me.pop._input = $();
                jQuery.onerror("请设置输入的ID");
            }
        },
        btn1_Click: function () {
            var me = this;
            var value = me.pop.input.val().trim();
            if (value == "") {
                _alert("请输入标签！");
                return;
            }
            if (me.pop._input.find("[title='" + value + "']").length) {
                _alert("已存在相同的标签！");
            } else {
                value = value.replace(/,/ig, "，").replace(/>/ig, "");
                me.pop._input.append(me.options.temp.replace(/\{0\}/ig, value));
                me.hide();
                me.pop.input.val("");
            }
        }
    }
    var op = jQuery.extend({}, def, options);
    op.html = [
        '<div class="f_cb pad020 pro_fast_edit pro_mark_edit">',
	    '    <ul class="pro_fast pro_mark f_cb">',
		'        <li class="f_cb f_mt10">',
		'	        <label class="p_name padt5 f_fl">标签录入</label>',
		'	        <div class="seo_r f_fl">',
		'		        <div class="f_cb add_sv">',
		'			        <span class="clear_bd f_fl p_inp1 p_inp2"><input class="com_input clear_word" type="text" maxlength="100"><i class="clear_x"></i>',
		'			        </span>',
		'		        </div>',
		'	        </div>',
		'        </li>',
	    '    </ul>',
	    '    <div class="f_cb f_mb35">',
		'        <div class="seo_r f_fr">',
		'	        <div class="view_btn "><span class="e_btn1 f_csp "><i class="save_icon"></i>保 存</span><span class="e_btn2 f_ml35 f_csp "><i class="cancel_icon"></i>取 消</span></div>',
		'        </div>',
	    '    </div>',
        '</div>'
    ].join('');
    return NewPopUp(op);
}

var ExtensionClassification = function (options) {
    var def = {
        title: '批量添加扩展分类',
        width: 405,
        height: 170,
        callback: function () {
            var me = this;
            var op = me.op;
            var select = $("select.pass_faq[name='cid']:first");
            me.pop.find("span.s_selt").append(select.clone());
            me.pop.find("select").attr("style", "");
        },
        btn1_Click: function () {
            var me = this;
            loadJs("js/other/ajax.js", function () {
                var checkeds = $("#nsw_list_table tr input[name='chkItem']:checked");
                if (checkeds.length) {
                    var ids = '';
                    checkeds.each(function () {
                        ids += $(this).val() + ",";
                    })
                    ids = ids.trim(',');
                    me.pop.find("input.ids").val(ids);
                    var _data = me.pop.find("form").serialize();
                    _PostAjax(op.action, _data, function (data) {
                        data = _Json(data);
                        if (data.error) {
                            _alert(data.msg, true);
                            me.hide();
                        } else {
                            _alert(data.msg);
                        }
                    });
                } else {
                    _alert("请先选择需要" + me.options.title + "的单篇！");
                }
            });
        },
        show: function () {
            var me = this;
            var checkeds = $("#nsw_list_table tr input[name='chkItem']:checked");
            if (checkeds.length) {
                me.pop.show();
            } else {
                _alert("请先选择需要" + me.options.title + "的单篇！");
            }
        }
    }
    var op = jQuery.extend({}, def, options);
    op.html = [
        '<div class="f_cb pad1520 extend_sort" style="padding:5px 0px 27px 66px;">',
		'	<div class="f_cb f_mt15"><form>',
		'		<span class="f_fl f_lht27 f_mr10">目标产品分类：</span>',
        '       <input type="hidden" name="typename" value="' + op.type + '"/>',
        '       <input type="hidden" name="ids" class="ids" value=""/>',
		'		<div class="f_cb add_sv">',
        '           <span class="clear_bd s_selt f_fl px215 f_mr15">',
        '               <i class="revise_sub"></i>',
        '           </span>',
		'		</div>',
		'	</form></div>',
		'	<div class="f_cb f_mt15">',
		'		<div class="view_btn"><span class="e_btn1 f_csp "><i class="save_icon"></i>确 定</span>',
        '        <span class="e_btn2 f_ml35 f_csp "><i class="cancel_icon"></i>取 消</span></div>',
		'	</div>',
		'</div>'
    ].join('');
    return NewPopUp(op);
}

/*新建属性*/
var ProductAttributeColumn = function (options) {
    var src = options.src;
    if (!src) {
        if (!MARK) {
            return;
        }
        src = MARK + "AttributeColumn.aspx";
    }
    var def = {
        title: '新建属性',
        width: 630,
        height: 515
    };
    var type = {
        "textbox": "文本框",
        "textarea": "多行文本框",
        "radiobuttonlist": "单选框",
        "checkboxlist": "多选框",
        "dropdownlist": "下拉框"
    };
    var op = jQuery.extend(def, options);
    if (!op.temp && op.temp_id) {
        op.temp = $(op.temp_id).html();
        op.temp = op.temp.replace('_attrid_', 'attrid');
        op.temp_ = $(op.temp_id).find("div.nsw_parent_tr").html();
        $(op.temp_id).find("input").attr("name", null);
    }
    op.updatelist = $(op.updatelist || "#nsw_list_table");
    var iframes = {
        src: src + (op.oid ? "?oid=" + op.oid : ""),
        height: 410,
        width: 610,
        cssName: "pad300",
        updateCallback: function (_data) {
            var me = this;
            var op = me.options;
            if (_data) {
                var data = _data["data"];
                var update = _data["update"];
                data.TxtValues = data.TxtValues || '';
                if (update == "False") {
                    var temp = op.temp;
                    temp = _allReplace(temp, "{title}", data.Title);
                    temp = _allReplace(temp, "{type}", type[data.ControlType.toLowerCase()]);
                    temp = _allReplace(temp, "{values}", _allReplace(data.TxtValues, Separate, ',').trim(','));
                    temp = _allReplace(temp, "{oid}", data.ID);
                    op.updatelist.append(temp);
                } else {
                    var src = me._src;
                    if (src) {
                        src = $(src).parents("div.nsw_parent_tr");
                        var temp = op.temp_;
                        temp = _allReplace(temp, "{title}", data.Title);
                        temp = _allReplace(temp, "{type}", type[data.ControlType.toLowerCase()]);
                        temp = _allReplace(temp, "{values}", _allReplace(data.TxtValues, Separate, ',').trim(','));
                        temp = _allReplace(temp, "{oid}", data.ID);
                        $(src).html(temp);
                    }
                }
            }
            me.hide();
        }
    };
    iframes = jQuery.extend(iframes, op.iframes || {});
    return NewPopUp(op, iframes);
}

var ImportedProductColumnAttribute = function (options) {
    var src = options.src;
    if (!src) {
        if (!MARK) {
            return;
        }
        src = "Imported" + MARK + "ColumnAttribute.aspx";
    }
    var def = {
        title: '属性导入',
        width: 600,
        height: 450
    };
    var type = {
        "textbox": "文本框",
        "textarea": "多行文本框",
        "radiobuttonlist": "单选框",
        "checkboxlist": "多选框",
        "dropdownlist": "下拉框"
    };
    var op = jQuery.extend(def, options);
    if (!op.temp && op.temp_id) {
        op.temp = $(op.temp_id).html();
        op.temp = op.temp.replace('_attrid_', 'attrid');
        op.temp_ = $(op.temp_id).find("div.nsw_parent_tr").html();
        $(op.temp_id).find("input").attr("name", null);
    }
    op.updatelist = $(op.updatelist || "#nsw_list_table");
    var iframes = {
        src: src,
        height: 380,
        width: 600,
        cssName: "pad100",
        updateCallback: function (_data) {
            var me = this;
            var op = me.options;
            if (_data) {
                var datas = _data["data"];
                var update = _data["update"];
                if (update == "True") {
                    op.updatelist.empty();
                }
                for (var i in datas) {
                    var data = datas[i];
                    if (!op.updatelist.find("input[value='" + data.ID + "']").length) {
                        var temp = op.temp;
                        temp = _allReplace(temp, "{title}", data.Title);
                        temp = _allReplace(temp, "{type}", type[data.ControlType.toLowerCase()]);
                        temp = _allReplace(temp, "{values}", _allReplace(data.TxtValues, Separate, ',').trim(','));
                        temp = _allReplace(temp, "{oid}", data.ID);
                        op.updatelist.append(temp);
                    }
                }
            }
            me.hide();
        },
        callback: function () {
            var me = this;
            $(me.iframe.document.body).find("#cpmc").html($(me.options.column).val() || "尚未保存分类");
        }
    };
    iframes = jQuery.extend(iframes, op.iframes || {});
    return NewPopUp(op, iframes);
}

var GalleryWindow = function (options, iframes) {
    var def = {
        title: '图片提取库',
        width: 740,
        height: 608
    };
    var op = jQuery.extend(def, options);
    var _iframes = jQuery.extend({
        src: "GalleryWindow.aspx",
        height: 515,
        width: 700,
        cssName: "pad20"
    }, iframes);
    return NewPopUp(op, _iframes);
}

var FilesWindow = function (options, iframes) {
    var def = {
        title: '附件库',
        width: 740,
        height: 608
    };
    var op = jQuery.extend(def, options);
    var _iframes = jQuery.extend({
        src: "FilesWindow.aspx",
        height: 515,
        width: 700,
        cssName: "pad20"
    }, iframes);
    return NewPopUp(op, _iframes);
}

var QuicklyAddColumn = function (options) {
    var def = {
        title: '快速添加分类',
        width: 600,
        height: 303,
        action: "BatchAddColumn",
        callback: function () {
            var me = this;
            var op = me.op;
            var dll = $("#ddlColumnsSource");
            me.pop.find("select.pass_faq").append(dll.children().clone());
        },
        btn1_Click: function () {
            var me = this;
            loadJs("js/other/ajax.js", function () {
                var input = me.pop.find("input.com_input");
                if (input.val() == "") {
                    _alert("请输入分类名称！");
                    return;
                }
                var _data = me.pop.find("form").serialize();
                var type = me.pop.type || MARK;
                if (!type) {
                    alert("请设置类型！");
                    return;
                }
                _data += "&typename=" + type + "column&cid=" + me.pop.find("select.pass_faq").val();
                _PostAjax(op.action, _data, function (data) {
                    data = _Json(data);
                    if (data.error) {
                        _alert(data.msg, true, function () {
                            $("#btnColumn").click();
                            me.hide();
                        }, 500);
                    } else {
                        _alert(data.msg);
                    }
                });
            });
        }
    }
    var op = jQuery.extend({}, def, options);
    op.html = [
        '<div class="f_cb pad020 pro_fast_edit pro_mark_edit"><form>',
            '<ul class="pro_fast pro_mark f_cb">',
		    '	<li class="f_cb f_mt10">',
		    '		<label class="p_name padt5 f_fl">分类名称</label>',
		    '		<div class="seo_r f_fl">',
		    '			<div class="f_cb add_sv">',
		    '				<span class="clear_bd f_fl p_inp1 p_inp3"><input class="com_input clear_word" name="columns" type="text" maxlength="100"><i class="clear_x"></i>',
		    '				</span><em class="inp_tips_gray pd010 f_lht27 f_fl">64字符</em>',
		    '			</div>',
		    '		</div>',
		    '	</li>',
		    '	<li class="f_cb">',
		    '		<label class="p_name padt5 f_fl">上级分类</label>',
		    '		<div class="seo_r f_fl">',
		    '			<div class="f_cb">',
		    '				<span class="clear_bd f_fl dlg_selt">',
		    '					<select class="pass_faq"></select>',
		    '					<i class="revise_sub"></i>',
		    '				</span>',
		    '				</span><em class="inp_tips_gray pd010 f_lht27 f_fl">分类可分多级</em>',
		    '			</div>',
		    '		</div>',
		    '	</li>',
		    '	<li class="f_cb"><i class="tips1_ico f_fl f_mr15"></i><span class="inp_tips_gray f_lht27 f_fl">设置更多分类信息，请到分类列表进行详细设置，如SEO、内页排版</span></li>',
		    '</ul></form>',
		    '<div class="f_cb f_mb35">',
		    '	<div class="view_btn f_tac"><span class="e_btn1 f_csp "><i class="save_icon"></i>保 存</span>',
            '    <span class="e_btn2 f_ml35 f_csp "><i class="cancel_icon"></i>取 消</span>',
            '</div>',
        '<div>'
    ].join('');
    return NewPopUp(op);
}


var GetTalk99Pop = function (options) {
    var def = {
        title: '获取Talk99链接',
        width: 400,
        height: 200
    }
    var op = jQuery.extend({}, def, options);
    op.html = [
        '<div class="f_cb pad020 pro_fast_edit pro_mark_edit">',
            '<textarea rows="2" cols="20" class="text_area tx1" style="border:1px solid #dcdcdc;margin-top:18px;height:110px;"></textarea>',
        '<div>'
    ].join('');
    var me = NewPopUp(op);
    me.setValue = function (value) {
        me.pop.show().find(".text_area").val(value).focus().select();
    }
    return me;
}

var SearchTemplates = function (options, iframes) {
    var def = {
        title: '模版选择',
        width: 740,
        height: 510
    }
    var op = jQuery.extend(def, options);
    var _iframes = jQuery.extend({
        src: "SearchTemplates.aspx",
        width: 700,
        height: 415,
        cssName: "pad20"
    }, iframes);
    if (op.parameter) {
        _iframes.src += "?" + op.parameter;
    }
//     else if(MARK){
//        _iframes.src += "?" + op.parameter;
//    }
    return NewPopUp(op, _iframes);
}

/*邮件群发*/
var ECEmail2 = function (options) {
    var def = {
        title: '邮件群发',
        width: 700,
        height: 815,
        show: function () {
            var me = this;
            me.pop.css({ "position": "absolute","margin-top":"0","top":'0'});
            me.pop.show();
        }
    }
    var op = jQuery.extend(def, options);
    var iframes = {
        src: "EC_Email2.aspx",
        height: 742,
        width: 680,
        cssName: "pt20"
    };
    iframes = jQuery.extend(iframes, op.iframes || {});
    return NewPopUp(op, iframes);
}

/*水印设置*/
var Watermark = function (options) {
    var def = {
        title: '水印设置',
        width: 740,
        height: 585
    }
    var op = jQuery.extend(def, options);
    var iframes = {
        src: "Config_Watermark.aspx",
        height: 540,
        width: 740,
        cssName: "pt00"
    };
    iframes = jQuery.extend(iframes, op.iframes || {});
    return NewPopUp(op, iframes);
}