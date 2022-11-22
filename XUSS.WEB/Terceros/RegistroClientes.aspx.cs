using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XUSS.BLL.Comun;
using System.Data;
using System.Text;

namespace XUSS.WEB.Terceros
{
    public partial class RegistroClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void edt_cedula_TextChanged(object sender, EventArgs e)
        {            
                if (ComunBL.GetExisteTercero(edt_cedula.Text))
                {
                    edt_cedula.Enabled = false;
                    using (IDataReader reader = ComunBL.GetInformacionTercero(edt_cedula.Text))
                    {
                        while (reader.Read())
                        {
                            edt_Nombre.Text = Convert.ToString(reader["TRNOMBRE"]);
                            edt_Apellido.Text = Convert.ToString(reader["TRAPELLI"]);
                            edt_email.Text = Convert.ToString(reader["TRCORREO"]);
                            edt_ocupacion.Text = Convert.ToString(reader["TRDTTEC4"]);
                            edt_telefono.Text = Convert.ToString(reader["TRNROTEL"]);
                            if ((reader.IsDBNull(reader.GetOrdinal("TRFECNAC")) != null) && (Convert.ToString(reader["TRFECNAC"]).Trim() != ""))
                            {
                                rcbDia.SelectedValue = Convert.ToString((Convert.ToDateTime(reader["TRFECNAC"]).Day));
                                if (Convert.ToInt32((Convert.ToDateTime(reader["TRFECNAC"]).Month)) < 10)
                                    rcbMes.SelectedValue = '0' + Convert.ToString((Convert.ToDateTime(reader["TRFECNAC"]).Month));
                                else
                                    rcbMes.SelectedValue = Convert.ToString((Convert.ToDateTime(reader["TRFECNAC"]).Month));

                                rcbAno.SelectedValue = Convert.ToString((Convert.ToDateTime(reader["TRFECNAC"]).Year));
                            }
                        }
                    }
                }
                else
                {
                    edt_cedula.Enabled = true;
                }
            
        }
        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            DateTime? l_fecha;
            StringBuilder lc_msj = new StringBuilder();
            LitTitulo.Text = "Información";
            
            try
            {
                l_fecha = Convert.ToDateTime( rcbDia.SelectedValue + "/" + rcbMes.SelectedValue + "/" + rcbAno.SelectedValue);
            }
            catch(Exception ex)
            { 
                l_fecha= null;
            }

            
            ComunBL.InsertTercero(null, "001", 0, edt_Nombre.Text, edt_Apellido.Text, null, null, edt_cedula.Text,
                                  null, null, null, null, null, edt_telefono.Text, null,
                                  null, edt_email.Text, null, null, null, null, null,
                                  null, null, null, null, null, null, null,
                                  null, null, null, "S", "N", "N", "N",
                                  "N", "N", null, null, null, null, null,
                                  null, null, null, null, edt_ocupacion.Text, null, null,
                                  null, null,
                                  null, null, null, null, null, null, null,
                                  null, null, null, null, l_fecha, null, null,
                                  edt_Apellido.Text, null,true);

            
            if (ComunBL.ExisteDescuentoHappy(edt_cedula.Text))            
                lc_msj.AppendLine("Ya Cuenta con Descuento de Cumpleaños,");
            
            lc_msj.AppendLine("Tercero Actualziado Correctamente");
            litTextoMensaje.Text = lc_msj.ToString();

            mpMessage.Show();
            //    //TRMODDES, TRTERDES, TRCATEGO, TRAGENTE, TRLISPRE,
            //                            TRLISPRA, TRDESCUE, TRCUPOCR, TRINDCLI, TRINDPRO, TRINDSOP, TRINDEMP, TRINDSOC, TRINDVEN, TRCDCLA1, TRCDCLA2, TRCDCLA3, TRCDCLA4, TRCDCLA5,
            //                            TRCDCLA6, TRDTTEC1, TRDTTEC2, TRDTTEC3, TRDTTEC4, TRDTTEC5, TRDTTEC6, TRPROGDT, TRSALCAR, TRCONVEN, TROBSERV, TRREQCLI, TRRETAUT, TRRETIVA,
            //                            TRRETICA, TRRETFUE, TRCODALT, TRSALVEN, TRCENCOS, TRTIPCART, TRFECNAC, TRRESPAL, TRRESCUP, TRAPELLI, TRNOMBR3
            edt_cedula.Enabled = true;
            edt_cedula.Text = "";
            edt_Nombre.Text = "";
            edt_Apellido.Text = "";
            edt_email.Text = "";
            edt_ocupacion.Text = "";
            edt_telefono.Text = "";
            rcbDia.SelectedValue = "Seleccione";
            rcbMes.SelectedValue = "Seleccione";
            rcbAno.SelectedValue = "Seleccione";
        }
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            edt_cedula.Enabled = true;
            edt_cedula.Text = "";
            edt_Nombre.Text = "";
            edt_Apellido.Text = "";
            edt_email.Text = "";
            edt_telefono.Text = "";
            edt_ocupacion.Text = "";
            rcbDia.SelectedValue = "Seleccione";
            rcbMes.SelectedValue = "Seleccione";
            rcbAno.SelectedValue = "Seleccione";

        }
    }
}