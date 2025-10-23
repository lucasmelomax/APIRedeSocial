using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Services {
    public class LikesService : ILikesService {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public LikesService(IUnitOfWork uof, IMapper mapper) {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<PagedList<LikesDTO>> GetAll(PagedParams pagedParams) {
            var query =  _uof.LikesRepository.GetAll()
            .OrderBy(p => p.UsersId);

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<LikesDTO>>(items);

            return new PagedList<LikesDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );
        }

        public async Task<PagedList<LikesDTO>> GetAllByPost(int id, PagedParams pagedParams) {
            var query = _uof.LikesRepository.GetAll()
            .OrderBy(p => p.UsersId).Where(c => c.PostsId == id);

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<LikesDTO>>(items);

            return new PagedList<LikesDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );
        }

        public async Task<LikesDTO> GetById(int id) {
            if (id <= 0) throw new ArgumentException("O ID é obrigatório e deve ser maior que zero.");
            var likes = await _uof.LikesRepository.GetById(id);
            var likesDTO = _mapper.Map<LikesDTO>(likes);
            return likesDTO;
        }

        public async Task<LikesDTO> Create(LikesDTO likesDTO) {
            if (likesDTO == null) throw new ArgumentException("Comentario invalido.");
            var like = _mapper.Map<Likes>(likesDTO);
            var criado = await _uof.LikesRepository.Create(like);
            var likesPost = await _uof.UserRepository.GetById(criado.UsersId);
            if (likesPost == null) throw new ArgumentException("Este usuario nao existe.");
            await _uof.Commit();
            return _mapper.Map<LikesDTO>(criado);
        }

        public async Task Delete(int id) {
            if (id <= 0) throw new ArgumentException("O ID é obrigatório e deve ser maior que zero.");
            var deletado = await _uof.LikesRepository.GetById(id);
            if (deletado is null) throw new InvalidOperationException("Post invalido.");
            if (deletado.LikesId != id) throw new InvalidOperationException("Id invalido.");
            await _uof.LikesRepository.DeleteById(id);
            await _uof.Commit();
        }
    }
}
