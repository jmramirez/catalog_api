using Catalog.Domain.Entities;
using Catalog.Domain.Requests.Item;
using Catalog.Domain.Requests.Item.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Catalog.Domain.Tests.Requests.Item.Validators;

public class EditItemRequestValidatorTests
{
    private readonly EditItemRequestValidator _validator;

    public EditItemRequestValidatorTests()
    {
        _validator = new EditItemRequestValidator();
    }
    
    [Fact]
    public void should_throw_error_when_id_is_null()
    {
        var editItemRequest = new EditItemRequest {Price = new Price()};
        var result = _validator.TestValidate(editItemRequest);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void should_throw_error_when_artistId_is_null()
    {
        var editItemRequest = new EditItemRequest {Price = new Price()};
        var result = _validator.TestValidate(editItemRequest);
        result.ShouldHaveValidationErrorFor(x => x.ArtistId);
    }
    
    [Fact]
    public void should_throw_error_when_genreId_is_null()
    {
        var editItemRequest = new EditItemRequest {Price = new Price()};
        var result = _validator.TestValidate(editItemRequest);
        result.ShouldHaveValidationErrorFor(x => x.GenreId);
    }
}