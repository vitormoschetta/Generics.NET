using Servico;

namespace Repositorio
{
    public interface IRepository<T>
    {
        List<T> Lista { get; set; }

        IList<T> GetAll();
        void Add(T item);
    }

    public class Repository<T> : IRepository<T>
    {
        public List<T> Lista { get; set; } = new List<T>();

        public IList<T> GetAll() => Lista;

        public void Add(T item) => Lista.Add(item);
    }

    public class User : UserModelBase
    {
        public string Phone { get; set; } = string.Empty;
    }

    public class RegisterUserRequest : RegisterRequestBase
    {
        public string Phone { get; set; } = string.Empty;
    }

    public class UserRepository : Repository<User>
    {
    }

    public class MapperRegisterRequestToUserModel : IMapperRegisterRequestToUserModel<User, RegisterUserRequest>
    {
        public User Map(RegisterUserRequest request)
        {
            return new User
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                Phone = request.Phone
            };
        }
    }
}