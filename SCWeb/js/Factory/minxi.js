var textareass = $("#supplier_textarea");

function fcour() {
    if (textareass.val().trim() == "综合评估:" || textareass.val().trim() == "") {
         textareass.val("综合评估:").css("color", "#000");
    }
};

function blurss() {
    if (textareass.val().trim() == "综合评估:") {
         textareass.val("综合评估:").css("color", "#a2a2a2");
    }
    //else {
    //    textareass.val("@ViewBag.GYSGC48");
    //}
};
//删除行
function deltr(delbtn) {
    $(delbtn).parents("tr").remove();
};

//添加行
function addtr() {
    //var a = $("table .xinjian tr").length;
    //  alert(a);
    //  if (a>12) {
    //      $(".sp_xj .td_bnt").attr("width","11%");
    //  }
    //绑定波段下拉框
  
   
                                                 
        var tr = '<tr class="new_input">';
        tr += ' <td><span values="0" class="poducts short_tit f_fl"></span><input type="text" name=""/></td>';
        tr += '  <td><input type="text" name=""/></td>';
        tr += ' <td><input type="text" name=""/> </td>';
        tr += ' <td><input type="text" name=""/></td>';
        tr += ' <td><input type="text" name=""/></td>';
        tr += ' <td><input type="text" name=""/></td>';
        tr += '<td class="td_bnt1"> <div  onclick="addtr()"  class="xj_bnt xj_bnt1" > <span class="bnt_span1"><img class="bnt_img" src="/skins/img/xj.png" style=""></span>新建 </div><div class="xj_bnt xj_bnt2" onclick="deltr(this)"> <i class="ace-icon fa fa-trash-o bigger-120 orange"></i>删除 </div></td>';
        tr += '</tr>'
        $(".supplier_minx tbody").append(tr);
  


};

$(function () {

    $(".supplier_minx .short_tit").click(function () {
        alert("222");
        $(this).hide();
        if ($(this).next("input").hasClass("Supplier_inp")) {
            alert("1");
            $(this).next("input").removeClass("Supplier_inp");

        } else {
            $(this).next("input").addClass("Supplier_inp");

        }


    });

});
