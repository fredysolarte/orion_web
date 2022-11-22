using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using BE.Administracion;

namespace DAL.Administracion
{
	public class AdmiOpcionxrolDB : GenericBaseDB<AdmiOpcionxrol>
	{
        public AdmiOpcionxrolDB()
            : base("admi_pGeneraSecuencia")
        {
        }
	}
}
