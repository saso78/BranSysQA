# Login Page Testing Project

## Overview
This project is a C# .NET Core application designed to test a login page for the Bransys website. It includes automated tests to verify the functionality of the login process, focusing on input validation and error message display.

## Technologies Used
- C# .NET Core (version above .NET 5)
- Selenium WebDriver for browser automation
- xUnit for unit testing
- ILogger for logging

## Project Structure
- **BaseTest.cs**: Contains the `TestFixture` class, which initializes the Selenium WebDriver, manages waits, and provides utility methods for interacting with the browser.
- **BrowserUtility.cs**: This would typically contain helper functions for browser-related operations.
- **TestOrder.cs**:Contains a method to give order of execution to the tests.
- **LoginTest.cs**: Contains the test cases that validate the login functionality.

## Test Scenarios
The following test scenarios have been implemented:

1. **Presence of Login Fields**: Verify that the username and password input fields are present on the login page.
2. **Input Data Validation**: Check if it's possible to input data values into the username and password fields.
3. **Invalid Credentials Handling**: Input invalid credentials, check for input validation, submit the form, and verify the presence of the error message "Incorrect email/username or password."

## Test Execution
To run the tests:
1. Clone this repository to your local machine.
2. Open the project in Visual Studio or your preferred IDE.
3. Ensure that you have the necessary dependencies installed (e.g., Selenium WebDriver).
4. Run the tests using the xUnit test runner.

This test case demonstrates the following flow:
- Inputs invalid username and password.
- Checks if the input fields contain the expected values.
- Submits the form by pressing Enter.
- Verifies that the appropriate error message is displayed if login fails.

