using ConexionBD;
using ElmahCore;
using ElmahCore.Mvc;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using DMBolsaTrabajo.IAplicacion;
using DMBolsaTrabajo.Aplicacion;
using DMBolsaTrabajo.Map;
using DMBolsaTrabajo.IRepositorio;
using DMBolsaTrabajo.Repositorio;
using DMBolsaTrabajo.ServiciosExt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DMBolsaTrabajo.Servicios
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Para usar System.Text.Encoding.GetEncoding("ISO-8859-8")
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddElmah<XmlFileErrorLog>(options =>
            {
                options.LogPath = "~/log";
                options.Notifiers.Add(new MyNotifier());
                options.Notifiers.Add(new MyNotifierWithId());
                options.Filters.Add(new CmsErrorLogFilter());
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "DMFormulariosPolicy",
                    builder =>
                    {
                        builder
                         .WithOrigins("http://localhost:4200", "https://dmbolsadetrabajo.derrama.org.pe")
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader();
                        // .SetIsOriginAllowed(origin => true);

                    });
            });


            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddTransient<IMySQLConexion, MySQLConexion>();

            services.AddTransient<ICatalogoDetalleAplicacion, CatalogoDetalleAplicacion>();
            services.AddTransient<ICatalogoDetalleRepositorio, CatalogoDetalleRepositorio>();

            services.AddTransient<IEventoAplicacion, EventoAplicacion>();
            services.AddTransient<IEventoRepositorio, EventoRepositorio>();

            services.AddTransient<IFormulariosAplicacion, FormulariosAplicacion>();
            services.AddTransient<IFormulariosRepositorio, FormulariosRepositorio>();

            services.AddTransient<IMenuAplicacion, MenuAplicacion>();
            services.AddTransient<IMenuRepositorio, MenuRepositorio>();

            services.AddTransient<IReportesAplicacion, ReportesAplicacion>();

            services.AddTransient<IPuestosAplicacion, PuestosAplicacion>();
            services.AddTransient<IPuestosRepositorio, PuestosRepositorio>();

            services.AddTransient<IRolAplicacion, RolAplicacion>();
            services.AddTransient<IRolRepositorio, RolRepositorio>();

            services.AddTransient<ISeguridadAplicacion, SeguridadAplicacion>();
            services.AddTransient<ISeguridadRepositorio, SeguridadRepositorio>();

            services.AddTransient<IUsuarioAplicacion, UsuarioAplicacion>();
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();

            services.AddTransient<IServicioEnviarCorreo, ServicioEnviarCorreo>();

            services.AddHttpClient();
            services.AddScoped<IRecaptchaService, RecaptchaService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            #region AutoMap

            services.AddAutoMapper(typeof(FormulariosMap));
            services.AddAutoMapper(typeof(SeguridadMap));
            services.AddAutoMapper(typeof(UsuarioMap));
            services.AddAutoMapper(typeof(CatalogoDetalleMap));

            #endregion AutoMap

            services.AddHostedService<DatabaseKeepAliveService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DM Formularios", Version = "v1" });
            });

            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("DMFormulariosPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseElmah();
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(next => context =>
            {
                context.Request.EnableBuffering();
                return next(context);
            });
        }
    }

    public class MyNotifier : IErrorNotifier
    {
        public void Notify(Error error)
        {
            Debug.WriteLine(error.Message);
        }

        public string Name => "my";
    }
    public class MyNotifierWithId : IErrorNotifierWithId
    {
        public void Notify(Error error)
        {
            throw new System.NotImplementedException();
        }

        public void Notify(string id, Error error)
        {
            Debug.WriteLine(error.Message);
        }

        public string Name => "myWithId";
    }

    public class CmsErrorLogFilter : IErrorFilter
    {
        public void OnErrorModuleFiltering(object sender, ExceptionFilterEventArgs args)
        {
            if (args.Exception.GetBaseException() is FileNotFoundException)
                args.Dismiss();
            if (args.Context is HttpContext httpContext)
                if (httpContext.Response.StatusCode == 404)
                    args.Dismiss();
        }
    }
}