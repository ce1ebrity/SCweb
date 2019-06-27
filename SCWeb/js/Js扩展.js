function FormatJsonTime(date) {
    if (date != null) {
        var de = new Date(parseInt(date.replace("/Date(", "").replace(")/", "").split("+")[0]));
        var y = de.getFullYear();
        var m = de.getMonth() + 1;
        var d = de.getDate();
        var h = de.getHours();
        var mi = de.getMinutes();
        var s = de.getSeconds();
        return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d) + ' ' + (h < 10 ? ('0' + h) : h) + ':' + (mi < 10 ? ('0' + mi) : mi) + ':' + (s < 10 ? ('0' + s) : s);
    }
    else {
        return "";
    }
}
//格式化日期
function FormatJsonDate(date) {
    if (date != null) {
        var de = new Date(parseInt(date.replace("/Date(", "").replace(")/", "").split("+")[0]));
        var y = de.getFullYear();
        var m = de.getMonth() + 1;
        var d = de.getDate();
        return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
    }
    else {
        return "";
    }
}
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return (r[2]);
    return null;
}  

function Re()
{
    location.href = "/MlghsMessage/Index/"
}
function ReCMT() {
    location.href = "/CPCMT/Index/"
}

