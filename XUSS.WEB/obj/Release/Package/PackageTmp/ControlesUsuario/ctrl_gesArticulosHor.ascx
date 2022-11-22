<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrl_gesArticulosHor.ascx.cs" Inherits="XUSS.WEB.ControlesUsuario.ctrl_gesArticulosHor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnl_control" runat="server">
    <table>
                <tr>
                    <td><asp:Label ID="lbl_codigo" runat="server" Text="Codigo">
                        </asp:Label></td>
                    <td colspan ="4"> 
                        <telerik:RadTextBox ID="edt_codigo" runat="server" Width="358px" AutoPostBack="true" 
                            ontextchanged="edt_codigo_TextChanged">
                        </telerik:RadTextBox></td>
                </tr>
                
                <tr>
                    <td>
                        <asp:Label ID="lbl_linea" runat="server" Text="Linea">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_clave1" runat="server" Text="." Visible="false">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_clave2" runat="server" Text="." Visible="false">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_clave3" runat="server" Text="." Visible="false">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_clave4" runat="server" Text="." Visible="false">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="rcb_linea" runat="server" Culture="es-CO" Width="180px"
                            DataSourceID="obj_linea" DataTextField="TANOMBRE" DataValueField="TATIPPRO" AppendDataBoundItems="true"
                            OnSelectedIndexChanged="rcb_linea_SelectedIndexChanged" AutoPostBack="true">
                            <Items>
                                <telerik:RadComboBoxItem Value="." Text="-Seleccione-" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcb_clave1" runat="server" Culture="es-CO" Width="100px"
                            DataSourceID="obj_clave1" DataTextField="ARCLAVE1" Visible="false" DataValueField="ARCLAVE1"
                            AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="rcb_clave1_SelectedIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Value="." Text="-Todos-" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcb_clave2" runat="server" Culture="es-CO" Width="100px"
                            DataSourceID="obj_clave2" DataTextField="ARCLAVE2" Visible="false" AutoPostBack="true"
                            DataValueField="ARCLAVE2" AppendDataBoundItems="true" OnSelectedIndexChanged="rcb_clave2_SelectedIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Value="." Text="-Todos-" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcb_clave3" runat="server" Culture="es-CO" Width="100px"
                            DataSourceID="obj_clave3" DataTextField="ARCLAVE3" Visible="false" DataValueField="ARCLAVE3"
                            AppendDataBoundItems="true">
                            <Items>
                                <telerik:RadComboBoxItem Value="." Text="-Todos-" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcb_clave4" runat="server" Culture="es-CO" Width="100px"
                            Visible="false" AppendDataBoundItems="true">
                            <Items>
                                <telerik:RadComboBoxItem Value="." Text="-Todos-" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
            </table>
    
</asp:Panel>
<asp:ObjectDataSource ID="obj_linea" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposProducto" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>    
    <asp:ObjectDataSource ID="obj_clave1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetClave1" TypeName="XUSS.BLL.Consultas.ConDescuentosArticulosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="TP" Type="String" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_clave2" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetClave2" TypeName="XUSS.BLL.Consultas.ConDescuentosArticulosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="TP" Type="String" DefaultValue="-1" />
            <asp:Parameter Name="C1" Type="String" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_clave3" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetClave3" TypeName="XUSS.BLL.Consultas.ConDescuentosArticulosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="TP" Type="String" DefaultValue="-1" />
            <asp:Parameter Name="C1" Type="String" DefaultValue="-1" />
            <asp:Parameter Name="C2" Type="String" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>