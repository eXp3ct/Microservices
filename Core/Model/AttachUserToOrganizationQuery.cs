﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
	public class AttachUserToOrganizationQuery : IRequest
	{
		public Guid UserId { get; set; }
		public Guid OrganizationId { get; set; }
	}
}
