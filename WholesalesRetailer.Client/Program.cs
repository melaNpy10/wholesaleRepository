using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WholesalesRetailer.Client;
using WholesalesRetailer.Client.Services;
using WholesalesRetailer.Client.Services.Interfaces;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IOrderServiceFE, OrderServiceFE>();
builder.Services.AddScoped<IProductServiceFE, ProductServiceFE>();
builder.Services.AddScoped<ICustomerServiceFE, CustomerServiceFE>();

builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();
