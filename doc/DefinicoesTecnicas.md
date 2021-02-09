# Decisões Técnicas
Neste arquivo estão listadas todas as decisões técnicas tomadas, bem como a justificativa para cada uma delas.

### Sistema de Controle de Versão
Foi escolhido o *Git* para controle de versão do código pela facilidade de uso e alta aceitação no mercado. Nenhum modelo de fluxo de desenvolvimento e políticas de branches será utilizado porque, além de desnecessário para o contexto, isso dependeria de características da estratégia de releases da aplicação, formação do time desenvolvedor, modelo de negócio, etc.

### Linguagem e Framework
Foi escolhida a linguagem *C#* e o framework de desenvolvimento *.Net Core 3.1* porque, além de serem requisitos básicos da vaga, possuem características interessantes para a situação:
- Simplicidade
- Tipagem forte
- Orientação a objetos
- Flexibilidade
- Injeção de dependência

### API First
Para cada domínio (cliente, veículo, reserva, etc.) serão definidas, primeiramente, as operações *REST* que deverão ser implementadas. Essas definições serão baseadas nas funcionalidades que cada domínio precisa expor.

### Arquitetura
Decidi pela utilização da *Onion Architecture* características interessantes como organização em camadas, flexibilidade e acoplamento unidirecional. Isso permite algumas estratégias que favorecem muito a qualidade:
- Posso priorizar, inicialmente, decisões estratégicas (camada de domínio)  enquanto outras decisões (persistência, por exemplo) podem ser abordadas posteriormente e, inclusive, alteradas futuramente de forma simples. Na minha opinião, a adoção de conceitos de arquitetura evolutiva adaptadas para o escopo do desenvolvimento favorecem a construção de uma aplicação robusta e flexível.

- Será utilizada inversão de controle em comunicações entre quaisquer camadas da arquitetura. Isso tornará a aplicação completamente testável, sendo possível testar as camadas de forma completamente isolada.

Será utilizado *Notification Pattern* para padronizar a validação de entidades, bem como a transição delas entre camadas. Além disso, o padrão favorece o lançamento de exceções, que são onerosas em vários aspectos, apenas onde forem estritamente necessárias.

Um desenho simplificado da arquitetura pode ser visto em *img/ArquiteturaSimplificada.png*.

#### Por que não microsserviços?
Não foram abordadas na especificação necessidades de escalonamento da solução. Para simplificar a execução e os eventuais deploys da aplicação, decidi não construir microsserviços. Entretanto, por trata-se de uma API de uso geral, considero que a aplicação poderia ser uma boa candidata para essa abordagem. Portanto, aspectos importantes de aplicação distribuída foram observadas durante o planejamento da implementação:
- Contrato das APIs padronizados e bem definidos.
- Abstração de regras e autonomia de cada domínio.
- Requisições independentes de estados.
- Camada de persitência de cada domínio própria e independente.

Portanto, uma eventual migração arquitetural seria simples. As únicas alterações seriam no domínio de *locação*, cujo microsserviço consumiria *cliente* e *veículo*, além de servir como orquestrador.

### TDD
O desenvolvimento será organizado da seguinte forma:
1. Levantamento das regras para cada entidade de domínio ou fluxo e documentação das mesmas em um arquivo de texto.
2. Criação de testes unitários que garantam a aplicação das regras levantadas.
3. Desenvolvimento do código.

Será utilizado o framework *xUnit* pelos seguintes motivos:
- Familiaridade e facilidade de uso :-)
- Extensibilidade.
- Execução de testes em paralelo.
- Aderente a TDD.
- Testes orientados a dados (*Theory*).

Utilizarei também o conjunto de extensões *Fluent Assertions* que permite a escrita da asserções dos testes em uma linguagem mais natural e BDD-like.
