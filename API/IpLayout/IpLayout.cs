using System;
using NLog;
using NLog.Config;
using System.Text;
using NLog.LayoutRenderers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.IpLayout
{
  [LayoutRenderer("ip")]
  public class IpLayoutRenderer : LayoutRenderer
  {
    protected override void Append(StringBuilder builder, LogEventInfo logEvent)
    {
      IpAddress ipAddress = new IpAddress();
      string? ip = ipAddress.GetIpAddress();
      builder.Append(ip);
    }
  }
  public class IpAddress : ControllerBase
  {
    public string? GetIpAddress()
    {
      return HttpContext.Connection.RemoteIpAddress?.ToString();
    }
  }
}
