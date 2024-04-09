using System;
using System.Collections.Generic;

namespace RSMEnterpriseIntegrationsAPI.Domain.Models;


public partial class ProductCategory
{

    public int ProductCategoryId { get; set; }
    public string? Name { get; set; } = null!;
    public Guid Rowguid { get; set; }
    public DateTime ModifiedDate { get; set; }
}
