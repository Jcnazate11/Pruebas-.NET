using Practica1Parcial3.Models;
using System.Data.SqlClient;


namespace Practica1Parcial3.Data
{
    public class ClientSqlDataAccessLayer
    {
        //Realizar la conexion hacia la BD, es decir el conecction string
        string connectionString = "Data Source = LAPTOP-B7T2HARV; Initial Catalog = dbProducts; user ID = crist; Password = 12345";

        public IEnumerable<ClienteSql> GetAllClientes()
        {
            List<ClienteSql> lst = new List<ClienteSql>();

            using (SqlConnection con = new SqlConnection(connectionString)) 
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Clientes", con);
                cmd.CommandType = System.Data.CommandType.Text;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClienteSql cliente = new ClienteSql();

                    cliente.Codigo = Convert.ToInt32(reader["Codigo"]);
                    cliente.Cedula = reader["Cedula"].ToString();
                    cliente.Nombre = reader["Nombre"].ToString();
                    cliente.Apellido = reader["Apellido"].ToString();
                    cliente.fechaNacimiento = reader["fechaNacimiento"] == DBNull.Value
                    ? (DateTime?)null
                    : Convert.ToDateTime(reader["fechaNacimiento"]);
                    cliente.Mail = reader["Mail"].ToString();
                    cliente.Telefono = reader["Telefono"].ToString();
                    cliente.Direccion = reader["Direccion"].ToString();
                    cliente.Estado = Convert.ToBoolean(reader["Estado"]);

                    lst.Add(cliente);


                }
                con.Close();
            }
        return lst;
        }

        public void AddCliente(ClienteSql cliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"INSERT INTO Clientes (Cedula, Nombre, Apellido, fechaNacimiento, Mail, Telefono, Direccion, Estado)
                             VALUES (@cedula, @nombre, @apellido, @fechaNacimiento, @mail, @telefono, @direccion, @estado)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@cedula", cliente.Cedula);
                    cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", cliente.fechaNacimiento.HasValue ? (object)cliente.fechaNacimiento.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@mail", cliente.Mail);
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                    cmd.Parameters.AddWithValue("@estado", cliente.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Aquí puedes agregar manejo de errores, como registro de logs o una notificación al usuario
                    throw new Exception("Ocurrió un error al intentar insertar el cliente: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void UpdateCliente(ClienteSql cliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Clientes SET Cedula = @cedula, Nombre = @nombre, Apellido = @apellido, 
                         fechaNacimiento = @fechaNacimiento, Mail = @mail, Telefono = @telefono, 
                         Direccion = @direccion, Estado = @estado WHERE Codigo = @codigo";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@codigo", cliente.Codigo);
                cmd.Parameters.AddWithValue("@cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@fechaNacimiento", cliente.fechaNacimiento);
                cmd.Parameters.AddWithValue("@mail", cliente.Mail);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@estado", cliente.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteCliente(int? codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Clientes WHERE Codigo = @codigo";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public ClienteSql GetClienteData(int? codigo)
        {
            ClienteSql cliente = new ClienteSql();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Clientes WHERE Codigo = @codigo";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cliente.Codigo = Convert.ToInt32(rdr["Codigo"]);
                    cliente.Cedula = rdr["Cedula"].ToString();
                    cliente.Nombre = rdr["Nombre"].ToString();
                    cliente.Apellido = rdr["Apellido"].ToString();
                    cliente.fechaNacimiento = rdr["fechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["fechaNacimiento"]);
                    cliente.Mail = rdr["Mail"].ToString();
                    cliente.Telefono = rdr["Telefono"].ToString();
                    cliente.Direccion = rdr["Direccion"].ToString();
                    cliente.Estado = Convert.ToBoolean(rdr["Estado"]);
                }

                con.Close();
            }
            return cliente;
        }

    }


}
