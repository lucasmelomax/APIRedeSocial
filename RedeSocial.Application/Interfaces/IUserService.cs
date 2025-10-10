
using Microsoft.AspNetCore.JsonPatch;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Interfaces
{
    public interface IUserService {

        Task<PagedList<UserResponseDTO>> GetAll(PagedParams pagedParams);

        Task<PagedList<UserResponseDTO>> GetActiveUsers(string ativo, PagedParams pagedParams);
        Task<UserResponseDTO> GetById(int id);
        Task<UserResponseDTO> Create(UsersDTO usersDTO);
        Task<UserResponseDTO> Put(int id, UserPutDTO userDTO);
        Task<UserResponseDTO> Patch(int id, JsonPatchDocument<UsersDTO> userDTO);
        Task DeleteById(int id);
    }
}
