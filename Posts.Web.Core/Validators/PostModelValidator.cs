using FluentValidation;
using Posts.Web.Core.Models;

namespace Posts.Web.Core.Validators
{
    public class PostModelValidator : AbstractValidator<PostModel>
    {
        public PostModelValidator()
        {
            RuleFor(p => p.Author).NotEmpty();
            RuleFor(p => p.Content).NotEmpty();
            RuleFor(p => p.Title).NotEmpty();
        }
    }
}
