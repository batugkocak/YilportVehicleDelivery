
using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

//TODO: Change the plate XXX to XXX_ when deleted, because of "CheckIfCarExistByPlate()".
public class VehicleManager : IVehicleService
{
    private IVehicleDal _vehicleDal;
    // private IBrandService _brandService;
    // private IOwnerService _ownerService;
    // private IDepartmentService _departmentService;
    public VehicleManager(IVehicleDal vehicleDal)
    {
        _vehicleDal = vehicleDal;
    }
    
    public IDataResult<List<VehicleDetailDto>> GetAll()
    {
        return new SuccessDataResult<List<VehicleDetailDto>>(_vehicleDal.GetVehicleDetails());
    }

    public IDataResult<Vehicle> GetById(int vehicleId)
    {
        return new SuccessDataResult<Vehicle>(_vehicleDal.Get(v => v.Id == vehicleId));
    }
    
    public IResult Add(Vehicle vehicle)
    {
        var result = BusinessRules.Run(CheckIfCarExistByPlate(vehicle.Plate!));
        if (result != null)
        {
            return result;
        }
        _vehicleDal.Add(vehicle);
        return new SuccessResult(Messages.VehicleAdded);
    }

    public IResult Delete(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }

    public IResult Update(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }
    
    private IResult CheckIfCarExistByPlate(String plate)
    {
        var result = _vehicleDal.GetAll(u => u.Plate == plate).Any();
        if (result)
        {
            return new ErrorResult(Messages.VehicleAlreadyExist);
        }

        return new SuccessResult();
    }
}




// public IDataResult<List<VehicleDetailDto>> GetAll()
// {
//     var vehicles = _vehicleDal.GetAll();
//     List<VehicleDetailDto> detailsList = new List<VehicleDetailDto>();
//     foreach (var vehicle in vehicles)
//     {
//         if (!vehicle.IsDeleted)
//         {
//             var owner = _ownerService.GetById(vehicle.OwnerId).Data;
//             var brand = _brandService.GetById(vehicle.BrandId).Data;
//             var department = _departmentService.GetById(vehicle.DepartmentId).Data;
//             
//             detailsList.Add( new VehicleDetailDto()
//             {
//                 Plate = vehicle.Plate,
//                 Type  = vehicle.Type.IntToString<VehicleType>(),
//                 FuelType = vehicle.FuelType.IntToString<FuelType>(),
//                 Owner  = owner.Name + " " + owner.Surname, //Owner Table
//                 Brand = brand.Name, // Brand Table
//                 ModelName = vehicle.ModelName,
//                 ModelYear = vehicle.ModelYear,
//                 Color = vehicle.Color.IntToString<VehicleColor>(),
//                 Department = department.Name, // Departmant Table
//                 Status =  vehicle.Status.IntToString<VehicleStatus>(),
//                 Note  = vehicle.Note
//             });
//         }
//     }
//     return new SuccessDataResult<List<VehicleDetailDto>>(detailsList);
// }