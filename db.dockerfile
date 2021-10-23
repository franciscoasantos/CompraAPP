FROM mcr.microsoft.com/mssql/server:2019-latest

ENV SA_PASSWORD EFwt0PkuqbstVH
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

WORKDIR /app
COPY scripts/run-initialization.sh /app
COPY scripts/db-entrypoint.sh /app
COPY scripts/load-database.sql /app

CMD /bin/bash db-entrypoint.sh
EXPOSE 1433