﻿namespace API.Helpers;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //Parks Dtos
        CreateMap<Park, ParkDto>();
        CreateMap<CreateParkDto, Park>();
        CreateMap<UpdateParkDto, Park>();

        //Trails Dtos
        CreateMap<TrailCreateDto, Trail>().ReverseMap();
        CreateMap<Trail, TrailDto>();
        CreateMap<UpdateTrailDto, Trail>();
    }
}
