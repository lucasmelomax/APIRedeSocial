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

        Task<PagedList<UserResponseDTO>> GetAll(PagedParams pagedParams);
        Task<UserResponseDTO> GetById(int id);
        Task<UserResponseDTO> Create(UsersDTO usersDTO);
        Task<UserResponseDTO> Put(int id, UsersDTO userDTO);
        Task<UserResponseDTO> Patch(int id, JsonPatchDocument<UsersDTO> userDTO);
        Task DeleteById(int id);
    }
}
