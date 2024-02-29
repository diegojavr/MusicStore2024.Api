using MusicStore.Domain;
using MusicStore.Persistence;
using MusicStore.Repositories.Interfaces;

namespace MusicStore.Repositories.Implementations
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(MusicStoreDbContext context)
            : base(context) { }

    }
}
