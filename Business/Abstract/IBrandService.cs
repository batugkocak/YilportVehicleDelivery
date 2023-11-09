using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IBrandService
{
    IDataResult<List<Brand>> GetAll();
    IDataResult<Brand> GetById(int brandId);
    IResult Add(Brand brand);
    IResult Delete(int id);
    IResult Update(Brand brand);
    IDataResult<List<SelectBoxDto>> GetForSelectBox();
    IDataResult<List<Brand>> GetForTable();

}