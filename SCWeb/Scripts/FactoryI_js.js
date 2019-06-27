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


//获取table的值并绑定
function GetSupplierTable(page, ShowPageCount) {
    layer.msg('加载中......', {
        icon: 16,
        shade: 0.01
    });
    $.ajax({
        url: "/Supplier/GetFactoryIndex",
        type: "post",
        async: false,
        data:
            {
                page: page,
                ShowPageCount: ShowPageCount,
                GYSGC01: "0",
                GYSGC02: $("#pinpai").find("span").attr("values"),
                GYSGC03: $("#GYSGC03").val(),
                GYSGC04: $("#jiagong").find("span").attr("values"),
            },

        success: function (data) {
            if (data == "error") {
                layer.alert("不存在数据哦!");
            }
            else {
                $("#pageNow").html(page);
                var dt = eval("(" + data + ")");
                var tableHtml = "";
                $("#gysTB").html("");
                if (dt.length>0) {
                    for (var i = 0; i < dt.length; i++) {
                        tableHtml += "<tr>";
                        tableHtml += "<td values=" + dt[i].id + " style='width: 35px;' class='td1'><div class='nsw_check_box'><span class='ck_box'><span class='dn'><input type='checkbox' name='chkItem' value='" + dt[i].id + "'></span></span></div></td>";

                        tableHtml += "<td style='width: 90px;'><span class='poducts short_tit f_fl tdtop'><a href='##' class='update_Title' >" + dt[i].GYSGC02 + "</a><i class='e_edi1 e_more_edit popUp' style='display: none;' onclick='btnEdit(" + dt[i].id + ")'></i></span></td>";
                        tableHtml += "<td style='width: 150px;'><span class='poducts short_tit f_fl'>" + dt[i].GYSGC50 + "</span></td>";
                        tableHtml += "<td style='width: 150px;'><span class='poducts short_tit f_fl'>" + dt[i].GYSGC03 + "</span></td>";
                        tableHtml += "<td style='width: 90px;'><span class='poducts short_tit f_fl'>" + dt[i].GYSGC04 + "</span></td>";
                        tableHtml += "<td style='width: 90px;'><span class='poducts short_tit f_fl'>" + dt[i].GYSGC05 + "</span></td>";
                        tableHtml += "<td style='width: 90px;'><span class='poducts short_tit f_fl'>" + dt[i].GYSGC06 + "</span></td>";
                        tableHtml += "<td style='width: 90px;'><span class='poducts short_tit f_fl'>" + dt[i].GYSGC07 + "</span></td>";
                        tableHtml += "<td style='width: 150px;'><span class='poducts short_tit f_fl'>" + dt[i].GYSGC08 + "</span></td>";
                        tableHtml += "<td style='width: 60px;'><span class='poducts short_tit f_fl'>" + dt[i].GYSGC09 + "</span></td>";
                        tableHtml += "<td style='width: 270px;'><span class='poducts short_tit f_fl'>" + dt[i].GYSGC10 + "</span></td>";
                        tableHtml += "<td style='width: 120px;'><span class='poducts short_tit f_fl'>" + dt[i].GYSGC11 + "</span></td>";
                        tableHtml += "<td style='width: 120px;'><span class='poducts short_tit f_fl'>" + dt[i].GYSGC12 + "</span></td>";
                        tableHtml += "<td class='table_yj' data-id='1' style='width: 100px;'><span class='frl_span'>" + dt[i].GYSGC13 + "</span><span style='display:none;'><select class='frl_select'><option value='强'> 强 </option><option value='中'> 中 </option><option value='弱'> 弱 </option></select></span></td>";
                        tableHtml += "<td class='table_yj' data-id='1' style='width: 100px;'><span class='frl_span'>" + dt[i].GYSGC14 + "</span><span style='display:none;'><select class='frl_select'><option value='强'> 强 </option><option value='中'> 中 </option><option value='弱'> 弱 </option></select></span></td>";
                        tableHtml += "<td style='width: 100px;' data-id='1' class='table_yj'><span class='frl_span'>" + dt[i].GYSGC15 + "</span><span style='display:none;'><select class='frl_select'><option value='是'> 是 </option><option value='否'> 否 </option></select></span></td>";
                        tableHtml += "<td class='table_yj' data-id='2' style='width: 100px;'><span class='frl_span'>" + dt[i].GYSGC16 + "</span><span style='display:none;'><select class='frl_select'><option value='强'> 强 </option><option value='中'> 中 </option><option value='弱'> 弱 </option></select></span></td>";
                        tableHtml += "<td class='table_yj' data-id='2' style='width: 100px;'><span class='frl_span'>" + dt[i].GYSGC17 + "</span><span style='display:none;'><select class='frl_select'><option value='强'> 强 </option><option value='中'> 中 </option><option value='弱'> 弱 </option></select></span></td>";
                        tableHtml += "<td style='width: 100px;' data-id='2' class='table_yj'><span class='frl_span'>" + dt[i].GYSGC18 + "</span><span style='display:none;'><select class='frl_select'><option value='是'> 是 </option><option value='否'> 否 </option></select></span></td>";
                        tableHtml += "<td style='width: 120px;' data-id='3' class='table_yj'><span class='frl_span'>" + dt[i].GYSGC19 + "</span><span style='display:none;'><select class='frl_select'><option value='是'> 是 </option><option value='否'> 否 </option></select></span></td>";
                        tableHtml += "<td class='nsw_cnt_area action_tx'><a onclick='btnEdit(" + dt[i].id + ")'><span class='pro_view pro_edit'><i></i>编辑</span></a></td>";
                        tableHtml += "</tr>";
                    }

                    $("#Crows").html(dt[0].RowsCount);
                    $("#Cpage").html(dt[0].PageCount);
                }
                else {
                    $("#Crows").html(0);
                    $("#Cpage").html(0);
                    $("#pageNow").html(0);
                }
                $("#gysTB").append(tableHtml);
                var Cpage = $("#Cpage").html();
                GetDDLPage(Cpage, page);
                layer.closeAll();
                ShowTable("1");
            }
        },
        error: function () {
            layer.alert("服务器连接超时");
        }
    })

}


function ShowTable(ee) {
    if (ee == "1") {

        $('#myTable01').fixedHeaderTable({ footer: false, cloneHeadToFoot: true, altClass: 'odd', autoShow: false });
        $('#myTable01').fixedHeaderTable('show');
    } else if (ee == "2") {

        $('#myTable07').fixedHeaderTable({ footer: false, cloneHeadToFoot: true, altClass: 'odd', autoShow: false });
        $('#myTable07').fixedHeaderTable('show');
    } else {

        if (ee == "3") {

            $('#myTable08').fixedHeaderTable({ footer: false, altClass: 'odd' });
            $('#myTable08').fixedHeaderTable('show');
        }
    }
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


//绑定意见下拉框
function GetDDLOP() {
    $("#gysTB").find("tr").each(function (index) {

        //var thArr = $(this).children();
        var GYSGC13 = $(this).find("td").eq(13).find("span").eq(0).html();
        var GYSGC14 = $(this).find("td").eq(14).find("span").eq(0).html();
        var GYSGC15 = $(this).find("td").eq(15).find("span").eq(0).html();
        var GYSGC16 = $(this).find("td").eq(16).find("span").eq(0).html();
        var GYSGC17 = $(this).find("td").eq(17).find("span").eq(0).html();
        var GYSGC18 = $(this).find("td").eq(18).find("span").eq(0).html();
        var GYSGC19 = $(this).find("td").eq(19).find("span").eq(0).html();
        //下拉框绑定值
        $(this).find("td").eq(13).find("select").val(GYSGC13);
        $(this).find("td").eq(14).find("select").val(GYSGC14);
        $(this).find("td").eq(15).find("select").val(GYSGC15);
        $(this).find("td").eq(16).find("select").val(GYSGC16);
        $(this).find("td").eq(17).find("select").val(GYSGC17);
        $(this).find("td").eq(18).find("select").val(GYSGC18);
        $(this).find("td").eq(19).find("select").val(GYSGC19);

    })
}

//修改意见后重新绑定意见'span'
function GetSpanByDDLOP() {
    $("#gysTB").find("tr").each(function (index) {

        //获取下拉框绑定值
        //var thArr = $(this).children();
        var GYSGC13 = $(this).find("td").eq(13).find("select").val();
        var GYSGC14 = $(this).find("td").eq(14).find("select").val();
        var GYSGC15 = $(this).find("td").eq(15).find("select").val();
        var GYSGC16 = $(this).find("td").eq(16).find("select").val();
        var GYSGC17 = $(this).find("td").eq(17).find("select").val();
        var GYSGC18 = $(this).find("td").eq(18).find("select").val();
        var GYSGC19 = $(this).find("td").eq(19).find("select").val();

        //将值付给Span
        $(this).find("td").eq(13).find("span").eq(0).html(GYSGC13);
        $(this).find("td").eq(14).find("span").eq(0).html(GYSGC14);
        $(this).find("td").eq(15).find("span").eq(0).html(GYSGC15);
        $(this).find("td").eq(16).find("span").eq(0).html(GYSGC16);
        $(this).find("td").eq(17).find("span").eq(0).html(GYSGC17);
        $(this).find("td").eq(18).find("span").eq(0).html(GYSGC18);
        $(this).find("td").eq(19).find("span").eq(0).html(GYSGC19);

    })
}

//修改意见
function EditOpinion() {
    var tableL = $("#gysTB").find("tr").length - 1;
    //var str = "";//用于存放表数据
    //var index = 0;
    $("#gysTB").find("tr").each(function (index) {

        var GYSGC13 = $(this).find("td").eq(13).find("select").val();//项目意见配合度
        var GYSGC14 = $(this).find("td").eq(14).find("select").val();//项目意见开发能力
        var GYSGC15 = $(this).find("td").eq(15).find("select").val();//项目意见是否可配合
        var GYSGC16 = $(this).find("td").eq(16).find("select").val();//生管意见配合度
        var GYSGC17 = $(this).find("td").eq(17).find("select").val();//生管意见货期
        var GYSGC18 = $(this).find("td").eq(18).find("select").val();//生管意见是否可配合
        var GYSGC19 = $(this).find("td").eq(19).find("select").val();//综合评定是否合作
        var str = GYSGC13 + "," + GYSGC14 + "," + GYSGC15 + "," + GYSGC16 + "," + GYSGC17 + "," + GYSGC18 + "," + GYSGC19;
        var ID = $(this).find("td").eq(0).attr("values");

        //alert(idd); return;
        //console.log(ID); return;
        $.ajax({
            url: "/Supplier/UpFactoryOpinion",
            type: "post",
            async: false,//设置成同步
            data:
                {
                    id: ID,
                    str: str,
                },
            success: function (data) {
                if (index == tableL) {
                    if (data == "success") {
                        GetSpanByDDLOP();
                        layer.alert("意见修改成功!");

                    } else if (data == "Operation") {
                        layer.alert("您没有权限请和管理员联系");
                    }
                    else {
                        layer.alert("系统故障，请联系信息部!");
                    }
                }
            },
            error: function () {
                layer.alert("服务器连接超时");
            }
        })
    })

}

//编辑前绑定数据
function btnEdit(id) {
    location.href = "/Supplier/FactoryAddUpdate/" + id;
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

$(function () {

    //获取值绑定产品类别下拉框
    $.get("/Supplier/GetDDLType", { LBid: "3" }, function (data) {
        var ddlType = "";
        var dt = eval("(" + data + ")");
        for (var i = 0; i < dt.length; i++) {
            ddlType += "<li><a values=" + dt[i]["id"] + ">" + dt[i]["GYSGCTypeName"] + "</a></li>";
        }
        $("#GYSGC02").append(ddlType);
    })

    //上一页
    $("#BackPage").click(function () {
        var page = parseInt($("#pageNow").html());
        var ShowPageCount = $("#pageList_btn").val();
        if (page > 1) {
            page = page - 1;
            GetSupplierTable(page, ShowPageCount);
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
            GetSupplierTable(page, ShowPageCount);
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
        GetSupplierTable(page, ShowPageCount);
    })

    //加载显示数量
    $("#pageList_btn").change(function () {
        var page = $("#pageNow").html();
        var ShowPageCount = $(this).val();
        GetSupplierTable(page, ShowPageCount);
    })

    //删除
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
                    $.get("/Supplier/DelFactory", { id: ckval }, function (data) {
                        if (data == "success") {
                            layer.open({
                                title: '消息',
                                content: '删除成功!',
                                btn: ['确定'],
                                yes: function (index, layero) {
                                    window.location.reload();//刷新页面
                                }
                            });
                        } else if (data == "Operation") {
                            layer.alert("您没有权限请和管理员联系");
                        }
                        else {
                            layer.alert("删除失败");
                        }
                    });
                }
            });

        }
    })

   
})





