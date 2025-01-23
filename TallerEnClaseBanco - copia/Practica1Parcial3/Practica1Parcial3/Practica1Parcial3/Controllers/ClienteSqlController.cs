using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica1Parcial3.Data;
using Practica1Parcial3.Models;

namespace Practica1Parcial3.Controllers
{
    public class ClienteSqlController : Controller
    {
        ClientSqlDataAccessLayer objClienteDAL = new ClientSqlDataAccessLayer();
        // GET: ClienteSqlController
        // GET: ClienteController
        public ActionResult Index()
        {
            List < ClienteSql> ListClient = new List<ClienteSql>();
            ListClient = objClienteDAL.GetAllClientes().ToList();
            return View(ListClient);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            var cliente = objClienteDAL.GetClienteData(id);
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
        public ActionResult Create(ClienteSql objCliente)
        {
            if (ModelState.IsValid)
            {
                objClienteDAL.AddCliente(objCliente);
                return RedirectToAction(nameof(Index));
            }
            return View(objCliente);
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            var cliente = objClienteDAL.GetClienteData(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClienteSql objCliente)
        {
            if (ModelState.IsValid)
            {
                objClienteDAL.UpdateCliente(objCliente);
                return RedirectToAction(nameof(Index));
            }
            return View(objCliente);
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            var cliente = objClienteDAL.GetClienteData(id);
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
            objClienteDAL.DeleteCliente(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
