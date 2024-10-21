using Autofac;
using InterviewTask.API.Feature.Authentication;
using InterviewTask.API.Persistence;
using InterviewTask.API.Repository;
using InterviewTask.API.Shared;

namespace InterviewTask.API
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType < ApplicationDbContext>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
           builder.RegisterType(typeof(CanceletionState)).AsImplementedInterfaces().InstancePerLifetimeScope();
           builder.RegisterType(typeof(GenerateTokenCommandHandler)).AsImplementedInterfaces().InstancePerLifetimeScope();


            base.Load(builder);
        }
    }
}
