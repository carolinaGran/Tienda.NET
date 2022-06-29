using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDatos;
namespace PedidosCapaDAL
{
    public class ProductosDAL
    {
        public static List<Producto> ObtenerProductosDAL()
        {
            // ...
            List<Producto> Productos = new List<Producto>();
            //{
            //    //throw new Exception("no tengo productos");
            //    List<Producto> Producto = new List<Producto>();
            //    //  Models.Producto[] Productos2 = new Models.Producto[90];

            //    var rnd = new Random();
            //    Producto p;
            //    for (int i = 1; i <= 5; i++)
            //    {
            //        p = new Producto();
            //        p.Codigo = "Prod" + i;
            //        p.Descripcion = "Producto " + i;
            //        p.PrecioVenta = (float)Math.Round(rnd.NextDouble() * 1000, 2);
            //        Productos.Add(p);
            //    }

            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();
    // cn.ConnectionString="data source=DESKTOP-4FI5E6R; initial catalog=Beca2021 ;User ID=UsrBeca; Password=1234";
           // cn.ConnectionString = "data source=(local); initial catalog=Beca2021 ;User ID=UsrBeca; Password=1234";
           //cn.ConnectionString= System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BBDD_Pedidos"].ConnectionString;
            // cn.ConnectionString = "data source=.; initial catalog=Beca2021 ;User ID=UsrBeca; Password=1234";

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cn.ConnectionString = Auxiliar.CadenaConexion;

            cmd.Connection = cn;
            cmd.CommandType =System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM PRODUCTOS";

            cn.Open();
            System.Data.SqlClient.SqlDataReader datos = cmd.ExecuteReader();
           while(datos.Read())
            {
                var p = new Producto();
                p.Codigo = datos["Codigo"].ToString();
                p.Descripcion = datos["Descripcion"].ToString();
                p.PrecioVenta =Convert.ToSingle(datos["PrecioVenta"]);
                Productos.Add(p);

            }
            cn.Close();
            return Productos;
            }
        }
    }

