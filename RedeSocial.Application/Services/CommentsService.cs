
using AutoMapper;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Services {
    public class CommentsService : ICommentsService {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CommentsService(IUnitOfWork uof, IMapper mapper) {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<PagedList<CommentsDTO>> GetAll(PagedParams pagedParams) {
            var query = _uof.CommentsRepository.GetAll()
            .OrderBy(p => p.UsersId);

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<CommentsDTO>>(items);

            return new PagedList<CommentsDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );
        }

        public async Task<PagedList<CommentsDTO>> GetAllByPost(int id, PagedParams pagedParams) {
            var query = _uof.CommentsRepository.GetAll()
            .OrderBy(p => p.UsersId).Where(c => c.PostsId == id);

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<CommentsDTO>>(items);

            return new PagedList<CommentsDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );
        }

        public async Task<CommentsDTO> GetById(int id) {
            if (id <= 0) throw new ArgumentException("O ID é obrigatório e deve ser maior que zero.");
            var comment = await _uof.CommentsRepository.GetById(id);
            var commentDTO = _mapper.Map<CommentsDTO>(comment);
            return commentDTO;
        }

        public async Task<CommentsDTO> Create(CommentsDTO commentsDTO) {
            if (commentsDTO == null) throw new ArgumentException("Comentario invalido.");
            var comment = _mapper.Map<Comments>(commentsDTO);
            var criado = await _uof.CommentsRepository.Create(comment);
            var commentPost = await _uof.UserRepository.GetById(criado.UsersId);
            if (commentPost == null) throw new ArgumentException("Este usuario nao existe.");
            await _uof.Commit();
            return _mapper.Map<CommentsDTO>(criado);
        }

        public async Task Delete(int id) {
            if (id <= 0) throw new ArgumentException("O ID é obrigatório e deve ser maior que zero.");
            var deletado = await _uof.CommentsRepository.GetById(id);
            if (deletado is null) throw new InvalidOperationException("Post invalido.");
            if (deletado.CommentsId != id) throw new InvalidOperationException("Id invalido.");
            await _uof.CommentsRepository.DeleteById(id);
            await _uof.Commit();
        }


    }
}
