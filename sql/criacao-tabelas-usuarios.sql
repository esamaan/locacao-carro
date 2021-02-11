CREATE TABLE usuario
(  
 id int IDENTITY(1,1),  
 nome varchar (20),
 sobrenome varchar (20),  
 hash_senha CHAR(256),
 //PRIMARY KEY (id)
);

CREATE TABLE cliente
(
 id_usuario INT NOT NULL,  
 cpf varchar (14),
 cep varchar (10),
 logradouro VARCHAR (40),
 numero VARCHAR (6),
 complemento VARCHAR (10),
 cidade VARCHAR (60),
 uf VARCHAR (2),
 aniversario DATE,
 PRIMARY KEY (cpf),
 FOREIGN KEY (id_usuario) REFERENCES usuario(id) ON DELETE CASCADE
);

CREATE TABLE operador
(  
 id_usuario INT NOT NULL,
 matricula VARCHAR(10),
 PRIMARY KEY (matricula),
 FOREIGN KEY (id_usuario) REFERENCES usuario(id) ON DELETE CASCADE
);