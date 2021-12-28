using AutoMapper;

namespace EmiSoft.CleanArchitecture.Application.Interfaces;

public interface IMapTo<T>
{
    void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T)).ReverseMap();
}
