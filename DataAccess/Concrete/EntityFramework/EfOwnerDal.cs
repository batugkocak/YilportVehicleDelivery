using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework;

public class EfOwnerDal: EfEntityRepositoryBase<Owner, VehicleDeliveryContext>, IOwnerDal
{
    public List<SelectBoxDto> GetOwnersForSelectBox()
    {
        using VehicleDeliveryContext context = new();
        var result =  (from owner in context.Owners
            where owner.IsDeleted != true
            select new SelectBoxDto()
            {
                Id = owner.Id,
                SelectBoxValue = owner.Name
            }).ToList();

        return result;
    }

    public List<Owner> GetForTable()
    {
        using VehicleDeliveryContext context = new();
        var result =  (from owner in context.Owners
            where owner.IsDeleted != true
            select new Owner()
            {
                Id = owner.Id,
                Name = owner.Name
            }).ToList();

        return result;
    }
}