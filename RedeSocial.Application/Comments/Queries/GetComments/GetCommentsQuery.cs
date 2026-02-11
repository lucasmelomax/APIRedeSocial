
using MediatR;
using RedeSocial.Application.DTOs.Comments;

namespace RedeSocial.Application.Comments.Queries.GetComments
{
    public record GetCommentsQuery() : IRequest<IEnumerable<CommentsDTO>>
    {
    }
}
