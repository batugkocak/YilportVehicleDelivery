using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Driver;

namespace DataAccess.Concrete.EntityFramework;

public class EfDriverDal: EfEntityRepositoryBase<Driver, VehicleDeliveryContext>, IDriverDal
{
    public List<DriverDto> GetForTable()
    {
            using VehicleDeliveryContext context = new VehicleDeliveryContext();
            var result = (from driver in context.Drivers 
                join department in context.Departments on driver.DepartmentId equals department.Id
                orderby driver.Created descending 
                where driver.IsDeleted != true
                select new DriverDto()
                {
                    Id = driver.Id,
                    Name = driver.Name,
                    Surname = driver.Surname,
                    DepartmentName = department.Name,
                    PhoneNumber = driver.PhoneNumber,
                    Mission = driver.Mission,
                    
                }).ToList();

            return result;
    }
    

    public List<SelectBoxDto> GetDriversForSelectBox()
    { using VehicleDeliveryContext context = new();
       var result =  (from driver in context.Drivers
           where driver.IsDeleted != true
            select new SelectBoxDto()
            {
                Id = driver.Id,
                SelectBoxValue = $"{driver.Name} {driver.Surname} - {driver.Mission}",
            }).ToList();

       return result;
    }
}