using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium使用
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IWebDriver driver = new OpenQA.Selenium.IE.InternetExplorerDriver())
            {
                driver.Navigate().GoToUrl("https://technet-info.com/Main.aspx");

                var source = driver.PageSource;

                Console.WriteLine(source);


                //by id
                var byID = driver.FindElement(By.Id("cards"));

                //by class name
                var byClassName = driver.FindElements(By.ClassName("menu"));

                //by tag name 
                var byTagName = driver.FindElement(By.TagName("iframe"));

                // by name
                var byName = driver.FindElement(By.Name("__VIEWSTATE"));

                //by linked text  
                //<a href="http://www.google.com">linkedtext</a>>  
                //var byLinkText = driver.FindElement(By.LinkText("linkedtext"));

                //by partial link text
                //<a href="http://www.google.com">linkedtext</a>>
                //var byPartialLinkText = driver.FindElement(By.PartialLinkText("text"));

                //by css
                var byCss = driver.FindElement(By.CssSelector("#header .content .logo"));

                //by xpath
                var byXPath = driver.FindElements(By.XPath("//div"));

                //execute javascript
                //var jsReturnValue = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("jsfunname");

                //get element value and attribute value
                var byIDText = byID.Text;
                var byIDAttributeText = byID.GetAttribute("id");

                //click
                driver.FindElement(By.Id("copyright")).Click();

                //Navigation
                driver.Navigate().Forward();
                driver.Navigate().Back();

                //Drag And Drop
                var element = driver.FindElement(By.Name("source"));
                IWebElement target = driver.FindElement(By.Name("target"));
                (new Actions(driver)).DragAndDrop(element, target).Perform();

            }
        }
    }
}
