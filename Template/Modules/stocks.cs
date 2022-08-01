using System;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Discord.Commands;
using System.Drawing;
using OpenQA.Selenium.Interactions;

namespace Template.Modules
{
    public class stocks : ModuleBase
    {
        [Command("stocks")]
        [Obsolete]
        public async Task stocksGen([Remainder] string text)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");//Comment if we want to see the window. 
            var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
            driver.Navigate().GoToUrl("https://finance.yahoo.com/");
            driver.FindElement(By.Id("yfin-usr-qry")).SendKeys(text);
            driver.FindElement(By.Id("header-desktop-search-button")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.Id("chrt-evts-mod"))); 
            driver.Manage().Window.Size = new Size(800, 600);
            var elem = driver.FindElement(By.XPath("//div[@id='Lead-1-FinanceHeader-Proxy']"));
            driver.ExecuteScript("arguments[0].scrollIntoView(true);", elem);
            var screenshot = (driver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile("screenshot.png");
            driver.Close();
            driver.Quit();
            await Context.Channel.SendMessageAsync(Context.Message.Author.Mention);
            await Context.Channel.SendFileAsync("screenshot.png");
        }
    }
}
