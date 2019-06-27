//复选框2017-12-13
$(".fancyTable tbody").delegate('span.ck_box', 'click', function () {
     var me = $(this);
    var input = me.find("input");
    if (input.attr("checked")) {
        input.attr("checked", false);
        me.removeClass("z_select");

    } else {
        input.attr("checked", true);
        me.addClass("z_select");

    }

});

function ShowTable(ee) {
    if (ee == "1") {
        $('#myTable01').fixedHeaderTable({ footer: false, cloneHeadToFoot: true, autoShow: false});
        $('#myTable01').fixedHeaderTable('show');
    } else if (ee == "2") {
         $('#myTable02').fixedHeaderTable({ footer: false, cloneHeadToFoot: true, autoShow: false });
        $('#myTable02').fixedHeaderTable('show');
    } else {
         if (ee == "3") {
            $('#myTable08').fixedHeaderTable({ footer: false });
            $('#myTable08').fixedHeaderTable('show');
        }
    }
}


//获取table的值并绑定
function GetSampleListTable(page, ShowPageCount) {
    layer.msg('加载中......', {
        icon: 16,
        shade: 0.01
    });
    var where = tertLoopSel();
    $.ajax({
        url: "/Bom/GetBomIndex",
        type: "post",
        async: false,
        data: {
            page: page,
            ShowPageCount: ShowPageCount,
            where: where,
        },
        success: function (data) {
            if (data == "error") {
                layer.alert("不存在数据");
            }
            else {
                var dt = eval("(" + data + ")");
                var tableHtml = "";
                $("#bomTB").html("");
                if (dt.length > 0) {
                    for (var i = 0; i < dt.length; i++) {
                        tableHtml += "<tr>";
                        tableHtml += "    <td class='td1'><div class='nsw_check_box'><span onclick='XZ_select(this)' class='ck_box'><span class='dn'><input type='checkbox' value='" + dt[i].id + "' name='chkItem'></span></span></div></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl tdtop'><a class='update_Title' href='##'>" + dt[i].Sample01 + "</a><i onclick='btnEdit(" + dt[i].id + ")' data-src='/Bom/SplBomyAddUpdate/" + dt[i].id + "' style='display: none;' class='e_edi1 e_more_edit popUp'></i></span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample02 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample03 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample04 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample05 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample06 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample07 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample08 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample09 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample10 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample11 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample12 + "</span></td>";
                        tableHtml += "    <td class='nsw_cnt_area action_tx'><a onclick='btnEdit(" + dt[i].id + ")' href='##'><span class='pro_view pro_edit'><i></i>编辑</span></a></td>";
                        tableHtml += "</tr>";
                    }
                    $("#Crows").html(dt[0].RowsCount);
                    $("#Cpage").html(dt[0].pageCount);
                    $("#pageNow").html(page);
                }
                else {
                    $("#Crows").html(0);
                    $("#Cpage").html(0);
                    $("#pageNow").html(0);
                }
                $("#bomTB").append(tableHtml);
                GetDDLPage($("#Cpage").html(), page)
                layer.closeAll();
                ShowTable("1");
            }
        },
        error: function () {
            alert("服务器连接超时");
        }
    })
}


//加载显示页数DDL
function GetDDLPage(Cpage, page) {

    var ddlPage = "";
    $("#pageOP").html("");
    for (var i = 0; i < Cpage; i++) {
        if (i == page - 1) {
            ddlPage += "<option selected='selected' value='" + (i + 1) + "'>" + (i + 1) + "</option>"
        }
        else {
            ddlPage += "<option value='" + (i + 1) + "'>" + (i + 1) + "</option>"
        }
    }
    $("#pageOP").append(ddlPage);

}


//编辑前绑定数据
function btnEdit(id) {
    location.href = "/Bom/SplBomyAddUpdate/" + id;
}

//数据导入上传Excel的方法
function importExcel(from, files, url) {
    var fName = files.attr("values");
    var file = files.val();
    if (file == null || file.length == 0) {
        layer.alert("请先选择上传文件！");
        return false;
    }
    from.ajaxSubmit({
        type: 'POST', // HTTP请求方式
        url: url, // 请求的URL地址
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
            } else {
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
            layer.alert("oh,似乎出现点问题了！");
        }
    });
}

//绑定波段下拉框
function LoadSCJD02(obj) {
    $.get("/GoodsInfo/GetDDLSCJD02", { DDLjidu: obj }, function (data) {
        var ddlBD = "";
        $(".sel_tr2").html("");//清空DDL
        var dt = eval("(" + data + ")");
        ddlBD += "<option value=''>请选择</option>";
        for (var i = 0; i < dt.length; i++) {
            ddlBD += "<option values=" + dt[i]["SXMC"] + ">" + dt[i]["SXMC"] + "</option>";
        }
        $(".sel_tr2").append(ddlBD);
    });
}

//条件查询,循环条件
function tertLoopSel() {

    var where = "";
    $("#tabHeader").find("th").find("input").each(function () {
        if ($(this).val() != "") {
            where += $(this).attr("lieName") + "|" + $(this).val() + ",";
        }
    })
    $("#tabHeader").find("th").find("select").each(function () {
        if ($(this).val() != "") {
            where += $(this).attr("lieName") + "|" + $(this).val() + ",";
        }
    })
    if (where != "") {
        where = where.substring(0, where.length - 1);
        //console.log(where);
        return where;
    }
}
//当鼠标移开文本/下拉框时执行查询操作
function blurss(obj) {
    if (obj == 1) {
        if ($("#tabHeader").find("th").find("select").eq(1).val() != "") {
            var DDLjidu = $("#ddlJD").val();
            LoadSCJD02(DDLjidu);//绑定波段下拉框
        }
        else {//当季节下拉框为请选择时
            LoadSCJD02(0);
        }
    }
    GetSampleListTable(1, $("#pageList_btn").val());
    GetBPM_CargoListTable(1, $("#pageList_btn").val());
}

$(function () {
    //上一页
    $("#BackPage").click(function () {
        var page = parseInt($("#pageNow").html());
        var ShowPageCount = $("#pageList_btn").val();
        if (page > 1) {
            page = page - 1;
            GetSampleListTable(page, ShowPageCount);
            GetBPM_CargoListTable(page, ShowPageCount);
        }
        else {
            layer.alert("已经到第一页了");
            return false;
        }
    })

    //下一页
    $("#NextPage").click(function () {
        var page = parseInt($("#pageNow").html());
        var ShowPageCount = $("#pageList_btn").val();
        if (page < $("#Cpage").html()) {
            page = page + 1;
            GetSampleListTable(page, ShowPageCount);
            GetBPM_CargoListTable(page, ShowPageCount);
        }
        else {
            layer.alert("已经到最后一页了");
            return false;
        }
    })

    //跳转页数
    $("#pageOP").change(function () {
        var page = $(this).val();
        var ShowPageCount = $("#pageList_btn").val();
        GetSampleListTable(page, ShowPageCount);
        GetBPM_CargoListTable(page, ShowPageCount);
    })

    //加载显示数量
    $("#pageList_btn").change(function () {
        var page = $("#pageNow").html();
        var ShowPageCount = $(this).val();
        GetSampleListTable(page, ShowPageCount);
        GetBPM_CargoListTable(page, ShowPageCount);
    })

    //样衣bom删除
    $("#lnkBtnDel").click(function () {
        var obj = document.getElementsByName("chkItem");
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
                    $.get("/Bom/DelBomTable", { id: ckval.substring(0, ckval.length - 1) }, function (data) {
                        if (data == "success") {
                            layer.open({
                                title: '消息',
                                content: '删除成功!',
                                btn: ['确定'],
                                yes: function (index, layero) {
                                    window.location.reload();//刷新页面
                                }
                            });
                        }
                            //else if (data == "Operation") {
                            //    layer.alert("您没有权限请和管理员联系");
                            //}
                        else {
                            layer.alert("删除失败");
                        }
                    });
                }
            });

        }
    })

    //大货bom删除
    $("#lnkBtnCargoDel").click(function () {
        var obj = document.getElementsByName("chkItem");
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
                    $.get("/Bom/DelCargoTable", { id: ckval.substring(0, ckval.length - 1) }, function (data) {
                        if (data == "success") {
                            layer.open({
                                title: '消息',
                                content: '删除成功!',
                                btn: ['确定'],
                                yes: function (index, layero) {
                                    window.location.reload();//刷新页面
                                }
                            });
                        }
                            //else if (data == "Operation") {
                            //    layer.alert("您没有权限请和管理员联系");
                            //}
                        else {
                            layer.alert("删除失败");
                        }
                    });
                }
            });

        }
    })
})

//批量修改的方法
function shenhe(obj, type) {
    var id = $(obj).attr("id");
    chkID = document.getElementsByName("chkItem");
    ckval = "";
    for (i in chkID) {
        if (chkID[i].checked)
            ckval += chkID[i].value + ",";
    }
    if (ckval == "") {
        layer.alert("您尚未选择修改项");
    }
    else {
        var staSel = "";//状态下拉框值
        var Bomstr = "";//后台接口

        var state = ""; //获取样衣bom当前状态
        var ss = 0; //判断（ss=0则无，ss=1则有）

        if (type == 1) {         //大货bom
            staSel = '</select>';
            Bomstr = "/Bom/UpdateState";

            $("#CargoTB").find("tr").each(function () {
                var aa = $(this).find("td").eq(0).find("input").attr("value");
                var bb = ckval.substring(0, ckval.length - 1).split(',');
                //var ddlZT = "";
                for (var i = 0; i < bb.length; i++) {
                    if (aa == bb[i]) {
                        var ddlZT = $(this).find("td").eq(12).find("span").html();
                        //state += ddlZT + ",";
                        if (ddlZT == "生成大货BOM" || ddlZT == "作废") {
                            ss = 1;
                        }
                    }
                }
            })//判断选中的Bom中有无已作废或已生成大货Bom单（ss=0则无，ss=1则有）
        }
        else {                 //样衣bom
            staSel = '<option value="3">生成大货BOM</option></select>';
            Bomstr = "/Bom/BatchEditState";

            $("#bomTB").find("tr").each(function () {
                var aa = $(this).find("td").eq(0).find("input").attr("value");
                var bb = ckval.substring(0, ckval.length - 1).split(',');
                //var ddlZT = "";
                for (var i = 0; i < bb.length; i++) {
                    if (aa == bb[i]) {
                        var ddlZT = $(this).find("td").eq(12).find("span").html();
                        //state += ddlZT + ",";
                        if (ddlZT == "生成大货BOM" || ddlZT == "作废") {
                            ss = 1;
                        }
                        else if (ddlZT == "待审") {
                            ss = 2;
                        }
                    }
                }
            })//判断选中的Bom中有无已作废或已生成大货Bom单（ss=0则无，ss=1则有）
        }
        layer.open({
            title: '批量修改状态',
            content: '请选择状态:<select id="BomState"><option value="">请选择</option><option value="0">反审</option><option value="1">审核</option><option value="2">作废</option>' + staSel,
            btn: ['确定', '关闭'],
            yes: function (index, layero) {
                var data = $("#BomState").val();
                if (data == "" || data == null) {
                    layer.alert("请选择批量修改状态");
                }
                else if (ss == 1) {
                    if (type == 1) {
                        layer.alert("您编辑项中有已作废单，操作已终止");
                    }
                    else {
                        layer.alert("您编辑项中有已作废或已生成大货Bom单，操作已终止");
                    }
                }
                else if (ss != 0 && data == 3) {
                    layer.alert("只有审核通过后才能生成大货Bom");
                }
                else if (ss == 2 && data == 0) {
                    layer.alert("您的反审对象中有待审单,操作已终止");
                }
                else {
                    $.get(Bomstr, { data: data, id: id, ckval: ckval }, function (data) {
                        if (data == "success") {
                            layer.open({
                                title: '消息',
                                content: '批量修改成功',
                                btn: ['确定'],
                                yes: function (index, layero) {
                                    if (type == 1) {
                                        GetBPM_CargoListTable(1, 100);  //重新加载大货bom
                                    } else {
                                        GetSampleListTable(1, 100);      //重新加载样衣bom
                                    }
                                    layer.closeAll();

                                    //批量修改成功后全面单选框依然保持选中状态
                                    ckval = ckval.substring(0, ckval.length - 1);//获取选中ID
                                    KeepChecked(ckval);
                                }
                            });
                        }
                        else if (data == "yes") {
                            layer.open({
                                title: '消息',
                                content: '生成大货Bom成功',
                                btn: ['确定'],
                                yes: function (index, layero) {
                                    GetSampleListTable(1, 100);
                                    layer.closeAll();

                                    //批量修改成功后全面单选框依然保持选中状态
                                    ckval = ckval.substring(0, ckval.length - 1);//获取选中ID
                                    KeepChecked(ckval);
                                }
                            });
                        } else if (data=="null") {
                            layer.alert("请完善数据后再生成!")
                        }
                            //else if (data == "Operation") {
                            //    layer.alert("您没有权限请和管理员联系");
                            //}
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

function xiugai() {
    alert("2");
}

function zuofei() {
    alert("3");
}

function baocun() {
    alert("4");
}



function Costadd() {
    alert("5");
}

function Costxiugai() {
    alert("6");
}

function Costdel() {
    alert("7");
}

function Costbaocun() {
    alert("8");
}


//------------------------------------大货bom-------------------------------------------------//
//获取table的值并绑定
function GetBPM_CargoListTable(page, ShowPageCount) {
    layer.msg('加载中......', {
        icon: 16,
        shade: 0.01
    });
    var where = tertLoopSel();
    $.ajax({
        url: "/Bom/GetCargoIndex",
        type: "post",
        async: false,
        data: {
            page: page,
            ShowPageCount: ShowPageCount,
            where: where,
        },
        success: function (data) {
            if (data == "error") {
                layer.alert("不存在数据");
            }
            else {
                var dt = eval("(" + data + ")");
                var tableHtml = "";
                $("#CargoTB").html("");
                if (dt.length > 0) {
                    for (var i = 0; i < dt.length; i++) {
                        tableHtml += "<tr>";
                        tableHtml += "    <td class='td1'><div class='nsw_check_box'><span onclick='XZ_select(this)' class='ck_box'><span class='dn'><input type='checkbox' value='" + dt[i].id + "' name='chkItem'></span></span></div></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl tdtop'><a class='update_Title' href='##'>" + dt[i].Sample01 + "</a><i onclick='btnCargoEdit(" + dt[i].id + ")' data-src='/Bom/BigBomAddUpdate/" + dt[i].id + "' style='display: none;' class='e_edi1 e_more_edit popUp'></i></span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample02 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample03 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample04 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample05 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample06 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample07 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample08 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample09 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample10 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample11 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample12 + "</span></td>";
                        tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i].Sample15 + "</span></td>";
                        tableHtml += "    <td class='nsw_cnt_area action_tx'><a onclick='btnCargoEdit(" + dt[i].id + ")' href='##'><span class='pro_view pro_edit'><i></i>编辑</span></a></td>";
                        tableHtml += "</tr>";
                    }
                    $("#Crows").html(dt[0].RowsCount);
                    $("#Cpage").html(dt[0].pageCount);
                    $("#pageNow").html(page);
                }
                else {
                    $("#Crows").html(0);
                    $("#Cpage").html(0);
                    $("#pageNow").html(0);
                }
                $("#CargoTB").append(tableHtml);
                GetDDLPage($("#Cpage").html(), page)
                layer.closeAll();
                ShowTable("1");
            }
        },
        error: function () {
            alert("服务器连接超时");
        }
    })
}


//编辑前绑定数据
function btnCargoEdit(id) {
    location.href = "/Bom/BigBomAddUpdate/" + id;
}

