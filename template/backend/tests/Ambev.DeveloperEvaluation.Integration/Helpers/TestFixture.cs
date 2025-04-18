using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Integration
{
    public class TestFixture
    {
        public HttpClient Client { get; }

        public TestFixture()
        {
            var factory = new CustomWebApplicationFactory<Program>();
            Client = factory.CreateClient();
        }
    }
}
