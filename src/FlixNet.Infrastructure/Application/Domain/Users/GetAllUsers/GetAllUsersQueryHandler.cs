using FlixNet.Application.Common;
using FlixNet.Application.Domain.Users.Commands;
using FlixNet.Application.Domain.Users.Queries.GetAllUsers;
using FlixNet.Core.Domain.Users.Common;
using FlixNet.Core.Domain.Users.Models;
using FlixNet.Persistence.EFCore.FlixNetDb;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlixNet.Infrastructure.Application.Domain.Users.GetAllUsers;

public class GetAllUsersQueryHandler(FlixNetDbContext dbContext)
    : IRequestHandler<GetAllUsersQuery, PageResponse<UserDto[]>>
{
    public async Task<PageResponse<UserDto[]>> Handle(
        GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var skip = request.PageSize * (request.Page - 1);

        var sqlQuery = dbContext.Users
            .AsNoTracking()
            .OrderBy(u => u.Name);

        var totalCount = await sqlQuery.CountAsync(cancellationToken);

        var users = await sqlQuery
            .Skip(skip)
            .Take(request.PageSize)
            .Select(u => new UserDto(
                u.Id,
                u.Name,
                u.Email,
                u.PictureUrl
            ))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<UserDto[]>(totalCount, users);
    }
}

