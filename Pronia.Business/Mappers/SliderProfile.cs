using AutoMapper;
using Pronia.Business.ViewModels.SliderViewModels;
using Pronia.Core.Entities;

namespace Pronia.Business.Mappers;

public class SliderProfile:Profile
{
    public SliderProfile()
    {
        CreateMap<Slider, SliderPostVM>().ReverseMap();
    }
}
