using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace MsTestUnit
{
    [TestClass]
    public class UnitTest1
    {
        By googleSearchBar = By.Id("APjFqb");
        By googleButtonClick = By.Name("btnK");
        By resultGoogleSearch = By.Id("_HVy7ZraBMYeNwbkPlObIQQ_77");

        int tiempoEspera = 3000;


        private readonly IWebDriver driver;

        public UnitTest1()
        {
            driver = new ChromeDriver();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }


        
        // Prueba para crear un cliente
        [TestMethod]
        public void Create_Get_ReturnCreateView()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            Thread.Sleep(tiempoEspera);

            // Navegar a la página de creación de clientes
            driver.Navigate().GoToUrl("https://localhost:7068/Cliente");

            Thread.Sleep(tiempoEspera);

            // Hacer clic en el botón de crear un nuevo cliente
            driver.FindElement(By.Name("crearNuevoCliente")).Click();

            Thread.Sleep(tiempoEspera);

            // Ingresar la cédula
            driver.FindElement(By.Name("cedulaCliente")).SendKeys("1753050051");

            Thread.Sleep(tiempoEspera);

            // Ingresar el nombre
            driver.FindElement(By.Name("nombresCliente")).SendKeys("Sebastian");

            Thread.Sleep(tiempoEspera);

            // Ingresar el apellido
            driver.FindElement(By.Name("apellidosCliente")).SendKeys("Cortes");

            Thread.Sleep(tiempoEspera);

            // Ingresar la fecha de nacimiento
            driver.FindElement(By.Name("fechaNacimientoCliente")).SendKeys("24-04-2003");

            Thread.Sleep(tiempoEspera);

            // Ingresar el correo
            driver.FindElement(By.Name("mailCliente")).SendKeys("sebas@espe.edu.ec");

            Thread.Sleep(tiempoEspera);

            // Ingresar la dirección
            driver.FindElement(By.Name("direccionCliente")).SendKeys("Av. Abdon Calderon");

            Thread.Sleep(tiempoEspera);

            // Seleccionar la provincia (ejemplo: Pichincha con código 17)
            var provinciaDropdown = driver.FindElement(By.Name("provinciaCliente"));
            var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(provinciaDropdown);
            selectElement.SelectByValue("17");  // Pichincha

            Thread.Sleep(tiempoEspera);

            // Ingresar el saldo de la cuenta
            driver.FindElement(By.Name("saldoCuentaCliente")).SendKeys("100.50");

            Thread.Sleep(tiempoEspera);

            // Seleccionar el checkbox de estado si no está seleccionado
            var estadoCheckbox = driver.FindElement(By.Id("estadoCliente"));
            if (!estadoCheckbox.Selected)
            {
                estadoCheckbox.Click();
            }

            Thread.Sleep(tiempoEspera);

            // Hacer clic en el botón "Guardar"
            driver.FindElement(By.Name("GuardarClienteNuevo")).Click();
        }


      
    }
}
