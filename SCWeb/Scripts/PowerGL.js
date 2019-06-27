

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


//绑定页数下拉的方法
function DDLPageChange(pageCount, pageI) {
    //var pageCount = $("#Cpage").html();
    //alert(pageCount + "," + pageI);
    $("#pageOP").html("");
    var ddlHtml = "";
    for (var i = 1; i <= pageCount; i++) {
        if (i == pageI) {
            ddlHtml += "<option selected='true' value='" + i + "'>" + i + "</option>"
        }
        else {
            ddlHtml += "<option value='" + i + "'>" + i + "</option>"
        }
    }
    $("#pageOP").append(ddlHtml);
}