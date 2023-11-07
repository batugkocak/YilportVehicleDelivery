using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract;

public interface IOwnerDal: IEntityRepository<Owner>
{
    public List<SelectBoxDto> GetOwnersForSelectBox();
}