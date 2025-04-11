using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixNet.Application.Domain.Users.Commands.UpdateProfile;

public record UpdateProfileCommand(
    Guid UserId, 
    string Name, 
    string PictureUrl) : IRequest<Unit>;
