using Autofac;
using AutoMapper;
using FriendsOrganizer.UI.Mapper;

namespace FriendsOrganizer.UI.DI
{
    public class AppMapperModule : Module
    {      
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppAutoMapperProfile());

            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>()
                .CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
