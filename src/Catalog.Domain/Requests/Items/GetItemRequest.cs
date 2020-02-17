using Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Requests.Items
{
    public class GetItemRequest
    {
        public Guid Id { get; set; }
    }
}
