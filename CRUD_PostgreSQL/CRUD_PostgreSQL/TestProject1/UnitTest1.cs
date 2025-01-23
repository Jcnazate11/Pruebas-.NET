using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {

        //Establecer la conexión
        private readonly IWebDriver driver;

        int tiempoEspera = 3000;
        public UnitTest1()
        {
            //driver.Quit();
            //driver.Dispose();
            driver = new ChromeDriver();
        }

        
        [TestMethod]
        public void Create_Get_ReturnView()
        {
            // Configuración del navegador
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--force-device-scale-factor=0.75"); // Establece el zoom al 75%

            using (IWebDriver driver = new ChromeDriver(options))
            {
                driver.Manage().Window.Maximize();

                // Visitar página principal
                driver.Navigate().GoToUrl("http://localhost:5113/Cliente");
                Thread.Sleep(tiempoEspera);

                // Dar click al botón Crear Nuevo Cliente
                driver.FindElement(By.Id("btnCrearCli")).Click();
                Thread.Sleep(tiempoEspera);

                // Llenar campos del formulario de creación
                driver.FindElement(By.Id("cedula")).SendKeys("1752935946");
                driver.FindElement(By.Id("apellidos")).SendKeys("Cabezas");
                driver.FindElement(By.Id("nombres")).SendKeys("Johana");
                driver.FindElement(By.Id("FechaNacimiento")).SendKeys("14/05/2000");
                driver.FindElement(By.Id("mail")).SendKeys("johanaC@gmail.com");
                driver.FindElement(By.Id("telefono")).SendKeys("0995623482");
                driver.FindElement(By.Id("direccion")).SendKeys("Sangolqui");
                driver.FindElement(By.Id("estado")).Click();
                Thread.Sleep(tiempoEspera);

                // Guardar Formulario
                driver.FindElement(By.Id("btnGuardarC")).Click();

                // Aquí puedes agregar más aserciones para verificar los resultados del formulario

                driver.Quit();
            }
        }

        
        [TestMethod]
        public void Edit_Get_ReturnView()
        {
            // Configuración del navegador
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--force-device-scale-factor=0.75"); // Establece el zoom al 75%

            using (IWebDriver driver = new ChromeDriver(options))
            {
                
                driver.Navigate().GoToUrl("http://localhost:5113/Cliente");
                driver.FindElement(By.CssSelector("a.btn-warning")).Click();
                Thread.Sleep(tiempoEspera);

                // Edita el campo nombre
                var nombreField = driver.FindElement(By.Id("nombres"));
                nombreField.Clear();
                nombreField.SendKeys("Alberto");

                Thread.Sleep(tiempoEspera);

                // Edita el campo apellido
                var apellidoField = driver.FindElement(By.Id("apellidos"));
                apellidoField.Clear();
                apellidoField.SendKeys("Paredes");

                Thread.Sleep(tiempoEspera);

                // Guardar los cambios
                driver.FindElement(By.Id("btnGuardarE")).Click();
                Thread.Sleep(tiempoEspera);

            }
        }
        
        [TestMethod]
        public void Delete_Get_ReturnView()
        {
           // Configuración del navegador
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--force-device-scale-factor=0.75"); // Establece el zoom al 75%

            using (IWebDriver driver = new ChromeDriver(options))
            {
              
                driver.Navigate().GoToUrl("http://localhost:5113/Cliente");
                driver.FindElement(By.CssSelector("a.btn-danger")).Click();
                Thread.Sleep(tiempoEspera);

                driver.FindElement(By.Id("btnEliminarD")).Click();
            }
        }

        [TestMethod]
        public void Details_Get_ReturnView()
        {
             // Configuración del navegador
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--force-device-scale-factor=0.75"); // Establece el zoom al 75%

            using (IWebDriver driver = new ChromeDriver(options))
            {
                    driver.Navigate().GoToUrl("http://localhost:5113/Cliente");
                driver.FindElement(By.CssSelector("a.btn-info")).Click();
                Thread.Sleep(tiempoEspera);
            }
        }
        

    }
}