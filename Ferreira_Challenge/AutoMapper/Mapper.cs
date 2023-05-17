using AutoMapper;
using Models;
using Models.DTOs.Input;
using Models.DTOs.Output;

namespace Ferreira_Challenge.AutoMapper
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>(); // Mapping from User to UserDTO
            CreateMap<UserDTO, User>(); // Mapping from UserDTO to User

            CreateMap<CreateUserDTO, User>(); // Mapping from CreateUserDTO to User
            CreateMap<User, CreateUserDTO>(); // Mapping from User to CreateUserDTO

            CreateMap<UpdateUserDTO, User>(); // Mapping from UpdateUserDTO to User
            CreateMap<User, UpdateUserDTO>(); // Mapping from User to UpdateUserDTO
        }
    }
}
