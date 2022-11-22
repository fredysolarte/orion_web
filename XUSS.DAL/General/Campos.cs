using System;

namespace BE.General
{
    [Serializable()]
    public class Campos
    {
        private string logicalName;

        public string LogicalName
        {
            get { return logicalName; }
            set { logicalName = value; }
        }
        private string fileColumnName;

        public string FileColumnName
        {
            get { return fileColumnName; }
            set { fileColumnName = value; }
        }
        private string format;

        public string Format
        {
            get { return format; }
            set { format = value; }
        }
        private bool estado;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}
