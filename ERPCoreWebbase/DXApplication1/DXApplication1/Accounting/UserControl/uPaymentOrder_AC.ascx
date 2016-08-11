<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPaymentOrder_AC.ascx.cs" Inherits="WebModule.Accounting.UserControl.uPaymentOrder_AC" %>
<style type="text/css">

.dxpcControl
{
	font: 12px Tahoma, Geneva, sans-serif;
	color: black;
	background-color: white;
	border: 1px solid #8B8B8B;
	width: 200px;
}
.dxpcHeader
{
	color: #404040;
	background-color: #DCDCDC;
	border-bottom: 1px solid #C9C9C9;
}
.dxpcHeader td.dxpc
{
	color: #404040;
	white-space: nowrap;
}
.dxpcHBCell {
    padding: 1px 1px 1px 2px;
}
.dxpcHBCellSys
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
    -webkit-touch-callout: none;
}

.dxWeb_pcCloseButton {
    background-position: 0px -50px;
}

.dxWeb_pcCloseButton,
.dxWeb_pcPinButton,
.dxWeb_pcRefreshButton,
.dxWeb_pcCollapseButton,
.dxWeb_pcMaximizeButton {
    width: 15px;
    height: 14px;
}
.dxWeb_rpHeaderTopLeftCorner,
.dxWeb_rpHeaderTopRightCorner,
.dxWeb_rpBottomLeftCorner,
.dxWeb_rpBottomRightCorner,
.dxWeb_rpTopLeftCorner,
.dxWeb_rpTopRightCorner,
.dxWeb_rpGroupBoxBottomLeftCorner,
.dxWeb_rpGroupBoxBottomRightCorner,
.dxWeb_rpGroupBoxTopLeftCorner,
.dxWeb_rpGroupBoxTopRightCorner,
.dxWeb_mHorizontalPopOut,
.dxWeb_mVerticalPopOut,
.dxWeb_mVerticalPopOutRtl,
.dxWeb_mSubMenuItem,
.dxWeb_mSubMenuItemChecked,
.dxWeb_mScrollUp,
.dxWeb_mScrollDown,
.dxWeb_tcScrollLeft,
.dxWeb_tcScrollRight,
.dxWeb_tcScrollLeftHover,
.dxWeb_tcScrollRightHover,
.dxWeb_tcScrollLeftPressed,
.dxWeb_tcScrollRightPressed,
.dxWeb_tcScrollLeftDisabled,
.dxWeb_tcScrollRightDisabled,
.dxWeb_nbCollapse,
.dxWeb_nbExpand,
.dxWeb_splVSeparator,
.dxWeb_splVSeparatorHover,
.dxWeb_splHSeparator,
.dxWeb_splHSeparatorHover,
.dxWeb_splVCollapseBackwardButton,
.dxWeb_splVCollapseBackwardButtonHover,
.dxWeb_splHCollapseBackwardButton,
.dxWeb_splHCollapseBackwardButtonHover,
.dxWeb_splVCollapseForwardButton,
.dxWeb_splVCollapseForwardButtonHover,
.dxWeb_splHCollapseForwardButton,
.dxWeb_splHCollapseForwardButtonHover,
.dxWeb_pcCloseButton,
.dxWeb_pcPinButton,
.dxWeb_pcRefreshButton,
.dxWeb_pcCollapseButton,
.dxWeb_pcMaximizeButton,
.dxWeb_pcSizeGrip,
.dxWeb_pcSizeGripRtl,
.dxWeb_pPopOut,
.dxWeb_pPopOutDisabled,
.dxWeb_pAll,
.dxWeb_pAllDisabled,
.dxWeb_pPrev,
.dxWeb_pPrevDisabled,
.dxWeb_pNext,
.dxWeb_pNextDisabled,
.dxWeb_pLast,
.dxWeb_pLastDisabled,
.dxWeb_pFirst,
.dxWeb_pFirstDisabled,
.dxWeb_tvColBtn,
.dxWeb_tvColBtnRtl,
.dxWeb_tvExpBtn,
.dxWeb_tvExpBtnRtl,
.dxWeb_fmFolder,
.dxWeb_fmFolderLocked,
.dxWeb_fmCreateButton,
.dxWeb_fmMoveButton,
.dxWeb_fmRenameButton,
.dxWeb_fmDeleteButton,
.dxWeb_fmRefreshButton,
.dxWeb_fmDwnlButton,
.dxWeb_fmCreateButtonDisabled,
.dxWeb_fmMoveButtonDisabled,
.dxWeb_fmRenameButtonDisabled,
.dxWeb_fmDeleteButtonDisabled,
.dxWeb_fmRefreshButtonDisabled,
.dxWeb_fmDwnlButtonDisabled,
.dxWeb_fmThumbnailCheck,
.dxWeb_ucClearButton,
.dxWeb_isPrevBtnHor,
.dxWeb_isNextBtnHor,
.dxWeb_isPrevBtnVert,
.dxWeb_isNextBtnVert,
.dxWeb_isPrevPageBtnHor,
.dxWeb_isNextPageBtnHor,
.dxWeb_isPrevPageBtnVert,
.dxWeb_isNextPageBtnVert,
.dxWeb_isPrevBtnHorDisabled,
.dxWeb_isNextBtnHorDisabled,
.dxWeb_isPrevBtnVertDisabled,
.dxWeb_isNextBtnVertDisabled,
.dxWeb_isPrevPageBtnHorDisabled,
.dxWeb_isNextPageBtnHorDisabled,
.dxWeb_isPrevPageBtnVertDisabled,
.dxWeb_isNextPageBtnVertDisabled,
.dxWeb_isDot,
.dxWeb_isDotDisabled,
.dxWeb_isDotSelected,
.dxWeb_isPlayBtn,
.dxWeb_isPauseBtn,
.dxWeb_igCloseButton,
.dxWeb_igNextButton,
.dxWeb_igPrevButton,
.dxWeb_igPlayButton,
.dxWeb_igPauseButton,
.dxWeb_igNavigationBarMarker
 {
    background-image: url('<%=WebResource("DevExpress.Web.Images.sprite.png")%>');
    background-repeat: no-repeat;
    background-color: transparent;
    display:block;
}

img
{
	border-width: 0;
}

.dxpcContent
{
	color: #010000;
	white-space: normal;
	vertical-align: top;
}
.dxpcContentPaddings 
{
	padding: 9px 12px;
}
.dxflFormLayout,
.dxflCaptionCell,
.dxflNestedControlCell,
div.dxflItem {
    font: 12px Tahoma, Geneva, sans-serif;
}

.dxflFormLayout {
    display: table;
}
.dxflGroup { padding: 6px 5px; width: 100%;}
.dxflGroup > table.dxflGroupTable { width: 100%; }
.dxflGroupCell { padding: 0 8px; }

.dxflItem { padding: 2px 0; width: 100%; }
.dxflTextEditItemSys .dxflVATSys.dxflCaptionCell { padding-top: 3px; }

.dxflCLLSys .dxflCaptionCell,
*[dir="rtl"].dxflFormLayout .dxflCLRSys .dxflCaptionCell {
    padding-left: 0px;
    padding-right: 6px;
}
.dxflCaptionCell {
    white-space: nowrap;
    line-height: 16px;
    height: 100%;
    width: 1%;
}
.dxflVATSys { vertical-align: top; }
.dxflHALSys { text-align: left; }
.dxflNestedControlCell {
    height: 0;
}

.dxflGroup tr:first-child > .dxflGroupCell > .dxflGroupBox { margin-top: 13px; }
.dxflGroupBox.dxflHeadingLineGroupBoxSys {
    border-width: 1px 0 0;
    border-radius: 0px;
}

.dxflGroupBox { 
    border: 1px Solid #9F9F9F;
    border-radius: 3px;
    padding: 0 0 12px;
    margin: 10px 0; 
}

.dxflHeadingLineGroupBoxSys > .dxflGroupBoxCaption {
    top: -19px;
}

.dxflGroupBoxCaption {
    background-color: White;
    color: #818181;
    display: inline-block;  
    left: 9px;
    line-height: 16px;
    padding: 0px 3px 0px 3px;  
    position: relative;
    top: -9px;
}

.dxflGroupBox > .dxflGroup { margin-top: -9px; padding: 0 4px; }

.dxflHeadingLineGroupBoxSys > .dxflGroupSys {
    margin-top: -6px;
}

.dxflGroupBox > .dxflGroup tr:first-child > .dxflGroupCell > .dxflItem { padding-top: 9px; }

.dxflEmptyItem {
    height: 21px;
}

.dxpControl
{
	font: 12px Tahoma, Geneva, sans-serif;
	color: black;
}
.dxpControl td.dxpCtrl 
{
	padding: 5px 2px;
}
.dxpSummary
{
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
	padding: 1px 4px 0px;
}
.dxpDisabled
{
	color: #acacac;
	border-color: #808080;
	cursor: default;
}
.dxpDisabledButton
{
	text-decoration: none;
}
.dxpButton
{
	color: #394EA2;
	text-decoration: underline;
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
}

.dxWeb_pPrevDisabled {
    background-position: -105px -25px;
    width: 16px;
    height: 17px;
}

.dxpCurrentPageNumber
{
	font-weight: bold;
	text-decoration: none;
	padding: 1px 3px 0px;
	white-space: nowrap;
}
.dxpPageNumber
{
	color: #394EA2;
	text-decoration: underline;
	text-align: center;
	vertical-align: middle;
	padding: 1px 5px 0px;
}

.dxWeb_pNextDisabled {
    background-position: -81px -25px;
    width: 16px;
    height: 17px;
}

    .float_right
    {
        float: right;
        margin-bottom: 10px;
        margin-top: 10px;
    }
</style>
    

        
