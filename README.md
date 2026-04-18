# Good Hamburger

Sistema para registro de pedidos da lanchonete Good Hamburger, com API REST em ASP.NET Core, regras de desconto no dominio e frontend opcional em Blazor.

## Stack atual

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL (Npgsql)
- Swagger/OpenAPI
- xUnit (testes unitarios e integracao)
- Blazor Server (frontend opcional)

## Estrutura da solucao

- `GoodHamburger.API`: endpoints REST, configuracao e middleware
- `GoodHamburger.Core`: entidades, enums, DTOs e interfaces
- `GoodHamburger.Infrastructure`: EF Core, repositórios, migrations e servicos
- `GoodHamburger.Blazor`: interface web consumindo a API
- `GoodHamburger.Tests`: testes unitarios e de integracao

## Regras de negocio implementadas

Cardapio base:

- Sanduiches: X Burger (5,00), X Egg (4,50), X Bacon (7,00)
- Acompanhamento: Batata frita (2,00)
- Bebida: Refrigerante (2,50)

Descontos:

- Sanduiche + batata + refrigerante: 20%
- Sanduiche + refrigerante: 15%
- Sanduiche + batata: 10%

Validacoes:

- Pedido deve conter 1 sanduiche.
- Duplicidade nao e permitida por categoria (sanduiche, acompanhamento, bebida).
- Quantidades inconsistentes retornam erro claro (ex.: quantidade informada sem item selecionado).

## Endpoints principais

- `GET /api/menu`: consulta cardapio
- `GET /api/orders`: lista pedidos
- `GET /api/orders/{id}`: consulta pedido por id
- `POST /api/orders`: cria pedido
- `PUT /api/orders/{id}`: atualiza pedido
- `DELETE /api/orders/{id}`: remove pedido

Exemplo de payload para criar pedido:

```json
{
  "sandwich": "XBurger",
  "sideDish": "Fries",
  "drink": "Soda",
  "sandwichQuantity": 1,
  "sideDishQuantity": 1,
  "drinkQuantity": 1
}
```

Se enviar quantidade maior que 1 em qualquer categoria, a API retorna `400 Bad Request` com mensagem clara de duplicidade.

## Como executar

### 1) Subir banco com Docker (recomendado)

Na raiz do repositorio:

```bash
docker compose up -d
```

Isso sobe PostgreSQL e pgAdmin conforme `docker-compose.yml`.

### 2) Rodar a API

```bash
dotnet restore
dotnet run --project GoodHamburger.API
```

Swagger: `https://localhost:xxxx/swagger` (porta exibida no console).

### 3) Rodar o frontend Blazor (opcional)

```bash
dotnet run --project GoodHamburger.Blazor
```

## Testes

```bash
dotnet test
```

## Decisoes tecnicas

- Regras de desconto e validacao concentradas na entidade `Order` para manter o dominio coeso.
- Controladores tratam `ArgumentException` do dominio e retornam `400` com mensagem explicita.
- Persistencia abstraida via repositorio (`IOrderRepository`).
- Cardapio exposto via servico dedicado (`IMenuService`/`MenuService`).

## Limitacoes conhecidas

- Testes de integracao usam `WebApplicationFactory` sem banco de teste isolado dedicado; podem depender do ambiente local.
- Build/teste podem falhar se API/Blazor estiverem rodando e bloqueando assemblies no Windows.
- O frontend atual e focado em funcionalidade e nao cobre todos os cenarios de UX/erros da API.
