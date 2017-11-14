
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/14/2017 19:23:20
-- Generated from EDMX file: C:\Users\Guilherme\source\repos\SGTCC-1\Sgtcc\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SGTCC];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(max)  NOT NULL,
    [cpf] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [telefone] nvarchar(max)  NOT NULL,
    [senha] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Tccs'
CREATE TABLE [dbo].[Tccs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [titulo] nvarchar(max)  NOT NULL,
    [semestre] nvarchar(max)  NOT NULL,
    [ano] nvarchar(max)  NOT NULL,
    [status] nvarchar(max)  NOT NULL,
    [Professor_Id] int  NOT NULL,
    [Aluno_Id] int  NOT NULL
);
GO

-- Creating table 'Bancas'
CREATE TABLE [dbo].[Bancas] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'MembrosBancas'
CREATE TABLE [dbo].[MembrosBancas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Professor_Id] int  NOT NULL,
    [Banca_Id] int  NOT NULL
);
GO

-- Creating table 'Arquivoes'
CREATE TABLE [dbo].[Arquivoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(max)  NOT NULL,
    [extensao] nvarchar(max)  NOT NULL,
    [caminho] nvarchar(max)  NOT NULL,
    [Tcc_Id] int  NOT NULL
);
GO

-- Creating table 'CronogramaArquivoes'
CREATE TABLE [dbo].[CronogramaArquivoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [prazoInicial] nvarchar(max)  NOT NULL,
    [prazoFinal] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CronogramaAgendamentoes'
CREATE TABLE [dbo].[CronogramaAgendamentoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [prazoInicial] nvarchar(max)  NOT NULL,
    [prazoFinal] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Usuarios_Professor'
CREATE TABLE [dbo].[Usuarios_Professor] (
    [instituicao] nvarchar(max)  NOT NULL,
    [tipo] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Tccs_Tcc2'
CREATE TABLE [dbo].[Tccs_Tcc2] (
    [data] nvarchar(max)  NOT NULL,
    [local] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL,
    [Banca_Id] int  NOT NULL
);
GO

-- Creating table 'Usuarios_Aluno'
CREATE TABLE [dbo].[Usuarios_Aluno] (
    [matricula] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tccs'
ALTER TABLE [dbo].[Tccs]
ADD CONSTRAINT [PK_Tccs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Bancas'
ALTER TABLE [dbo].[Bancas]
ADD CONSTRAINT [PK_Bancas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MembrosBancas'
ALTER TABLE [dbo].[MembrosBancas]
ADD CONSTRAINT [PK_MembrosBancas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Arquivoes'
ALTER TABLE [dbo].[Arquivoes]
ADD CONSTRAINT [PK_Arquivoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CronogramaArquivoes'
ALTER TABLE [dbo].[CronogramaArquivoes]
ADD CONSTRAINT [PK_CronogramaArquivoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CronogramaAgendamentoes'
ALTER TABLE [dbo].[CronogramaAgendamentoes]
ADD CONSTRAINT [PK_CronogramaAgendamentoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Usuarios_Professor'
ALTER TABLE [dbo].[Usuarios_Professor]
ADD CONSTRAINT [PK_Usuarios_Professor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tccs_Tcc2'
ALTER TABLE [dbo].[Tccs_Tcc2]
ADD CONSTRAINT [PK_Tccs_Tcc2]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Usuarios_Aluno'
ALTER TABLE [dbo].[Usuarios_Aluno]
ADD CONSTRAINT [PK_Usuarios_Aluno]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Professor_Id] in table 'MembrosBancas'
ALTER TABLE [dbo].[MembrosBancas]
ADD CONSTRAINT [FK_ProfessorMembrosBanca]
    FOREIGN KEY ([Professor_Id])
    REFERENCES [dbo].[Usuarios_Professor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfessorMembrosBanca'
CREATE INDEX [IX_FK_ProfessorMembrosBanca]
ON [dbo].[MembrosBancas]
    ([Professor_Id]);
GO

-- Creating foreign key on [Banca_Id] in table 'MembrosBancas'
ALTER TABLE [dbo].[MembrosBancas]
ADD CONSTRAINT [FK_BancaMembrosBanca]
    FOREIGN KEY ([Banca_Id])
    REFERENCES [dbo].[Bancas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BancaMembrosBanca'
CREATE INDEX [IX_FK_BancaMembrosBanca]
ON [dbo].[MembrosBancas]
    ([Banca_Id]);
GO

-- Creating foreign key on [Tcc_Id] in table 'Arquivoes'
ALTER TABLE [dbo].[Arquivoes]
ADD CONSTRAINT [FK_TccArquivo]
    FOREIGN KEY ([Tcc_Id])
    REFERENCES [dbo].[Tccs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TccArquivo'
CREATE INDEX [IX_FK_TccArquivo]
ON [dbo].[Arquivoes]
    ([Tcc_Id]);
GO

-- Creating foreign key on [Professor_Id] in table 'Tccs'
ALTER TABLE [dbo].[Tccs]
ADD CONSTRAINT [FK_ProfessorTcc]
    FOREIGN KEY ([Professor_Id])
    REFERENCES [dbo].[Usuarios_Professor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfessorTcc'
CREATE INDEX [IX_FK_ProfessorTcc]
ON [dbo].[Tccs]
    ([Professor_Id]);
GO

-- Creating foreign key on [Banca_Id] in table 'Tccs_Tcc2'
ALTER TABLE [dbo].[Tccs_Tcc2]
ADD CONSTRAINT [FK_BancaTcc2]
    FOREIGN KEY ([Banca_Id])
    REFERENCES [dbo].[Bancas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BancaTcc2'
CREATE INDEX [IX_FK_BancaTcc2]
ON [dbo].[Tccs_Tcc2]
    ([Banca_Id]);
GO

-- Creating foreign key on [Aluno_Id] in table 'Tccs'
ALTER TABLE [dbo].[Tccs]
ADD CONSTRAINT [FK_AlunoTcc]
    FOREIGN KEY ([Aluno_Id])
    REFERENCES [dbo].[Usuarios_Aluno]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlunoTcc'
CREATE INDEX [IX_FK_AlunoTcc]
ON [dbo].[Tccs]
    ([Aluno_Id]);
GO

-- Creating foreign key on [Id] in table 'Usuarios_Professor'
ALTER TABLE [dbo].[Usuarios_Professor]
ADD CONSTRAINT [FK_Professor_inherits_Usuario]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Tccs_Tcc2'
ALTER TABLE [dbo].[Tccs_Tcc2]
ADD CONSTRAINT [FK_Tcc2_inherits_Tcc]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Tccs]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Usuarios_Aluno'
ALTER TABLE [dbo].[Usuarios_Aluno]
ADD CONSTRAINT [FK_Aluno_inherits_Usuario]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------