using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using SportsStore.Domain.Maps;

namespace NHibernateConfig
{
    public class NHibernateConfiguration
    {
        public static ISessionFactory CreateFactory()
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(d =>
            {
                d.Dialect<NHibernate.Dialect.PostgreSQLDialect>();
                d.Driver<NHibernate.Driver.NpgsqlDriver>();
                d.ConnectionString = "Server=localhost;Port=5432;Database=sports_store;User ID=postgres;Password=123";
                d.LogSqlInConsole = true;
            });

            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<ProductMap>();

            configuration.AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());

            var sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory;
        }
    }
}
