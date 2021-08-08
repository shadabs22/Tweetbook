using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1.Requests;

namespace Tweetbook.Validators
{
    public class TagPostRequestValidator : AbstractValidator<TagPostRequest>
    {
        public TagPostRequestValidator()
        {
            RuleFor(x => x.text)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z0-9]*$");
                
        }
    }
}
