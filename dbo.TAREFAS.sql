CREATE TABLE [dbo].[TAREFAS] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Nome]        VARCHAR (50)  NOT NULL,
    [Prioridade]  INT           NOT NULL,
    [Concluida]   BIT           NOT NULL,
    [Observacoes] VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

