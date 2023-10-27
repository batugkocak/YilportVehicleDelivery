using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract;

public interface IVehicleDal : IEntityRepository<Vehicle>
{
    public List<VehicleForTableDTO> GetVehicleDetails();
    public VehicleDetailDTO GetVehicleDetailsById(int vehicleId);

}