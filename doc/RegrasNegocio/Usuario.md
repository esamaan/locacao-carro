### Usuário
- Existem 2 perfis de usuários:
> - Cliente
>> - Campos: ID, nome, CPF, aniversário, endereço (CEP, logradouro, número, complemento, cidade e estado).
>> - Login feito com CPF e senha; deve retornar os dados do cliente.
> - Operador
>> - Campos: ID, matrícula, nome.
>> - Login feito com matrícula e senha; deve retornar os dados do operador.
- Campos obrigatórios para cadastro.

As rotas de cliente e operador serão distintas para evitar, basicamente, 2 problemas:
- Conflito entre matrícula e CPF no login.
- Indefinição do retorno do login ou busca de usuário.

Não será disponibilizadas rotas para criação/exclusão de operadores por questão de segurança, uma vez que esse perfil só deveria ser criado por algum outro perfil superior, o que está fora do escopo.

- **GET** *usuarios/operadores/{id}* : busca dados do operador.
- **POST** *usuarios/operadores/autenticar* : autentica o operador.

- **GET** *usuarios/clientes/{id}* : busca dados do cliente.
- **POST** *usuarios/clientes* : cria um cliente.
- **PUT** *usuarios/clientes/{id}* : edita um cliente
- **DELETE** *usuarios/clientes/{id}* : remove um cliente
- **POST** *usuarios/clientes/autenticar* : autentica o cliente.
