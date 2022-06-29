using ModeloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosCapaDAL
{
   public class PedidosDAL
    {
        public static Pedido CrearPedido(int codigoUsuario)
        {
            Pedido p = null;


            using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection())
            {
                cn.ConnectionString = Auxiliar.CadenaConexion;
                var cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CrearPedido";
                cmd.Parameters.AddWithValue("@codigoCliente", codigoUsuario);


                cn.Open();
                var datos = cmd.ExecuteReader();
                if (datos.Read())
                {
                    p = new Pedido();
                    p.Codigo = Convert.ToInt32(datos["Codigo"]);
                    p.CodigoCliente = Convert.ToInt32(datos["CodigoCliente"]);
                    p.ImporteTotal = Convert.ToSingle(datos["ImporteTotal"]);
                    p.FechaPedido = Convert.ToDateTime(datos["FechaPedido"]);

                }

            }
       
            return p;
        }

        public static LineaDetalle[] ObtenerLineasDetalle(Dictionary<string, string> datos)
        {
            throw new NotImplementedException();
        }

        public static float AltaLineasDetalle(int codigoPedido, LineaDetalle[] lineasDetalle) {
            float total = 0;
            using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection())
            {
                cn.ConnectionString = Auxiliar.CadenaConexion;
                var cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "AltaLineaDetalle";
                cmd.Parameters.AddWithValue("@codigoPedido", codigoPedido);

                cmd.Parameters.Add("@codigoProducto",System.Data.SqlDbType.NVarChar,25);
                cmd.Parameters.Add("@descripcion", System.Data.SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@unidades", System.Data.SqlDbType.Int);
                cmd.Parameters.Add("@precioVenta", System.Data.SqlDbType.Float);


                cn.Open();
                foreach (var ld in lineasDetalle)
                {
                    cmd.Parameters["@codigoProducto"].Value= ld.CodigoProducto;
                    cmd.Parameters["@descripcion"].Value= ld.Descripcion;
                    cmd.Parameters["@unidades"].Value= ld.Unidades;
                    cmd.Parameters["@precioVenta"].Value= ld.PrecioVenta;
                    cmd.ExecuteNonQuery();
                    total += ld.Unidades * ld.PrecioVenta;
                }
                //var datos = cmd.ExecuteReader();
                ////if (datos.Read())
                //{
                //    p = new Pedido();
                //    p.Codigo = Convert.ToInt32(datos["Codigo"]);
                //    p.CodigoCliente = Convert.ToInt32(datos["CodigoCliente"]);
                //    p.ImporteTotal = Convert.ToSingle(datos["ImporteTotal"]);
                //    p.FechaPedido = Convert.ToDateTime(datos["FechaPedido"]);

                //}

            }
            return total;
        }
        public static void ActualizarImportePedido(int codigoPedido, float importe)
        {
            using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection())
            {
                cn.ConnectionString = Auxiliar.CadenaConexion;
                var cmd = cn.CreateCommand();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandText = "UPDATE Pedidos SET ImporteTotal = " + importe + " WHERE Codigo = " + codigoPedido;
                cmd.CommandText = "UPDATE Pedidos SET ImporteTotal = @importe WHERE Codigo = @codigoPedido";
                cmd.Parameters.AddWithValue("@codigoPedido", codigoPedido);
                cmd.Parameters.AddWithValue("@importe", importe);
                cn.Open();
                cmd.ExecuteNonQuery();


            }
        }
        public static Pedido ModificarPedido(Dictionary<string,string> datos)
        {
            Pedido p=null;
            using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection())
            {
                cn.ConnectionString = Auxiliar.CadenaConexion;
                var cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "UPDATE Pedidos SET ";
                string accion = datos["accion"];
                switch (accion)
                {
                    case "Preparar":
                        cmd.CommandText += "FechaPreparacion = GETDATE(), CodEmpleadoPrep = @codigoEmpleado WHERE Codigo = @codigoPedido AND FechaPreparacion IS NULL AND FechaCancelacion IS NULL";
                        break;
                    case "Enviar":
                        cmd.CommandText += "FechaEnvio = GETDATE(), CodEmpleadoEnv = @codigoEmpleado WHERE Codigo = @codigoPedido AND FechaEnvio IS NULL AND FechaCancelacion IS NULL";
                        break;
                    case "Cancelar":
                        cmd.CommandText += "FechaCancelacion= GETDATE() WHERE codigo=@codigoPedido";
                        break;
                    default:
                        break;
                }
                cmd.Parameters.AddWithValue("@codigoPedido", datos["codigoPedido"]);
                if(datos.ContainsKey("codigoEmpleado"))
                    cmd.Parameters.AddWithValue("@codigoEmpleado", datos["codigoEmpleado"]);
                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if(rowsAffected != 0)
                {
                    var d = new Dictionary<string, string>();
                    d.Add("codigoPedido", datos["codigoPedido"]);
                    var pedidos = ObtenerPedidos(d);
                    p = pedidos[0];
                }
            }
            return p;
        }
            public static Pedido[] ObtenerPedidos(Dictionary<string,string> datos)
        {
            using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection())
            {
                cn.ConnectionString = Auxiliar.CadenaConexion;
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Pedidos";
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandText = "UPDATE Pedidos SET ImporteTotal = " + importe + " WHERE Codigo = " + codigoPedido;
                var filtro = "";
                if (datos.ContainsKey("codigoCliente"))
                {
                    if (filtro == "")
                    {
                        filtro = " WHERE ";
                        filtro += "CodigoCliente = @codigoCliente";
                        cmd.Parameters.AddWithValue("@codigoCliente", Convert.ToInt32(datos["codigoCliente"]));
                    }
                }
                    if (datos.ContainsKey("fDesde"))
                    {
                        if (filtro == "")

                            filtro += " WHERE ";

                        else

                            filtro += " AND ";

                        filtro += "FechaPedido >= @fDesde";
                        cmd.Parameters.AddWithValue("@fDesde", Convert.ToDateTime(datos["fDesde"]));

                    }
                    if (datos.ContainsKey("fHasta"))
                    {
                        if (filtro == "")

                            filtro += " WHERE ";

                        else

                            filtro += " AND ";

                        filtro += "FechaPedido <= @fHasta";
                        cmd.Parameters.AddWithValue("@fHasta", Convert.ToDateTime(datos["fHasta"]));

                    }
                if (datos.ContainsKey("codigoPedido"))
                {
                    if (filtro == "")

                        filtro += " WHERE ";

                    else

                        filtro += " AND ";

                    filtro += "Codigo = @codigoPedido";
                    cmd.Parameters.AddWithValue("@codigoPedido", Convert.ToInt32(datos["codigoPedido"]));

                }

                cmd.CommandText += filtro;

                    cn.Open();
                    var pedidosReader = cmd.ExecuteReader();
                    List<Pedido> pedidos = new List<Pedido>();
                while (pedidosReader.Read())
                {
                    Pedido p = new Pedido();
                    p.Codigo = Convert.ToInt32(pedidosReader["Codigo"]);
                    p.CodigoCliente = Convert.ToInt32(pedidosReader["CodigoCliente"]);
                    p.ImporteTotal = Convert.ToSingle(pedidosReader["ImporteTotal"]);
                    p.FechaPedido = Convert.ToDateTime(pedidosReader["FechaPedido"]);
                    //p.FechaPedidoAAAAMMDD = p.FechaPedido.ToString("u");
                    p.FechaPedidoCadena = p.FechaPedido.ToString();
                    //p.FechaPedidoAAAAMMDD = p.FechaPedido.ToString("yyyyMMdd HH:mm:ss");
                    if (pedidosReader["FechaPreparacion"] != DBNull.Value)
                        p.FechaPreparacionCadena = Convert.ToDateTime(pedidosReader["fechaPreparacion"]).ToString();

                    if (pedidosReader["FechaEnvio"] != DBNull.Value)
                        p.FechaEnvioCadena = Convert.ToDateTime(pedidosReader["fechaEnvio"]).ToString();
                    if (pedidosReader["FechaCancelacion"] != DBNull.Value)
                        p.FechaCancelacionCadena = Convert.ToDateTime(pedidosReader["FechaCancelacion"]).ToString();
                    if (datos.ContainsKey("incluirEmpleados") && bool.Parse(datos["incluirEmpleados"])) { 
                    if (pedidosReader["CodEmpleadoPrep"] != DBNull.Value)
                        p.CodigoEmpleadoPrep = Convert.ToInt32(pedidosReader["CodEmpleadoPrep"]);
                    if (pedidosReader["CodEmpleadoEnv"] != DBNull.Value)
                        p.CodigoEmpleadoEnv = Convert.ToInt32(pedidosReader["CodEmpleadoEnv"]);
                }



                    pedidos.Add(p);
                    }
                    return pedidos.ToArray();


                }
            }
        
    public static LineaDetalle[] ObtenerLineasDetalles(Dictionary<string, string> datos)
    {
        using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection())
        {

            cn.ConnectionString = Auxiliar.CadenaConexion;

            var cmd = cn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = "SELECT * FROM LineasDetalle WHERE codigoPedido = @codigoPedido";
            cmd.Parameters.AddWithValue("@codigoPedido", Convert.ToInt32(datos["codigoPedido"]));

            cn.Open();

            var detallesReader = cmd.ExecuteReader();
            List<LineaDetalle> detalles = new List<LineaDetalle>();



            while (detallesReader.Read())
            {
                LineaDetalle d = new LineaDetalle();

                d.Codigo = Convert.ToInt32(detallesReader["Codigo"]);
                d.CodigoPedido = Convert.ToInt32(detallesReader["CodigoPedido"]);
                d.CodigoProducto = detallesReader["CodigoProducto"].ToString();
                d.Descripcion = detallesReader["Descripcion"].ToString();
                d.Unidades = Convert.ToInt32(detallesReader["Unidades"]);
                d.PrecioVenta = Convert.ToSingle(detallesReader["PrecioVenta"]);

                detalles.Add(d);


            }
            return detalles.ToArray();


        }

    }
}
    }

