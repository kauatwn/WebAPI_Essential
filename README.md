# WebAPI Essential

## Descrição

Este repositório é um guia essencial para criar APIs REST utilizando a arquitetura em camadas (Layered Architecture) com ASP.NET Core na plataforma .NET, seguindo os princípios demonstrados pelo Professor José Carlos Macoratti. Aqui, você encontrará um exemplo prático baseado em seu ensino, com todas as camadas e componentes essenciais implementados.

## Estrutura do Repositório

- **Context**: Esta pasta contém o contexto do banco de dados e as classes de configuração relacionadas ao Entity Framework Core.
- **Controllers**: Aqui estão os controladores da API, que recebem as solicitações HTTP e chamam os serviços apropriados.
- **DTOs**: Contém os Data Transfer Objects (DTOs) utilizados para mapear os dados entre a camada de apresentação e a camada de serviço.
- **Entities**: Esta pasta contém as entidades do domínio, que representam os objetos principais do modelo de negócio.
- **Extensions**: Aqui está o arquivo ApiExceptionMiddlewareExtension.cs, que contém extensões para lidar com exceções na API.
- **Migrations**: Contém as migrações do Entity Framework Core para gerenciar o esquema do banco de dados.
- **Pagination**: Este diretório contém arquivos relacionados à paginação de resultados em consultas.
- **Repositories**: Classes responsáveis pela interação com o banco de dados, seguindo o padrão Repository.
- **Services**: Aqui estão os serviços da aplicação, incluindo o TokenService para geração de tokens JWT.