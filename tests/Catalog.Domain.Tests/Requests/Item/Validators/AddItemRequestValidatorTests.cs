using Catalog.Domain.Entities;
using Catalog.Domain.Requests.Item;
using Catalog.Domain.Requests.Item.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Catalog.Domain.Tests.Requests.Item.Validators;

public class AddItemRequestValidatorTests
{
    private readonly AddItemRequestValidator _validator;

    public AddItemRequestValidatorTests()
    {
        _validator = new AddItemRequestValidator();
    }

    [Fact]
    public void should_throw_error_when_artistId_is_null()
    {
        var addItemRequest = new AddItemRequest {Price = new Price()};
        var result = _validator.TestValidate(addItemRequest);
        result.ShouldHaveValidationErrorFor(x => x.ArtistId);
    }
    
    [Fact]
    public void should_throw_error_when_genreId_is_null()
    {
        var addItemRequest = new AddItemRequest {Price = new Price()};
        var result = _validator.TestValidate(addItemRequest);
        result.ShouldHaveValidationErrorFor(x => x.GenreId);
    }
}