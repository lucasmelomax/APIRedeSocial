
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Interfaces {
    public interface IPostsService {
        Task<PagedList<PostsResponseDTO>> GetAll(PagedParams pagedParams);
        Task<PagedList<PostsResponseDTO>> GetAllPostsByUser(int? id, PagedParams pagedParams);
        Task<PostsResponseDTO> GetById(int id);
        Task<PostsResponseDTO> Create(PostsDTO postsDTO);
        Task<PostsResponseDTO> Put(int id, PostsDTO postsDTO);
        Task Delete(int id);
    }
}
