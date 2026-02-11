# 📱 RedeSocial API

API REST desenvolvida em **ASP.NET Core**, utilizando os padrões **Clean Architecture** e **CQRS**, com autenticação via **JWT**, foco em boas práticas, segurança, escalabilidade e organização de código.

---

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, com separação clara de responsabilidades:

---

## 🧠 CQRS (Command Query Responsibility Segregation)

O projeto separa claramente **operações de escrita** e **leitura**.

---

## ⚙️ Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- MediatR
- AutoMapper
- JWT Bearer Authentication
- mySQL
- Swagger

---

## 📑 Documentação

A API possui documentação via **Swagger**, permitindo:
- Visualizar endpoints
- Testar requisições
- Autenticar usando JWT

---

## 🚀 Como Executar

1. Clone o repositório
2. Configure a connection string no `appsettings.json`
3. Execute as migrations:
   ```bash
   dotnet ef database update
