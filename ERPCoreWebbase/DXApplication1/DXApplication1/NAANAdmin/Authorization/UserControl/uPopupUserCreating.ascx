<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPopupUserCreating.ascx.cs" Inherits="WebModule.NAANAdmin.Authorization.UserControl.uPopupUserCreating" %>
<script type="text/javascript">
	var ASPxCallbackPanel_PersonClass =
	{
		transitions: {
			tCreate: 'Create',
			tEdit: 'Edit',
			tSave: 'Save'
            
		},


        events: {
			eClosing: 'closing'
		},
        

		Show: function (key) {
			var params = '';
			if (key)
				params += this.transitions.tEdit + ',' + key;
			else
				params += this.transitions.tCreate;
			this.Callback(params);
		},

		Save: function () {
			var params = this.transitions.tSave;
			var validated = ASPxClientEdit.ValidateEditorsInContainer(popup_PersonCreate.GetMainElement(), null, true);
			if (validated){
                if (ASPxTreeList_OfDepartment.GetVisibleSelectedNodeKeys().length <= 0)
                {
                    alert('Người dùng bắt buộc phải thuộc ít nhất một phòng ban');
                    return; 
                }
				this.Callback(params);
            }
		},

		Callback: function (params) {
			if (!popup_PersonCreate.InCallback()) {
				// Callback
				ASPxCallbackPanel_PopupPerson.PerformCallback(params);
			}
		},

		EndCallback: function(s, e)
		{
			if (s.cpIsSaved) {
                delete s.cpIsSaved;
                alert('Đã cập nhật thông tin của người dùng');
            }

        },


        BindClosingEvent: function (callback) {
            $(ASPxCallbackPanel_PersonClass).on(this.events.eClosing, callback);

        },

        Closing: function (s, e) {
            $(ASPxCallbackPanel_PersonClass).triggerHandler(ASPxCallbackPanel_PersonClass.events.eClosing);
        }
	};

	function ASPxButton_EditSave_Click(s, e) {
		ASPxCallbackPanel_PersonClass.Save();
	}

	function ASPxButton_Cancel_Click(s, e) {
		popup_PersonCreate.Hide();

	}

	//	function ASPxGriviewPerson_CustomButton(s, e) {
	//		console.log(buttonID);
	//		console.log(ASPxGridView_LoginAccount.GetSelectedKeysOnPage());
	//		switch (buttonID) {
	//			case "button_NewPerson":
	//				ASPxCallbackPanel_PersonClass.Show();
	//				break;
	//			case "button_EditPerson":
	//				ASPxCallbackPanel_PersonClass.Show(ASPxGridView_LoginAccount.GetSelectedKeysOnPage());
	//				break;
	//		}
	//	}

	//DND 862
	function ASPxGridView_LoginAccount_Init(s, e) {
	    Utils.AttachStandardShortcutToGridview(s);
	    s.GetMainElement().focus();
	}
    //END DND 862
</script>
<dx:ASPxCallbackPanel ID="ASPxCallbackPanel_PopupPerson" runat="server" ShowLoadingPanel="false" ClientInstanceName="ASPxCallbackPanel_PopupPerson"
	Width="100%" OnCallback="ASPxCallbackPanel_PopupPerson_Callback">
    <ClientSideEvents EndCallback="ASPxCallbackPanel_PersonClass.EndCallback" />
	<PanelCollection>
		<dx:PanelContent ID="PanelContent1" runat="server">
			<div id="lineContainerUnit">
				<dx:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
					Modal="True" AllowDragging="True" AllowResize="True" ScrollBars="Auto" HeaderText="Thông Tin Người Dùng - "
					ShowMaximizeButton="True" Height="600px" CloseAction="CloseButton" ID="popup_PersonCreate" Maximized="true"
					ShowFooter="true" Width="800px">
                    <ClientSideEvents Closing="ASPxCallbackPanel_PersonClass.Closing" />
					<ContentCollection>
						<dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
							<dx:ASPxFormLayout ID="frmPersonEdit" runat="server" AlignItemCaptionsInAllGroups="True" EnableTheming="True"
								Width="100%">
								<Items>
									<dx:LayoutGroup Caption="Thông tin chung" ShowCaption="true">
										<Items>
											<dx:LayoutItem Caption="Mã người dùng" HelpText="Tối đa 128 ký tự, , không cho phép trùng lắp" FieldName="Code"
												RequiredMarkDisplayMode="Required">
												<LayoutItemNestedControlCollection>
													<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
														<dx:ASPxTextBox ID="txt_Code" runat="server" ClientInstanceName="txt_Code" Width="200px" 
															OnValidation="txt_Code_Validation">
															<NullTextStyle ForeColor="Silver">
															</NullTextStyle>
															<ValidationSettings ErrorText="" SetFocusOnError="false">
																<RequiredField IsRequired="True" ErrorText="Chưa nhập mã người dùng"></RequiredField>
																<RegularExpression ValidationExpression="^[A-Za-z0-9]{1}[A-Za-z0-9_-]{0,35}$" ErrorText="Mã người dùng không đúng định dạng" />
															</ValidationSettings>
														</dx:ASPxTextBox>
													</dx:LayoutItemNestedControlContainer>
												</LayoutItemNestedControlCollection>
												<CaptionCellStyle CssClass="CaptionStyle">
												</CaptionCellStyle>
											</dx:LayoutItem>
											<dx:LayoutItem Caption="Họ và tên" FieldName="Name" HelpText="255 ký tự">
												<LayoutItemNestedControlCollection>
													<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
														<dx:ASPxTextBox ID="txt_Name" runat="server" ClientInstanceName="txt_Name" MaxLength="255" Width="400px">
															<NullTextStyle ForeColor="Silver">
															</NullTextStyle>
															<ValidationSettings ErrorText="" SetFocusOnError="false">
																<RequiredField IsRequired="True" ErrorText="Chưa nhập tên người dùng"></RequiredField>
															</ValidationSettings>
														</dx:ASPxTextBox>
													</dx:LayoutItemNestedControlContainer>
												</LayoutItemNestedControlCollection>
											</dx:LayoutItem>
											<dx:LayoutItem Caption="Trạng Thái" FieldName="RowStatus" Visible="true">
												<LayoutItemNestedControlCollection>
													<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
														<dx:ASPxComboBox ID="Combo_RowStatus" runat="server" ClientInstanceName="Combo_RowStatus" Width="200px">
															<Items>
																<dx:ListEditItem Text="Sử dụng" Value="1" />
																<dx:ListEditItem Text="Tạm ngưng" Value="-1" />
															</Items>
														</dx:ASPxComboBox>
													</dx:LayoutItemNestedControlContainer>
												</LayoutItemNestedControlCollection>
											</dx:LayoutItem>
										</Items>
									</dx:LayoutGroup>
									<dx:LayoutGroup Caption="Thông tin đăng nhập và tổ chức" ShowCaption="true">
										<Items>
											<dx:LayoutItem Caption="Email đăng nhập">
												<LayoutItemNestedControlCollection>
													<dx:LayoutItemNestedControlContainer>
														<dx:ASPxGridView ID="ASPxGridView_LoginAccount" runat="server" Width="100%" AutoGenerateColumns="False"
															KeyFieldName="LoginAccountId" KeyboardSupport="true" ClientInstanceName="ASPxGridView_LoginAccount"
															OnRowInserting="ASPxGridView_LoginAccount_RowInserting" OnRowUpdating="ASPxGridView_LoginAccount_RowUpdating"
															OnDataBinding="ASPxGridView_LoginAccount_DataBinding" OnRowDeleting="ASPxGridView_LoginAccount_RowDeleting" 
                                                            OnCellEditorInitialize="ASPxGridView_LoginAccount_CellEditorInitialize">
															<ClientSideEvents Init="ASPxGridView_LoginAccount_Init" CustomButtonClick="ASPxGriviewPerson_CustomButton" />
															<Columns>
																<dx:GridViewDataTextColumn Name="txt_Email"  FieldName="Email" ShowInCustomizationForm="True" VisibleIndex="0" Width="75%">
																	<Settings AllowSort="False" />
																</dx:GridViewDataTextColumn>
																<dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="1" Caption="Thao tác"
																	Width="15%">
																	<NewButton Visible="true">
																		<Image>
																			<SpriteProperties CssClass="Sprite_New" />
																		</Image>
																	</NewButton>
																	<EditButton Visible="true">
																		<Image>
																			<SpriteProperties CssClass="Sprite_Edit" />
																		</Image>
																	</EditButton>
																	<DeleteButton Visible="True">
																		<Image>
																			<SpriteProperties CssClass="Sprite_Delete" />
																		</Image>
																	</DeleteButton>
																	<UpdateButton>
																		<Image>
																			<SpriteProperties CssClass="Sprite_Apply" />
																		</Image>
																	</UpdateButton>
																	<CancelButton Visible="true">
																		<Image>
																			<SpriteProperties CssClass="Sprite_Cancel" />
																		</Image>
																	</CancelButton>
																	<HeaderStyle HorizontalAlign="Center" />
																</dx:GridViewCommandColumn>
															</Columns>
															<SettingsBehavior AllowFocusedRow="true" ConfirmDelete="true" />
															<SettingsText EmptyDataRow="Chưa nhập email" ConfirmDelete="Bạn có muốn xóa email này" />
															<SettingsEditing Mode="Inline" />
														</dx:ASPxGridView>
													</dx:LayoutItemNestedControlContainer>
												</LayoutItemNestedControlCollection>
											</dx:LayoutItem>
											<dx:LayoutItem Caption="Thuộc phòng ban" ShowCaption="True">
												<LayoutItemNestedControlCollection>
													<dx:LayoutItemNestedControlContainer>
														<dx:ASPxTreeList ID="ASPxTreeList_OfDepartment" runat="server" AutoGenerateColumns="False" DataSourceID="XpoDepartment"
															KeyFieldName="DepartmentId" ParentFieldName="ParentDepartmentId!Key" Width="100%" ClientInstanceName="ASPxTreeList_OfDepartment">
															<Columns>
																<dx:TreeListTextColumn Caption="Phòng ban" FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="0">
																</dx:TreeListTextColumn>
															</Columns>
															<SettingsBehavior AllowDragDrop="False" AllowFocusedNode="True" AutoExpandAllNodes="True" FocusNodeOnExpandButtonClick="False"
																ProcessSelectionChangedOnServer="True" />
															<SettingsSelection Enabled="True" />
															<Border BorderWidth="0px" />
														</dx:ASPxTreeList>
													</dx:LayoutItemNestedControlContainer>
												</LayoutItemNestedControlCollection>
											</dx:LayoutItem>
											<dx:EmptyLayoutItem>
											</dx:EmptyLayoutItem>
										</Items>
									</dx:LayoutGroup>
								</Items>
							</dx:ASPxFormLayout>
						</dx:PopupControlContentControl>
					</ContentCollection>
					<FooterTemplate>
						<div style="padding: 10px;">
							<div class="float-left">
								<div class="float-left">
									<dx:ASPxButton ID="btnPersonEditHelp" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
										Text="Trợ Giúp" Wrap="False" ClientInstanceName="btnPersonEditHelp">
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
									<dx:ASPxButton ID="ASPxButton_EditSave" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
										ClientInstanceName="ASPxButton_EditSave" Text="Lưu lại" Wrap="False" CausesValidation="true">
										<Image>
											<SpriteProperties CssClass="Sprite_Apply" />
										</Image>
										<ClientSideEvents Click="ASPxButton_EditSave_Click" />
									</dx:ASPxButton>
								</div>
								<div class="float-left button-left-margin">
									<dx:ASPxButton ID="ASPxButton_Cancel" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
										ClientInstanceName="ASPxButton_Cancel" Text="Thoát" Wrap="False" CausesValidation="false">
										<Image>
											<SpriteProperties CssClass="Sprite_Cancel" />
										</Image>
										<ClientSideEvents Click="ASPxButton_Cancel_Click" />
									</dx:ASPxButton>
								</div>
							</div>
							<div class="clear">
							</div>
						</div>
					</FooterTemplate>
				</dx:ASPxPopupControl>
			</div>
		</dx:PanelContent>
	</PanelCollection>
</dx:ASPxCallbackPanel>
<%--<dx:XpoDataSource ID="XpoPerson" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Person" Criteria="[RowStatus] > 0 And [PersonId] = ?">
	<CriteriaParameters>
		<asp:SessionParameter Name="PersonId" SessionField="uPopupUserCreating_PersonId" />
	</CriteriaParameters>
</dx:XpoDataSource>--%>
<%--<dx:XpoDataSource ID="XpoLoginAccount" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.LoginAccount"
	Criteria="[RowStatus] > 0 And [PersonId!Key] = ?" DefaultSorting="">
	<CriteriaParameters>
		<asp:SessionParameter Name="PersonId" SessionField="uPopupUserCreating_PersonId" />
	</CriteriaParameters>
</dx:XpoDataSource>--%>
<dx:XpoDataSource ID="XpoDepartment" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Department"
	Criteria="[RowStatus] > 0 And [OrganizationId] = ?">
	<CriteriaParameters>
		<asp:SessionParameter Name="OrganizationId" SessionField="uPopupOrganization_OrganizationId" />
	</CriteriaParameters>
</dx:XpoDataSource>
