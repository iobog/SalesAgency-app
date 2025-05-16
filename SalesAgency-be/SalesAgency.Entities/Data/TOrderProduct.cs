using System;
using System.Collections.Generic;

namespace SalesAgency.Entities.Data;

public partial class TOrderProduct
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual TOrder? Order { get; set; }

    public virtual TProduct? Product { get; set; }
}
