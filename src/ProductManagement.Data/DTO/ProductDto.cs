using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Data.DTO;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
