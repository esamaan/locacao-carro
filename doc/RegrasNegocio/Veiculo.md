### Veículo
O esquema do banco de dados de *Veículo* pode ser visto em *img/BDVeículos.png*. Como mencionado no documento de decisões técnicas, os 3 serviços definidos (usuário, veículo e locação) possuem bases independentes entre si. Não existe nenhum relacionamento entre a tabela de veículos e a de locação. Portanto, foi definido uma coluna *status* na tabela de veículo que indica a situação do mesmo (disponível, alugado, manutenção, etc.). Isso foi feito para que não seja necessário consultar o serviço de locação para se descobrir a situação do veículo, o que criaria uma dependência circular entre esses 2 serviços.

Não serão implementadas todos os verbos para as entidades por questões de simplificação. Serão implementados apenas as necessárias para a API em questão.

- **GET** *veiculos/marcas* : retorna todas as marcas.
- **POST** *veiculos/marcas* : cadastra uma marca.

- **GET** *veiculos/modelos* : retorna todos os modelos.
- **POST** *veiculos/modelos* : cadastra um modelo.

- **GET** *veiculos/{placa}* : busca um veículo por placa.
- **GET** *veiculos* : cadastra um veículo.
- **POST** *veiculos/reservar/{idModelo}* : reserva um veículo de determinado modelo e retorna os dados do mesmo. Isso garante que o veículo estará disponível para a reserva que está sendo feita. Como efeito colateral, o serviço de reservas precisa desalocar o veículo caso a reserva não seja concluída. Isso pode fazer com que a base de veículo fique em um estado inconsistente, com veículo vinculados a reservas que não existem. Mas essa é uma característica intrínseca à aborgadem de serviço com responsabilidade bem definidas. Poderia ser feito, por exemplo, um job que sanitizasse a base de veículos de tempos em tempos para evitar esses casos.

- **GET** *veiculos/categorias* : lista todas as categorias disponíveis.

#### Autenticação
Não será armazenada senhas em texto plano no banco de dados. Será utilizado o algoritmo Base64(Sha256(SENHA_PLANA)) e o resultado será armazenado em banco. No processo de autenticação, o algoritmo será aplicado novamente na senha informada pelo usuário e, caso o resultado coincida com o que está armazenado no banco, a autenticação será bem sucedida e retornaremos ao usuário um token JWT que poderá ser acrescentado ao header das requisições seguintes para acessar as rotas restritas.

#### Persistência
O script utilizado para criar a base de dados de usuários está em *criacao-tabelas-veiculos.sql*.