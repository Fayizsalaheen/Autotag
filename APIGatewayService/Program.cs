using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Microsoft.AspNetCore.Hosting; // ⬅️ إضافة هذا الاستدعاء

var builder = WebApplication.CreateBuilder(args);

// 1. إضافة توجيه Kestrel للاستماع على المنفذ 8080 داخل الحاوية 
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});
// -----------------------------------------------------------

builder.Services.AddControllers();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// تهيئة Swagger ثم Ocelot 
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddOcelot();


var app = builder.Build();


// استخدام واجهة Swagger (يجب أن يكون قبل app.UseOcelot())
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});

app.UseHttpsRedirection();


// 3. استخدام Ocelot
await app.UseOcelot();

app.MapControllers();

app.Run();