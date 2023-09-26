using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework;

public class EfVehicleOnTaskDal: EfEntityRepositoryBase<VehicleOnTask, VehicleDeliveryContext>, IVehicleOnTaskDal
{
    public List<VehicleOnTaskDetailDto> GetVehicleOnTaskDetailDal()
    {
        using VehicleDeliveryContext context = new VehicleDeliveryContext();
            var result = (from v in context.VehiclesOnTask
                join d in context.Departments on v.DepartmentId equals d.Id 
                join dr in context.Drivers on v.DriverId equals dr.Id
                join vehicle in context.Vehicles on v.VehicleId equals vehicle.Id 
                where v.IsDeleted != true
                select new VehicleOnTaskDetailDto{
                    VehicleOnTaskId = v.Id,
                    VehiclePlate = vehicle.Plate,
                    VehicleId = v.VehicleId,
                    DriverName = dr.Name,
                    DepartmentName = d.Name,
                    AuthorizedPerson = v.AuthorizedPerson,
                    GivenDate = v.GivenDate,
                    ReturnDate = v.ReturnDate
                    
                }).ToList();
       

            return result;
        
    }
}