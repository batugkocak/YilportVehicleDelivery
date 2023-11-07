using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface IOwnerService
{
    IDataResult<List<Owner>> GetAll();
    IDataResult<Owner> GetById(int brandId);
    IResult Add(Owner brand);
    IResult Delete(Owner brand);
    IResult Update(Owner brand);
    
}