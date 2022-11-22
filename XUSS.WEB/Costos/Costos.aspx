<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
	CodeBehind="Costos.aspx.cs" Inherits="XUSS.WEB.Costos.Costos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
	</telerik:RadScriptManager>
	<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
	</telerik:RadAjaxLoadingPanel>
	<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
		<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
			Skin="Default" />
		<telerik:RadListView ID="RadListView1" runat="server" PageSize="1" AllowPaging="True"
			ItemPlaceholderID="pnlGeneral" DataSourceID="ObjectDataSource1" OnItemCommand="RadListView1_ItemCommand"
			OnItemDataBound="RadListView1_ItemDataBound" OnItemUpdating="RadListView1_ItemUpdating">
			<LayoutTemplate>
				<fieldset class="cssFieldSetContainer">
					<div class="box">
						<div class="title">
							<h5>
								Ingreso de Costos</h5>
						</div>
					</div>
					<div class="paginadorRadListView">
						<telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="RadListView1"
							PageSize="1">
							<Fields>
								<telerik:RadDataPagerButtonField FieldType="FirstPrev" />
								<telerik:RadDataPagerButtonField FieldType="NextLast" />
								<telerik:RadDataPagerTemplatePageField HorizontalPosition="RightFloat">
									<PagerTemplate>
										<div style="float: right">
											<b>Items
												<asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.Owner.StartRowIndex+1%>" />
												en
												<asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Container.Owner.TotalRowCount > (Container.Owner.StartRowIndex+Container.Owner.PageSize) ? Container.Owner.StartRowIndex+Container.Owner.PageSize : Container.Owner.TotalRowCount %>" />
												de
												<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.Owner.TotalRowCount%>" />
												<br />
											</b>
										</div>
									</PagerTemplate>
								</telerik:RadDataPagerTemplatePageField>
							</Fields>
						</telerik:RadDataPager>
					</div>
					<asp:Panel runat="server" ID="pnlGeneral" Style="padding: 5px 5px 5px 5px">
					</asp:Panel>
				</fieldset>
			</LayoutTemplate>
			<ItemTemplate>
				<div runat="server" id="BotonesBarra" class="toolBarsMenu">
					<asp:ImageButton ID="iBtnEdit" runat="server" CommandName="Edit" Text="Modificar"
						SkinID="SkinEditUC" />
					<asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />
				</div>
				<asp:Panel ID="pnItemMaster" runat="server">
					<table cellspacing="8">
						<tr>
							<td>
								<label>
								</label>
							</td>
							<td>
								<telerik:RadTextBox ID="RadTextBox1" runat="server" Text='<%# Bind("ICCONSE") %>'
									Enabled="false" Visible="false">
								</telerik:RadTextBox>
							</td>
							<td rowspan="8">
								<telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("IM_IMAGEN") is DBNull ? null : Eval("IM_IMAGEN")%>'
									AutoAdjustImageControlSize="false" Width="300" Height="300px" ToolTip='<%#Eval("ICREFERENCIA", "Foto {0}") %>'
									AlternateText='<%#Eval("ICREFERENCIA", "Foto {0}") %>' />
							</td>
						</tr>
						<tr>
							<td>
								<label>
									Ref. Proveedor</label>
							</td>
							<td>
								<telerik:RadTextBox ID="RadTextBox2" runat="server" Text='<%# Bind("ICREFERENCIA") %>'
									Enabled="false">
								</telerik:RadTextBox>
							</td>
						</tr>
						<tr>
							<td>
								<label>
									Referencia Xuss</label>
							</td>
							<td>
								<telerik:RadTextBox ID="RadTextBox5" runat="server" Text='<%# Bind("ICCLAVE1") %>'
									Enabled="false">
								</telerik:RadTextBox>
							</td>
						</tr>
						<tr>
							<td>
								<label>
									Proveedor</label>
								<td>
									<telerik:RadComboBox ID="rcbProveedor" runat="server" SelectedValue='<%# Bind("ICPROVEE") %>'
										Culture="es-CO" Width="130px" DataSourceID="ObjectDataSource1" DataTextField="TRNOMBRE"
										Enabled="false" DataValueField="TRCODTER">
									</telerik:RadComboBox>
									<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
										SelectMethod="GetProveedor" TypeName="XUSS.BLL.Costos.CostosBL">
										<SelectParameters>
											<asp:Parameter Name="connection" Type="String" />
											<asp:Parameter Name="filter" Type="String" />
											<asp:Parameter Name="startRowIndex" Type="Int32" />
											<asp:Parameter Name="maximumRows" Type="Int32" />
										</SelectParameters>
									</asp:ObjectDataSource>
								</td>
							</td>
						</tr>
						<tr>
							<td>
								<label>
									Destino</label>
							</td>
							<td>
								<telerik:RadComboBox ID="rcbMarca" runat="server" SelectedValue='<%# Bind("ICMARCA") %>'
									Culture="es-CO" Width="130px" DataTextField="TTVALORC" Enabled="False" DataValueField="TTCODCLA"
									DataSourceID="ObjectDataSource2">
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetMarca" TypeName="XUSS.BLL.Costos.CostosBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:Parameter Name="filter" Type="String" />
										<asp:Parameter Name="startRowIndex" Type="Int32" />
										<asp:Parameter Name="maximumRows" Type="Int32" />
									</SelectParameters>
								</asp:ObjectDataSource>
							</td>
						</tr>
						<tr>
							<td>
								<label>
									Categoria</label>
							</td>
							<td>
								<telerik:RadComboBox ID="rcbTipPro" runat="server" SelectedValue='<%# Bind("ICTIPPRO") %>'
									Culture="es-CO" Width="130px" DataTextField="TANOMBRE" Enabled="False" DataValueField="TATIPPRO"
									DataSourceID="ObjectDataSource3">
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetTipPro" TypeName="XUSS.BLL.Costos.CostosBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:Parameter Name="filter" Type="String" />
										<asp:Parameter Name="startRowIndex" Type="Int32" />
										<asp:Parameter Name="maximumRows" Type="Int32" />
									</SelectParameters>
								</asp:ObjectDataSource>
							</td>
						</tr>
						<tr>
							<td>
								<label>
									Cantidad</label>
							</td>
							<td>
								<telerik:RadNumericTextBox ID="RadTextBox3" runat="server" Text='<%# Bind("ICCANTIDAD") %>'
									Enabled="false" NumberFormat-DecimalDigits="0">
								</telerik:RadNumericTextBox>
							</td>
						</tr>						
						<tr>
							<td>
								<label>
									Costo UND</label>
							</td>
							<td>
								<telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" Text='<%# Bind("ICCOSTOUUSD") %>' Culture="es-CO"
									Enabled="false" NumberFormat-PositivePattern="USD n" NumberFormat-DecimalSeparator="." Type="Currency"
									>
								</telerik:RadNumericTextBox>
							</td>
						</tr>											
						<tr>
							<td>
								<label>
									P.Venta Publico</label>
							</td>
							<td>
								<telerik:RadNumericTextBox ID="RadNumericTextBox6" runat="server" Text='<%# Bind("ICCOSTOSVT") %>'
									Enabled="false" NumberFormat-DecimalDigits="0" NumberFormat-PositivePattern="$ n"
									NumberFormat-DecimalSeparator="," NumberFormat-GroupSeparator=" ">
								</telerik:RadNumericTextBox>
							</td>
						</tr>
					</table>
				</asp:Panel>
			</ItemTemplate>
			<EmptyDataTemplate>
				<fieldset class="cssFieldSetContainer">
					<div class="box">
						<div class="title">
							<h5>
								Asignar Costos</h5>
						</div>
					</div>
					<table>
						<tr>
							<td>
								<table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
									<tr>
										<td>
											<div style="padding-top: 7px">
												<asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />
											</div>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
					<table cellspacing="8">
						<tr>
							<td>
								<label>
								Destino</label>
							</td>
							<td>
								<telerik:RadComboBox ID="rcbMarca" runat="server" Culture="es-CO" AppendDataBoundItems="true"  
									DataSourceID="ObjectDataSource2" DataTextField="TTVALORC" DataValueField="TTCODCLA" Width="130px">
									<Items>
										<telerik:RadComboBoxItem Text="Seleccione" Value="" />
									</Items>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
									OldValuesParameterFormatString="original_{0}" SelectMethod="GetMarca" 
									TypeName="XUSS.BLL.Costos.CostosBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:Parameter Name="filter" Type="String" />
										<asp:Parameter Name="startRowIndex" Type="Int32" />
										<asp:Parameter Name="maximumRows" Type="Int32" />
									</SelectParameters>
								</asp:ObjectDataSource>
							</td>
						</tr>
						<tr>
							<td>
								<label>
								Linea</label>
							</td>
							<td>
								<telerik:RadComboBox ID="rcbTipPro" runat="server" AppendDataBoundItems="true" 
									Culture="es-CO" DataSourceID="ObjectDataSource3" DataTextField="TANOMBRE" 
									DataValueField="TATIPPRO" Enabled="True" Width="130px">
									<Items>
										<telerik:RadComboBoxItem Text="Seleccione" Value="" />
									</Items>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
									OldValuesParameterFormatString="original_{0}" SelectMethod="GetTipPro" 
									TypeName="XUSS.BLL.Costos.CostosBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:Parameter Name="filter" Type="String" />
										<asp:Parameter Name="startRowIndex" Type="Int32" />
										<asp:Parameter Name="maximumRows" Type="Int32" />
									</SelectParameters>
								</asp:ObjectDataSource>
							</td>
						</tr>
						<tr>
							<td>
								<label>
								Referencia Xuss</label>
							</td>
							<td>
								<telerik:RadTextBox ID="rtbrefxuss" runat="server" Culture="es-CO" 
									Width="358px">
								</telerik:RadTextBox>
							</td>
						</tr>
						<tr>
							<td>
								<label>
								Ref. Proveedor</label>
							</td>
							<td>
								<telerik:RadTextBox ID="rdtrefImportada" runat="server" Culture="es-CO" 
									Width="358px">
								</telerik:RadTextBox>
							</td>
						</tr>
						<tr>
							<td>
								<label>
								Estado</label>
							</td>
							<td>
								<telerik:RadComboBox ID="rcbestado" runat="server" AppendDataBoundItems="True" 
									Culture="es-CO" DataSourceID="ObjectDataSource1" DataTextField="nombre" 
									DataValueField="codigo" Width="130px">
									<%--<Items>
                                        <telerik:RadComboBoxItem Value="" Text="Seleccione" />
                                    </Items>--%>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
									OldValuesParameterFormatString="original_{0}" SelectMethod="GetEstados" 
									TypeName="XUSS.BLL.Costos.CostosBL"></asp:ObjectDataSource>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Button ID="Button9" runat="server" OnClick="BuscarGrilla" Text="Buscar" />
							</td>
						</tr>
						</tr>
					</table>
					<asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
				</fieldset>
			</EmptyDataTemplate>
			<EditItemTemplate>
				<table cellspacing="8">
                    <tr>
                        <td>
                            <telerik:RadButton ID="RadButton1" runat="server" Text="RadButton" 
                                onclick="RadButton1_Click">
                            </telerik:RadButton>
                        </td>
                    </tr>
					<tr>
						<td>
							<label>
							</label>
						</td>
						<td>
							<telerik:RadTextBox ID="RadTextBox1" runat="server" Text='<%# Bind("ICCONSE") %>'
								Enabled="false" Visible="false">
							</telerik:RadTextBox>
						</td>
						<td rowspan="8">
							<telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("IM_IMAGEN") is DBNull ? null : Eval("IM_IMAGEN")%>'
								AutoAdjustImageControlSize="false" Width="300" Height="300px" ToolTip='<%#Eval("ICREFERENCIA", "Foto {0}") %>'
								AlternateText='<%#Eval("ICREFERENCIA", "Foto {0}") %>' />
						</td>
					</tr>
					<tr>
						<td>
							<telerik:RadTextBox ID="RadTextBox4" runat="server" Text='<%# Bind("ICCONSEINT") %>'
								Enabled="false" Visible="false">
							</telerik:RadTextBox>
						</td>
					</tr>
					<tr>
						<td>
							<label>
								Ref. Proveedor</label>
						</td>
						<td>
							<telerik:RadTextBox ID="RadTextBox2" runat="server" Text='<%# Bind("ICREFERENCIA") %>'
								Enabled="false">
							</telerik:RadTextBox>
						</td>
					</tr>
					<tr>
						<td>
							<label>
								Referencia Xuss</label>
						</td>
						<td>
							<telerik:RadTextBox ID="RadTextBox5" runat="server" Text='<%# Bind("ICCLAVE1") %>'
								Enabled="false">
							</telerik:RadTextBox>
						</td>
					</tr>
					<tr>
						<td>
							<label>
								Proveedor</label>
							<td>
								<telerik:RadComboBox ID="rcbProveedor" runat="server" SelectedValue='<%# Bind("ICPROVEE") %>'
									Culture="es-CO" Width="130px" DataSourceID="ObjectDataSource1" DataTextField="TRNOMBRE"
									Enabled="false" DataValueField="TRCODTER">
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetProveedor" TypeName="XUSS.BLL.Costos.CostosBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:Parameter Name="filter" Type="String" />
										<asp:Parameter Name="startRowIndex" Type="Int32" />
										<asp:Parameter Name="maximumRows" Type="Int32" />
									</SelectParameters>
								</asp:ObjectDataSource>
							</td>
						</td>
					</tr>
					<tr>
						<td>
							<label>
								Destino</label>
						</td>
						<td>
							<telerik:RadComboBox ID="rcbMarca" runat="server" SelectedValue='<%# Bind("ICMARCA") %>'
								Culture="es-CO" Width="130px" DataTextField="TTVALORC" Enabled="False" DataValueField="TTCODCLA"
								DataSourceID="ObjectDataSource2">
							</telerik:RadComboBox>
							<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
								SelectMethod="GetMarca" TypeName="XUSS.BLL.Costos.CostosBL">
								<SelectParameters>
									<asp:Parameter Name="connection" Type="String" />
									<asp:Parameter Name="filter" Type="String" />
									<asp:Parameter Name="startRowIndex" Type="Int32" />
									<asp:Parameter Name="maximumRows" Type="Int32" />
								</SelectParameters>
							</asp:ObjectDataSource>
						</td>
					</tr>
					<tr>
						<td>
							<label>
								Linea</label>
						</td>
						<td>
							<telerik:RadComboBox ID="rcbTipPro" runat="server" SelectedValue='<%# Bind("ICTIPPRO") %>'
								Culture="es-CO" Width="130px" DataTextField="TANOMBRE" Enabled="False" DataValueField="TATIPPRO"
								DataSourceID="ObjectDataSource3">
							</telerik:RadComboBox>
							<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
								SelectMethod="GetTipPro" TypeName="XUSS.BLL.Costos.CostosBL">
								<SelectParameters>
									<asp:Parameter Name="connection" Type="String" />
									<asp:Parameter Name="filter" Type="String" />
									<asp:Parameter Name="startRowIndex" Type="Int32" />
									<asp:Parameter Name="maximumRows" Type="Int32" />
								</SelectParameters>
							</asp:ObjectDataSource>
						</td>
					</tr>
					<tr>
						<td>
							<label>
								Cantidad</label>
						</td>
						<td>
							<telerik:RadNumericTextBox ID="RadTextBox3" runat="server" Text='<%# Bind("ICCANTIDAD") %>'
								Enabled="false" NumberFormat-DecimalDigits="0">
							</telerik:RadNumericTextBox>
						</td>
					</tr>					
					<tr>
						<td>
							<label>
								Costo UND</label>
						</td>
						<td>
							<telerik:RadNumericTextBox ID="RadNumericTextBox2" runat="server" Text='<%# Bind("ICCOSTOUUSD") %>' Culture="es-CO"
								Enabled="false" NumberFormat-PositivePattern="USD n" NumberFormat-DecimalSeparator="." Type="Currency">
							</telerik:RadNumericTextBox>
						</td>
					</tr>									
					<tr>
						<td>
							<label>
								P.V.Publico</label>
						</td>
						<td>
							<telerik:RadNumericTextBox ID="RadNumericTextBox6" runat="server" Text='<%# Bind("ICCOSTOSVT") %>'
								Enabled="true" NumberFormat-DecimalDigits="0" NumberFormat-PositivePattern="$ n"
								NumberFormat-DecimalSeparator="," NumberFormat-GroupSeparator=" ">
								<NumberFormat DecimalDigits="0" />
							</telerik:RadNumericTextBox>
							<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadNumericTextBox6"
								ErrorMessage="(*)">
								<asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>--%>
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" />
							<asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
						</td>
					</tr>
				</table>
			</EditItemTemplate>
		</telerik:RadListView>
	
    <div style="display: none;">
            <asp:Button ID="Button3" runat="server" Text="Button" />
        </div>
        <asp:ModalPopupExtender ID="mpPrecosteo" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="MEditTicket" PopupControlID="pnPopUpEdit" TargetControlID="Button3" CancelControlID="Button4">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnPopupEdit" runat="server" CssClass="modalPopup" Style="display: none;">
            <fieldset class="cssFieldSetContainer" style="width: 1100px !important">
                <div class="box">
                    <div class="title">
                        <h5>
                            Precosteo Prenda</h5>
                    </div>
                </div>
                <div style="width: 100%">                  
                    <table>
                        <tr align="right">
                            <td><label>Costo Prenda</label></td>
                            <td colspan="2" align="right">
                                <telerik:RadNumericTextBox ID="edt_pcostopr" runat="server" Width="200px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr align="right">
                            <td><label>Insumos</label></td>
                            <td colspan="2" align="right">
                                <telerik:RadNumericTextBox ID="edt_pinsumos" runat="server" Width="200px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr align="right">
                            <td><label>Empaque</label></td>
                            <td colspan="2" align="right">
                                <telerik:RadNumericTextBox ID="edt_pempaque" runat="server" Width="200px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><label>Mano Obra</label></td>
                            <td colspan="2" align="right">
                                <telerik:RadNumericTextBox ID="edt_pmobra" runat="server" Width="200px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><label><b>TOTAL COSTO PRIMO</b></label></td>
                            <td colspan="2" align="right">
                                <telerik:RadNumericTextBox ID="edt_ptotprimo" runat="server" Width="200px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><label>Costos Indirectos Fabricacion</label></td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_pPctFab" runat="server" Width="40px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_pVctFab" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><label>Gastos Administracion y Ventas</label></td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_pPadmonvta" runat="server" Width="40px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_pVadmonvta" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><label><b>IVA</b></label></td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_pPiva" runat="server" Width="40px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_Viva" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><label><b>TOTAL COSTO + GASTOS ADMON Y VENTAS</b></label></td>
                            <td colspan="2" align="right"> 
                                <telerik:RadNumericTextBox ID="edt_ptgtvta" runat="server" Width="200px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><label><b>PRECIO SUGERIDO</b></label></td>
                            <td colspan="2" align="right">
                                <telerik:RadNumericTextBox ID="edt_pVlrSug" runat="server" Width="200px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button4" runat="server" Text="Aceptar" />
                            </td>
                        </tr>
                    </table>
                    <asp:CollapsiblePanelExtender ID="clp_panel" runat="server" TargetControlID="pnl_pru"
                    CollapsedSize="0" ExpandedSize="300" Collapsed="True" ExpandControlID="pHeader" CollapseControlID="pHeader"
                    AutoCollapse="False" AutoExpand="False" ScrollContents="True" TextLabelID="lblText" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="Image1" ExpandDirection="Vertical">
                        
                    </asp:CollapsiblePanelExtender>                                        
                    <asp:Panel ID="pHeader" runat="server">
                        <asp:Label ID="lblText" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnl_pru" runat="server">
                    
                    </asp:Panel>
                </div>
            </fieldset>
        </asp:Panel>
	</telerik:RadAjaxPanel>
	<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
		SelectMethod="GetCostosIng" TypeName="XUSS.BLL.Costos.CostosBL" UpdateMethod="UpdateCostosIng"
		OnUpdated="ObjectDataSource1_Updated">
		<SelectParameters>
			<asp:Parameter Name="connection" Type="String" />
			<asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
		</SelectParameters>
		<UpdateParameters>
			<asp:Parameter Name="connection" Type="String" />
			<asp:Parameter Name="ICCODEMP" Type="String" />
			<asp:Parameter Name="ICCONSE" Type="Int32" />
			<asp:Parameter Name="ICCONSEINT" Type="Int32" />
			<asp:Parameter Name="ICPROVEE" Type="Int32" />
			<asp:Parameter Name="ICMARCA" Type="String" />
			<asp:Parameter Name="ICTIPPRO" Type="String" />
			<asp:Parameter Name="ICREFERENCIA" Type="String" />
			<asp:Parameter Name="ICCLAVE1" Type="String" />
			<asp:Parameter Name="ICCANTIDAD" Type="Int32" />
			<asp:Parameter Name="ICTASA" Type="Double" />
			<asp:Parameter Name="ICCOSTOUUSD" Type="Double" />
			<asp:Parameter Name="ICTOTALUSD" Type="Double" />
			<asp:Parameter Name="ICCOSTOUND" Type="Double" />
			<asp:Parameter Name="ICCOSTOTOT" Type="Double" />
			<asp:Parameter Name="ICCOSTOSVT" Type="Double" />
			<asp:Parameter Name="ICFECHA" Type="DateTime" />
			<asp:Parameter Name="ICFECING" Type="DateTime" />
			<asp:Parameter Name="ICCDUSER" Type="String" />
			<asp:Parameter Name="ICESTADO" Type="String" />
		</UpdateParameters>
	</asp:ObjectDataSource>
</asp:Content>
