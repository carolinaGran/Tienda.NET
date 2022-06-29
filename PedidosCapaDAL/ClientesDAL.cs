using ModeloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosCapaDAL
{
    public class ClientesDAL
    {
        public static Cliente ObtenerCliente(string mail)
        {
            Cliente cli = null;
            using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection()) {
                //cn.ConnectionString = "data source=(local); initial catalog=Beca2021 ;User ID=UsrBeca; Password=1234";
                cn.ConnectionString = Auxiliar.CadenaConexion;
                //  System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                // cmd.Connection = cn;
                var cmd = cn.CreateCommand();
                cmd.Parameters.AddWithValue("@mail", mail);
                //cmd.CommandType = System.Data.CommandType.Text;//lo es por defecto
                //cmd.CommandText = "SELECT * FROM Clientes WHERE Mail= '" + mail + "' ";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "ObtenerClientePorMail";
                cn.Open();
            System.Data.SqlClient.SqlDataReader datos = cmd.ExecuteReader();

            
            while (datos.Read())
            {
                cli = new Cliente();
                cli.Codigo = Convert.ToInt32(datos["Codigo"]);
                //cli.Codigo = Convert.ToInt32(datos[0]);
                //cli.Codigo = datos.GetInt32(0);

                cli.MailCliente = datos["Mail"].ToString();
                cli.PasswordCliente = datos["Password"].ToString();
                cli.NombreCliente = datos["Nombre"].ToString();
                cli.ApellidosCliente = datos["Apellidos"].ToString();


            

            }
            //cn.Close();
            }
            return cli;
        }

    }
}
