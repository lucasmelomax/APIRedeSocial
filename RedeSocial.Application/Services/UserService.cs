
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;
using RedeSocial.Domain.Pagination;

namespace RedeSocial.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;

        public UserService(IMapper mapper, IUnitOfWork uof)
        {
            _mapper = mapper;
            _uof = uof;
        }
        public async Task<PagedList<UserResponseDTO>> GetAll(PagedParams pagedParams) {
            var query = _uof.UserRepository.GetAll()
                        .OrderBy(u => u.UsersId);

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<UserResponseDTO>>(items);

            return new PagedList<UserResponseDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );
        }

        public async Task<UserResponseDTO> GetById(int id) {
            var cliente = await _uof.UserRepository.GetById(id);
            if (cliente is null) {
                throw new InvalidOperationException("Erro ao encontrar usuario.");
            }
            return _mapper.Map<UserResponseDTO>(cliente);
        }
        public async Task<PagedList<UserResponseDTO>> GetActiveUsers(string ativo, PagedParams pagedParams) {

            IQueryable<Users?> query;

            if (ativo == "ativos") {
                query = _uof.UserRepository.GetAll().OrderBy(u => u.UsersId).Where(u => u.Active == true);
                if (!query.Any()) throw new InvalidOperationException("Nao tem usuarios ativos");
            }
            else if (ativo == "inativos") {
                query = _uof.UserRepository.GetAll().OrderBy(u => u.UsersId).Where(u => u.Active == false);
                if (!query.Any()) throw new InvalidOperationException("Nao tem usuarios ativos");
            }
            else {
                throw new InvalidOperationException("Os valores passados devem ser 'ativos' ou 'inativos'.");
            }

            var totalCount = query.Count();

            var items = query
                .Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize)
                .ToList();

            var itemsDTO = _mapper.Map<List<UserResponseDTO>>(items);

            return new PagedList<UserResponseDTO>(
                itemsDTO,
                totalCount,
                pagedParams.PageNumber,
                pagedParams.PageSize
            );

        }
        public async Task<UserResponseDTO> Create(UsersDTO usersDTO) {
            if (usersDTO is null) {
                throw new InvalidOperationException("Dados do usuario invalidos.");
            }
            var cliente = _mapper.Map<Users>(usersDTO);
            var clienteCriado = await _uof.UserRepository.Create(cliente);
            await _uof.Commit();
            return _mapper.Map<UserResponseDTO>(clienteCriado);
        }
        public async Task<UserResponseDTO> Put(int id, UserPutDTO userDTO) {
            if (userDTO is null)
                throw new InvalidOperationException("Dados do usuário inválidos.");

            var user = await _uof.UserRepository.GetById(id);
            if (user is null)
                throw new InvalidOperationException("Usuário não encontrado.");

            if (user.UsersId != id) {
                throw new InvalidOperationException("id invalido!");
            }

            _mapper.Map(userDTO, user);

            await _uof.UserRepository.Update(id, user);
            await _uof.Commit();

            return _mapper.Map<UserResponseDTO>(user);
        }
        public async Task<UserResponseDTO> Patch(int id, JsonPatchDocument<UsersDTO> userDTO) {
            if (userDTO is null) {
                throw new InvalidOperationException("Dados do usuario invalidos.");
            }

            var user = await _uof.UserRepository.GetById(id);

            if (user is null) {
                throw new InvalidOperationException("Usuario não encontrado.");
            }

            var DTO = _mapper.Map<UsersDTO>(user);

            userDTO.ApplyTo(DTO);

            _mapper.Map(DTO, user);

            var updated = await _uof.UserRepository.Update(id, user);
            await _uof.Commit();

            return _mapper.Map<UserResponseDTO>(updated);
        }
        public async Task DeleteById(int id) {
            var cliente = await _uof.UserRepository.GetById(id);
            if (cliente is null) {
                throw new InvalidOperationException("Erro ao encontrar usuario.");
            }
            if(cliente.UsersId != id) {
                throw new InvalidOperationException("id invalido!");
            }
            var UserPosts = _uof.PostsRepository.GetAll().Where(p => p.UsersId == id);
            foreach(var i in UserPosts) {
                await _uof.PostsRepository.DeleteById(i.PostsId);
            }
            await _uof.UserRepository.DeleteById(id);
            await _uof.Commit();
        }

    }
}
