using healthcheck.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace healthcheck.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult HealthCheck()
        {
            // instance name check
            string COMPUTERNAME = Environment.GetEnvironmentVariable("COMPUTERNAME");
            string RoleInstanceId = Environment.GetEnvironmentVariable("RoleInstanceId");

            // read instance name from list file and compare
            //List<String> instanceList = new List<string>();
            try
            {
                String filepath = Path.Join(Environment.GetEnvironmentVariable("HOME"), "ignore_instance.txt");
                StreamReader sr = new StreamReader(filepath);
                string line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    _logger.LogInformation(line.TrimEnd());
                    //instanceList.Add();
                    if (line.TrimEnd().Equals(COMPUTERNAME, StringComparison.OrdinalIgnoreCase))
                    {
                        return BadRequest();
                    }
                    if (line.TrimEnd().Equals(RoleInstanceId, StringComparison.OrdinalIgnoreCase))
                    {
                        return BadRequest();
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
