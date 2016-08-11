<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uEvaluantCalculator.ascx.cs" Inherits="WebModule.UserControls.uEvaluantCalculator" %>
<script type="text/javascript">
    function uEvaluantCalculator_GetSelectionInfo(inputElement) {
    var start, end;
    if (ASPxClientUtils.ie) {
        var range = document.selection.createRange();
        var rangeCopy = range.duplicate();
        range.move('character', -inputElement.value.length);
        range.setEndPoint('EndToStart', rangeCopy);
        start = range.text.length;
        end = start + rangeCopy.text.length;
    } else {
        start = inputElement.selectionStart;
        end = inputElement.selectionEnd;
    }
    return { startPos: start, endPos: end };
    }
    function uEvaluantCalculator_InsertText(edit, text) {
        var inputElement = edit.GetInputElement();
        var selectionInfo = uEvaluantCalculator_GetSelectionInfo(inputElement);
        var s = edit.GetText();
        s = s.substring(0, selectionInfo.startPos) + text + s.substring(selectionInfo.endPos, s.length);
        edit.SetText(s);
        //edit.SetSelection(selectionInfo.startPos, selectionInfo.endPos, true);
        edit.SetCaretPosition(s.length  )
    }

    function uEvaluantCalculator_InsertTextByClick(s, e) {
        uEvaluantCalculator_InsertText(txtFormulaExpress, s.GetText());
    }

</script>

<dx:aspxformlayout runat="server" ID="FormCaculator" Width="100%">
    <Items>
        <dx:LayoutGroup ShowCaption="False" Width="100%">
            <Items>
                <dx:LayoutItem Caption="Công thức giá" ShowCaption="true">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxTextBox ID="txtFormulaExpress" ClientInstanceName="txtFormulaExpress" 
                                runat="server" ReadOnly="false" Width="70%" OnValidation="txtFormulaExpress_Validation">
                                <ValidationSettings ErrorText="" ValidationGroup="groupConfigFormula">
                                    <RequiredField ErrorText="Chưa nhập công thức tính giá" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutGroup ColCount="1" ShowCaption="False" Width="100%" GroupBoxDecoration="None">
                    <Items>
                        <dx:LayoutGroup ShowCaption="False" Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Phép tính" ColCount="4" Width="260px" GroupBoxDecoration="None">
                                    <Items>
                                        <dx:LayoutItem ShowCaption="false" Caption="Phép tính" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E1" runat="server" Text="(" Width="40px" AutoPostBack="false"> 
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E2" runat="server" Text=")" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E3" runat="server" Text="/" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E4" runat="server" Text="*" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E5" runat="server" Text="7" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E6" runat="server" Text="8" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E7" runat="server" Text="9" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E8" runat="server" Text="-" Width="40px" 
                                                        AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E9" runat="server" Text="4" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E10" runat="server" Text="5" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E11" runat="server" Text="6" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px" RowSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E12" runat="server" Text="+" Width="40px" Height="56px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E13" runat="server" Text="1" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E14" runat="server" Text="2" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E15" runat="server" Text="3" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E16" runat="server" Text="0" Width="105px" 
                                                        AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E17" runat="server" Text="." Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxButton ID="FormCaculator_E18" runat="server" Text="^" Width="40px" AutoPostBack="false">
                                                        <ClientSideEvents Click="uEvaluantCalculator_InsertTextByClick" />
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="Tham số về thuế" Width="100%">
                            <Items>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="grdVariableOfTax" runat="server" 
                                                AutoGenerateColumns="False" DataSourceID="TaxTypeXDS" 
                                                KeyFieldName="Code" Settings-ShowColumnHeaders="true"
                                                OnHtmlDataCellPrepared="grdVariableOfTax_HtmlDataCellPrepared">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Code" Caption="Mã loại" 
                                                        ShowInCustomizationForm="True" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <dx:ASPxButton ID="btnSelectVariable" runat="server" AutoPostBack="false" Width="100%">
                                                            </dx:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Name" Caption="Thuế" 
                                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="TaxTypeId" Caption="TaxTypeId" 
                                                        ShowInCustomizationForm="True" VisibleIndex="2" Visible="false">
                                                    </dx:GridViewDataTextColumn>
                                                    <%--ss
                                                    <%--<dx:GridViewDataTextColumn FieldName="Percentage" Caption="Tỉ lệ thuế (%)" 
                                                        ShowInCustomizationForm="True" VisibleIndex="3">
                                                    </dx:GridViewDataTextColumn>--%>
                                                    <%--<dx:GridViewDataTextColumn Name="Type" Caption="Cách tính" 
                                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                                        <DataItemTemplate>
                                                            <dx:ASPxRadioButton ID="rdbCaculateByValue" Checked="true" Text="Tính theo giá trị (VNĐ)" runat="server" GroupName="TypeOfGrid">
                                                            </dx:ASPxRadioButton>
                                                            <br />
                                                            <dx:ASPxRadioButton ID="rdbCaculateByRate" Text="Tính theo %" runat="server" GroupName="TypeOfGrid">
                                                            </dx:ASPxRadioButton>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="Value" Caption="Giá trị" 
                                                        ShowInCustomizationForm="True" VisibleIndex="3">
                                                        <DataItemTemplate>
                                                            <dx:ASPxSpinEdit ID="spinCaculateValue" runat="server" AutoPostBack="false">
                                                            </dx:ASPxSpinEdit>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataTextColumn>--%>
                                                    <dx:GridViewDataTextColumn FieldName="TaxTypeId" 
                                                        ShowInCustomizationForm="True" VisibleIndex="4" Visible="false">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsBehavior AllowSort="false" />
                                            </dx:ASPxGridView>
                                            <dx:XpoDataSource ID="TaxTypeXDS" runat="server" Criteria="[RowStatus] &gt; 0" 
                                                TypeName="NAS.DAL.Invoice.TaxType">
                                            </dx:XpoDataSource>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="Tham số về giá" Width="100%">
                            <Items>
                                <dx:LayoutItem ShowCaption="false" Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                                <dx:ASPxButton ID="btnSelectFirstPrice" runat="server" Text="[COGS]: Giá vốn" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s, e){
                                                        uEvaluantCalculator_InsertText(txtFormulaExpress, '[COGS]');
                                                    }" />
                                                        </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:LayoutGroup>
    </Items>
</dx:aspxformlayout>
