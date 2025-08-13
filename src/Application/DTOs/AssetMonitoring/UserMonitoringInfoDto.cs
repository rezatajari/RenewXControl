using Domain.Entities.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AssetMonitoring;

public record UserMonitoringInfoDto(
    string? Username,
    string? SiteName,
    string? SiteLocation,
    Solar Solar,
    Turbine Turbine,
    BatteryDto Battery);
