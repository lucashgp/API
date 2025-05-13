# **Ebanx API**

Este projeto é uma aplicação para gerenciar contas bancárias, permitindo operações como depósito, saque, transferência e consulta de saldo, utilizando uma modelagem de software baseada DDD

## **Estrutura do Projeto**

O projeto está organizado da seguinte forma:

- **domain**: Contém as entidades do domínio e Interface.
- **application**: Contém objetos de transferência de dados para facilitar a comunicação entre as camadas (DTOs) e os serviços de aplicação que realizam a lógica de negócio, interagindo com os repositórios e serviços de domínio.
- **infrastructure**: Contém implementação do repositório, criando a interação com a fonte de dados.
- **API**: Contém os Controladores responsáveis por lidar com as requisições HTTP e configurações para executar o projeto.

Ebanx-api/

- Domain/
   - Entities/
       - Account.cs             
   - Interfaces/
       - IAccountRepository.cs 

- Application/
   - Services/
       - AccountService.cs 
   - DTOs/
       - AccountDTO.cs 

- API/
   - Controllers/
       - AccountController.cs  
   - Program.cs                  


## **Uso**

### Iniciar o Servidor

O servidor será iniciado e estará disponível em [http://localhost:5030](http://localhost:5030/).
O programa conta com o auxilio do Swagger para poder realizar a chamada dos EndPoints, disponivel em [http://localhost:5030/swagger/index.html](http://localhost:5030/swagger/index.html).

### **Endpoints Disponíveis**

#### **Resetar Banco de Dados**

- **Endpoint:** `POST /reset`
- **Descrição:** Reseta o banco de dados para o estado inicial.

#### **Consultar Saldo**

- **Endpoint:** `GET /balance?account_id={id}`
- **Descrição:** Retorna o saldo da conta especificada.
- **Resposta:**
  - `200`: Retorna o saldo.
  - `404`: Conta não encontrada.

#### **Realizar Operações**

- **Endpoint:** `POST /event`
- **Descrição:** Realiza operações como depósito, saque e transferência.
- **Body:**
  ```json
  {
    "type": "deposit | withdraw | transfer",
    "origin": "id da conta de origem (opcional para depósito)",
    "destination": "id da conta de destino (opcional para saque)",
    "amount": "valor da operação"
  }
  ```
- **Resposta:**
  - `201`: Operação realizada com sucesso.
  - `404`: Conta não encontrada.
  - `400`: Tipo de operação inválido.
