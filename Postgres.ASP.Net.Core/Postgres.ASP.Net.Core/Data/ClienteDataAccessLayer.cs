using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Npgsql;
using Postgres.ASP.Net.Core.Models;

namespace Postgres.ASP.Net.Core.Data
{
    public class ClienteDataAccessLayer
    {
        string connectionString = "Host=localhost;Port=5434;Database=dbProducts;Username=postgres;Password=rodito11";

        public IEnumerable<Cliente> GetAllClientes()
        {
            List<Cliente> lst = new List<Cliente>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Cliente", con);
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
                        fechaNacimiento = rdr["fechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["fechaNacimiento"]),
                        Mail = rdr["Mail"].ToString(),
                        Telefono = rdr["Telefono"].ToString(),
                        Direccion = rdr["Direccion"].ToString(),
                        Estado = Convert.ToBoolean(rdr["Estado"])
                    };

                    lst.Add(cliente);
                }

                con.Close();
            }
            return lst;
        }

        public void AddCliente(Cliente cliente)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string query = @"INSERT INTO Cliente (Cedula, Apellidos, Nombres, fechaNacimiento, Mail, Telefono, Direccion, Estado)
                         VALUES (@cedula, @apellidos, @nombres, @fechaNacimiento, @mail, @telefono, @direccion, @estado)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                cmd.Parameters.AddWithValue("@cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
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


        public void UpdateCliente(Cliente cliente)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string query = @"UPDATE Cliente SET Cedula = @cedula, Apellidos = @apellidos, Nombres = @nombres,
                         fechaNacimiento = @fechaNacimiento, Mail = @mail, Telefono = @telefono, 
                         Direccion = @direccion, Estado = @estado WHERE Codigo = @codigo";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                cmd.Parameters.AddWithValue("@codigo", cliente.Codigo);
                cmd.Parameters.AddWithValue("@cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
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

        public Cliente GetClienteData(int? codigo)
        {
            Cliente cliente = new Cliente();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cliente WHERE Codigo = @codigo";
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
