using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Interfaces {
    public interface IFollowersService {
        Task<PagedList<FollowersDTO>> GetAll(PagedParams pagedParams);
        Task<FollowersDTO> GetById(int id);
        Task<PagedList<FollowersDTO>> GetAllByUser(int id, PagedParams pagedParams);
        Task<FollowersDTO> Create(FollowersDTO followersDTO);
    }
}
