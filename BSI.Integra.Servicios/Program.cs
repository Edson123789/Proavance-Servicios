using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.Helper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BSI.Integra.Servicios
{
    public class Program
    {
        public static void Main(string[] args)
        {

				CreateWebHostBuilder(args)
                .ConfigureServices(servicesCollection => {
                    var Error = ErrorSistema.Instance;
                    var Valor = ValorEstatico.Instance;
                })
                .Build().Run();

            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>();
    }
}
