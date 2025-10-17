
using AutoMapper;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Services {
    public class PhostPhotosService : IPhostPhotosService {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;

        public PhostPhotosService(IMapper mapper, IUnitOfWork uof) {
            _mapper = mapper;
            _uof = uof;
        }

        public async Task<PagedList<PostsPhotosDTO>> GetAll(PagedParams pagedParams) {
            var query = _uof.PostsPhotosRepository.GetAll()
            .OrderBy(u => u.PostsPhotosId);

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<PostsPhotosDTO>>(items);

            return new PagedList<PostsPhotosDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );
        }
        public async Task<PostsPhotosDTO> GetById(int id) {
            var post = await _uof.PostsPhotosRepository.GetById(id);
            if (post == null) throw new InvalidOperationException("Post Photo invalido.");
            var postDTO = _mapper.Map<PostsPhotosDTO>(post);
            return postDTO;
        }
        public async Task<PagedList<PostsPhotosDTO>> GetByPost(int id, PagedParams pagedParams) {
            if (id <= 0) throw new InvalidOperationException("Id invalido.");

            var query = _uof.PostsPhotosRepository.GetAll()
                .OrderBy(u => u.PostsPhotosId).Where(p => p.PostsId == id);

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<PostsPhotosDTO>>(items);

            return new PagedList<PostsPhotosDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );
        }
        public async Task<PostsPhotosDTO> Create(PostsPhotosDTO postsPhotosDTO) {
            if (postsPhotosDTO == null) throw new InvalidOperationException("Post Photo invalido.");
            var criadoMapper = _mapper.Map<PostsPhotos>(postsPhotosDTO);
            var criado = await _uof.PostsPhotosRepository.Create(criadoMapper);
            await _uof.Commit();
            var criadoDTO = _mapper.Map<PostsPhotosDTO>(criado);
            return criadoDTO;
        }
        public async Task DeleteById(int id) {
            var deletado = await _uof.PostsPhotosRepository.GetById(id);
            if (deletado is not null && deletado.PostsPhotosId != id) throw new InvalidOperationException("Post Photo invalido.");
            await _uof.PostsPhotosRepository.DeleteById(id);
            await _uof.Commit();
        }

    }
}
