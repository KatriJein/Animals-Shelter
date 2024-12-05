using AnimalsShelterBackend.Domain.Feedbacks;
using AnimalsShelterBackend.Services.Users;
using Core.Base.Services;
using Core.Requests;
using Core.Requests.Feedbacks;
using Core.Responses.Feedbacks;
using Core.Responses.General;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Feedbacks
{
	public class FeedbacksService: BaseService<Feedback>, IFeedbacksService
	{
		private readonly IFeedbacksRepository _feedbacksRepository;

		public FeedbacksService(IFeedbacksRepository feedbacksRepository): base(feedbacksRepository)
		{
			_feedbacksRepository = feedbacksRepository;
		}

		public async Task<CreateFeedbackResponse> AddAsync(Feedback feedback, IUserService userService)
		{
			var user = await userService.GetByGuidAsync(feedback.UserId, CancellationToken.None);
			if (user == null) return new CreateFeedbackResponse() { IsSuccess = false, Message = "Несуществующий пользователь" };
			if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Surname))
				return new CreateFeedbackResponse() { IsSuccess = false, Message = "Необходимо заполнить имя и фамилию перед отправкой отзыва" };
			if (feedback.Rating < 1 || feedback.Rating > 5)
				return new CreateFeedbackResponse() { IsSuccess = false, Message = "Оценка должна быть в пределах от 1 до 5 звезд" };
			await userService.LoadUserFeedbackAsync(user);
			if (user.Feedback != null)
				return new CreateFeedbackResponse() { IsSuccess = false, Message = "Необходимо удалить предыдущий отзыв или редактировать текущий" };
			feedback.UserName = UserUtils.CreateFeedbackName(user.Name, user.Surname);
			feedback.CreatedAt = DateTime.UtcNow;
			feedback.User = user;
			var entityIdResponse = await base.AddAsync(feedback);
			return new CreateFeedbackResponse() { IsSuccess = true, EntityId = entityIdResponse.Id };
		}

		public override async Task<BaseResponse> UpdateAsync(Guid id, IUpdateRequest request)
		{
			var feedback = await GetByGuidAsync(id, CancellationToken.None);
			if (feedback == null) return new UpdateFeedbackResponse() { IsSuccess = false, Message = "Несуществующий отзыв" };
			var updateFeedbackData = (UpdateFeedbackRequest)request;
			feedback.Text = updateFeedbackData.Text ?? feedback.Text;
			feedback.Rating = updateFeedbackData.Rating ?? feedback.Rating;
			if (feedback.Rating < 1 || feedback.Rating > 5)
				return new UpdateFeedbackResponse() { IsSuccess = false, Message = "Оценка должна быть в пределах от 1 до 5 звезд" };
			return await base.UpdateAsync(id, request);
		}
	}
}
