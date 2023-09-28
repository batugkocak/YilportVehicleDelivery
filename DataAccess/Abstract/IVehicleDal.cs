using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract;

public interface IVehicleDal : IEntityRepository<Vehicle>
{
    public List<VehicleForTableDto> GetVehicleDetails();
    public VehicleDetailDto GetVehicleDetailsById(int vehicleId);

}