using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;



var  MyAllowSpecificOrigins = "_myFrontEnd";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("*").AllowAnyHeader().AllowAnyOrigin();
        });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IVehicleService, VehicleManager>();
builder.Services.AddSingleton<IVehicleDal, EfVehicleDal>();

builder.Services.AddSingleton<IVehicleOnTaskService, VehicleOnTaskManager>();
builder.Services.AddSingleton<IVehicleOnTaskDal, EfVehicleOnTaskDal>();

builder.Services.AddSingleton<IBrandService, BrandManager>();
builder.Services.AddSingleton<IBrandDal, EfBrandDal>();

builder.Services.AddSingleton<IDriverService, DriverManager>();
builder.Services.AddSingleton<IDriverDal, EfDriverDal>();

builder.Services.AddSingleton<ITaskService, TaskManager>();
builder.Services.AddSingleton<ITaskDal, EfTaskDal>();

builder.Services.AddSingleton<IDepartmentService, DepartmentManager>();
builder.Services.AddSingleton<IDepartmentDal, EfDepartmentDal>();

builder.Services.AddSingleton<IOwnerService, OwnerManager>();
builder.Services.AddSingleton<IOwnerDal, EfOwnerDal>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();