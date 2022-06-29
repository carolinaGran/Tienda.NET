using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace PedidosCapaDAL
{
   class Auxiliar
    {
        static string cadenaConexion;
        static Auxiliar() {
       
            cadenaConexion= WebConfigurationManager.ConnectionStrings["BBDD_Pedidos"].ConnectionString;
        
        }
        //public Auxiliar()
        //{
          
        //}
        public static string CadenaConexion
        {
            get
            {
                //if (cadenaConexion == null)
                //    cadenaConexion = WebConfigurationManager.ConnectionStrings["BBDD_Pedidis"].ConnectionString;
                return cadenaConexion;
            }
        }

    }
}
