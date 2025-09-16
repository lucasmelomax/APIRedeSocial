using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Interfaces
{
    public interface IUserService {

        Task<PagedList<UsersDTO>> GetAll(int pageNumber, int pageSize);
        Task<UsersDTO> GetById(int id);
        Task<UsersDTO> Create(UsersDTO usersDTO);
        Task<UsersDTO> Update(int id, JsonPatchDocument<UpdateUserDTO> updateUserDTO);
        Task DeleteById(int id);
    }
}
