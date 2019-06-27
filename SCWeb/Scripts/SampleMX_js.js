

//获取样衣Bom明细table并绑定
function GetSampleMXTable(obj) {
    layer.msg('加载中......', {
        icon: 16,
        shade: 0.01
    });
    $.ajax({
        url: "/Bom/GetBomMXIndex",
        type: "post",
        async: false,
        data: { id: obj },
        success: function (data) {
            if (data == "" || data == "[]") {
                var tr = "<tr values='0' data-dd='true'>";
                tr += "    <td><select id=''><option value=''>请选择</option><option value='面料'>面料</option><option value='配料'>配料</option><option value='里料'>里料</option><option value='一般辅料'>一般辅料</option><option value='包装辅料'>包装辅料</option><option value='工艺工序'>工艺工序</option></select></td>";
                tr += "    <td><select id=''><option value=''>请选择</option><option value='A1'>A1</option><option value='B1'>B1</option><option value='C1'>C1</option></select></td>";
                tr += "    <td class='Sample_finput_3' data-lie='3' style='width:100%;'><input type='text' name='' class='div_dblclick'/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td class='Sample_finput_4' data-lie='4' style='width:100%;'><input type='text' name='' class='div_dblclick'/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td class='Sample_finput_0' data-lie='0'><input type='text' name='' class='div_dblclick'/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td class='Sample_finput_2' data-lie='2'><input type='text' name='' class='div_dblclick'/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td class='Sample_finput_12' data-lie='12'><input type='text' name='' class='div_dblclick'/></td>";
                tr += "    <td><input type='text' name='' value='0'/></td>";
                tr += "    <td><input type='text' name='' value='0'/></td>";
                tr += "    <td><input type='text' name='' value='0'/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "<td style='width:250px;'><div style='width:250px;'><div class='b_bntlan lit fl' onclick='cost.addfy(this)'>新建</div><div class='b_bntlan lit  fl' onclick='cost.editfy(this)'>确定</div><div class='b_bntred lit  fl' onclick='cost.delOther(this);' data-id='0'>删除</div></div></td>";
                tr += "</tr>";
                $("#bomMXTB").append(tr);
                layer.closeAll();
            }
            else {
                if (data == "error") {
                    layer.alert("数据出错,请联系信息部");
                }
                else {
                    var dt = eval("(" + data + ")");
                    var tableHtml = "";
                    $("#bomMXTB").html("");
                    if (dt.length > 0) {
                        for (var i = 0; i < dt.length; i++) {
                            tableHtml += "<tr values=" + dt[i].id + " data-dd='true'>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl' style='width:100px'>" + dt[i].SampleMX01 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl' style='width:100px'>" + dt[i].SampleMX02 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl' style='width:100%'>" + dt[i].SampleMX03 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX04 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl' style='width:100%'>" + dt[i].SampleMX05 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX06 + "</span></td>";
                            tableHtml += "    <td ><span class='poducts short_tit f_fl'>" + dt[i].SampleMX07 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX08 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl' style='width:80px'>" + dt[i].SampleMX09 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX10 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX11 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX12 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX13 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX14 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX15 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX16 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl' style='width:100px'>" + dt[i].SampleMX17 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl' style='width:120px'>" + dt[i].SampleMX18 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX19 + "</span></td>";
                            tableHtml += "<td style='width:250px;'><div style='width:250px;'><div class='b_bntlan lit fl' onclick='cost.addfy(this)'>新建</div><div class='b_bntlan lit  fl' onclick='cost.editfy(this)'>修改</div><div class='b_bntred lit  fl' onclick='cost.delOther(this);' data-id='0'>删除</div></div></td>";
                            tableHtml += "</tr>";
                        }
                    }
                    $("#bomMXTB").append(tableHtml);
                    layer.closeAll();
                }
            }

        },
        error: function () {
            layer.alert("服务器连接超时");
        }
    })
}

//编辑和添加的方法
function baocun(obj, state) {
    if (state == 1) {
        layer.alert("该样衣Bom已审核，请您先在主页进行反审！");
    }
    else if (state == 2) {
        layer.alert("该样衣Bom已作废，无法编辑！");
    }
    else if (state == 3) {
        layer.alert("该Bom已生成大货Bom，请勿更改！");
    }
    else {
        var photo = $("#preview").find("img").attr("src"); //图片路径
        //获取样衣Bom明细表数据用于添加和编辑
        var MXArr = "";
        var MXid = "";
        var MXnum = 0;
        var isTrue = "true";//检验明细数据是否全部确定 $("#bomMXTB").find("tr").eq(0).attr("data-dd")
        var isNumber = true;//判断数量/价格/金额为数字
        $("#bomMXTB").find("tr").each(function (index) {
            if ($(this).attr("data-dd") == "false") {
                isTrue = "false";
                return isTrue;
            }

            var SampleMX01 = $(this).find("td").eq(0).find("span").html();//项目
            var SampleMX02 = $(this).find("td").eq(1).find("span").html();//组别
            var SampleMX03 = $(this).find("td").eq(2).find("span").html();//物料编码
            var SampleMX04 = $(this).find("td").eq(3).find("span").html();//色号
            var SampleMX05 = $(this).find("td").eq(4).find("span").html();//物料名称
            var SampleMX06 = $(this).find("td").eq(5).find("span").html();//物料规格
            var SampleMX07 = $(this).find("td").eq(6).find("span").html();//颜色
            var SampleMX08 = $(this).find("td").eq(7).find("span").html();//门幅
            var SampleMX09 = $(this).find("td").eq(8).find("span").html();//单位
            var SampleMX10 = $(this).find("td").eq(9).find("span").html();//公斤米数
            var SampleMX11 = $(this).find("td").eq(10).find("span").html();//使用部位
            var SampleMX12 = $(this).find("td").eq(11).find("span").html();//单件用量
            var SampleMX13 = $(this).find("td").eq(12).find("span").html();//损耗
            var SampleMX14 = $(this).find("td").eq(13).find("span").html();//实际数量
            var SampleMX15 = $(this).find("td").eq(14).find("span").html();//价格
            var SampleMX16 = $(this).find("td").eq(15).find("span").html();//金额
            var SampleMX17 = $(this).find("td").eq(16).find("span").html();//备注
            var SampleMX18 = $(this).find("td").eq(17).find("span").html();//供应商
            var SampleMX19 = $(this).find("td").eq(18).find("span").html();//供应商物料编码
            var str = SampleMX01 + "," + SampleMX02 + "," + SampleMX03 + "," + SampleMX04 + "," + SampleMX05 + "," + SampleMX06 + "," + SampleMX07 + "," + SampleMX08 + "," + SampleMX09 + "," + SampleMX10 + "," + SampleMX11 + "," + SampleMX12 + "," + SampleMX13 + "," + SampleMX14 + "," + SampleMX15 + "," + SampleMX16 + "," + SampleMX17 + "," + SampleMX18 + "," + SampleMX19;
            var id = $(this).eq(0).attr("values");
            MXid += id + ",";//样衣Bom明细ID
            MXArr += str + "|";//样衣Bom明细数据
            MXnum = index + 1;//数据条数
            if (SampleMX14 != undefined && SampleMX15 != undefined && SampleMX16 != undefined) {
                if (!/^[0-9]+$/.test(SampleMX14) || !/^[0-9]+$/.test(SampleMX15) || !/^[0-9]+$/.test(SampleMX16)) {
                    isNumber = false;
                }
            }
        })
        if (isNullB() == false) {
            layer.alert("还有数据未填写!");
        }
        else if (isTrue == "false") {
            layer.alert("包含未确定数据,请先确定后再保存!")
        }
        else if (!isNumber) {
            layer.alert("实际数量、价格和金额只能输入数字!")
        }
        else {
            $.ajax({
                url: "/Bom/AddUpdateBom",
                type: "post",
                async: false,
                data: {
                    id: obj,
                    Sample06: $("#Sample06").val(),
                    Sample03: $("#Sample03").val(),
                    Sample04: $("#Sample04").val(),
                    Sample05: $("#Sample05").val(),
                    Sample11: $("#Sample11").val(),
                    Sample12: $("#Sample12").val(),
                    Sample02: $("#Sample02").val(),
                    Sample08: $("#Sample08").val(),
                    Sample07: $("#Sample07").val(),
                    Sample09: $("#Sample09").val(),
                    Sample10: $("#Sample10").val(),
                    Sample16: photo,
                    Sample17: $("#Sample05_id").val(),   //波段ID
                    Sample18: $("#Sample11_id").val(),   //设计师ID
                    Sample19: $("#Sample08_id").val(),   //款式类别ID
                    Sample20: $("#Sample09_id").val(),   //大类ID
                    Sample21: $("#Sample13_id").val(),   //颜色ID
                    Sample22: $("#Sample13").val(),   //颜色

                    //---------明细表------------//
                    MXArr: MXArr.substring(0, MXArr.length - 1),
                    MXid: MXid.substring(0, MXid.length - 1),//ID
                    MXnum: MXnum,//数据条数

                },
                success: function (data) {
                    if (data == "success") {
                        layer.open({
                            title: '消息',
                            content: '保存成功!',
                            btn: ['确定'],
                            yes: function (index, layero) {
                                location.href = "/Bom/SplBomIndex";
                            }
                        });
                    }
                    if (data == "null") {
                        layer.alert("保存失败！");
                    }
                },
                error: function () {
                    layer.alert("服务器连接超时");
                }
            })
        }
    }

}
//非空判断
function isNullB() {
    var ss = true;
    $(".Bom_table").find("tr").find("td").find(".Bom_finput").each(function (index) {
        var aa = "";
        var bb = "";
        if (index != 2 && index != 6 && index != 5) {
            aa = $(this).find("input").val();
            if (aa == "") {
                ss = false;
            }
        }
        else {
            bb = $(this).find("select").val();
            if (bb == "") {
                ss = false;
            }
        }
    })
    return ss;
}

function Cost(id) {
    this.id = id;
    this.delid = "";
    this.delts = true;
    this.baseUrl = $(window.parent.document).find("#frmEditor").attr("src");
}

//绑定项目下拉框
function setItem(text, sel) {
    var mySelect = document.createElement("select");
    mySelect.add(new Option("请选择", ""));
    mySelect.add(new Option("面料", "面料"));
    mySelect.add(new Option("配料", "配料"));
    mySelect.add(new Option("里料", "里料"));
    mySelect.add(new Option("一般辅料", "一般辅料"));
    mySelect.add(new Option("包装辅料", "包装辅料"));
    mySelect.add(new Option("工艺工序", "工艺工序"));
    mySelect.value = text;
    mySelect.style.width = sel + "px";
    return mySelect;
}

//绑定项目下拉框
function setInput(text, sel) {
    var myInput = document.createElement('input');
    myInput.setAttribute('type', 'text');
    myInput.setAttribute('value', text);
    myInput.style.width = sel;
    return myInput;
}
//绑定组别下拉框
function setGroup(text, sel) {
    var mySelect = document.createElement("select");
    mySelect.add(new Option("请选择", ""));
    mySelect.add(new Option("A1", "A1"));
    mySelect.add(new Option("B1", "B1"));
    mySelect.add(new Option("C1", "C1"));
    mySelect.value = text;
    mySelect.style.width = sel + "px";
    return mySelect;
}

//返回主页面
function GoBack(obj)
{
    if (obj==1) {
        location.href = "/Bom/SplBomIndex";
    }
    else {
        location.href = "/Bom/BigBomIndex";
    }
}

//样衣bom明细列表修改
Cost.prototype.editfy = function (obj) {
    var dk = this.baseUrl;
    var div_edit = $(obj);
    if (div_edit.html() == "修改") {

        var td_index = div_edit.parents("tr").find("td").length;
        var tdspan_html = "";
        div_edit.parents("tr").find("td").each(function (index) {
            $(this).parents("tr").attr("data-dd", false); //当点击修改时把"data-dd"属性改为false

            tdspan_html = $(this).find("span").html();
            if (index == 0) {
                $(this).html(setItem(tdspan_html, $(this).find("span").width()));
            } else if (index == 1) {
                if (dk == "/Bom/BigBomIndex") {
                    var myi = setInput(tdspan_html, $(this).find("span").width() + "px");
                    $(this).html(myi);
                } else {
                    $(this).html(setGroup(tdspan_html, $(this).find("span").width()));
                }
            } else if (index == 2) {
                $(this).attr("class", "Sample_finput_3");
                $(this).attr("data-lie", "3");
                var myi = setInput(tdspan_html, $(this).find("span").width() + "px");
                myi.className = "div_dblclick";
                $(this).html(myi);
            } else if (index == 4) {
                $(this).attr("class", "Sample_finput_4");
                $(this).attr("data-lie", "4");
                var myi = setInput(tdspan_html, $(this).find("span").width() + "px");
                myi.className = "div_dblclick";
                $(this).html(myi);
            } else if (index == 6) {
                $(this).attr("class", "Sample_finput_0");
                $(this).attr("data-lie", "0");
                var myi = setInput(tdspan_html, $(this).find("span").width() + "px");
                myi.className = "div_dblclick";
                $(this).html(myi);
            } else if (index == 8) {
                $(this).attr("class", "Sample_finput_2");
                $(this).attr("data-lie", "2");
                var myi = setInput(tdspan_html, $(this).find("span").width() + "px");
                myi.className = "div_dblclick";
                $(this).html(myi);
            } else if (index == 10) {
                $(this).attr("class", "Sample_finput_12");
                $(this).attr("data-lie", "12");
                var myi = setInput(tdspan_html, $(this).find("span").width() + "px");
                myi.className = "div_dblclick";
                $(this).html(myi);
            } else if (index + 1 != td_index) {
                $(this).html(setInput(tdspan_html, $(this).find("span").width() + "px"));
            }
           
        });
       div_edit.html("确定");
    } else {
        var pp = true;
        var td_index = div_edit.parents("tr").find("td").length;
        var tdinupt_html = "";
        var Gygx = div_edit.parents("tr").find("td").eq(0).find("select").val();
        div_edit.parents("tr").find("td").each(function (index) {
            if (Gygx == "工艺工序") {
                if (dk == "/Bom/BigBomIndex") {
                    if (index == 2 || index == 4 || index == 13 || index == 14) {
                        tdinupt_html = $(this).find("input").val();
                    }
                    else {
                        tdinupt_html = "0";
                    }
                }
                else {
                    if (index == 2 || index == 4 || index == 14 || index == 15) {
                        tdinupt_html = $(this).find("input").val();
                    }
                    else {
                        tdinupt_html = "0";
                    }
                }

            }
            //------------------------------------//
            else {
                if (index + 1 != td_index && index != 0 && index != 1) {
                    tdinupt_html = $(this).find("input").val();
                }
                else if (index == 0) {
                    tdinupt_html = $(this).find("select").val();
                }
                else if (index == 1) {
                    if (dk == "/Bom/BigBomIndex") {
                        tdinupt_html = $(this).find("input").val();
                    } else {
                        tdinupt_html = $(this).find("select").val();
                    }
                }
            }
            
            if (tdinupt_html == "") {

                pp = false;
                return;
            }
         
        });

        if (pp) {
            div_edit.parents("tr").find("td").each(function (index) {
                $(this).parents("tr").attr("data-dd", true); //当点击确定时把"data-dd"属性改为true

                if (index + 1 != td_index && index != 0 && index != 1) {
                    tdinupt_html = $(this).find("input").val();
                    var span = "<span class='poducts short_tit f_fl'>" + tdinupt_html + "</span>";
                    $(this).html(span);
                }
                else if (index == 0) {
                    tdinupt_html = $(this).find("select").val();
                    var span = "<span class='poducts short_tit f_fl'>" + tdinupt_html + "</span>";
                    $(this).html(span);
                }
                else if (index == 1) {
                    if (dk == "/Bom/BigBomIndex") {
                        tdinupt_html = $(this).find("input").val();
                    } else {
                        tdinupt_html = $(this).find("select").val();
                    }
                    var span = "<span class='poducts short_tit f_fl'>" + tdinupt_html + "</span>";
                    $(this).html(span);
                }
            });
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
                btn: ['确定', '取消'],
                yes: function (index, layero) {
                    layer.closeAll();
                }
            });
        }
        this.delts = false;
        this.delid += $(obj).parents("tr").attr("values") + ",";
        $(obj).parents("tr").remove();
    } else {
        layer.alert("请保留一行数据！");
    }
}
Cost.prototype.addfy = function (obj) {
    var div_edit = $(obj);
    var pp = true;
    if ($(obj).parent().find("div:eq(1)").html() == "确定") {
        layer.alert("此行数据未确认，请确认之后再添加！");
    } else {
        if (pp) {
            var Ytr = '<tr values="0" data-dd="false"><td><select id=""><option value="">请选择</option><option value="面料">面料</option><option value="配料">配料</option><option value="里料">里料</option><option value="一般辅料">一般辅料</option><option value="包装辅料">包装辅料</option><option value="工艺工序">工艺工序</option></select></td> <td><select id=""><option value="">请选择</option><option value="A1">A1</option><option value="B1">B1</option><option value="C1">C1</option></select></td><td class="Sample_finput_3" data-lie="3"><input type="text" value="" style="width:100%;"  class="div_dblclick"></td><td ><input type="text" value="" style="width:100%;"></td><td class="Sample_finput_4" data-lie="4"><input type="text" value="" style="width:100%;" class="div_dblclick"></td><td><input type="text" value="" style="width:100%;"></td><td class="Sample_finput_0" data-lie="0"><input type="text" value="" style="width:100%;" class="div_dblclick"></td><td><input type="text" value="" style="width:100%;"></td><td class="Sample_finput_2" data-lie="2"><input type="text" value="" style="width:100%;" class="div_dblclick"></td><td><input type="text" value="" style="width:100%;"></td><td class="Sample_finput_12" data-lie="12"><input type="text" value="" style="width:100%;" class="div_dblclick"></td><td><input type="text" value="" style="width:100%;"></td><td ><input type="text" value="0" style="width:100%;"></td><td><input type="text" value="0" style="width:100%;"></td><td><input type="text" value="0" style="width:100%;"></td><td><input type="text" value="0" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td style="width:250px;"><div class="b_bntlan lit fl" onclick="cost.addfy(this)">新建</div><div class="b_bntlan lit  fl" onclick="cost.editfy(this)">确定</div><div class="b_bntred lit  fl" onclick="cost.delOther(this);" data-id="0">删除</div></td></tr>';
            $(obj).parents("tbody").prepend(Ytr);
        } else {
            layer.alert("表格中还有数据未填写！");
        }
    }
}


//----------------------------------------大货Bom明细--------------------------------------------------//

//获取样衣Bom明细table并绑定
function GetCargoMXTable(obj) {
    layer.msg('加载中......', {
        icon: 16,
        shade: 0.01
    });
    $.ajax({
        url: "/Bom/GetCargoMXIndex",
        type: "post",
        async: false,
        data: { id: obj },
        success: function (data) {
            if (data == "" || data == "[]") {
                var tr = "<tr values='0' data-dd='true'>";
                tr += "    <td><select id=''><option value=''>请选择</option><option value='面料'>面料</option><option value='配料'>配料</option><option value='里料'>里料</option><option value='一般辅料'>一般辅料</option><option value='包装辅料'>包装辅料</option><option value='工艺工序'>工艺工序</option></select></td>";
              tr += "    <td><input type='text' name=''/></td>";
              tr += "    <td class='Sample_finput_3' data-lie='3'><input type='text' name='' class='div_dblclick'/></td>";
               tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td class='Sample_finput_4' data-lie='4'  ><input type='text' name='' class='div_dblclick'/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td class='Sample_finput_0' data-lie='0'><input type='text' name='' class='div_dblclick'/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td class='Sample_finput_2' data-lie='2'><input type='text' name='' class='div_dblclick'/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td class='Sample_finput_12' data-lie='12'><input type='text' name='' class='div_dblclick'/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td><input type='text' name=''value='0'/></td>";
                tr += "    <td><input type='text' name=''value='0'/></td>";
                tr += "    <td><input type='text' name=''value='0'/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "    <td><input type='text' name=''/></td>";
                tr += "<td style='width:250px;'><div style='width:250px;'><div class='b_bntlan lit fl' onclick='cost.addCargo(this)'>新建</div><div class='b_bntlan lit  fl' onclick='cost.editfy(this)'>确定</div><div class='b_bntred lit  fl' onclick='cost.delOther(this);' data-id='0'>删除</div></div></td>";
                tr += "</tr>";
                $("#CargoMXTB").append(tr);
                layer.closeAll();
            }
            else {
                if (data == "error") {
                    layer.alert("数据出错,请联系信息部");
                }
                else {
                    var dt = eval("(" + data + ")");
                    var tableHtml = "";
                    $("#CargoMXTB").html("");
                    if (dt.length > 0) {
                        for (var i = 0; i < dt.length; i++) {
                            tableHtml += "<tr values=" + dt[i].id + "  data-dd='true'>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl' style='width:120px;'>" + dt[i].SampleMX01 + "</span></td>";
                             tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX04 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'  style='width:100%;'>" + dt[i].SampleMX03 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX06 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'style='width:100%;' >" + dt[i].SampleMX05 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX08 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX07 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX10 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl' style='width:80px'>" + dt[i].SampleMX09 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX12 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX11 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX13 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX14 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX15 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX16 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'style='width:100px'>" + dt[i].SampleMX17 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl' style='width:120px'>" + dt[i].SampleMX18 + "</span></td>";
                            tableHtml += "    <td><span class='poducts short_tit f_fl'>" + dt[i].SampleMX19 + "</span></td>";
                            tableHtml += "<td style='width:250px;'><div style='width:250px;'><div class='b_bntlan lit fl' onclick='cost.addCargo(this)'>新建</div><div class='b_bntlan lit  fl' onclick='cost.editfy(this)'>修改</div><div class='b_bntred lit  fl' onclick='cost.delOther(this);' data-id='0'>删除</div></div></td>";
                            tableHtml += "</tr>";
                        }
                    }
                    $("#CargoMXTB").append(tableHtml);
                    layer.closeAll();
                }
            }

        },
        error: function () {
            layer.alert("服务器连接超时");
        }
    })
}

var interval = setInterval(function () {
    var wid_kuan = $(".fancyTable tr .wid_kuan").find(".fht-cell");
    var wid_kuan_4 = $(".fancyTable tr .wid_kuan_4").find(".fht-cell");
    var wid_kuan_0= $(".fancyTable tr .wid_kuan_0").find(".fht-cell");
    wid_kuan.css({
        "width": parseInt(wid_kuan.css("width")) + 100 + "px"
        });
    wid_kuan_0.css({
        "width": parseInt(wid_kuan_0.css("width")) + 20 + "px"
    });
    wid_kuan_4.css({
        "width": parseInt(wid_kuan_4.css("width")) + 50 + "px"
    });
     clearInterval(interval);
}, 600);



//大货Bom编辑和添加的方法
function CargoAddUp(obj, state) {
    if (state == 1) {
        layer.alert("该样衣Bom已审核，请您先在主页进行反审！");
    }
    else if (state == 2) {
        layer.alert("该样衣Bom已作废，无法编辑！");
    }
    else if (state == 3) {
        layer.alert("该Bom已生成大货Bom，请勿更改！");
    }
    else {
        var photo = $("#preview").find("img").attr("src"); //图片路径
        //获取样衣Bom明细表数据用于添加和编辑
        var MXArr = "";
        var MXid = "";
        var MXnum = 0;
        var isTrue = "true";//检验明细数据是否全部确定
        var isNumber = true;//判断数量/价格/金额为数字
        $("#CargoMXTB").find("tr").each(function (index) {
            if ($(this).attr("data-dd") == "false") {
                isTrue = "false";
                return isTrue;
            }

            var SampleMX01 = $(this).find("td").eq(0).find("span").html();//项目
            var SampleMX04 = $(this).find("td").eq(2).find("span").html();//色号
            var SampleMX03 = $(this).find("td").eq(1).find("span").html();//物料编码
            var SampleMX06 = $(this).find("td").eq(4).find("span").html();//物料规格
            var SampleMX05 = $(this).find("td").eq(3).find("span").html();//物料名称
            var SampleMX07 = $(this).find("td").eq(5).find("span").html();//颜色
            var SampleMX08 = $(this).find("td").eq(6).find("span").html();//门幅
            var SampleMX09 = $(this).find("td").eq(7).find("span").html();//单位
            var SampleMX10 = $(this).find("td").eq(8).find("span").html();//公斤米数
            var SampleMX11 = $(this).find("td").eq(9).find("span").html();///使用部位
            var SampleMX12 = $(this).find("td").eq(10).find("span").html();//单件用量
            var SampleMX13 = $(this).find("td").eq(11).find("span").html();//损耗
            var SampleMX14 = $(this).find("td").eq(12).find("span").html();//实际数量
            var SampleMX15 = $(this).find("td").eq(13).find("span").html();//价格
            var SampleMX16 = $(this).find("td").eq(14).find("span").html();//金额
            var SampleMX17 = $(this).find("td").eq(15).find("span").html();//备注
            var SampleMX18 = $(this).find("td").eq(16).find("span").html();//供应商  
            var SampleMX19 = $(this).find("td").eq(17).find("span").html();//供应商物料编码
            var str = SampleMX01 + "," + SampleMX04 + "," + SampleMX03 + "," + SampleMX06+ "," + SampleMX05 + "," + SampleMX07 + "," + SampleMX08 + "," + SampleMX09 + "," + SampleMX10 + "," + SampleMX11 + "," + SampleMX12 + "," + SampleMX13 + "," + SampleMX14 + "," + SampleMX15 + "," + SampleMX16 + "," + SampleMX17 + "," + SampleMX18 + "," + SampleMX19;
            var id = $(this).eq(0).attr("values");
            MXid += id + ",";//样衣Bom明细ID
            MXArr += str + "|";//样衣Bom明细数据
            MXnum = index + 1;//数据条数

            if (SampleMX14 != undefined && SampleMX15 != undefined && SampleMX16 != undefined) {
                if (!/^[0-9]+$/.test(SampleMX14) || !/^[0-9]+$/.test(SampleMX15) || !/^[0-9]+$/.test(SampleMX16)) {
                    isNumber = false;
                }
            }
        })
        if (isNullB() == false) {
            layer.alert("还有数据未填写!");
        }
        else if (isTrue == "false") {
            layer.alert("包含未确定数据,请先确定后再保存!")
        }
        else if (!isNumber) {
            layer.alert("实际数量、价格和金额只能输入数字!")
        }
        else {
            $.ajax({
                url: "/Bom/AddUpdateCargo",
                type: "post",
                async: false,
                data: {
                    id: obj,
                    Sample06: $("#Sample06").val(),
                    Sample03: $("#Sample03").val(),
                    Sample04: $("#Sample04").val(),
                    Sample05: $("#Sample05").val(),
                    Sample11: $("#Sample11").val(),
                    Sample12: $("#Sample12").val(),
                    Sample02: $("#Sample02").val(),
                    Sample08: $("#Sample08").val(),
                    Sample07: $("#Sample07").val(),
                    Sample09: $("#Sample09").val(),
                    Sample10: $("#Sample10").val(),
                    Sample16: photo,

                    Sample17: $("#Sample05_id").val(),   //波段ID
                    Sample18: $("#Sample11_id").val(),   //设计师ID
                    Sample19: $("#Sample08_id").val(),   //款式类别ID
                    Sample20: $("#Sample09_id").val(),   //大类ID
                    Sample21: $("#Sample13_id").val(),   //颜色ID
                    Sample22: $("#Sample13").val(),   //颜色

                    //---------明细表------------//
                    MXArr: MXArr.substring(0, MXArr.length - 1),
                    MXid: MXid.substring(0, MXid.length - 1),//ID
                    MXnum: MXnum,//数据条数

                },
                success: function (data) {
                    if (data == "success") {
                        layer.open({
                            title: '消息',
                            content: '保存成功!',
                            btn: ['确定'],
                            yes: function (index, layero) {
                                location.href = "/Bom/BigBomIndex";
                            }
                        });
                    }
                    else if (data == "null") {
                        layer.alert("保存失败！");
                    }
                    else {
                        layer.alert("服务器连接超时");
                    }

                },
                error: function () {
                    layer.alert("服务器连接超时");
                }
            })
        }
    }
}

//大货bom明细列表修改
Cost.prototype.addCargo = function (obj) {

    var pp = true;
    if ($(obj).parent().find("div:eq(1)").html() == "确定") {
        layer.alert("此行数据未确认，请确认之后再添加！");
    } else {
        if (pp) {
            var Ytr = ' <tr values="0" data-dd="false"><td><select id=""><option value="">请选择</option><option value="面料">面料</option><option value="配料">配料</option><option value="里料">里料</option><option value="一般辅料">一般辅料</option><option value="包装辅料">包装辅料</option><option value="工艺工序">工艺工序</option></select></td><td><input type="text" value="" style="width:100%;"></td><td class="Sample_finput_3" data-lie="3"><input type="text" value="" style="width:100%;" class="div_dblclick"></td><td><input type="text" value="" style="width:100%;"></td><td class="Sample_finput_4" data-lie="4"><input type="text" value="" style="width:100%;" class="div_dblclick"></td><td><input type="text" value="" style="width:100%;"></td><td class="Sample_finput_0" data-lie="0"><input type="text" value="" style="width:100%;" class="div_dblclick"></td><td><input type="text" value="" style="width:100%;"></td><td class="Sample_finput_2" data-lie="2"><input type="text" value="" style="width:100%;" class="div_dblclick"></td><td><input type="text" value="" style="width:100%;"></td><td  class="Sample_finput_12" data-lie="12"><input type="text" value="" style="width:100%;"class="div_dblclick"></td><td><input type="text" value="0" style="width:100%;"></td><td><input type="text" value="0" style="width:100%;"></td><td><input type="text" value="0" style="width:100%;"></td><td><input type="text" value="0" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td><input type="text" value="" style="width:100%;"></td><td style="width:250px;"><div class="b_bntlan lit fl" onclick="cost.addCargo(this)">新建</div><div class="b_bntlan lit  fl" onclick="cost.editfy(this)">确定</div><div class="b_bntred lit  fl" onclick="cost.delOther(this);" data-id="0">删除</div></td></tr>';
            $(obj).parents("tbody").prepend(Ytr);
        } else {
            layer.alert("表格中还有数据未填写！");
        }
    }
}


//-----------------------------------------部分文本框连接其它数据库绑定------------------------------------------------------------//

//从其它表获取样衣bom一些字段的值
function GetBomField(obj, selXM) {
    
    $(".Reconciliation_ser tbody tr").each(function () {//循环所有tr数据
        if (!$(this).hasClass("ad_cls")) {//如果没有选中tr数据
            $(this).remove();//把没选中的tr数据删除
        } 
        //相反就留下数据
    });
   
    var DesignerID = $("#GYSGC50s").val();
    var DesignerName = $("#GYSGC03s").val();

    var strID = "";   //数据库ID
    var strName = "";  //数据库名称
    if (obj == "设计师") {
        strID = "DesignID";
        strName = "Designer";
    }
    else if (obj == "款式类别") {
        strID = "SXDM";
        strName = "SXMC";
    }
    else if (obj == "大类") {
        strID = "BigType";
        strName = "BigTypeName";
    }
    else if (obj == "波段") //波段
    {
        strID = "SXDM";
        strName = "SXMC";
    }
    else if (obj == "颜色") {
        strID = "GGDM";
        strName = "GGMC";
    }
    else if (obj == "物料") {
        if (selXM=="工艺工序") {
            strID = "Code";
            strName = "Name";
        }
        else if (selXM == "一般辅料" || selXM == "包装辅料") {
            strID = "FLDM";
            strName = "FLMC";
        }
        else {
            strID = "MLDM";
            strName = "MLMC";
        }
    }
    else if (obj == "使用部位") {
        strID = "BWDM";
        strName = "BWMC";
    }
    else {
        strID = "DWDM";
        strName = "DWMC";
    }

    $.ajax({
        url: "/Bom/SelectOther",
        type: "post",
        data: { Did: DesignerID, DName: DesignerName, HtmlName: obj, selXM: selXM },
        async: false,
        success: function (data) {
            var dt = eval("(" + data + ")");
            var tableHtml = "";
            //$("#DBomTB").html("");
            if (dt.length > 0) {
                for (var i = 0; i < dt.length; i++) {
                    tableHtml += "<tr>";
                    tableHtml += "<td><span class='short_tit f_fl'>" + dt[i][strID] + "</span></td>";
                    tableHtml += "<td><span class='short_tit f_fl'>" + dt[i][strName] + "</span></td>";
                    tableHtml += "</tr>";
                }
                $("#DBomTB").append(tableHtml);//append()数据累加，保留的tr数据留下来，查询没显示的数据
            }
        },
        error: function () {
            layer.alert("服务器连接超时");
        }
    })
}

function ShowTable(ee) {
    if (ee == "1") {
        $('#myTable01').fixedHeaderTable({ footer: false, cloneHeadToFoot: true, autoShow: false });
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


function del() {

    //var img = document.getElementById('imghead');
    //img.src = "/skins/img/photo_icon.png";

    //$("#imghead").attr("src", "/skins/img/photo_icon.png");
    alert("确定删除图片吗？");
    var div = document.getElementById('preview');
    div.innerHTML = '<img width="99%" height="99%" src="/skins/img/photo_icon.png" id=imghead onclick=$("#previewImg").click()>';
}

/*Bom详细双击事件*/
var hhhpp = $("#hhhpp", window.parent.document).html();
var $Reconciliation = $("#Bom_ser_wai1");


function dbclick() {
    $Reconciliation.show(100);
    $("#maskIframe").show(100);
}
//确认按钮
function queren2() {
    if (document.getElementById("maskIframe")) {
        document.getElementById("maskIframe").style.display = "none";
    }
    var $Reconciliation_ser = $(".Reconciliation_ser .ad_cls");
    var shu = $Reconciliation.attr("data-id");
    if (shu == 0 || shu == 2||shu==12) {
        $(".Sample_finput_" + shu).find("input:eq(0)").val($Reconciliation_ser.find("td span:eq(1)").html());
    }else if(shu==3||shu==4){
        $(".Sample_finput_3").find("input:eq(0)").val($Reconciliation_ser.find("td span:eq(0)").html());
        $(".Sample_finput_4").find("input:eq(0)").val($Reconciliation_ser.find("td span:eq(1)").html());
    } else if (shu == 13) {
        var numss1 = [];
        var numss = [];
        $Reconciliation_ser.each(function (index) {
            var ss1 = $(".Reconciliation_ser .ad_cls:eq(" + index + ")").find("td span:eq(0)");
            var ss = $(".Reconciliation_ser .ad_cls:eq(" + index + ")").find("td span:eq(1)");
            numss1.push(ss1.html());
            numss.push(ss.html());
            $(".Sample_finput_" + shu).find("input:eq(0)").val(numss1);
            $(".Sample_finput_" + shu).find("input:eq(1)").val(numss);
         
           });
    } else {
        $(".Sample_finput_" + shu).find("input:eq(0)").val($Reconciliation_ser.find("td span:eq(0)").html());
        $(".Sample_finput_" + shu).find("input:eq(1)").val($Reconciliation_ser.find("td span:eq(1)").html());
    }
   
    $('.Reconciliation_ser_wai').toggle(100);

}

/*明细表格*/
$(function () {
    $(".fancyTable ").delegate(".div_dblclick", "dblclick", function () {
        $("#GYSGC50s").val("");
        $("#GYSGC03s").val("");
        $("#DBomTB").html("");
        var tit_dblclick = "";
        dbclick();
        $Reconciliation.attr("data-id", $(this).parent().attr("data-lie"));
        //var tit_dblclick = $(this).parent().attr("data-lie") == "0" ? "颜色" : "单位";
        var selXM = $(this).parents("tr").find("td:eq(0) select").val();
       if ($(this).parent().attr("data-lie") == "0") {
            tit_dblclick = "颜色";
        } else if ($(this).parent().attr("data-lie") == "2") {
            tit_dblclick = "单位";
        } else if ($(this).parent().attr("data-lie") == "3") {
            tit_dblclick = "物料";
        } else if ($(this).parent().attr("data-lie") == "4") {
            tit_dblclick = "物料";
        } else if ($(this).parent().attr("data-lie") == "12") {
            $(".Reconciliation_ser_div2 input").css("width","46%");
            tit_dblclick = "使用部位";
           
        }
       $(".tit_dblclick").attr("values", tit_dblclick);
       $(".tit_dblclick").html(tit_dblclick);
       $("#tit_hhhpp").html(hhhpp);

       $(".tit_dblclick").attr("data-XM", selXM);

        GetBomField(tit_dblclick,selXM);
    });

    /*明细文本*/
    $(".div_dblclick").dblclick(function () {
        $("#GYSGC50s").val("");
        $("#GYSGC03s").val("");
        $("#DBomTB").html("");
        dbclick();
        $Reconciliation.attr("data-id", $(this).parent().attr("data-lie"));
        var tit_dblclick = $(this).parent().parent().prev().find(".Bom_span").html().trim();
      
        $(".tit_dblclick").html(tit_dblclick);
        $(".tit_dblclick").attr("values", tit_dblclick);
        $("#tit_hhhpp").html(hhhpp);

        $(".tit_dblclick").attr("data-XM", tit_dblclick);

        if (tit_dblclick == "波段") {
            GetBomField(tit_dblclick);
        }
        if ($(this).parent().attr("data-lie") == "8") {
            $(".Reconciliation_ser_div2 input").css("width", "46%");
        }
    });
    //弹窗双击
   $(" #Bom_ser_wai1 tbody").delegate("tr", "dblclick", function () {
        var shu = $Reconciliation.attr("data-id");
       if (shu == 0 || shu == 2||shu==12) {
             $(".Sample_finput_" + shu).find("input:eq(0)").val($(this).find("span:eq(1)").html());
        } else if (shu == 3 || shu == 4) {
            $(".Sample_finput_3").find("input:eq(0)").val($(this).find("td span:eq(0)").html());
            $(".Sample_finput_4").find("input:eq(0)").val($(this).find("td span:eq(1)").html());
         } else {
            $(".Sample_finput_" + shu).find("input:eq(0)").val($(this).find("span:eq(0)").html());
            $(".Sample_finput_" + shu).find("input:eq(1)").val($(this).find("span:eq(1)").html());
        }
        if (document.getElementById("maskIframe")) {
            document.getElementById("maskIframe").style.display = "none";
        }
        $('.Reconciliation_ser_wai').toggle(100);
    });

    $("#Bom_ser_wai1 .Reconciliation_ser").delegate("td", "click", function () {
        var shu = $Reconciliation.attr("data-id");
        if (shu == 13) {
            if ($(".Reconciliation_ser tr").hasClass("ad_cls")) {
                if ($(this).parent("tr").hasClass("")) {
                  $(this).parent("tr").addClass("ad_cls");
                } else {
                    $(this).parent("tr").removeClass("ad_cls");
                }
            }else {
                $(this).parent("tr").addClass("ad_cls");
            }
        } else {
            if ($(".Reconciliation_ser tr").hasClass("ad_cls")) {
                $(".Reconciliation_ser tr").removeClass("ad_cls");
                $(this).parent("tr").addClass("ad_cls");
            } else {
                $(this).parent("tr").addClass("ad_cls");
            }
          }
  })
});
