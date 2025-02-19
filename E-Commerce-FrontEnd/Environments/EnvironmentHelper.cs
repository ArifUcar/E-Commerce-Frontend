using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace E_Commerce_FrontEnd.Environments;

public class EnvironmentHelper
{
    private readonly IWebAssemblyHostEnvironment _environment;

    public EnvironmentHelper(IWebAssemblyHostEnvironment environment)
    {
        _environment = environment;
    }

    public bool IsDevelopment()
    {
        return _environment.IsDevelopment();
    }

    public bool IsProduction()
    {
        return _environment.IsProduction();
    }

    public string GetEnvironmentName()
    {
        return _environment.Environment;
    }
} 