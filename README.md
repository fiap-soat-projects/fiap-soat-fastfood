# 🍔 FastFood API

Este projeto foi desenvolvido para o curso de [pós-graduação em Arquitetura de Software (Soat Póstech) da FIAP](https://postech.fiap.com.br/curso/software-architecture/).

A API presente nesse repositório disponibiliza rotas para gerenciamento de clientes, cardápio, pedidos e pagamento, com integração direta com [MongoDb](https://www.mongodb.com/) e [Mercado Pago](https://www.mercadopago.com.br/developers/pt/reference)

## 🏃 Integrantes do grupo 21

- Jeferson dos Santos Gomes - **RM 362669**
- Jamison dos Santos Gomes - **RM 362671**
- Alison da Silva Cruz - **RM 362628**

## 📜 Linguagem Ubíqua 

Para mais detalhes sobre a linguagem do domínio, consulte [`docs/ubiquitous-language.md`](docs/ubiquitous-language.md).

## 🗂️ Diagramas do Projeto

Os seguintes diagramas estão disponíveis no diretório [`/diagrams`](diagrams):

- **Diagrama de Contexto:** Apresenta uma visão geral dos sistemas e atores que interagem com a FastFood API, destacando integrações externas e fluxos principais.

- **Event Storming:** Ilustra os principais eventos de negócio, comandos e agregados do domínio, facilitando o entendimento dos fluxos e regras do sistema.

- **Domain Storytelling:** Mostra narrativas visuais dos processos de negócio, detalhando como os diferentes atores interagem com o sistema em cenários típicos.

Consulte o diretório [`/diagrams`](diagrams) para visualizar os arquivos e obter mais detalhes sobre cada diagrama.

## 👨‍💻 Tecnologias Utilizadas

- **.NET 8** (C# 12)
- **ASP.NET Core Web API**
- **MongoDB** (banco de dados)
- **Mongo Express** (client web para MongoDB)
- **Docker** e **Docker Compose**
- **Polly** (resiliência HTTP)
- **Swagger** (documentação automática)
- **MercadoPago** (integração de pagamentos via Pix)

## 🏁 Como Inicializar

### Pré-requisitos

- 🐳 Instalação do [Docker](https://www.docker.com/get-started/)

### Passos

1. Clone o repositório:

2. No diretório raíz do projeto, utilize uma ferramenta de linha de comando de sua preferência e execute o comando `docker-compose up --build`

3. Acesse a API e seus recursos:
   - API: [http://localhost:8080/swagger](http://localhost:8080/swagger)
   - Mongo Express: [http://localhost:8081](http://localhost:8081)

## Endpoints Disponíveis

### 🍔 Order (Pedido)
- `GET /Order?page=1&size=10&status=Received` — Listar pedidos em página
- `GET /Order/{id}` — Detalhar pedido
- `POST /Order` — Criar pedido
- `PATCH /Order/{id}/status` — Atualizar status do pedido
- `DELETE /Order/{id}` — Remover pedido

### 💸 Transaction (Pagamento)
- `POST /Order/{id}/checkout` — Iniciar checkout/pagamento
- `POST /Order/{id}/confirm-payment` — Confirmar pagamento

### 🤖 SelfOrdering (Cliente)
- `GET /SelfOrdering/customer/{id}` — Buscar cliente por ID
- `GET /SelfOrdering/customer/{cpf}` — Buscar cliente por CPF
- `POST /SelfOrdering/customer` — Registrar cliente

### 📲 Menu (Cardápio)
- `GET /Menu/{id}` — Detalhar item do cardápio
- `GET /Menu?name=string&category=0&skip=0&limit=10` — Listar itens do cardápio
- `POST /Menu` — Cadastrar item no cardápio
- `PUT /Menu/{id}` — Atualizar item do cardápio
- `DELETE /Menu/{id}` — Remover item do cardápio

Se preferir, as requisições descritas acima podem ser acessadas via [postman](https://www.postman.com/) por meio da seguinte collection:

- [fiap-soat-fastfood](https://www.postman.com/jefersondsgomes/workspace/fiap-soat-fastfood/collection/7741479-dde54050-3ced-4dcb-830c-bf6e9ec5a8da?action=share&creator=7741479&active-environment=7741479-37d60702-c589-45f8-834c-83c5462c84e7)

## 👤 Convenções

- Todos os endpoints aceitam e retornam JSON.
- Utilize o Swagger para explorar e testar os endpoints.

## 🏦 Banco de Dados

- O MongoDB inicializa com uma seed de dados para um cardápio pré preenchido. Isso ocorre via script em `scripts/init-db.js`.
- Usuário padrão: `fastfooduser` / `f4sTf00dP4ssW0rd!`
- Admin: `admin` / `admin` (para Mongo Express)

---