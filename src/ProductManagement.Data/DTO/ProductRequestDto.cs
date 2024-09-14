namespace ProductManagement.Data.DTO;

public class ProductRequestDto
{
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
