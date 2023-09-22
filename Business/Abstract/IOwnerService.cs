using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface IOwnerService
{
    IDataResult<Owner> GetById(int ownerId);
}