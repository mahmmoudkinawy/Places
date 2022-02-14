namespace API.Helpers;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //Parks Dtos
        CreateMap<Park, ParkDto>();
    }
}
