﻿using LocacaoCarro.Dominio.Entidades.Veiculos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IMarcaRepositorio
    {
        Task<IEnumerable<Marca>> ListarAsync();
        Task CriarAsync(Marca marca);
    }
}
