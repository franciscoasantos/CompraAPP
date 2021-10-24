# CompraAPP
### Como executar o projeto
CompraApp requer [Docker](https://www.docker.com/) para rodar.
- Clonar o repositório;
- No diretório raíz do projeto, executar o comando:
```sh
docker-compose up --build
```
- Aguardar inicio dos containers;
- A API será hospedada no endereço: http://localhost:5000/swagger

### Instruções API
- Para uso dos endpoints Get - Aplicativo e Post - Pedido, um token deve ser recuperado, via endpoint Login.
- O token deve ser passado como header: Authorization: Bearer (token), conforme exemplo:
```sh
curl --location --request GET 'http://localhost:5000/api/Aplicativo' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE0MDY1MzE1NjEzIiwibmJmIjoxNjM1MDEwMzgyLCJleHAiOjE2MzUwMzkxODIsImlhdCI6MTYzNTAxMDM4Mn0.LkSZVKp8RfcHuT224Kn26Qm8aFWwlQ6Dvcoq2JODkU0'
```

### Logs
Os logs de exceptions serão salvos no diretório raíz de cada projeto (Consumer e API), na pasta Logs/

### Principais ferramentas utilizadas
 - [.NET 5 C#](https://dotnet.microsoft.com/)
 - [SqlServer](https://www.microsoft.com/pt-br/sql-server/)
 - [Apache Kafka](https://kafka.apache.org/)
 - [JsonWebToken](https://jwt.io/)
 - [Docker](https://www.docker.com/)
