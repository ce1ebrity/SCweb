//选中
function XZ_select(obj) {
    if ($(obj).hasClass("z_select")) {
        $(obj).removeClass("z_select");
        $(obj).find("input").attr("checked", false).trigger("ev_click");
    }
    else {
        $(obj).addClass("z_select");
        $(obj).find("input").attr("checked", true).trigger("ev_click");
    }
}
//加载显示页数
function loadOption(Cpage, page) {
    var ddlHtml = "";
    $("#pageOP").html("");//清空DDL页数
    for (var i = 0; i < Cpage; i++) {
        if (i == page - 1) {
            ddlHtml += "<option selected='true' value='" + (i + 1) + "'>" + (i + 1) + "</option>";
        } else {
            ddlHtml += "<option value='" + (i + 1) + "'>" + (i + 1) + "</option>";
        }
    }
    $("#pageOP").append(ddlHtml);
}

//绑定波段下拉框
function LoadSCJD02(obj) {
    $.get("/GoodsInfo/GetDDLSCJD02", { DDLjidu: obj }, function (data) {

        var ddlBD = "";
        $("#xuanz_ul_boduan").html("");//清空DDL
        var dt = eval("(" + data + ")");
        ddlBD += "<li><a values='0'>请选择</a></li>";
        for (var i = 0; i < dt.length; i++) {
            ddlBD += "<li><a values=" + dt[i]["SXMC"] + ">" + dt[i]["SXMC"] + "</a></li>";
        }
        $("#xuanz_ul_boduan").append(ddlBD);
    })
}

//调用flie
function Getfile() {
    $("#filed").click();
}
//上传EXCEL
function importExcel() {
    var fName = $("#filed").attr("values");
    var file = $("#filed").val();
    if (file == null || file.length == 0) {
        layer.alert("请先选择上传文件！");
        return false;
    }
    $('#file-form').ajaxSubmit({
        type: 'POST', // HTTP请求方式
        url: '../Ajax/Handler1.ashx', // 请求的URL地址
        dataType: 'text', // 服务器返回数据转换成的类型
        async: false,
        data: {
            fName: fName
        },
        beforeSend: function () {
            layer.msg('正在导入......', {
                icon: 16,
                shade: 0.01
            });
        },
        success: function (data) {
            if (data == "Operation") {
                layer.alert("您没有权限请和管理员联系");
            }
            else {
                layer.closeAll();
                layer.open({
                    title: '消息',
                    content: data,
                    btn: ['确定'],
                    yes: function (index, layero) {
                        window.location.reload();
                    }
                });
            }
        },
        error: function (data) {
            layer.alert("出错了，请联系信息部！");
        }
    });
}
//编辑
function jEdit(id) {
    location.href = "/GoodsInfo/GoodsUpdate/" + id;

}
//模糊查询
//模糊查询
function SearchObjectByGet(page, OnePageC, daima_id) {
    layer.msg('加载中......', {
        icon: 16,
        shade: 0.01
    });
    $("#pageNow").html(1);
    $.ajax({
        url: "/GoodsInfo/SelSCJDBInfo",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        data:
        {
            page: page,
            OnePageC: OnePageC,
            DDLbo: $("#boduan").find("span").attr("values"),
            DDLpp: $("#pinpai").find("span").attr("values"),
            DDLmode: $("#jiagong").find("span").attr("values"),
            QXK: $("#select1").val(),

            DDLjd: $("#jidu").find("span").attr("values"),//添加季度
            txtFactory: "",//给工厂筛选默认值
            txtGD: "",//给跟单员默认值
            txtMlcg: "",//给面料采购员筛选默认值
            txtFlcg: "",//给辅料采购员筛选默认值
            txtPG: "",//添加辅料采购员筛选

            kh_id: daima_id,//款号id

            txtStyle: $("#txtStyle").val(),
            txtGirard: $("#txtGirard").val(),
            txtColor: $("#txtColor").val(),
            txtTime: $("#txtTime").val(),
            year: $("#txty").val()
        },
        async: false,
        success: function (data) {
            if (data == "error") {
                layer.alert("出错了，请联系管理员!");
            }
            else {
                $("#pageNow").html(page);
                var dt = eval("(" + data + ")");
                var tableHtml = "";
                $("#sptbd").html("");
                var classchild = "";
                if (dt.length > 0) {
                    for (var i = 0; i < dt.length; i++) {
                        if (i == 0) {
                            classchild = "child_2";
                            tableHtml += "<tr class='" + classchild + "'>";
                            tableHtml += "    <td class='td1'><div class='nsw_check_box'><span class='ck_box' onclick='XZ_select(this)'><span class='dn'><input type='checkbox' name='chkItem' value='" + dt[i].id + "'></span></span></div></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl tdtop'><a href='##' class='update_Title'>" + dt[i].SCJD01 + "</a><i class='e_edi1 e_more_edit popUp' style='display: none;' onclick='jEdit(" + dt[i].id + ")'></i></span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].years + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD02 + "</span></td>";
                            tableHtml += "    <td class='pimgsv''><span class='poducts short_tit f_fl'>" + dt[i].SCJD03 + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD04 + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD06 + "</span></td>";
                            tableHtml += "    <td class='pimgsv' ><span class='poducts short_tit f_fl'>" + dt[i].SCJD05 + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD07 + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].GG1DM + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD08 + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD09 + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD10 + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD11 + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD12 + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD13 + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].QXK + "</span></td>";
                            tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + GetdateTimeL(dt[i].huoqi70day) + "</span></td>";
                            tableHtml += "    <td class='nsw_cnt_area action_tx'><a href='##' onclick='jEdit(" + dt[i].id + ")'><span class='pro_view pro_edit'><i></i>编辑</span></a></td>";

                            tableHtml += "</tr>";
                        }
                        else if (i > 0) {
                            if (dt[i - 1]["SCJD05"] == dt[i]["SCJD05"]) {
                                tableHtml += "<tr class='" + classchild + "'>";
                                tableHtml += "    <td class='td1'><div class='nsw_check_box'><span class='ck_box' onclick='XZ_select(this)'><span class='dn'><input type='checkbox' name='chkItem' value='" + dt[i].id + "'></span></span></div></td>";
                                tableHtml += "    <td class='pimgsv' ><span class='poducts short_tit f_fl tdtop'><a href='##' class='update_Title'>" + dt[i].SCJD01 + "</a><i class='e_edi1 e_more_edit popUp' style='display: none;' onclick='jEdit(" + dt[i].id + ")'></i></span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].years + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD02 + "</span></td>";
                                tableHtml += "    <td class='pimgsv' ><span class='poducts short_tit f_fl'>" + dt[i].SCJD03 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD04 + "</span></td>";
                                tableHtml += "    <td class='pimgsv' ><span class='poducts short_tit f_fl'>" + dt[i].SCJD06 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD05 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD07 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].GG1DM + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD08 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD09 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD10 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD11 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD12 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD13 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].QXK + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + GetdateTimeL(dt[i].huoqi70day) + "</span></td>";
                                tableHtml += "    <td class='nsw_cnt_area action_tx'><a href='##' onclick='jEdit(" + dt[i].id + ")'><span class='pro_view pro_edit'><i></i>编辑</span></a></td>";

                                tableHtml += "</tr>";
                            }
                            else {
                                if (classchild == "child_2") {
                                    classchild = "child_1";
                                } else {
                                    classchild = "child_2";
                                }
                                tableHtml += "<tr class='" + classchild + "'>";
                                tableHtml += "    <td class='td1'><div class='nsw_check_box'><span class='ck_box' onclick='XZ_select(this)'><span class='dn'><input type='checkbox' name='chkItem' value='" + dt[i].id + "'></span></span></div></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl tdtop'><a href='##' class='update_Title'>" + dt[i].SCJD01 + "</a><i class='e_edi1 e_more_edit popUp' style='display: none;' onclick='jEdit(" + dt[i].id + ")'></i></span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].years + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD02 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'  ><span class='poducts short_tit f_fl'>" + dt[i].SCJD03 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD04 + "</span></td>";
                                tableHtml += "    <td class='pimgsv' ><span class='poducts short_tit f_fl'>" + dt[i].SCJD06 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD05 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD07 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].GG1DM + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD08 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD09 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD10 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD11 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD12 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].SCJD13 + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].QXK + "</span></td>";
                                tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + GetdateTimeL(dt[i].huoqi70day) + "</span></td>";
                                tableHtml += "    <td class='nsw_cnt_area action_tx'><a href='##' onclick='jEdit(" + dt[i].id + ")'><span class='pro_view pro_edit'><i></i>编辑</span></a></td>";

                                tableHtml += "</tr>";
                            }
                        }
                    }

                    $("#Crows").html(dt[0]["RowsCount"]);
                    $("#Cpage").html(dt[0]["pageCount"]);
                }
                else {
                    $("#Crows").html(0);
                    $("#Cpage").html(0);
                }
                $("#sptbd").append(tableHtml);
                var CountPage = $("#Cpage").html();
                loadOption(CountPage, page);
                ShowTable("1");
                layer.closeAll();
            }
        },
        error: function () {
            layer.alert("出错了，请联系管理员!");
        }
    })

}

//兼容单选框JS
function getByName(Name) {
    var i = document.getElementsByName(Name)
    if (i > 0) {
        return i;
    } else {
        var aEle = document.getElementsByTagName('*');
        var arr = [];
        for (var i = 0; i < aEle.length; i++) {
            if (aEle[i].getAttribute("name") == Name) {
                arr.push(aEle[i])
            }
        }
        return arr;
    }
}
//var Box = getByName('pox');
//alert(Box.length);

function Editfactory(obj, text) {
    var id = $(obj).attr("id");
    var input = "";
    chkID = getByName("chkItem");
    ckval = "";
    for (i in chkID) {
        if (chkID[i].checked)
            ckval += chkID[i].value + ",";
    }
    if (ckval == "") {
        layer.alert("您尚未选择修改项");
    }
    else {
        if (text == 1) {
            input = "<input class='ssinp' type='text' id='txtGc' />";
        } else if (text == 2) {
            input = "<select class='frl_select' id='txtGc'><option value='1'> 是 </option><option value='0'> 否 </option></select>";
        }
        else if (text == 3) {
            input = "<select class='frl_select' id='txtGc'><option value='FOB'> FOB </option><option value='CMT'> CMT </option></select>";
        }
        layer.open({
            title: '批量修改',
            content: '请输入:' + input,
            btn: ['确定', '关闭'],
            yes: function (index, layero) {
                var Gc = $('#txtGc').val();
                if (Gc == "" || Gc == null) {
                    layer.alert("请填写批量修改内容");
                    return;
                }
                else {
                    $.get("/GoodsInfo/EditPDfactory", { Gc: Gc, id: id, ckval: ckval }, function (data) {
                        if (data == "OK") {
                            layer.open({
                                title: '消息',
                                content: '批量修改成功',
                                btn: ['确定'],
                                yes: function (index, layero) {
                                    SearchObjectByGet(1, 100);
                                    layer.closeAll();

                                    //批量修改成功后全面单选框依然保持选中状态
                                    ckval = ckval.substring(0, ckval.length - 1);//获取选中ID
                                    KeepChecked(ckval);
                                }
                            });
                        } else if (data == "Operation") {
                            layer.alert("您没有权限请和管理员联系");
                        }
                        else {
                            layer.alert("修改失败，请联系信息部");
                        }
                    })
                }

            }
        });
    }
}
//批量修改时间提示框
function Gxgjiaoq(obj) {
    var id = $(obj).attr("id");
    chkID = getByName("chkItem");
    ckval = "";
    for (i in chkID) {
        if (chkID[i].checked)
            ckval += chkID[i].value + ",";
    }
    if (ckval == "") {
        layer.alert("您尚未选择修改项");
    }
    else {
        layer.open({
            title: '批量修改时间',
            content: '输入时间:<input  class="Wdate wdate_pl" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd\',readOnly:true})" type="text" id="yyyo" />',
            btn: ['确定', '关闭'],
            yes: function (index, layero) {
                var data = $('#yyyo').val();
                if (data == "" || data == null) {
                    layer.alert("请选择批量修改时间");
                    return;
                } else {
                    $.get("/GoodsInfo/EditSCJDB", { data: data, id: id, ckval: ckval }, function (data) {
                        if (data == "OK") {
                            layer.open({
                                title: '消息',
                                content: '批量修改成功',
                                btn: ['确定'],
                                yes: function (index, layero) {
                                    SearchObjectByGet(1, 100);
                                    layer.closeAll();

                                    //批量修改成功后全面单选框依然保持选中状态
                                    ckval = ckval.substring(0, ckval.length - 1);//获取选中ID
                                    KeepChecked(ckval);
                                }
                            });
                        } else if (data == "Operation") {
                            layer.alert("您没有权限请和管理员联系");
                        }
                        else {
                            layer.alert("修改失败，请联系信息部");
                        }
                    })
                }

            }
        });
    }
}
//批量修改成功后全面单选框依然保持选中状态的方法
function KeepChecked(obj) {
    var str = obj.split(",");
    $(".fancyTable").find(".ck_box").each(function () {
        //console.log($(this).find("input").attr("value"));
        for (var i = 0; i < obj.length; i++) {
            if ($(this).find("input").attr("value") == str[i]) {
                $(this).find("input").attr("checked", true);
                $(this).addClass("z_select");
            }
        }
    });
}

//款号双击输入框弹出的窗口
$(function () {
    $("#txtGirard").dblclick(function () {
        $("#Girardbd").html("");
        $(".Reconciliation_ser_wai").show(100);
        $("#maskIframe").show(100);
    });

    $(" #Reconciliation_ser_wai2 .Reconciliation_ser").delegate("td", "click", function () {
        if ($(this).parent("tr").hasClass("ad_cls")) {
            $(this).parent("tr").removeClass("ad_cls");
        } else {
            $(this).parent("tr").addClass("ad_cls");
        }
    });

});

//款号查询
function SelByGirard() {
    var SCJD05 = $("#SCJD05s").val();
    var SCJD06 = $("#SCJD06s").val();
    $.ajax({
        url: "/GoodsInfo/SelSCJDBByGirard",
        type: "post",
        data: { SCJD05: SCJD05, SCJD06: SCJD06 },
        success: function (data) {
            var dt = eval("(" + data + ")");
            var tableHtml = "";
            //清除未选中项
            $("#Girardbd").find("tr").each(function () {
                if (!$(this).hasClass("ad_cls")) {
                    $(this).remove();
                };
            });
            for (var i = 0; i < dt.length; i++) {
                tableHtml += "<tr data-id='" + dt[i]["id"] + "'>";
                tableHtml += "    <td><span class='short_tit f_fl'>" + dt[i]["SCJD04"] + "</span></td>";
                tableHtml += "    <td><span class='short_tit f_fl'>" + dt[i]["SCJD05"] + "</span></td>";
                tableHtml += "    <td><span class='short_tit f_fl'>" + dt[i]["SCJD06"] + "</span></td>";
                tableHtml += "    <td><span class='short_tit f_fl'>" + dt[i]["SCJD07"] + "</span></td>";
                tableHtml += "</tr>";
            }
            $("#Girardbd").append(tableHtml);
        },
        error: function () {
            layer.alert("出错了，请联系管理员!");
        }
    })
}

function GetdateTimeL(obj) {
    if (isNull(obj) || obj == "1900-01-01") {
        return "";
    } else {
        return obj;
    }
}


//款号查询确认
function KHchk(obj) {
    var daima_name = "";
    var daima_id = "";
    $("#Girardbd").find("tr").each(function () {
        if ($(this).hasClass("ad_cls")) {
            daima_name += "'" + $(this).find("td").eq(1).find("span").html() + "'" + ",";
            daima_id += $(this).attr("data-id") + ",";
        }
    });
    if (daima_id.length > 0) {
        daima_id = daima_id.substring(0, daima_id.length - 1);
        daima_name = daima_name.substring(0, daima_name.length - 1);
    }
    //console.log(daima_id);
    //console.log(daima_name);
    $("#txtGirard").val(daima_name);
    if (obj == 1) {//商品部下单
        SearchObjectByGet(1, 100, daima_id);
        $(".Reconciliation_ser_wai").toggle(100);
        $("#maskIframe").toggle(100);
    }
    else if (obj == 2) {//生产部排单
        SearchPDByGet(1, 100, daima_id);
        $(".Reconciliation_ser_wai").toggle(100);
        $("#maskIframe").toggle(100);
    }
    else if (obj == 3) {//采购部原料排期
        SearchCGByGet(1, 100, daima_id);
        $(".Reconciliation_ser_wai").toggle(100);
        $("#maskIframe").toggle(100);
    }
    else {//品控质检报工
        SearchQACheckByGet(1, 100, daima_id);
        $(".Reconciliation_ser_wai").toggle(100);
        $("#maskIframe").toggle(100);
    }
}
//$(function () {

//    //双击表格中的行后隐藏tbody
//    $(" .Reconciliation_ser tbody").delegate(".short_tit", "dblclick", function () {
//        var daima = $(".ad_cls").find("td").eq(2);
//        var daima_name = $(".ad_cls").find("td").eq(1);
//        //console.log(daima.html() + "," + daima_name.html());
//        //$("#SCJD05").val(daima.find("span").html());
//        //$("#SCJD06").val(daima_name.find("span").html());
//        $("#txtGirard").val(daima_name.find("span").html());
//    })

//    //$(" #Reconciliation_ser_wai2 .Reconciliation_ser").delegate("td", "click", function () {
//    //    $(this).parent("tr").addClass("ad_cls");
//    //})

//});


$(function () {
    //删除
    $("#lnkBtnDel").click(function () {
        obj = getByName("chkItem");
        ckval = "";
        for (i in obj) {
            if (obj[i].checked)
                ckval += obj[i].value + ",";
        }
        if (ckval == "") {
            layer.alert("您尚未选择删除项");
        }
        else {
            layer.open({
                title: '消息',
                content: '您确定要删除吗？',
                btn: ['确定', '取消'],
                yes: function (index, layero) {
                    $.get("/GoodsInfo/DelSCJDB", { id: ckval }, function (data) {
                        if (data == "OK") {
                            layer.open({
                                title: '消息',
                                content: '删除成功!',
                                btn: ['确定'],
                                yes: function (index, layero) {
                                    window.location.reload();//刷新页面
                                }
                            });
                        }
                        else if (data == "Operation") {
                            layer.alert("您没有权限请和管理员联系");
                        }
                        else {
                            layer.alert("出错了，请联系管理员?");
                        }
                    });
                }
            });
        }
    })

    $('#sptbd').delegate('tr', 'mouseenter', function () {
        $(this).children("td").eq(1).find(".e_edi1").show();
    });
    $('#sptbd').delegate('tr', 'mouseleave', function () {
        $(this).children("td").eq(1).find(".e_edi1").hide();
    });
})

function ShowTable(ee) {
    if (ee == "1") {
        $('#myTable01').fixedHeaderTable({ footer: false, cloneHeadToFoot: true, autoShow: false });
        $('#myTable01').fixedHeaderTable('show');
    } else if (ee == "2") {
        $('#myTable07').fixedHeaderTable({ footer: false, cloneHeadToFoot: true, autoShow: false });
        $('#myTable07').fixedHeaderTable('show');
    } else if (ee == "3") {
        $('#myTable08').fixedHeaderTable({ footer: false });
        $('#myTable08').fixedHeaderTable('show');
    } else if (ee == "5") {
        $(".fht-fixed-column").remove();
        $('#myTable05').fixedHeaderTable({ footer: false, fixedColumns: 9 });
    } else {
        if (ee == "6") {
            $(".fht-fixed-column").remove();
            $('#myTable06').fixedHeaderTable({ footer: false, fixedColumns: 8 });
        }
    }
    $(".fancyTable").find("tbody").delegate('tr', 'mouseover', function () {
        $(this).addClass("t_tr_on");
        $(this).find(".e_edi1").show();
    });
    $(".fancyTable").find("tbody").delegate('tr', 'mouseout', function () {
        $(this).removeClass("t_tr_on");
        $(this).find(".e_edi1").hide();
    });
}


document.onkeydown = function (event) {
    var e = event || window.event || arguments.callee.caller.arguments[0];
    if (e && e.keyCode == 13) { // enter 键
        $(".sswin_td3").find(".so_btn").click();
    }
};
