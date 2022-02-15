﻿namespace API.Helpers;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //Parks Dtos
        CreateMap<Park, ParkDto>();
        CreateMap<CreateParkDto, Park>();
        CreateMap<UpdateParkDto, Park>();
    }
}
