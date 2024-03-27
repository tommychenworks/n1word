using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using n1word_api.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n1word_api.IntegrationTests
{
    public class WebApplicationFactoryIntegration : WebApplicationFactory<Program>
    {
        


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureTestServices(services =>
            {

                // 3 - Use configuration (before BuildServiceProvider)
                services.Configure<WordConfig>(opt =>
                {
                    opt.jsonWordDB = "AppData\\word.json";
                });
            });

            base.ConfigureWebHost(builder);
        }
    }
}
