using AutoMapper;
using LocacaoCarro.Aplicacao.Interfaces;
using LocacaoCarro.Aplicacao.Modelos.Veiculos;
using LocacaoCarro.Aplicacao.Resultados;
using LocacaoCarro.Dominio.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocacaoCarro.Aplicacao
{
    public class VeiculoAplicacao : IVeiculoAplicacao
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly IMarcaRepositorio _marcaRepositorio;
        private readonly IModeloRepositorio _modeloRepositorio;
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public VeiculoAplicacao(IMapper mapper, ICategoriaRepositorio categoriaRepositorio, IMarcaRepositorio marcaRepositorio, IModeloRepositorio modeloRepositorio, IVeiculoRepositorio veiculoRepositorio)
        {
            _mapper = mapper;
            _categoriaRepositorio = categoriaRepositorio;
            _marcaRepositorio = marcaRepositorio;
            _modeloRepositorio = modeloRepositorio;
            _veiculoRepositorio = veiculoRepositorio;
        }

        public Task<Resultado<VeiculoModel>> ConsultarVeiculoPorPlacaAsync(string placa)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resultado> CriarMarcaAsync(MarcaModel marcaModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resultado> CriarModeloAsync(ModeloModel modeloModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resultado> CriarVeiculoAsync(VeiculoModel veiculoModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resultado<IEnumerable<CategoriaModel>>> ListarCategoriasAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Resultado<IEnumerable<MarcaModel>>> ListarMarcasAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Resultado<IEnumerable<ModeloModel>>> ListarModelosAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Resultado<VeiculoModel>> ReservarVeiculoPorPlacaAsync(int idModelo)
        {
            throw new System.NotImplementedException();
        }
    }
}
