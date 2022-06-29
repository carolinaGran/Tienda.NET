using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDatos;
namespace PedidosCapaBL
{
   public class EmpleadosBL
    {
        public static Empleado ValidarEmpleado(Dictionary<string,string> info) { 

            if (!info.ContainsKey("txtNombreEmpleado"))
            {
                throw new Exception("falta nombre");
            }
            if (!info.ContainsKey("txtPassword"))
            {
                throw new Exception("falta Password ");
            }
            var txtNombreEmpleado = info["txtNombreEmpleado"];
            var txtPassword = info["txtPassword"];

            Empleado emp = PedidosCapaDAL.EmpleadosDAL.ObtenerEmpleado(txtNombreEmpleado);

            //if (txtMail.IndexOf("@") != -1 && txtPassword != "X")
            //{
            //    cli = new Cliente();
            //    cli.Codigo = 1;
            //    cli.NombreCliente = "carol";
            //    cli.ApellidosCliente = "gg";
            //    cli.MailCliente = txtMail;
            //}
            if (emp != null && emp.Password != txtPassword)
                emp = null;


            return emp;
        }

    }
}
