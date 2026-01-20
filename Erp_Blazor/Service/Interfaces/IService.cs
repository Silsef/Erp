namespace Erp_Blazor.Service.Interfaces
{
    // Interface pour la lecture seule (Get All, Get By Id)
    public interface IReadableService<TReadDto>
    {
        Task<List<TReadDto>> GetAll();
        Task<TReadDto> GetByID(int id);
    }

    // Interface pour la création (accepte un DTO de création, retourne le DTO complet)
    public interface ICreatableService<TReadDto, TCreateDto>
    {
        Task<TReadDto> Post(TCreateDto item);
    }

    // Interface pour la modification
    public interface IUpdatableService<TReadDto, TUpdateDto>
    {
        Task<TReadDto> Put(int id, TUpdateDto item);
    }

    // Interface pour la suppression
    public interface IDeletableService
    {
        Task Delete(int id);
    }

    // Interface "Package complet" pour ceux qui ont besoin de tout (comme Employe)
    // T = DTO de lecture, TC = DTO de création, TU = DTO de modification
    public interface ICrudService<T, TC, TU> :
        IReadableService<T>,
        ICreatableService<T, TC>,
        IUpdatableService<T, TU>,
        IDeletableService
    {
    }
}
