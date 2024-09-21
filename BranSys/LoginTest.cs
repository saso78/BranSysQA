using BranSys.BaseTest;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranSys
{
    public class LoginTest : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;
        public LoginTest(TestFixture fixture) 
        {
            _fixture = fixture;
            _fixture.Logger.LogInformation("Test started.");
        }
    }
}
