namespace VirheBT;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services
         .AddBlazorise()
         .AddBootstrapProviders()
         .AddFontAwesomeIcons()
         .AddBlazoriseRichTextEdit();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("VirheBT.Infrastructure")).EnableSensitiveDataLogging());
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddDefaultUI();

        services.AddScoped<ApplicationDbContext>();
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();

        services.AddScoped<IIssueCommentRepository, IssueCommentRepository>();
        services.AddScoped<IIssueHistoryRepository, IssueHistoryRepository>();

        services.AddScoped<IIssueRepository, IssueRepository>();
        services.AddScoped<IIssueService, IssueService>();

        services.AddSingleton<IAppState, AppState>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });

        CreateRoles(serviceProvider);
    }

    private void CreateRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        Task<IdentityResult> roleResult;
        string email = "test@test.com";

        string[] roles = { "Admin", "Programmer", "Tester", "ProjectManager" };

        foreach (var item in roles)
        {
            Task<bool> hasRole = roleManager.RoleExistsAsync(item);
            hasRole.Wait();

            if (!hasRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole(item));
                roleResult.Wait();
            }
        }

        Task<ApplicationUser> testUser = userManager.FindByEmailAsync(email);
        testUser.Wait();

        if (testUser.Result == null)
        {
            ApplicationUser administrator = new ApplicationUser();
            administrator.Email = email;
            administrator.UserName = email;

            Task<IdentityResult> newUser = userManager.CreateAsync(administrator, "Qazwsx1.");
            newUser.Wait();

            if (newUser.Result.Succeeded)
            {
                Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Admin");

                newUserRole.Wait();
            }
        }
    }
}