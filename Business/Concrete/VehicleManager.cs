
using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class VehicleManager : IVehicleService
{
    private IVehicleDal _vehicleDal;
    public VehicleManager(IVehicleDal vehicleDal)
    {
        _vehicleDal = vehicleDal;
    }


    public IDataResult<List<VehicleDetailsDto>> GetAll()
    {
        var vehicles = _vehicleDal.GetAll();
        List<VehicleDetailsDto> detailsList = new List<VehicleDetailsDto>();
        foreach (var vehicle in vehicles)
        {
            detailsList.Add( new VehicleDetailsDto()
            {
                Plate = vehicle.Plate,
                Type  = vehicle.Type.IntToString<VehicleType>(),
                FuelType = vehicle.FuelType.IntToString<FuelType>(),
                Owner  = "Owner Test", // Owner Table
                Brand = "Brand Test", //Brand Table
                ModelName = vehicle.ModelName,
                ModelYear = vehicle.ModelYear,
                Color = vehicle.Color.IntToString<VehicleColor>(),
                Department = "Department Test", //Department Table
                Status =  vehicle.Status.IntToString<VehicleStatus>(),
                Note  = vehicle.Note
            });
        }
        return new SuccessDataResult<List<VehicleDetailsDto>>(detailsList);
    }

    public IDataResult<Vehicle> GetById(int vehicleId)
    {
        return new SuccessDataResult<Vehicle>(_vehicleDal.Get(v => v.Id == vehicleId));
    }

    //TODO: Should I Get IDs of Owner, Department from API? OR should I cast them somehow?
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