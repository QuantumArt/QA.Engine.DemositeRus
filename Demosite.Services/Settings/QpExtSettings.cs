using QA.DotNetCore.Engine.Persistent.Interfaces.Settings;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demosite.Services.Settings;
public class QpExtSettings : QpSettings
{
    [Required]
    public string Url { get; set; } = default!;
    public TimeSpan ContentCacheExpiration { get; set; }
}
