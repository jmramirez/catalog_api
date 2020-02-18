using Catalog.Domain.Entities;
using Catalog.Domain.Requests.Items;
using Catalog.Domain.Requests.Items.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Catalog.Domain.Tests.Item.Validators
{
    public class AddItemRequestValidatorTests
    {
        private readonly AddItemRequestValidator _validator;

        public AddItemRequestValidatorTests()
        {
            _validator = new AddItemRequestValidator();
        }

        [Fact]
        public void should_have_error_when_ArtistId_is_null()
        {
            var addItemResquest = new AddItemRequest { Price = new Price() };
            _validator.ShouldHaveValidationErrorFor(x => x.ArtistId, addItemResquest);

        }

        [Fact]
        public void should_have_error_when_GenreId_is_null()
        {
            var addItemResquest = new AddItemRequest { Price = new Price() };
            _validator.ShouldHaveValidationErrorFor(x => x.GenreId, addItemResquest);
        }
    }

}
