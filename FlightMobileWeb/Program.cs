using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using FlightMobileApp.models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FlightMobileApp
{
    public class Program
    {
        public static string screenshotPort;
        public static string valuesPort;
        public static string simIP;
        public static ClientToSim c = new ClientToSim();
        public static void Main(string[] args)
        {

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"misc.txt");
            string[] files = File.ReadAllLines(path);
            screenshotPort = files[0]; valuesPort = files[1]; simIP = files[2]; string url = files[3];
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            CreateHostBuilder(args, url).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, string url) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(url);
                    webBuilder.UseStartup<Startup>();
                });

        public static void OnProcessExit(object sender, EventArgs e)
        {
            c.Close();
        }
    }
}
