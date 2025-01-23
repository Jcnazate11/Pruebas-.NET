namespace CRUD_PostgreSQL.Models
{
    public class Calculadora
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }

        public int Add() { 
            return FirstNumber + SecondNumber;
        }

    }
}
