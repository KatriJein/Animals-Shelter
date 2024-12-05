using AnimalsShelterBackend.Domain.Feedbacks;
using AnimalsShelterBackend.Services.Users;
using Core.Base.Services;
using Core.Responses.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Feedbacks
{
	public interface IFeedbacksService: IService<Feedback>
	{
		Task<CreateFeedbackResponse> AddAsync(Feedback feedback, IUserService userService);
	}
}
