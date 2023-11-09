using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IOwnerService
{
    IDataResult<List<Owner>> GetAll();
    IDataResult<Owner> GetById(int brandId);
    IResult Add(Owner brand);
    IResult Delete(int id);
    IResult Update(Owner brand);
    IDataResult<List<SelectBoxDto>> GetForSelectBox();
    IDataResult<List<Owner>> GetForTable();
    
}