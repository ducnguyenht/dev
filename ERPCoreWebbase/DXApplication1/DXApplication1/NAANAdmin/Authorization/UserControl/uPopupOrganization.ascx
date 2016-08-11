<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPopupOrganization.ascx.cs" Inherits="WebModule.NAANAdmin.Authorization.UserControl.uPopupOrganization" %>
<script type="text/javascript">
    var OrganizationClass =
    {
        transitions: {
            tEdit: 'Edit',
            tCreate: 'Create',
            tSave: 'Save',
            tCancel: 'Cancel'
        },

        events: {
            eClosing: 'closing'
        },

        // method
        Show: function (parentKey, nodeKey) {
            var params = '';
            if (nodeKey) {
                params = this.transitions.tEdit + ',' + key;
            }
            if (!parentKey) {
                params = this.transitions.tCreate;
            }
            // Callback
            this.Callback(params);
        },

        Save: function () {
            var params = this.transitions.tSave;
            var validated = ASPxClientEdit.ValidateEditorsInContainer(popup_Organization.GetMainElement());
			console.log(validated);
            if (validated)
			{
                // Callback
                this.Callback(params);
            }
        },

        DepartmentChanged: function() {
            var params = this.transitions.tEdit + ',' + "DepartmentChange";
            this.Callback(params);
        },

		Callback: function (params) {
			if (!cpPopupOrganization.InCallback()) {
				// Callback
				cpPopupOrganization.PerformCallback(params);
			}
        },

        BindClosingEvent: function (callback) {
            $(OrganizationClass).on(this.events.eClosing, callback);
        },

        Closing: function (s, e) {
            $(OrganizationClass).triggerHandler(OrganizationClass.events.eClosing);
        },

        EndCallback: function (args) {
            
        }
    };

    function btnOrganEditCancel_Click(s, e) {
        popup_Organization.Hide();
    }

    function btnOrganEditSave_Click(s, e) {
        OrganizationClass.Save();
    }

    function Department2_FocusedNodeChanged(s, e) {
        OrganizationClass.DepartmentChanged();
    }

//	function ASPxTreeListDepartment_Callback(s,e)
//	{
//		//
//		ASPxTreeList_Department2.PerformCallback();
//	}

	function ASPxButtonNewPerson_Click(s, e) {
	    if (ASPxTreeList_Department2.GetVisibleNodeKeys().length == 0) {
	        alert('Không thể thêm người dùng vì chưa có phòng ban trong tổ chức');
	        return;
	    }
		ASPxCallbackPanel_PersonClass.Show();
	}

	function ASPxGriviewPerson_CustomButton(s, e)
	{
		console.log(e.buttonID);
		switch (e.buttonID) {
		    case "button_NewPerson":
		        if (ASPxTreeList_Department2.GetVisibleNodeKeys().length == 0) {
		            alert('Không thể thêm người dùng vì chưa có phòng ban trong tổ chức');
		            return;
		        }
		        ASPxCallbackPanel_PersonClass.Show();
		        break;
			case "button_EditPerson":
			var key = s.GetRowKey(e.visibleIndex);
				console.log(key);
				ASPxCallbackPanel_PersonClass.Show(key);
				break;
		}
	}
    function btn_Helper_Click(s,e){
        window.open("https://www.youtube.com/watch?v=JwKf7AVhUTo", "_blank");
    }

    $(document).ready(function () {
        ASPxCallbackPanel_PersonClass.BindClosingEvent(function (event) {
            //grdReceiptVouches.Refresh();
            OrganizationClass.DepartmentChanged();
        });
    });

</script>
<dx:ASPxCallbackPanel ID="cpPopupOrganization" runat="server" OnCallback="cpOrganization_Callback">
	<PanelCollection>
		<dx:PanelContent ID="PanelContentPopup" runat="server">
			<dx:ASPxPopupControl ID="popup_Organization" runat="server" Maximized="false" RenderMode="Lightweight"
				ClientInstanceName="popup_Organization" HeaderText="Thông tin tổ chức" ShowMaximizeButton="true"
				ShowFooter="True" Width="860px" Height="500px" CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter"
				PopupVerticalAlign="WindowCenter" Modal="True" AllowDragging="True" AllowResize="True">
                <ClientSideEvents Closing="OrganizationClass.Closing" />
				<ContentCollection>
					<dx:PopupControlContentControl runat="server">
						<!-- GENERAL INFOMATION -->
						<dx:ASPxFormLayout ID="form_Organization" runat="server" Width="100%" DataSourceID="XpoOrganization">
							<Items>
								<dx:LayoutGroup Caption="Thông tin chung" ColCount="2">
									<Items>
										<dx:LayoutItem Caption="Mã tổ chức" FieldName="Code">
											<LayoutItemNestedControlCollection>
												<dx:LayoutItemNestedControlContainer runat="server">
													<dx:ASPxTextBox ID="txt_MaToChuc" runat="server" Width="170px">
														<ValidationSettings>
															<RequiredField IsRequired="true" ErrorText="Chưa nhập mã tổ chức" />
														</ValidationSettings>
													</dx:ASPxTextBox>
												</dx:LayoutItemNestedControlContainer>
											</LayoutItemNestedControlCollection>
										</dx:LayoutItem>
										<dx:LayoutItem Caption="Tên tổ chức" FieldName="Name">
											<LayoutItemNestedControlCollection>
												<dx:LayoutItemNestedControlContainer runat="server">
													<dx:ASPxTextBox ID="txt_TenToChuc" runat="server" Width="100%">
														<ValidationSettings>
															<RequiredField IsRequired="true" ErrorText="Chưa nhập tên tổ chức" />
														</ValidationSettings>
													</dx:ASPxTextBox>
												</dx:LayoutItemNestedControlContainer>
											</LayoutItemNestedControlCollection>
										</dx:LayoutItem>
										<dx:LayoutItem Caption="Mã số thuế" FieldName="TaxNumber">
											<LayoutItemNestedControlCollection>
												<dx:LayoutItemNestedControlContainer runat="server">
													<dx:ASPxTextBox ID="txt_MaSoThue" runat="server" Width="170px">
														<ValidationSettings>
															<RequiredField IsRequired="true" ErrorText="Chưa nhập mã số thuế" />
														</ValidationSettings>
													</dx:ASPxTextBox>
												</dx:LayoutItemNestedControlContainer>
											</LayoutItemNestedControlCollection>
										</dx:LayoutItem>
										<dx:LayoutItem Caption="Địa chỉ" ColSpan="2" FieldName="Address">
											<LayoutItemNestedControlCollection>
												<dx:LayoutItemNestedControlContainer runat="server">
													<dx:ASPxTextBox ID="txt_DiaChi" runat="server" Width="100%">
													</dx:ASPxTextBox>
												</dx:LayoutItemNestedControlContainer>
											</LayoutItemNestedControlCollection>
										</dx:LayoutItem>
										<dx:LayoutItem Caption="Mô tả" ColSpan="2" FieldName="Description">
											<LayoutItemNestedControlCollection>
												<dx:LayoutItemNestedControlContainer runat="server">
													<dx:ASPxMemo ID="txt_MoTa" runat="server" Height="71px" Width="100%">
													</dx:ASPxMemo>
												</dx:LayoutItemNestedControlContainer>
											</LayoutItemNestedControlCollection>
										</dx:LayoutItem>
									</Items>
								</dx:LayoutGroup>
							</Items>
						</dx:ASPxFormLayout>
						<!-- END - GENERAL INFOMATION -->
						<!-- DEPARTMENT & USER MANAGEMENT INFO -->
						<dx:ASPxPageControl ID="ASPxPageControl_OrganizationTabs" runat="server" 
                            RenderMode="Classic" ActiveTabIndex="1"
							Width="100%">
							<TabPages>
								<dx:TabPage Text="Phòng ban">
									<ContentCollection>
										<dx:ContentControl runat="server">
											<dx:ASPxTreeList ID="ASPxTreeList_Department" runat="server" 
                                                AutoGenerateColumns="False" DataSourceID="XpoDepartment"
												KeyFieldName="DepartmentId" EnableCallbacks="true" ParentFieldName="ParentDepartmentId!Key" KeyboardSupport="True"
												OnNodeInserting="ASPxTreeList_Department_NodeInserting" 
                                                OnNodeValidating="ASPxTreeList_Department_NodeValidating">
												<Columns>
													<dx:TreeListTextColumn Caption="Mã phòng ban" FieldName="Code" ShowInCustomizationForm="True" VisibleIndex="0"
														Width="15%">
													</dx:TreeListTextColumn>
													<dx:TreeListTextColumn Caption="Tên phòng ban" FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="1"
														Width="20%">
													</dx:TreeListTextColumn>
													<dx:TreeListTextColumn Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="2"
														Width="55%">
													</dx:TreeListTextColumn>
													<dx:TreeListCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="3"
														Width="10%" ShowNewButtonInHeader="true">
														<EditButton Visible="true">
															<Image>
																<SpriteProperties CssClass="Sprite_Edit" />
															</Image>
														</EditButton>
														<NewButton Visible="true">
															<Image>
																<SpriteProperties CssClass="Sprite_New" />
															</Image>
														</NewButton>
														<DeleteButton Visible="true">
															<Image>
																<SpriteProperties CssClass="Sprite_Delete" />
															</Image>
														</DeleteButton>
														<UpdateButton>
															<Image>
																<SpriteProperties CssClass="Sprite_Apply" />
															</Image>
														</UpdateButton>
														<CancelButton>
															<Image>
																<SpriteProperties CssClass="Sprite_Cancel" />
															</Image>
														</CancelButton>
													</dx:TreeListCommandColumn>
												</Columns>
												<SettingsEditing AllowNodeDragDrop="True" Mode="EditFormAndDisplayNode" />
												<ClientSideEvents EndCallback="function(s, e){ ASPxTreeList_Department2.PerformCallback(); }" />
											</dx:ASPxTreeList>
										</dx:ContentControl>
									</ContentCollection>
								</dx:TabPage>
								<dx:TabPage Text="Người dùng">
									<ContentCollection>
										<dx:ContentControl runat="server">
											<dx:ASPxSplitter ID="ASPxSplitter1" Width="100%" runat="server">
												<Panes>
													<dx:SplitterPane Size="300" MaxSize="400" ScrollBars="Auto">
														<ContentCollection>
															<dx:SplitterContentControl runat="server">
																<dx:ASPxTreeList ID="ASPxTreeList_Department2" runat="server" 
                                                                    AutoGenerateColumns="False" DataSourceID="XpoDepartment"
																	KeyFieldName="DepartmentId" ParentFieldName="ParentDepartmentId!Key" Width="100%" KeyboardSupport="true"
																	ClientInstanceName="ASPxTreeList_Department2" OnCustomCallback="ASPxTreeList_Department2_CustomCallback">
																	<SettingsBehavior AutoExpandAllNodes="True" AllowFocusedNode="True" AllowDragDrop="False" FocusNodeOnExpandButtonClick="true" />
																	<Columns>
																		<dx:TreeListTextColumn Caption="Phòng ban" FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="2">
																		</dx:TreeListTextColumn>
																	</Columns>
																	<ClientSideEvents FocusedNodeChanged="Department2_FocusedNodeChanged"/>
																</dx:ASPxTreeList>
															</dx:SplitterContentControl>
														</ContentCollection>
													</dx:SplitterPane>
													<dx:SplitterPane>
														<ContentCollection>
															<dx:SplitterContentControl runat="server">
																<dx:ASPxGridView ID="ASPxGridView_Person" runat="server" Width="100%" ClientInstanceName="ASPxGridView_Person"
																	KeyFieldName="PersonId" KeyboardSupport="true" OnRowInserting="ASPxGridView_Person_RowInserting"
																	OnRowDeleting="ASPxGridView_Person_RowDeleting">
																	<SettingsBehavior AllowFocusedRow="true" ConfirmDelete="true" />
																	<SettingsEditing Mode="EditFormAndDisplayRow" />
																	<Settings ShowFilterBar="Visible" ShowFilterRow="True" ShowFilterRowMenu="True" />
																	<SettingsText ConfirmDelete="Bạn có chắc chắn muốn xóa?" FilterBarCreateFilter="Tạo bộ lọc" EmptyDataRow="Không có dữ liệu" />
																	<ClientSideEvents CustomButtonClick="ASPxGriviewPerson_CustomButton" />
																	<Templates>
																		<EmptyDataRow>
																			<div style="width: 100%;">
																				<dx:ASPxButton ID="ASPxButton_NewPerson" AutoPostBack="false" runat="server" CssClass="float_center"
																					Image-SpriteProperties-CssClass="Sprite_New" ClientInstanceName="ASPxButton_NewPerson" BackgroundImage-Repeat="NoRepeat">
																					<ClientSideEvents Click="ASPxButtonNewPerson_Click" />
																				</dx:ASPxButton>
																			</div>
																		</EmptyDataRow>
																	</Templates>
																	<Columns>
																		<dx:GridViewDataTextColumn Caption="Mã người dùng" FieldName="Code" VisibleIndex="1" Width="30%">
																		</dx:GridViewDataTextColumn>
																		<dx:GridViewDataTextColumn Caption="Họ và tên" VisibleIndex="2" FieldName="Name" Width="45%">
																		</dx:GridViewDataTextColumn>
																		<dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="3" Width="45%" 
                                                                            Caption="Mô tả">
																		</dx:GridViewDataTextColumn>
																		<dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="RowStatus" VisibleIndex="4" Width="15%">
																			<HeaderStyle HorizontalAlign="Center" />
																			<PropertiesComboBox>
																				<Items>
																					<dx:ListEditItem Text="Đang hoạt động" Value="1" />
																					<dx:ListEditItem Text="Tạm ngưng" Value="2" />
																				</Items>
																			</PropertiesComboBox>
																		</dx:GridViewDataComboBoxColumn>
																		<dx:GridViewCommandColumn Caption="Thao tác" ButtonType="Image" Width="10%" Visible="true" VisibleIndex="5">
																			<HeaderStyle HorizontalAlign="Center" />
																			<CustomButtons>
																				<dx:GridViewCommandColumnCustomButton ID="button_NewPerson">
																					<Image>
																						<SpriteProperties CssClass="Sprite_New" />
																					</Image>
																				</dx:GridViewCommandColumnCustomButton>
																				<dx:GridViewCommandColumnCustomButton ID="button_EditPerson">
																					<Image>
																						<SpriteProperties CssClass="Sprite_Edit" />
																					</Image>
																				</dx:GridViewCommandColumnCustomButton>
																			</CustomButtons>
																			<DeleteButton Visible="true">
																				<Image>
																					<SpriteProperties CssClass="Sprite_Delete" />
																				</Image>
																			</DeleteButton>
																		</dx:GridViewCommandColumn>
																	</Columns>
																</dx:ASPxGridView>
															</dx:SplitterContentControl>
														</ContentCollection>
													</dx:SplitterPane>
												</Panes>
											</dx:ASPxSplitter>
										</dx:ContentControl>
									</ContentCollection>
								</dx:TabPage>
							</TabPages>
						</dx:ASPxPageControl>
						<!-- END - DEPARTMENT & USER MANAGEMENT INFO -->
					</dx:PopupControlContentControl>
				</ContentCollection>
				<FooterTemplate>
					<div style="padding: 10px;">
						<div class="float-left">
							<div class="float-left">
								<!-- Places button here -->
								<dx:ASPxButton ID="btn_Helper" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btn_Helper" Text="Trợ Giúp"
									Wrap="False" CausesValidation="False">
									<ClientSideEvents Click="btn_Helper_Click" />
									<Image>
										<SpriteProperties CssClass="Sprite_Help" />
									</Image>
								</dx:ASPxButton>
							</div>
							<div class="float-left button-left-margin">
								<!-- Places button here -->
							</div>
						</div>
						<div class="float-right">
							<div class="float-left">
								<!-- Places button here -->
								<dx:ASPxButton ID="btnOrganEditSave" AutoPostBack="false" runat="server" Text="Lưu lại" Wrap="False"
									ClientInstanceName="btnOrganEditSave" CausesValidation="true">
									<Image>
										<SpriteProperties CssClass="Sprite_Apply" />
									</Image>
									<ClientSideEvents Click="btnOrganEditSave_Click" />
								</dx:ASPxButton>
							</div>
							<div class="float-left button-left-margin">
								<!-- Places button here -->
								<dx:ASPxButton ID="btnOrganEditCancel" runat="server" AutoPostBack="False" ClientInstanceName="btnOrganEditCancel"
									Text="Thoát" Wrap="False" CausesValidation="false">
									<Image>
										<SpriteProperties CssClass="Sprite_Cancel" />
									</Image>
									<ClientSideEvents Click="btnOrganEditCancel_Click" />
								</dx:ASPxButton>
							</div>
						</div>
						<div class="clear">
						</div>
					</div>
				</FooterTemplate>
			</dx:ASPxPopupControl>
		</dx:PanelContent>
	</PanelCollection>
</dx:ASPxCallbackPanel>
<!-- XPO Provider -->
<%--
XpoDepartment Criteria : [RowStatus] > 0 And [OrganizationId] = ?
XpoOrganization Criteria: "[RowStatus] > 0 And [OrganizationId] = ?
--%>
<dx:XpoDataSource ID="XpoDepartment" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Department"
	Criteria="[RowStatus] > 0 And [OrganizationId] = ?">
	<CriteriaParameters>
		<asp:SessionParameter Name="OrganizationId" SessionField="uPopupOrganization_OrganizationId" />
	</CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="XpoOrganization" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Organization"
	Criteria="[RowStatus] > 0 And [OrganizationId] = ?">
	<CriteriaParameters>
		<asp:SessionParameter Name="OrganizationId" SessionField="uPopupOrganization_OrganizationId" />
	</CriteriaParameters>
</dx:XpoDataSource>
<!-- END - XPO Provider -->
