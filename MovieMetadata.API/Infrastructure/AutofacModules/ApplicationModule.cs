using Autofac;
using MovieMetadata.API.Application;
using MovieMetadata.Infrastructure;

namespace MovieMetadata.API.Infrastructure.AutofacModules
{
    public class ApplicationModule: Autofac.Module
    {
        private readonly string _connectionString;
        private readonly string _blobConnectionString;

        public ApplicationModule(string connectionString, string blobConnectionString)
        {
            _connectionString = connectionString;
            _blobConnectionString = blobConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MoviesQueries>()
                .As<IMoviesQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new MovieRepository(_connectionString, _blobConnectionString))
                .As<IMovieRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
