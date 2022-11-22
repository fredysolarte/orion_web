<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsultaInv.ascx.cs"
	Inherits="XUSS.WEB.ControlesUsuario.ConsultaInv" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">

</telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
</telerik:RadAjaxManagerProxy>
<asp:Panel ID="pnFinder" runat="server" CssClass="modalPopup" Style="display: none;
	width: 600px !important; z-index: 1003 !important;">
	<script type="text/javascript">
    function stopPropagation(e) {
        e.cancelBubble = true;
        if (e.stopPropagation) {
            e.stopPropagation();
        }
    }
	</script>
	<fieldset class="cssFieldSetContainer" style="width: 590px !important">
		<div class="box">
			<div class="title">
				<h5>
					Consultar Inventarios Vs Ventas</h5>
			</div>
		</div>
		<div style="width: 100%">
			<table>
				<tr>
					<td>
						<label>
							Almacen</label>
					</td>
					<td style="width: 100%" colspan="3">
						<telerik:RadComboBox ID="rcb_bodega" runat="server" Culture="es-CO" Width="400px"
							DataTextField="BDNOMBRE" DataValueField="BDBODEGA" DataSourceID="ObjectDataSource1"
							AppendDataBoundItems="true" HighlightTemplatedItems="true">
							<Items>
								<telerik:RadComboBoxItem Text="Seleccione" Value="" />
							</Items>
							<ItemTemplate>							
								<asp:CheckBox ID="chk_bg" runat="server" Text="" onclick="stopPropagation(event);"/> <%# DataBinder.Eval(Container, "Text") %> 
							</ItemTemplate>
						</telerik:RadComboBox>
					</td>
				</tr>
				<tr>
					<td>
						<label>
							Linea</label>
					</td>
					<td style="width: 80%" colspan="3">
						<telerik:RadComboBox ID="rcb_TipPro" runat="server" Culture="es-CO" Width="400px"
							DataTextField="TANOMBRE" DataValueField="TATIPPRO" DataSourceID="ObjectDataSource2"
							AppendDataBoundItems="true" HighlightTemplatedItems="true">
							<Items>
								<telerik:RadComboBoxItem Text="Seleccione" Value="" />
							</Items>
							<ItemTemplate>
							<asp:CheckBox runat="server" ID="chk_tp" onclick="stopPropagation(event);"/> <%# DataBinder.Eval(Container, "Text") %> 
							</ItemTemplate>
						</telerik:RadComboBox>
					</td>
				</tr>
				<tr>
					<td>
						<label>
							Referencia</label>
					</td>
					<td colspan="2">
						<telerik:RadTextBox ID="txt_referencia" runat="server">
						</telerik:RadTextBox>
					</td>
				</tr>
				<tr>
					<td>
						<label>
							Talla</label>
					</td>
					<td colspan="2">
						<telerik:RadTextBox ID="txt_talla" runat="server">
						</telerik:RadTextBox>
					</td>
				</tr>
				<tr>
					<td>
						<label>
							Color</label>
					</td>
					<td colspan="2">
						<telerik:RadTextBox ID="txt_color" runat="server">
						</telerik:RadTextBox>
					</td>
				</tr>
				<tr>
					<td>
						<label>
							R. Fechas</label>
					</td>
					<td>
						<telerik:RadDatePicker ID="txt_FechaI" runat="server">
						</telerik:RadDatePicker>
					</td>
					<td>
						<telerik:RadDatePicker ID="txt_FechaF" runat="server">
						</telerik:RadDatePicker>
					</td>
				</tr>
				<tr>
				<td><label>Sin Saldo</label></td>
				<td>
				
				</td>
				</tr>
			</table>
		</div>
		<div>
			<asp:Button ID="btnBuscar" runat="server" Text="Buscar"  onclick="btnBuscar_Click" />
			<asp:Button ID="btnCancel" runat="server" Text="Cancelar" onclick="btnCancel_Click" />
		</div>
	</fieldset>
</asp:Panel>
<asp:ImageButton ID="iBtnFind" runat="server" SkinID="SkinFindUC" />
<cc1:ModalPopupExtender ID="ModalPopupFinder" runat="server" BackgroundCssClass="modalBackground"
	PopupControlID="pnFinder" TargetControlID="iBtnFind" CancelControlID="btnCancel">
</cc1:ModalPopupExtender>
<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
	SelectMethod="GetTipPro" TypeName="XUSS.BLL.Consultas.ConsultasBL">
	<SelectParameters>
		<asp:Parameter Name="connection" Type="String" />
		<asp:Parameter Name="filter" Type="String" />
		<asp:Parameter Name="startRowIndex" Type="Int32" />
		<asp:Parameter Name="maximumRows" Type="Int32" />
	</SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
	SelectMethod="GetBodegas" TypeName="XUSS.BLL.Consultas.ConsultasBL">
	<SelectParameters>
		<asp:Parameter Name="connection" Type="String" />
		<asp:Parameter Name="filter" Type="String" />
		<asp:Parameter Name="startRowIndex" Type="Int32" />
		<asp:Parameter Name="maximumRows" Type="Int32" />
	</SelectParameters>
</asp:ObjectDataSource>
