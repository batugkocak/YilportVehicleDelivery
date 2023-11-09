using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;

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
            return _userDal.Get(u => u.Username == username);
        }
    }
