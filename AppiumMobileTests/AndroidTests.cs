using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android; // install Appuim - NuGet Packages
using System;


namespace AndroidTests // to be added additional name
{
    public class AndroidTests
    {
        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";//to be check during the exam
        private const string ContactBookUrl = "https://contactbook.nakov.repl.co/api"; // to be changed during the exam
        private const string applocation =
            @"D:\SoftUni\QA Automation\QA Automation-HomeWorks\ContactBook\contactbook-androidclient (1).apk";// to be change during the exam

        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private object appLlocation;

        // start command prompt and put "adb devices" to see the name of device, which shall be added to Appium Inpector. 
        // start Appium server
        // start Android studio or connect Android device to the laptop
        // start Appium Inspector - here you shall put "platformName", appium: app - the path to "apk" file, and deviceName.


        [SetUp]
        public void StartApp()
        {
            options = new AppiumOptions()
            { PlatformName = "Android" };
            options.AddAdditionalCapability("app", appLlocation);

            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]

        public void CloseApp()
        {
            driver.Quit();
        }


        [Test]
        public void Test_SearchContact_VerifyFirstResult()
        {
            // Arrange
            var urlField = driver.FindElement(By.Id("contactbook.androidclient:id/editTextApiUrl"));
            urlField.Clear();
            urlField.SendKeys(ContactBookUrl);

            var buttonConnect = driver.FindElement(By.Id("contactbook.androidclient:id/buttonConnect"));
            buttonConnect.Click();

            var editTextField = driver.FindElement(By.Id("contactbook.androidclient:id/editTextKeyword"));
            editTextField.SendKeys("steve");

            //Act
            var buttonSearch = driver.FindElement(By.Id("contactbook.androidclient:id/buttonSearch"));
            buttonSearch.Click();

            // Assert
            var firstName = driver.FindElement(By.Id("contactbook.androidclient:id/textViewFirstName"));
            var lastName = driver.FindElement(By.Id("contactbook.androidclient:id/textViewLastName"));

            Assert.That(firstName.Text, Is.EqualTo("Steve"));
            Assert.That(lastName.Text, Is.EqualTo("Jobs"));
        }
        [Test]
        public void Test_SearchContact_VerifySeveralResults()
        {
            // Arrange
            var urlField = driver.FindElement(By.Id("contactbook.androidclient:id/editTextApiUrl"));
            urlField.Clear();
            urlField.SendKeys(ContactBookUrl);

            var buttonConnect = driver.FindElement(By.Id("contactbook.androidclient:id/buttonConnect"));
            buttonConnect.Click();

            var editTextField = driver.FindElement(By.Id("contactbook.androidclient:id/editTextKeyword"));
            editTextField.SendKeys("american");

            //Act
            var buttonSearch = driver.FindElement(By.Id("contactbook.androidclient:id/buttonSearch"));
            buttonSearch.Click();

            // Assert
            var results = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            Assert.That(results.Text, Is.EqualTo("Contacts found: 2"));
            //var firstName = driver.FindElement(By.Id("contactbook.androidclient:id/textViewFirstName"));
            //var lastName = driver.FindElement(By.Id("contactbook.androidclient:id/textViewLastName"));

            //Assert.That(firstName.Text, Is.EqualTo("Steve"));
            //Assert.That(lastName.Text, Is.EqualTo("Jobs"));
        }
     }
 }