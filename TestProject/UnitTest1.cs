namespace TestProject;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

[TestClass]
public class SidebarTests
{
    private IWebDriver _driver;
    private WebDriverWait wait;


    [TestInitialize]
    public void TestInitialize()
    {
        _driver = new ChromeDriver();
        wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        _driver.Navigate().GoToUrl("https://daviddunne165.github.io/EAD-CA3-MatchScore/");
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _driver.Quit();
    }

    [TestMethod]
    public void Sidebar_Loads_Correctly()
    {
        var sidebarElement =  wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("sidebar")));
        Assert.IsNotNull(sidebarElement);
    }
}