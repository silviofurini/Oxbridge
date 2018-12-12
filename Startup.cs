using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ApiOxStd.Models;
using ApiOxStd.Repositorio;


namespace ApiOxStd
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
   //        var connectionString = "Server=(local)\\SQLEXPRESS;Database=oxbridge2;Trusted_Connection=True;MultipleActiveResultSets=true";
            string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oxbridge2;Data Source=CENW10N09199\\SQLEXPRESS";


            services.AddDbContext<StudentDbContext>(options =>
            options.UseSqlServer(connectionString));
            
            // Pegar informações de Aulas por Aluno
            services.AddDbContext<AulasAlunoDbContext>(options =>
            options.UseSqlServer(connectionString));
            
            // Pegar informações de Aulas por Aluno
            services.AddDbContext<AulasDoAlunoDbContext>(options =>
            options.UseSqlServer(connectionString));

            // Pegar informações de Reposicao de Aulas 
            services.AddDbContext<AulasReposicaoDbContext>(options =>
            options.UseSqlServer(connectionString));


            // Pegar informações de Aulas por Aluno
            services.AddDbContext<CursosDoAlunoDbContext>(options =>
            options.UseSqlServer(connectionString));

            // Pegar informações de Informativos - Contrato
            services.AddDbContext<InformativoDbContext>(options =>
            options.UseSqlServer(connectionString));

            // Pegar informações de Informativos - Contrato
            services.AddDbContext<AvisoDbContext>(options =>
            options.UseSqlServer(connectionString));

            // Pegar informações da Aula selecionada
            services.AddDbContext<DetalheAulaDbContext>(options =>
            options.UseSqlServer(connectionString));

            // Pegar informações do Contrato do aluno 
            services.AddDbContext<PessoaContratoDbContext>(options =>
            options.UseSqlServer(connectionString));

            // Pegar informações do Notas do aluno 
            services.AddDbContext<NotaAlunoDbContext>(options =>
            options.UseSqlServer(connectionString));


            // Pegar informações do Notas do aluno 
            services.AddDbContext<LoginDbContext>(options =>
            options.UseSqlServer(connectionString));

            // --------------------------------------------------------------------------------------------------//

            //options.UseSqlServer(Configuration.GetConnectionString(connectionString)));

            // Adicionar Transient e Repository Students
            services.AddTransient<IStudentRepository, StudentRepository>();

            // Adicionar Transient e Repository Aulas Aluno
            services.AddTransient<IAulasAlunoRepository, AulasAlunoRepository>();

            // Adicionar Transient e Repository Aulas Aluno
            services.AddTransient<IAulasDoAlunoRepository, AulasDoAlunoRepository>();

            // Adicionar Transient e Repository Aulas Reposicao
            services.AddTransient<IAulasReposicaoRepository, AulasReposicaoRepository>();

            // Adicionar Transient e Repository Cursos do Aluno
            services.AddTransient<ICursosDoAlunoRepository, CursosDoAlunoRepository>();

            // Adicionar Transient e Repository Informativo
            services.AddTransient<IInformativoRepository, InformativoRepository>();

            // Adicionar Transient e Repository Informativo
            services.AddTransient<IAvisoRepository, AvisosRepository>();

            // Adicionar Transient e Repository Detalhe Aula
            services.AddTransient<IDetalheAulaRepository, DetalheAulaRepository>();

            // Adicionar Transient e Repository PessoaContrato
            services.AddTransient<IPessoaContratoRepository, PessoaContratoRepository>();

            // Adicionar Transient e Repository NotaAluno
            services.AddTransient<INotaAlunoRepository, NotaAlunoRepository>();

            // Adicionar Transient e Repository Login
            services.AddTransient<ILoginRepository, LoginRepository>();


            // Adicionar o Cross

            services.AddCors(options =>
            {
              options.AddPolicy("AllowFromAll", 
              builder => builder  
              .WithMethods("GET", "POST")
              .AllowAnyOrigin()
              .AllowAnyHeader()); 
            });

            services.AddMvc();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowFromAll");
            app.UseMvc();
        }
    }

}

