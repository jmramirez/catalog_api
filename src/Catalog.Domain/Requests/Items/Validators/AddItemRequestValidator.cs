using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Requests.Items.Validators
{
    public class AddItemRequestValidator : AbstractValidator<AddItemRequest>
    {
        public AddItemRequestValidator()
        {
            RuleFor(x => x.GenreId).NotEmpty();
            RuleFor(x => x.ArtistId).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.ReleaseData).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).Must(x => x?.Amount > 0);
            RuleFor(x => x.AvailableStock).Must(x => x > 0);
        }
    }
}
