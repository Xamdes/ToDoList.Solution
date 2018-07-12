using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ToDoList
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
      .SetBasePath(env.ContentRootPath)
      .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration
    {
      get;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseStaticFiles();
      app.UseMvc(routes =>
      {
        routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
      });

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello World!");
      });
    }
  }

  public static class DBConfiguration
  {
    private static string _connectionString = "server=localhost;user id=root;password=root;port=8889;database=sc_todolist;Convert Zero Datetime=True;Allow User Variables=true;";

    public static string GetConnection()
    {
      return _connectionString;
    }

    public static void SetConnection(string s)
    {
      _connectionString = s;
    }

    public static void DefaultConnection()
    {
      _connectionString = "server=localhost;user id=root;password=root;port=8889;database=sc_todolist;Convert Zero Datetime=True;Allow User Variables=true;";
    }
  }
}
