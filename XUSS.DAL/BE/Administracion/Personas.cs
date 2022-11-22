using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mapping;

namespace BE.Administracion
{
    [Table(LogicalName = "Personas", PhysicalName = "personas", EntityId = 42, NumberColumns = 10)]
    public class Personas
    {
        private int intParmTipoDoc;
        private long intPersNroDoc;
        private string strDiviCodigo;
        private string strPersNombre;
        private string strPersNombreCorto;
        private string strPersDireccion;
        private string strPersTelefono;
        private string strPersEmail;
        private int? intPersTipoDoc;
        private string strPersNumDoc;

        public Personas(int parmTipoDoc, long persNroDoc, string diviCodigo, string persNombre, string persNombreCorto, string persDireccion, string persTelefono, string persEmail, int? persTipoDoc, string persNumDoc)
        {
            this.intParmTipoDoc = parmTipoDoc;
            this.intPersNroDoc = persNroDoc;
            this.strDiviCodigo = diviCodigo;
            this.strPersNombre = persNombre;
            this.strPersNombreCorto = persNombreCorto;
            this.strPersDireccion = persDireccion;
            this.strPersTelefono = persTelefono;
            this.strPersEmail = persEmail;
            this.intPersTipoDoc = persTipoDoc;
            this.strPersNumDoc = persNumDoc;
        }

        public Personas()
        { }

        [Field(LogicalName = "PersNroDoc", PhysicalName = "pers_nrodoc", FieldType = System.Data.DbType.Int64, Alias = "Numero de Documento", PrimaryKey = true, IsSequence = false, IsDiscriminant = false, IsFilterable = true)]
        public long PersNroDoc
        {
            get { return intPersNroDoc; }
            set { intPersNroDoc = value; }
        }

        [Field(LogicalName = "ParmTipoDoc", PhysicalName = "parm_tipodoc", FieldType = System.Data.DbType.Int32, Alias = "Tipo Documento", PrimaryKey = true, IsSequence = false, IsDiscriminant = false, IsFilterable = true)]
        public int ParmTipoDoc
        {
            get { return intParmTipoDoc; }
            set { intParmTipoDoc = value; }
        }
        [Field(LogicalName = "DiviCodigo", PhysicalName = "divi_codigo", FieldType = System.Data.DbType.String, Alias = "Codigo", PrimaryKey = false, IsSequence = false, IsDiscriminant = false, IsFilterable = true)]
        public string DiviCodigo
        {
            get { return strDiviCodigo; }
            set { strDiviCodigo = value; }
        }

        [Field(LogicalName = "PersNombre", PhysicalName = "pers_nombre", FieldType = System.Data.DbType.String, Alias = "Nombre", PrimaryKey = false, IsSequence = false, IsDiscriminant = false, IsFilterable = true)]
        public string PersNombre
        {
            get { return strPersNombre; }
            set { strPersNombre = value; }
        }

        [Field(LogicalName = "PersNombreCorto", PhysicalName = "pers_nombrecorto", FieldType = System.Data.DbType.String, Alias = "Nombre Corto", PrimaryKey = false, IsSequence = false, IsDiscriminant = false, IsFilterable = true)]
        public string PersNombreCorto
        {
            get { return strPersNombreCorto; }
            set { strPersNombreCorto = value; }
        }
        [Field(LogicalName = "PersDireccion", PhysicalName = "pers_direccion", FieldType = System.Data.DbType.String, Alias = "Direccion", PrimaryKey = false, IsSequence = false, IsDiscriminant = false, IsFilterable = true)]
        public string PersDireccion
        {
            get { return strPersDireccion; }
            set { strPersDireccion = value; }
        }
        [Field(LogicalName = "PersTelefono", PhysicalName = "pers_telefono", FieldType = System.Data.DbType.String, Alias = "Telefono", PrimaryKey = false, IsSequence = false, IsDiscriminant = false, IsFilterable = true)]
        public string PersTelefono
        {
            get { return strPersTelefono; }
            set { strPersTelefono = value; }
        }
        [Field(LogicalName = "PersEmail", PhysicalName = "pers_email", FieldType = System.Data.DbType.String, Alias = "Email", PrimaryKey = false, IsSequence = false, IsDiscriminant = false, IsFilterable = true)]
        public string PersEmail
        {
            get { return strPersEmail; }
            set { strPersEmail = value; }
        }
        [Field(LogicalName = "PersTipoDoc", PhysicalName = "pers_tipodoc", FieldType = System.Data.DbType.Int32, Alias = " ", PrimaryKey = false, IsSequence = false, IsDiscriminant = false, IsFilterable = false)]
        public int? PersTipoDoc
        {
            get { return intPersTipoDoc; }
            set { intPersTipoDoc = value; }
        }
        [Field(LogicalName = "PersNumDoc", PhysicalName = "pers_numdoc", FieldType = System.Data.DbType.String, Alias = " ", PrimaryKey = false, IsSequence = false, IsDiscriminant = false, IsFilterable = false)]
        public string PersNumDoc
        {
            get { return strPersNumDoc; }
            set { strPersNumDoc = value; }
        }
    }
}
