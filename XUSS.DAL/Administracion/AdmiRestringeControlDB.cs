using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using BE.Administracion;

namespace DAL.Administracion
{
	public class AdmiRestringeControlDB : GenericBaseDB<AdmiRestringeControl>
	{
        public AdmiRestringeControlDB()
            : base("admi_pGeneraSecuencia")
        {
        }
	}
}
