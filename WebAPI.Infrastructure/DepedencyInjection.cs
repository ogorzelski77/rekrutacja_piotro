using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using WebAPI.Application.Interfaces;
using WebAPI.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			


			//services.AddDbContext<RekrutacjaContext>(opts =>
			//		opts.UseSqlServer(configuration.GetConnectionString("Rekrutacja")));

	
			services.AddScoped<IArticleService, ArticleService>();
			services.AddScoped<IPriceService, PriceService>();
			return services;
		}
	}
}
