using ModeloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosCapaBL
{
   public class ClientesBL
    {
        public static Cliente  ValidarCliente(Dictionary<string, string> info){
       
        if (!info.ContainsKey("txtMail"))
                {
                    throw new Exception("falta email ");
    }
                if (!info.ContainsKey("txtPassword"))
                {
                    throw new Exception("falta Password ");
}
var txtMail = info["txtMail"];
var txtPassword = info["txtPassword"];

Cliente cli = PedidosCapaDAL.ClientesDAL.ObtenerCliente(txtMail);

//if (txtMail.IndexOf("@") != -1 && txtPassword != "X")
//{
//    cli = new Cliente();
//    cli.Codigo = 1;
//    cli.NombreCliente = "carol";
//    cli.ApellidosCliente = "gg";
//    cli.MailCliente = txtMail;
//}
if(cli != null && cli.PasswordCliente != txtPassword)
                cli = null;
            

return cli;
    }
    }
}
