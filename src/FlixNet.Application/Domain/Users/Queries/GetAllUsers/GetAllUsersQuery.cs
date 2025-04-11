using FlixNet.Application.Common;
using FlixNet.Application.Domain.Users.Commands;
using FlixNet.Core.Domain.Users.Models;
using MediatR;

namespace FlixNet.Application.Domain.Users.Queries.GetAllUsers;

public record GetAllUsersQuery(int Page, int PageSize) : IRequest<PageResponse<UserDto[]>>;

