using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.PostsPhotos.Query.GetPostsPhotosByPost
{
    public class GetPostsPhotosByPostQueryHandler : IRequestHandler<GetPostsPhotosByPostQuery, IEnumerable<PostsPhotosDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;
        public GetPostsPhotosByPostQueryHandler(IMapper mapper, IUnitOfWork uof)
        {
            _mapper = mapper;
            _uof = uof;
        }
        public async Task<IEnumerable<PostsPhotosDTO>> Handle(GetPostsPhotosByPostQuery request, CancellationToken ct)
        {
            if (request.id <= 0) throw new InvalidOperationException("Id invalido.");

            var query = _uof.PostsPhotosRepository.GetAll()
                .OrderBy(u => u.PostsPhotosId).Where(p => p.PostsId == request.id).ToList();

            return _mapper.Map<IEnumerable<PostsPhotosDTO>>(query);

        }
    }
}
