using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Orion.Fiscal
{
    public partial class frm_principal : Form
    {
        private DataTable tbFacturaHD;
        private DataTable tbFacturaDT;

        /*[DllImport("pnpdll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr PFultimo();
        [DllImport("pnpdll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr PFabrepuerto(string puerto);*/

        //[DllImport(" pnpdlltest pnpdll.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        [DllImport("pnpdll.dll")] public static extern string PFtotal();
        [DllImport("pnpdll.dll")] public static extern string PFrepz();
        [DllImport("pnpdll.dll")] public static extern string PFrepx();
        [DllImport("pnpdll.dll")] public static extern string PFabrepuerto(String numero);
        [DllImport("pnpdll.dll")] public static extern string PFabrefiscal(String Razon, String RIF);
        [DllImport("pnpdll.dll")] public static extern string PFComando(String comando);        
        [DllImport("pnpdll.dll")] public static extern string PFrenglon(String Descripcion, String cantidad, String monto, String iva);
        [DllImport("pnpdll.dll")] public static extern string PFestatus(String edlinea);
        //public static extern IntPtr PFSerial();

        //public frm_principal() => InitializeComponent();

        public frm_principal() {
            InitializeComponent();
            DataTable tbTFacturas = new DataTable();
            tbTFacturas.Columns.Add("codigo", typeof(string));
            tbTFacturas.Columns.Add("nombre", typeof(string));
            
            tbTFacturas.Rows.Add("MG","Margarita");
            tbTFacturas.Rows.Add("FL", "JRL");

            rc_tipofac.DataSource = tbTFacturas;
            rc_tipofac.DisplayMember = "nombre";
            

            txt_puerto.Text = ConfigurationManager.AppSettings["puerto"];
            txt_rif.Text = ConfigurationManager.AppSettings["rif"];
            txt_compania.Text = ConfigurationManager.AppSettings["nombre"];
            txt_direccion.Text = ConfigurationManager.AppSettings["direccion"];
            txt_municipio.Text = ConfigurationManager.AppSettings["municipio"];
            txt_telefonos.Text = ConfigurationManager.AppSettings["telefono"];
        }

        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            double ln_total = 0;
            StringBuilder slog = new StringBuilder();
            try
            {
                PFabrepuerto(txt_puerto.Text);
                slog.AppendLine("Abre Puerto");
                rt_log.Text = slog.ToString();
                PFabrefiscal(txt_compania.Text, txt_rif.Text);

                /*
                slog.AppendLine(PFestatus("E"));
                slog.AppendLine(PFrenglon("testtttttt", "2", "344", "0000"));
                slog.AppendLine(PFestatus("E"));
                slog.AppendLine(PFrenglon("testttttt2", "4", "567", "0000"));
                slog.AppendLine(PFestatus("E"));
                slog.AppendLine(PFrenglon("testttttt3", "3", "444", "0000"));
                slog.AppendLine(PFestatus("E"));
                slog.AppendLine(PFrenglon("testttttt4", "6", "800", "0000"));
                slog.AppendLine(PFestatus("E"));
                slog.AppendLine(PFrenglon("testttttt5", "2", "115", "0000"));
                slog.AppendLine(PFestatus("E"));
                slog.AppendLine(PFrenglon("testttttt6", "5", "239", "0000"));
                */
                foreach (DataRow rw in tbFacturaDT.Rows)
                {
                    slog.AppendLine(PFestatus("E"));
                    slog.AppendLine(PFrenglon(Convert.ToString(rw["ARNOMBRE"]), Convert.ToString(rw["DTCANTID"]), Convert.ToString(rw["DTPRECIO"]), "0000"));
                    ln_total += Convert.ToDouble(rw["DTCANTID"]) * Convert.ToDouble(rw["DTPRECIO"]);
                }

                ln_total = Math.Round(ln_total, 0);
                if (chk_igtf.Checked)
                {
                    ln_total = ln_total * 100;
                    slog.AppendLine(PFComando("E|U|" + Convert.ToString(ln_total)));
                    //slog.AppendLine(PFComando("E|U"));
                    //slog.AppendLine(PFtotal());
                }
                else
                {
                    slog.AppendLine(PFtotal());
                }

                rt_log.Text = slog.ToString();                                                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {

            }
        }

        private void btn_reportez_Click(object sender, EventArgs e)
        {
            StringBuilder slog = new StringBuilder();
            try
            {
                PFabrepuerto(txt_puerto.Text);
                slog.AppendLine("Abre Puerto");
                rt_log.Text = slog.ToString();
                PFrepz();
                slog.AppendLine("Imprimir Reporte Z");
                rt_log.Text = slog.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            { 
            
            }
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            DBAccess objDB = new DBAccess(ConfigurationManager.AppSettings["CONEXION"]);
            FacturaBD Obj = new FacturaBD();
            string lc_tipfac = "MG";
            try
            {
                tbFacturaHD = Obj.GetFacturaHD(objDB, lc_tipfac, Convert.ToInt32(txt_nrofac.Text), "001");
                foreach (DataRow rw in tbFacturaHD.Rows)
                {
                    txt_compania.Text = Convert.ToString(rw["CLIENTE"]);
                    txt_rif.Text = Convert.ToString(rw["TRCODNIT"]);
                    txt_direccion.Text = Convert.ToString(rw["TRDIRECC"]);
                }
                tbFacturaDT = Obj.GetFacturaDT(objDB, "001" , lc_tipfac, Convert.ToInt32(txt_nrofac.Text));
                rg_datos.DataSource = tbFacturaDT;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDB = null;
                Obj = null;
            }
        }

        private void btn_reportex_Click(object sender, EventArgs e)
        {
            StringBuilder slog = new StringBuilder();
            try
            {
                PFabrepuerto(txt_puerto.Text);
                slog.AppendLine("Abre Puerto");
                rt_log.Text = slog.ToString();
                PFrepx();
                slog.AppendLine("Imprimir Reporte X");
                rt_log.Text = slog.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {

            }
        }
    }
}
