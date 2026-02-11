
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Posts.Commands.CreatePosts
{
    public class CreatePostsCommandHandler : IRequestHandler<CreatePostsCommand, PostsResponseDTO>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CreatePostsCommandHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<PostsResponseDTO> Handle(CreatePostsCommand request, CancellationToken ct)
        {
            if (request.postsDTO is null)
                throw new InvalidOperationException("Post inválido.");

            var user = await _uof.UserRepository.GetById(request.postsDTO.UsersId, ct);

            if (user is null)
                throw new InvalidOperationException("Usuário não encontrado.");

            var post = _mapper.Map<Domain.Models.Posts>(request.postsDTO);

            await _uof.PostsRepository.Create(post, ct);
            await _uof.Commit();

            return _mapper.Map<PostsResponseDTO>(post);
        }
    }
}
