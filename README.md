# Descrição

Projeto .NET Core 8 de exemplo criado para interagir com Entity Framework e Migrations e demonstrar suas funcionalidades. Como base de dados é utilizado o SQLite representado pelo arquivo app.db na raiz do projeto. Este projeto contém também o Swagger para a documentação de seus endpoints.

Como funcionalidade essa API apresenta a possibilidade de efetuar um CRUD de registros de lançamentos financeiros conténdo campos como descrição, valor, data lançamnento e o tipo de lançamento (RECEITA / DESPESA).

Cada lançamento pode ter zero ou mais categorias (denomidada como tag no projeto). 

# Comandos úteis criação do Projeto

dotnet new web -o FinanceControl -f net8.0

dotnet add package Microsoft.EntityFrameworkCore.Sqlite (SqlServer / MySql /  PgSql)

dotnet add package Microsoft.EntityFrameworkCore.Design (Migrações)

Obs:. Verificar se a ferramenta dotnet-ef está instalada

dotnet ef --version

dotnet tool install --global dotnet-ef

dotnet tool install --global dotnet-ef --version x.x.x

dotnet tool update --global dotnet-ef

# Comandos úteis Migration

dotnet clean

dotnet build

dotnet ef migrations add InitialCreate

dotnet ef database update (boa prática aplicar sempre após realizar ajustes na migração)

dotnet ef migrations remove (remove última migração)





