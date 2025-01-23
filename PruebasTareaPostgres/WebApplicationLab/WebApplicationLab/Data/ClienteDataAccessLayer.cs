using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using WebApplicationLab.Models;

namespace WebApplicationLab.NewFolder
{
    public class ClienteDataAccessLayer
    {
        string connectionString = "Host=localhost;Port=5434;Database=banco;Username=postgres;Password=rodito11";

        // Retrieve all clients
        public IEnumerable<Cliente> GetAllClientes()
        {
            List<Cliente> lst = new List<Cliente>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT Codigo, Cedula, Apellidos, Nombres, fechaNacimiento, Mail, Direccion, SaldoCuenta, Provincia, Estado FROM Cliente", con);
                con.Open();
                NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Codigo = Convert.ToInt32(rdr["Codigo"]),
                        Cedula = rdr["Cedula"].ToString(),
                        Apellidos = rdr["Apellidos"].ToString(),
                        Nombres = rdr["Nombres"].ToString(),
                        FechaNacimiento = rdr["fechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["fechaNacimiento"]),
                        Mail = rdr["Mail"].ToString(),
                        Direccion = rdr["Direccion"].ToString(),
                        SaldoCuenta = Convert.ToDecimal(rdr["SaldoCuenta"]),
                        Provincia = rdr["Provincia"].ToString(),
                        Estado = Convert.ToBoolean(rdr["Estado"])
                    };

                    lst.Add(cliente);
                }

                con.Close();
            }
            return lst;
        }

        // Add a new client
        public void AddCliente(Cliente cliente)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string query = @"INSERT INTO Cliente (Cedula, Apellidos, Nombres, fechaNacimiento, Mail, Direccion, SaldoCuenta, Provincia, Estado)
                         VALUES (@cedula, @apellidos, @nombres, @fechaNacimiento, @mail, @direccion, @saldoCuenta, @provincia, @estado)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                cmd.Parameters.AddWithValue("@cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@fechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@mail", cliente.Mail);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@saldoCuenta", cliente.SaldoCuenta);
                cmd.Parameters.AddWithValue("@provincia", cliente.Provincia);
                cmd.Parameters.AddWithValue("@estado", cliente.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            }
            catch (Exception ex)
            {
                // Loguea la excepción o manéjala de alguna manera
                throw new Exception("Error al agregar el cliente: " + ex.Message);
            }
        }

        // Update an existing client
        public void UpdateCliente(Cliente cliente)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string query = @"UPDATE Cliente SET Cedula = @cedula, Apellidos = @apellidos, Nombres = @nombres,
                         fechaNacimiento = @fechaNacimiento, Mail = @mail, Direccion = @direccion, SaldoCuenta = @saldoCuenta, Provincia = @provincia, Estado = @estado WHERE Codigo = @codigo";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                cmd.Parameters.AddWithValue("@codigo", cliente.Codigo);
                cmd.Parameters.AddWithValue("@cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@fechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@mail", cliente.Mail);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@saldoCuenta", cliente.SaldoCuenta);
                cmd.Parameters.AddWithValue("@provincia", cliente.Provincia);
                cmd.Parameters.AddWithValue("@estado", cliente.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Get a single client's data by ID
        public Cliente GetClienteData(int? codigo)
        {
            Cliente cliente = new Cliente();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT Codigo, Cedula, Apellidos, Nombres, fechaNacimiento, Mail, Direccion, SaldoCuenta, Provincia, Estado FROM Cliente WHERE Codigo = @codigo";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                con.Open();
                NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cliente.Codigo = Convert.ToInt32(rdr["Codigo"]);
                    cliente.Cedula = rdr["Cedula"].ToString();
                    cliente.Apellidos = rdr["Apellidos"].ToString();
                    cliente.Nombres = rdr["Nombres"].ToString();
                    cliente.FechaNacimiento = rdr["fechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["fechaNacimiento"]);
                    cliente.Mail = rdr["Mail"].ToString();
                    cliente.Direccion = rdr["Direccion"].ToString();
                    cliente.SaldoCuenta = Convert.ToDecimal(rdr["SaldoCuenta"]);
                    cliente.Provincia = rdr["Provincia"].ToString();
                    cliente.Estado = Convert.ToBoolean(rdr["Estado"]);
                }

                con.Close();
            }
            return cliente;
        }

        // Delete a client by ID
        public void DeleteCliente(int codigo)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string query = "DELETE FROM Cliente WHERE Codigo = @codigo";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public Cliente GetClienteByCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                return null;
            }

            Cliente cliente = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT Codigo, Cedula, Apellidos, Nombres, fechaNacimiento, Mail, Direccion, SaldoCuenta, Provincia, Estado FROM Cliente WHERE Cedula = @cedula";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.Add("@cedula", NpgsqlDbType.Varchar).Value = cedula;

                con.Open();
                NpgsqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cliente = new Cliente
                    {
                        Codigo = Convert.ToInt32(rdr["Codigo"]),
                        Cedula = rdr["Cedula"].ToString(),
                        Apellidos = rdr["Apellidos"].ToString(),
                        Nombres = rdr["Nombres"].ToString(),
                        FechaNacimiento = rdr["fechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["fechaNacimiento"]),
                        Mail = rdr["Mail"].ToString(),
                        Direccion = rdr["Direccion"].ToString(),
                        SaldoCuenta = Convert.ToDecimal(rdr["SaldoCuenta"]),
                        Provincia = rdr["Provincia"].ToString(),
                        Estado = Convert.ToBoolean(rdr["Estado"])
                    };
                }

                con.Close();
            }

            return cliente;
        }


    }
}
