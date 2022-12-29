using MoviesApp.Models;
using AutoMapper;

namespace MoviesApp.ViewModels.AutoMapperProfiles;

public class ActorProfile:Profile
{
    public ActorProfile()
    {
        CreateMap<Actor, InputActorViewModel>().ReverseMap();
        CreateMap<Actor, DeleteActorViewModel>();
        CreateMap<Actor, EditActorViewModel>().ReverseMap();
        CreateMap<Actor, ActorViewModel>();
    }
}