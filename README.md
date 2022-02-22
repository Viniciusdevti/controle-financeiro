
<h1 align="center"> 
	Controle financeiro
</h1>

<p align="center">
  <a href="#-project-theme">Tema do projeto</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
 <a href="#construction-Como-executar-o-projeto">Como executar o projeto</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#pushpin-technologies">Tecnologias</a>

</p>



## 💻 Tema do projeto

# Controle Financeiro

Projeto foi desenvolvido, foi uma api de controle de finaças. Onde é possivel fazer o crud categorias, subcategorias e lançamentos, positivos e negativos. Também é possivel fazer um balanço para saber as despesas, receita e o saldo.



## :construction: Como executar o projeto

<p align="center">
	
	Para executar o projeto faça o clone no repositório: https://github.com/Viniciusdevti/controle-financeiro.git.

Faça checkout na branch main.

Abra o visual studio selecione o projeto da api e click em executar.
 <img   src="https://github.com/Viniciusdevti/assets/blob/main/controleFinanceiro/iniciar%20projeto.png">
	
Para configurar as informações do banco de dados:

Caso utilize o proprio sql server altere a CONNECTIONSTRING no arquivo appsettings.json.
Ex:
"SQLSERVER": {
    "CONNECTIONSTRING": "Server=localhost,1433;Database=controleFinanceiro;User ID=sa;Password=fakePassw0rd"
  },
  "ConnectionStrings": {
    "SERILOGS": "Server=localhost,1433;Database=controleFinanceiro;User ID=sa;Password=fakePassw0rd"

Também é possivel configurar pelo docker pelos os comandos(caso utilize pelo docker. não será necessario alterar a connection string:

docker pull viniciusdevti/controle_financeiro_db

docker run -t -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=fakePassw0rd' -p 1433:1433 --name sql1 -d viniciusdevti/controle_financeiro_db

Depois disso execute o script sql presente no init.sql

Caso não tenha o sqlserver instalado, pode ser usado o docker.

Execute os comandos abaixo.

Comandos docker
docker pull viniciusdevti/controle_financeiro_db

docker run -t -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=fakePassw0rd' -p 1433:1433 --name sql1 -d viniciusdevti/controle_financeiro_db

execute o comando: 	 docker exec -it sql1 "bash"
execute o comando: 	 uname -a
execute o comando: 	 cd /opt/mssql-tools/bin
execute o comando:	 pwd
execute o comando:	 ./sqlcmd -S localhost -U SA

Digite a senha do banco: fakePassw0rd (precione ENTER).

	1 create database controleFinanceiro
	2 go

	1 use controleFinanceiro
	2 go

	1 CREATE TABLE [Categoria]([IdCategoria]  bigInt IDENTITY(1, 1) NOT NULL PRIMARY KEY CLUSTERED,[Nome] nvarchar(300) UNIQUE NOT NULL,[Ativo] bit NOT NULL)
	2 go

	1 CREATE TABLE [SubCategoria([IdSubCategoria]  bigInt IDENTITY(1, 1) NOT NULL PRIMARY KEY CLUSTERED,	[Nome] nvarchar(300) UNIQUE NOT NULL, [IdCategoria] bigInt NOT 		NULL FOREIGN KEY REFERENCES Categoria ([IdCategoria]),[Ativo] bit NOT NULL)
	2 go

	1 CREATE TABLE [dbo].Lancamento([IdLancamento]  bigInt IDENTITY(1, 1) NOT NULL PRIMARY KEY CLUSTERED,	[Valor] float NOT NULL,	[DATA] datetime NOT NULL,[IdSubCategoria]         bigInt NOT NULL FOREIGN KEY REFERENCES [dbo].SubCategoria ([IdSubCategoria]),	[Comentario] nvarchar(300) NULL,[Ativo] bit NOT NULL)
	2 go

Para executar as requisições no swagger adicione a API-KEY aXRhw7o=.
 <img   src="https://github.com/Viniciusdevti/assets/blob/main/controleFinanceiro/Authorize01.png"> 
 <img   src="https://github.com/Viniciusdevti/assets/blob/main/controleFinanceiro/Authorize02.png">

Para acessar os logs existe uma tabela no banco de dados chamada logs, lá é possível verificar os logs da aplicação.

Para acessar o status da aplicação basta acessar o EndPoint Status. Será retornado um json com o status da aplicação.
<img   src="https://github.com/Viniciusdevti/assets/blob/main/controleFinanceiro/Status01.png"> 
	
 Para acessar de forma gráfica acesse o EndePoint monitor.
  <img   src="https://github.com/Viniciusdevti/assets/blob/main/controleFinanceiro/Status02.png"> 
Para executar os testes basta abrir o gerenciador de testes do visual studio.
	 <img   src="https://github.com/Viniciusdevti/assets/blob/main/controleFinanceiro/testes.png"> 


Os testes devem ser executados por cada classe de teste, por exemplo: balancoController => executar
	
 <img   src="https://github.com/Viniciusdevti/assets/blob/main/controleFinanceiro/testes02.png"> 
	
	
</p>


## :pushpin: Technologies
Tecnologias utilizadas:

* C# :white_check_mark:
* .NET 5.0 :white_check_mark:
* AspNet Core :white_check_mark:
* AspNet healthchecks :white_check_mark:
* Serilog :white_check_mark:
* Entity Framework:white_check_mark:
* Sqlserver :white_check_mark:



