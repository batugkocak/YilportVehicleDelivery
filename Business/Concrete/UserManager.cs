using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs.User;

namespace Business.Concrete;


    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public User Add(User user)
        {
            return _userDal.AddWithReturn(user);
        }   

        public User GetByUsername(string username)
        {
            return _userDal.Get(u => u.Username == username && u.IsDeleted != true);
        }

        public IDataResult<List<UserForList>> GetForList()
        {
            var result = _userDal.GetForList();
            return new SuccessDataResult<List<UserForList>>(result);
        }

        public IDataResult<User> GetById(int userId)
        {
            var result = _userDal.Get(u => u.Id == userId);
            return new SuccessDataResult<User>(result);

        }

        public IResult Delete(int id)
        {
            var deletedUser = _userDal.Get(u => u.Id == id);
            deletedUser.IsDeleted = true;
            _userDal.Update(deletedUser);
            return new SuccessResult(Messages.UserDeleted);

        }
    }
