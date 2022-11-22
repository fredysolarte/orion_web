using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml;

namespace FACTURACION.SERVICIO
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            
            DBAccess objDB = new DBAccess(ConfigurationManager.AppSettings["CONEXION"]);
            string CODEMP = ConfigurationManager.AppSettings["CODEMP"],lc_nit="",lc_ano="",lc_factura="";
            string ServerFTP = ConfigurationManager.AppSettings["ServerFTP"];
            string UserFTP = ConfigurationManager.AppSettings["UserFTP"];
            string PwdFTP = ConfigurationManager.AppSettings["PwdFTP"];
            FacturacionElectroBD Obj = new FacturacionElectroBD();
            FtpTransfer objftp = new FtpTransfer();
            try
            {                
                foreach (DataRow ry in Obj.GetFacPendientesEnvio(objDB).Rows)
                {
                    XmlDocument doc = new XmlDocument();
                    XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlElement root = doc.DocumentElement;
                    doc.InsertBefore(xmlDeclaration, root);

                    //etiqueta factura
                    XmlElement factura = doc.CreateElement(string.Empty, "factura", string.Empty);
                    doc.AppendChild(factura);

                    foreach (DataRow rw in Obj.GetFacturaHD(objDB, Convert.ToString(ry["HDTIPFAC"]), Convert.ToInt32(ry["HDNROFAC"]), CODEMP).Rows)
                    {
                        lc_ano = Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Year);
                        lc_nit = Convert.ToString(rw["CNNRONIT"]);
                        lc_factura = Convert.ToString(rw["TFPREFIJ"]) + Convert.ToString(rw["HDNROFAC"]);

                        XmlElement numeroDocumento = doc.CreateElement(string.Empty, "numeroDocumento", string.Empty);
                        XmlText numeroDocumentot = doc.CreateTextNode(Convert.ToString(rw["TFPREFIJ"]) + Convert.ToString(rw["HDNROFAC"]));
                        numeroDocumento.AppendChild(numeroDocumentot);
                        factura.AppendChild(numeroDocumento);

                        XmlElement tipoDocumento = doc.CreateElement(string.Empty, "tipoDocumento", string.Empty);
                        XmlText tipoDocumentot = doc.CreateTextNode(Convert.ToString(rw["TIP_DOC"]));
                        tipoDocumento.AppendChild(tipoDocumentot);
                        factura.AppendChild(tipoDocumento);

                        XmlElement subtipoDocumento = doc.CreateElement(string.Empty, "subtipoDocumento", string.Empty);
                        XmlText subtipoDocumentot = doc.CreateTextNode(Convert.ToString(rw["SUBTIPO"]));
                        subtipoDocumento.AppendChild(subtipoDocumentot);
                        factura.AppendChild(subtipoDocumento);

                        XmlElement tipoOperacion = doc.CreateElement(string.Empty, "tipoOperacion", string.Empty);
                        XmlText tipoOperaciont = doc.CreateTextNode(Convert.ToString(rw["TIPO_OPERACION"]));
                        tipoOperacion.AppendChild(tipoOperaciont);
                        factura.AppendChild(tipoOperacion);

                        XmlElement divisa = doc.CreateElement(string.Empty, "divisa", string.Empty);
                        XmlText divisat = doc.CreateTextNode(Convert.ToString(rw["DIVISA"]));
                        divisa.AppendChild(divisat);
                        factura.AppendChild(divisa);

                        XmlElement fechaDocumento = doc.CreateElement(string.Empty, "fechaDocumento", string.Empty);
                        XmlText fechaDocumentot = doc.CreateTextNode(Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Year) + "-" + (Convert.ToDateTime(rw["HDFECING"]).Month < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Month) + "-" + (Convert.ToDateTime(rw["HDFECING"]).Day < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Day) + " " + (Convert.ToDateTime(rw["HDFECING"]).Hour < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Hour) + ":" + (Convert.ToDateTime(rw["HDFECING"]).Minute < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Minute) + ":" + (Convert.ToDateTime(rw["HDFECING"]).Second < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Second));
                        fechaDocumento.AppendChild(fechaDocumentot);
                        factura.AppendChild(fechaDocumento);

                        XmlElement fechaVencimiento = doc.CreateElement(string.Empty, "fechaVencimiento", string.Empty);
                        XmlText fechaVencimientot = doc.CreateTextNode(Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Year) + "-" + (Convert.ToDateTime(rw["HDFECING"]).Month < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Month) + "-" + (Convert.ToDateTime(rw["HDFECING"]).Day < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Day) + " " + (Convert.ToDateTime(rw["HDFECING"]).Hour < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Hour) + ":" + (Convert.ToDateTime(rw["HDFECING"]).Minute < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Minute) + ":" + (Convert.ToDateTime(rw["HDFECING"]).Second < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Second));
                        fechaVencimiento.AppendChild(fechaVencimientot);
                        factura.AppendChild(fechaVencimiento);

                        XmlElement direccionFactura = doc.CreateElement(string.Empty, "direccionFactura", string.Empty);
                        XmlText direccionFacturat = doc.CreateTextNode(Convert.ToString(rw["BDDIRECC"]));
                        direccionFactura.AppendChild(direccionFacturat);
                        factura.AppendChild(direccionFactura);

                        XmlElement areaFactura = doc.CreateElement(string.Empty, "areaFactura", string.Empty);
                        XmlText areaFacturat = doc.CreateTextNode(Convert.ToString(rw["CIUDAD_CMP"]).Substring(0, 2));
                        areaFactura.AppendChild(areaFacturat);
                        factura.AppendChild(areaFactura);

                        XmlElement ciudadFactura = doc.CreateElement(string.Empty, "ciudadFactura", string.Empty);
                        XmlText ciudadFacturat = doc.CreateTextNode(Convert.ToString(rw["CIUDAD_CMP"]));
                        ciudadFactura.AppendChild(ciudadFacturat);
                        factura.AppendChild(ciudadFactura);

                        XmlElement codigoPostalFactura = doc.CreateElement(string.Empty, "codigoPostalFactura", string.Empty);
                        XmlText codigoPostalFacturat = doc.CreateTextNode(Convert.ToString(rw["CIUDAD_CMP"]));
                        codigoPostalFactura.AppendChild(codigoPostalFacturat);
                        factura.AppendChild(codigoPostalFactura);

                        XmlElement paisFactura = doc.CreateElement(string.Empty, "paisFactura", string.Empty);
                        XmlText paisFacturat = doc.CreateTextNode(Convert.ToString(rw["BDPAIS"]));
                        paisFactura.AppendChild(paisFacturat);
                        factura.AppendChild(paisFactura);

                        XmlElement idProveedor = doc.CreateElement(string.Empty, "idProveedor", string.Empty);
                        XmlText idProveedort = doc.CreateTextNode(Convert.ToString(rw["CNNRONIT"]));
                        idProveedor.AppendChild(idProveedort);
                        factura.AppendChild(idProveedor);

                        XmlElement idCliente = doc.CreateElement(string.Empty, "idCliente", string.Empty);
                        if (Convert.ToString(rw["TIP_DOC_CLIENTE"]) == "31")
                        {
                            XmlText idClientet = doc.CreateTextNode(Convert.ToString(rw["IDE_CLIENTE"])+ "-"+Convert.ToString(rw["TRDIGCHK"]));
                            idCliente.AppendChild(idClientet);
                        }
                        else
                        {
                            XmlText idClientet = doc.CreateTextNode(Convert.ToString(rw["IDE_CLIENTE"]));
                            idCliente.AppendChild(idClientet);
                        }
                            
                        factura.AppendChild(idCliente);

                        XmlElement tipoDocumentoIdCliente = doc.CreateElement(string.Empty, "tipoDocumentoIdCliente", string.Empty);                        
                        XmlText tipoDocumentoIdClientet = doc.CreateTextNode(Convert.ToString(rw["TIP_DOC_CLIENTE"]));
                        tipoDocumentoIdCliente.AppendChild(tipoDocumentoIdClientet);                        
                        factura.AppendChild(tipoDocumentoIdCliente);

                        XmlElement razonSocialCliente = doc.CreateElement(string.Empty, "razonSocialCliente", string.Empty);                        
                        XmlText razonSocialClientet = doc.CreateTextNode(Convert.ToString(rw["TRNOMCOMERCIAL"])); 
                        razonSocialCliente.AppendChild(razonSocialClientet);
                        factura.AppendChild(razonSocialCliente);

                        XmlElement nombreCliente = doc.CreateElement(string.Empty, "nombreCliente", string.Empty);
                        XmlText nombreClientet = doc.CreateTextNode(Convert.ToString(rw["P_NOMCLIENTE"]));
                        nombreCliente.AppendChild(nombreClientet);
                        factura.AppendChild(nombreCliente);

                        XmlElement apellido1Cliente = doc.CreateElement(string.Empty, "apellido1Cliente", string.Empty);
                        XmlText apellido1Clientet = doc.CreateTextNode(Convert.ToString(rw["P_APECLIENTE"]));
                        apellido1Cliente.AppendChild(apellido1Clientet);
                        factura.AppendChild(apellido1Cliente);

                        XmlElement apellido2Cliente = doc.CreateElement(string.Empty, "apellido2Cliente", string.Empty);
                        XmlText apellido2Clientet = doc.CreateTextNode("");
                        apellido2Cliente.AppendChild(apellido2Clientet);
                        factura.AppendChild(apellido2Cliente);

                        XmlElement tipoPersonaCliente = doc.CreateElement(string.Empty, "tipoPersonaCliente", string.Empty);
                        XmlText tipoPersonaClientet = doc.CreateTextNode(Convert.ToString(rw["TIP_PERSONA"]));
                        tipoPersonaCliente.AppendChild(tipoPersonaClientet);
                        factura.AppendChild(tipoPersonaCliente);

                        XmlElement direccionCliente = doc.CreateElement(string.Empty, "direccionCliente", string.Empty);
                        XmlText direccionClientet = doc.CreateTextNode(Convert.ToString(rw["TRDIRECC"]));
                        direccionCliente.AppendChild(direccionClientet);
                        factura.AppendChild(direccionCliente);

                        XmlElement areaCliente = doc.CreateElement(string.Empty, "areaCliente", string.Empty);
                        XmlText areaClientet = doc.CreateTextNode(Convert.ToString(rw["AREA_CLIENTE"]));
                        areaCliente.AppendChild(areaClientet);
                        factura.AppendChild(areaCliente);

                        XmlElement ciudadCliente = doc.CreateElement(string.Empty, "ciudadCliente", string.Empty);
                        XmlText ciudadClientet = doc.CreateTextNode(Convert.ToString(rw["CIUDAD_CLIENTE"]));
                        ciudadCliente.AppendChild(ciudadClientet);
                        factura.AppendChild(ciudadCliente);

                        XmlElement codigoPostalCliente = doc.CreateElement(string.Empty, "codigoPostalCliente", string.Empty);
                        XmlText codigoPostalClientet = doc.CreateTextNode(Convert.ToString(rw["CIUDAD_CLIENTE"]));
                        codigoPostalCliente.AppendChild(codigoPostalClientet);
                        factura.AppendChild(codigoPostalCliente);

                        XmlElement emailCliente = doc.CreateElement(string.Empty, "emailCliente", string.Empty);
                        XmlText emailClientet = doc.CreateTextNode(Convert.ToString(rw["EMAIL_CLIENTE"]));
                        emailCliente.AppendChild(emailClientet);
                        factura.AppendChild(emailCliente);

                        XmlElement paisCliente = doc.CreateElement(string.Empty, "paisCliente", string.Empty);
                        XmlText paisClientet = doc.CreateTextNode(Convert.ToString(rw["PAIS_CLIENTE"]));
                        paisCliente.AppendChild(paisClientet);
                        factura.AppendChild(paisCliente);

                        XmlElement caracteristicasFiscalesCliente = doc.CreateElement(string.Empty, "caracteristicasFiscalesCliente", string.Empty);
                        XmlText caracteristicasFiscalesClientet = doc.CreateTextNode(Convert.ToString(rw["CAR_FIS_CLIENTE"]));
                        caracteristicasFiscalesCliente.AppendChild(caracteristicasFiscalesClientet);
                        factura.AppendChild(caracteristicasFiscalesCliente);

                        XmlElement tributosCliente = doc.CreateElement(string.Empty, "tributosCliente", string.Empty);
                        factura.AppendChild(tributosCliente);

                        XmlElement tributo = doc.CreateElement(string.Empty, "tributo", string.Empty);
                        XmlText tributot = doc.CreateTextNode("01");
                        tributo.AppendChild(tributot);
                        tributosCliente.AppendChild(tributo);

                        XmlElement importe = doc.CreateElement(string.Empty, "importe", string.Empty);
                        XmlText importet = doc.CreateTextNode(Convert.ToString(rw["HDSUBTOT"]));
                        importe.AppendChild(importet);
                        factura.AppendChild(importe);

                        XmlElement impuesto = doc.CreateElement(string.Empty, "impuesto", string.Empty);
                        factura.AppendChild(impuesto);

                        XmlElement base_ = doc.CreateElement(string.Empty, "base", string.Empty);
                        XmlText base_t = doc.CreateTextNode(Convert.ToString(rw["HDSUBTOT"]));
                        base_.AppendChild(base_t);
                        impuesto.AppendChild(base_);

                        XmlElement porcentaje = doc.CreateElement(string.Empty, "porcentaje", string.Empty);
                        XmlText porcentajet = doc.CreateTextNode("19.00");
                        porcentaje.AppendChild(porcentajet);
                        impuesto.AppendChild(porcentaje);

                        XmlElement valor = doc.CreateElement(string.Empty, "valor", string.Empty);
                        XmlText valort = doc.CreateTextNode(Convert.ToString(rw["HDTOTIVA"]));
                        valor.AppendChild(valort);
                        impuesto.AppendChild(valor);

                        XmlElement codigo = doc.CreateElement(string.Empty, "codigo", string.Empty);
                        XmlText codigot = doc.CreateTextNode("01");
                        codigo.AppendChild(codigot);
                        impuesto.AppendChild(codigo);

                        XmlElement total = doc.CreateElement(string.Empty, "total", string.Empty);
                        XmlText totalt = doc.CreateTextNode(Convert.ToString(rw["HDTOTFAC"]));
                        total.AppendChild(totalt);
                        factura.AppendChild(total);

                        foreach (DataRow rx in Obj.GetPagos(objDB, CODEMP, Convert.ToString(rw["TFTIPFAC"]), Convert.ToInt32(rw["HDNROFAC"])).Rows)
                        {
                            XmlElement formaPago = doc.CreateElement(string.Empty, "formaPago", string.Empty);
                            XmlText formaPagot = doc.CreateTextNode(Convert.ToString(rx["FORMA_PAGO"]));
                            formaPago.AppendChild(formaPagot);
                            factura.AppendChild(formaPago);

                            XmlElement medioPago = doc.CreateElement(string.Empty, "medioPago", string.Empty);
                            XmlText medioPagot = doc.CreateTextNode(Convert.ToString(rx["MEDIO_PAGO"]));
                            medioPago.AppendChild(medioPagot);
                            factura.AppendChild(medioPago);
                        }

                        XmlElement fechaPago = doc.CreateElement(string.Empty, "fechaPago", string.Empty);
                        XmlText fechaPagot = doc.CreateTextNode(Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Year) + "-" + (Convert.ToDateTime(rw["HDFECING"]).Month < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Month) + "-" + (Convert.ToDateTime(rw["HDFECING"]).Day < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Day) + " " + (Convert.ToDateTime(rw["HDFECING"]).Hour < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Hour) + ":" + (Convert.ToDateTime(rw["HDFECING"]).Minute < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Minute) + ":" + (Convert.ToDateTime(rw["HDFECING"]).Second < 10 ? "0" : "") + Convert.ToString(Convert.ToDateTime(rw["HDFECING"]).Second));
                        fechaPago.AppendChild(fechaPagot);
                        factura.AppendChild(fechaPago);

                        using (SqlDataReader reader = Obj.GetFacturaDT(objDB, CODEMP, Convert.ToString(rw["TFTIPFAC"]), Convert.ToInt32(rw["HDNROFAC"])))
                        {
                            while (reader.Read())
                            {
                                XmlElement linea = doc.CreateElement(string.Empty, "linea", string.Empty);
                                factura.AppendChild(linea);

                                XmlElement numero = doc.CreateElement(string.Empty, "numero", string.Empty);
                                XmlText numerot = doc.CreateTextNode(Convert.ToString(reader["DTNROITM"]));
                                numero.AppendChild(numerot);
                                linea.AppendChild(numero);

                                XmlElement descripcion = doc.CreateElement(string.Empty, "descripcion", string.Empty);
                                XmlText descripciont = doc.CreateTextNode(Convert.ToString(reader["ARNOMBRE"]));
                                descripcion.AppendChild(descripciont);
                                linea.AppendChild(descripcion);

                                XmlElement idEstandarLinea = doc.CreateElement(string.Empty, "idEstandarLinea", string.Empty);
                                XmlText idEstandarLineat = doc.CreateTextNode("999");
                                idEstandarLinea.AppendChild(idEstandarLineat);
                                linea.AppendChild(idEstandarLinea);

                                XmlElement codigoEstandarLinea = doc.CreateElement(string.Empty, "codigoEstandarLinea", string.Empty);
                                XmlText codigoEstandarLineat = doc.CreateTextNode(Convert.ToString(reader["CODIGO"]));
                                codigoEstandarLinea.AppendChild(codigoEstandarLineat);
                                linea.AppendChild(codigoEstandarLinea);

                                XmlElement unidadMedidaLinea = doc.CreateElement(string.Empty, "unidadMedidaLinea", string.Empty);
                                XmlText unidadMedidaLineat = doc.CreateTextNode("94");
                                unidadMedidaLinea.AppendChild(unidadMedidaLineat);
                                linea.AppendChild(unidadMedidaLinea);

                                XmlElement unidades = doc.CreateElement(string.Empty, "unidades", string.Empty);
                                XmlText unidadest = doc.CreateTextNode(Convert.ToString(reader["DTCANTID"]));
                                unidades.AppendChild(unidadest);
                                linea.AppendChild(unidades);

                                XmlElement precioUnidad = doc.CreateElement(string.Empty, "precioUnidad", string.Empty);
                                XmlText precioUnidadt = doc.CreateTextNode(Convert.ToString(reader["DTPRELIS"]));
                                precioUnidad.AppendChild(precioUnidadt);
                                linea.AppendChild(precioUnidad);

                                XmlElement imported = doc.CreateElement(string.Empty, "importe", string.Empty);
                                XmlText importedt = doc.CreateTextNode(Convert.ToString(reader["DTSUBTOT"]));
                                imported.AppendChild(importedt);
                                linea.AppendChild(imported);

                                XmlElement based = doc.CreateElement(string.Empty, "base", string.Empty);
                                XmlText basedt = doc.CreateTextNode(Convert.ToString(reader["DTSUBTOT"]));
                                based.AppendChild(basedt);
                                linea.AppendChild(based);

                                XmlElement porcentajeImpuesto = doc.CreateElement(string.Empty, "porcentajeImpuesto", string.Empty);
                                XmlText porcentajeImpuestot = doc.CreateTextNode("19.00");
                                porcentajeImpuesto.AppendChild(porcentajeImpuestot);
                                linea.AppendChild(porcentajeImpuesto);

                                XmlElement impuestod = doc.CreateElement(string.Empty, "impuesto", string.Empty);
                                XmlText impuestodt = doc.CreateTextNode(Convert.ToString(reader["DTTOTIVA"]));
                                impuestod.AppendChild(impuestodt);
                                linea.AppendChild(impuestod);

                                XmlElement codigoImpuesto = doc.CreateElement(string.Empty, "codigoImpuesto", string.Empty);
                                XmlText codigoImpuestot = doc.CreateTextNode("01");
                                codigoImpuesto.AppendChild(codigoImpuestot);
                                linea.AppendChild(codigoImpuesto);



                                XmlElement datoAdicionalb = doc.CreateElement(string.Empty, "datoAdicional", string.Empty);
                                linea.AppendChild(datoAdicionalb);

                                XmlElement tipoDatob = doc.CreateElement(string.Empty, "tipoDato", string.Empty);
                                XmlText tipoDatobt = doc.CreateTextNode("A");
                                tipoDatob.AppendChild(tipoDatobt);
                                datoAdicionalb.AppendChild(tipoDatob);

                                XmlElement valorStringb = doc.CreateElement(string.Empty, "valorString", string.Empty);
                                XmlText valorStringbt = doc.CreateTextNode(Convert.ToString(reader["BARRAS"]));
                                valorStringb.AppendChild(valorStringbt);
                                datoAdicionalb.AppendChild(valorStringb);

                                XmlElement ordenb = doc.CreateElement(string.Empty, "orden", string.Empty);
                                XmlText ordenbt = doc.CreateTextNode("1");
                                ordenb.AppendChild(ordenbt);
                                datoAdicionalb.AppendChild(ordenb);
                            }
                        }

                        XmlElement datoAdicional = doc.CreateElement(string.Empty, "datoAdicional", string.Empty);
                        factura.AppendChild(datoAdicional);

                        XmlElement tipoDato = doc.CreateElement(string.Empty, "tipoDato", string.Empty);
                        XmlText tipoDatot = doc.CreateTextNode("A");
                        tipoDato.AppendChild(tipoDatot);
                        datoAdicional.AppendChild(tipoDato);

                        XmlElement valorString = doc.CreateElement(string.Empty, "valorString", string.Empty);
                        XmlText valorStringt = doc.CreateTextNode(Convert.ToString(rw["LETRAS"]));
                        valorString.AppendChild(valorStringt);
                        datoAdicional.AppendChild(valorString);

                        XmlElement orden = doc.CreateElement(string.Empty, "orden", string.Empty);
                        XmlText ordent = doc.CreateTextNode("1");
                        orden.AppendChild(ordent);
                        datoAdicional.AppendChild(orden);

                        XmlElement tipoDatoo = doc.CreateElement(string.Empty, "tipoDato", string.Empty);
                        XmlText tipoDatoot = doc.CreateTextNode("A");
                        tipoDatoo.AppendChild(tipoDatoot);
                        datoAdicional.AppendChild(tipoDatoo);

                        XmlElement Observaciones = doc.CreateElement(string.Empty, "valorString", string.Empty);
                        XmlText Observacionest = doc.CreateTextNode(Convert.ToString(rw["HDOBSERV"]));
                        Observaciones.AppendChild(Observacionest);
                        datoAdicional.AppendChild(Observaciones);

                        XmlElement ordeno = doc.CreateElement(string.Empty, "orden", string.Empty);
                        XmlText ordenot = doc.CreateTextNode("2");
                        ordeno.AppendChild(ordenot);
                        datoAdicional.AppendChild(ordeno);


                        XmlElement tipoDatov = doc.CreateElement(string.Empty, "tipoDato", string.Empty);
                        XmlText tipoDatovt = doc.CreateTextNode("A");
                        tipoDatov.AppendChild(tipoDatovt);
                        datoAdicional.AppendChild(tipoDatov);

                        XmlElement Vendedor = doc.CreateElement(string.Empty, "valorString", string.Empty);
                        XmlText VendedorT = doc.CreateTextNode(Convert.ToString(rw["TRNOMBRE"]));
                        Vendedor.AppendChild(VendedorT);
                        datoAdicional.AppendChild(Vendedor);

                        XmlElement ordenv = doc.CreateElement(string.Empty, "orden", string.Empty);
                        XmlText ordenvt = doc.CreateTextNode("3");
                        ordenv.AppendChild(ordenvt);
                        datoAdicional.AppendChild(ordenv);

                        XmlElement tipoDatop = doc.CreateElement(string.Empty, "tipoDato", string.Empty);
                        XmlText tipoDatopt = doc.CreateTextNode("A");
                        tipoDatop.AppendChild(tipoDatopt);
                        datoAdicional.AppendChild(tipoDatop);

                        XmlElement Pedido = doc.CreateElement(string.Empty, "valorString", string.Empty);
                        XmlText Pedidot = doc.CreateTextNode("0");
                        Pedido.AppendChild(Pedidot);
                        datoAdicional.AppendChild(Pedido);

                        XmlElement ordenp = doc.CreateElement(string.Empty, "orden", string.Empty);
                        XmlText ordenpt = doc.CreateTextNode("4");
                        ordenp.AppendChild(ordenpt);
                        datoAdicional.AppendChild(ordenp);

                        doc.Save("C://tmp//" + lc_nit + "_" + lc_ano + "_" + lc_factura + ".xml");

                        //objftp.SendFile("C://tmp//" + lc_nit + "-" + lc_ano + "-" + lc_factura + ".xml", "186.154.255.74", "operaciones", "O12345");
                        //objftp.SendFile("C://tmp//" + lc_nit + "-" + lc_ano + "-" + lc_factura + ".xml", ServerFTP, UserFTP, PwdFTP);
                        
                        
                        //objftp.SendFileSTFP("C://tmp//" + lc_nit + "_" + lc_ano + "_" + lc_factura + ".xml", ServerFTP, UserFTP, PwdFTP);

                        Obj.InsertFacturaEnvio(objDB, CODEMP, Convert.ToString(ry["HDTIPFAC"]), Convert.ToInt32(ry["HDNROFAC"]));
                    }

                    xmlDeclaration = null;
                    root = null;
                    doc = null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                objDB = null;                
                Obj = null;
            }

            Console.WriteLine("Hello World!");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
