using Xunit;
using Entity.Dtos;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Entity.Data;
using Xunit.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API;
namespace Testing
{
  public class FileTesting : BaseTesting
  {
    private readonly WebApplicationFactory<Startup> _factory;

    public FileTesting(WebApplicationFactory<Startup> factory)
    {
      _factory = factory;
    }
  }
}