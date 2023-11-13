using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Brand;

namespace DataAccess.Abstract;

public interface IBrandDal : IEntityRepository<Brand>
{
    public List<SelectBoxDto> GetBrandsForSelectBox();
    public List<BrandForTableDto> GetForTable();
    
}