using BPUNGUILS5.Models;

using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BPUNGUILS5.Utils
{
    public class PersonRepository
    {
        string _dbPath; //Ruta
        private SQLiteConnection conn;
        //Mensaje a mostrar
        public string StatusMessage { get; set; }

        private void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }
        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                Init();
                // Validar que se ingrese el nombre
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre requerido");

                Persona person = new() { Name = nombre };
                result = conn.Insert(person);
                StatusMessage = string.Format("{0} record(s) added (Nombre: {1})", result, nombre);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", nombre, ex.Message);
            }
        }

        public List<Persona> GetAllPersonas()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to Retrive data. {0}", ex.Message);
            }

            return new List<Persona>();
        }

        public void UpdatePerson(Persona persona)
        {
            try
            {
                Init();
                if (persona == null || persona.Id == 0)
                    throw new Exception("Invalid person data");

                int result = conn.Update(persona);
                StatusMessage = string.Format("{0} record(s) updated (ID: {1})", result, persona.Id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update person. Error: {0}", ex.Message);
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();
                if (id == 0)
                    throw new Exception("Invalid person ID");

                int result = conn.Delete<Persona>(id);
                StatusMessage = string.Format("{0} record(s) deleted (ID: {1})", result, id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete person. Error: {0}", ex.Message);
            }
        }
    }
}
    


