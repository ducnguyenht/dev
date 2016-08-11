<%@ Control Language="C#"  ClientIDMode="AutoID"  AutoEventWireup="true" CodeBehind="uGeneralAccount.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.uGeneralAccount" %>
<%@ Register Src="uPopupGeneralAccount.ascx" TagName="uPopupGeneralAccount" TagPrefix="uc1" %>

<%@ Register src="uChungTuGoc_2.ascx" tagname="uChungTuGoc_2" tagprefix="uc2" %>
<script type ="text/javascript">
    function CusBut(s, e) {
        if (e.buttonID == "CTG") {
            popctg.Show();
        }
        else if (e.buttonID == "New") {
            pophtc.Show();
            cphtc.PerformCallback("new");
        }
        else if (e.buttonID == "Approve") {
            pophtc.Show();
            cphtc.PerformCallback("ht");           
        }
    }
</script>
<dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Font-Bold="True" Font-Size="Medium"
    Height="55px" Text="Danh sách chứng từ" Width="251px">
    <Border BorderStyle="None" />
</dx:ASPxTextBox>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
    Width="100%">
    <ClientSideEvents CustomButtonClick = "CusBut"/>
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã chứng từ" FieldName="MCT" 
            VisibleIndex="1" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Des" VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Chứng từ liên quan" 
            VisibleIndex="3" Width="150px">
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="CTG" Text="Xem">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Document" />
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
            VisibleIndex="4" Width="150px">
            <EditButton Visible="True">
                <Image>
                    <SpriteProperties CssClass="Sprite_Edit" />
                </Image>
            </EditButton>
            <NewButton>
                <Image>
                    <SpriteProperties CssClass="Sprite_New" />
                </Image>
            </NewButton>
            <DeleteButton Visible="True">
                <Image>
                    <SpriteProperties CssClass="Sprite_Delete" />
                </Image>
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="New" Text="Tạo mới">
                    <Image>
                        <SpriteProperties CssClass="Sprite_New" />
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>
        <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" 
            VisibleIndex="0">
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Duyệt" VisibleIndex="5">
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="Approve" Text="Duyệt">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Approve" />
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>
    </Columns>
    <SettingsEditing Mode="Inline" />
    <Settings ShowFilterRow="True" />
    <Styles>
        <Header HorizontalAlign="Center">
        </Header>
    </Styles>
</dx:ASPxGridView>
<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
    <Items>
        <dx:LayoutItem ShowCaption="False">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxButton ID="ASPxFormLayout1_E1" runat="server" CssClass="float-right button-right-margin" Text="Duyệt">
                        <ClientSideEvents Click="function(s, e) {
	                    alert('Duyệt tất cả các chứng từ đã chọn');
}" 
                        />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Approve" />
                        </Image>
                    </dx:ASPxButton>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>
<uc1:uPopupGeneralAccount ID="uPopupGeneralAccount1" runat="server" />
<uc2:uChungTuGoc_2 ID="uChungTuGoc1" runat="server" />

