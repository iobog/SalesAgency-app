using System;
using System.Collections.Generic;

namespace SalesAgency.Entities.Data;

public partial class TOrder
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ClientId { get; set; }

    public string? Adress { get; set; }

    public decimal? Total { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TClient? Client { get; set; }

    public virtual ICollection<TOrderProduct> TOrderProducts { get; set; } = new List<TOrderProduct>();

    public virtual TUser? User { get; set; }
}
