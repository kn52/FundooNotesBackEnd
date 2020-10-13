using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using EShoppingService.Infc;
using EShoppingService.Impl;
using EShoppingModel.Infc;
using EShoppingModel.Impl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace EShopping
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Book service
            services.AddSingleton<IBookService, BookService>();
            services.AddSingleton<IBookRepository, BookRepository>();

            //Admin service
            services.AddSingleton<IAdminService, AdminService>();
            services.AddSingleton<IAdminRepository, AdminRepository>();

            //User service
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserRepository, UserRepository>();

            //Database config
            services.AddSingleton(Configuration);

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "e Bookstore"});
            });

            //Cross Origin
            //services.AddCors();
            services.AddCors(c =>
            {
                c.AddPolicy("CORS",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .WithExposedHeaders();
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            //app.UseSwaggerUI();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "e Bookstore");
            });

            //CORS
            app.UseCors();
        }
    }
}
