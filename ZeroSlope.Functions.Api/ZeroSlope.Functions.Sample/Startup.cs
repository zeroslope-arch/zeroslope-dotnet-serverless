using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ZeroSlope.Domain.Services;
using AutoMapper.Configuration;
using ZeroSlope.Packages.DotNet.AutoMapper.Installers;
using ZeroSlope.Packages.DotNet.IService.Installers;
using ZeroSlope.Packages.DotNet.Redis.Installers;
using ZeroSlope.Packages.DotNet.Serilogger.Installers;
using System;

[assembly: FunctionsStartup(typeof(ZeroSlope.Functions.Sample.Startup))]

namespace ZeroSlope.Functions.Sample
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var sqlConnectionString = GetConfigSetting("SqlConnectionString");

            new SeriloggerInstaller()
                .Install(builder.Services);

            new IDbConnectionInstaller(sqlConnectionString)
                .Install(builder.Services);

            new AutoMapperInstaller(new MapperConfigurationExpression() { })
                .Install(builder.Services);

            builder.Services.Scan(scan => new IServiceInstaller(typeof(Domain.Init)).Install(scan));
        }

        private string GetConfigSetting(string configName)
        {
            return Environment.GetEnvironmentVariable(configName, EnvironmentVariableTarget.Process);
        }
    }
}