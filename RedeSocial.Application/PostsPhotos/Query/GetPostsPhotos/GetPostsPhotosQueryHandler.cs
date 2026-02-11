using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;


namespace RedeSocial.Application.PostsPhotos.Query.GetPostsPhotos
{
    public class GetPostsPhotosQueryHandler : IRequestHandler<GetPostsPhotosQuery, IEnumerable<PostsPhotosDTO>>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;
        public GetPostsPhotosQueryHandler(IMapper mapper, IUnitOfWork uof)
        {
            _mapper = mapper;
            _uof = uof;
        }

        public async Task<IEnumerable<PostsPhotosDTO>> Handle(GetPostsPhotosQuery request, CancellationToken cancellationToken)
        {
            var query = _uof.PostsPhotosRepository.GetAll()
            .OrderBy(u => u.PostsPhotosId).ToList();
                
            return _mapper.Map<IEnumerable<PostsPhotosDTO>>(query);
        }
    }
}
