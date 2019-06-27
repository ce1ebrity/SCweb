if (typeof (window.loadjs_obj) == "undefined") {
    window.loadjs_obj = {};
}
window.loadjs_obj["/editor.js"] = true;

/*点击编辑*/
function newFunction(fnName, src, options) {
    if (fnName in window) {
        var fn = window[fnName];
        if (fn && _isFunction(fn)) {
            return new fn(src, options);
        }
    }
    return null;
}

function _editor_(src, options) {
    return this.init(src, options);
}

_extend(_editor_, {
    options: {
        type: "",
        oid: -1,
        pid: -1
    },
    dataTypes: {
        number: function (value) {
            return /^[1-9][0-9]*$/.test(value);
        },
        number1: function (value) {
            return /^[1-9][0-9]*$/.test(value);
        }
    },
    init: function (src, options) {
        var me = this;
        me.src = $(src);
        this.options = jQuery.extend({}, me.options, options);
    },
    post: function (data) {
        var me = this;
        loadJs("js/other/ajax.js", function () {
            _PostAjax(me.options.action, data, function (msg) {
                me.postCallback(msg);
            });
        })
    },
    postCallback: function (msg) {
        var me = this;
        var data = _Json(msg);
        if (data.error) {
            me.input.attr("defvalue", me.input.val());
            if (me.text) {
                me.text.text(me.input.val());
            }
            _alert("更新成功！", true);
        } else {
            _alert(data.msg);
            me.input.val(me.input.attr("defvalue"));
        }
    }
});

function input_editor(src, options) {
    return this.init(src, options);
}

//比如双击修改排序
_extend(input_editor, _editor_, {
    init: function (src, options) {
        var me = this;
        me.src = $(src);
        //options.widht = 20;
        input_editor.parent.init.call(me, src, options);
        var op = me.options;
        var text = me.src.text();

        me.src.html('<i style="font-style:normal;"><i>');
        me.text = $(me.src).find("i").text(text);
        //alert(me.src.parent().width());
        //op.widht = op.widht || me.src.parent().width() - 5;
        me.src.append('<input type="text" style="border:0;width:90%;height:' + me.src.height() + 'px;line-height:' + me.src.height() + 'px;display:none;border:1px solid #ccc;" />');
        me.input = $(me.src).find("input").val(text).attr("defvalue", text).hide();
        me.src.bind("click", function () {
            me.input.data("editor", true);
            me.input.show().focus();
            me.text.hide();
        })
        me.input.bind("blur", function () {
            if (!me.input.data("editor")) {
                return;
            }
            me.input.hide();
            var value = me.input.val();
            if (value != me.input.attr("defvalue")) {
                value = value.trim();
                if (me.formatValidation(value)) {
                    var data = {};
                    data.OID = op.oid;
                    data.value = value;
                    data.typename = op.type;
                    me.post(data);
                } else {
                    me.input.val(me.input.attr("defvalue"));
                }
            }
            me.text.show();
        })
    },
    formatValidation: function (value) {
        var me = this;
        var op = me.options;
        if (op.dataType && me.dataTypes[op.dataType]) {
            var fn = me.dataTypes[op.dataType];
            if (_isFunction(fn)) {
                return fn.call(me, value);
            }
        }
        return true;
    }
});

function select_editor(src, options) {
    return this.init(src, options);
}
//select修改，比如直接修改分类
_extend(select_editor, _editor_, {
    init: function (src, options) {
        var me = this;
        me.src = $(src);
        select_editor.parent.init.call(me, src, options);

        var op = me.options;
        op.value = op.value || "";
        if (op.source) {
            var source = $(op.source);
            //alert(op.source)
            if (source.length) {
                me.src.append(source.clone());
                me.input = me.src.find("select"); //.css("width", "120%");
                me.input.parent().show();
                if (me.input.children(":first").attr("oid")) {
                    me.input.children().each(function () {
                        $(this).attr("value", $(this).attr("oid"));
                    });
                }
                me.input.val(op.value);
                me.input.attr("defvalue", op.value);
                me.input.bind("change", function () {
                    if (me.input.val() == me.input.attr("defvalue")) {
                        return;
                    }
                    _confirm("确定修改？", function () {
                        var data = {};
                        data.pid = me.input.val();
                        data.oid = op.oid;
                        data.typename = op.type;

                        me.post(data);
                    }, function () {
                        me.input.val(me.input.attr("defvalue"));
                    })
                })
            }
        }
    }
});

function delete_editor(src, options) {
    return this.init(src, options);
}

var MessageTools = {
    "product": "产品",
    "productcolumn": "产品分类",
    "project": "方案",
    "projectcolumn": "方案分类",
    "news": "资讯",
    "newscolumn": "资讯分类",
    "help": "帮助",
    "helpcolumn": "帮助分类",
    "help": "加盟",
    "helpcolumn": "加盟分类",
    "productattributecolumn": "产品属性",
    "projectattributecolumn": "方案属性",
    "admin_systemmenu":"系统菜单"
};

//删除按钮
_extend(delete_editor, _editor_, {
    init: function (src, options) {
        var me = this;
        me.src = $(src);
        select_editor.parent.init.call(me, src, options);
        var op = me.options;
        op.tips = op.tips || "确定删除";
        op.type = op.type || "-";
        me.src.bind("click", function () {
            _confirm(op.tips + MessageTools[op.type] + "？", function () {
                me.post(options);
            })
        })
    },
    postCallback: function (data) {
        data = _Json(data);
        var me = this;
        if (data.error) {
            me.src.parents("tr").remove();
            _alert(data.msg, true);
        } else {
            _alert(data.msg);
        }
    }
});
/*点击结束*/

function img_gallerywindow(src, options) {
    return this.init(src, options);
}

//单个图片上传
_extend(img_gallerywindow, _editor_, {
    init: function (src, options) {
        var me = this;
        me.src = $(src);
        select_editor.parent.init.call(me, src, options);
        var op = me.options;
        if (!op.init) {
            op.input = op.input ? $j(op.input) : null;
            loadJs("js/common/popup.js", function () {
                me.src.bind("click", function () {
                    if (!me.pop) {
                        var data = me.src.attr("data-src") ? _Json(me.src.attr("data-src")) : {};
                        var iframes = {
                            updateCallback: me.UpdateCallback
                        };
                        iframes.src = "GalleryWindow.aspx";
                        if (op.max) {
                            iframes.src += "?max=" + op.max;
                        }
                        if (op.directory) {
                            iframes.src += (iframes.src.indexOf('?') > 0 ? "&" : "?") + "directory=" + op.directory;
                        }
                        if (op.popcallback) {
                            data.callback = op.popcallback;
                        }
                        if (op.iframecallback) {
                            iframes.callback = op.iframecallback;
                        }
                        me.pop = GalleryWindow(data, iframes);
                        me.pop.pop.data("_this", me);
                    } else {
                        me.pop.pop.show();
                    }
                });
            });
            me.ShowImg();
            return me;
        } else {
            return op.init.call(me);
        }
    },
    ShowImg: function () {
        var me = this;
        var op = me.options;
        if (op.input) {
            var url = op.input.val();
            if (url != "") {
                var ext = url.substr(url.lastIndexOf('.') + 1).toLowerCase();
                if (",ico,jpg,png,gif,bmp,jpeg,".indexOf(ext) > 0) {//图片
                    var img = null;
                    var warp = null;
                    this.options.imgwarp = warp = !op.imgwarp ? me.src.siblings(".insertimg_list_li") : op.imgwarp.jquery ? op.imgwarp : me.src.parent().find(op.imgwarp);
                    var img = warp.show().find("img").show();
                    if (img.length) {
                        img.attr("src", url);
                    } else {
                        if (!warp || !warp.length) {
                            me.src.after('<div class="insertimg_list_li"><img src="' + url + '" height="' + me.src.outerHeight() + 'px" class="show_" /><i class="v_edit"></i><i class="v_del"></i></div>');
                            this.options.imgwarp = warp = me.src.siblings(".insertimg_list_li").show();
                        }
                        img = warp.find("img").show();
                        warp.find("i.v_edit").unbind("click").bind("click", function () {
                            me.src.click();
                        })
                        warp.find("i.v_del").unbind("click").bind("click", function () {
                            img.hide().attr("src", "/js/grey.gif");
                            warp.hide();
                            me.src.show();
                            op.input.val("");
                        })
                    }

                    if (img && img.length) {
                        warp.show();
                        pictureEnlarge(img);
                        me.src.hide();
                    }
                } else if (',doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2,'.indexOf(ext) > 0) {//附件

                }
            }
        }
    },
    UpdateCallback: function (_data) {
        var _me = this;
        var me = _me.pop.data("_this");
        var op = me.options;
        var datas = _data.data;
        var update = _data.update;
        var i = 0;
        for (var data in datas) {
            data = datas[data];
            if (data.error == 0) {
                if (me.Item(data)) {
                    i++;
                }
            }
        }
        if (i > 0 && me.warp) {
            me.warp.trigger("_change_");
        }
        if (update === "False") {
            me.pop.hide();
        } else {
            var clear = _me.iframe._clear_;
            if (clear) {
                clear();
                me.pop.hide();
            }
        }
        _alert("添加成功！", true);
    },
    Item: function (data) {
        var me = this;
        if (me.options.input) {
            me.options.input.val(data.url);
            me.ShowImg();
            return true;
        }
        return false;
    }
});

function ThumbnailImage(src, options) {
    return this.init(src, options);
}

//产品大图上传
var ThumbnailImage_Index = 0;
_extend(ThumbnailImage, img_gallerywindow, {
    init: function (src, options) {
        var me = this;
        me.src = $(src);
        ThumbnailImage.parent.init.call(me, src, options);
        var op = me.options;

        op.temp = [
            '<li pictureid="{pictureid}">',
            '<a href="javascript:;"><img src="{src}" /><i class="pro_view"></i><i class="pro_del"></i></a>',
            '<input type="hidden" name="thumbnail_src" value="{src}"/>',
            '</li>'
        ].join('');
        me.warp = $(op.warp);
        me.SetDraggable(me.warp.find("li"));
    },
    draggableClass: 'draggable_',
    SetDraggable: function (lis) {
        lis = $(lis);
        if (!lis.length) return;
        var me = this;
        Timeout(function () {
            loadJs("js/easyui/jquery.easyui.min.js", function () {
                //移动对象
                var width = lis.eq(0).width();
                var maxWidth = lis.parent().parent().width();
                lis.each(function () {
                    ThumbnailImage_Index++;
                    var me = $(this);
                    me.attr("index", ThumbnailImage_Index);
                }).addClass(me.draggableClass).draggable({
                    proxy: function (source) {
                        var li = $(source).clone().addClass("no_li");
                        li = $(source).parent().append(li).find(".no_li");
                        return li;
                    },
                    onBeforeDrag: function (e) {
                        if (e && e.target) {
                            target = $(e.target);
                            if (target.length > 0 && target[0].tagName == "I") {
                                return false;
                            }
                        }
                    },
                    onStopDrag: function () {
                        $(this).draggable('options').cursor = 'move';
                    },
                    revert: true,
                    axis: 'h',
                    cursor: 'pointer'
                }).find("i.pro_view").bind("click", function () {
                    ShowImg_($(this).parent().find("img"));
                }).end().find("i.pro_del").bind("click", function () {
                    $(this).parents("li").remove();
                });
                //可放置的容器
                lis.droppable({
                    onDrop: function (e, source) {
                        var me = $(this);
                        var n = me.next(); //下一个
                        var p = me.prev(); //上一个
                        source = $(source);
                        if (source.attr("index") == n.attr("index")) {
                            source.insertBefore(me);
                        } else if (p.attr("index") == source.attr("index")) {
                            source.insertAfter(me);
                        } else {
                            me.insertAfter(source);
                            if (n.length) {
                                source.insertBefore(n);
                            } else if (p.length) {
                                source.insertAfter(p);
                            }
                        }
                    }
                });
            })
        })
    },
    ShowImg: function () { },
    Item: function (data) {
        var me = this;
        var fileID = data.fileID;
        if (!me.warp.children("[pictureid='" + fileID + "']").length) {
            var html = me.options.temp;
            data.title = data.title.replace('>', '').replace("'", "”").replace('"', "”").replace(",", "，");
            html = _allReplace(html, "{src}", data.url);
            html = _allReplace(html, "{title}", data.title);
            html = html.replace("{pictureid}", data.fileID);
            me.warp.append(html);
            me.SetDraggable(me.warp.find("li").not("." + me.draggableClass));
            return true;
        }
        return false;
    }
});
//多图片上传
function AddImg(src, options) {
    return this.init(src, options);
}
//多图片上传
_extend(AddImg, img_gallerywindow, {
    init: function (src, options) {
        var me = this;
        me.src = $(src);
        options.iframecallback = function () {
            var _me = this;
            if (_me.iframe) {
                $(".tab_ul li:last", _me.iframe.document.body).remove();
                _me.options.show = _me.options._show;
                _me.options.show.call(_me);
            }
        }
        options.popcallback = function () {
            var _me = this;
            _me.options._show = _me.options.show;
            _me.options.show = null;
        }
        AddImg.parent.init.call(me, src, options);
    },
    ShowImg: function () { },
    Item: function (data) {
        return true;
    }
});

function kindeditor_image(src, options) {
    return this.init(src, options || {});
}
//编辑器专用
_extend(kindeditor_image, img_gallerywindow, {
    init: function (src, options) {
        var me = this;
        options.init = function () {
            var me = this;
            if (!me.pop) {
                loadJs("js/common/popup.js", function () {
                    var op = me.options;
                    var iframes = {
                        updateCallback: me.UpdateCallback
                    };
                    iframes.src = "GalleryWindow.aspx";
                    if (op.max) {
                        iframes.src += "?max=" + op.max;
                    }
                    if (op.directory) {
                        iframes.src += (iframes.src.indexOf('?') > 0 ? "&" : "?") + "directory=" + op.directory;
                    }
                    me.pop = GalleryWindow({}, iframes);
                    me.pop.pop.data("_this", me);
                });
            }
        }
        kindeditor_image.parent.init.call(me, src, options);
        var op = me.options;
    },
    ShowImg: function () { },
    Item: function (data) {
        if (this.options.Item) {
            return this.options.Item.call(this, data);
        }
        return false;
    }
});


function _fileswindow(src, options) {
    return this.init(src, options);
}

//单个附件上传
_extend(_fileswindow, _editor_, {
    init: function (src, options) {
        var me = this;
        me.src = $(src);
        _fileswindow.parent.init.call(me, src, options);
        var op = me.options;
        if (!op.init) {
            op.input = op.input ? $j(op.input) : null;
            loadJs("js/common/popup.js", function () {
                me.src.bind("click", function () {
                    if (!me.pop) {
                        var data = me.src.attr("data-src") ? _Json(me.src.attr("data-src")) : {};
                        var iframes = {
                            updateCallback: me.UpdateCallback
                        };
                        iframes.src = "FilesWindow.aspx";
                        if (op.max) {
                            iframes.src += "?max=" + op.max;
                        }
                        if (op.directory) {
                            iframes.src += (iframes.src.indexOf('?') > 0 ? "&" : "?") + "directory=" + op.directory;
                        }
                        if (op.popcallback) {
                            data.callback = op.popcallback;
                        }
                        if (op.iframecallback) {
                            iframes.callback = op.iframecallback;
                        }
                        me.pop = FilesWindow(data, iframes);
                        me.pop.pop.data("_this", me);
                    } else {
                        me.pop.pop.show();
                    }
                });
                return me;
            });
            me.ShowImg();
        } else {
            return op.init.call(me);
        }
    },
    ShowImg: function () { },
    UpdateCallback: function (_data) {
        var _me = this;
        var me = _me.pop.data("_this");
        var op = me.options;
        var datas = _data.data;
        var update = _data.update;
        var i = 0;
        for (var data in datas) {
            data = datas[data];
            if (data.error == 0) {
                if (me.Item(data)) {
                    i++;
                }
            }
        }
        if (i > 0 && me.warp) {
            me.warp.trigger("_change_");
        }
        if (update === "False") {
            me.pop.hide();
        } else {
            var clear = _me.iframe._clear_;
            if (clear) {
                clear();
                me.pop.hide();
            }
        }
        _alert("添加成功！", true);
    },
    Item: function (data) {
        var me = this;
        alert("fileswindow.Item");
        if (me.options.input) {
            me.options.input.val(data.url);
            me.ShowImg();
            return true;
        }
        return false;
    }
});

//多图片上传
function AddFiles(src, options) {
    return this.init(src, options);
}
_extend(AddFiles, _fileswindow, {
    init: function (src, options) {
        var me = this;
        me.src = $(src);
        options.iframecallback = function () {
            var _me = this;
            if (_me.iframe) {
                $(".tab_ul li:last", _me.iframe.document.body).remove();
                _me.options.show = _me.options._show;
                _me.pop.show().css("visibility", "visible");
                _me.options.show.call(_me);
            }
        }
        options.popcallback = function () {
            var _me = this;
            _me.options._show = _me.options.show;
            _me.options.show = function () {
                var me = this;
                me.pop.show().css("visibility", "hidden");
            };
        }
        AddFiles.parent.init.call(me, src, options);
    },
    ShowImg: function () { },
    Item: function (data) {
        return true;
    }
});

//编辑器专用附件上传
function kindeditor_files(src, options) {
    return this.init(src, options || {});
}
_extend(kindeditor_files, _fileswindow, {
    init: function (src, options) {
        var me = this;
        options.init = function () {
            var me = this;
            if (!me.pop) {
                loadJs("js/common/popup.js", function () {
                    var op = me.options;
                    var iframes = {
                        updateCallback: me.UpdateCallback
                    };
                    iframes.src = "FilesWindow.aspx";
                    if (op.max) {
                        iframes.src += "?max=" + op.max;
                    }
                    if (op.directory) {
                        iframes.src += (iframes.src.indexOf('?') > 0 ? "&" : "?") + "directory=" + op.directory;
                    }
                    me.pop = FilesWindow({}, iframes);
                    me.pop.pop.data("_this", me);
                });
            }
        }
        kindeditor_files.parent.init.call(me, src, options);
        var op = me.options;
    },
    ShowImg: function () { },
    Item: function (data) {
        if (this.options.Item) {
            return this.options.Item.call(this, data);
        }
        return false;
    }
});

//编辑器专用图片上传
function kindeditor_selecttemps(src, options) {
    return this.init(src, options || {});
}
_extend(kindeditor_selecttemps, _editor_, {
    init: function (src, options) {
        var me = this;
        kindeditor_selecttemps.parent.init.call(me, src, options);
        var op = me.options;
        if (!me.pop) {
            loadJs("js/common/popup.js", function () {
                var op = me.options;
                var iframes = me.iframes || {};
                iframes.updateCallback = me.UpdateCallback;
                iframes.src = iframes.src || "SearchTemplates.aspx";
                if (op.max) {
                    iframes.src += "?max=" + op.max;
                }
                if (op.types) {
                    iframes.src += (iframes.src.indexOf('?') > 0 ? '&' : "?") + 'types=' + op.types;
                }
                if (op.directory) {
                    iframes.src += (iframes.src.indexOf('?') > 0 ? "&" : "?") + "directory=" + op.directory;
                }
                me.pop = SearchTemplates(op, iframes);
                me.pop.pop.data("_this", me);
            });
        }
    },
    UpdateCallback: function (_data) {
        var _me = this;
        var me = _me.pop.data("_this");
        var op = me.options;
        var datas = _data.data;
        var update = _data.update;
        var i = 0;
        //        for (var data in datas) {
        //            data = datas[data];
        //            if (data.error == 0) {
        if (me.Item(datas)) {
            i++;
        }
        //            }
        //        }
        if (i > 0 && me.warp) {
            me.warp.trigger("_change_");
        }
        if (!update) {
            me.pop.hide();
        }
    },
    ShowImg: function () { },
    Item: function (data) {
        if (this.options.Item) {
            return this.options.Item.call(this, data);
        }
        return false;
    }
});