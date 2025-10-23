using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Interfaces {
    public interface ICommentsService {

        Task<PagedList<CommentsDTO>> GetAll(PagedParams pagedParams);
        Task<CommentsDTO> GetById(int id);
        Task<PagedList<CommentsDTO>> GetAllByPost(int id, PagedParams pagedParams);
        Task<CommentsDTO> Create(CommentsDTO commentsDTO);
        Task Delete(int id);
    }
}
