using Microsoft.EntityFrameworkCore;
using lab3_School.Models;
using lab3_School.CachedModels;

namespace lab3_School
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = "Data Source=DESKTOP-I3E57O3\\SQLEXPRESS;Initial Catalog=School;Integrated Security=True; Encrypt=False";
            services.AddDbContext<SchoolContext>(options => options.UseSqlServer(connection));
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddScoped<CachedTeacher>();
            services.AddScoped<CachedClassType>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.Map("/info", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    string strResponse =
                    "<html><head><title>Информация</title>" +
                    "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/></head>" +
                    "<body>" +
                        "<p><a href = '/'>На главную</a></p>" +
                        "<p>Информация:</p>" +
                        "Сервер: " + context.Request.Host +
                        "<br>Путь: " + context.Request.PathBase +
                        "<br>Протокол: " + context.Request.Protocol +
                    "</body></html>";
                    await context.Response.WriteAsync(strResponse);
                });
            });

            
            app.Map("/teachers", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    CachedTeacher cachedTeacher = context.RequestServices.GetService<CachedTeacher>();
                    IEnumerable<Teacher> teachers = cachedTeacher.GetTeachers("Teacher20", 20);
                    await context.Response.WriteAsync(cachedTeacher.GetTable(teachers));
                });
            });
            app.Map("/classTypes", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    CachedClassType cachedClassType = context.RequestServices.GetService<CachedClassType>();
                    IEnumerable<ClassType> classTypes = cachedClassType.GetClassTypes(20);
                    await context.Response.WriteAsync(cachedClassType.GetTable(classTypes));
                });
            });
            app.Map("/searchTeacher", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    string surname = "";
                    if (context.Request.Cookies.ContainsKey("surname"))
                    {
                        surname = context.Request.Cookies["surname"] ?? string.Empty;
                    }

                    string strResponse = "<html><head><title>Найти учителя</title>" +
                                        "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/></head><body>" +
                                        "<p><a href = '/'>На главную</a></p>" +
                                        "<form action = / >" +
                                            "Найти учителя по фамилии:<br><input type = 'text', name = 'surname', value = " + surname + ">" +
                                            "<br><br><input type = 'submit' value = 'Найти' >" +
                                        "</form>" +
                                        "</body></html>";

                    await context.Response.WriteAsync(strResponse);
                });
            });
            app.MapWhen(context =>
            {
                return context.Request.Query.ContainsKey("surname");
            }, HandleSearch1);
            app.Map("/searchClassType", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    string name = string.Empty;
                    if (context.Request.Cookies.ContainsKey("name"))
                    {
                        name = context.Request.Cookies["name"] ?? string.Empty;
                    }

                    string strResponse = "<html><head><title>Найти тип класса</title>" +
                                        "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/></head><body>" +
                                        "<p><a href = '/'>На главную</a></p>" +
                                        "<form action = / >" +
                                            "Найти тип класса по названию:<br><input type = 'text', name = 'name', value = " + name + ">" +
                                            "<br><br><input type = 'submit' value = 'Найти' >" +
                                        "</form>" +
                                        "</body></html>";

                    await context.Response.WriteAsync(strResponse);
                });
            });
            app.MapWhen(context =>
            {
                return context.Request.Query.ContainsKey("name");
            }, HandleSearch2);
            app.Run((context) =>
            {
                CachedTeacher cachedTeacher = context.RequestServices.GetService<CachedTeacher>();
                //CashedOrder cashedOrder = context.RequestServices.GetService<CashedOrder>();
                cachedTeacher.AddTeachers("Teacher20", 20);
                string HtmlString = "<html><head><title>Главная</title>" +
                "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/></head><body>";
                HtmlString += "<br><a href='/info'>Информация</a></br>";
                HtmlString += "<br><a href='/teachers'>Учителя</a></br>";
                HtmlString += "<br><a href='/classTypes'>Типы классов</a></br>";
                HtmlString += "<br><a href='/searchTeacher'>Найти учителя</a></br>";
                HtmlString += "<br><a href='/searchClassType'>Найти тип класса</a></br>";
                HtmlString += "</body></html>";
                return context.Response.WriteAsync(HtmlString);
            });
        }
        public static void HandleSearch1(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                CachedTeacher cached = context.RequestServices.GetService<CachedTeacher>();
                if (context.Request.Cookies.ContainsKey("surname"))
                {
                    context.Response.Cookies.Delete("surname");
                }
                context.Response.Cookies.Append("surname", context.Request.Query["surname"]);
                await context.Response.WriteAsync(cached.GetTable(context.Request.Query["surname"]));
            });
        }
        public static void HandleSearch2(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                CachedClassType cached = context.RequestServices.GetService<CachedClassType>();
                if (context.Request.Cookies.ContainsKey("name"))
                {
                    context.Response.Cookies.Delete("name");
                }
                context.Response.Cookies.Append("name", context.Request.Query["name"]);
                await context.Response.WriteAsync(cached.GetTable(context.Request.Query["name"]));
            });
        }
    }
}