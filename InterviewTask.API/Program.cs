
using Autofac.Extensions.DependencyInjection;
using Autofac;
using InterviewTask.API.Shared;
using Autofac.Core;
using System.Reflection;
using InterviewTask.API.Middelwares;
using Hangfire;

namespace InterviewTask.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddServicesConfigration(builder.Configuration);
            builder.Services.AddScoped<CanceletionState>();


            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new AutoFacModule());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddelware>();
            app.UseMiddleware<TransactionMiddleware>();
            app.UseHttpsRedirection();
            app.MapHangfireDashboard("/jobs");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
