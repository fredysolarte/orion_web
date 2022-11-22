using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mapping;


namespace BE.Administracion
{
    [Table(LogicalName = "AdmiColumna", PhysicalName = "Admi_tColumna", EntityId = 17, NumberColumns = 3)]
    public class AdmiColumna
    {
        private int intTablTabla;
        private int intColuColumna;
        private string strColuNombre;
        private string strColuAlias;
        private string strColuDescripcion;
        private string strColuTipoDato;

        public AdmiColumna(int tablTabla, int coluColumna, string coluNombre, string coluAlias, string coluDescripcion, string coluTipoDato)
        {
            this.intTablTabla = tablTabla;
            this.intColuColumna = coluColumna;
            this.strColuNombre = coluNombre;
            this.strColuAlias = coluAlias;
            this.strColuDescripcion = coluDescripcion;
            this.strColuTipoDato = coluTipoDato;

        }

        public AdmiColumna()
        { }

        [Field(LogicalName = "TablTabla", PhysicalName = "tabl_tabla", FieldType = System.Data.DbType.Int32, PrimaryKey = true, IsSequence = false, IsDiscriminant = false)]
        public int TablTabla
        {
            get { return intTablTabla; }
            set { intTablTabla = value; }
        }
        [Field(LogicalName = "ColuColumna", PhysicalName = "colu_columna", FieldType = System.Data.DbType.Int32, PrimaryKey = false, IsSequence = false, IsDiscriminant = false)]
        public int ColuColumna
        {
            get { return intColuColumna; }
            set { intColuColumna = value; }
        }
        [Field(LogicalName = "ColuNombre", PhysicalName = "colu_nombre", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false)]
        public string ColuNombre
        {
            get { return strColuNombre; }
            set { strColuNombre = value; }
        }
        [Field(LogicalName = "ColuAlias", PhysicalName = "colu_alias", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false)]
        public string ColuAlias
        {
            get { return strColuAlias; }
            set { strColuAlias = value; }
        }
        [Field(LogicalName = "ColuDescripcion", PhysicalName = "colu_descripcion", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false)]
        public string ColuDescripcion
        {
            get { return strColuDescripcion; }
            set { strColuDescripcion = value; }
        }
        [Field(LogicalName = "ColuTipoDato", PhysicalName = "colu_tipoDato", FieldType = System.Data.DbType.String, PrimaryKey = false, IsSequence = false, IsDiscriminant = false)]
        public string ColuTipoDato
        {
            get { return strColuTipoDato; }
            set { strColuTipoDato = value; }
        }

    }
}
