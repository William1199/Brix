using Application.System.Bill;
using Application.System.Carts;
using Application.System.Category;
using Application.System.Commitments;
using Application.System.ContractorPosts;
using Application.System.Identification;
using Application.System.MaterialStores;
using Application.System.Notifies;
using Application.System.Payments;
using Application.System.PostInvite;
using Application.System.Quizzes;
using Application.System.Reports;
using Application.System.SavePost;
using Application.System.Skills;
using Application.System.Types;
using Application.System.Users;
using Application.System.VnPay;
using BuildingConstructApi.Hubs;
using Data.DataContext;
using Data.DbInitiallize;
using Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ViewModels.ContractorPost;
using ViewModels.Users;
using static Constants.Constants;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuildingConstruct.Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
});
string issuer = builder.Configuration.GetValue<string>("Tokens:Issuer");
string signingKey = builder.Configuration.GetValue<string>("Tokens:Key");
byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = issuer,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = System.TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = async (context) =>
        {
            Console.WriteLine("Printing in the delegate OnChallenge");

            context.HandleResponse();

            if (context.AuthenticateFailure != null)
            {
                //DentistResponse response = new();
                //response.Message = "Token Validation Has Failed. Request Access Denied";
                //response.Code = "900";
                if (!context.Response.HasStarted)
                {
                    string result;
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    result = JsonConvert.SerializeObject(new { code = "900", message = "Token Validation Has Failed. Request Access Denied" });
                    context.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync(result);
                }

            }
        },
        OnAuthenticationFailed = async (context) =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                //context.Response.StatusCode = 200;
                //DentistResponse response = new();
                string result;
                //response.Message = "Token has expired";
                //response.Code = "901";
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    result = JsonConvert.SerializeObject(new { code = "901", message = "Token has expired" });
                    context.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync(result);
                }
            }
        }
    };

});
builder.Services.AddDbContext<BuildingConstructDbContext>(options => options.
           UseSqlServer(builder.Configuration.GetConnectionString(SystemsConstant.MainConnectionString)));
builder.Services.AddIdentity<User, Role>(
    options =>
    {
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
    }
    ).AddEntityFrameworkStores<BuildingConstructDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager<User>, UserManager<User>>();
builder.Services.AddScoped<SignInManager<User>, SignInManager<User>>();
builder.Services.AddScoped<IUserConnectionManager, UserConnectionManager>();
builder.Services.AddScoped<RoleManager<Role>, RoleManager<Role>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<ISaveService, SaveService>();
builder.Services.AddScoped<IPaymentsService, PaymentsService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IValidator<RegisterRequestDTO>, RegisterRequestValidatorDTO>();
builder.Services.AddScoped<IValidator<ContractorPostModels>, ContractorPostValidator>();
builder.Services.AddTransient<IContractorPostService, ContractorPostService>();
builder.Services.AddScoped<IMaterialStoreService, MaterialStoreService>();
builder.Services.AddScoped<INotificationServices, NotificationServices>();
builder.Services.AddScoped<ICommitmentService, CommitmentService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ISkillService, SkillServices>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddScoped<IBillServices, BillServices>();
builder.Services.AddScoped<IIdentificationService, IdentificationService>();
builder.Services.AddScoped<IPostInviteService, PostIniviteService>();
builder.Services.AddScoped<IQuizServices, QuizServices>();

var app = builder.Build();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}
//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(x => x
        .WithOrigins("https://localhost:4000", "https://buildingconstruct.space")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials());
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationUserHub>("/NotificationUserHub");
});

app.Run();
