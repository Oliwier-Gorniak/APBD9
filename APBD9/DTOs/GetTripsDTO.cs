namespace APBD9.DTOs;

public class GetTripsDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public ICollection<GetTripsCountryDTO> Countries { get; set; } = null!;
    public ICollection<GetTripsClientDTO> Clients { get; set; } = null!;
}

public class GetTripsResponseDTO
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public ICollection<GetTripsDTO> Trips { get; set; }
}

public class GetTripsCountryDTO
{
    public string Name { get; set; }
}

public class GetTripsClientDTO
{
    public string FirstName { get; set; }
    public string Lastname { get; set; }
}