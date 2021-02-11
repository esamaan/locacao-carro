// Estrutura

CREATE TABLE usuario
(  
 id int IDENTITY(1,1),  
 nome varchar (20),
 sobrenome varchar (20),  
 hash_senha CHAR(256),
 PRIMARY KEY (id)
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

// Dados

BEGIN
    TRY
    BEGIN
        TRANSACTION;

        INSERT INTO usuario (nome
            , sobrenome
            , hash_senha)
        VALUES
            ('Ada' 
            , 'Lovelace'
            , '73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=')
        INSERT INTO operador (id_usuario
            , matricula)
        VALUES
            ((SELECT SCOPE_IDENTITY ())
            , '12345')
        COMMIT;
    END
    TRY
    BEGIN
        CATCH
        ROLLBACK
        DECLARE @ErrorMessage NVARCHAR (4000) = ERROR_MESSAGE ()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY ()
        DECLARE @ErrorState INT = ERROR_STATE ()
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END
    CATCH;

BEGIN
    TRY
    BEGIN
        TRANSACTION;

        INSERT INTO usuario (nome
            , sobrenome
            , hash_senha)
        VALUES
            ('Marie' 
            , 'Curie'
            , '73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=')
        INSERT INTO operador (id_usuario
            , matricula)
        VALUES
            ((SELECT SCOPE_IDENTITY ())
            , '12346')
        COMMIT;
    END
    TRY
    BEGIN
        CATCH
        ROLLBACK
        DECLARE @ErrorMessage NVARCHAR (4000) = ERROR_MESSAGE ()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY ()
        DECLARE @ErrorState INT = ERROR_STATE ()
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END
    CATCH;


BEGIN
    TRY
    BEGIN
        TRANSACTION;

        INSERT INTO usuario (nome
            , sobrenome
            , hash_senha)
        VALUES
            ('Nelson' 
            , 'Mandela'
            , '73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=')
        INSERT INTO operador (id_usuario
            , matricula)
        VALUES
            ((SELECT SCOPE_IDENTITY ())
            , '12347')
        COMMIT;
    END
    TRY
    BEGIN
        CATCH
        ROLLBACK
        DECLARE @ErrorMessage NVARCHAR (4000) = ERROR_MESSAGE ()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY ()
        DECLARE @ErrorState INT = ERROR_STATE ()
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END
    CATCH;

