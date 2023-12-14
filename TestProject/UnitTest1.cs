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
        _driver.Navigate().GoToUrl("http://localhost:5253/");
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

    [TestMethod]
    public void Index_Loads_Correctly()
    {
        var indexElement =  wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("center-content")));
        Assert.IsNotNull(indexElement);
    }

    [TestMethod]
    public void Search_Clicks_Correctly()
    {
        var searchElement =  wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='spotify-search']")));
        Assert.IsNotNull(searchElement);
    }

    [TestMethod]
    public void Search_Button_Clicks_Correctly()
    {
        var searchElement =  wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='spotify-search']")));
        searchElement.Click();
        var searchButElement =  wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Search']")));
        searchButElement.Click();
        Assert.IsNotNull(searchButElement);
    }

    [TestMethod]
    public void SearchUser_Clicks_Correctly()
    {
        var searchUserElement =  wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='user']")));
        Assert.IsNotNull(searchUserElement);
    }

    [TestMethod]
    public void SearchUser_Button_Clicks_Correctly()
    {
        var searchUserElement =  wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='user']")));
        searchUserElement.Click();
        var searchUserButElement =  wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Search']")));
        searchUserButElement.Click();
        Assert.IsNotNull(searchUserButElement);
    }
}
