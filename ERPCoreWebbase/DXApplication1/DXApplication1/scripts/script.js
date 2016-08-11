jQuery.fn.middle = function (obj) {
    if (typeof obj === "undefined") {
        obj = this.parent();
    }
    this.css({
        "position": "absolute",
        "top": (obj.height() - this.outerHeight()) / 2 + 'px'
    });
    return this;
};

jQuery.fn.center = function (obj) {
    if (typeof obj === "undefined") {
        obj = this.parent();
    }
    //alert(obj.width() + '-' + this.outerWidth());
    this.css({
        "position": "absolute",
        "left": (obj.width() - this.outerWidth()) / 2 + 'px'
    });
    return this;
};

function ValidateMaxLength(value, maxLength) {
    var _value = value;
    if (_value) {
        _value = _value.trim();
    }
    else {
        return true;
    }
    if (_value.length > maxLength) {
        return false;
    }
    return true;
}

function ValidateMinLength(value, minLength) {
    var _value = value;
    if (_value) {
        _value = _value.trim();
    }
    else {
        return false;
    }
    if (_value.length < minLength) {
        return false;
    }
    return true;
}

function ValidateLengthRange(value, minLength, maxLength) {
    var _value = value;
    if (_value) {
        _value = _value.trim();
    }
    else {
        return false;
    }
    if (_value.length < minLength || _value.length > maxLength) {
        return false;
    }
    return true;
}

function CreateClass(parentClass, properties) {
    var result = function () {
        if (result.preparing)
            return delete (result.preparing);
        if (result.ctor)
            result.ctor.apply(this);
    };

    result.prototype = {};
    if (parentClass) {
        parentClass.preparing = true;
        result.prototype = new parentClass;
        result.base = parentClass;
    }
    if (properties) {
        var ctorName = "constructor";
        for (var name in properties)
            if (name != ctorName)
                result.prototype[name] = properties[name];
        var ctor = properties[ctorName];
        if (ctor)
            result.ctor = ctor;
    }
    return result;
}

var Utils = {
    AttachShortcut: function (combination, callback, opt) {
        shortcut.add(combination, callback, opt);
    },
    AttachShortcutTo: function (target, combination, callback) {
        this.AttachShortcut(combination, callback, { 'target': target });
    },
    AttachStandardShortcutToGridview: function (gridview) {
        var htmlGridviewObject = gridview.GetMainElement();
        //Enter
        this.AttachShortcutTo(htmlGridviewObject, "Ctrl+Enter", function () {
            gridview.UpdateEdit();
            gridview.Focus();
        });
        //F2
        this.AttachShortcutTo(htmlGridviewObject, "F2", function () {
            var focusedRowIndex = gridview.GetFocusedRowIndex();
            gridview.StartEditRow(focusedRowIndex);
            gridview.Focus();
        });
        //Esc
        this.AttachShortcutTo(htmlGridviewObject, "Esc", function () {
            gridview.CancelEdit();
            gridview.Focus();
        });
        //Insert
        this.AttachShortcutTo(htmlGridviewObject, "Insert", function () {
            gridview.AddNewRow();
            gridview.Focus();
        });
        //Delete
        this.AttachShortcut("Delete", function () {
            var focusedRowIndex = gridview.GetFocusedRowIndex();
            var isConfirm = confirm('Bạn có chắc chắn muốn xóa bản ghi này không?');
            if (isConfirm) {
                gridview.DeleteRow(focusedRowIndex);
            }
            gridview.Focus();
        },
        {
            'disable_in_input': true,
            'target': htmlGridviewObject
        });
    }
};

var UtilsForTreeList = {

    RemoveShortcut: function (combination) {
        shortcut.remove(combination);
    },
    AttachShortcut: function (combination, callback, opt) {
        shortcut.add(combination, callback, opt);
    },
    AttachShortcutTo: function (target, combination, callback) {
        this.AttachShortcut(combination, callback, { 'target': target });
    },
    AttachStandardShortcutToTreeList: function (treelist) {
        var htmlTreeListObject = treelist.GetMainElement();
        //Ctrl+Insert to insert root node
        this.AttachShortcutTo(htmlTreeListObject, "Ctrl+Insert", function () {
            treelist.StartEditNewNode();
            treelist.Focus();
        });
        //Ctrl+Enter to save
        this.AttachShortcutTo(htmlTreeListObject, "Ctrl+Enter", function () {
            treelist.UpdateEdit();
            treelist.Focus();
        });
        //F2 to edit
        this.AttachShortcutTo(htmlTreeListObject, "F2", function () {
            var focusedNodeKey = treelist.GetFocusedNodeKey();
            treelist.StartEdit(focusedNodeKey);
            treelist.Focus();
        });
        //Esc to exit 
        this.AttachShortcutTo(htmlTreeListObject, "Esc", function () {
            treelist.CancelEdit();
            treelist.Focus();
        });
        //Insert ti insert node
        this.AttachShortcutTo(htmlTreeListObject, "Insert", function () {
            var focusedNodeKey = treelist.GetFocusedNodeKey();
            treelist.StartEditNewNode(focusedNodeKey);
            treelist.Focus();
        });
        //Delete to delete node
        this.AttachShortcut("Delete", function () {
            var focusedNodeKey = treelist.GetFocusedNodeKey();
            var isConfirm = confirm('Bạn có chắc chắn muốn xóa bản ghi này không?');
            if (isConfirm) {
                treelist.DeleteNode(focusedNodeKey);
            }
            treelist.Focus();
        },
        {
            'disable_in_input': true,
            'target': htmlTreeListObject,
            'propagate': false
        });
    }
};

NASClientEvent = CreateClass(null, {
    constructor: function () {
        this.handlerList = [];
    },
    AddHandler : function (handler) {
        this.handlerList.push(handler);
    },
    FireEvent: function (obj, args) {
        for (var i = 0; i < this.handlerList.length; i++) {
            var handler = this.handlerList[i];
            handler.call(obj, args);
        }
    }
});

PageModuleBase = CreateClass(null, {

    MinDesktopLandscapeSize: 1024,
    PendingCallbacks: {},

    DoCallback: function (sender, callback) {
        if (sender.InCallback()) {
            ERPCore.PendingCallbacks[sender.name] = callback;
            sender.EndCallback.RemoveHandler(ERPCore.DoEndCallback);
            sender.EndCallback.AddHandler(ERPCore.DoEndCallback);
        } else {
            callback();
        }
    },

    DoEndCallback: function (s, e) {
        var pendingCallback = ERPCore.PendingCallbacks[s.name];
        if (pendingCallback) {
            pendingCallback();
            delete ERPCore.PendingCallbacks[s.name];
        }
    },

    ChangeState: function (view, command, key) {
        var prev = this.State;
        var current = { View: view, Command: command, Key: key };

        if (prev && current && prev.View == current.View && prev.Command == current.Command && prev.Key == current.Key)
            return;

        this.State = current;
        this.OnStateChanged();
        this.ShowMenuItems();
    },

    OnStateChanged: function () { },
    ShowMenuItems: function () { },

    Adjust: function () {
        var isLandscape = ERPCore.GetIsLandscapeOrientation();
        if (!window.ClientLayoutSplitter || ERPCore.PrevIsLandscape === isLandscape)
            return;

        if (isLandscape)
            $('body').removeClass("Portrait").addClass("Landscape");
        else
            $('body').removeClass("Landscape").addClass("Portrait");

        //ERPCore.ChangeLeftPaneExpandedState(false);
        ERPCore.ChangeLeftPaneExpandedState(isLandscape);
        ERPCore.PrevIsLandscape = isLandscape;

        //ERPCore.WindowResize_Adjust();
    },

    ChangeLeftPaneExpandedState: function (expand) {
        var leftPane = ClientLayoutSplitter.GetPaneByName("LeftPane");
        var rightPane = ClientLayoutSplitter.GetPaneByName("RightPane");
        if (expand)
            leftPane.Expand(rightPane);
        else
            leftPane.Collapse(rightPane);

        ClientExpandPaneImage.SetVisible(!expand);
        ClientCollapsePaneImage.SetVisible(expand);
    },

    GetIsLandscapeOrientation: function () {
        if (ASPxClientUtils.touchUI)
            return ASPxClientTouchUI.getIsLandscapeOrientation();
        else
            return ASPxClientUtils.GetDocumentClientWidth() >= this.MinDesktopLandscapeSize;
    },

    // Site master

    ClientActionMenu_ItemClick: function (s, e) { },

    //Implement this method to adjust controls in specified page
    PartialAdjust: function () { },

    WindowResize_Adjust: function (s, e) {
        //ASPxClientControl.AdjustControls();
        //this.PartialAdjust();
    },

    ClientLayoutSplitter_PaneResized: function (s, e) {
        ERPCore.WindowResize_Adjust(s, e);
    },

    ClientCollapsePaneImage_Click: function (s, e) {
        ERPCore.ChangeLeftPaneExpandedState(false);
        //ERPCore.WindowResize_Adjust();
    },

    ClientExpandPaneImage_Click: function (s, e) {
        ERPCore.ChangeLeftPaneExpandedState(true);
        //ERPCore.WindowResize_Adjust();
    },

    ClientInfoMenu_ItemClick: function (s, e) {
        if (e.item.parent && e.item.parent.name == "theme") {
            ASPxClientUtils.SetCookie("ERPCoreCurrentTheme", e.item.name || "");
            e.processOnServer = true;
        }
    },

    ClientSearchBox_KeyPress: function (s, e) {
        e = e.htmlEvent;
        if (e.keyCode === 13) {
            // prevent default browser form submission
            if (e.preventDefault)
                e.preventDefault();
            else
                e.returnValue = false;
        }
    },

    ClientSearchBox_KeyDown: function (s, e) {
        window.clearTimeout(ERPCore.searchBoxTimer);
        ERPCore.searchBoxTimer = window.setTimeout(function () {
            ERPCore.OnSearchTextChanged();
        }, 1200);
    },

    ClientSearchBox_TextChanged: function (s, e) {
        ERPCore.OnSearchTextChanged();
    },

    OnSearchTextChanged: function () {
        window.clearTimeout(ERPCore.searchBoxTimer);
        var searchText = ClientSearchBox.GetText();
        if (ClientHiddenField.Get("SearchText") == searchText)
            return true;
        ClientHiddenField.Set("SearchText", searchText);
    },

    ClearSearchBox: function () {
        ClientHiddenField.Set("SearchText", "");
        ClientSearchBox.SetText("");
    },

    ShowLoadingPanel: function (element) {
        this.loadingPanelTimer = window.setTimeout(function () {
            ClientLoadingPanel.ShowInElement(element);
        }, 500);
    },

    HideLoadingPanel: function () {
        if (this.loadingPanelTimer > -1) {
            window.clearTimeout(this.loadingPanelTimer);
            this.loadingPanelTimer = -1;
        }
        ClientLoadingPanel.Hide();
    },

    PostponeAction: function (action, canExecute) {
        var f = function () {
            if (!canExecute())
                window.setTimeout(f, 50);
            else
                action();
        };
        f();
    },

    SideNavBar_CollapseAll: function () {
        try {
            navSideBar.CollapseAll();
        } catch (e) {

        }
    },

    SideNavBar_ExpandAll: function () {
        try {
            navSideBar.ExpandAll();
        } catch (e) {

        }
    },

    ShowReportPopupByReportCode: function (reportCode, reportTitle, args) {
        rpvHf.Set('ReportCode', reportCode);
        pcReportViewerPopup.Show();
        pcReportViewerPopup.SetHeaderText(reportTitle);
        rpvReportViewerPopup.Refresh();
        //alert(rpvHf.Get('ReportCode'));
        //pcReportViewerPopup.PerformCallback();
    },

    SideNavBar_ExpandSelectedGroup: function () {
        try {
            ERPCore.SideNavBar_CollapseAll();
            var selectedItem = navSideBar.GetSelectedItem();
            if (selectedItem != null)
                selectedItem.group.SetExpanded(true);
        } catch (e) {

        }
    },

    SubmitContainer_Expand: function () {
        var pane = MainContentSplitter.GetPaneByName("SubmitContainer");
        if (pane.IsCollapsed()) {
            pane.Expand();
        }
    },

    SubmitContainer_Collapse: function () {
        var pane = MainContentSplitter.GetPaneByName("SubmitContainer");
        if (!pane.IsCollapsed()) {
            pane.Collapse(pane);
        }
    },

    GetContentPane: function () {
        var pane = MainContentSplitter.GetPaneByName("ContentContainer");
        return pane;
    },

    RaiseMainPaneResizeEvent: function () {
        ClientLayoutSplitter.GetPaneByName("MainPane").RaiseResizedEvent();
    }


});

SystemPageModule = CreateClass(PageModuleBase, {
    constructor: function () {
        this.State = { View: "MailForm" };
    },
    ShowMenuItems: function () {
    }
});


(function () {
    var pageModule;
    var bodyElement = $("body");
    if (bodyElement.hasClass("system"))
        pageModule = new SystemPageModule();
    else
        pageModule = new PageModuleBase();
    window.ERPCore = pageModule;
})();

SubmitContainer = function () {

    SubmitContainer.prototype.init = function () {
        try {
            $("#submitcontainer").width(MainContentSplitter.GetWidth());
            $("#submitcontainer").height(MainContentSplitter.GetPaneByName("SubmitContainer").GetSize());
            $("#submitcontainer").addClass("dxpcFooter_" + ASPxClientUtils.GetCookie("ERPCoreCurrentTheme"));

            var innerH = 0;
            $("#submitcontainer_inner").children(":not(script)").each(function () {
                if ($(this).height() > innerH) {
                    innerH = $(this).height();
                }
            });
            $("#submitcontainer_inner").height(innerH);
            $("#submitcontainer_inner").middle();
        } catch (e) {
            console.log('error');
        }
    };
    SubmitContainer.prototype.adjust = function () {
        try {
            $("#submitcontainer").width(MainContentSplitter.GetWidth());
        } catch (e) {
            console.log('error');
        }
    };

};

EditFormPopup = CreateClass(null, {
    Show: function (recordId) { throw new Error("Not implemented"); },
    Save: function () { throw new Error("Not implemented"); },
    Hide: function () { throw new Error("Not implemented"); },
    Help: function () { throw new Error("Not implemented"); },
    SetHeaderText: function (text) { throw new Error("Not implemented"); }
});

var submitContainer = new SubmitContainer();
$(document).ready(function () {
    submitContainer.init();
    $(".abscenter").each(function () {
        $(this).center();
    });
    $(".absmiddle").each(function () {
        $(this).middle();
    });
    ASPxClientUtils.AttachEventToElement(window, "resize", ERPCore.Adjust);
    if (ASPxClientUtils.touchUI) {
        ASPxClientUtils.AttachEventToElement(window, "orientationchange", function () {
            ASPxClientTouchUI.ensureOrientationChanged(ERPCore.Adjust);
        }, false);
    }
    ERPCore.Adjust();
    ERPCore.ShowMenuItems();
    var bodyElement = $("body");
    if (!bodyElement.hasClass("default")) {
        ERPCore.SideNavBar_ExpandSelectedGroup();
    }
    $('[class^="Sprite_"]').css("vertical-align", "middle");
    $('ul[class^="dxnbLite"].nav-left li:first div[class^="dxnb-header"]').css("border-top", "0px");
});


