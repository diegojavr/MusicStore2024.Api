using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repositories
{
    public interface IGenreRepository
    {
        
        List<Genre> ListAll(); //Listar generos
        Genre? GetById(int id); //Obtener genero por id

        void Add(Genre entity); //agregar genero

        void Update(int id, Genre entity); //actualizar genero dado un id

        void Delete(int id); //eliminar un genero dado un id
    }
}
