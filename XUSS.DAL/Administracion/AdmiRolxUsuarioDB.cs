using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using BE.Administracion;

namespace DAL.Administracion
{
	public class AdmiRolxUsuarioDB : GenericBaseDB<AdmiRolxUsuario>
	{
        public AdmiRolxUsuarioDB()
            : base("admi_pGeneraSecuencia")
        {
        }
	}
}
