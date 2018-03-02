using FluentValidation;
using Posts.Web.Core.Models;

namespace Posts.Web.Core.Validators
{
    public class CommentModelValidator : AbstractValidator<CommentModel>
    {
        public CommentModelValidator()
        {
            RuleFor(p => p.Author).NotEmpty();
            RuleFor(p => p.Content).NotEmpty();
            RuleFor(p => p.PostId).NotEmpty();
        }
    }
}