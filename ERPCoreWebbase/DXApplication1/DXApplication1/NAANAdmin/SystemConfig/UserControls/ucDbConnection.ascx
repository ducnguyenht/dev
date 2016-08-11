<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDbConnection.ascx.cs"
    Inherits="WebModule.NAANAdmin.SystemConfig.UserControls.ucDbConnection" %>
<script type="text/javascript">
    //    $(document).ready(function () {
    //        $("#<%= pagDbConnection.ClientID %>").css("min-height","257px");
    //    });
    var DbConfig = {
        Reset: function (s, e) {
            var args = 'reset';
            pagDbConnection.PerformCallback(args);
        },
        Save: function (s, e) {
            var args = 'save';
            if (ASPxClientEdit.AreEditorsValid("frmMSSQLDbConfig")) {
                pagDbConnection.PerformCallback(args);
                $(DbConfig).triggerHandler('inprocess');
            }
        },
        CheckConnection: function (s, e) {
            var args = 'check';
            if (ASPxClientEdit.AreEditorsValid("frmMSSQLDbConfig")) {
                pagDbConnection.PerformCallback(args);
                $(DbConfig).triggerHandler('inprocess');
            }
        },
        Populate: function(s, e)
        {
            var args = 'populate';
            if (ASPxClientEdit.AreEditorsValid("frmMSSQLDbConfig")) {
                pagDbConnection.PerformCallback(args);
                $(DbConfig).triggerHandler('inprocess');
            }
        },
        EndCallback: function (s, e) {
            if (s.cpSaveOk == 'true') {
                alert('Lưu cấu hình thành công');
            }
            else if (s.cpSaveOk == 'false') {
                alert('Lưu cấu hình thất bại');
            }
            if (s.cpCheckOk == 'true') {
                alert('Kết nối thành công');
            }
            else if (s.cpCheckOk == 'false') {
                alert('Kết nối thất bại');
            }
            if (s.cpPopulateOk == 'true') {
                alert('Cập nhật dữ liệu đầu thành công');
            }
            else if (s.cpPopulateOk == 'false') {
                alert('Cập nhật dữ liệu đầu thất bại');
            }
            $(DbConfig).triggerHandler('endprocess');
            delete s.cpSaveOk;
            delete s.cpCheckOk;
            delete s.cpPopulateOk;
        }
    };
</script>
<%--<div style="min-height: 300px; height: 100%; padding:0 10px;">--%>
    <dx:ASPxPageControl ID="pagDbConnection" runat="server" ActiveTabIndex="0"
        ClientInstanceName="pagDbConnection" RenderMode="Lightweight" 
    Width="100%" oncallback="pagDbConnection_Callback">
        <TabPages>
            <dx:TabPage Name="mssql" Text="MS SQL">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="frmMSSQLDbConfig" ClientInstanceName="frmMSSQLDbConfig" runat="server" Width="50%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin cấu hình" GroupBoxDecoration="HeadingLine">
                                    <Items>
                                        <dx:LayoutItem Caption="Máy chủ CSDL" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtMSSQLServer" runat="server" Width="170px">
                                                        <ValidationSettings ErrorDisplayMode="Text">
                                                            <RequiredField IsRequired="True" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Chế độ xác thực">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxRadioButton ID="radWindowsAuth" runat="server" Checked="True" GroupName="auth"
                                                        Text="Xác thực Windows">
                                                        <ClientSideEvents Init="function(s,e) {
                                                        txtMSSQLUsername.SetEnabled(!s.GetChecked());
                                                        txtMSSQLPassword.SetEnabled(!s.GetChecked());
                                                    }" CheckedChanged="function(s,e) {
                                                        txtMSSQLUsername.SetEnabled(!s.GetChecked());
                                                        txtMSSQLPassword.SetEnabled(!s.GetChecked());
                                                    }" />
                                                    </dx:ASPxRadioButton>
                                                    <dx:ASPxRadioButton ID="radSQLServerAuth" runat="server" GroupName="auth" Text="Xác thực SQL Server">
                                                    </dx:ASPxRadioButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tên đăng nhập" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtMSSQLUsername" ClientInstanceName="txtMSSQLUsername" runat="server"
                                                        Width="170px">
                                                        <ValidationSettings ErrorDisplayMode="Text">
                                                            <RequiredField IsRequired="True" />
                                                        </ValidationSettings>
                                                        <DisabledStyle BackColor="#EEEEEE">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mật khẩu">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtMSSQLPassword" runat="server" ClientInstanceName="txtMSSQLPassword"
                                                        Password="true" Width="170px" OnPreRender="txtMSSQLPassword_PreRender">
                                                        <DisabledStyle BackColor="#EEEEEE">
                                                        </DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tên CSDL" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtMSSQLDbName" runat="server" Width="170px">
                                                        <ValidationSettings ErrorDisplayMode="Text">
                                                            <RequiredField IsRequired="True" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="mysql" Text="MySQL" Visible="False">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Width="50%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin cấu hình" GroupBoxDecoration="HeadingLine">
                                    <Items>
                                        <dx:LayoutItem Caption="Máy chủ CSDL" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout2_E6" runat="server" Width="170px">
                                                        <ValidationSettings ErrorDisplayMode="Text">
                                                            <RequiredField IsRequired="True" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tài đăng nhập" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout2_E8" runat="server" Width="170px">
                                                        <ValidationSettings ErrorDisplayMode="Text">
                                                            <RequiredField IsRequired="True" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mật khẩu">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout2_E9" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tên CSDL" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout2_E10" runat="server" Width="170px">
                                                        <ValidationSettings ErrorDisplayMode="Text">
                                                            <RequiredField IsRequired="True" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <ContentStyle>
            <BorderLeft BorderWidth="0px" />
            <BorderRight BorderWidth="0px" />
            <BorderBottom BorderWidth="0px" />
        </ContentStyle>
        <ClientSideEvents EndCallback="DbConfig.EndCallback" />
    </dx:ASPxPageControl>
<%--</div>--%><%--<dx:ASPxPanel ID="pnSubmit" runat="server">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxButton CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="false" ID="btnReset" ClientInstanceName="btnResetDbConfig" CssClass="float-left" runat="server" Text="Khôi phục">
                <Image>
                    <SpriteProperties CssClass="Sprite_Refresh" />
                </Image>
            </dx:ASPxButton>
            <dx:ASPxButton ID="btnSave" AutoPostBack="true" UseSubmitBehavior="true" ClientInstanceName="btnSaveDbConfig" CssClass="float-left button-left-margin" runat="server"
                Text="Lưu cấu hình">
                <Image>
                    <SpriteProperties CssClass="Sprite_Apply" />
                </Image>
            </dx:ASPxButton>
            <div class="clear">
            </div>
        </dx:PanelContent>
    </PanelCollection>
    <Paddings PaddingTop="10" />
</dx:ASPxPanel>--%>
