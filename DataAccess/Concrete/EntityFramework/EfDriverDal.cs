using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs.Driver;

namespace DataAccess.Concrete.EntityFramework;

public class EfDriverDal: EfEntityRepositoryBase<Driver, VehicleDeliveryContext>, IDriverDal
{
    public List<DriverDto> GetDriverDetails()
    {
            using VehicleDeliveryContext context = new VehicleDeliveryContext();
            var result = (from driver in context.Drivers 
                join department in context.Departments on driver.DepartmentId equals department.Id
                select new DriverDto()
                {
                    Name = driver.Name,
                    Surname = driver.Surname,
                    DepartmentName = department.Name,
                    PhoneNumber = driver.PhoneNumber,
                    Mission = driver.Mission,
                    
                }).ToList();

            return result;
    }
}