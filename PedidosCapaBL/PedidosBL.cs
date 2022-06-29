using ModeloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosCapaBL
{
    public class PedidosBL
    {
        public static Pedido CrearPedido(int codigoUsuario, LineaDetalle[] lineasDetalle)
        {
            Pedido p = PedidosCapaDAL.PedidosDAL.CrearPedido(codigoUsuario); ;

            var importe = PedidosCapaDAL.PedidosDAL.AltaLineasDetalle(p.Codigo, lineasDetalle);
            PedidosCapaDAL.PedidosDAL.ActualizarImportePedido(p.Codigo, importe);
            return p;
        }


        public static Pedido[] ObtenerPedidos(Dictionary<string, string> datos)
        {
            var pedidos = PedidosCapaDAL.PedidosDAL.ObtenerPedidos(datos);
            if (datos.ContainsKey("incluirEmpleados") && bool.Parse(datos["incluirEmpleados"])) {
                var empleados = PedidosCapaDAL.EmpleadosDAL.ObtenerEmpleados();
                Dictionary<int, Empleado> dicEmpleados = new Dictionary<int, Empleado>();
                foreach (var emp in empleados)
                {
                    dicEmpleados.Add(emp.Codigo, emp);
                }
                foreach (var p in pedidos)
                {
                    if (p.CodigoEmpleadoPrep.HasValue)
                    {
                        //foreach(var e in empleados)
                        //{
                        //    if (e.Codigo == p.CodigoEmpleadoPrep.Value)
                        //    {
                        //        p.NombreEmpleadoPrep = e.NombreCompleto;
                        //        break;
                        //    }
                        //}
                        p.NombreEmpleadoPrep = dicEmpleados[p.CodigoEmpleadoPrep.Value].NombreCompleto;
                    }
                    if (p.CodigoEmpleadoEnv.HasValue) {
                        p.NombreEmpleadoEnv = dicEmpleados[p.CodigoEmpleadoEnv.Value].NombreCompleto;
                    }
                
            }
        }
            return pedidos;
        }
        public static LineaDetalle[] ObtenerLineasDetalle(Dictionary<string, string> datos)
        {
            return PedidosCapaDAL.PedidosDAL.ObtenerLineasDetalles(datos);
        }
        public static Pedido ModificarPedido(Dictionary<string, string> datos)
        {
            return PedidosCapaDAL.PedidosDAL.ModificarPedido(datos);
        }

    }
}