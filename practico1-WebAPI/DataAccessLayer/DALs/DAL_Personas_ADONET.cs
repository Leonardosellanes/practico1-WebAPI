using DataAccessLayer.IDALs;
using Microsoft.Data.SqlClient;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_Personas_ADONET : IDAL_Personas
    {
        private string _connectionString = "Server=localhost,1433;Database=Practico2;User Id=sa;Password=Abc*123!;Encrypt=False;";

        public List<Persona> Get()
        {
            // Instanciar la lista de personas a devolver
            List<Persona> personas = new List<Persona>();
            
            // Abrir la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Personas", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Persona persona = new Persona();

                            persona.Documento = reader["Documento"].ToString();
                            persona.Nombre = reader["Nombres"].ToString();

                            personas.Add(persona);
                        }
                    }
                }
            }

            return personas;
        }

        public Persona Get(string documento)
        {
            Persona persona;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Personas WHERE Documento = @documento", connection))
                {
                    command.Parameters.AddWithValue("documento", documento);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        persona = new Persona();

                        persona.Documento = reader["Documento"].ToString();
                        persona.Nombre = reader["Nombres"].ToString();
                    }
                }
            }

            return persona;
        }

        public void Insert(Persona persona)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Personas (Documento, Nombres) VALUES (@documento, @nombres)", connection))
                {
                    command.Parameters.AddWithValue("@documento", persona.Documento);
                    command.Parameters.AddWithValue("@nombres", persona.Nombre);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Persona persona)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Personas SET Nombres = @nombre WHERE Documento = @documento", connection))
                {
                    command.Parameters.AddWithValue("@documento", persona.Documento);
                    command.Parameters.AddWithValue("@nombre", persona.Nombre);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string documento)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Personas WHERE Documento = @documento", connection))
                {
                    command.Parameters.AddWithValue("@documento", documento);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
