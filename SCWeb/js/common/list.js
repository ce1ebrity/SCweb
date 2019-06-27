if (typeof (window.loadjs_obj) == "undefined") {
    window.loadjs_obj = {};
}
window.loadjs_obj["/list.js"] = true;

var Myself = location.pathname;
//获取选中数量
var SltedItemCnt = function () {
    var cbs = $("#nsw_list_table td.td1 input:checked");
    return cbs.length;
}
//操作提示
var BatchOperation = function (msg) {
    var length = SltedItemCnt();
    msg = msg || '确定删除选中项（共{count}项）？';
    if (!length) {
        _alert('未有选中项');
        return false;
    }

    if (confirm(msg.replace('{count}', length))) {
        return true;
    }
    return false;
}



$(function () {
    var nsw_list_table = $("#nsw_list_table"), //列表
    nsw_tools_bar = $("div.nsw_tools_bar"), //操作栏
    span$sele_all = nsw_tools_bar.find("span.sele_all"), //全选
    span$sele_inverse = nsw_tools_bar.find("span.sele_inverse"), //反选
    span$span_refresh = nsw_tools_bar.find("span.span_refresh"), //刷新
    span$ck_box = nsw_list_table.find("span.ck_box"); //全部的input
    /*$("span.sele_all").unbind("click").click(function () {
    var me = $(this);
    var input = me.find("input");
    if (input.attr("checked")) {
    input.attr("checked", false);
    me.find(".e_word").removeClass("z_select");
    } else {
    input.attr("checked", true);
    me.find(".e_word").addClass("z_select");
    }
    if (input.attr("checked")) {
    $("#nsw_list_table tr span.ck_box").addClass("z_select").find("input").attr("checked", true);
    } else {
    $("#nsw_list_table tr span.ck_box").removeClass("z_select").find("input").attr("checked", false);
    }
    alert("123");
    })*/
    //全选
    //span$sele_all.unbind("click").click(function () {
      
    //    var me = $(this);
    //    var input = me.find("input");
    //    if (input.attr("checked")) {
    //        input.attr("checked", false);
    //        me.find(".e_word").removeClass("z_select");
    //    } else {
    //        input.attr("checked", true);
    //        me.find(".e_word").addClass("z_select");
    //    }
    //    if (input.attr("checked")) {
    //        nsw_list_table.find("span.ck_box").find("input").attr("checked", true).trigger("ev_click");
    //    } else {
    //        nsw_list_table.find("span.ck_box").find("input").attr("checked", false).trigger("ev_click");
    //    }
    //})
    //反选
    //span$sele_inverse.unbind("click").click(function () {
    //    var sele_all = $("span.sele_all");
    //    sele_all.find("input").attr("checked", false).end().find(".e_word").removeClass("z_select");
    //    span$ck_box.click();
    //})
    //刷新
    span$span_refresh.click(function () {
        var ps = parse_url((location.href + ''));
        if (ps.oid) {
            Myself += "?oid=" + ps.oid;
        }
        location.href = Myself;
    })

    //表格行鼠标时间
 //$("#nsw_list_table.nsw_pro_list .t_tr,#nsw_list_table tr").mouseover(function () {
 //   $(this).addClass("t_tr_on");
 //        $(this).find(".e_edi1").show();
 //   });
 //   $("#nsw_list_table.nsw_pro_list .t_tr,#nsw_list_table tr").mouseout(function () {
       
 //       $(this).removeClass("t_tr_on");
 //       $(this).find(".e_edi1").hide();
 //   }).click(function (event) {//行点击 
 //       var _me = $(this), _check = _me.find("td:first span.ck_box");
 //       var classname = "  " + $(event.target).attr("class") + "  ";
 //       if (event.target.TagName === "A" || classname.indexOf(" e_lowpoint ") > 0 || classname.indexOf(" editor_warp ") > 0 || classname.indexOf(" popUp ") > 0) {
 //           return;
 //       }
 //       if (_check.hasClass("z_select")) {
 //           _check.removeClass("z_select").find("input").attr("checked", false).trigger("ev_click");
 //       } else {
 //           _check.addClass("z_select").find("input").attr("checked", true).trigger("ev_click");
 //       }
 //   });
  
    $(".fancyTable").find("tbody").delegate('tr', 'mouseover', function () {
        $(this).addClass("t_tr_on");
        $(this).find(".e_edi1").show();
    });
    $(".fancyTable").find("tbody").delegate('tr', 'mouseout', function () {
        $(this).removeClass("t_tr_on");
        $(this).find(".e_edi1").hide();
    });
    ////表格行鼠标时间
    ////$(".fancyTable tbody tr").mouseover(
    //function fancyTableover (obj) {
    //    $(obj).addClass("t_tr_on");
    //    $(obj).find(".e_edi1").show();
    //};
    ////$(".fancyTable tbody tr").mouseout(
    //function fancyTableout(obj) {
    //    $(obj).removeClass("t_tr_on");
    //    $(obj).find(".e_edi1").hide();
    //};

    
    //其他操作鼠标时间
    $(".batch_act").hover(function () {
        $(this).find(".c_act").addClass("z_select");
        $(this).find(".batch_dropup").stop(true, true).slideDown("fast");
    }, function () {
        $(this).find(".c_act").removeClass("z_select");
        $(this).find(".batch_dropup").stop(true, true).slideUp("fast");
    })
})
//搜索用
var SetValueToDiv = function (_src, parames) {
    var src = $(_src);
    var obj = {};
    src.find("select[name],textarea[name],input[name][type='hidden'],input[name][type='text']").each(function () {
        var me = $(this);
        var name = me.attr("name");
        var value = parames[name];
        if (value) {
            me.focus().val(value).blur();
        }
        obj[name] = true;
    })
    var _obj = {};
    for (var name in parames) {
        if (obj[name] === true) {

        } else {
            _obj[name] = parames[name];
        }
    }

    for (var name in _obj) {
        var value = _obj[name];
        if (value) {
            var arr = value.split(',');
            for (var v in arr) {
                v = arr[v];
                if (v != undefined && v != null && v != "") {
                    src.find("span.e_radio input[name='" + name + "'],span.ck_box input[name='" + name + "']").each(function () {
                        $(this).attr("checked", false).parents("span.e_radio,span.ck_box").removeClass("z_select");
                    }).filter("input[value='" + v + "']").parents("span.e_radio,span.ck_box").click();
                }
            }
        }
    }
    var fields = src.find("#ddlFields");
    var kwd = src.find("input[name='kwd']");
    if (fields.length) {
        //alert(parames["field"]);
        fields.val(parames["field"] || "");
        kwd.val(parames["kwd"]);
    }
    if (kwd.length) {
        var defValue = kwd.val();
        kwd.val("").focus().val(defValue).blur();
    }
}
var SerializeDiv = function (_src) {
    var src = $(_src);
    var url = '';
    var obj = {};
    src.find("select[name],textarea[name],input[name][type='hidden'],input[name][type='text'],input[name][type='radio']:checked").each(function () {
        var me = $(this);
        var value = me.val();
        if (value) {
            obj[me.attr("name")] = encodeURIComponent(value);
        }
    }).end().find("input[name][type='checkbox']:checked").each(function () {
        var me = $(this);
        var name = me.attr("name");
        var value = me.val();
        if (value != "") {
            value = encodeURIComponent(value);
            if (name in obj) {
                obj[name] += "," + value;
            } else {
                obj[name] = value;
            }
        }
    });
    if (obj.ddlFields) {
        obj.field = obj.ddlFields;
        delete obj.ddlFields;
    }
    var fields = src.find("#ddlFields,[name='ddlFields']");
    if (fields.length) {
        obj["field"] = encodeURIComponent(fields.val());
        if (!obj["kwd"]) {
            delete obj.field;
        }
    }
    var value = "";
    for (var name in obj) {
        value = obj[name];
        if (value) {
            url += "&" + name + "=" + value.trim(',');
        }
    }
    var oid = parse_url((location.href + '').replace(location.hash + '', '').toLowerCase(), "oid");
    if (oid) {
        url += "&OID=" + oid;
    }
    //alert(url);
    return url + "#div_" + $("div.form_search").index(src);
}


$(function () {
    //将查询的值填充
    var parames = parse_url((location.href + '').replace(location.hash + '', ''));
    var hash = (window.location.hash + "").replace("#", "");
    if (!jQuery.isEmptyObject(parames)) {
        var hash = (window.location.hash + "").replace("#div_", "");
        if (hash) {
            SetValueToDiv($("div.form_search:eq(" + hash + ")").show(), parames);
        } else {
            $("div.form_search").each(function () {
                SetValueToDiv(this, parames);
            });
        }
        //高级表单显示
        //$("div.advan_posi").show();
    }
    var ajaxSave = $("span.ajaxSave");
    if (ajaxSave.length) {
        loadJs("js/other/ajax.js", function () {
            ajaxSave.bind("click", function () {
                var me = $(this);
                var data = me.attr("data-src");
                data = data ? _Json(data) : null;
                if (data) {
                    if (ValidateForm(me)) {
                        _PostAjax(data.action, me.parent("form").serialize(), function (msg) {
                            var data = _Json(msg);
                            if (data.error) {
                                _alert(data.msg, true, function () {
                                    window.location = window.location;
                                });
                            } else {
                                _alert(data.msg);
                            }
                        })
                    }
                }
            })
        })
    }
    //精华置顶
    var radios = $("#nsw_list_table div.set_radios[typename] > span.radio,#nsw_list_table div.hd_input[typename] > span.p_show");
    if (radios.length > 0) {
        loadJs("js/other/ajax.js", function () {
            radios.unbind("click").bind("click", function () {
                var me = $(this);
                var div = me.parent();
                var data = { OID: div.attr("oid"), fileColumn: div.attr("filecolumn") || '', typename: div.attr("typename") };
                var cur = me.attr("cur") || "z_select";
                data.enable = me.hasClass(cur) ? 0 : 1;
                _PostAjax("ajaxbit", data, function (msg) {
                    var data = _Json(msg);
                    if (data.error) {
                        _alert("更新成功！", true);
                        if (me.hasClass(cur)) {
                            me.removeClass(cur);
                            me.find("input").attr("checked", false);
                        } else {
                            me.addClass(cur);
                            me.find("input").attr("checked", true);
                        }
                    } else {
                        _alert(data.msg);
                    }
                })
            })
        })
    }

    var noItemYet = $("#noItemYet");
    if (noItemYet.length) {
        noItemYet.find("td").attr("colspan", $("#tabHeader").find("th").length || 12);
    } else {
        var tabHeader = $("#tabHeader");
        tabHeader = tabHeader.next();
        if (tabHeader.hasClass("dn") && tabHeader.next().length == 0) {
            tabHeader.after('<tr id="noItemYet"><td class="noItemYet" style=" text-align:center;" colspan="11">没有任何项目</td></tr>');
            tabHeader.next().attr("colspan", tabHeader.find("th").length || 12);
            $("div.nsw_pagination").hide();
        }
    }

    $("div.search_so input[name='kwd']").bind("keydown", function (event) {
        var e = event || window.event || null;
        if (e && e.keyCode == 13) {
            $(this).next().click();
        }
    });

    if ((window.location + '').toLowerCase().indexOf('_column.aspx') > 0) {
        var td = $("td.td1:first input[name='chkItem'][value='1']").parents("td");
        if (!td.hasClass("no_hide")) {
            td.html("");
        }
    }

 
    //把根栏目下的信息给清空掉
    $(".f_noinfo").empty();

    //表单添加回车事件
    $(".form_search").keydown(function (_e) {
        if (_e.keyCode == 13) { $(this).find("[onclick='SearchObjectByGet(this)']").trigger("click"); }
    });
    //高级点击后的显示隐藏
    $(".advan_btn").click(function () {
        $(".advan_posi").toggle();
    });
});
function onImgErrors(src) {
    src.src = '/js/grey.gif';
}

var PageTitle = "";
function SubclassOp(me, isShow, trs) {
    me = $(me);
    var oid = me.attr("oid");
    var _trs = me.data("trs") || trs.filter(".parentid_" + oid);
    if (!me.data("trs")) {
        me.data("trs", _trs);
    }
    if (_trs.length) {
        if (isShow) {
            _trs.fadeIn()
        } else {
            _trs.fadeOut();
        }
        _trs.each(function () {
            SubclassOp(this, isShow, trs);
        })
    }
}
function EditMobile(e, oid) {
    var url = edit(e, oid, null, null, true);
    if (url) {
        edit(e, null, "Mobile" + url);
    }
}

//设置手机二维码
$(function () {
    $("<div class='code_img'></div>").appendTo("p.scl_d");
    $("<div class='con_header'><span class='close_btn' onclick='hideQR(this);'></span><p>分享到微信朋友圈</p></div><div class='con_img'><img src=''></div><div class='con_foot'><p>打开微信，点击底部的'发现',使用'扫一扫' <br/>即可将网页分享到我的朋友圈。 </p></div>").appendTo(".code_img");
    $(".code_img").css({ "position": "absolute", "top": "15px", "right": "0", "z-index": "99", "width": "303px", "height": "303px", "display": "none", "background-image": "url(/Manager/Mobile/Img/ailog_bg_03.gif)", "overflow": "hidden" });
    $(".con_header").css({ "text-align": "left", "height": "47px", "line-height": "45px", "text-indent": "1em" });
    $(".con_header span").css({ "display": "block", "float": "right", "width": "30px", "height": "30px", "margin": "8px 5px 0 0", "cursor": "pointer", "background-image": "url(/Manager/Mobile/Img/ailog_close.gif)", "background-position": "center", "background-repeat": "no-repeat" });
    $(".con_img").css({ "height": "201px", "text-align": "center" });
    $(".con_foot").css({ "text-align": "left", "height": "40px", "line-height": "22px", "padding": "6px 0 0 10px" });
});
function hideQR(src) { $(src).parents('div.code_img').hide(); }
function qrcode(src) {
    $(src).attr("src", $(src).attr("src") + "&time" + Math.random());
}
function Qrcode(src, url) {
    var h = document.body.clientHeight || 559;
    var _h = 303;

    $("p.scl_d").css("zIndex", 0);
    $("div.code_img").hide();
    var me = $(src);
    var td = me.parents("td");
    var _imgHtml = "<img width='201' height='201' onerror='qrcode(this);' src='http://nswcode.nsw88.com/?w=200&h=200&qc=8dd84307afe274a5&t=1212&url=" + url + "' />";
    $("div.con_img").html(_imgHtml);
    if (me.offset().top + _h > h) {
        td.find("div.code_img").css("top", "auto").css("bottom", "0");
    }
    if (td.find("div.code_img").css("display") == "none" || td.find(".code_img").css("display") == undefined) {
        td.children(".scl_d").css("zIndex", 10).find(".code_img").addClass("show").show("fast");
    }
    return false;
}
$(function () {
    PageTitle = $("span.nsw_add a:last");
    if (PageTitle.length > 0) {
        PageTitle = PageTitle.text().trim();
    } else {
        PageTitle = "";
    }

    var dates = $("input.calendar_tx");
    if (dates.length) {
        Timeout(function () {
            loadJs("/Component/My97DatePicker/WdatePicker.js", function () {
                if (typeof (WdatePicker) != "undefined") {
                    dates.attr("readonly", "true").bind("focus", function () {
                        WdatePicker({ el: $(this).attr("id"), dateFmt: $(this).attr("dateFmt") || 'yyyy-MM-dd', onpicked: function () { $(this).trigger("hasValue"); } });
                    });
                }
            })
        });
    }

    var imgs = $("td.pimgsv img");
    if (imgs.length) {
        pictureEnlarge(imgs);
    }

    var table = $("table.grouping");
    if (table.length) {
        var isHide = table.hasClass("gh");
        var trs = table.find("tr").each(function () {
            var me = $(this);
            me.addClass("parentid_" + me.attr("parentid"));
        })
        trs.filter(".parentid_1").each(function () {
            var tr = $(this);
            var td = tr.children("td.rname");
            if (!td.length) {
                return false;
            }
            if (td.find("span.short_tit").length) {
                td.find("span.short_tit").append("<i class='contraction popUp'></i>");
            } else {
                td.append("<i class='contraction popUp'></i>");
            }
            var contraction = td.find("i.contraction");
            contraction.bind("click", function () {
                var oid = tr.attr("oid");
                var _trs = contraction.data("trs") || trs.filter(".parentid_" + oid);
                if (!contraction.data("trs")) {
                    contraction.data("trs", _trs);
                }
                if (_trs.length) {
                    var isShow = contraction.hasClass("cur");
                    isShow ? _trs.fadeIn() & contraction.removeClass("cur") : _trs.fadeOut() & contraction.addClass("cur");
                    _trs.each(function () {
                        SubclassOp(this, isShow, trs);
                    })
                }
            })
            if (isHide) {
                setTimeout(function () {
                    contraction.click();
                }, 300);
            }
        })
    }
})