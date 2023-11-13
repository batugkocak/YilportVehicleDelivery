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
    public VehicleDetailDTO GetVehicleDetailsById(int vehicleId)
    {
        using VehicleDeliveryContext dtoContext = new VehicleDeliveryContext();
        var result = (from v in dtoContext.Vehicles
            join d in dtoContext.Departments on v.DepartmentId equals d.Id
            join b in dtoContext.Brands on v.BrandId equals b.Id
            join o in dtoContext.Owners on v.OwnerId equals o.Id
            where v.Id == vehicleId
            select new VehicleDetailDTO
            {
                Id = v.Id,
                Plate = v.Plate,
                Type  = v.Type.IntToString<VehicleType>(),
                FuelType = v.FuelType.IntToString<FuelType>(),
                Brand = b.Name, 
                Owner = o.Name,
                ModelName = v.ModelName,
                ModelYear = v.ModelYear,
                Color = v.Color.IntToString<VehicleColor>(),
                Department = d.Name, 
                Status =  v.Status.IntToString<VehicleStatus>(),
                Note  = v.Note
            }).SingleOrDefault();

        return result;
    }

    public List<SelectBoxDto> GetVehiclesForSelectBox()
    {
        using VehicleDeliveryContext context = new();
        var result =  (from vehicle in context.Vehicles 
            where vehicle.IsDeleted != true

            select new SelectBoxDto()
            {
                Id = vehicle.Id,
                SelectBoxValue = vehicle.Plate,
            }).ToList();

        return result;
    }

    List<VehicleForTableDTO> IVehicleDal.GetVehicleDetails()
    {
        using VehicleDeliveryContext dtoContext = new VehicleDeliveryContext();
        var result = (from v in dtoContext.Vehicles
            join d in dtoContext.Departments on v.DepartmentId equals d.Id
            join b in dtoContext.Brands on v.BrandId equals b.Id
            join o in dtoContext.Owners on v.OwnerId equals o.Id
            where v.IsDeleted != true
            orderby v.Created
            select new VehicleForTableDTO()
            {
                Id = v.Id,
                Plate = v.Plate,
                Type  = v.Type.IntToString<VehicleType>(),
                Brand = b.Name, 
                Owner = o.Name,
                ModelName = v.ModelName,
                Status =  v.Status.IntToString<VehicleStatus>(),
            }).ToList();

        return result;
    }
}
