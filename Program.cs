using lab3_School.CachedModels;
using lab3_School.Models;
using Microsoft.EntityFrameworkCore;

namespace lab3_School
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).Build().Run();
        }
    }
}