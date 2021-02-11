CREATE TABLE categoria
(  
 id int IDENTITY(1,1),  
 descricao varchar (20),
 valor_hora int,
 PRIMARY KEY (id)
);

CREATE TABLE marca
(  
 id int IDENTITY(1,1),  
 nome varchar (40)
 PRIMARY KEY (id)
);

CREATE TABLE combustivel
(  
 id int IDENTITY(1,1),  
 nome varchar (40)
 PRIMARY KEY (id)
);

CREATE TABLE situacao_veiculo
(  
 id int IDENTITY(1,1),  
 descricao varchar (20)
 PRIMARY KEY (id)
);

CREATE TABLE modelo
(  
 id int IDENTITY(1,1),  
 nome varchar (40),
 capacidade_bagageiro SMALLINT,
 numero_ocupantes SMALLINT,
 ano_modelo SMALLINT,
 id_marca INT NOT NULL,
 id_combustivel INT NOT NULL,
 id_categoria INT NOT NULL,
 FOREIGN KEY (id_marca) REFERENCES marca(id),
 FOREIGN KEY (id_combustivel) REFERENCES combustivel(id),
 FOREIGN KEY (id_categoria) REFERENCES categoria(id),
 PRIMARY KEY (id)
);

CREATE TABLE veiculo
(  
 id int IDENTITY(1,1),  
 placa varchar (10) NOT NULL UNIQUE,
 ano_fabricacao SMALLINT,
 id_modelo INT NOT NULL,
 id_situacao INT NOT NULL,
 FOREIGN KEY (id_modelo) REFERENCES modelo(id),
 FOREIGN KEY (id_situacao) REFERENCES situacao_veiculo(id),
 PRIMARY KEY (id)
);