using CRUD_PostgreSQL.Models;
using CRUD_SQLServer.Data;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowPS.StepDefinitions
{
    [Binding]
    public class EliminarStepDefinitions
    {
        private readonly ClienteDataAccessLayer _clienteDataAccessLayer = new ClienteDataAccessLayer();
        private Cliente _clienteParaEliminar;

        [Given(@"Seleccionar el cliente para eliminar de la BDD")]
        public void GivenSeleccionarElClienteParaEliminarDeLaBDD(Table table)
        {
            _clienteParaEliminar = table.CreateSet<Cliente>().First();
            _clienteParaEliminar = _clienteDataAccessLayer.GetClienteByCedula(_clienteParaEliminar.Cedula);
            _clienteParaEliminar.Should().NotBeNull("El cliente no existe en la base de datos.");
        }

        [When(@"Eliminamos el cliente de la BDD")]
        public void WhenEliminamosElClienteDeLaBDD()
        {
            _clienteDataAccessLayer.DeleteCliente(_clienteParaEliminar.Codigo);
        }

        [Then(@"Verificamos que el cliente fue eliminado de la BDD")]
        public void ThenVerificamosQueElClienteFueEliminadoDeLaBDD()
        {
            var clienteReal = _clienteDataAccessLayer.GetClienteByCedula(_clienteParaEliminar.Cedula);
            clienteReal.Should().BeNull("El cliente con la cédula {0} todavía existe en la base de datos", _clienteParaEliminar.Cedula);
        }
    }
}
