function SJSQR(obj) {
    var spdm = $(obj).parents("tr").find("td:eq(1)").html();
    var isSJ = $(obj).find("span:eq(1)").html() == "确认" ? 1 : 0;
    var trueName = "";
    var istrue = false;
    $.ajax({
        url: "/ExamineBoss/Auditing",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        async: false,
        data: { IsSJ: isSJ, spdm: spdm },
        success: function (data1) {
            if (data1 == "error") {
                layer.alert("出错了请联系信息部");
            } else if (data1 == "error2") {
                layer.alert("公司已确认，暂时不支持修改");
            } else if (data1 == "error3") {
                layer.alert("商品已确认，暂时不支持修改");
            } else if (data1 == "Operation") {
                layer.alert("您没有此确认权限，请联系管理员");
            } else {
                var list = data1.split('|');
                if (list.length > 1) {
                    trueName = list[1];
                }
                istrue = true;
            }
        },
        error: function () {

        }
    })

    if (istrue) {
        if (isSJ == 1) {
            $(obj).find("span:eq(1)").html("取消");
            $(obj).find("span:eq(1)").parent().addClass("tbodys-tr-td-quer-ren");
            $(obj).find("span:eq(0)").find("img").attr("src", "/Content/imgSj/qux.png");
            $(obj).parent().find("span:eq(0)").html(trueName);
            $(obj).parent().find("span:eq(0)").show();

        } else {
            $(obj).find("span:eq(1)").html("确认");
            $(obj).find("span:eq(0)").find("img").attr("src", "/Content/imgSj/quer2.png");
            $(obj).find("span:eq(1)").parent().removeClass("tbodys-tr-td-quer-ren");
            $(obj).parent().find("span:eq(0)").hide();
        }
    }
}
function SPQR(obj) {
   debugger
    var spdm = $(obj).parents("tr").find("td:eq(1)").html();
    var isSP = $(obj).find("span:eq(1)").html() == "确认" ? 1 : 0;
    var textrar = $("#textrar1").val();
    var trueName = "";
    var istrue = false;
   
    $.ajax({
        url: "/ExamineBoss/Commodity",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        async: false,
        data: {
            IsSP: isSP,
            spdm: spdm,
            textrar: textrar
        },
        success: function (data1) {
            if (data1 == "error") {
                layer.alert("出错了请联系信息部");
            } else if (data1 == "error2") {
                layer.alert("设计师未确认，暂时不支持修改");
            } else if (data1 == "error3") {
                layer.alert("公司已确认，暂时不支持修改");
            } else if (data1 == "Operation") {
                layer.alert("您没有此确认权限，请联系管理员");
            } else {
                var list = data1.split('|');
                if (list.length > 1) {
                    trueName = list[1];
                }
                istrue = true;
            }
        },
        error: function () {

        }
    })

    if (istrue) {
        if (isSP == 1) {
            $(obj).find("span:eq(1)").html("取消");
            $(obj).find("span:eq(1)").parent().addClass("tbodys-tr-td-quer-ren");
            $(obj).find("span:eq(0)").find("img").attr("src", "/Content/imgSj/qux.png");
            $(obj).parent().find("span:eq(0)").html(trueName);
            $(obj).parent().find("span:eq(0)").show();

            $(obj).parents("tr").find(".gc6 input").attr("disabled", "disabled");
            $(obj).parents("tr").find(".gc7 input").attr("disabled", "disabled");

           // $(obj).parents("tr").find(".gc00 textarea").attr("disabled", "disabled")
            //$("#textrar1").attr("disabled", true);

        } else {
            $(obj).find("span:eq(1)").html("确认");
            $(obj).find("span:eq(0)").find("img").attr("src", "/Content/imgSj/quer2.png");
            $(obj).find("span:eq(1)").parent().removeClass("tbodys-tr-td-quer-ren");
            $(obj).parent().find("span:eq(0)").hide();
            $(obj).parents("tr").find(".gc6 input").removeAttr("disabled");
            $(obj).parents("tr").find(".gc7 input").removeAttr("disabled");
            
            //$("#textrar1").attr("disabled", false);

            //$(obj).parents("tr").find(".gc00 textarea").attr("disabled");
        }
    }
}
function BOSSQR(obj) {
    var spdm = $(obj).parents("tr").find("td:eq(1)").html();
    var IsBoss = $(obj).find("span:eq(1)").html() == "确认" ? 1 : 0;

    var trueName = "";
    var istrue = false;
    $.ajax({
        url: "/ExamineBoss/Boss",
        timeout: 0, //超时时间设置，单位毫秒
        type: "post",
        async: false,
        data: { IsBoss: IsBoss, spdm: spdm },
        success: function (data1) {
            if (data1 == "error") {
                layer.alert("出错了请联系信息部");
            } else if (data1 == "error2") {
                layer.alert("请先让设计师确认");
            } else if (data1 == "error3") {
                layer.alert("请先让商品部确认");
            } else if (data1 == "Operation") {
                layer.alert("您没有此确认权限，请联系管理员");
            } else {
                var list = data1.split('|');
                if (list.length > 1) {
                    trueName = list[1];
                }
                istrue = true;
            }
        },
        error: function () {

        }
    })

    if (istrue) {
        if (IsBoss == 1) {
            $(obj).find("span:eq(1)").html("取消");
            $(obj).find("span:eq(1)").parent().addClass("tbodys-tr-td-quer-ren");
            $(obj).find("span:eq(0)").find("img").attr("src", "/Content/imgSj/qux.png");
            $(obj).parent().find("span:eq(0)").html(trueName);
            $(obj).parent().find("span:eq(0)").show();
            $(obj).parents("tr").find(".gc8 input").attr("disabled", "disabled");
            $(obj).parents("tr").find(".gc9 input").attr("disabled", "disabled");
        } else {
            $(obj).find("span:eq(1)").html("确认");
            $(obj).find("span:eq(0)").find("img").attr("src", "/Content/imgSj/quer2.png");
            $(obj).find("span:eq(1)").parent().removeClass("tbodys-tr-td-quer-ren");
            $(obj).parent().find("span:eq(0)").hide();
            $(obj).parents("tr").find(".gc8 input").removeAttr("disabled");
            $(obj).parents("tr").find(".gc9 input").removeAttr("disabled");
        }
    }
}

$(function () {
    servermergetable_rowspan('#my-table', '41', '1');
    servermergetable_rowspan('#my-table', '40', '1');
    servermergetable_rowspan('#my-table', '39', '1');

    servermergetable_rowspan('#my-table', '38', '1');
    servermergetable_rowspan('#my-table', '37', '1');
    servermergetable_rowspan('#my-table', '36', '1');
    servermergetable_rowspan('#my-table', '35', '1');
    servermergetable_rowspan('#my-table', '34', '1');
    servermergetable_rowspan('#my-table', '33', '1');
    servermergetable_rowspan('#my-table', '32', '1');
    servermergetable_rowspan('#my-table', '31', '1');
    servermergetable_rowspan('#my-table', '30', '1');
    servermergetable_rowspan('#my-table', '29', '1');
    servermergetable_rowspan('#my-table', '28', '1');
    servermergetable_rowspan('#my-table', '27', '1');
    servermergetable_rowspan('#my-table', '26', '1');
    servermergetable_rowspan('#my-table', '25', '1');
    servermergetable_rowspan('#my-table', '24', '1');
    //servermergetable_rowspan('#my-table', '23', '1');
    //servermergetable_rowspan('#my-table', '22', '1');
    //servermergetable_rowspan('#my-table', '21', '1');
    //servermergetable_rowspan('#my-table', '20', '1');

    //servermergetable_rowspan('#my-table', '18', '1');
    //servermergetable_rowspan('#my-table', '19', '1');

    servermergetable_rowspan('#my-table', '17', '1');
    servermergetable_rowspan('#my-table', '16', '1');
    servermergetable_rowspan('#my-table', '11', '1');
    servermergetable_rowspan('#my-table', '5', '1');
    servermergetable_rowspan('#my-table', '4', '1');
    servermergetable_rowspan('#my-table', '3', '1');
    servermergetable_rowspan('#my-table', '2', '1');
    servermergetable_rowspan('#my-table', '1', '1');
    //$("#my-table").rowspan({ th: 1 });

})

/**
 依据前面的列内容合并后面的列内容
 param table_id tableID
 param table_colnum 待合并的列
 param table_colnum_decide 依据的列
 **/
function servermergetable_rowspan(table_id, table_colnum, table_colnum_decide) {
    //    查找依据列是否有相同待合并的
    var table_firsttd_decide = "";
    var table_currenttd_decide = "";
    var table_SpanNum_decide = 0;
    var array = [];
    var table_Obj_decide = $(table_id + " tbody td:nth-child(" + table_colnum_decide + ")");
    table_Obj_decide.each(function (i) {
        if (i == 0) {
            table_firsttd_decide = $(this);
            table_SpanNum_decide = 1;
        } else {
            table_currenttd_decide = $(this);
            if (table_firsttd_decide.text() == table_currenttd_decide.text()) {
                table_SpanNum_decide++;
            } else {
                table_firsttd_decide = $(this);
                table_SpanNum_decide = 1;
            }
        }
        //将依据列td的rowspan属性加入数组
        array.push(table_SpanNum_decide);
    });
    var table_firsttd = "";
    var table_currenttd = "";
    var table_SpanNum = 0;
    var table_Obj = $(table_id + " tbody td:nth-child(" + table_colnum + ")");
    table_Obj.each(function (i) {
        if (i == 0) {
            table_firsttd = $(this);
            table_SpanNum = array[i];
        } else {
            table_currenttd = $(this);
            table_SpanNum = array[i];
            if (table_SpanNum == 1) {//rowspan值为1的不合并
                table_firsttd = $(this);
            } else {//否则合并
                table_firsttd.attr("rowSpan", table_SpanNum);
                table_currenttd.remove();
            }
        }

    });
}


!function ($) {
    $.fn.rowspan = function (options) {
        var defaults = {}
        var options = $.extend(defaults, options);
        this.each(function () {

            var tds = $(this).find("tbody td:nth-child(" + options.td + ")");
            var current_td = tds.eq(0);
            var k = 2;
            tds.each(function (index, element) {
                if ($(this).text() == current_td.text() && index != 0) {
                    k++;
                    $(this).remove();
                    current_td.attr("rowspan", k);
                    current_td.css("vertical-align", "middle");
                } else {
                    current_td = $(this);
                    k = 1;
                }
            });

        })
    }
}(window.jQuery);





/*点击放大原图*/

$(function () {
    $(".pimg").click(function () {
        var _this = $(this);
        imgShow("#outerdiv", "#innerdiv", "#bigimg", _this);
    });
});
/*获取当前图片的真实大小，淡入淡出层级和大图*/
function imgShow(outerdiv, innerdiv, bigimg, _this) {
    var src = _this.attr("src");//获取当前点击图片的src
    $(bigimg).attr("src", src);//将获取当前点击图片的src，传入bigimg里
    $("<img/>").attr("src", src).load(function () {//img刷新加载
        var windowW = $(window).width();/*获取当前窗口的宽度*/
        var windowH = $(window).height();/*获取当前窗口的高度*/
        var realWidth = this.width;//获取图片真实宽
        var realHeight = this.height;//获取图片真实高
        var imgWidth;
        var imgHeight;
        var scale = 0.8;//缩放的尺寸,当图片的宽高大于窗口的宽高的时候进行缩放
        if (realHeight > windowH * scale) {//判断图片高度
            imgHeight = windowH * scale; //如果大于窗口的高度，图片的高度进行缩放
            imgWidth = imgHeight / realHeight * realWidth;//同比例缩放图片的宽度
            if (imgWidth > windowW * scale) {//如果图片的宽度依然大于窗口的宽度
                imgWidth = windowW * scale;//再对图片宽度继续缩小
            }
        } else if (realWidth > windowW * scale) {  //判断图片高度
            imgWidth = windowW * scale;//如果大于窗口的宽度，图片的宽度进行缩放
            imgHeight = imgWidth / realWidth * realHeight//同比例缩放图片的高度

        } else {//如果图片真实宽度和高度都符合要求，宽高都不变
            imgWidth = realWidth;
            imgHeight = realHeight;
        }
        var w = (windowW - imgWidth) / 2;//计算图片左右两边的距离
        var h = (windowH - imgHeight) / 2;//计算图片上下边的距离

        $(innerdiv).css({ "left": w, "top": h });
        $(bigimg).attr("width", imgWidth);//最终的宽度对图片缩放
        $(outerdiv).fadeIn("fast");//淡入显示#outerdiv 及图片pimg
    });
    $(outerdiv).click(function () {
        $(this).fadeOut("fast");
    });

}

//绑定款式下拉框
function LoadStyle(obj) {
    $.get("/ExamineBoss/GetDDLStyle", {}, function (data) {

        var ddlKS = "";
        $("#kuanshi").html("");//清空DDL
        var dt = eval("(" + data + ")");
        if(obj=="")
            ddlKS += "<option selected='selected' value=''>请选择</option>";
        else
            ddlKS += "<option value=''>请选择</option>";

        for (var i = 0; i < dt.length; i++) {
            if (dt[i]["SXMC"]==obj)
                ddlKS += "<option selected='selected' value=" + dt[i]["SXMC"] + ">" + dt[i]["SXMC"] + "</option>";
            else
                ddlKS += "<option value=" + dt[i]["SXMC"] + ">" + dt[i]["SXMC"] + "</option>";
        }
        $("#kuanshi").append(ddlKS);
    })
}

//绑定款式下拉框
function LoadFjsx2(obj) {
    $.get("/ExamineBoss/GetFjsx2", {}, function (data) {

        var ddlKS = "";
        $("#boduan").html("");
        var dt = eval("(" + data + ")");
        if (obj == "")
            ddlKS += "<option selected='selected' value=''>请选择</option>";
        else
            ddlKS += "<option value=''>请选择</option>";

        for (var i = 0; i < dt.length; i++) {
            if (dt[i]["SXMC"] == obj)
                ddlKS += "<option selected='selected' value=" + dt[i]["SXMC"] + ">" + dt[i]["SXMC"] + "</option>";
            else
                ddlKS += "<option value=" + dt[i]["SXMC"] + ">" + dt[i]["SXMC"] + "</option>";
        }
        $("#boduan").append(ddlKS);
    })
}

