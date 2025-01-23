using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD_PostgreSQL.Models; // Cambia el namespace si es necesario
using CRUD_SQLServer.Data;

namespace CRUD_PostgreSQL.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteDataAccessLayer _clienteDAL;

        // Constructor para inicializar el Data Access Layer
        public ClienteController()
        {
            _clienteDAL = new ClienteDataAccessLayer();
        }

        // GET: ClienteController
        public ActionResult Index()
        {
            // Obtener la lista de clientes desde la capa de acceso a datos
            List<Cliente> ListClient = _clienteDAL.GetAllClientes().ToList();

            // Ordenar la lista por el campo Codigo (ID) de forma ascendente
            ListClient = ListClient.OrderBy(c => c.Codigo).ToList();

            // Pasar la lista ordenada a la vista
            return View(ListClient);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            var cliente = _clienteDAL.GetClienteData(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente objCliente)
        {
            if (ModelState.IsValid)
            {
                _clienteDAL.AddCliente(objCliente);
                return RedirectToAction(nameof(Index));
            }
            return View(objCliente);
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            var cliente = _clienteDAL.GetClienteData(id);
            if (cliente == null)
            {
                return NotFound();
            }
            Console.WriteLine($"Saldo cargado: {cliente.Saldo}");
            return View(cliente);
        }
        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cliente objCliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objCliente.Codigo = id;
                    _clienteDAL.UpdateCliente(objCliente);
                    TempData["SuccessMessage"] = "Cliente actualizado con éxito.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al actualizar el cliente: {ex.Message}");
                    // Opcionalmente, registra la excepción
                    // _logger.LogError(ex, "Error al actualizar el cliente");
                }
            }

            // Si llegamos aquí, algo falló, volvemos a mostrar el formulario
            return View(objCliente);
        }


        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            var cliente = _clienteDAL.GetClienteData(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: ClienteController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _clienteDAL.DeleteCliente(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
