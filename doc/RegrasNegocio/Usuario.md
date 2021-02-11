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

- **GET** *usuarios/operadores/{id}* : busca dados do operador. Permissão: operador.
- **POST** *usuarios/operadores/autenticar* : autentica o operador. Permissão: autenticação não necessária.

- **GET** *usuarios/clientes/{id}* : busca dados do cliente. Permissão: cliente e operador.
- **POST** *usuarios/clientes* : cria um cliente. Permissão: operador.
- **PUT** *usuarios/clientes/{id}* : edita um cliente. Permissão: operador.
- **DELETE** *usuarios/clientes/{id}* : remove um cliente. Permissão: operador.
- **POST** *usuarios/clientes/autenticar* : autentica o cliente. Permissão: autenticação não necessária.

#### Autenticação
Não será armazenada senhas em texto plano no banco de dados. Será utilizado o algoritmo Base64(Sha256(SENHA_PLANA)) e o resultado será armazenado em banco. No processo de autenticação, o algoritmo será aplicado novamente na senha informada pelo usuário e, caso o resultado coincida com o que está armazenado no banco, a autenticação será bem sucedida e retornaremos ao usuário um token JWT que poderá ser acrescentado ao header das requisições seguintes para acessar as rotas restritas.

#### Persistência
O script utilizado para criar a base de dados de usuários está em *criacao-tabelas-usuarios.sql*.
