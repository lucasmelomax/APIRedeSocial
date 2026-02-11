

using MediatR;
using RedeSocial.Application.DTOs.Comments;

namespace RedeSocial.Application.Comments.Queries.GetCommentsById
{
    public record GetCommentsByIdQuery(int id) : IRequest<CommentsDTO>
    {
    }
}
