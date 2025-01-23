using CRUD_PostgreSQL.Models;
using CRUD_SQLServer.Data;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowPS.StepDefinitions
{
    [Binding]
    public class ActualizacionStepDefinitions
    {
        private readonly ClienteDataAccessLayer _clienteDataAccessLayer = new ClienteDataAccessLayer();
        private Cliente _clienteOriginal;

        // Paso para verificar si el cliente existe en la base de datos
        [Given(@"el cliente existe en la base de datos")]
        public void GivenElClienteExisteEnLaBaseDeDatos(Table table)
        {
            // Obtener los datos originales del cliente
            _clienteOriginal = table.CreateInstance<Cliente>();
            _clienteOriginal = _clienteDataAccessLayer.GetClienteByCedula(_clienteOriginal.Cedula);

            // Verificar que el cliente exista en la base de datos
            _clienteOriginal.Should().NotBeNull("El cliente original no existe en la base de datos.");
        }

        // Paso para actualizar los datos del cliente
        [When(@"actualizo los datos del cliente")]
        public void WhenActualizoLosDatosDelCliente(Table table)
        {
            // Obtener los datos editados del cliente
            var clienteEditado = table.CreateInstance<Cliente>();

            // Mantener el código original del cliente
            clienteEditado.Codigo = _clienteOriginal.Codigo;

            // Actualizar los datos del cliente en la base de datos
            _clienteDataAccessLayer.UpdateCliente(clienteEditado);
        }

        // Paso para verificar que los datos del cliente se han actualizado correctamente
        [Then(@"el cliente debe estar actualizado en la base de datos")]
        public void ThenElClienteDebeEstarActualizadoEnLaBaseDeDatos(Table table)
        {
            // Obtener los datos esperados del cliente
            var clienteEsperado = table.CreateInstance<Cliente>();

            // Obtener los datos del cliente en la base de datos
            var clienteReal = _clienteDataAccessLayer.GetClienteByCedula(clienteEsperado.Cedula);

            // Verificar que el cliente exista
            clienteReal.Should().NotBeNull($"El cliente con la cédula {clienteEsperado.Cedula} no fue encontrado en la base de datos");

            // Comparar todos los campos del cliente
            clienteReal.Cedula.Should().Be(clienteEsperado.Cedula);
            clienteReal.Apellidos.Should().Be(clienteEsperado.Apellidos);
            clienteReal.Nombres.Should().Be(clienteEsperado.Nombres);
            clienteReal.FechaNacimiento.Should().Be(clienteEsperado.FechaNacimiento); // Asegúrate de que "FechaNacimiento" sea correcto
            clienteReal.Mail.Should().Be(clienteEsperado.Mail);
            clienteReal.Telefono.Should().Be(clienteEsperado.Telefono);
            clienteReal.Direccion.Should().Be(clienteEsperado.Direccion);
            clienteReal.Estado.Should().Be(clienteEsperado.Estado);
            clienteReal.Saldo.Should().Be(clienteEsperado.Saldo);
            clienteReal.Provincia.Should().Be(clienteEsperado.Provincia);
        }
    }
}
