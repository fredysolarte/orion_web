using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE.Administracion;
using DataAccess;

namespace DAL.Administracion
{
	public class AdmiControlxrolDB : GenericBaseDB<AdmiControlxrol>
	{
        public AdmiControlxrolDB()
            : base("admi_pGeneraSecuencia")
        {
        }
	}
}
