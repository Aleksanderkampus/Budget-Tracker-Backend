namespace BLL.DTO.Categories;

public class PieChartData
{
    public string Name { get; set; } = default!;

    public string HexColor { get; set; } = default!;

    public double TotalAmount { get; set; }
}