
using AutoMapper;
using MediatR;
using RedeSocial.Application.DTOs;
using RedeSocial.Domain.Interfaces;

namespace RedeSocial.Application.Likes.Commands.CreateLikes
{
    public class CreateLikesCommandHandler : IRequestHandler<CreateLikesCommand, LikesDTO>
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public CreateLikesCommandHandler(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<LikesDTO> Handle(CreateLikesCommand request, CancellationToken ct)
        {
            if (request.likesDTO == null) throw new ArgumentException("Comentario invalido.");
            var like = _mapper.Map<Domain.Models.Likes>(request.likesDTO);
            var criado = await _uof.LikesRepository.Create(like, ct);
            var likesPost = await _uof.UserRepository.GetById(criado.UsersId, ct);
            if (likesPost == null) throw new ArgumentException("Este usuario nao existe.");
            await _uof.Commit();
            return _mapper.Map<LikesDTO>(criado);
        }
    }
}
