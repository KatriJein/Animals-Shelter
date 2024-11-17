﻿using Core.Responses.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Users.Auth
{
	public class UserRegistrationResponse : BaseResponse
	{
		public Guid UserId { get; set; }
	}
}
