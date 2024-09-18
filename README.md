Monitoramento Ambiental

Este projeto é uma aplicação ASP.NET Core que monitora dados ambientais, como sensores e previsões de chuva,
utilizando MongoDB para armazenamento. O projeto inclui containerização com Docker e orquestração com Docker Compose.

Sumário
Descrição
Pré-requisitos
Configuração do Ambiente
Executando a Aplicação
Executando os Testes
Docker
Construindo a Imagem Docker
Executando com Docker Compose
Configuração do Pipeline CI/CD
Licença

Descrição

Esta aplicação ASP.NET Core fornece um conjunto de APIs para gerenciar dados relacionados, sensores ambientais, 
leituras, previsões de chuva, controle de irrigação e alertas. Utiliza MongoDB Atlas como banco de dados e foi containerizada usando Docker.

Pré-requisitos
Certifique-se de ter as seguintes ferramentas instaladas:

.NET 8.0 SDK
Docker
Docker Compose
Git

Configuração do Ambiente

1. Clone o repositório:

bash
git clone https://github.com/seu-usuario/monitoramento_ambiental_mongodb.git
cd monitoramento_ambiental_mongodb

2. Crie um arquivo .env na raiz do projeto com as variáveis de ambiente necessárias:

MONGODB_URI=mongodb+srv://<username>:<password>@cluster0.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0
DATABASE_NAME=monitoramento_ambiental
SENSOR_COLLECTION_NAME=sensores
LEITURA_COLLECTION_NAME=leituras
PREVISAO_CHUVA_COLLECTION_NAME=previsoes_chuva
CONTROLE_IRRIGACAO_COLLECTION_NAME=controle_irrigacoes
ALERTA_COLLECTION_NAME=alertas

Executando a Aplicação

1. Executar a aplicação localmente:

	*Certifique-se de que o .NET SDK está instalado.**

	*Execute o comando:*
	bash
	dotnet run

	*A aplicação estará disponível em http://localhost:8080.*

2. Executar com Docker Compose:
			
   *Certifique-se de que Docker e Docker Compo*

   *Construa e inicie os containers:se estão instalados.*
    bash
    docker-compose up --build

	*A aplicação estará disponível em http://localhost:8080.*

Executando os Testes

1. Testes Locais:

	*Execute o comando:*
	bash
	dotnet test

2. Testes no Docker:

   *Testes podem ser executados dentro de um container Docker se configurado no Dockerfile.*

Docker

Construindo a Imagem Docker

   *Para construir a imagem Docker do projeto, execute o comando:*
    bash
    docker build -t nome-do-usuario/monitoramento_ambiental_mgdb:v1 .

Executando com Docker Compose

   *Para executar o projeto usando Docker Compose, utilize o arquivo docker-compose.yml já configurado na raiz do projeto. Execute:*
    bash
	docker-compose up

Configuração do Pipeline CI/CD

O pipeline de CI/CD foi configurado para compilar o código, testar e realizar o deploy contínuo. Ele utiliza GitHub Actions para 
integração contínua e deploy contínuo para ambientes de staging e produção.

Configuração do GitHub Actions

*O pipeline está definido no arquivo .github/workflows/ci-cd.yml.*
*Certifique-se de configurar os segredos do repositório no GitHub para armazenar credenciais do Docker Hub e outros detalhes sensíveis.*


Segurança de Credenciais

*Para manter suas credenciais seguras, utilize variáveis de ambiente ou serviços de gerenciamento de segredos em seus pipelines CI/CD. 
Evite expor credenciais sensíveis diretamente em scripts ou arquivos de configuração.*

Licença

*Este projeto está licenciado sob a Licença MIT (Massachusetts Institute of Technology).*
MIT License

Copyright (c) 2024 Wagner Machado

A permissão é concedida, gratuitamente, a qualquer pessoa que obtenha uma cópia deste software e arquivos de documentação associados (o "Software"), 
para lidar no Software sem restrição, incluindo, sem limitação, os direitos de usar, copiar, modificar, mesclar, publicar, distribuir, sublicenciar e/ou 
vender cópias do Software, sujeito às seguintes condições: O aviso de direitos autorais acima e este aviso de permissão devem ser incluídos em todas as 
cópias ou partes substanciais do Software.
O SOFTWARE É FORNECIDO "COMO ESTÁ", SEM GARANTIAS OU CONDIÇÕES DE QUALQUER TIPO, EXPRESSAS OU IMPLÍCITAS, INCLUINDO, MAS NÃO SE LIMITANDO A,
GARANTIAS IMPLÍCITAS DE COMERCIALIZAÇÃO, ADEQUAÇÃO A UM FIM ESPECÍFICO E NÃO INFRAÇÃO. EM NENHUM CASO OS AUTORES OU DETENTORES DOS DIREITOS AUTORAIS 
SERÃO RESPONSÁVEIS POR QUALQUER RECLAMAÇÃO, DANO OU OUTRA RESPONSABILIDADE, SEJA EM AÇÃO DE CONTRATO, TORT OU OUTRA, DECORRENTE DE OU EM CONEXÃO COM 
O SOFTWARE OU O USO OU OUTRO TIPO DE OPERAÇÃO NO SOFTWARE.
