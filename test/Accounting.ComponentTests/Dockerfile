FROM microsoft/dotnet:2.2-sdk
WORKDIR /testing

COPY ./ .

RUN dotnet restore

COPY . .

ENV RUN_ENV=docker 

CMD ["dotnet", "test"]