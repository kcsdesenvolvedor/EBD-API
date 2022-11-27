using EBD.API.Models;

namespace EBD.API.Domain
{
    public interface IFetchDataWeb
    {
        Task<List<Lesson>> FetchLessonsFromEBD();
    }
}
