using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using EntityLayer.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerApi
{
    public static class DataSeed
    {
        //public static void ConfigureDbSeed(ConfigurationDbContext context)
        //{
        //    if (!context.Clients.Any())
        //    {
        //        foreach (var client in Config.Clients())
        //        {
        //            context.Clients.Add(client.ToEntity());
        //        }
        //    }
        //    if (!context.ApiResources.Any())
        //    {
        //        foreach (var client in Config.ApiResources())
        //        {
        //            context.ApiResources.Add(client.ToEntity());

        //        }
        //    }
        //    if (!context.ApiScopes.Any())
        //    {
        //        foreach (var client in Config.ApiScopes())
        //        {
        //            context.ApiScopes.Add(client.ToEntity());

        //        }
        //    }
        //    if (!context.IdentityResources.Any())
        //    {
        //        foreach (var client in Config.IdentityResources())
        //        {
        //            context.IdentityResources.Add(client.ToEntity());

        //        }
        //    }
        //    context.SaveChanges();

        //}

    }
}
