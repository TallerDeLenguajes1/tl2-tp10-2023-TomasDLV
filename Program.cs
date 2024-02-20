using tl2_proyecto_TomasDLV.Repositorios;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Agrego para session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor(); //Allows HttpContextAccesor dependency inyection over UserCheck


var CadenaDeConexion = builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();
builder.Services.AddSingleton<string>(CadenaDeConexion);

builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();
builder.Services.AddScoped<ITableroRepository, TableroRepository>();
builder.Services.AddScoped<ITareaRepository, TareaRepository>();
builder.Services.AddScoped<UserCheck>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(300);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//Agrego esto
app.UseSession();

app.UseAuthorization();

//Agrego middleware para hacer que el objeto UserCheck esté disponible en el contexto de cada solicitud HTTP. 
app.Use((context, next) =>
{
    context.Items["UserCheck"] = context.RequestServices.GetRequiredService<UserCheck>();
    return next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
