using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Posts.Queries.GetPostsById
{
    public class GetPostsByIdQueryHandler : IRequestHandler<GetPostsByIdQuery, PostsResponseDTO>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public GetPostsByIdQueryHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<PostsResponseDTO> Handle(GetPostsByIdQuery request, CancellationToken ct)
        {
            if (request.id < 0) throw new InvalidOperationException("Id invalido.");

            var post = await _uof.PostsRepository.GetById(request.id, ct);

            if (post == null) throw new InvalidOperationException("Erro ao encontrar post.");

            return _mapper.Map<PostsResponseDTO>(post);
        }
    }
}
