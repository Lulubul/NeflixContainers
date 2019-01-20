using Autofac;
using MovieMetadata.API.Application;
using MovieMetadata.Infrastructure;

namespace MovieMetadata.API.Infrastructure.AutofacModules
{
    public class ApplicationModule: Autofac.Module
    {
        private readonly string _connectionString;

        public ApplicationModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MoviesQueries>()
                .As<IMoviesQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new MovieRepository(_connectionString))
                .As<IMovieRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
