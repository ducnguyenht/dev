function ShowNextSession() {
    CallbackPanel.PerformCallback('Next');
}
function ShowPrevSession() {
    CallbackPanel.PerformCallback('Prev');
}
//Registration
function OnBackButtonClick() {
/////////////////////// Duc.Vo modify 04/09/2013/////////////////////////START
    var currentTab = pc.GetTab(pc.GetActiveTabIndex());
    var previousTab = pc.GetTab(pc.GetActiveTabIndex() - 1);
    currentTab.SetEnabled(false);
    previousTab.SetEnabled(true);
    pc.SetActiveTab(previousTab);
    dxpError.SetVisible(false);
    UpdateButtonsEnabled();
    /////////////////////// Duc.Vo modify 04/09/2013/////////////////////////END
}
function OnNextButtonClick(s, e) {
    var tabName = pc.GetActiveTab().name;
    var areEditorsValid = ASPxClientEdit.ValidateEditorsInContainerById(tabName);
    if (areEditorsValid) {
/////////////////////// Duc.Vo modify 04/09/2013/////////////////////////START
        var nextTab = pc.GetTab(pc.GetActiveTabIndex() + 1);
        var currentTab = pc.GetTab(pc.GetActiveTabIndex());
        currentTab.SetEnabled(false);
        nextTab.SetEnabled(true);
        pc.SetActiveTab(nextTab);
/////////////////////// Duc.Vo modify 04/09/2013/////////////////////////END
    }
    dxpError.SetVisible(!areEditorsValid);
    UpdateButtonsEnabled();    
}
function OnFinishButtonClick(s, e) {
    var finishTab = pc.GetTabByName('Finish');
    DisableRegistrationTabs();
    finishTab.SetEnabled(true);
    pc.SetActiveTab(finishTab);
    UpdateButtonsEnabled();
}

function DisableRegistrationTabs() {
    var tabIndex = 0;
    while(pc.GetTab(tabIndex).name != 'Finish')
        pc.GetTab(tabIndex++).SetEnabled(false);
}
function OnActiveTabChanging(s, e) {
    e.reloadContentOnCallback = (e.tab.name == 'Confirmation');
}
function UpdateButtonsEnabled() {
/////////////////////// Duc.Vo modify 04/09/2013/////////////////////////START
//    var tabName = pc.GetActiveTab().name;
//    btnBack.SetVisible(tabName != 'Personal' && tabName != 'Finish');
//    var isNextAllow = (tabName != 'Confirmation' && tabName != 'Finish');
//    btnNext.SetVisible(isNextAllow);
//    btnNext.SetEnabled(isNextAllow);
//    if(isNextAllow)
//        btnNext.Focus();
    //    btnFinish.SetVisible(tabName == 'Confirmation');
    var IdxOfLatsTab = pc.GetTabCount() - 1;
    if (pc.GetActiveTabIndex() == IdxOfLatsTab) {
        btnNext.SetClientVisible(false);
        btnFinish.SetClientVisible(true);
    } else {
        btnNext.SetClientVisible(true);
        btnFinish.SetClientVisible(false);
    }

    if (pc.GetActiveTabIndex() == 0)
        btnBack.SetClientVisible(false);
    else
        btnBack.SetClientVisible(true);
    /////////////////////// Duc.Vo modify 04/09/2013/////////////////////////END
}
function UpdateFullName() {
    SetRegField('FullName', (GetRegField('FirstName') + ' ' + GetRegField('LastName')));
}
function SaveRegField(sender, fieldName) {    
    SetRegField(fieldName, sender.GetValue());
}
function LoadRegField(sender, fieldName) {
    return sender.SetText(GetRegField(fieldName));
}

function GetRegField(fieldName) {
    return jQuery.parseJSON(GetRegData())[fieldName];
}
function SetRegField(fieldName, value) {
    var dataString = GetRegData();
    var data = jQuery.parseJSON(dataString);
    data[fieldName] = value;
    SaveRegData(data);
}

function GetRegData() {
    return hfRegInfo.Get('RegData').toString();
}
function SaveRegData(value) {
    hfRegInfo.Set('RegData', _aspxToJson(value));
}

//Validation
function IsDateEditorValueSetted(editor) {
    return !!(parseInt(editor.GetValue()));
}
function IsExpirationDateFilled() {
    return IsDateEditorValueSetted(cmbCardExpirationMonth) && IsDateEditorValueSetted(cmbCardExpirationYear);
}
function OnCardExpirationDateValidation(s, e, field) {
    if (!IsExpirationDateFilled())
        e.isValid = IsDateEditorValueSetted(s);
    else {
        e.isValid = ValidateCardExpirationDate();
        var otherDateField = field == "Month"
            ? cmbCardExpirationYear
            : cmbCardExpirationMonth;
        otherDateField.SetIsValid(e.isValid);
        UpdateEditorCssClassIe(otherDateField, e.isValid);
    }    
    UpdateEditorCssClassIe(s, e.isValid);
    OnValidation(s, e);
}
function ValidateCardExpirationDate() {
    var date = new Date();
    return cmbCardExpirationYear.GetValue() > date.getFullYear() ||
        cmbCardExpirationMonth.GetValue() >= date.getMonth() + 1;
}
function OnValidation(s,e) { 
    if(!e.isValid) {
        if(pc.GetActiveTab().name == 'Personal')
            pc.GetTabByName('Payment').SetEnabled(false); 
        pc.GetTabByName('Confirmation').SetEnabled(false); 
        dxpError.SetVisible(true);
    }
    UpdateEditorCssClassIe(s, e.isValid);
}

function UpdateEditorCssClassIe(editor, isValid) {
    if (ASPxClientUtils.ie && ASPxClientUtils.browserMajorVersion < 9) {
        var ieInValidCssClass = " invalidEditorIE";
        var className = editor.GetMainElement().className;
        if (!isValid && className.indexOf(ieInValidCssClass) == -1)
            editor.GetMainElement().className = className.concat(ieInValidCssClass);
        if (isValid && className.indexOf(ieInValidCssClass) != -1)
            editor.GetMainElement().className = className.replace(ieInValidCssClass, "");
    }
}

//schedule
function ChangeSheduleDay(s, e) {    
    ASPxScheduler1.GotoDate(new Date(Date.parse(s.tabs[e.tab.index].name)));
}
function SheduleDayTabClick(s, e) {
    e.cancel = ASPxScheduler1.InCallback();    
}
function ShowSelectedAppointmentDetails() {
    ASPxScheduler1.ShowAppointmentFormByServerId(ASPxScheduler1.GetSelectedAppointmentIds()[0]);
}
function ChangeAppointmentLabel(aptId, value) {
    apt = ASPxScheduler1.GetAppointmentById(aptId);
    apt.SetLabelId(value);
    ASPxScheduler1.UpdateAppointment(apt);
}
function ResizeSwitcher(s, e) {
    var sizes = [115, 143, 93];
    s.SetWidth(sizes[s.GetValue()]);
}
function OnReportToolbarItemValueChanged(s, e) {
    cbChangePrintingStatus.PerformCallback(s.GetValue());
}
function OnEndChangePrintingStatusCallback(s, e) {
    ClientReportViewer.Refresh();
}