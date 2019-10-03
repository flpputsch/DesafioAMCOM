using AMcom.Teste.DAL.Interface;
using AMcom.Teste.DAL.Repository;
using AMcom.Teste.Service.Interfaces;
using AMcom.Teste.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AMcom.Teste.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IUbsService, UbsService>();
            services.AddScoped<IErroHandler, ErroHandler>();

            //Data
            services.AddScoped<IUbsRepository, UbsRepository>();
        }
    }
}