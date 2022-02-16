namespace API.Helpers;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //Parks Dtos
        CreateMap<Park, ParkDto>();
        CreateMap<CreateParkDto, Park>();
        CreateMap<UpdateParkDto, Park>();

        //Trails Dtos
        CreateMap<Trail, TrailCreateDto>();
        CreateMap<TrailCreateDto, Trail>();
        CreateMap<Trail, TrailDto>();
    }
}
