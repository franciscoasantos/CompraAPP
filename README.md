# CompraAPP
### Como executar o projeto
CompraApp requer [Docker](https://www.docker.com/) para rodar.
- Clonar o repositório;
- No diretório raíz do projeto, executar o comando:
```sh
docker-compose up --build
```
- Aguardar inicio dos containers;
- A API será hospedada no endereço: http://localhost:5000

### Logs
Os logs de exceptions serão salvos no diretório raíz de cada projeto (Consumer e API), na pasta Logs/

### Principais ferramentas utilizadas
 - [.NET 5 C#](https://dotnet.microsoft.com/)
 - [SqlServer](https://www.microsoft.com/pt-br/sql-server/)
 - [Apache Kafka](https://kafka.apache.org/)
 - [JsonWebToken](https://jwt.io/)
 - [Docker](https://www.docker.com/)
