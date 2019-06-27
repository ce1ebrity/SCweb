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


var page = 1;

//MoneyIndex
function GetMoneyIndexTbody(page, type) {
    layer.msg('加载中......', {
        icon: 16,
        shade: 0.01
    });
    var where = GetSelectCount();
    $.ajax({
        url: "/MoneyManage/GetYCList",
        type: "post",
        data: { page: page, showRows: $("#pageList_btn").val(), where: where },
        async: false,
        beforeSend: function () {

        },
        success: function (data) {
            var jsons = eval("(" + data + ")");
            $("#dbMoneyIndex").html("");
            var outHtml = "";

            console.log(jsons);
            for (var i = 0; i < jsons.length; i++) {
                outHtml = "<tr>";
                outHtml += "<td><span>" + jsons[i]["异常单号"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["品牌"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["加工方式"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["季节"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["波段"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["款号"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["颜色"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["尺码"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["生产厂家"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["商品要求"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["合同签订"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["下单生产量"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["生产下单量"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["实裁数量"] + "</span></td>";
                outHtml += "<td><span>" + jsons[i]["正确出货量"] + "</span></td>";
                outHtml += "<td><span style='word-wrap:break-word;white-space:normal;line-height:16px;'>" + jsons[i]["异常情况说明"] + "</span></td>";
                outHtml += "</tr>";
                $("#dbMoneyIndex").append(outHtml);
            }
            if (jsons.length > 0) {
                $("#rowsCount").html(jsons[0]["rowsCount"]);
                $("#pageCount").html(jsons[0]["pageCount"]);
                $("#page").html(page);
            } else {
                $("#rowsCount").html(0);
                $("#pageCount").html(0);
                $("#page").html(0);

            }
            ShowTable("1");
            layer.closeAll();
        },
        error: function (data) {
            layer.alert("服务器连接超时！");
        }
    });
    
   //合并单元格
   $('#myTable01').mergeCell({
        cols: [15]
    });

    //$('#myTable01').fixedHeaderTable({ footer: false, cloneHeadToFoot: true, altClass: 'odd', autoShow: false });

    //$('#myTable01').fixedHeaderTable('show');
}
//CKIndex
function GetCKIndexTbody(page, type) {
    layer.msg('加载中......', {
        icon: 16,
        shade: 0.01
    });
    var stringWhere = burr();
    $.ajax({
        url: "/MoneyManage/GetCKList",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        data: { page: page, showpage: $("#pageList_btn").val(), stringWhere: stringWhere },
        success: function (data) {
            if (data == "error") {
                layer.alert("出错了，请联系管理员!");
            }
            else {
                var dt = eval("(" + data + ")");
                var tableHtml = "";
                $("#sptbd").html("");
                var classchild = "";
                for (var i = 0; i < dt.length; i++) {
                    tableHtml += "<tr class='child_2'>";
                    tableHtml += "    <td class='td1'><div class='nsw_check_box'><span class='ck_box'><span class='dn'><input type='checkbox' name='chkItem' value='" + dt[i]["id"] + "'></span></span></div></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl tdtop'><a href='##' class='update_Title'>" + dt[i]["GYSJ"] + "</a><i class='e_edi1 e_more_edit popUp' onclick='edits(" + dt[i].id + ")'></i></span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["GYSM"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["DJLX"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["DJBH"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv' title='" + dt[i]["CKYY"] + "' ><span class='poducts short_tit f_fl'>" + dt[i]["CKYY"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["CKJE"] + "</span></td>";
                    tableHtml += "    <td class='nsw_cnt_area action_tx'><a href='##' onclick='edits(" + dt[i]["id"] + ")'><span class='pro_view pro_edit'><i></i>编辑</span></a></td>";
                    tableHtml += "</tr>";
                }

                $("#sptbd").append(tableHtml);

                if (dt.length > 0) {
                    $("#rowsCount").html(dt[0]["rowsCount"]);
                    $("#pageCount").html(dt[0]["pageCount"]);
                    $("#page").html(page);
                } else {
                    $("#rowsCount").html(0);
                    $("#pageCount").html(0);
                    $("#page").html(0);

                }

                layer.closeAll();
                ShowTable("1");
            }
        },
        error: function () {
            layer.alert("出错了，请联系管理员!");
        }
    });
}
//SetBpmFKSQIndex
function GetSetBpmFKSQIndexTbody1(page, type) {

    var stringWhere = burr1();
    $.ajax({
        url: "/MoneyManage/SetBpmFKSQList1",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        async: false,
        data: { page: page, showpage: $("#pageList_btn1").val(), stringWhere: stringWhere },
        success: function (data) {
            if (data == "error") {
                layer.alert("出错了，请联系管理员!");
            }
            else {
                var dr = eval("(" + data + ")");
                var tableHtml = "";
                $("#sptbd1").html("");
                var classchild = "";
                for (var i = 0; i < dr.length; i++) {
                    tableHtml = '<tr class="child_2"><td class="td1"><div class="nsw_check_box"><span class="ck_box"><span class="dn"><input type="checkbox" value="' + dr[i]["id"] + '" name="chkItem" /></span></span></div></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl tdtop"><a class="update_Title" href="##">' + dr[i]["FKSQ01"] + '</a><i  class="e_edi1 e_more_edit popUp" onclick="subAjax(' + dr[i]["id"] + ')" style="display: none;"></i></span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ14"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ02"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ03"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ04"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ07"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ05"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ06"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ09"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ10"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ11"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ12"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ13"] + '</span></td>';
                    tableHtml += '<td class="nsw_cnt_area action_tx"><a onclick="subAjax(' + dr[i]["id"] + ')" href="##"><span class="pro_view pro_edit"><i></i>编辑</span></a></td>';
                    tableHtml += '</tr>';
                    $("#sptbd1").append(tableHtml);
                }
                if (dr.length > 0) {
                    $("#rowsCount1").html(dr[0]["rowsCount"]);
                    $("#pageCount1").html(dr[0]["pageCount"]);
                    $("#page1").html(page);

                } else {
                    $("#rowsCount").html(0);
                    $("#pageCount").html(0);
                    $("#page").html(0);

                }
                ShowTable("1");
                layer.closeAll();
            }
        },
        error: function () {
            layer.alert("出错了，请联系管理员!");
        }
    });
}
function GetSetBpmFKSQIndexTbody2(page, type) {

    var stringWhere = burr2();
    $.ajax({
        url: "/MoneyManage/SetBpmFKSQList2",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        async: false,
        data: { page: page, showpage: $("#pageList_btn2").val(), stringWhere: stringWhere },
        success: function (data) {
            if (data == "error") {
                layer.alert("出错了，请联系管理员!");
            }
            else {
                var dr = eval("(" + data + ")");
                var tableHtml = "";
                $("#sptbd2").html("");
                var classchild = "";
                for (var i = 0; i < dr.length; i++) {

                    tableHtml = '<tr class="child_2"><td class="td1"><div class="nsw_check_box"><span class="ck_box"><span class="dn"><input type="checkbox" value="' + dr[i]["id"] + '" name="chkItem"></span></span></div></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl tdtop"><a class="update_Title" href="##">' + dr[i]["FKSQ01"] + '</a><i class="e_edi1 e_more_edit popUp" onclick="subAjax(' + dr[i]["id"] + ')" style="display: none;"></i></span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ14"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ02"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ05"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ06"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ03"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ04"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ09"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ10"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ11"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ12"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ13"] + '</span></td>';
                    tableHtml += '<td class="nsw_cnt_area action_tx"><a onclick="subAjax(' + dr[i]["id"] + ')" href="##"><span class="pro_view pro_edit"><i></i>编辑</span></a></td>';
                    tableHtml += '</tr>';
                    $("#sptbd2").append(tableHtml);
                }

                if (dr.length > 0) {


                    $("#rowsCount2").html(dr[0]["rowsCount"]);
                    $("#pageCount2").html(dr[0]["pageCount"]);
                    $("#page2").html(page);

                } else {
                    $("#rowsCount").html(0);
                    $("#pageCount").html(0);
                    $("#page").html(0);

                }
                ShowTable("2");
                layer.closeAll();

            }
        },
        error: function () {
            layer.alert("出错了，请联系管理员!");
        }
    });
}
function GetSetBpmFKSQIndexTbody3(page, type) {

    var stringWhere = burr3();
    $.ajax({
        url: "/MoneyManage/SetBpmFKSQList3",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        data: { page: page, showpage: $("#pageList_btn3").val(), stringWhere: stringWhere },
        success: function (data) {
            if (data == "error") {
                layer.alert("出错了，请联系管理员!");
            }
            else {
                var dr = eval("(" + data + ")");
                var tableHtml = "";
                $("#sptbd3").html("");
                var classchild = "";
                for (var i = 0; i < dr.length; i++) {
                    tableHtml = '<tr class="child_2"><td class="td1"><div class="nsw_check_box"><span class="ck_box"><span class="dn"><input type="checkbox" value="' + dr[i]["id"] + '" name="chkItem"></span></span></div></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl tdtop"><a class="update_Title" href="##">' + dr[i]["FKSQ01"] + '</a><i class="e_edi1 e_more_edit popUp" onclick="subAjax(' + dr[i]["id"] + ')" style="display: none;" ></i></span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ14"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ02"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ03"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ04"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ08"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ09"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ07"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ05"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ06"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ10"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ11"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ12"] + '</span></td>';
                    tableHtml += '<td class="pimgsv"><span class="poducts short_tit f_fl">' + dr[i]["FKSQ13"] + '</span></td>';
                    tableHtml += '<td class="nsw_cnt_area action_tx"><a onclick="subAjax(' + dr[i]["id"] + ')" href="##"><span class="pro_view pro_edit"><i></i>编辑</span></a></td>';
                    $("#sptbd3").append(tableHtml);
                }
                if (dr.length > 0) {
                    $("#rowsCount3").html(dr[0]["rowsCount"]);
                    $("#pageCount3").html(dr[0]["pageCount"]);
                    $("#page3").html(page);
                } else {
                    $("#rowsCount").html(0);
                    $("#pageCount").html(0);
                    $("#page").html(0);
                }
                ShowTable("3");
                layer.closeAll();

            }
        },
        error: function () {
            layer.alert("出错了，请联系管理员!");
        }
    });
}
function ShowTable(ee) {
    if (ee == "1") {

        $('#myTable01').fixedHeaderTable({ footer: false, cloneHeadToFoot: true, autoShow: false });
        $('#myTable01').fixedHeaderTable('show');
    } else if (ee == "2") {

        $('#myTable07').fixedHeaderTable({ footer: false, cloneHeadToFoot: true, autoShow: false });
        $('#myTable07').fixedHeaderTable('show');
    } else {

        if (ee == "3") {
            $('#myTable08').fixedHeaderTable({ footer: false });
            $('#myTable08').fixedHeaderTable('show');
        }
    }




    //$('#myTable08').fixedHeaderTable({ footer: true, altClass: 'odd' });

    //$('#myTable01').fixedHeaderTable({ footer: false, cloneHeadToFoot: true, altClass: 'odd', autoShow: false });
    //$('#myTable01').fixedHeaderTable('show');

    //$('#myTable07').fixedHeaderTable({ footer: true, altClass: 'odd' });

    //$('#myTable08').fixedHeaderTable({ footer: true, altClass: 'odd' });
}
//SetBpmCWDZIndex
function GetCWDZIndexTbody(page, type) {
    layer.msg('加载中......', {
        icon: 16,
        shade: 0.01
    });

    var stringWhere = "";
    $.ajax({
        url: "/MoneyManage/GetCWDZLists",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        async: false,
        data: { page: page, showpage: $("#pageList_btn").val(), stringWhere: stringWhere },
        success: function (data) {
            if (data == "error") {
                layer.alert("出错了，请联系管理员!");
            }
            else {
                var dt = eval("(" + data + ")");
                var tableHtml = "";
                $("#sptbd").html("");
                var classchild = "";
                for (var i = 0; i < dt.length; i++) {
                    tableHtml += "<tr class='child_2'>";
                    tableHtml += "    <td class='td1'><div class='nsw_check_box'><span class='ck_box'><span class='dn'><input type='checkbox' name='chkItem' value='" + dt[i]["id"] + "'></span></span></div></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl tdtop'><a href='##' class='update_Title'>" + dt[i]["CWDZ01"] + "</a><i class='e_edi1 e_more_edit popUp' style='display: none;' onclick='edits(" + dt[i].id + ")'></i></span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["CWDZ02"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["CWDZ03"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["CWDZ04"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv' title='" + dt[i]["CWDZ05"] + "'><span class='poducts short_tit f_fl'>" + dt[i]["CWDZ05"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["CWDZ06"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["CWDZ07"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["CWDZ08"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["CWDZ09"] + "</span></td>";
                    tableHtml += "    <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["CWDZ10"] + "</span></td>";
                    tableHtml += "    <td class='nsw_cnt_area action_tx'><a href='##' onclick='edits(" + dt[i]["id"] + ")'><span class='pro_view pro_edit'><i></i>编辑</span></a></td>";
                    tableHtml += "</tr>";
                }

                $("#sptbd").append(tableHtml);

                if (dt.length > 0) {
                    $("#rowsCount").html(dt[0]["rowsCount"]);
                    $("#pageCount").html(dt[0]["pageCount"]);
                    $("#page").html(page);
                } else {
                    $("#rowsCount").html(0);
                    $("#pageCount").html(0);
                    $("#page").html(0);

                }
                layer.closeAll();
            }
        },
        error: function () {
            layer.alert("出错了，请联系管理员!");
        }
    });
}

function GetCostIndexTbody(page, type) {
    layer.msg('加载中......', {
        icon: 16,
        shade: 0.01
    });
    var where = "";
    $("#tabHeader").find("th").each(function (index) {

        if ($(this).find(".hdspan02 input").length > 0) {
            if ($(this).find(".hdspan02 input").val().length > 0)
                where += $(this).find(".hdspan02 input").attr("lieName") + "," + $(this).find(".hdspan02 input").val() + "|";
        } else if ($(this).find(".hdspan02 select").length > 0) {
            if ($(this).find(".hdspan02 select").val().length > 0)
                where += $(this).find(".hdspan02 select").attr("lieName") + "," + $(this).find(".hdspan02 select").val() + "|";
        }
    });
    if (where.length > 0) {
        where = where.substring(0, where.length - 1);
    }

    $.ajax({
        url: "/Cost/CostListIndex",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        data: { page: page, showpage: $("#pageList_btn").val(), stringWhere: where },
        success: function (data) {
            if (data == "error") {
                layer.alert("出错了，请联系管理员!");
            }
            else {
                var dt = eval("(" + data + ")");
                var tableHtml = "";
                $("#gysTB").html("");
                var classchild = "";
                for (var i = 0; i < dt.length; i++) {
                    tableHtml += "<tr>";
                    tableHtml += "  <td class='td1'><div class='nsw_check_box'><span class='ck_box' onclick='XZ_select(this)'><span class='dn'><input type='checkbox' name='chkItem' value='" + dt[i]["id"] + "'></span></span></div></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl tdtop'><a class='update_Title' href='##'>" + dt[i]["COST01"] + "</a><i onclick='cost.Bedit(" + dt[i]["id"] + ")' data-src='/Cost/CostAddUpdate/" + dt[i]["id"] + "' style='display: none;' class='e_edi1 e_more_edit popUp'></i></span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST02"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST03"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST04"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST05"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST06"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST07"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST08"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST09"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST10"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST11"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST12"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + dt[i]["COST13"] + "</span></td>";
                    tableHtml += "  <td class='pimgsv'><span class='poducts short_tit f_fl'>" + (parseFloat(dt[i]["COST09"]) + parseFloat(dt[i]["COST10"]) + parseFloat(dt[i]["COST11"]) + parseFloat(dt[i]["COST12"]) + parseFloat(dt[i]["COST13"])) + "</span></td>";
                    tableHtml += "  <td class='nsw_cnt_area action_tx'><a onclick='cost.Bedit(" + dt[i]["id"] + ")' href='##'><span class='pro_view pro_edit'><i></i>编辑</span></a></td>";
                    tableHtml += "</tr>";
                }

                $("#gysTB").append(tableHtml);

                if (dt.length > 0) {
                    $("#rowsCount").html(dt[0]["RowsCount"]);
                    $("#pageCount").html(dt[0]["pageCount"]);
                    $("#page").html(page);
                } else {
                    $("#rowsCount").html(0);
                    $("#pageCount").html(0);
                    $("#page").html(0);
                }
                ShowTable("1");
                layer.closeAll();

            }
        },
        error: function () {
            layer.alert("出错了，请联系管理员!");
        }
    });
}




















//上一页方法
function BackPageUpper(obj, text) {
    var $span = $(obj).parent().parent().find("span span");
    var $spanPage = $span.eq(1);//当前页

    if ($spanPage.html() == "1") {
        layer.alert("已经是第一页了！");
    } else if ($spanPage.html() == "0") {
        layer.alert("暂无数据！");
    } else {
        var pages = parseInt($spanPage.html()) - 1;

        if (text == '异常管理') {
            GetMoneyIndexTbody(pages, '异常管理');
        } else if (text == '扣款管理') {
            GetCKIndexTbody(pages, '扣款管理');
        }
        else if (text == '付款申请1') {
            GetSetBpmFKSQIndexTbody1(pages, "翻页");
        }
        else if (text == '付款申请2') {
            GetSetBpmFKSQIndexTbody2(pages, "翻页");
        }
        else if (text == '付款申请3') {
            GetSetBpmFKSQIndexTbody3(pages, "翻页");
        }
        else if (text == '财务对账') {
            GetCWDZIndexTbody(pages, "翻页");
        }
        $spanPage.html(pages);
    }
}
//下一页方法
function BackPageLower(obj, text) {
    var $span = $(obj).parent().parent().find("span span");
    var $spanPage = $span.eq(1);//当前页
    var $spanCountPage = $span.eq(2);//总页数


    if ($spanPage.html() == "0") {
        layer.alert("暂无数据！");
    } else if ($spanPage.html() == $spanCountPage.html()) {
        layer.alert("已经是最后一页了！");
    } else {
        var pages = parseInt($spanPage.html()) + 1;

        if (text == '异常管理') {
            GetMoneyIndexTbody(pages, '翻页');
        } else if (text == '扣款管理') {
            GetCKIndexTbody(pages, '翻页');
        }
        else if (text == '付款申请1') {
            GetSetBpmFKSQIndexTbody1(pages, "翻页");
        }
        else if (text == '付款申请2') {
            GetSetBpmFKSQIndexTbody2(pages, "翻页");
        }
        else if (text == '付款申请3') {
            GetSetBpmFKSQIndexTbody3(pages, "翻页");
        }
        else if (text == '财务对账') {
            GetCWDZIndexTbody(pages, "翻页");
        }
        $spanPage.html(pages);
    }
}

function loadOption(Cpage, page) {
    var ddlHtml = "";
    $("#pageOP").html("");
    for (var i = 0; i < Cpage; i++) {
        if (i == page - 1) {
            ddlHtml += "<option selected='true' value='" + (i + 1) + "'>" + (i + 1) + "</option>";
        } else {
            ddlHtml += "<option value='" + (i + 1) + "'>" + (i + 1) + "</option>";
        }
    }
    $("#pageOP").append(ddlHtml);
}
//显示页数方法
function ShowPageCount(obj, text) {
    if (text == '异常管理') {
        GetMoneyIndexTbody(1, '查询');
    } else if (text == '扣款管理') {
        GetCKIndexTbody(1, '查询');
    }
    else if (text == '付款申请1') {
        GetSetBpmFKSQIndexTbody1(1, "翻页");
    }
    else if (text == '付款申请2') {
        GetSetBpmFKSQIndexTbody2(1, "翻页");
    }
    else if (text == '付款申请3') {
        GetSetBpmFKSQIndexTbody3(1, "翻页");
    }
    else if (text == '财务对账') {
        GetCWDZIndexTbody(pages, "翻页");
    }
}

function del(url, text) {
    var pa = $("#page").html();

    var obj = getByName("chkItem");
    var ckval = "";
    for (i in obj) {
        if (obj[i].getAttribute("checked") == "checked")
            ckval += obj[i].value + ",";
    }
    if (ckval == "") {
        layer.alert("尚未选择项");
    } else {
        layer.open({
            title: '消息',
            content: '您确定要删除已选中的项吗？',
            btn: ['确定', '取消'],
            yes: function (index, layero) {
                ckval = ckval.substring(0, ckval.length - 1);
                $.ajax({
                    url: url,
                    timeout: 0, //超时时间设置，单位毫秒
                    type: "post",
                    data: { ids: ckval },
                    success: function (data) {
                        if (data == "OK") {
                            layer.open({
                                title: '消息',
                                content: '删除成功！',
                                btn: ['确定'],
                                yes: function (index, layero) {
                                    if (text == '异常管理') {
                                        GetMoneyIndexTbody(pa, '查询');
                                    } else if (text == '扣款管理') {
                                        GetCKIndexTbody(pa, '查询');
                                    }
                                    else if (text == '付款申请1') {
                                        GetSetBpmFKSQIndexTbody1(pa, "翻页");
                                    }
                                    else if (text == '付款申请2') {
                                        GetSetBpmFKSQIndexTbody2(pa, "翻页");
                                    }
                                    else if (text == '付款申请3') {
                                        GetSetBpmFKSQIndexTbody3(pa, "翻页");
                                    }
                                    else if (text == '财务对账') {
                                        GetCWDZIndexTbody(pa, "翻页");
                                    }
                                    else if (text == '成本核算列表') {
                                        GetCostIndexTbody(pa, '查询');
                                    }


                                }
                            });
                        } else if (data == "NO") {
                            layer.alert("编辑失败！");
                        } else {
                            layer.alert(data);
                        }
                    },
                    error: function () {
                        layer.alert("出错了，请联系管理员!");
                    }
                });

            }
        });


    }
}



























//根据name值获得集合，主用于多选
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

$(".sswin").find(".search_div_xiala").delegate('li', 'click', function () {
    var xuanze = $(this).find("a").html();
    $(this).parent().parent().find(".xuanz_span").attr("values", $(this).find("a").attr("values"));
    $(this).parent().parent().find(".xuanz_span").html(xuanze);

});


$(".xuanz").hover(function () {
    $(this).find(".xuanz_ul").stop(true, true).slideDown("fast");
}, function () {
    $(this).find(".xuanz_ul").stop(true, true).slideUp("fast");
});


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
            layer.closeAll();
            layer.open({
                title: '消息',
                content: data,
                btn: ['确定'],
                yes: function (index, layero) {
                    window.location.reload();
                }
            });
        },
        error: function (data) {
            layer.alert("oh,似乎出现点问题了！");
        }
    });
}


//图片上传预览    IE是用了滤镜。
function previewImage(file) {

    var div = document.getElementById('preview');

    if (file.files && file.files[0]) {
        div.innerHTML = '<img width="99%" height="99%" id=imghead onclick=$("#previewImg").click()>';
        var img = document.getElementById('imghead');
        var reader = new FileReader();
        reader.onload = function (evt) { img.src = evt.target.result; }
        reader.readAsDataURL(file.files[0]);
    }
    else //兼容IE
    {
        var sFilter = 'filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src="';
        file.select();
        var src = document.selection.createRange().text;
        div.innerHTML = '<img id=imghead>';
        var img = document.getElementById('imghead');
        img.filters.item('DXImageTransform.Microsoft.AlphaImageLoader').src = src;
        var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
        status = ('rect:' + rect.top + ',' + rect.left + ',' + rect.width + ',' + rect.height);
        div.innerHTML = "<div id=divhead style='width:" + rect.width + "px;" + "px;margin-top:" + rect.top + "px;" + sFilter + src + "\"'></div>";
    }
}
function clacImgZoomParam(maxWidth, maxHeight, width, height) {
    var param = { top: 0, left: 0, width: width, height: height };
    if (width > maxWidth || height > maxHeight) {
        rateWidth = width / maxWidth;
        rateHeight = height / maxHeight;

        if (rateWidth > rateHeight) {
            param.width = maxWidth;
            param.height = Math.round(height / rateWidth);
        } else {
            param.width = Math.round(width / rateHeight);
            param.height = maxHeight;
        }
    }
    param.left = Math.round((maxWidth - param.width) / 2);
    param.top = Math.round((maxHeight - param.height) / 2);
    return param;
}


//function del() {
//    //var img = document.getElementById('imghead');
//    //img.src = "/skins/img/photo_icon.png";
//    //$("#imghead").attr("src", "/skins/img/photo_icon.png");
//    alert("删除");
//    var div = document.getElementById('preview');
//    div.innerHTML = '<img width="99%" height="99%" src="/skins/img/photo_icon.png" id=imghead onclick=$("#previewImg").click()>';
//}


function Cost(id) {
    this.id = id;
    this.delid = "";
    this.delts = true;
}
Cost.prototype.CBIndexLoad = function (page) {
    GetCostIndexTbody(page, '查询');
}
Cost.prototype.CBUpdateLoad = function () {
    var idtop = this.id;
    if (idtop > 0) {
        $.ajax({
            url: "/Cost/CostPList",
            timeout: 0, //超时时间设置，单位毫秒
            type: "post",
            data: { topid: idtop },
            success: function (data1) {
                if (data1 == "NO") {
                    layer.alert("初始化失败！请联系管理员");
                } else {
                    var outHtml = "";
                    var jine = parseFloat(0);//总金额
                    var ZLCB = parseFloat(0);//主料成本
                    var FLCB = parseFloat(0);//辅料成本

                    var jsondata = eval("(" + data1 + ")");
                    if (jsondata.length > 0) {
                        for (var i = 0; i < jsondata.length; i++) {
                            outHtml += "<tr> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["项目"] + "</span></td>";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["供应商"] + "</span></td>";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["物料编号"] + "</span></td>";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["色号"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["物料名称"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["物料规格"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["单位"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["使用部位"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["单件用量"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["损耗(%)"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["单价(元)"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["金额(元)"] + "</span></td> ";
                            outHtml += "</tr>";
                            if (jsondata[i]["项目"] == "面料" || jsondata[i]["项目"] == "配料" || jsondata[i]["项目"] == "里料") {
                                ZLCB += parseFloat(jsondata[i]["金额(元)"]);
                            } else {
                                FLCB += parseFloat(jsondata[i]["金额(元)"]);
                            }
                            jine += parseFloat(jsondata[i]["金额(元)"]);
                        }


                        $("#COST02").val(jsondata[0]["BOM单类型"]);//BOM单类型
                        $("#COST06").val(jsondata[0]["设计款号"]);//设计款号
                        $("#COST07").val(jsondata[0]["颜色"]);//颜色
                        $("#COST01").val(jsondata[0]["BOM单号"]);
                        $("#COST08").val(jsondata[0]["款式描述"]);
                        $(".shang_xiaoji").html(returnFloat(jine));
                        $("#imghead").attr("src", jsondata[0]["图片"])
                        $("#COST09").val(returnFloat(ZLCB));
                        $("#COST10").val(returnFloat(FLCB));
                        $("#COST15").val(jsondata[0]["目标成本"]);
                        $("#BOMHS").html(outHtml);
                    }
                    ShowTable("1");
                }

            },
            error: function () {
                layer.alert("出错了，请联系管理员!");
            }
        });
        $.ajax({
            url: "/Cost/CostP2List",
            timeout: 0, //超时时间设置，单位毫秒
            type: "post",
            data: { topid: idtop },
            success: function (data1) {
                if (data1 == "NO") {
                    layer.alert("初始化失败！请联系管理员");
                } else {
                    var outHtml = "";
                    var jsondata = eval("(" + data1 + ")");
                    if (jsondata.length > 0) {
                        $("#qtTbody").html("");
                        for (var i = 0; i < jsondata.length; i++) {
                            outHtml = "<tr data-id='" + jsondata[i]["id"] + "'> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["OTHER01"] + "</span></td>";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["OTHER02"] + "</span></td>";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["OTHER03"] + "</span></td>";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["OTHER04"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["OTHER05"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["OTHER06"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["OTHER07"] + "</span></td> ";
                            outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["OTHER08"] + "</span></td> ";
                            outHtml += "    <td><div style='width:250px;'><div class='b_bntlan lit fl' onclick='cost.addfy(this)'>新建</div><div class='b_bntlan lit fl' onclick='cost.editfy(this)'>修改</div><div class='b_bntred lit fl' onclick='cost.delOther(this);' data-id='" + jsondata[i]["id"] + "'>删除</div></div></td> ";
                            outHtml += "</tr>";
                            $("#qtTbody").append(outHtml);
                        }
                    }
                    ShowTable("2");

                    var COST11 = 0; //$("#COST11").val(); //加工费
                    var COST12 = 0; //$("#COST12").val(); //二次加工
                    var COST13 = 0; //$("#COST13").val(); //其他费用
                    var COST15 = 0;
                    $("#qtTbody").find("tr").each(function () {
                        if ($(this).find("td:eq(0) span").length > 0) {

                            var xiangmu = $(this).find("td:eq(0) span").html();
                            if (xiangmu == "加工费") {
                                COST11 += parseFloat($(this).find("td:eq(6) span").html());
                            } else if (xiangmu == "二次加工") {
                                COST12 += parseFloat($(this).find("td:eq(6) span").html());
                            } else {
                                COST13 += parseFloat($(this).find("td:eq(6) span").html());
                            }
                        }
                    });


                    $("#COST11").val(returnFloat(COST11));
                    $("#COST12").val(returnFloat(COST12));
                    $("#COST13").val(returnFloat(COST13));
                }
            },
            error: function () {
                layer.alert("出错了，请联系管理员!");
            }
        });

    }
}
//成本核算 颜色下拉
Cost.prototype.selectYS = function () {
    var ddlHtml = "";
    $("#COST07").html("");
    $.ajax({
        url: "/Cost/CostYSList",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        data: {},
        success: function (data) {
            var jsondata = eval("(" + data + ")");
            ddlHtml = "<option selected='true' value=''>请选择</option>";
            for (var i = 0; i < jsondata.length; i++) {
                ddlHtml += "<option value='" + jsondata[i]["SampleMX07"] + "'>" + jsondata[i]["SampleMX07"] + "</option>";
            }
            $("#COST07").html(ddlHtml);
        },
        error: function () {
            layer.alert("出错了，请联系管理员!");
        }
    });
}
//成本核算 BOM列表
Cost.prototype.selectBOMList = function (indexs) {
    var data;
    var ret = false;
    var COST01 = $("#COST01").val();//单号
    var COST02 = $("#COST02").val();//BOM单类型
    var COST06 = $("#COST06").val();//设计款号
    var COST07 = $("#COST07").val();//颜色
    data = { COST01: COST01, COST02: COST02, COST06: COST06, COST07: COST07, indexs: indexs, COST02: COST02, };
    ret = true;

    if (ret == true) {
        ret == false;
        $.ajax({
            url: "/Cost/CostBOMList",
            timeout: 0, //超时时间设置，单位毫秒
            type: "post",
            data: data,
            success: function (data1) {
                var outHtml = "";
                var jine = parseFloat(0);//总金额
                var ZLCB = parseFloat(0);//主料成本
                var FLCB = parseFloat(0);//辅料成本

                var jsondata = eval("(" + data1 + ")");
                if (jsondata.length > 0) {
                    for (var i = 0; i < jsondata.length; i++) {
                        outHtml += "<tr> ";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["项目"] + "</span></td>";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["供应商"] + "</span></td>";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["物料编号"] + "</span></td>";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["色号"] + "</span></td> ";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["物料名称"] + "</span></td> ";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["物料规格"] + "</span></td> ";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["单位"] + "</span></td> ";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["使用部位"] + "</span></td> ";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["单件用量"] + "</span></td> ";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["损耗(%)"] + "</span></td> ";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["单价(元)"] + "</span></td> ";
                        outHtml += "    <td><span class='poducts short_tit f_fl'>" + jsondata[i]["金额(元)"] + "</span></td> ";
                        outHtml += "</tr>";
                        if (jsondata[i]["项目"] == "面料" || jsondata[i]["项目"] == "配料" || jsondata[i]["项目"] == "里料") {
                            ZLCB += parseFloat(jsondata[i]["金额(元)"]);
                        } else {
                            FLCB += parseFloat(jsondata[i]["金额(元)"]);
                        }
                        jine += parseFloat(jsondata[i]["金额(元)"]);
                    }
                    $("#COST01").val(jsondata[0]["BOM单号"]);
                    $("#COST08").val(jsondata[0]["款式描述"]);
                    $(".shang_xiaoji").html(returnFloat(jine));
                    $("#imghead").attr("src", jsondata[0]["图片"])
                    $("#COST09").val(returnFloat(ZLCB));
                    $("#COST10").val(returnFloat(FLCB));
                    $("#BOMHS").html(outHtml);
                    $("#COST06").val(jsondata[0]["设计款号"]);//设计款号
                    $("#COST02").val(jsondata[0]["BOM单类型"]);//BOM单类型
                    $("#COST07").val(jsondata[0]["颜色"]);//颜色
                }
                ShowTable("1");


            },
            error: function () {
                layer.alert("出错了，请联系管理员!");
            }
        });
    }
}

Cost.prototype.Bedit = function (id) {
    if (id.length <= 0) {
        id = 0;
    }
    var fulls = "left=0,screenX=0,top=0,screenY=0,scrollbars=1";    //定义弹出窗口的参数  
    if (window.screen) {
        var ah = screen.availHeight - 30;
        var aw = screen.availWidth - 10;
        fulls += ",height=" + ah;
        fulls += ",innerHeight=" + ah;
        fulls += ",width=" + aw;
        fulls += ",innerWidth=" + aw;
        fulls += ",resizable"
    } else {
        fulls += ",resizable"; // 对于不支持screen属性的浏览器，可以手工进行最大化。 manually  
    }
    var winObj = window.open("/Cost/CostAddUpdate/" + id, "_blank", fulls);//"menubar=no,toolbar=no,status=no,scrollbars=yes,channelmode=yes,fullscreen=yes"
    var loop = setInterval(function () {
        if (winObj.closed) {
            clearInterval(loop);
            window.location.reload();
        }
    }, 1);
}
//成本核算 其他费用列表修改
Cost.prototype.editfy = function (obj) {

    var div_edit = $(obj);
    if (div_edit.html() == "修改") {
        var td_index = div_edit.parents("tr").find("td").length;
        var tdspan_html = "";
        div_edit.parents("tr").find("td").each(function (index) {
            if (index + 1 != td_index && index != 0) {
                tdspan_html = $(this).find("span").html();
                var input = document.createElement('input');
                input.setAttribute('type', 'text');
                input.setAttribute('value', tdspan_html);
                input.style.width = $(this).find("span").width() + "px";
                $(this).html(input);
            } else if (index == 0) {
                tdspan_html = $(this).find("span").html();
                $(this).html(setFun(tdspan_html));
            }
        });
        div_edit.html("确定");
    } else {
        var pp = true;

        var td_index = div_edit.parents("tr").find("td").length;
        var tdinupt_html = "";

        div_edit.parents("tr").find("td").each(function (index) {
            if (index + 1 != td_index && index != 0 && index + 2 != td_index) {
                tdinupt_html = $(this).find("input").val();
            } else if (index == 0) {
                tdinupt_html = $(this).find("select").val();
            }
            if (tdinupt_html == "") {
                pp = false;
                return;
            }

        });

        if (pp) {
            div_edit.parents("tr").find("td").each(function (index) {
                if (index + 1 != td_index && index != 0) {
                    tdinupt_html = $(this).find("input").val();
                    var span = "<span class='poducts short_tit f_fl'>" + tdinupt_html + "</span>";
                    $(this).html(span);
                } else if (index == 0) {
                    tdinupt_html = $(this).find("select").val();
                    var span = "<span class='poducts short_tit f_fl'>" + tdinupt_html + "</span>";
                    $(this).html(span);
                }
            });


            var COST11 = 0; //$("#COST11").val(); //加工费
            var COST12 = 0; //$("#COST12").val(); //二次加工
            var COST13 = 0; //$("#COST13").val(); //其他费用
            div_edit.parents("tbody").find("tr").each(function () {
                if ($(this).find("td:eq(0) span").length > 0) {

                    var xiangmu = $(this).find("td:eq(0) span").html();
                    if (xiangmu == "加工费") {
                        COST11 += parseFloat($(this).find("td:eq(6) span").html());
                    } else if (xiangmu == "二次加工") {
                        COST12 += parseFloat($(this).find("td:eq(6) span").html());
                    } else {
                        COST13 += parseFloat($(this).find("td:eq(6) span").html());
                    }
                }
            });
            $("#COST11").val(returnFloat(COST11));
            $("#COST12").val(returnFloat(COST12));
            $("#COST13").val(returnFloat(COST13));
            div_edit.html("修改");
        } else {
            layer.alert("此行还有数据未填写！");
        }
    }

}

Cost.prototype.delOther = function (obj) {
    if ($(obj).parents("tbody").find("tr").length > 1) {
        if (this.delts) {
            layer.open({
                title: '温馨提示',
                content: '删除行后,请点击右上角保存，才会正式提交！',
                btn: ['确定'],
                yes: function (index, layero) {
                    layer.closeAll();
                }
            });
        }
        this.delts = false;
        this.delid += $(obj).attr("data-id") + ",";
        $(obj).parents("tr").remove();
    } else {
        layer.alert("请保留一行数据！");
    }
}

Cost.prototype.addfy = function (obj) {

    var pp = true;
    if ($(obj).parent().find("div:eq(1)").html() == "确定") {
        layer.alert("此行数据未确认，请确认之后再添加！");
    } else {
        if (pp) {
            var Ytr = ' <tr data-id="0"><td><select id=""><option value="">请选择</option><option value="加工费">加工费</option><option value="二次加工">二次加工</option><option value="其他费用">其他费用</option></select></td><td><input type="text" value="" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td style="width:250px;"><div style="width:250px;"><div class="b_bntlan lit fl" onclick="cost.addfy(this)">新建</div><div class="b_bntlan lit  fl" onclick="cost.editfy(this)">确定</div><div class="b_bntred lit  fl" onclick="cost.delOther(this);" data-id="0">删除</div></div></td></tr>';
            $(obj).parents("tbody").prepend(Ytr);
        } else {
            layer.alert("表格中还有数据未填写！");
        }
    }
}

Cost.prototype.alertTest = function () {
    alert(this.delid);
}

Cost.prototype.subtj = function () {
    var delid = this.delid;
    if (delid.length > 0) {
        delid = delid.substring(0, delid.length - 1)
    }
    var idtop = this.id;
    var COST01 = $("#COST01").val();//BOM单号
    var COST02 = $("#COST02").val();//BOM单类型
    var COST07 = $("#COST07").val();//颜色
    var COST09 = $("#COST09").val();//主料成本
    var COST10 = $("#COST10").val();//辅料成本
    var COST11 = $("#COST11").val();//加工费
    var COST12 = $("#COST12").val();//二次加工
    var COST13 = $("#COST13").val();//其他费用
    var COST14 = $("#COST14").val();//二次加工
    var COST15 = $("#COST15").val();//其他费用
    if (COST01.length <= 0) {
        layer.alert("BOM单号不能为空！");
        return;
    }
    if (COST02.length <= 0) {
        layer.alert("BOM单类型不能为空！");
        return;
    }
    var COSTOTHER = "";
    var lengtd = $("#qtTbody").find("tr:eq(0) td").length;
    var ret = true;
    $("#qtTbody").find("tr").each(function () {
        var qued = $(this).find("td:eq(" + (lengtd - 1) + ") div div:eq(1)").html();
        if (qued == "确定") {
            ret = false;
        }
    });
    if (ret) {
        //子表数据循环开始
        $("#qtTbody").find("tr").each(function (index) {
            COSTOTHER += $(this).attr("data-id") + ",";
            $(this).find("td").each(function (index1) {
                if (index1 != lengtd - 1)
                    COSTOTHER += $(this).find("span").html() + ",";
            })
            COSTOTHER = COSTOTHER.substring(0, COSTOTHER.length - 1) + "|";
        });
        if (COSTOTHER.length > 0)
            COSTOTHER = COSTOTHER.substring(0, COSTOTHER.length - 1);
        //子表数据循环结束
        CostSubtjajax(idtop, delid, COST01, COST02, COST07, COST09, COST10, COST11, COST12, COST13, COST14, COST15, COSTOTHER);
    } else {
        layer.open({
            title: '温馨提示',
            content: '项目费用有未确定项目,提交将不会保存,是否继续？',
            btn: ['确定', '取消'],
            yes: function (index, layero) {
                //子表数据循环开始
                $("#qtTbody").find("tr").each(function () {
                    var qued = $(this).find("td:eq(" + (lengtd - 1) + ") div div:eq(1)").html();
                    if (qued == "修改") {
                        COSTOTHER += $(this).attr("data-id") + ",";
                        $(this).find("td").each(function (index1) {
                            if (index1 != lengtd - 1)
                                COSTOTHER += $(this).find("span").html() + ",";
                        })
                        COSTOTHER = COSTOTHER.substring(0, COSTOTHER.length - 1) + "|";
                    }
                });
                if (COSTOTHER.length > 0)
                    COSTOTHER = COSTOTHER.substring(0, COSTOTHER.length - 1);
                //子表数据循环结束
                CostSubtjajax(idtop, delid, COST01, COST02, COST07, COST09, COST10, COST11, COST12, COST13, COST14, COST15, COSTOTHER);


            }
        });
    }


}

function CostSubtjajax(id, delid, COST01, COST02, COST07, COST09, COST10, COST11, COST12, COST13, COST14, COST15, COSTOTHER) {
    $.ajax({
        url: "/Cost/Costedit",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        data: { id: id, delid: delid, COST01: COST01, COST02: COST02, COST07: COST07, COST09: COST09, COST10: COST10, COST11: COST11, COST12: COST12, COST13: COST13, COST14: COST14, COST15: COST15, COSTOTHER: COSTOTHER },
        success: function (data) {
            if (data == "OK") {
                layer.open({
                    title: '提示',
                    content: '操作成功！',
                    btn: ['确定'],
                    yes: function (index, layero) {
                        window.close();
                    }
                });
            } else if (data == "NO") {
                layer.alert("操作失败！");
            } else {
                layer.alert("没有权限！");
            }
        },
        error: function () {
            layer.alert("出错了，请联系管理员!");
        }
    });
}

function returnFloat(value) {
    var value = Math.round(parseFloat(value) * 100) / 100;
    var xsd = value.toString().split(".");
    if (xsd.length == 1) {
        value = value.toString() + ".00";
        return value;
    }
    if (xsd.length > 1) {
        if (xsd[1].length < 2) {
            value = value.toString() + "0";
        }
        return value;
    }
}

function setFun(text) {
    var id = new Array("加工费", "二次加工", "其他费用");
    var value = new Array("加工费", "二次加工", "其他费用");
    var mySelect = document.createElement("select");
    mySelect.add(new Option("请选择", ""));
    mySelect.add(new Option("加工费", "加工费"));
    mySelect.add(new Option("二次加工", "二次加工"));
    mySelect.add(new Option("其他费用", "其他费用"));
    mySelect.value = text;
    return mySelect;
}

function JSCB() {
    var COST09 = $("#COST09").val();
    var COST10 = $("#COST10").val();
    var COST11 = $("#COST11").val();
    var COST12 = $("#COST12").val();
    var COST13 = $("#COST13").val();
    $("#COST14").val(returnFloat(parseFloat(COST09) + parseFloat(COST10) + parseFloat(COST11) + parseFloat(COST12) + parseFloat(COST13)));
}

