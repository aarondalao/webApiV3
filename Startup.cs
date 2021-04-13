using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

// added this along the way
using Newtonsoft.Json.Serialization;
using System.IO;
using Microsoft.Extensions.FileProviders;
namespace webAPIv3
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
            // Enable CORS (Cross-Origin Requests)
            // Browser security prevents a web page from making requests to a different domain than the one that served the web page. 
            // This restriction is called the same-origin policy. The same-origin policy prevents a malicious site from reading sensitive 
            // data from another site. Sometimes, you might want to allow other sites to make cross-origin requests to your app. 

            // NOTE::::
            // for the sake of the tutorial, leave this enabled. once I have the experience dealing with security measures in ASP.NET core, 
            // change this policy.

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            //JSON Serializer
            // JSON is a format that encodes objects in a string. Serialization means to convert an object into that string, and deserialization is its 
            // inverse operation (convert string -> object).

            // When transmitting data or storing them in a file, the data are required to be byte strings, but complex objects are seldom in this format. 
            // Serialization can convert these complex objects into byte strings for such use. After the byte strings are transmitted, the receiver will 
            // have to recover the original object from the byte string. This is known as deserialization.

            // Say, you have an object:

            // {foo: [1, 4, 7, 10], bar: "baz"}
            // serializing into JSON will convert it into a string:

            // '{"foo":[1,4,7,10],"bar":"baz"}'
            // which can be stored or sent through wire to anywhere. The receiver can then deserialize this string to get back the original object. 
            // {foo: [1, 4, 7, 10], bar: "baz"}.

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "webAPIv3", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //used Cors
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "webAPIv3 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //create instructions in startup.cs to use the newly created photo local folder
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"Photos")),
                RequestPath="/Photos"
            });
        }
    }
}
