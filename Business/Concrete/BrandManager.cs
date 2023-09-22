using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class BrandManager : IBrandService
{
    private IBrandDal _brandDal;
    public BrandManager(IBrandDal brandDal)
    {
        _brandDal = brandDal;
    }
    
    public IDataResult<Brand> GetById(int brandId)
    {
        Brand brand = _brandDal.Get(b => b.Id == brandId);
        //return new SuccessDataResult<Vehicle>(_vehicleDal.Get(v => v.Id == vehicleId));
        return new SuccessDataResult<Brand>(brand);
    }
}