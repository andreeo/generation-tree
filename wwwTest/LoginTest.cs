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
        public void The001LoginErrorTest()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44371/Login.aspx");
            driver.FindElement(By.XPath("//form[@id='form1']/div[3]/table/tbody/tr[4]/td[3]/input")).Click();
            driver.FindElement(By.XPath("//form[@id='form1']/div[3]/table/tbody/tr[4]/td[3]/input")).Clear();
            driver.FindElement(By.XPath("//form[@id='form1']/div[3]/table/tbody/tr[4]/td[3]/input")).SendKeys("error");
            driver.FindElement(By.XPath("//form[@id='form1']/div[3]/table/tbody/tr[5]/td[3]/input")).Clear();
            driver.FindElement(By.XPath("//form[@id='form1']/div[3]/table/tbody/tr[5]/td[3]/input")).SendKeys("error");
            driver.FindElement(By.XPath("//form[@id='form1']/div[3]/table/tbody/tr[6]/td[3]/input")).Click();
            Assert.AreEqual("Error: Email / Password erroneos", driver.FindElement(By.XPath("//form[@id='form1']/div[3]/table/tbody/tr[7]/td[3]/span")).Text);
            driver.Close();
        }

        [TestMethod]
        public void The0002LoginErrorBadPasswordTest()
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

        [TestMethod]
        public void The003LoginOkTest()
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

    }
}
