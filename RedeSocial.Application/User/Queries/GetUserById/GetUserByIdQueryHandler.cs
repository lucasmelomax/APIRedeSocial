using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponseDTO>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            if (request.id <= 0)
            {
                throw new InvalidOperationException("Erro ao encontrar usuario.");
            }
            var user = await _unitOfWork.UserRepository.GetById(request.id, ct);
            if (user is null)
            {
                throw new InvalidOperationException("Erro ao encontrar usuario.");
            }
            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}
