using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Soccer.Web.DataAccess;
using Soccer.Web.DataAccess.Data;
using Soccer.Web.DataAccess.Data.Repository;
using Soccer.Web.Models.Entities;
using System.Text;

namespace Soccer.API
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
            services.AddIdentity<ApplicationUser, IdentityRole>(cfg =>
            {
                cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                cfg.User.RequireUniqueEmail = true;
                cfg.SignIn.RequireConfirmedEmail = false;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0; /*cantidad de caracteres especiales*/
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequiredLength = 6; /*cantidad minima requerida de caracteres para la clave*/
            })
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            /*esta linea hay que agregarla para la conexion con la base de datos*/
            /*cualquier clase que implemente el DataContext en su constructor esta utilizando la conexion con la BD*/
            services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = this.Configuration["Tokens:Issuer"],
                        ValidAudience = this.Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(this.Configuration["Tokens:Key"]))
                    };
                });

            services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();  /*Inyector de dependencias*/

            /*esta linea permite eliminar los errores de referencia ciclica*/
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
