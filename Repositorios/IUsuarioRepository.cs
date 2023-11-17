using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp09_2023_TomasDLV.Models;

namespace tl2_tp09_2023_TomasDLV.Repositorios
{
    public interface IUsuarioRepository
    {

        public void CreateUser(Usuario usuario);

        public void UpdateUser(int id, Usuario usuario);
        public List<Usuario> GetAllUser();
        public Usuario GetByIdUser(int idUsuario);
        public void RemoveUser(int id);
    }

}