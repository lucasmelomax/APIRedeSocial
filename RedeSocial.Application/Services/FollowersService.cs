
using AutoMapper;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Services {
    public class FollowersService : IFollowersService {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public FollowersService(IUnitOfWork uof, IMapper mapper) {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<PagedList<FollowersDTO>> GetAll(PagedParams pagedParams) {
            var query = _uof.FollowersRepository.GetAll()
            .OrderBy(p => p.UsersId);

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<FollowersDTO>>(items);

            return new PagedList<FollowersDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );
        }

        public async Task<PagedList<FollowersDTO>> GetAllByUser(int id, PagedParams pagedParams) {
            var query = _uof.FollowersRepository.GetAll()
            .OrderBy(p => p.UsersId).Where(c => c.UsersId == id);

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<FollowersDTO>>(items);

            return new PagedList<FollowersDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );
        }

        public async Task<FollowersDTO> GetById(int id) {
            if (id <= 0) throw new ArgumentException("O ID é obrigatório e deve ser maior que zero.");
            var follower = await _uof.FollowersRepository.GetById(id);
            var followerDTO = _mapper.Map<FollowersDTO>(follower);
            return followerDTO;
        }

        public async Task<FollowersDTO> Create(FollowersDTO followersDTO) {
            if (followersDTO == null) throw new ArgumentException("Comentario invalido.");
            var follower = _mapper.Map<Followers>(followersDTO);
            var criado = await _uof.FollowersRepository.Create(follower);
            var followerUser = await _uof.UserRepository.GetById(criado.UsersId);
            if (followerUser == null) throw new ArgumentException("Este usuario nao existe.");
            await _uof.Commit();
            return _mapper.Map<FollowersDTO>(criado);
        }
    }
}
