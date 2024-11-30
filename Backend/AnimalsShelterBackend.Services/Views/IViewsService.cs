using AnimalsShelterBackend.Domain.ArticleViews;
using Core.Base.Services;
using Core.Requests.Views;
using Core.Responses.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Views
{
	public interface IViewsService: IService<View>
	{
		Task<CountViewResponse> CountViewAsync(CountViewRequest countViewRequest);
	}
}
