

//绑定工厂明细
function GetSupplierAddUpTable(id) {
    $.ajax({
        url: "/Supplier/GetGYSGCGLMXIndex/" + id,
        type: "post",
        data: {},
        success: function (data) {
            if (data == "" || data == "[]") {
                var tr = '<tr class="new_input">';
                tr += ' <td><span values="0" class="poducts short_tit f_fl"></span><input type="text" name=""/></td>';
                tr += '  <td><input type="text" name=""/></td>';
                tr += ' <td><input type="text" name=""/> </td>';
                tr += ' <td><input type="text" name=""/></td>';
                tr += ' <td><input type="text" name=""/></td>';
                tr += ' <td><input type="text" name=""/></td>';
                tr += '<td class="td_bnt1"> <div  onclick="addtr()"  class="xj_bnt xj_bnt1" > <span class="bnt_span1"><img class="bnt_img" src="/skins/img/xj.png" style=""></span>新建 </div><div class="xj_bnt xj_bnt2" onclick="NotDel()"> <i class="ace-icon fa fa-trash-o bigger-120 orange"></i>删除 </div></td>';
                tr += '</tr>'
                $(".supplier_minx tbody").append(tr);
            }
            else {
                var dt = eval("(" + data + ")");
                $("#MXTB").html("");
                tableHtml = "";
                for (var i = 0; i < dt.length; i++) {
                    tableHtml += "<tr>";
                    tableHtml += "<td><span values=" + dt[i].id + " class='poducts short_tit f_fl'>" + dt[i].GYSGCMX01 + "</span><input class='Supplier_inp' type='text' name='' value='" + dt[i].GYSGCMX01 + " '/></td>";
                    tableHtml += "<td><span class='poducts short_tit f_fl'>" + dt[i].GYSGCMX02 + "</span><input class='Supplier_inp' type='text' name='' value='" + dt[i].GYSGCMX02 + " '/></td>";
                    tableHtml += "<td><span class='poducts short_tit f_fl'>" + dt[i].GYSGCMX03 + "</span><input class='Supplier_inp' type='text' name='' value='" + dt[i].GYSGCMX03 + " '/></td>";
                    tableHtml += "<td><span class='poducts short_tit f_fl'>" + dt[i].GYSGCMX04 + "</span><input class='Supplier_inp' type='text' name='' value='" + dt[i].GYSGCMX04 + " '/></td>";
                    tableHtml += "<td><span class='poducts short_tit f_fl'>" + dt[i].GYSGCMX05 + "</span><input class='Supplier_inp' type='text' name='' value='" + dt[i].GYSGCMX05 + " '/></td>";
                    tableHtml += "<td><span class='poducts short_tit f_fl'>" + dt[i].GYSGCMX06 + "</span><input class='Supplier_inp' type='text' name='' value='" + dt[i].GYSGCMX06 + " '/></td>";
                    tableHtml += "<td class='td_bnt1'><div class='xj_bnt xj_bnt1' onclick='addtr()'> <span class='bnt_span1'><img style='' src='/skins/img/xj.png' class='bnt_img'></span>新建 </div><div onclick='BtnDel(" + dt[i].id + ")' class='xj_bnt xj_bnt2'> <i class='ace-icon fa fa-trash-o bigger-120 orange'></i>删除 </div></td>";
                    tableHtml += "</tr>";
                }
                $("#MXTB").append(tableHtml);
            }
        },
        error: function () {
            layer.alert("服务器连接超时");
        }
    })
}

//绑定选中"企业形态"单选框ace
function BindRadioQyC(qy) {
    //var qy="@ViewBag.GYSGC28";
    $(".radio").find("input[type=radio]").each(function () {
        if ($(this).val() == qy) {
            $(this).attr("checked", true);
        }
    })
}

//绑定选中"业务种类"复选框ace
function BindYwChk(zl) {
    //var zl= "@ViewBag.GYSGC30";
    var str = zl.split(",");

    $(".checkbox").find("input[type=checkbox]").each(function () {
        for (var j = 0; j < str.length; j++) {
            //console.log($(this).val() +"|"+ str[j])
            //对比传过来的值选中复选框
            if ($(this).val() == str[j]) {
                $(this).attr("checked", true);
            }
        }
    });
}

//删除工厂明细的方法
function BtnDel(obj) {
    layer.open({
        title: '消息',
        content: '这是正式数据，您确定要继续删除吗?',
        btn: ['确定', '取消'],
        yes: function (index, layero) {
            $.get("/Supplier/DelFactoryMX", { id: obj }, function (data) {
                if (data == "success") {
                    layer.alert("删除成功");
                    window.location.reload();
                }
                else if (data == "Operation") {
                    layer.alert("您没有权限请和管理员联系");
                }
                else {
                    layer.alert("删除失败");
                }
            });
        }
    })
}

//编辑的方法
function EditFactory(Eid) {
    var MXArr = "";//供应商商品明细数据
    var MXid = "";//供应商商品明细ID
    var MXnum = 0;//数据条数
    var qyForm = "";//选中的企业形态的值
    var ywType = "";//选中的业务种类值

    //获取产品明细表数据用于添加和编辑
    $("#MXTB").find("tr").each(function (index) {
        var GYSGCMX01 = $(this).find("td").eq(0).find("input").val();//主要产品
        var GYSGCMX02 = $(this).find("td").eq(1).find("input").val();//产品促销
        var GYSGCMX03 = $(this).find("td").eq(2).find("input").val();//价格范围
        var GYSGCMX04 = $(this).find("td").eq(3).find("input").val();//年产量
        var GYSGCMX05 = $(this).find("td").eq(4).find("input").val();//占总产量的比例
        var GYSGCMX06 = $(this).find("td").eq(5).find("input").val();//主要销售对象及地区
        var str = GYSGCMX01 + "," + GYSGCMX02 + "," + GYSGCMX03 + "," + GYSGCMX04 + "," + GYSGCMX05 + "," + GYSGCMX06;
        //console.log(GYSGCMX01 + "," + GYSGCMX02 + "," + GYSGCMX03 + "," + GYSGCMX04 + "," + GYSGCMX05 + "," + GYSGCMX06);
        var id = $(this).find("td").eq(0).find("span").attr("values");

        MXid += id + ",";//供应商商品明细ID
        MXArr += str + "|";//供应商商品明细数据
        MXnum = index + 1;//数据条数
    })
    //console.log(MXArr);
    //console.log(MXid); return;

    //获取单选框的值用于编辑和添加
    var obj = document.getElementsByName('form-field-radio');
    for (var i = 0; i < obj.length; i++) {
        if (obj[i].checked)
            qyForm = obj[i].value;
    }
    //获取复选框的值用于编辑和添加
    var str = "";
    var obj = document.getElementsByName('form-field-checkbox');
    for (var i = 0; i < obj.length; i++) {
        if (obj[i].checked) {
            str += obj[i].value + ",";
        }
    }
    ywType = str.substring(0, str.length - 1);//选中的业务种类值

    //判断是否选中
    if (ywType == "") {
        layer.alert("请选择业务种类");
        return false;
    }
    if (qyForm == "") {
        layer.alert("请选择企业形态");
        return false;
    }
    //非空判断
    //01
    if ($("#GYSGC01").val() == "请选择") { layer.alert("请选择面/辅料!"); }
    else if ($("#GYSGC03").val() == "") { layer.alert("请输入加工厂名称!"); }
    else if ($("#GYSGC50").val() == "") { layer.alert("请输入供应商代码!"); }
    else if ($("#GYSGC04").val() == "请选择") { layer.alert("请选择经营性质!"); }
    //else if ($("#GYSGC05").val() == "请选择") { layer.alert("请选择开发能力!"); }
    //else if ($("#GYSGC06").val() == "请选择") { layer.alert("请选择生产能力!"); }
    //else if ($("#GYSGC07").val() == "请选择") { layer.alert("请选择质量管控能力!"); }
    else if ($("#GYSGC08").val() == "请选择") { layer.alert("请选择配合度!"); }
    else if ($("#GYSGC09").val() == "请选择") { layer.alert("请选择价位!"); }
    //else if ($("#GYSGC10").val() == "") { layer.alert("请输入付款方式!"); }
    else if ($("#GYSGC11").val() == "请选择") { layer.alert("请选择是否与品牌目标合作!"); }
    else if ($("#GYSGC12").val() == "请选择") { layer.alert("请选择采购部初步合作定位!"); }
        //13-19
    //else if ($("#GYSGC20").val() == "") { layer.alert("请输入评定等级!"); }
    //else if ($("#GYSGC21").val() == "") { layer.alert("请输入地址!"); }
    //else if ($("#GYSGC22").val() == "") { layer.alert("请输入法人代表!"); }
    //else if ($("#GYSGC23").val() == "") { layer.alert("请输入法人代表职务!"); }
    //else if ($("#GYSGC24").val() == "") { layer.alert("请输入法人代表电话!"); }
    //else if ($("#GYSGC25").val() == "") { layer.alert("请输入业务代表!"); }
    //else if ($("#GYSGC26").val() == "") { layer.alert("请输入业务代表职务!"); }
    //else if ($("#GYSGC27").val() == "") { layer.alert("请输入业务代表电话!"); }
        //28
    //else if ($("#GYSGC29").val() == "") { layer.alert("请输入注册资本!"); }
        //30
    //else if ($("#GYSGC31").val() == "") { layer.alert("请输入厂房面积!"); }
    //else if ($("#GYSGC32").val() == "") { layer.alert("请输入人员规模!"); }
    //else if ($("#GYSGC33").val() == "") { layer.alert("请输入加工所占比例!"); }
    //else if ($("#GYSGC34").val() == "") { layer.alert("请输入自营经销所占比例!"); }
    //else if ($("#GYSGC35").val() == "") { layer.alert("请输入通过何种认证!"); }
    //else if ($("#GYSGC36").val() == "") { layer.alert("请输入年总产量!"); }
    //else if ($("#GYSGC37").val() == "") { layer.alert("请输入检验标准!"); }
    //else if ($("#GYSGC38").val() == "") { layer.alert("请输入所用染化料!"); }
    //else if ($("#GYSGC39").val() == "") { layer.alert("请输入国内主要合作品牌!"); }
    //else if ($("#GYSGC40").val() == "") { layer.alert("请输入国际主要合作品牌!"); }
    //else if ($("#GYSGC41").val() == "") { layer.alert("请输入打色周期!"); }
    //else if ($("#GYSGC42").val() == "") { layer.alert("请输入放样周期FOB!"); }
    //else if ($("#GYSGC43").val() == "") { layer.alert("请输入放样周期CMT!"); }
    //else if ($("#GYSGC44").val() == "") { layer.alert("请输入大货生产周期FOB!"); }
    //else if ($("#GYSGC45").val() == "") { layer.alert("请输入大货生产周期CMT!"); }
        //else if ($("#GYSGC46").val() == "") { layer.alert("请输入纱/胚布品种及产地!"); }
    //else if ($("#GYSGC47").val() == "") { layer.alert("请输入生产设备品种及数量!"); }
    //else if ($("#GYSGC48").val() == "") { layer.alert("请输入综合评估!"); }
    //正则验证
    else if (!(/^1[3|4|5|7|8][0-9]\d{4,8}$/.test($("#GYSGC24").val())) && !(/^0\d{2,3}-?\d{7,8}$/.test($("#GYSGC24").val())) && $("#GYSGC24").val() != "") {
        layer.alert("请输入正确的法人代表电话格式!");
    }
    else if (!(/^1[3|4|5|7|8][0-9]\d{4,8}$/.test($("#GYSGC27").val())) && !(/^0\d{2,3}-?\d{7,8}$/.test($("#GYSGC27").val())) && $("#GYSGC27").val() != "") {
        layer.alert("请输入正确的业务代表电话格式!");
    }
    //else if (!(/^[0-9]*$/.test($("#GYSGC32").val()))) {
    //    layer.alert("人员规模格式不正确!");
    //}
    //else if (!(/^\d+(\.\d+)?$/.test($("#GYSGC41").val()))) {
    //    layer.alert("打色周期格式不正确!");
    //}
    //else if (!(/^\d+(\.\d+)?$/.test($("#GYSGC42").val()))) {
    //    layer.alert("放样周期FOB格式不正确!");
    //}
    //else if (!(/^\d+(\.\d+)?$/.test($("#GYSGC43").val()))) {
    //    layer.alert("放样周期CMT格式不正确!");
    //}
    //else if (!(/^\d+(\.\d+)?$/.test($("#GYSGC44").val()))) {
    //    layer.alert("大货生产周期FOB格式不正确!");
    //}
    //else if (!(/^\d+(\.\d+)?$/.test($("#GYSGC45").val()))) {
    //    layer.alert("大货生产周期CMT格式不正确!");
    //}
    else if (!(/^\d+(\.\d+)?$/.test($("#GYSGC33").val())) || $("#GYSGC33").val() > 5) {
        layer.alert("加工所占比例格式不正确!");
    }
    else if (!(/^\d+(\.\d+)?$/.test($("#GYSGC34").val())) || $("#GYSGC34").val() > 5) {
        layer.alert("自营经销所占比例格式不正确!");
    }
    else {
        //数据传到后台编辑操作
        $.ajax({
            url: "/Supplier/EditFactoryInfo",
            type: "post",
            data:
                {
                    id: Eid,
                    //GYSGC01: $("#GYSGC01").val(),
                    GYSGC02: $("#GYSGC02").val(),
                    GYSGC03: $("#GYSGC03").val(),
                    GYSGC04: $("#GYSGC04").val(),
                    GYSGC05: $("#GYSGC05").val(),
                    GYSGC06: $("#GYSGC06").val(),
                    GYSGC07: $("#GYSGC07").val(),
                    GYSGC08: $("#GYSGC08").val(),
                    GYSGC09: $("#GYSGC09").val(),
                    GYSGC10: $("#GYSGC10").val(),
                    GYSGC11: $("#GYSGC11").val(),
                    GYSGC12: $("#GYSGC12").val(),
                    //13-19
                    GYSGC20: $("#GYSGC20").val(),
                    GYSGC21: $("#GYSGC21").val(),
                    GYSGC22: $("#GYSGC22").val(),
                    GYSGC23: $("#GYSGC23").val(),
                    GYSGC24: $("#GYSGC24").val(),
                    GYSGC25: $("#GYSGC25").val(),
                    GYSGC26: $("#GYSGC26").val(),
                    GYSGC27: $("#GYSGC27").val(),
                    GYSGC28: qyForm,
                    GYSGC29: $("#GYSGC29").val(),
                    GYSGC30: ywType,
                    GYSGC31: $("#GYSGC31").val(),
                    GYSGC32: $("#GYSGC32").val(),
                    GYSGC33: $("#GYSGC33").val(),
                    GYSGC34: $("#GYSGC34").val(),
                    GYSGC35: $("#GYSGC35").val(),
                    GYSGC36: $("#GYSGC36").val(),
                    GYSGC37: $("#GYSGC37").val(),
                    GYSGC38: $("#GYSGC38").val(),
                    GYSGC39: $("#GYSGC39").val(),
                    GYSGC40: $("#GYSGC40").val(),
                    GYSGC41: $("#GYSGC41").val(),
                    GYSGC42: $("#GYSGC42").val(),
                    GYSGC43: $("#GYSGC43").val(),
                    GYSGC44: $("#GYSGC44").val(),
                    GYSGC45: $("#GYSGC45").val(),
                    //GYSGC46: $("#GYSGC46").val(),
                    GYSGC47: $("#GYSGC47").val(),
                    GYSGC48: $("#supplier_textarea").val(),
                    GYSGC50: $("#GYSGC50").val(),

                    //供应商产品明细数据
                    MXArr: MXArr.substring(0, MXArr.length - 1),
                    MXid: MXid.substring(0, MXid.length - 1),//ID
                    MXnum: MXnum,//数据条数
                },
            success: function (data) {
                if (data == "MessageZY") {
                    layer.alert("请输入主要产品");
                }
                else if (data == "MessageGG") {
                    layer.alert("请输入产品规格");
                }
                else if (data == "MessageJG") {
                    layer.alert("请输入价格范围");
                }
                else if (data == "MessageNCL") {
                    layer.alert("请输入年产量");
                }
                else if (data == "MessageBL") {
                    layer.alert("请输入占总产量的比例%");
                }
                else if (data == "MessageDX") {
                    layer.alert("请输入主要销售对象及地区");
                }
                else if (data == "success") {
                    layer.open({
                        title: '消息',
                        content: '提交成功!',
                        btn: ['确定'],
                        yes: function (index, layero) {
                            window.history.back(-1);//返回上一级
                        }
                    });
                } else if (data == "Operation") {
                    layer.alert("您没有权限请和管理员联系");
                }
                else if (data == "Repeat") {
                    layer.alert("供应商代码重复，请重新输入!");
                }
                else {
                    layer.alert("提交失败");
                }
            },
            error: function () {
                layer.alert("服务器连接超时");
            }
        })
    }

}

$(function () {
    //编辑工厂明细点击table显示文本框编辑
    $('.supplier_minx').delegate('span', 'click', function () {

        $(this).hide();
        if ($(this).next("input").hasClass("Supplier_inp")) {

            $(this).next("input").removeClass("Supplier_inp");

        } else {
            $(this).next("input").addClass("Supplier_inp");

        }
    });

});