<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
	CodeBehind="CtrlCalidad.aspx.cs" Inherits="XUSS.WEB.Calidad.CtrlCalidad" %>
<%@ Register Src="../ControlesUsuario/Ctl_Personas.ascx" TagName="FinderConsultas" TagPrefix="uc1" %>
<%@ Register Src="../ControlesUsuario/Ctl_Articulos.ascx" TagName="FinderConsultas"	TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
	</telerik:RadScriptManager>
	<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
	</telerik:RadAjaxLoadingPanel>
	<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
		<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
			Skin="Default" />
		<telerik:RadListView ID="RadListView1" runat="server" ItemPlaceholderID="pnlGeneral"
			DataSourceID="ObjectDataSource1" allowPaging="True" PageSize="1" OnItemCommand="RadListView1_ItemCommand" 
			OnItemDataBound="RadListView1_ItemDataBound"  OnItemInserting="RadListView1_ItemInserting" OnItemEditing="RadListView1_ItemEditing" >
			<LayoutTemplate>
				<fieldset class="cssFieldSetContainer">
					<div class="box">
						<div class="title">
							<h5>
								Control de Calidad de Prendas</h5>
						</div>
					</div>
					<div class="paginadorRadListView">
						<telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="RadListView1"
							PageSize="1">
							<Fields>
								<telerik:RadDataPagerButtonField FieldType="FirstPrev" />
								<telerik:RadDataPagerButtonField FieldType="NextLast" />
							</Fields>
						</telerik:RadDataPager>
					</div>
					<asp:Panel runat="server" ID="pnlGeneral" Style="padding: 5px 5px 5px 5px">
					</asp:Panel>
				</fieldset>
			</LayoutTemplate>
			<EmptyDataTemplate>
				<fieldset class="cssFieldSetContainer">
					<div class="box">
						<div class="title">
							<h5>
								Control de Calidad de Prendas</h5>
						</div>
					</div>
					<table>
						<tr>
							<td>
								<table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
									<tr>
										<td>
											<div style="padding-top: 7px">
												<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC"
													ToolTip="Insertar" />
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
									Almacen</label>
							</td>
							<td>
								<telerik:RadComboBox runat="server" ID="cb_bodega" DataSourceID="ObjectDataSource1"
									DataTextField="BDNOMBRE" DataValueField="BDBODEGA" AppendDataBoundItems="True">
									<Items>
										<telerik:RadComboBoxItem Value="" Text="Seleccione" />
									</Items>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetBodegas" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
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
									Arreglo No.</label>
							</td>
							<td>
								<telerik:RadTextBox ID="rdtArreglo" runat="server" Culture="es-CO" Width="50%">
								</telerik:RadTextBox>
							</td>
						</tr>
						<tr>
							<td>
								<label>
									No. Identifiacion</label>
							</td>
							<td>
								<telerik:RadTextBox ID="rdt_identificacion" runat="server" Width="50%">
								</telerik:RadTextBox>
							</td>
						</tr>
						<tr>
							<td>
								<label>
									Nombre</label>
							</td>
							<td>
								<telerik:RadTextBox ID="rdt_nombre" runat="server" Width="50%">
								</telerik:RadTextBox>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Button ID="Button9" runat="server" OnClick="BuscarGrilla" Text="Buscar" />
							</td>
						</tr>
					</table>
					<asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
				</fieldset>
			</EmptyDataTemplate>
			<InsertItemTemplate>
				<table cellspacing="8">
					<tr>
						<td>
							<label>
								Almacen</label>
						</td>
						<td>
							<telerik:RadComboBox runat="server" ID="cb_bodega" DataSourceID="ObjectDataSource1" AutoPostBack="true"
								Width="300px" DataTextField="BDNOMBRE" DataValueField="BDBODEGA" AppendDataBoundItems="True"
								SelectedValue='<%# Bind("CA_BODEGA") %>' OnSelectedIndexChanged="bodega_SelectedIndexChanged">
								<Items>
									<telerik:RadComboBoxItem Value="" Text="Seleccione" />
								</Items>
							</telerik:RadComboBox>
							<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
								SelectMethod="GetBodegas" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
								<SelectParameters>
									<asp:Parameter Name="connection" Type="String" />
									<asp:Parameter Name="filter" Type="String" />
									<asp:Parameter Name="startRowIndex" Type="Int32" />
									<asp:Parameter Name="maximumRows" Type="Int32" />
								</SelectParameters>
							</asp:ObjectDataSource>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="PostBackBoton"
								ControlToValidate="cb_bodega" ErrorMessage="(*)" InitialValue="Seleccione">
								<asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
							</asp:RequiredFieldValidator>
						</td>
						<td>
							<label>
								ARREGLO No.</label>
						</td>
						<td>
							<telerik:RadTextBox ID="rdtArreglo" runat="server" Culture="es-CO" Width="80%" Text='<%# Bind("CA_NCONSE") %>'
								Enabled="false">
							</telerik:RadTextBox>
						</td>
					</tr>
					<tr>
						<td>
							<label>
								Fecha</label>
						</td>
						<td>
							<telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("CA_FECHA") %>'>
							</telerik:RadDatePicker>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="PostBackBoton"
								ControlToValidate="txt_fecha" ErrorMessage="(*)">
								<asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
							</asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td>
							<label>
								No. Identificación</label>
						</td>
						<td>
							<telerik:RadTextBox ID="rdt_identificacion" runat="server" Width="50%" Text='<%# Bind("CA_NRODOC") %>' OnTextChanged="identificacion_TextChanged"
							AutoPostBack="true" >
							</telerik:RadTextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="PostBackBoton"
								ControlToValidate="rdt_identificacion" ErrorMessage="(*)">
								<asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
							</asp:RequiredFieldValidator>
						</td>
						<td>
							<uc1:FinderConsultas ID="FinderConsultas1" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<label>
								Nombre Cliente</label>
						</td>
						<td colspan="4">
							<telerik:RadTextBox ID="rdt_nombre" runat="server" Culture="es-CO" Width="80%" Text='<%# Bind("CA_NOMBRE") %>' Enabled="false">
							</telerik:RadTextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="PostBackBoton"
								ControlToValidate="rdt_nombre" ErrorMessage="(*)">
								<asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
							</asp:RequiredFieldValidator>
						</td>
					</tr>
					<div>
						<tr>
							<td>
								<label>
									Linea</label>
							</td>
							<td>
								<telerik:RadComboBox runat="server" ID="rcb_linea" DataSourceID="ObjectDataSource2" AutoPostBack="true"
									Width="100px" DataTextField="TANOMBRE" DataValueField="TATIPPRO" AppendDataBoundItems="True"
									SelectedValue='<%# Bind("CA_TIPPRO") %>'>
									<Items>
										<telerik:RadComboBoxItem Value="" Text="Seleccione" />
									</Items>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetTipPro" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:Parameter Name="filter" Type="String" />
										<asp:Parameter Name="startRowIndex" Type="Int32" />
										<asp:Parameter Name="maximumRows" Type="Int32" />
									</SelectParameters>
								</asp:ObjectDataSource>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="PostBackBoton"
									ControlToValidate="rcb_linea" ErrorMessage="(*)" InitialValue="Seleccione">
									<asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
								</asp:RequiredFieldValidator>
							</td>
							<td>
								<uc2:FinderConsultas ID="FinderConsultas2" runat="server" />
							</td>
						</tr>
						<tr>
							<td>
								<label>
									Referencia</label>
							</td>
							<td>
								<telerik:RadTextBox ID="rdt_referencia" runat="server" Culture="es-CO" Width="50px"
									Text='<%# Bind("CA_CLAVE1") %>' OnTextChanged="referencia_TextChanged" AutoPostBack="true">
								</telerik:RadTextBox>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="PostBackBoton"
									ControlToValidate="rdt_referencia" ErrorMessage="(*)">
									<asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
								</asp:RequiredFieldValidator>
							</td>
							<td>
								<label>
									Talla</label>
							</td>
							<td>								
								<telerik:RadComboBox runat="server" ID="rcb_clave2" DataSourceID="ObjectDataSource6"
									Width="50px" DataTextField="ASNOMBRE" DataValueField="ASCLAVEO" AppendDataBoundItems="True"
									><%--SelectedValue='<%# Bind("CA_CLAVE2") %>'--%>
									<Items>
										<telerik:RadComboBoxItem Value="" Text="Seleccione" />
									</Items>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource6" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetClave" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:ControlParameter Name="TP" Type="String" ControlID="rcb_linea" PropertyName="SelectedValue" />
										<asp:Parameter Name="clave" Type="Int32" DefaultValue="2" />	
									</SelectParameters>
								</asp:ObjectDataSource>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="PostBackBoton"
									ControlToValidate="rcb_clave2" ErrorMessage="(*)" InitialValue="Seleccione">
									<asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
								</asp:RequiredFieldValidator>
							</td>
							<td>
								Color
							</td>
							<td>
								<telerik:RadComboBox runat="server" ID="rcb_clave3" DataSourceID="ObjectDataSource7"
									Width="50px" DataTextField="ASNOMBRE" DataValueField="ASCLAVEO" AppendDataBoundItems="True"
									><%--SelectedValue='<%# Bind("CA_CLAVE3") %>'--%>
									<Items>
										<telerik:RadComboBoxItem Value="" Text="Seleccione" />
									</Items>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource7" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetClave" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:ControlParameter Name="TP" Type="String" ControlID="rcb_linea" PropertyName="SelectedValue" />
										<asp:Parameter Name="clave" Type="Int32" DefaultValue="3" />	
									</SelectParameters>
								</asp:ObjectDataSource>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="PostBackBoton"
									ControlToValidate="rcb_clave3" ErrorMessage="(*)" InitialValue="Seleccione">
									<asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
								</asp:RequiredFieldValidator>
							</td>
						</tr>
					</div>
					<tr>
						<td>
							<label>
								Novedad<label>
						</td>
						<td>
							<telerik:RadComboBox runat="server" ID="rc_novedad" DataSourceID="ObjectDataSource3"
								Width="100px" DataTextField="TTVALORC" DataValueField="TTCODCLA" AppendDataBoundItems="True"
								SelectedValue='<%# Bind("CA_NOVEDAD") %>'>
								<Items>
									<telerik:RadComboBoxItem Value="" Text="Seleccione" />
								</Items>
							</telerik:RadComboBox>
							<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
								SelectMethod="GetTipError" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
								<SelectParameters>
									<asp:Parameter Name="connection" Type="String" />
									<asp:Parameter Name="filter" Type="String" />
									<asp:Parameter Name="startRowIndex" Type="Int32" />
									<asp:Parameter Name="maximumRows" Type="Int32" />
								</SelectParameters>
							</asp:ObjectDataSource>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="PostBackBoton"
								ControlToValidate="rc_novedad" ErrorMessage="(*)" InitialValue="Seleccione">
								<asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
							</asp:RequiredFieldValidator>
						</td>
						<td>
							<label>
								Pieza</label>
						</td>
						<td>
							<telerik:RadComboBox runat="server" ID="rcb_pieza" DataSourceID="ObjectDataSource4"
								Width="80%" DataTextField="TTVALORC" DataValueField="TTCODCLA" AppendDataBoundItems="True"
								SelectedValue='<%# Bind("CA_PIEZA") %>'>
								<Items>
									<telerik:RadComboBoxItem Value="" Text="Seleccione" />
								</Items>
							</telerik:RadComboBox>
							<asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}"
								SelectMethod="GetParteP" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
								<SelectParameters>
									<asp:Parameter Name="connection" Type="String" />
									<asp:Parameter Name="filter" Type="String" />
									<asp:Parameter Name="startRowIndex" Type="Int32" />
									<asp:Parameter Name="maximumRows" Type="Int32" />
								</SelectParameters>
							</asp:ObjectDataSource>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="PostBackBoton"
								ControlToValidate="rcb_pieza" ErrorMessage="(*)" InitialValue="Seleccione">
								<asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
							</asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
					</tr>
					<tr>
						<td>
							<label>
								Observaciones</label>
						</td>
						<td colspan="5">
							<telerik:RadTextBox ID="rtb_Observaciones" runat="server" TextMode="MultiLine" Width="80%"
								Text='<%# Bind("CA_OBSERVACIONES") %>'>
							</telerik:RadTextBox>
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar"
								ValidationGroup="PostBackBoton" />
							<asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
						</td>
					</tr>
				</table>
			</InsertItemTemplate>
			<ItemTemplate>
				<div runat="server" id="BotonesBarra" class="toolBarsMenu">
					<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />
					<asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />
					<asp:ImageButton ID="IBImprimir" runat="server" OnClick="OnClick_Imprimir" SkinID="SkinImprimirUC" />
				</div>
				<asp:Panel ID="pnItemMaster" runat="server">
					<table cellspacing="8">
						<tr>
							<td>
								<label>
									Almacen</label>
							</td>
							<td>
								<telerik:RadComboBox runat="server" ID="cb_bodega" DataSourceID="ObjectDataSource1"
									Width="80%" Enabled="false" DataTextField="BDNOMBRE" DataValueField="BDBODEGA"
									AppendDataBoundItems="True" SelectedValue='<%# Bind("CA_BODEGA") %>'>
									<Items>
										<telerik:RadComboBoxItem Value="" Text="Seleccione" />
									</Items>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetBodegas" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:Parameter Name="filter" Type="String" />
										<asp:Parameter Name="startRowIndex" Type="Int32" />
										<asp:Parameter Name="maximumRows" Type="Int32" />
									</SelectParameters>
								</asp:ObjectDataSource>
							</td>
							<td>
								<label>
									ARREGLO No.</label>
							</td>
							<td>
								<telerik:RadTextBox ID="rdtArreglo" runat="server" Culture="es-CO" Width="80%" Text='<%# Bind("CA_NCONSE") %>'
									Enabled="false">
								</telerik:RadTextBox>
							</td>
						</tr>
						<tr>
							<td>
								<label>
									Fecha</label>
							</td>
							<td>
								<telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("CA_FECHA") %>'
									Enabled="false">
								</telerik:RadDatePicker>
							</td>
						</tr>
						<tr>
							<td>
								<label>
									No. Identificación</label>
							</td>
							<td>
								<telerik:RadTextBox ID="rdt_identificacion" runat="server" Width="50%" Text='<%# Bind("CA_NRODOC") %>'
									Enabled="false">
								</telerik:RadTextBox>
							</td>
							<td>
								<uc1:FinderConsultas ID="FinderConsultas1" runat="server" />
							</td>
						</tr>
						<tr>
							<td>
								<label>
									Nombre Cliente</label>
							</td>
							<td colspan="4">
								<telerik:RadTextBox ID="rdt_nombre" runat="server" Culture="es-CO" Width="80%" Text='<%# Bind("CA_NOMBRE") %>'
									Enabled="false">
								</telerik:RadTextBox>
							</td>
						</tr>
						<div>
							<tr>
								<td>
									<label>
										Linea</label>
								</td>
								<td>
									<telerik:RadComboBox runat="server" ID="rcb_linea" DataSourceID="ObjectDataSource2"
										Width="80%" DataTextField="TANOMBRE" DataValueField="TATIPPRO" AppendDataBoundItems="True"
										Enabled="false" SelectedValue='<%# Bind("CA_TIPPRO") %>'>
										<Items>
											<telerik:RadComboBoxItem Value="" Text="Seleccione" />
										</Items>
									</telerik:RadComboBox>
									<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
										SelectMethod="GetTipPro" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
										<SelectParameters>
											<asp:Parameter Name="connection" Type="String" />
											<asp:Parameter Name="filter" Type="String" />
											<asp:Parameter Name="startRowIndex" Type="Int32" />
											<asp:Parameter Name="maximumRows" Type="Int32" />
										</SelectParameters>
									</asp:ObjectDataSource>
								</td>
								<td>
									<uc2:FinderConsultas ID="FinderConsultas2" runat="server" />
								</td>
							</tr>
							<tr>
								<td>
									<label>
										Referencia</label>
								</td>
								<td>
									<telerik:RadTextBox ID="rdt_referencia" runat="server" Culture="es-CO" Width="80%"
										Text='<%# Bind("CA_CLAVE1") %>' Enabled="false">
									</telerik:RadTextBox>
								</td>
								<td>
								<label>
									Talla</label>
							</td>
							<td>								
								<telerik:RadComboBox runat="server" ID="rcb_clave2" DataSourceID="ObjectDataSource6"
									Width="80%" DataTextField="ASNOMBRE" DataValueField="ASCLAVEO" AppendDataBoundItems="True"
									SelectedValue='<%# Bind("CA_CLAVE2") %>' Enabled="false">
									<Items>
										<telerik:RadComboBoxItem Value="" Text="Seleccione" />
									</Items>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource6" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetClave" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:ControlParameter Name="TP" Type="String" ControlID="rcb_linea" PropertyName="SelectedValue" />
										<asp:Parameter Name="clave" Type="Int32" DefaultValue="2" />	
									</SelectParameters>
								</asp:ObjectDataSource>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="PostBackBoton"
									ControlToValidate="rcb_linea" ErrorMessage="(*)" InitialValue="Seleccione">
									<asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
								</asp:RequiredFieldValidator>
							</td>
							<td>
								Color
							</td>
							<td>
								<telerik:RadComboBox runat="server" ID="rcb_clave3" DataSourceID="ObjectDataSource7"
									Width="80%" DataTextField="ASNOMBRE" DataValueField="ASCLAVEO" AppendDataBoundItems="True"
									SelectedValue='<%# Bind("CA_CLAVE3") %>' Enabled="false">
									<Items>
										<telerik:RadComboBoxItem Value="" Text="Seleccione" />
									</Items>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource7" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetClave" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:ControlParameter Name="TP" Type="String" ControlID="rcb_linea" PropertyName="SelectedValue" />
										<asp:Parameter Name="clave" Type="Int32" DefaultValue="3" />	
									</SelectParameters>
								</asp:ObjectDataSource>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="PostBackBoton"
									ControlToValidate="rcb_linea" ErrorMessage="(*)" InitialValue="Seleccione">
									<asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
								</asp:RequiredFieldValidator>
							</td>
							</tr>
						</div>
						<tr>
							<td>
								<label>
									Novedad<label>
							</td>
							<td>
								<telerik:RadComboBox runat="server" ID="rc_novedad" DataSourceID="ObjectDataSource3"
									Width="80%" Enabled="false" DataTextField="TTVALORC" DataValueField="TTCODCLA"
									AppendDataBoundItems="True" SelectedValue='<%# Bind("CA_NOVEDAD") %>'>
									<Items>
										<telerik:RadComboBoxItem Value="" Text="Seleccione" />
									</Items>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetTipError" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
									<SelectParameters>
										<asp:Parameter Name="connection" Type="String" />
										<asp:Parameter Name="filter" Type="String" />
										<asp:Parameter Name="startRowIndex" Type="Int32" />
										<asp:Parameter Name="maximumRows" Type="Int32" />
									</SelectParameters>
								</asp:ObjectDataSource>
							</td>
							<td>
								<label>
									Pieza</label>
							</td>
							<td>
								<telerik:RadComboBox runat="server" ID="rcb_pieza" DataSourceID="ObjectDataSource4"
									Width="80%" Enabled="false" DataTextField="TTVALORC" DataValueField="TTCODCLA"
									AppendDataBoundItems="True" SelectedValue='<%# Bind("CA_PIEZA") %>'>
									<Items>
										<telerik:RadComboBoxItem Value="" Text="Seleccione" />
									</Items>
								</telerik:RadComboBox>
								<asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}"
									SelectMethod="GetParteP" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL">
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
						</tr>
						<tr>
							<td>
								<label>
									Observaciones</label>
							</td>
							<td colspan="5">
								<telerik:RadTextBox ID="rtb_Observaciones" runat="server" TextMode="MultiLine" Width="80%"
									Text='<%# Bind("CA_OBSERVACIONES") %>' Enabled="false">
								</telerik:RadTextBox>
							</td>
						</tr>
					</table>
				</asp:Panel>
			</ItemTemplate>
		</telerik:RadListView>
	</telerik:RadAjaxPanel>
	<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
		SelectMethod="GetTb_Calidad" TypeName="XUSS.BLL.Calidad.CtrlCalidadBL" OnInserted="ObjectDataSource1_Inserted"
		InsertMethod="InsertTb_Calidad">
		<InsertParameters>
			<asp:Parameter Name="connection" Type="String" />
			<asp:Parameter Name="CA_BODEGA" Type="String" />
			<asp:Parameter Name="CA_NCONSE" Type="Int32" />
			<asp:Parameter Name="CA_NRODOC" Type="Int32" />
			<asp:Parameter Name="CA_FECHA" Type="DateTime" />
			<asp:Parameter Name="CA_NOMBRE" Type="String" />
			<asp:Parameter Name="CA_TELEFONO" Type="String" />
			<asp:Parameter Name="CA_CELULAR" Type="String" />
			<asp:Parameter Name="CA_TIPPRO" Type="String" />
			<asp:Parameter Name="CA_CLAVE1" Type="String" />
			<asp:Parameter Name="CA_CLAVE2" Type="String" />
			<asp:Parameter Name="CA_CLAVE3" Type="String" />
			<asp:Parameter Name="CA_CLAVE4" Type="String" />
			<asp:Parameter Name="CA_URECIBE" Type="String" />
			<asp:Parameter Name="CA_NOVEDAD" Type="String" />
			<asp:Parameter Name="CA_PIEZA" Type="String" />
			<asp:Parameter Name="CA_OBSERVACIONES" Type="String" />
		</InsertParameters>
		<SelectParameters>
			<asp:Parameter Name="connection" Type="String" />
			<asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<div style="display: none">
		<asp:Button ID="Button2" runat="server" />
	</div>
	<asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlAlertMensaje"
		TargetControlID="Button2" BackgroundCssClass="modalBackground" CancelControlID="LinkButton2232">
	</asp:ModalPopupExtender>
	<asp:Panel ID="pnlAlertMensaje" runat="server" CssClass="modalPopup" Style="display: none;">
		<fieldset class="cssFieldSetContainer"style="width: 500px !important">
			<%--<div class="box" >
				<div class="title" style="text-align: center;">
					<h5>
						Imprimir
					</h5>
				</div>
			</div>--%>
			<div style="width: 100%">
				<asp:ObjectDataSource ID="ObjectDataSource5" runat="server" OldValuesParameterFormatString="original_{0}"
					SelectMethod="GetCtrlCalidadUsr" TypeName="XUSS.BLL.Informes.InformesBL">
					<SelectParameters>
						<asp:Parameter Name="connection" Type="String" />
						<asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
						<asp:Parameter Name="startRowIndex" Type="Int32" />
						<asp:Parameter Name="maximumRows" Type="Int32" />
						<asp:Parameter Name="consecutivo" Type="Int32" />
					</SelectParameters>
				</asp:ObjectDataSource>
				<%--<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"  style="width: 100%"
					InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ShowFindControls="False" 
					ShowZoomControl="False" ShowPrintButton="true" ShowRefreshButton="True" ShowPageNavigationControls="True" ShowExportControls="True" 
					ShowBackButton="True">
					<LocalReport ReportPath="Informes\rCtrlCalidadUsr.rdlc">
						<DataSources>
							<rsweb:ReportDataSource DataSourceId="ObjectDataSource5" Name="DataSet1" />
						</DataSources>
					</LocalReport>
				</rsweb:ReportViewer>--%>
				<div style="text-align: center;">
					<asp:LinkButton ID="LinkButton2232" runat="server">Cerrar</asp:LinkButton>
				</div>
			</div>
		</fieldset>
	</asp:Panel>	
	 <div style="display:none">
		<asp:Button id="Button1" runat="server"/>
	</div>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="Button1" BackgroundCssClass="modalBackground" 
    CancelControlID="Button3"> 
	</asp:ModalPopupExtender>
	<asp:Panel ID="Panel1" runat="server"   CssClass="modalPopup"  Style="display: none;">
		<fieldset class="cssFieldSetContainer"  style="width:auto !important;">
			<div class="box">
				<div class="title">
					<h5> <asp:Label runat="server" id="LitTitulo"></asp:Label> </h5>
				</div>
			</div>
			<div style="padding: 5px 5px 5px 5px">
				<ul>
					<asp:Literal runat="server" id="litTextoMensaje"></asp:Literal>    
				</ul>				
				<div style="text-align:center;" >
					<asp:Button ID="Button3" runat="server" Text="Cerrar"  />
				</div>
			</div>
		</fieldset>
	</asp:Panel>
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadListView1">
                <UpdatedControls>                   
                    <telerik:AjaxUpdatedControl ControlID="litTextoMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="LitTitulo" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
</asp:Content>
