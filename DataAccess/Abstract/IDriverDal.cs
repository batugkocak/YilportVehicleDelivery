using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.Driver;

namespace DataAccess.Abstract;

public interface IDriverDal: IEntityRepository<Driver>
{
    public List<DriverDto> GetDriverDetails();
}