using CRUD_PostgreSQL.Models;
using CRUD_SQLServer.Data;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowPS.StepDefinitions
{
    [Binding]
    public class IngresoStepDefinitions

    {
        private readonly ClienteDataAccessLayer _clienteDAL = new ClienteDataAccessLayer();

        [Given(@"llenar los campos de la BDD")]
        public void GivenLlenarLosCamposDeLaBDD(Table table)
        {
            var dataTable = table.Rows.Count();
            dataTable.Should().BeGreaterThanOrEqualTo(1);
        }

        [When(@"El registro se ingresa en la BDD")]
        public void WhenElRegistroSeIngresaEnLaBDD(Table table)
        {
            var cliente = table.CreateSet<Cliente>().ToList();
            Cliente cli= new Cliente();
            foreach (var item in cliente)
            {
                cli.Cedula = item.Cedula;
                cli.Apellidos = item.Apellidos;
                cli.Nombres = item.Nombres;
                cli.FechaNacimiento= item.FechaNacimiento;
                cli.Mail = item.Mail;
                cli.Provincia = item.Provincia;
                cli.Telefono = item.Telefono;
                cli.Direccion = item.Direccion;
                cli.Estado= item.Estado;
            }
            _clienteDAL.AddCliente(cli);
        }
        [Then(@"El resultado se me ingresa en la BDD")]
        public void ThenElResultadoSeMeIngresaEnLaBDD(Table table)
        {
            // Verificar que hay al menos un registro en la tabla proporcionada
            int resultado = table.Rows.Count();
            resultado.Should().BeGreaterThanOrEqualTo(1);

            // Validar que cada registro en la tabla haya sido ingresado en la base de datos
            var clientesEsperados = table.CreateSet<Cliente>().ToList();
            foreach (var clienteEsperado in clientesEsperados)
            {
                var clienteEnBD = _clienteDAL.GetClienteByCedula(clienteEsperado.Cedula);

                // Verificar que el cliente con la cédula dada existe en la base de datos
                clienteEnBD.Should().NotBeNull($"El cliente con la cédula {clienteEsperado.Cedula} no se encontró en la base de datos.");

                // Verificar que los demás datos coinciden
                clienteEnBD.Apellidos.Should().Be(clienteEsperado.Apellidos);
                clienteEnBD.Nombres.Should().Be(clienteEsperado.Nombres);
                clienteEnBD.FechaNacimiento.Should().Be(clienteEsperado.FechaNacimiento);
                clienteEnBD.Mail.Should().Be(clienteEsperado.Mail);
                clienteEnBD.Provincia.Should().Be(clienteEsperado.Provincia);
                clienteEnBD.Telefono.Should().Be(clienteEsperado.Telefono);
                clienteEnBD.Direccion.Should().Be(clienteEsperado.Direccion);
                clienteEnBD.Estado.Should().Be(clienteEsperado.Estado);
            }
        }

    }
}
