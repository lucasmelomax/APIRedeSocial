using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Interfaces {
    public interface ILikesService {
        Task<PagedList<LikesDTO>> GetAll(PagedParams pagedParams);
        Task<LikesDTO> GetById(int id);
        Task<PagedList<LikesDTO>> GetAllByPost(int id, PagedParams pagedParams);
        Task<LikesDTO> Create(LikesDTO likesDTO);
        Task Delete(int id);
    }
}
