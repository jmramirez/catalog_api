using Catalog.Domain.Requests.Items;
using Catalog.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Filters
{
    public class ItemExistsAttribute : TypeFilterAttribute
    {
        public ItemExistsAttribute() : base(typeof(ItemExistFilterImpl)) { }

        public class ItemExistFilterImpl : IAsyncActionFilter
        {
            private readonly IItemService _itemService;

            public ItemExistFilterImpl(IItemService itemService)
            {
                _itemService = itemService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if(!(context.ActionArguments["id"] is Guid id))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                var result = await _itemService.GetItemAsync(new GetItemRequest { Id = id });

                if(result == null)
                {
                    context.Result = new NotFoundObjectResult($"Item with id {id} not exist.");
                }

                await next();
            }
        }
    }
}
