using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module 
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<VehicleManager>().As<IVehicleService>();
    builder.RegisterType<EfVehicleDal>().As<IVehicleDal>();

    builder.RegisterType<VehicleOnTaskManager>().As<IVehicleOnTaskService>();
    builder.RegisterType<EfVehicleOnTaskDal>().As<IVehicleOnTaskDal>();

    builder.RegisterType<BrandManager>().As<IBrandService>();
    builder.RegisterType<EfBrandDal>().As<IBrandDal>();

    builder.RegisterType<DriverManager>().As<IDriverService>();
    builder.RegisterType<EfDriverDal>().As<IDriverDal>();

    builder.RegisterType<TaskManager>().As<ITaskService>();
    builder.RegisterType<EfTaskDal>().As<ITaskDal>();

    builder.RegisterType<DepartmentManager>().As<IDepartmentService>();
    builder.RegisterType<EfDepartmentDal>().As<IDepartmentDal>();

    builder.RegisterType<OwnerManager>().As<IOwnerService>();
    builder.RegisterType<EfOwnerDal>().As<IOwnerDal>();
    
    
    var assembly = System.Reflection.Assembly.GetExecutingAssembly();

    builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
      .EnableInterfaceInterceptors(new ProxyGenerationOptions()
      {
        Selector = new AspectInterceptorSelector()
      }).SingleInstance();

  }
}

// builder.Services.AddSingleton<IVehicleService, VehicleManager>();
// builder.Services.AddSingleton<IVehicleDal, EfVehicleDal>();
//
// builder.Services.AddSingleton<IVehicleOnTaskService, VehicleOnTaskManager>();
// builder.Services.AddSingleton<IVehicleOnTaskDal, EfVehicleOnTaskDal>();
//
// builder.Services.AddSingleton<IBrandService, BrandManager>();
// builder.Services.AddSingleton<IBrandDal, EfBrandDal>();
//
// builder.Services.AddSingleton<IDriverService, DriverManager>();
// builder.Services.AddSingleton<IDriverDal, EfDriverDal>();
//
// builder.Services.AddSingleton<ITaskService, TaskManager>();
// builder.Services.AddSingleton<ITaskDal, EfTaskDal>();
//
// builder.Services.AddSingleton<IDepartmentService, DepartmentManager>();
// builder.Services.AddSingleton<IDepartmentDal, EfDepartmentDal>();
//
// builder.Services.AddSingleton<IOwnerService, OwnerManager>();
// builder.Services.AddSingleton<IOwnerDal, EfOwnerDal>();
