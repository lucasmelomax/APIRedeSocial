
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Interfaces {
    public interface IPhostPhotosService {
        Task<PagedList<PostsPhotosDTO>> GetAll(PagedParams pagedParams);
        Task<PostsPhotosDTO> GetById(int id);
        Task<PagedList<PostsPhotosDTO>> GetByPost(int id, PagedParams pagedParams);
        Task<PostsPhotosDTO> Create(PostsPhotosDTO postsPhotosDTO);
        Task DeleteById(int id);
    }
}
