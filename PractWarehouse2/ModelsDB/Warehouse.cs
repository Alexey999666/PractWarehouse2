using System;
using System.Collections.Generic;

namespace PractWarehouse2.ModelsDB;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string ManagerLastName { get; set; } = null!;

    public virtual ICollection<StockBalance> StockBalances { get; set; } = new List<StockBalance>();
}
