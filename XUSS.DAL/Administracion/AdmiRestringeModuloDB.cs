using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using BE.Administracion;

namespace DAL.Administracion
{
	public class AdmiRestringeModuloDB : GenericBaseDB<AdmiRestringeModulo>
	{
        public AdmiRestringeModuloDB()
            : base("admi_pGeneraSecuencia")
        {
        }
	}
}
