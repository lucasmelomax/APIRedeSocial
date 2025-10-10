
using AutoMapper;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Services {
    public class PostsService : IPostsService {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public PostsService(IUnitOfWork uof, IMapper mapper) {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<PagedList<PostsResponseDTO>> GetAll(PagedParams pagedParams) {
            var query = _uof.PostsRepository.GetAll()
             .OrderBy(p => p.UsersId);

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<PostsResponseDTO>>(items);

            return new PagedList<PostsResponseDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );
        }

        public async Task<PagedList<PostsResponseDTO>> GetAllPostsByUser(int? id, PagedParams pagedParams) {

            if (id == null || id <= 0)
                throw new ArgumentException("O ID é obrigatório e deve ser maior que zero.");

            var query = _uof.PostsRepository.GetAll()
            .OrderBy(p => p.UsersId).Where(p => p.UsersId == id);

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<PostsResponseDTO>>(items);

            return new PagedList<PostsResponseDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );
        }
        public async Task<PostsResponseDTO> GetById(int id) {
            if (id < 0) throw new InvalidOperationException("Id invalido.");
            var post = await _uof.PostsRepository.GetById(id);
            if (post == null) throw new InvalidOperationException("Erro ao encontrar post.");
            return _mapper.Map<PostsResponseDTO>(post);
        }

        public async Task<PostsResponseDTO> Create(PostsDTO postsDTO) {
            if (postsDTO is null) throw new InvalidOperationException("Post invalido.");
            var post = _mapper.Map<Posts>(postsDTO);
            var criado = await _uof.PostsRepository.Create(post);
            var userPost =  await _uof.UserRepository.GetById(criado.UsersId);
            if (userPost == null) throw new ArgumentException("Este usuario nao existe.");
            await _uof.Commit();
            return _mapper.Map<PostsResponseDTO>(criado);
        }

        public async Task<PostsResponseDTO> Put(int id, PostsDTO postsDTO) {
            if (postsDTO is null)
                throw new InvalidOperationException("Post inválido.");

            var post = await _uof.PostsRepository.GetById(id);

            if (post is null)
                throw new InvalidOperationException("Usuário não encontrado.");

            if (post.PostsId != id) {
                throw new InvalidOperationException("id invalido!");
            }

            _mapper.Map(postsDTO, post);

            await _uof.PostsRepository.Update(id, post);
            await _uof.Commit();

            return _mapper.Map<PostsResponseDTO>(post);
        }
        public async Task Delete(int id) {
            if (id <= 0) throw new InvalidOperationException("O id precisa ser maior que 0.");
            var deletado = await _uof.PostsRepository.GetById(id);
            if (deletado is null) throw new InvalidOperationException("Post invalido.");
            if (deletado.PostsId != id) throw new InvalidOperationException("Id invalido.");
            await _uof.PostsRepository.DeleteById(id);
            await _uof.Commit();
        }

    }
}
