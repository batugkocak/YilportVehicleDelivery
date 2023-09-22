using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IBrandService
{
    IDataResult<Brand> GetById(int brandId);
}