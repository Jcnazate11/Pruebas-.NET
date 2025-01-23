using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CRUD_PostgreSQL.Models;

namespace CRUD_SQLServer.Data
{
    public class ClienteDataAccessLayer
    {
        string connectionString = "Server=DESKTOP-0F5Q7NB\\MYSQLSERVER2022;Database=Prueba;User Id=sa;Password=rodito11;";

        // Método para obtener todos los clientes usando un procedimiento almacenado
        public IEnumerable<Cliente> GetAllClientes()
        {
            List<Cliente> lst = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllClientes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Codigo = Convert.ToInt32(rdr["Codigo"]),
                        Cedula = rdr["Cedula"].ToString(),
                        Apellidos = rdr["Apellidos"].ToString(),
                        Nombres = rdr["Nombres"].ToString(),
                        FechaNacimiento = rdr["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["FechaNacimiento"]),
                        Mail = rdr["Mail"].ToString(),
                        Telefono = rdr["Telefono"].ToString(),
                        Direccion = rdr["Direccion"].ToString(),
                        Provincia = rdr["Provincia"].ToString(),
                        Saldo = Convert.ToDecimal(rdr["Saldo"]),
                        Estado = Convert.ToBoolean(rdr["Estado"])
                    };

                    lst.Add(cliente);
                }

                con.Close();
            }
            return lst;
        }

        // Método para agregar un cliente usando un procedimiento almacenado
        public void AddCliente(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cedula", cliente.Cedula ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento.HasValue ? (object)cliente.FechaNacimiento.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Mail", cliente.Mail ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", cliente.Estado);
                cmd.Parameters.AddWithValue("@Saldo", cliente.Saldo);
                cmd.Parameters.AddWithValue("@Provincia", cliente.Provincia);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Método para actualizar un cliente usando una consulta directa
        public void UpdateCliente(Cliente cliente)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Verificación de la existencia del cliente por cédula
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM Cliente WHERE Cedula = @Cedula", con);
                    checkCmd.Parameters.AddWithValue("@Cedula", cliente.Cedula);

                    con.Open();
                    int clienteExiste = (int)checkCmd.ExecuteScalar();
                    if (clienteExiste == 0)
                    {
                        throw new Exception("No se encontró ningún cliente con esa cédula.");
                    }

                    // Si el cliente existe, se procede a la actualización
                    SqlCommand cmd = new SqlCommand("UpdateCliente", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros que requiere el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Codigo", cliente.Codigo);
                    cmd.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                    cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                    cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento.HasValue ? (object)cliente.FechaNacimiento.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Mail", cliente.Mail);
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Provincia", cliente.Provincia ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Estado", cliente.Estado);
                    cmd.Parameters.AddWithValue("@Saldo", cliente.Saldo);

                    // Ejecutar la actualización
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("No se actualizó ningún registro.");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejar excepciones específicas de SQL
                Console.WriteLine($"Error de SQL al actualizar cliente: {sqlEx.Message}");
                throw new Exception($"Error de base de datos: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                Console.WriteLine($"Error al actualizar cliente: {ex.Message}");
                throw new Exception($"Error al actualizar cliente: {ex.Message}", ex);
            }
        }



        // Método para eliminar un cliente usando un procedimiento almacenado
        public void DeleteCliente(int? codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", codigo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Método para obtener un cliente por su ID usando un procedimiento almacenado
        public Cliente GetClienteData(int? codigo)
        {
            Cliente cliente = new Cliente();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetClienteData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", codigo);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cliente.Codigo = Convert.ToInt32(rdr["Codigo"]);
                    cliente.Cedula = rdr["Cedula"].ToString();
                    cliente.Apellidos = rdr["Apellidos"].ToString();
                    cliente.Nombres = rdr["Nombres"].ToString();
                    cliente.FechaNacimiento = rdr["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["FechaNacimiento"]);
                    cliente.Mail = rdr["Mail"].ToString();
                    cliente.Telefono = rdr["Telefono"].ToString();
                    cliente.Direccion = rdr["Direccion"].ToString();
                    cliente.Estado = Convert.ToBoolean(rdr["Estado"]);
                    cliente.Saldo = Convert.ToDecimal(rdr["Saldo"]);
                    cliente.Provincia = rdr["Provincia"].ToString(); // Nuevo campo

                }

                con.Close();
            }
            return cliente;
        }
        // Método para obtener un cliente por su Cédula
        public Cliente GetClienteByCedula(string cedula)
        {
            Cliente cliente = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Llamada al procedimiento almacenado
                SqlCommand cmd = new SqlCommand("GetClienteByCedula", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cedula", cedula);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cliente = new Cliente
                    {
                        Codigo = Convert.ToInt32(rdr["Codigo"]),
                        Cedula = rdr["Cedula"].ToString(),
                        Apellidos = rdr["Apellidos"].ToString(),
                        Nombres = rdr["Nombres"].ToString(),
                        FechaNacimiento = rdr["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["FechaNacimiento"]),
                        Mail = rdr["Mail"].ToString(),
                        Telefono = rdr["Telefono"].ToString(),
                        Direccion = rdr["Direccion"].ToString(),
                        Provincia = rdr["Provincia"].ToString(),
                        Saldo = Convert.ToDecimal(rdr["Saldo"]),
                        Estado = Convert.ToBoolean(rdr["Estado"])
                    };
                }

                con.Close();
            }
            return cliente;
        }


    }
}
