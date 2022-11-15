using Hyperlogy.BL.Extensions;
using Hyperlogy.Common.Entities;
using Hyperlogy.Common.Global;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hyperlogy.API
{
    public class Startup
    {
        private Appsettings appsettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hyperlogy.API", Version = "v1" });
            });

            Global.ConnectionString = Configuration.GetConnectionString("NQLinh");
            services.AddServices();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        //Xac thuc may chu
            //        ValidateIssuer = true,
            //        //Xác thực người nhận mã thông báo được phép nhận
            //        ValidateAudience = true,
            //        //Kiểm tra xem mã thông báo chưa hết hạn và khóa ký của tổ chức phát hành có hợp lệ không
            //        ValidateLifetime = true,
            //        //Xác thực chữ ký của mã thông báo
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        ValidAudience = Configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //    };
            //});
            services.AddMvc();

            // configure strongly typed settings object
            services.Configure<Appsettings>(Configuration.GetSection("Jwt"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<Appsettings> appSettingsAccessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hyperlogy.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            //app.UseMvc();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            appsettings = appSettingsAccessor.Value;
            LoadConfig();
        }
        private void LoadConfig()
        {
            WebJwtConstants.SECRET_KEY = string.Format(WebJwtConstants.SECRET_KEY, appsettings.Secret);
        }
    }
}
