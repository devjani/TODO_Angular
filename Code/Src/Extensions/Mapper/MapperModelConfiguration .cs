using AutoMapper;
using System;
using TODO.Model.DTO;
using TODO.Model.Entities;

namespace Mapper
{
    public class MapperModelConfiguration : Profile
    {
        public MapperModelConfiguration()
        {
            #region User
            CreateMap<UsersDTO, Users>();
            CreateMap<Users, UsersDTO>();
            #endregion
            #region Todo
            CreateMap<TodoDTO, Todos>();
            CreateMap<Todos, TodoDTO>();
            #endregion
        }
    }
}
