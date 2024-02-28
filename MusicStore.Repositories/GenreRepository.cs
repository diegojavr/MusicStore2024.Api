using MusicStore.Domain;
using MusicStore.Persistence;

namespace MusicStore.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        
        private readonly MusicStoreDbContext _context;
        public GenreRepository(MusicStoreDbContext context)
        {
            _context = context;
        }


        public void Add(Genre entity)
        {
            _context.Set<Genre>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var registro=GetById(id);
            if (registro != null)
            {
                registro.Status = false; //solo queremos que cambie de estado y no se remueva de la bd
                registro.ModifiedDate = DateTime.Now;
                //_context.Set<Genre>().Remove(registro);
                _context.SaveChanges();
            }
        }

        public Genre? GetById(int id)
        {
            return _context.Set<Genre>().Find(id);
        }

        public List<Genre> ListAll()
        {
            return _context.Set<Genre>().ToList();
        }

        public void Update(int id, Genre entity)
        {
            var registro = GetById(id);
            if (registro != null)
            {
                registro.Name = entity.Name;
                registro.ModifiedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
