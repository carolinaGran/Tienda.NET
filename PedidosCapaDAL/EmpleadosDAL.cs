using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDatos;
namespace PedidosCapaDAL
{
   public class EmpleadosDAL
    {
        public static List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>();
            using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection())
            {
                //cn.ConnectionString = "data source=(local); initial catalog=Beca2021 ;User ID=UsrBeca; Password=1234";
                cn.ConnectionString = Auxiliar.CadenaConexion;
                //  System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                // cmd.Connection = cn;
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT*FROM Empleados";// WHERE NombreEmpleado = @nombreEmpleado";
                                                          //cmd.Parameters.AddWithValue("@mail", mail);
                                                          //cmd.CommandType = System.Data.CommandType.Text;//lo es por defecto
                                                          //cmd.CommandText = "SELECT * FROM Clientes WHERE Mail= '" + mail + "' ";
                                                          //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                                          //cmd.CommandText = "ObtenerClientePorMail";
                                                          // cmd.Parameters.AddWithValue("@nombreEmpleado", nombreEmpleado);
                cn.Open();
                System.Data.SqlClient.SqlDataReader datos = cmd.ExecuteReader();


                while (datos.Read())
                {
                    var emp = new Empleado();
                    emp.Codigo = Convert.ToInt32(datos["Codigo"]);
                    //cli.Codigo = Convert.ToInt32(datos[0]);
                    //cli.Codigo = datos.GetInt32(0);

                    emp.Nombre = datos["Nombre"].ToString();
                    emp.Password = datos["Password"].ToString();
                    emp.NombreEmpleado = datos["NombreEmpleado"].ToString();
                    emp.Apellidos = datos["Apellidos"].ToString();
                    if (datos["PuedePrepararPedidos"] != DBNull.Value)
                        emp.PuedePrepararPedidos = Convert.ToBoolean(datos["PuedePrepararPedidos"]);
                    if (datos["PuedeEnviarPedidos"] != DBNull.Value)

                        emp.PuedeEnviarPedidos = Convert.ToBoolean(datos["PuedeEnviarPedidos"]);




                }
                //cn.Close();
            }
            return empleados;
        }

        public static Empleado ObtenerEmpleado(string nombreEmpleado)
        {
            Empleado emp = null;
            using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection())
            {
                //cn.ConnectionString = "data source=(local); initial catalog=Beca2021 ;User ID=UsrBeca; Password=1234";
                cn.ConnectionString = Auxiliar.CadenaConexion;
                //  System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                // cmd.Connection = cn;
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT*FROM Empleados WHERE NombreEmpleado = @nombreEmpleado";
                //cmd.Parameters.AddWithValue("@mail", mail);
                //cmd.CommandType = System.Data.CommandType.Text;//lo es por defecto
                //cmd.CommandText = "SELECT * FROM Clientes WHERE Mail= '" + mail + "' ";
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandText = "ObtenerClientePorMail";
                cmd.Parameters.AddWithValue("@nombreEmpleado", nombreEmpleado);
                cn.Open();
                System.Data.SqlClient.SqlDataReader datos = cmd.ExecuteReader();


                while (datos.Read())
                {
                    emp = new Empleado();
                    emp.Codigo = Convert.ToInt32(datos["Codigo"]);
                    //cli.Codigo = Convert.ToInt32(datos[0]);
                    //cli.Codigo = datos.GetInt32(0);

                    emp.Nombre = datos["Nombre"].ToString();
                    emp.Password= datos["Password"].ToString();
                    emp.NombreEmpleado = datos["NombreEmpleado"].ToString();
                    emp.Apellidos = datos["Apellidos"].ToString();
                    if(datos["PuedePrepararPedidos"]!=DBNull.Value)
                    emp.PuedePrepararPedidos = Convert.ToBoolean(datos["PuedePrepararPedidos"]);
                    if (datos["PuedeEnviarPedidos"] != DBNull.Value)

                        emp.PuedeEnviarPedidos = Convert.ToBoolean(datos["PuedeEnviarPedidos"]);




                }
                //cn.Close();
            }
            return emp;
        }

    }
}
