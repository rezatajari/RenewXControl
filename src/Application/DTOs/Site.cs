
using System.Security.AccessControl;

namespace Application.DTOs;

public record Site(
    Guid SiteId,
    string Name,
    string Location);
