# Iba.Net.Backend.Template

Este repositório contém um template de aplicação backend desenvolvido em .NET 8, seguindo os princípios da **Clean Architecture** e **SOLID**, facilitando a criação de novos projetos com uma estrutura escalável e bem organizada. Ele já integra autenticação com **Bearer Token** usando email e senha, suporte a migrações de banco de dados e a criação de um administrador padrão.

## Sumário
- [Arquitetura](#arquitetura)
- [Padrões Utilizados](#padrões-utilizados)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Configurações de Ambiente](#configurações-de-ambiente)
- [Autenticação e Autorização](#autenticação-e-autorização)
- [Migrações e Usuário Administrador Padrão](#migrações-e-usuário-administrador-padrão)
- [Como Executar](#como-executar)
- [Contribuições](#contribuições)

## Arquitetura

A arquitetura segue o padrão **Clean Architecture**, que divide a aplicação em camadas bem definidas:

1. **Presentation**: A camada de API, responsável pela interação com o usuário.
2. **Application**: Contém os casos de uso da aplicação, implementa serviços e lógica de negócio.
3. **Domain**: Define as entidades e regras de negócio do domínio.
4. **Data**: Implementa o acesso ao banco de dados e repositórios.
5. **Infrastructure**: Integração com serviços externos (ex.: provedores de email, APIs).

## Padrões Utilizados

- **Single Responsibility Principle**: Cada classe tem uma única responsabilidade.
- **Dependency Inversion Principle**: Utilizamos a injeção de dependência para desacoplar módulos.
- **Interface Segregation**: Interfaces são divididas conforme suas responsabilidades.

## Estrutura do Projeto

A estrutura do projeto é organizada conforme a arquitetura limpa:

├── Iba.Net.Backend.Template             # Camada de Apresentação (API)<br>
│   ├── Authentication                   # Serviços de autenticação e autorização<br>
│   │   ├── AuthenticationServices.cs    # Serviço para lidar com autenticação JWT<br>
│   │   ├── PermissionsAuthorizeAttribute.cs # Atributo personalizado para autorização baseada em permissões<br>
│   │   └── roles.json                   # Arquivo de configuração de papéis e permissões<br>
│   ├── Configurations                   # Configurações da aplicação (CORS, Swagger, etc.)<br>
│   │   ├── RoleConfigurationService.cs  # Serviço de configuração de roles<br>
│   │   └── SwaggerConfigurationService.cs # Serviço de configuração do Swagger<br>
│   ├── Controllers                      # Controladores da API<br>
│   │   └── AuthenticationController.cs  # Controlador de autenticação (Login, Logout, etc.)<br>
│   ├── Extensions                       # Extensões para métodos auxiliares<br>
│   │   └── DatabaseExtensions.cs        # Métodos de extensão para interações com banco de dados<br>
│   ├── Middlewares                      # Middlewares para tratamento de requisições/respostas<br>
│   │   └── ExceptionHandlerMiddleware.cs # Middleware para tratamento centralizado de exceções<br>
│   ├── appsettings.json                 # Configurações da aplicação em produção<br>
│   ├── appsettings.Development.json     # Configurações da aplicação em desenvolvimento<br>
└── Program.cs                           # Arquivo de inicialização do projeto (Main)<br>

├── Iba.Net.Backend.Template.Application                          # Camada de Aplicação<br>
│   ├── Contracts                        # Contratos (Interfaces e abstrações)<br>
│   │   ├── Repositories                 # Interfaces de repositórios<br>
│   │   │   └── IBaseRepository.cs       # Interface base para repositórios<br>
│   │   ├── Services                     # Interfaces para serviços da aplicação<br>
│   │   │   ├── IRoleService.cs          # Interface para gerenciamento de roles<br>
│   │   │   ├── ITokenService.cs         # Interface para criação e validação de tokens JWT<br>
│   │   │   ├── IUserService.cs          # Interface para gerenciamento de usuários<br>
│   │   │   └── IDatetimeUtilsService.cs # Interface para utilitários de data e hora<br>
│   │   ├── Settings                     # Configurações de aplicação<br>
│   │   │   └── IAppSettings.cs          # Interface para acessar configurações do app<br>
│   ├── Attributes                       # Atributos personalizados<br>
│   │   └── MinValueAttribute.cs         # Atributo personalizado para validação de valor mínimo<br>
│   ├── Enums                            # Enumerações<br>
│   │   └── ERole.cs                     # Enumeração para os diferentes tipos de papéis (roles)<br>
│   ├── Exceptions                       # Exceções personalizadas<br>
│   │   ├── BadRequestException.cs       # Exceção para erros de requisição (400)<br>
│   │   ├── ConflictException.cs         # Exceção para conflitos de estado (409)<br>
│   │   ├── NoContentException.cs        # Exceção para ausência de conteúdo (204)<br>
│   │   └── NotFoundException.cs         # Exceção para recursos não encontrados (404)<br>
│   ├── Extensions                       # Métodos de extensão e utilitários<br>
│   │   └── StringExtensions.cs          # Métodos de extensão para manipulação de strings<br>
│   ├── Models                           # Modelos de domínio e DTOs<br>
│   │   ├── Dtos                         # Objetos de transferência de dados (DTOs)<br>
│   │   ├── Exceptions                   # Modelos de exceções<br>
│   │   ├── Pagination                   # Modelos para paginação de resultados<br>
│   │   └── Role                         # Modelos relacionados a papéis (roles)<br>
│   │       └── Role.cs                  # Modelo de um papel (role)<br>
│   │   └── Settings                     # Modelos de configurações da aplicação<br>
│   ├── Services                         # Implementações dos serviços da aplicação<br>
│   │   ├── RoleService.cs               # Serviço para gerenciamento de papéis (roles)<br>
│   │   ├── TokenService.cs              # Serviço para gerenciamento de tokens JWT<br>
│   │   ├── UserService.cs               # Serviço para gerenciamento de usuários<br>
│   │   └── DatetimeUtilsService.cs      # Serviço de utilitários para manipulação de datas<br>

├── Iba.Net.Backend.Template.Domain       # Camada de Domínio<br>
│   └── Entities                          # Definição das entidades do domínio<br>
│       ├── BaseEntity.cs                 # Classe base para todas as entidades do domínio<br>
│       └── UserEntity.cs                 # Entidade que representa um usuário no sistema<br>

├── Iba.Net.Backend.Template.Data         # Camada de Acesso a Dados<br>
│   ├── Common                            # Componentes comuns usados por repositórios<br>
│   │   └── BaseRepository.cs             # Repositório base para operações CRUD<br>
│   ├── Context                           # Contexto de banco de dados e migrações<br>
│   │   └── DatabaseContext.cs            # Configuração do Entity Framework para o banco de dados<br>
│   └── Migrations                        # Migrações do banco de dados geradas pelo Entity Framework<br>
│   └── DependencyInjection.cs            # Configuração de injeção de dependências para a camada de dados<br>

├── Iba.Net.Backend.Template.Infrastructure # Camada de Infraestrutura<br>
│   └── DependencyInjection.cs              # Configuração de injeção de dependências para a camada de infraestrutura<br>


## Configurações de Ambiente

As configurações estão centralizadas no arquivo `appsettings.json`. Em desenvolvimento, utilize o arquivo `appsettings.Development.json` para definir os valores de banco de dados, autenticação JWT, e outras variáveis.

### Exemplo de `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Username=postgres;Password=P@ssw0rd;Database=postgres"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "AdminSettings": {
    "Email": "admin@ibasolutions.com.br",
    "Password": "StrongPassword"
  },
  "ConfigurationsSettings": {
    "DatabaseSchema": "Schema",
    "DatabaseMigrationHistoryTable": "__EFMigrationsHistory"
  },
  "JwtSettings": {
    "SecretKey": "f1d4e222-0b3b-2222-1111-1111b3b2b1b0b",
    "Issuer": "https://localhost:7210/",
    "Audience": "https://localhost:7210/",
    "ExpiresInMinutes": "60"
  }
}
```

## Autenticação e Autorização

Este template está configurado para utilizar **autenticação JWT (JSON Web Token)** para proteger os endpoints da API. Os usuários se autenticam usando email e senha, e um token JWT é gerado e retornado para ser usado nas requisições subsequentes.

### Fluxo de Autenticação

1. O cliente envia uma solicitação de **login** com as credenciais (email e senha).
2. Se as credenciais forem válidas, um token JWT é gerado.
3. O cliente deve incluir o token JWT no cabeçalho de todas as requisições subsequentes para acessar os recursos protegidos da API.

### Exemplo de Autenticação:

- **Endpoint**: `/api/users/authenticate`
- **Método**: `POST`
- **Payload**:
  ```json
  {
    "email": "admin@myapp.com",
    "password": "Admin@123"
  }
- **Resposta**:
    ```json
    {
        "token": "Token",
        "expiresInMinutes": "60"
    }
  ```

Uso do Token JWT
O token JWT retornado no processo de autenticação deve ser incluído no cabeçalho Authorization das requisições subsequentes para acessar os endpoints protegidos.
Exemplo de Cabeçalho HTTP com o Token:
Authorization: Bearer {jwt_token}

Validade do Token
O token JWT tem uma validade configurável, e após expirar, o cliente precisará fazer login novamente para obter um novo token.

Permissões e Papéis
O template também oferece suporte a autorização baseada em papéis (roles) e permissões:

As permissões são definidas no arquivo roles.json.
Os usuários autenticados são autorizados a acessar determinados recursos com base em seus papéis.
```json
{
  "roles": [
    {
      "name": "Admin",
      "permissions": [
        "CAN_USER_CHANGE_ROLE",
        "CAN_USER_DISABLE",
        "CAN_USER_ENABLE",
        "CAN_USER_GET_BY_ID",
        "CAN_USER_GET_BY_EMAIL",
        "CAN_USER_GET_ALL"
      ]
    },
    {
      "name": "User",
      "permissions": [
        "CAN_UPDATE_PROFILE"
      ]
    }
  ]
}
```

## Migrações e Usuário Administrador Padrão

Ao iniciar o projeto, as migrações do **Entity Framework** são aplicadas automaticamente, criando a estrutura necessária no banco de dados. Além disso, um **usuário administrador padrão** é criado com as credenciais definidas no arquivo `appsettings.json` ou `appsettings.development.json`.

### Aplicando Migrações

O Entity Framework está configurado para realizar o versionamento do banco de dados usando **migrações**. Para garantir que o banco de dados esteja atualizado com a estrutura mais recente, você pode rodar o comando a seguir:

Abra o Package Manager Console
Digite: *Update-database*
Esse comando irá aplicar todas as migrações pendentes e atualizar o banco de dados com as tabelas e colunas necessárias.

## Usuário Administrador Padrão
A primeira vez que a aplicação for executada, um usuário administrador será criado automaticamente no banco de dados. Esse usuário é útil para acessar funcionalidades administrativas sem necessidade de criar manualmente.

As credenciais do administrador padrão são definidas no arquivo appsettings.json ou appsettings.Development.json. Aqui está um exemplo de como configurar o usuário administrador
```json
"AdminSettings": {
  "Email": "admin@ibasolutions.com.br",
  "Password": "StrongPassword"
}
```

Ao rodar a aplicação pela primeira vez, o sistema verificará se o usuário administrador já existe. Caso não exista, ele será criado automaticamente com o email e senha especificados nas configurações.

## Como Executar

### Pré-requisitos:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (Utilizada PostgreSQL 15.1, compiled by Visual C++ build 1914, 64-bit)

### Passos:

1. Clone o repositório:

  ```bash
   git clone https://dev.azure.com/intelligent-business-automation/IBA/_git/Iba.Net.Backend.Template
  ```
2. Navegue até a pasta do projeto:
  ```bash
  cd Iba.Net.Backend.Template
  ```
3. Configure o arquivo appsettings.Development.json com a string de conexão para PostgreSQL e outras variáveis necessárias (como o usuário administrador e configurações de JWT). Exemplo de appsettings.Development.json para PostgreSQL:
  ```json
    {
        "ConnectionStrings": {
            "Default": "Host=localhost;Port=5432;Username=postgres;Password=P@ssw0rd;Database=postgres"
        },
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*",
        "AdminSettings": {
            "Email": "admin@ibasolutions.com.br",
            "Password": "StrongPassword"
        },
        "ConfigurationsSettings": {
            "DatabaseSchema": "Schema",
            "DatabaseMigrationHistoryTable": "__EFMigrationsHistory"
        },
        "JwtSettings": {
            "SecretKey": "f1d4e222-0b3b-2222-1111-1111b3b2b1b0b",
            "Issuer": "https://localhost:7210/",
            "Audience": "https://localhost:7210/",
            "ExpiresInMinutes": "60"
        }
    }
  ```

4. Execute as migrações para criar as tabelas no banco de dados PostgreSQL:
  Abra o Package Manager Console e digite Update-Database

5. Inicie a aplicação

## Contribuições
Contribuições são bem-vindas! Se você deseja adicionar novas funcionalidades, corrigir problemas ou melhorar o projeto, siga os passos abaixo:

1. Crie uma nova **branch** para a sua funcionalidade ou correção de bug:
  ```bash
  git checkout -b feature/nome-da-sua-feature
  ```

2. Faça as alterações necessárias no código e adicione as mudanças para commit:
  ```bash
  git add .
  ```

3. Faça o commit das suas alterações:
  ```bash
  git commit -m "Descrição das mudanças que você fez"
  ```

4. Envie as suas mudanças para o repositório remoto:
  ```bash
  git push origin feature/nome-da-sua-feature
  ```

5. Abra um Pull Request no repositório original, explicando detalhadamente o que você fez e por que suas mudanças são importantes.

6. Aguarde a revisão da equipe de mantenedores. Eles irão revisar o seu código e fornecer feedback ou aprovar suas mudanças.

## Regras de Contribuição
Mantenha o código bem documentado e organizado.
Certifique-se de que seu código siga as diretrizes de estilo e padrões do projeto.
Verifique se todas as funcionalidades implementadas estão cobertas por testes automatizados (se aplicável).
Sempre descreva claramente suas mudanças nos commits e no Pull Request.