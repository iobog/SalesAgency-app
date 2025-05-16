using System;
using System.Collections.Generic;

namespace SalesAgency.Entities.Data;

public partial class TClient
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<TOrder> TOrders { get; set; } = new List<TOrder>();
}
