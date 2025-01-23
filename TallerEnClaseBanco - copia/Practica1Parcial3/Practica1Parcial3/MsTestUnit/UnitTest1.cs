using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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

        //[TestMethod]
        //public void TestPageGoogle()
        //{
        //    IWebDriver driver = new ChromeDriver();
        //    driver.Manage().Window.Maximize();

        //    driver.Navigate().GoToUrl("https://www.google.com/");

        //    Thread.Sleep(tiempoEspera);

        //    driver.FindElement(googleSearchBar).SendKeys("Visual Studio Code");

        //    Thread.Sleep(tiempoEspera);

        //    driver.FindElement(googleButtonClick).Click();

        //    Thread.Sleep(tiempoEspera);

        //    var resultadoBusqueda = driver.FindElement(resultGoogleSearch);

        //    Assert.IsNotNull(resultadoBusqueda);

        //    driver.Quit();
        //}

        [TestMethod]
        public void Create_Get_ReturnCreateView()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            Thread.Sleep(tiempoEspera);

            driver.Navigate().GoToUrl("https://localhost:7180/ClienteSql/Create");

            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Name("cedula")).SendKeys("0201369238");

            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Name("nombre")).SendKeys("Sebastian");

            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Name("apellido")).SendKeys("Cortes");

            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Name("fechaNacimiento")).SendKeys("24-04-2003");

            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Name("mail")).SendKeys("sebas@espe.edu.ec");

            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Name("telefono")).SendKeys("0987654321");

            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Name("direccion")).SendKeys("Av. Abdon Calderon");

            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Id("estado")).Click();

            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Name("guardarCliente")).Click();
        }

        [TestMethod]
        public void Edit_Get_ReturnEditView()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            Thread.Sleep(tiempoEspera);

            // Navega a la página de edición de un cliente específico (por ejemplo, con id=1)
            driver.Navigate().GoToUrl("https://localhost:7180/ClienteSql/Edit/1");

            Thread.Sleep(tiempoEspera);

            // Edita el campo cedula
            var cedulaField = driver.FindElement(By.Id("cedula"));
            cedulaField.Clear();
            cedulaField.SendKeys("0201369239");

            Thread.Sleep(tiempoEspera);

            // Edita el campo nombre
            var nombreField = driver.FindElement(By.Id("nombre"));
            nombreField.Clear();
            nombreField.SendKeys("Alberto");

            Thread.Sleep(tiempoEspera);

            // Edita el campo apellido
            var apellidoField = driver.FindElement(By.Id("apellido"));
            apellidoField.Clear();
            apellidoField.SendKeys("Paredes");

            Thread.Sleep(tiempoEspera);

            // Edita el campo fechaNacimiento
            var fechaNacimientoField = driver.FindElement(By.Name("fechaNacimiento"));
            fechaNacimientoField.Clear();
            fechaNacimientoField.SendKeys("01/01/1990 0:00:00");

            Thread.Sleep(tiempoEspera);

            // Edita el campo mail
            var mailField = driver.FindElement(By.Id("mail"));
            mailField.Clear();
            mailField.SendKeys("alberto@espe.edu.ec");

            Thread.Sleep(tiempoEspera);

            // Edita el campo telefono
            var telefonoField = driver.FindElement(By.Id("telefono"));
            telefonoField.Clear();
            telefonoField.SendKeys("0999999999");

            Thread.Sleep(tiempoEspera);

            // Edita el campo direccion
            var direccionField = driver.FindElement(By.Id("direccion"));
            direccionField.Clear();
            direccionField.SendKeys("Av. Wall Street");

            Thread.Sleep(tiempoEspera);

            // Edita el estado (Ejemplo: Si es un checkbox)
            var estadoField = driver.FindElement(By.Id("estado"));
            if (!estadoField.Selected)
            {
                estadoField.Click(); // Cambia el estado si no está seleccionado
            }

            Thread.Sleep(tiempoEspera);

            // Guardar los cambios
            driver.FindElement(By.Name("editarCliente")).Click();

        }

        [TestMethod]
        public void Delete_Get_ReturnDeleteView()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            Thread.Sleep(tiempoEspera);

            // Navega a la página de edición de un cliente específico (por ejemplo, con id=1)
            driver.Navigate().GoToUrl("https://localhost:7180/ClienteSql/Delete/1");

            Thread.Sleep(tiempoEspera);

            driver.FindElement(By.Name("eliminarCliente")).Click();
        }
    }
}