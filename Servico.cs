using Repositorio;

namespace Servico
{
    public class UserModelBase
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // não faz sentido passar isso no request de registro de usuário
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; } = false; // não faz sentido passar isso no request de registro de usuário
    }

    public class RegisterRequestBase
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }

    public interface IMapperRegisterRequestToUserModel<TUserModel, TRegisterRequest>
                            where TUserModel : UserModelBase, new()
                            where TRegisterRequest : RegisterRequestBase, new()
    {
        TUserModel Map(TRegisterRequest request);
    }

    public class AuthService<TUserModel, TRegisterRequest>
                            where TUserModel : UserModelBase, new()
                            where TRegisterRequest : RegisterRequestBase, new()
    {
        private readonly IRepository<TUserModel> _repository;
        private readonly IMapperRegisterRequestToUserModel<TUserModel, TRegisterRequest> _mapper;

        public AuthService(IRepository<TUserModel> repository, IMapperRegisterRequestToUserModel<TUserModel, TRegisterRequest> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual void Register(TRegisterRequest request)
        {
            var user = _mapper.Map(request);

            _repository.Add(user);
        }

        public virtual LoginResponse Login(LoginRequest request)
        {
            var user = _repository.GetAll().FirstOrDefault(x => x.Username == request.Username && x.Password == request.Password);

            if (user == null)
                throw new Exception("Invalid credentials");

            return new LoginResponse()
            {
                Token = Guid.NewGuid().ToString()
            };
        }

    }
}