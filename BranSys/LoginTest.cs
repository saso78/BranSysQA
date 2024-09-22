using BranSys.BaseTest;
using BranSys.Utilities;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace BranSys
{
    [TestCaseOrderer("TestOrderer", "BranSys")]
    public class LoginTest : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        public LoginTest(TestFixture fixture)
        {
            _fixture = fixture;
            _fixture.Logger.LogInformation("Test started.");
        }

        [Fact]
        [TestPriority(1)]
        public void TestPresenceOfLoginFields()
        {
            try
            {
                // Check for the presence of username and password fields
                Assert.True(_fixture.IsElementPresent(By.Id("input-204")), "Username field is not present.");
                Assert.True(_fixture.IsElementPresent(By.Id("input-207")), "Password field is not present.");

                _fixture.Logger.LogInformation("Presence of login fields test passed.");
            }
            catch (Exception ex)
            {
                _fixture.Logger.LogError($"Test failed: {ex.Message}");
                throw;
            }
        }

        [Fact]
        [TestPriority(2)]
        public void TestInvalidCredentialsAndErrorMessage()
        {
            try
            {
                // Explicit wait for the fields to be visible
                _fixture.wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-204")));
                _fixture.wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-207")));

                // Input invalid credentials
                string invalidUsername = "wronguser";
                string invalidPassword = "wrongpassword";
                _fixture.Driver.FindElement(By.Id("input-204")).SendKeys(invalidUsername);
                _fixture.Driver.FindElement(By.Id("input-207")).SendKeys(invalidPassword);

                // Check input values
                var usernameFieldValue = _fixture.Driver.FindElement(By.Id("input-204")).GetAttribute("value");
                var passwordFieldValue = _fixture.Driver.FindElement(By.Id("input-207")).GetAttribute("value");

                Assert.Equal(invalidUsername, usernameFieldValue);
                Assert.Equal(invalidPassword, passwordFieldValue);

                // Press Enter to submit the form
                var passwordField = _fixture.Driver.FindElement(By.Id("input-207"));
                passwordField.SendKeys(Keys.Enter);

                // Wait for the error message to appear
                _fixture.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div/div/div/div/div/div/div/div[2]/div[2]/form/div[1]/div[3]")));

                // Verify the presence of the error message
                var errorMessage = _fixture.Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div/div/div/div/div[2]/div[2]/form/div[1]/div[3]")).Text;

                // Assert that the error message is displayed
                Assert.Contains("Incorrect email/username or password", errorMessage);

                // If we see the error message, the test should pass
                _fixture.Logger.LogInformation("Invalid credentials test passed with expected error message.");
            }
            catch (Exception ex)
            {
                _fixture.Logger.LogError($"Test failed: {ex.Message}");
                throw;
            }
        }

    }
}
