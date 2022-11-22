using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using BE.Administracion;

namespace DAL.Administracion
{
	public class AdmiRestringeOpcionDB : GenericBaseDB<AdmiRestringeOpcion>
	{
        public AdmiRestringeOpcionDB()
            : base("admi_pGeneraSecuencia")
        {
        }
	}
}
