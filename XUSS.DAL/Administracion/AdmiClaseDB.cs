using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using BE.Administracion;

namespace DAL.Administracion
{
	public class AdmiClaseDB : GenericBaseDB<AdmiClase>
	{
        public AdmiClaseDB()
            : base("admi_pGeneraSecuencia")
        {
        }
	}
}
