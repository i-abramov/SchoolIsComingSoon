using MediatR;

namespace SchoolIsComingSoon.Application.PostFiles.Commands.DeletePostFile
{
    public class DeletePostFileCommand : IRequest<Unit>
    {
        public Guid PostId { get; set; }
        public Guid Id { get; set; }
    }
}