using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace wwwTest
{
    [TestClass]
    public class LoginTest
    {


        private static IWebDriver driver;

        [TestMethod]
        public void Login0001Test()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44371/Login.aspx");
            driver.FindElement(By.Id("tbxEmail")).Clear();
            driver.FindElement(By.Id("tbxEmail")).SendKeys("user1@example.com");
            driver.FindElement(By.Id("tbxPassword")).Clear();
            driver.FindElement(By.Id("tbxPassword")).SendKeys("User1245test@");
            driver.FindElement(By.Id("btnLogin")).Click();
            Assert.AreEqual("Pagina de Inicio", driver.FindElement(By.Id("lblTitle")).Text);
            driver.FindElement(By.Id("LinkButtonSignOut")).Click();
            driver.Close();
        }

        [TestMethod]
        public void login0002Test()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44371/Login.aspx");
            driver.FindElement(By.Id("tbxEmail")).Clear();
            driver.FindElement(By.Id("tbxEmail")).SendKeys("user1@example.com");
            driver.FindElement(By.Id("tbxPassword")).Clear();
            driver.FindElement(By.Id("tbxPassword")).SendKeys("badpassword");
            driver.FindElement(By.Id("btnLogin")).Click();
            Assert.AreEqual("Error: Email / Password erroneos", driver.FindElement(By.Id("lblErrorMsg")).Text);
            driver.Close();
        }

    }
}
