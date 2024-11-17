using AnimalsShelterBackend.Services.Users.AuthServices;
using Core.Requests.Users;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users.Seeds
{
	public class SeedService : ISeedService
	{
		private readonly IUserService _userService;
		private readonly IAuthService _authService;
		private readonly ILogger _logger;

		public SeedService(IAuthService authService,  ILogger logger, IUserService userService)
		{
			_authService = authService;
			_logger = logger;
			_userService = userService;
		}
		public async Task CreateAdminUser()
		{
			var adminUser = await _userService.FindAdminUserAsync();
			if (adminUser != null)
			{
				_logger.Information("Администратор уже создан, дополнительных действий не требуется");
				return;
			}
			var adminRegisterRequest = new UserRegisterRequest("Lapochka.Administrator@yandex.ru", "Admin_1_Password");
			try
			{
				await _authService.RegisterAsync(adminRegisterRequest, createAdmin:true);
				_logger.Information("Администратор создан.");
			}
			catch (Exception e)
			{
				_logger.Error(e.Message);
			}
		}
	}
}
