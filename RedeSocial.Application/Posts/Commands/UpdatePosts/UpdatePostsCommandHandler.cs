
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Posts.Commands.UpdatePosts
{
    public class UpdatePostsCommandHandler : IRequestHandler<UpdatePostsCommand, PostsResponseDTO>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public UpdatePostsCommandHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<PostsResponseDTO> Handle(UpdatePostsCommand request, CancellationToken ct)
        {
            if (request.postsDTO is null)
                throw new InvalidOperationException("Post inválido.");

            var post = await _uof.PostsRepository.GetById(request.id, ct);

            if(request.postsDTO.PostsId != request.id) throw new InvalidOperationException("Post não encontrado.");

            if (post is null)
                throw new InvalidOperationException("Post não encontrado.");

            _mapper.Map(request.postsDTO, post);

            await _uof.PostsRepository.Update(request.id, post, ct);
            await _uof.Commit();

            return _mapper.Map<PostsResponseDTO>(post);
        }
    }
}
