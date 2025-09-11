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

namespace RedeSocial.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Users> _repository;

        public UserService(IMapper mapper, IRepository<Users> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<UsersDTO>> GetAll()
        {
            var clientes = await _repository.GetAll();
            return _mapper.Map<IEnumerable<UsersDTO>>(clientes);
        }
        public async Task<UsersDTO?> GetById(int id)
        {
            var cliente = await _repository.GetById(id);
            return _mapper.Map<UsersDTO>(cliente);
        }
        public async Task<UsersDTO?> Create(UsersDTO usersDTO)
        {
            var cliente = _mapper.Map<Users>(usersDTO);
            var clienteCriado = await _repository.Create(cliente);
            return _mapper.Map<UsersDTO>(clienteCriado);
        }
        public async Task<UsersDTO?> Update(UsersDTO usersDTO)
        {
            var cliente = _mapper.Map<Users>(usersDTO);
            var clienteAtualizado = await _repository.Update(cliente);
            return _mapper.Map<UsersDTO>(clienteAtualizado);
        }
        public async Task DeleteById(int id)
        {
            await _repository.DeleteById(id);
        }
    }
}
