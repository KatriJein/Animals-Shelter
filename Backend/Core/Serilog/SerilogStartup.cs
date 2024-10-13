using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Serilog
{
	public static class SerilogStartup
	{
		public static ConfigureHostBuilder AddSerilog(this ConfigureHostBuilder hostBuilder)
		{
			hostBuilder.UseSerilog((context, config) =>
			{
				config.ReadFrom.Configuration(context.Configuration);
			});
			return hostBuilder;
		}
	}
}
