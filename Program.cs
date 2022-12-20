using System.Text.Json;
using Repositorio;
using Servico;

var repositorio = new UserRepository();

var mapper = new MapperRegisterRequestToUserModel();

var servico = new AuthService<User, RegisterUserRequest>(repositorio, mapper);

var registerUserRequest = new RegisterUserRequest
{
    Username = "vitor",
    Password = "123456",
    Email = "vitor@gmail.com",
    Phone = "99222-1727"
};

servico.Register(registerUserRequest);

var users = repositorio.GetAll();

foreach (var item in users)
{
    Console.WriteLine(JsonSerializer.Serialize(item));
}

var login = new LoginRequest()
{
    Username = "vitor",
    Password = "123456"
};

var response = servico.Login(login);

Console.WriteLine(response.Token);
