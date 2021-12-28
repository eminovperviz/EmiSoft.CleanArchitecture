using AutoMapper;

namespace EmiSoft.CleanArchitecture.Application.Interfaces;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
