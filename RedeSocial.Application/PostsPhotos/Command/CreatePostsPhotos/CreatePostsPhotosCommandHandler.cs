using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.PostsPhotos.Command.CreatePostsPhotos
{
    public class CreatePostsPhotosCommandHandler : IRequestHandler<CreatePostsPhotosCommand, PostsPhotosDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;

        public CreatePostsPhotosCommandHandler(IMapper mapper, IUnitOfWork uof)
        {
            _mapper = mapper;
            _uof = uof;
        }

        public async Task<PostsPhotosDTO> Handle(CreatePostsPhotosCommand request, CancellationToken ct)
        {
            if (request.postsPhotosDTO == null) throw new InvalidOperationException("Post Photo invalido.");

            var criadoMapper = _mapper.Map<Domain.Models.PostsPhotos>(request.postsPhotosDTO);

            var criado = await _uof.PostsPhotosRepository.Create(criadoMapper, ct);

            await _uof.Commit();

            var criadoDTO = _mapper.Map<PostsPhotosDTO>(criado);

            return criadoDTO;
        }
    }
}
