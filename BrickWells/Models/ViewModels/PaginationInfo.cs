namespace BrickWells.Models.ViewModels;

public class PaginationInfo
{
    public int totalItems { get; set; }
    public int itemsPerPage { get; set; }
    public int currentPage { get; set; }
    public int totalPages => (int)(Math.Ceiling((decimal)totalItems / itemsPerPage));
}