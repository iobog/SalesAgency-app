using System;
using System.Collections.Generic;

namespace SalesAgency.Entities.Data;

public partial class TProduct
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<TOrderProduct> TOrderProducts { get; set; } = new List<TOrderProduct>();
}
