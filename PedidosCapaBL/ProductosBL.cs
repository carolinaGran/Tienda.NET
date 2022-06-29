using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDatos;

namespace PedidosCapaBL
{
    public class ProductosBL
    {
        public static List<Producto> ObtenerProductosBL()
        {
            //var pDAL = new PedidosCapaDAL.ProductosDAL();
            //var p = pDAL.ObtenerProductosDAL();
            return PedidosCapaDAL.ProductosDAL.ObtenerProductosDAL();
        }
    }
}
