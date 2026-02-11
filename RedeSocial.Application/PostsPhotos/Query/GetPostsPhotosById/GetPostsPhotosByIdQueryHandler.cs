using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.PostsPhotos.Query.GetPostsPhotosById
{
    public class GetPostsPhotosByIdQueryHandler : IRequestHandler<GetPostsPhotosByIdQuery, PostsPhotosDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;
        public GetPostsPhotosByIdQueryHandler(IMapper mapper, IUnitOfWork uof)
        {
            _mapper = mapper;
            _uof = uof;
        }
        public async Task<PostsPhotosDTO> Handle(GetPostsPhotosByIdQuery request, CancellationToken ct)
        {
            var post = await _uof.PostsPhotosRepository.GetById(request.id, ct);

            if (post == null) throw new InvalidOperationException("Post Photo invalido.");

            var postDTO = _mapper.Map<PostsPhotosDTO>(post);

            return postDTO;
        }
    }
}
