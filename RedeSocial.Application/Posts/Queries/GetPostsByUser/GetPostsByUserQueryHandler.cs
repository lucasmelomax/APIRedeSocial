using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Posts.Queries.GetPostsByUser
{
    public class GetPostsByUserQueryHandler : IRequestHandler<GetPostsByUserQuery, IEnumerable<PostsResponseDTO>>
    {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public GetPostsByUserQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostsResponseDTO>> Handle(GetPostsByUserQuery request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
                throw new ArgumentException("O ID é obrigatório e deve ser maior que zero.");

            var query = _uof.PostsRepository.GetAll()
            .OrderBy(p => p.UsersId).Where(p => p.UsersId == request.id).ToList();

            return _mapper.Map<IEnumerable<PostsResponseDTO>>(query);
        }
    }
}
