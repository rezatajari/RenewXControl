using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User.Auth;

public record AuthUser
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }

}