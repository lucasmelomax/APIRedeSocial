using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using RedeSocial.Application.DTOs;
using RedeSocial.Application.Interfaces;
using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Models;

namespace RedeSocial.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Users> _repository;
        private readonly IUnitOfWork _uof;

        public UserService(IMapper mapper, IRepository<Users> repository, IUnitOfWork uof)
        {
            _mapper = mapper;
            _repository = repository;
            _uof = uof;
        }
        public async Task<IEnumerable<UsersDTO>> GetAll() {
            var clientes = await _repository.GetAll();
            if(clientes is null) {
                throw new InvalidOperationException("Erro ao encontrar usuarios.");
            }
            return _mapper.Map<IEnumerable<UsersDTO>>(clientes);
        }
        public async Task<UsersDTO> GetById(int id) {
            var cliente = await _repository.GetById(id);
            if (cliente is null) {
                throw new InvalidOperationException("Erro ao encontrar usuario.");
            }
            return _mapper.Map<UsersDTO>(cliente);
        }
        public async Task<UsersDTO> Create(UsersDTO usersDTO) {
            if (usersDTO is null) {
                throw new InvalidOperationException("Dados do usuario invalidos.");
            }
            var cliente = _mapper.Map<Users>(usersDTO);
            var clienteCriado = await _repository.Create(cliente);
            await _uof.Commit();
            return _mapper.Map<UsersDTO>(clienteCriado);
        }
        public async Task<UsersDTO> Update(int id, JsonPatchDocument<UpdateUserDTO> updateUserDTO) {
           
            if (updateUserDTO is null) {
                throw new InvalidOperationException("Dados do usuario invalidos.");
            }

            var user = await _repository.GetById(id);

            if (user is null) {
                throw new InvalidOperationException("Usuario não encontrado.");
            }

            var dtoPatch = _mapper.Map<UpdateUserDTO>(user);

            updateUserDTO.ApplyTo(dtoPatch);

            _mapper.Map(dtoPatch, user);

            var updated = await _repository.Update(user);
            await _uof.Commit();

            return _mapper.Map<UsersDTO>(updated);
        }
        public async Task DeleteById(int id) {
            await _repository.DeleteById(id);
            await _uof.Commit();
        }
    }
}
