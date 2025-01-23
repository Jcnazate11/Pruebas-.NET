using Microsoft.AspNetCore.Mvc;
using Postgres.ASP.Net.Core.Data;
using Postgres.ASP.Net.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Postgres.ASP.Net.Core.Controllers
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
            List<Cliente> ListClient = new List<Cliente>();
            ListClient = _clienteDAL.GetAllClientes().ToList();
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
            List<Cliente> clientes = new List<Cliente>();
            clientes = _clienteDAL.GetAllClientes().ToList();
            return View(clientes);
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
            return View(cliente);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cliente objCliente)
        {
            if (ModelState.IsValid)
            {
                _clienteDAL.UpdateCliente(objCliente);
                return RedirectToAction(nameof(Index));
            }
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
