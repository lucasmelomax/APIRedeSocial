

using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs.Comments;
using RedeSocial.Domain.Account;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Comments.Commands.CreateComments
{
    public class CreateCommentsCommandHandler : IRequestHandler<CreateCommentsCommand, CommentsDTO>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly ICurrenteUserService _currenteUserService;

        public CreateCommentsCommandHandler(IUnitOfWork uof, IMapper mapper, ICurrenteUserService currenteUserService)
        {
            _uof = uof;
            _mapper = mapper;
            _currenteUserService = currenteUserService;
        }

        public async Task<CommentsDTO> Handle(CreateCommentsCommand request, CancellationToken ct)
        {
            var userId = _currenteUserService.UserId;

            if (request.commentsDTO == null)
                throw new ArgumentException("Comentário inválido.");

            var comment = _mapper.Map<Domain.Models.Comments>(request.commentsDTO);
            comment.UsersId = userId;

            var criado = await _uof.CommentsRepository.Create(comment, ct);
            await _uof.Commit();

            return _mapper.Map<CommentsDTO>(criado);
        }
    }
}
