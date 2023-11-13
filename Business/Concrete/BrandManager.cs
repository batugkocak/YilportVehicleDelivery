using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Brand;

namespace Business.Concrete;

public class BrandManager : IBrandService
{
    private IBrandDal _brandDal;
    public BrandManager(IBrandDal brandDal)
    {
        _brandDal = brandDal;
    }

    public IDataResult<List<Brand>> GetAll()
    {
        return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandsListed);
    }

    public IDataResult<Brand> GetById(int brandId)
    {
        return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == brandId), Messages.BrandListed);
    }

    public IResult Add(Brand brand)
    {
        if (BrandExists(brand.Name).Success)
        {
            return new ErrorResult(Messages.BrandAlreadyExists);
        }
        _brandDal.Add(brand);
        return new SuccessResult(Messages.BrandAdded);
    }

    public IResult Delete(int id)
    {
        var deletedBrand= _brandDal.Get(o=> o.Id == id);
        deletedBrand.IsDeleted = true;
        _brandDal.Update(deletedBrand);
        return new SuccessResult(Messages.BrandDeleted);
    }

    public IResult Update(Brand brand)
    {
        if (BrandExists(brand.Name).Success)
        {
            return new ErrorResult(Messages.BrandAlreadyExists);
        }
        _brandDal.Update(brand);
        return new SuccessResult(Messages.BrandUpdated);
    }

    public IDataResult<List<SelectBoxDto>> GetForSelectBox()
    {
        return new SuccessDataResult<List<SelectBoxDto>>(_brandDal.GetBrandsForSelectBox(), Messages.BrandsListed);


    }

    public IDataResult<List<BrandForTableDto>> GetForTable()
    {
        return new SuccessDataResult<List<BrandForTableDto>>(_brandDal.GetForTable(), Messages.BrandsListed);

    }
    
    public IResult BrandExists(string name)
    {
        if (_brandDal.GetAll(b => b.Name == name &&  b.IsDeleted != true).Any())
        {
            return new SuccessResult(Messages.BrandAlreadyExists);
        }
        return new ErrorResult();
    }
}