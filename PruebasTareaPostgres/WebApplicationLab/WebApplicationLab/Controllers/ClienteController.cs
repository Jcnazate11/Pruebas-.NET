using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLab.NewFolder;
using WebApplicationLab.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplicationLab.Controllers
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
            // Puedes pasar una lista de provincias si lo requieres en la vista
            ViewBag.Provincias = GetProvincias();
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente objCliente)
        {
            // Generar la cédula si no está asignada
            if (string.IsNullOrWhiteSpace(objCliente.CedulaGenerada))
            {
                objCliente.CedulaGenerada = GenerarCedula(objCliente.Provincia);
                objCliente.Cedula = objCliente.CedulaGenerada; // Asignar la cédula generada al campo Cedula
            }

            // Validar si la cédula ya está registrada
            var existingCliente = _clienteDAL.GetClienteByCedula(objCliente.Cedula);
            if (existingCliente != null)
            {
                ModelState.AddModelError("Cedula", "La cédula ya está registrada.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clienteDAL.AddCliente(objCliente);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar: " + ex.Message);
                }
            }

            ViewBag.Provincias = GetProvincias();
            return View(objCliente);
        }


        // Método auxiliar para generar cédula
        private string GenerarCedula(string provincia)
        {
            Random random = new Random();
            string provinciaCodigo = provincia;

            string cedula = provinciaCodigo + random.Next(1000000, 9999999).ToString(); // Generar los 7 dígitos siguientes

            // Calcular el dígito verificador
            int digitoVerificador = CalcularDigitoVerificador(cedula);
            cedula += digitoVerificador;

            return cedula;
        }

        private int CalcularDigitoVerificador(string cedula)
        {
            // Implementación del algoritmo Módulo 10
            int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int suma = 0;

            for (int i = 0; i < coeficientes.Length; i++)
            {
                int valor = (cedula[i] - '0') * coeficientes[i];
                suma += valor >= 10 ? valor - 9 : valor;
            }

            int digitoVerificador = 10 - (suma % 10);
            return digitoVerificador == 10 ? 0 : digitoVerificador;
        }


        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            var cliente = _clienteDAL.GetClienteData(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewBag.Provincias = GetProvincias(); // Pasar la lista de provincias a la vista
            return View(cliente);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cliente objCliente)
        {
            if (ModelState.IsValid)
            {
                objCliente.Codigo = id; // Asegúrate de que el ID sea el correcto
                _clienteDAL.UpdateCliente(objCliente);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Provincias = GetProvincias(); // Asegurarse de volver a cargar las provincias en caso de error
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

        // Método auxiliar para obtener las provincias
        private List<SelectListItem> GetProvincias()
        {
            return new List<SelectListItem>
{
    new SelectListItem { Value = "01", Text = "Azuay" },
    new SelectListItem { Value = "02", Text = "Bolívar" },
    new SelectListItem { Value = "03", Text = "Cañar" },
    new SelectListItem { Value = "04", Text = "Carchi" },
    new SelectListItem { Value = "05", Text = "Chimborazo" },
    new SelectListItem { Value = "06", Text = "Cotopaxi" },
    new SelectListItem { Value = "07", Text = "El Oro" },
    new SelectListItem { Value = "08", Text = "Esmeraldas" },
    new SelectListItem { Value = "09", Text = "Galápagos" },
    new SelectListItem { Value = "10", Text = "Guayas" },
    new SelectListItem { Value = "11", Text = "Imbabura" },
    new SelectListItem { Value = "12", Text = "Loja" },
    new SelectListItem { Value = "13", Text = "Los Ríos" },
    new SelectListItem { Value = "14", Text = "Manabí" },
    new SelectListItem { Value = "15", Text = "Morona Santiago" },
    new SelectListItem { Value = "16", Text = "Napo" },
    new SelectListItem { Value = "17", Text = "Pastaza" },
    new SelectListItem { Value = "18", Text = "Pichincha" },
    new SelectListItem { Value = "19", Text = "Tungurahua" },
    new SelectListItem { Value = "20", Text = "Zamora Chinchipe" },
    new SelectListItem { Value = "21", Text = "Sucumbíos" },
    new SelectListItem { Value = "22", Text = "Orellana" },
    new SelectListItem { Value = "23", Text = "Santo Domingo de los Tsáchilas" },
    new SelectListItem { Value = "24", Text = "Santa Elena" }
};

        }
    }
}
