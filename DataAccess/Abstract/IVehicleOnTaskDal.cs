using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract;

public interface IVehicleOnTaskDal : IEntityRepository<VehicleOnTask>
{
    public List<VehicleOnTaskDetailDto> GetVehicleOnTaskDetail();
    public VehicleOnTaskDetailDto GetVehicleOnTaskDetailById(int id);

    public List<VehicleOnTaskForTableDto> GetVehicleOnTaskForTable();

}