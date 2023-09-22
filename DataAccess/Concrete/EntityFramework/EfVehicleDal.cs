using Business.Constants;
using Core.DataAccess.EntityFramework;
using Core.Extensions;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework;

public class EfVehicleDal : EfEntityRepositoryBase<Vehicle, VehicleDeliveryContext>, IVehicleDal
{
    public List<VehicleDetailDto> GetVehicleDetails()
    {
        using VehicleDeliveryContext context = new VehicleDeliveryContext();
        var result = (from v in context.Vehicles
            join d in context.Departments on v.DepartmentId equals d.Id
            join b in context.Brands on v.BrandId equals b.Id
            join o in context.Owners on v.OwnerId equals o.Id
            select new VehicleDetailDto
            {
                Plate = v.Plate,
                Type  = v.Type.IntToString<VehicleType>(),
                FuelType = v.FuelType.IntToString<FuelType>(),
                Owner  = o.Name + " " + o.Surname, //Owner Table
                Brand = b.Name, // Brand Table
                ModelName = v.ModelName,
                ModelYear = v.ModelYear,
                Color = v.Color.IntToString<VehicleColor>(),
                Department = d.Name, // Departmant Table
                Status =  v.Status.IntToString<VehicleStatus>(),
                Note  = v.Note
            }).ToList();

        return result;
    }
}