using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace wwwTest
{
    /// <summary>
    /// Descripción resumida de SignUpTest
    /// </summary>
    [TestClass]
    public class SignUpTest
    {

        private static IWebDriver driver;


        [TestMethod]
        public void signUp0001Test()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44371/SignUp.aspx");
          
            driver.FindElement(By.Id("tbxUsername")).SendKeys("userName");
            driver.FindElement(By.Id("tbxEmail")).SendKeys("test@test.com");
            driver.FindElement(By.Id("tbxFirstName")).SendKeys("test1");
            driver.FindElement(By.Id("tbxLastName")).SendKeys("testSurname");
            driver.FindElement(By.Id("tbxPassword")).SendKeys("TestCorrect123#");
            driver.FindElement(By.Id("btnSignUp")).Click();
            Assert.AreEqual("Inicio de sesion", driver.FindElement(By.Id("lblTitle")).Text);
            driver.Close();
        }

        [TestMethod]
        public void signup0002Test()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44371/SignUp.aspx");

            driver.FindElement(By.Id("tbxUsername")).SendKeys("userName");
            driver.FindElement(By.Id("tbxEmail")).SendKeys("test@test.com");
            driver.FindElement(By.Id("tbxFirstName")).SendKeys("test1");
            driver.FindElement(By.Id("btnSignUp")).Click();
            Assert.AreEqual("- El apellido es obligatorio", driver.FindElement(By.Id("lblError")).Text);
            Assert.AreEqual("- La password debe contener almenos 7 caracteres, mayusculas, numeros y simbolos", driver.FindElement(By.Id("lblErrorPassword")).Text);
            driver.Close();
        }

        [TestMethod]
        public void signUp000Test()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44371/SignUp.aspx");

            driver.FindElement(By.Id("tbxUsername")).SendKeys("userName");
            driver.FindElement(By.Id("tbxEmail")).SendKeys("user1@example.com");
            driver.FindElement(By.Id("tbxFirstName")).SendKeys("test1");
            driver.FindElement(By.Id("tbxLastName")).SendKeys("testSurname");
            driver.FindElement(By.Id("tbxPassword")).SendKeys("TestCorrect123#");
            driver.FindElement(By.Id("btnSignUp")).Click();
            Assert.AreEqual("- El email introducido ya esta registrado", driver.FindElement(By.Id("lblError")).Text);
            driver.Close();
        }
    }
}
