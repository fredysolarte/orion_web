<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CrearFormularios.aspx.cs" Inherits="Administrador.CrearFormularios" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%--<%@ Register src="../ControlesUsuario/LoadFileBinary.ascx" tagname="LoadFileBinary" tagprefix="uc1" %>--%>
<%@ Register src="../ControlesUsuario/UserControlAdvancedDropDownList.ascx" tagname="SelectAndText" tagprefix="uc2" %>
<%--<%@ Register src="../ControlesUsuario/UserControlFind.ascx" tagname="UserControlFind" tagprefix="uc3" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<script type="text/javascript">
		function TestClick(sender, args) {
			if (args.get_node().get_checked()) {
				if (args.get_node().get_parent() != null) {
					setPadre(args.get_node().get_parent());
				}
			} else {        
				setHijos(args.get_node());
			}
		}

		function setHijos(nodo) {
			var i = 0;
			var hijos = nodo.get_allNodes();
			for (i = 0; i < hijos.length; i++) {
				hijos[i].set_checked(false);
			}        
		}
	
		function setPadre(nodo) {
			if (nodo._parent != undefined) {            
				if (!nodo.get_checked()) {
					nodo.set_checked(true);
					if (nodo.get_parent() != null) {
						setPadre(nodo.get_parent());
					}
				}
			}
		}
	</script>   
	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
	</telerik:RadScriptManager>
	
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="Button1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="lstbControlForm" UpdatePanelHeight="" />
					<telerik:AjaxUpdatedControl ControlID="lstbColtrolBD" UpdatePanelHeight="" />
					<telerik:AjaxUpdatedControl ControlID="GridView1" UpdatePanelHeight="" />
					<telerik:AjaxUpdatedControl ControlID="fsDetails" UpdatePanelHeight="" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="btnAceptar">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="fsDetails" UpdatePanelHeight="" />
					<telerik:AjaxUpdatedControl ControlID="rtvArbol" UpdatePanelHeight="" />
					<telerik:AjaxUpdatedControl ControlID="btnAceptar" UpdatePanelHeight="" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadListView11" UpdatePanelHeight="" />
				</UpdatedControls>
			</telerik:AjaxSetting>                
			<telerik:AjaxSetting AjaxControlID="RadListView11">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="fsDetails" UpdatePanelHeight="" />
					<telerik:AjaxUpdatedControl ControlID="rtvArbol" UpdatePanelHeight="" />
				</UpdatedControls>
			</telerik:AjaxSetting>      
			<telerik:AjaxSetting AjaxControlID="fsDetails">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="rtvArbol" UpdatePanelHeight="" />
				</UpdatedControls>
			</telerik:AjaxSetting>                            
		</AjaxSettings>
	</telerik:RadAjaxManager>  
	<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" 
        DecoratedControls="Default, Textbox, Textarea, Label, Select" />      
	<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
		OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllList" 
		TypeName="BLL.Administracion.AdmiFormularioBL" 
		InsertMethod="Insert" DeleteMethod="Delete" UpdateMethod="Update"          
		oninserting="ObjectDataSource1_Inserting" 
		onupdating="ObjectDataSource1_Updating" 
		oninserted="ObjectDataSource1_Inserted" >
		<SelectParameters>
			<asp:Parameter DefaultValue="" Name="connection" Type="String" />
			<asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
			<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
			<asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
		</SelectParameters>
		<InsertParameters>        
			<asp:Parameter Name="ListaControlesForm" Type="Object" />              
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="ListaControlesForm" Type="Object" />           
			<asp:Parameter Name="lista" Type="Object" />                                        
		</UpdateParameters>
	</asp:ObjectDataSource> 
	<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
		OldValuesParameterFormatString="original_{0}" SelectMethod="GetList" 
		TypeName="BLL.Administracion.AdmiSistemaBL">
		<SelectParameters>
			<asp:Parameter DefaultValue="" Name="connection" Type="String" />
			<asp:Parameter DefaultValue="" Name="filter" Type="String" />
			<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
			<asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>     
	<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" >
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" >   
	<telerik:RadListView ID="RadListView11" runat="server" 
		DataSourceID="ObjectDataSource1" ItemPlaceholderID="ItemContainer" 
		PageSize="1" AllowPaging="True" onitemcommand="RadListView1_ItemCommand" 
		DataKeyNames="FormFormulario" 
		onitemdatabound="RadListView1_ItemDataBound">
		<LayoutTemplate>
			<fieldset class="cssFieldSetContainer">
				<div class="box">
					<div class="title">
						<h5>Formularios</h5>
					</div>
				</div>
                <div class="paginadorRadListView">
					<telerik:RadDataPager ID="RadDataPager1" runat="server" 
						PagedControlID="RadListView11" PageSize="1">
						<Fields>
							<telerik:RadDataPagerButtonField FieldType="FirstPrev" />
							<telerik:RadDataPagerButtonField FieldType="NextLast" />
						</Fields>
					</telerik:RadDataPager>
				</div>
				<asp:Panel ID="ItemContainer" runat="server" />
				
			</fieldset>
		</LayoutTemplate>
		<ItemTemplate>
			<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
				OldValuesParameterFormatString="original_{0}" SelectMethod="GetListBySystem" 
				TypeName="BLL.Administracion.AdmiModuloBL">
				<SelectParameters>
					<asp:Parameter DefaultValue="" Name="connection" Type="String" />
					<asp:Parameter DefaultValue="" Name="filter" Type="String" />
					<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
					<asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText1" Name="systemId" 
							PropertyName="SelectedValue" Type="Int32" />
				</SelectParameters>
			</asp:ObjectDataSource>        
			<asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
				OldValuesParameterFormatString="original_{0}" 
				SelectMethod="GetListByIdModuloAndIdSistema" 
				TypeName="BLL.Administracion.AdmiOpcionBL">
				<SelectParameters>
					<asp:Parameter DefaultValue="" Name="connection" Type="String" />
					<asp:Parameter DefaultValue="" Name="filter" Type="String" />
					<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
					<asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText2" Name="idModulo" 
						PropertyName="SelectedValue" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText1" Name="idSistema" 
						PropertyName="SelectedValue" Type="Int32" />                                        
				</SelectParameters>
			</asp:ObjectDataSource>        
            <div runat="server" id="BotonesBarra"   class="toolBarsMenu"  >
				<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC"/>
				<asp:ImageButton ID="iBtnEdit" runat="server" CommandName="Edit" Text="Modificar" SkinID="SkinEditUC" />
				<asp:ImageButton ID="iBtnDelete" runat="server" CommandName="Delete" SkinID="SkinDeleteUC" Text="Eliminar" OnClientClick="return confirm('¿confirma que desea eliminar el registro?')" />
				<asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />                                
			</div>
			<asp:Panel ID="pnItemMaster" runat="server">
			    <table cellspacing="8" >
				    <tr>
					    <td><label>Id formulario</label> </td>
					    <td><telerik:RadTextBox ID="RadTextBox5" Runat="server" Width="358px" 
							    Text='<%# Eval("FormFormulario") %>' Enabled="False"/></td>
				    </tr>
				    <tr>
					    <td>
						    <label>
						    Sistema
						    </label>
					    </td>
					    <td style="padding:0px; width:400px">
						    <uc2:SelectAndText ID="SelectAndText1" runat="server" 
							    DataTextField="SistNombre" DataValueField="SistSistema" 
							    DataSourceID="ObjectDataSource2" DropDownListWidth="330px" 
							    SelectedValue='<%# Eval("SistSistema") %>'  Validate="false" Enabled="false" />                                                                                    
											
					    </td>
				    </tr>
				    <tr>
					    <td>
						    <label>
						    Módulo
						    </label>
					    </td>
					    <td style="padding:0px">
						    <uc2:SelectAndText ID="SelectAndText2" runat="server" 
							    DataTextField="ModuNombre" DataValueField="ModuModulo" 
							    DataSourceID="ObjectDataSource3" DropDownListWidth="330px" 
							    SelectedValue='<%# Eval("ModuModulo") %>'  Validate="false" Enabled="false" />
					    </td>
				    </tr>
				    <tr>
					    <td>
						    <label>
						    Opción
						    </label>
					    </td>
					    <td style="padding:0px">
						    <uc2:SelectAndText ID="SelectAndText3" runat="server" 
							    DataTextField="OpciNombre" DataValueField="OpciOpcion" 
							    DataSourceID="ObjectDataSource4" DropDownListWidth="330px" 
							    SelectedValue='<%# Eval("OpciOpcion") %>'  Validate="false" Enabled="false" />                                             
					    </td>
				    </tr>
				    <tr>
					    <td><label>Descripcion</label> </td>
					    <td><telerik:RadTextBox ID="RadTextBox1" Runat="server" Width="358px" 
							    Text='<%# Eval("FormDescripcion") %>' Enabled="False"/></td>
				    </tr>
				    <tr>
					    <td><label>Nombre</label> </td>
					    <td><telerik:RadTextBox ID="RadTextBox2" Runat="server" Width="358px" 
							    Text='<%# Eval("FormNombre") %>' Enabled="False"/></td>
				    </tr>
				    <tr>
					    <td><label>Ayuda</label> </td>
					    <td><telerik:RadTextBox ID="RadTextBox4" Runat="server" Width="358px" 
							    Text='<%# Eval("BlobNombre") %>' Enabled="False"/>                                        
					    </td>
				    </tr>                                                                   
				    <tr>
					    <td><label>Tabla Base</label> </td>
					    <td>
					    <telerik:RadTextBox ID="RadTextBox3" Runat="server" Width="358px" 
							    Text='<%# Eval("FormTablabase") %>' Enabled="False"/></td>
				    </tr>
				    <tr>
					    <td><label>Estado</label> </td>
					    <td><asp:CheckBox ID="CheckBox1" runat="server" Text="" 
							    TextAlign="Left" Checked='<%# Eval("FormEstado") %>' Enabled="False" /></td>
				    </tr>                                                                   
			    </table>						
			</asp:Panel>
		</ItemTemplate>
		<EditItemTemplate>
			<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
				OldValuesParameterFormatString="original_{0}" SelectMethod="GetListBySystem" 
				TypeName="BLL.Administracion.AdmiModuloBL">
				<SelectParameters>
					<asp:Parameter DefaultValue="" Name="connection" Type="String" />
					<asp:Parameter DefaultValue="" Name="filter" Type="String" />
					<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
					<asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText1" Name="systemId" 
							PropertyName="SelectedValue" Type="Int32" />
				</SelectParameters>
			</asp:ObjectDataSource>        
			<asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
				OldValuesParameterFormatString="original_{0}" 
				SelectMethod="GetListByIdModuloAndIdSistema" 
				TypeName="Administracion.AdmiOpcionBL">
				<SelectParameters>
					<asp:Parameter DefaultValue="" Name="connection" Type="String" />
					<asp:Parameter DefaultValue="" Name="filter" Type="String" />
					<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
					<asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText2" Name="idModulo" 
						PropertyName="SelectedValue" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText1" Name="idSistema" 
						PropertyName="SelectedValue" Type="Int32" />                                        
				</SelectParameters>
			</asp:ObjectDataSource>         
			<asp:Panel ID="pnItemMaster" runat="server">
				<table>
				   <tr>
						<td>
							<div style="float:left; padding-top:7px" >
								<asp:ImageButton ID="iBtnDelete" runat="server" CommandName="Delete" SkinID="SkinDeleteUC" Text="Eliminar" OnClientClick="return confirm('¿confirma que desea eliminar el registro?')" />
							</div>                            
						</td>
					</tr>
				</table>
				<table cellspacing="8" >
					<tr>
						<td>
							<label>
							Sistema
							</label>
						</td>
						<td style="padding:0px">
							<uc2:SelectAndText ID="SelectAndText1" runat="server" 
								DataTextField="SistNombre" DataValueField="SistSistema" 
								DataSourceID="ObjectDataSource2" DropDownListWidth="330px" 
								SelectedValue='<%# Bind("SistSistema") %>'  Validate="true"  /> 
						</td>
					</tr>
					<tr>
						<td>
							<label>
							Módulo
							</label>
						</td>
						<td style="padding:0px">
							<uc2:SelectAndText ID="SelectAndText2" runat="server" 
								DataTextField="ModuNombre" DataValueField="ModuModulo" 
								DataSourceID="ObjectDataSource3" DropDownListWidth="330px" 
								SelectedValue='<%# Bind("ModuModulo") %>'  Validate="true"/>
						</td>
					</tr>
					<tr>
						<td>
							<label>
							Opción
							</label>
						</td>
						<td style="padding:0px">
							<uc2:SelectAndText ID="SelectAndText3" runat="server" 
								DataTextField="OpciNombre" DataValueField="OpciOpcion" 
								DataSourceID="ObjectDataSource4" DropDownListWidth="330px" 
								SelectedValue='<%# Bind("OpciOpcion") %>'  Validate="true" />                                             
						</td>
					</tr>                                
					<tr>
						<td><label>Descripción</label> </td>
						<td>
							<telerik:RadTextBox ID="RadTextBox1" Runat="server" Width="358px" 
								Text='<%# Bind("FormDescripcion") %>' MaxLength="255"/>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
								ControlToValidate="RadTextBox1" ErrorMessage="Campo Obligatorio" >
								<asp:Image ID="Image3" runat="server"  SkinID="ImagenError"/>
							</asp:RequiredFieldValidator>                                                
						</td>
					</tr>
					<tr>
						<td><label>Nombre</label> </td>
						<td>
							<telerik:RadTextBox ID="RadTextBox2" Runat="server" Width="358px" 
								Text='<%# Bind("FormNombre") %>' MaxLength="200"/>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
								ControlToValidate="RadTextBox2" ErrorMessage="Campo Obligatorio" >
								<asp:Image ID="Image1" runat="server"  SkinID="ImagenError"/>
							</asp:RequiredFieldValidator>                                                
						</td>
					</tr>
					<tr>
						<td><label>Ayuda</label> </td>
						<td>
								<%--<uc1:LoadFileBinary ID="LoadFileBinary1" runat="server" ActiveViewIndex="2" IdBlob='<%# Bind("BlobAyuda") %>' Text='<%# Eval("BlobNombre") %>'/>--%>
						</td>
					</tr>                                                                   
					<tr>
						<td><label>Tabla Base</label> </td>
						<td>
							<telerik:RadTextBox ID="RadTextBox3" Runat="server" Width="358px" 
								Text='<%# Bind("FormTablabase") %>' MaxLength="50"/></td>
					</tr>
					<tr>
						<td><label>Estado</label> </td>
						<td><asp:CheckBox ID="CheckBox1" runat="server" Text="" 
								TextAlign="Left" Checked='<%# Bind("FormEstado") %>' /></td>
					</tr>       
					<tr>
						<td colspan="2">
							<asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" />
							<asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
						</td>
					</tr>                                                                                                 
				</table>
			</asp:Panel>        
		</EditItemTemplate>
		<InsertItemTemplate>
			<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
				OldValuesParameterFormatString="original_{0}" SelectMethod="GetListBySystem" 
				TypeName="BLL.Administracion.AdmiModuloBL">
				<SelectParameters>
					<asp:Parameter DefaultValue="" Name="connection" Type="String" />
					<asp:Parameter DefaultValue="" Name="filter" Type="String" />
					<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
					<asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText1" Name="systemId" 
							PropertyName="SelectedValue" Type="Int32" />
				</SelectParameters>
			</asp:ObjectDataSource>        
			<asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
				OldValuesParameterFormatString="original_{0}" 
				SelectMethod="GetListByIdModuloAndIdSistema" 
				TypeName="BLL.Administracion.AdmiOpcionBL">
				<SelectParameters>
					<asp:Parameter DefaultValue="" Name="connection" Type="String" />
					<asp:Parameter DefaultValue="" Name="filter" Type="String" />
					<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
					<asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText2" Name="idModulo" 
						PropertyName="SelectedValue" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText1" Name="idSistema" 
						PropertyName="SelectedValue" Type="Int32" />                                        
				</SelectParameters>
			</asp:ObjectDataSource>         
			<asp:Panel ID="pnItemMaster" runat="server">
		        <table cellspacing="8" >
			        <tr>
				        <td>
					        <label>
					        Sistema
					        </label>
				        </td>
				        <td style="padding:0px">
					        <uc2:SelectAndText ID="SelectAndText1" runat="server" 
						        DataTextField="SistNombre" DataValueField="SistSistema" 
						        DataSourceID="ObjectDataSource2" DropDownListWidth="330px" 
						        SelectedValue='<%# Bind("SistSistema") %>'  Validate="true"  />                                                                                    
											
				        </td>
			        </tr>
			        <tr>
				        <td>
					        <label>
					        Módulo
					        </label>
				        </td>
				        <td style="padding:0px">
					        <uc2:SelectAndText ID="SelectAndText2" runat="server" 
						        DataTextField="ModuNombre" DataValueField="ModuModulo" 
						        DataSourceID="ObjectDataSource3" DropDownListWidth="330px" 
						        SelectedValue='<%# Bind("ModuModulo") %>'  Validate="true"/>
				        </td>
			        </tr>
			        <tr>
				        <td>
					        <label>
					        Opción
					        </label>
				        </td>
				        <td style="padding:0px">
					        <uc2:SelectAndText ID="SelectAndText3" runat="server" 
						        DataTextField="OpciNombre" DataValueField="OpciOpcion" 
						        DataSourceID="ObjectDataSource4" DropDownListWidth="330px" 
						        SelectedValue='<%# Bind("OpciOpcion") %>'  Validate="true" />                                             
				        </td>
			        </tr>                                
			        <tr>
				        <td><label>Descripción</label> </td>
				        <td>
					        <telerik:RadTextBox ID="RadTextBox1" Runat="server" Width="358px" 
						        Text='<%# Bind("FormDescripcion") %>' MaxLength="255"/>
					        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
						        ControlToValidate="RadTextBox1" ErrorMessage="Campo Obligatorio" >
						        <asp:Image ID="Image3" runat="server"  SkinID="ImagenError"/>
					        </asp:RequiredFieldValidator>                                        
				        </td>
			        </tr>
			        <tr>
				        <td><label>Nombre</label> </td>
				        <td>
					        <telerik:RadTextBox ID="RadTextBox2" Runat="server" Width="358px" 
						        Text='<%# Bind("FormNombre") %>' MaxLength="200"/>
					        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
						        ControlToValidate="RadTextBox2" ErrorMessage="Campo Obligatorio" >
						        <asp:Image ID="Image2" runat="server"  SkinID="ImagenError"/>
					        </asp:RequiredFieldValidator>                                                
				        </td>
			        </tr>
			        <tr>
				        <td><label>Ayuda</label> </td>
				        <td>
					        <%--<uc1:LoadFileBinary ID="LoadFileBinary1" runat="server" ActiveViewIndex="2" IdBlob='<%# Bind("BlobAyuda") %>' />--%>
				        </td>
			        </tr>                                                                   
			        <tr>
				        <td><label>Tabla Base</label> </td>
				        <td>
				        <telerik:RadTextBox ID="RadTextBox3" Runat="server" Width="358px" 
						        Text='<%# Bind("FormTablabase") %>' MaxLength="50"/></td>
			        </tr>
			        <tr>
				        <td><label>Estado</label> </td>
				        <td><asp:CheckBox ID="CheckBox1" runat="server" Text="" 
						        TextAlign="Left" Checked='<%# Bind("FormEstado") %>' />                                            
				        </td>
			        </tr>  
			        <tr>
				        <td colspan="2">
					        <%--<asp:Button ID="btnBuscarControl" runat="server" Text="Buscar Controles" 
						        CommandName="BuscarControles"  CommandArgument="List" CausesValidation="false" />--%>
					        <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" Enabled="true" />
					        <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
				        </td>
			        </tr>
		        </table>
			</asp:Panel>
		</InsertItemTemplate>
		<EmptyDataTemplate>
			<%--<uc3:UserControlFind ID="UserControlFind1" runat="server" Titulo="Formulario" Entidad="AdmiFormulario"  DataSourceID="ObjectDataSource1" FilterControl="RadListView11"/>   --%>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
				OldValuesParameterFormatString="original_{0}" SelectMethod="GetListBySystem" 
				TypeName="BLL.Administracion.AdmiModuloBL">
				<SelectParameters>
					<asp:Parameter DefaultValue="" Name="connection" Type="String" />
					<asp:Parameter DefaultValue="" Name="filter" Type="String" />
					<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
					<asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText1" Name="systemId" 
							PropertyName="SelectedValue" Type="Int32" />
				</SelectParameters>
			</asp:ObjectDataSource>        
			<asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
				OldValuesParameterFormatString="original_{0}" 
				SelectMethod="GetListByIdModuloAndIdSistema" 
				TypeName="BLL.Administracion.AdmiOpcionBL">
				<SelectParameters>
					<asp:Parameter DefaultValue="" Name="connection" Type="String" />
					<asp:Parameter DefaultValue="" Name="filter" Type="String" />
					<asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
					<asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText2" Name="idModulo" 
						PropertyName="SelectedValue" Type="Int32" />
					<asp:ControlParameter ControlID="SelectAndText1" Name="idSistema" 
						PropertyName="SelectedValue" Type="Int32" />                                        
				</SelectParameters>
			</asp:ObjectDataSource>
            <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Formularios</h5>
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
                                                <asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />
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
						    Sistema
						    </label>
					    </td>
					    <td style="padding:0px; width:400px">
						    <uc2:SelectAndText ID="SelectAndText1" runat="server" 
							    DataTextField="SistNombre" DataValueField="SistSistema" 
							    DataSourceID="ObjectDataSource2" DropDownListWidth="330px" 
							    SelectedValue='<%# Eval("SistSistema") %>'  Validate="false" />                                                                                    
											
					    </td>
				    </tr>
				    <tr>
					    <td>
						    <label>
						    Módulo
						    </label>
					    </td>
					    <td style="padding:0px">
						    <uc2:SelectAndText ID="SelectAndText2" runat="server" 
							    DataTextField="ModuNombre" DataValueField="ModuModulo" 
							    DataSourceID="ObjectDataSource3" DropDownListWidth="330px" 
							    SelectedValue='<%# Eval("ModuModulo") %>'  Validate="false" />
					    </td>
				    </tr>
				    <tr>
					    <td>
						    <label>
						    Opción
						    </label>
					    </td>
					    <td style="padding:0px">
						    <uc2:SelectAndText ID="SelectAndText3" runat="server" 
							    DataTextField="OpciNombre" DataValueField="OpciOpcion" 
							    DataSourceID="ObjectDataSource4" DropDownListWidth="330px" 
							    SelectedValue='<%# Eval("OpciOpcion") %>'  Validate="false" />                                             
					    </td>
				    </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button9" runat="server" OnClick="BuscarGrilla" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
		</EmptyDataTemplate>
	</telerik:RadListView>
	
		<fieldset runat="server" id="fsDetails" class="cssFieldSetContainer">
		 <div class="box">
				<div class="title">
					<h5>Controles</h5>
				</div>
			</div>
	
		<telerik:RadTreeView ID="rtvArbol" Runat="server"
			CheckBoxes="true" CheckChildNodes="False"  
			onnodedatabound="rtvArbol_NodeDataBound" 
			onclientnodechecked="TestClick" >            
			<DataBindings>
				<telerik:RadTreeNodeBinding Expanded="True"   />
			</DataBindings>
		</telerik:RadTreeView>    
		</fieldset>
	</telerik:RadAjaxPanel>
	<asp:ModalPopupExtender ID="ModalPopupExtender145" runat="server" PopupControlID="pnlElerta" TargetControlID="Button2"  
		BackgroundCssClass="modalBackground"  />    
	<asp:Panel ID="pnlElerta" runat="server"  CssClass="modalPopupTexto" style="display:none;">
		<fieldset class="cssFieldSet" style="width:auto !important;">
			<legend>Confirmar</legend>
			<table>
				<tr>
					<td>
						<b>Controles En la Forma</b><br />
						<asp:ListBox ID="lstbControlForm" runat="server" 
							Width="150px" Height="200px"/>
					</td>
					<td>
						<b>Campos en base de datos</b><br />
						<asp:ListBox ID="lstbColtrolBD" runat="server" Width="150px" Height="200px" 
							style="text-align:left;"/>                 
					</td>
				</tr>
				<tr>
					<td colspan="2" align="center">                
						<asp:Button ID="Button1" runat="server" Text="Agregar" 
								onclick="Button1_Click" />
					</td>
				</tr>
				<tr>
					<td colspan="2">            
						<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
							DataKeyNames="LogicalName,FileColumnName">      
							<Columns>                    
								<asp:TemplateField HeaderText="Nombre del Campo" SortExpression="LogicalName">
									<ItemTemplate>
										<asp:Label ID="Label1" runat="server" Text='<%# Bind("LogicalName") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>                        
								<asp:TemplateField HeaderText="Nombre de Columna" SortExpression="FileColumnName">
									<ItemTemplate>
										<asp:Label ID="Label2" runat="server" Text='<%# Bind("FileColumnName") %>'></asp:Label>                                   
									</ItemTemplate>
								</asp:TemplateField>                            
								<asp:TemplateField ShowHeader="False">
									<ItemTemplate>
										<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
											CommandName="Delete" Text="Eliminar"></asp:LinkButton>                                    
								</ItemTemplate>
									</asp:TemplateField>
							</Columns>      
						</asp:GridView>
					</td>
				</tr>
				<tr>
					<td  align="center" colspan="2">
						<asp:Button ID="btnAceptar" runat="server" Text="Aceptar" onclick="btnAceptar_Click"/>                    
					</td>
				</tr>
			</table>                          
		</fieldset>        
	</asp:Panel>    
	<div style="display:none;">
		<asp:Button ID="Button2" runat="server" Text="Button"   />
	</div>    
</asp:Content>