

using MediatR;
using RedeSocial.Application.DTOs.Comments;

namespace RedeSocial.Application.Comments.Queries.GetCommentsByPost
{
    public record GetCommentsByPostQuery(int id) : IRequest<IEnumerable<CommentsDTO>>
    {
    }
}
