using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using BE.Administracion;

namespace DAL.Administracion
{
    public class AdmiModuloxrolDB : GenericBaseDB<AdmiModuloxrol>
    {
        public AdmiModuloxrolDB()
            : base("admi_pGeneraSecuencia")
        {
        }
    }
}
