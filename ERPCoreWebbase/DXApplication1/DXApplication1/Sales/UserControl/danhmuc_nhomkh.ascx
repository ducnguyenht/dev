<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="danhmuc_nhomkh.ascx.cs" Inherits="DXApplication1.GUI.danhmuc_nhomkh" %>             
<%@ Register src="~/Sales/UserControl/uEditCustomerGrp.ascx" tagname="uEditCustomerGrp" tagprefix="uc1" %>
<script type="text/javascript">
    function grd_customergrp_custombtn_click(s, e) {
        if (e.buttonID == 'edit_customergrp' || e.buttonID == 'add_customergrp')
            popup_input_customerGroup.Show();

        if (e.buttonID == 'showCommonDetail')
            popupCommonDetailInfo.Show();
    }    
</script>

<uc1:uEditCustomerGrp ID="uEditCustomerGrp1" runat="server"/>

