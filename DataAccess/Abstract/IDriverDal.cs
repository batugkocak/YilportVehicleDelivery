using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Driver;

namespace DataAccess.Abstract;

public interface IDriverDal: IEntityRepository<Driver>
{
    public List<DriverDto> GetForTable();

    public List<SelectBoxDto> GetDriversForSelectBox();
}