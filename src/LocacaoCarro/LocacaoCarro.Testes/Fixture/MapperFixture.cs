using AutoMapper;
using LocacaoCarro.Aplicacao.Mapeamentos;

namespace LocacaoCarro.Testes.Fixture
{
    public class MapperFixture
    {
        public IMapper Mapper { get; }

        public MapperFixture()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new ClienteMap());
                opts.AddProfile(new OperadorMap());
            });

            Mapper = config.CreateMapper();
        }
    }
}
