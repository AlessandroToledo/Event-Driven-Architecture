# Event-Driven Architecture  
Sistema de aprendizado baseado em arquitetura orientada a eventos.

---

## ✅ Requisitos

Antes de começar, certifique-se de ter os seguintes recursos instalados:

- Uma IDE de sua preferência (Visual Studio, VS Code etc.)
- Docker (necessário para executar o Redis via container)
- Node.js instalado na máquina (necessário para rodar o front-end)

---

## 🚀 Como executar o projeto

### 1. Front-End

Após clonar o repositório, abra o terminal, navegue até a pasta `front` e execute os seguintes comandos:

```bash
npm install                      # Instala as dependências do projeto
npm install @microsoft/signalr   # Biblioteca de WebSocket para comunicação com o back-end
npm install piii piii-filters    # Biblioteca para filtragem de mensagens ofensivas
npm run dev                      # Inicia o servidor de desenvolvimento do front-end
```

### 2. Redis (Pub/Sub)

Com o Docker instalado, execute o seguinte comando para subir o container do Redis:

```bash
docker run -d --name redis -p 6379:6379 redis
```

Confirme se o container foi iniciado corretamente pela interface do Docker Desktop ou com:

```bash
docker ps
```

### 3. Back-End

Abra os seguintes projetos em sua IDE:

- `ChatApp`
- `LogWorker`
- `EmailWorker`

Execute os três projetos. Com isso, o sistema estará pronto para o envio e tratamento de mensagens.

---

## ⚙️ Funcionamento do Sistema

### 🗨️ Chat em Tempo Real

Na interface do front-end, qualquer usuário pode acessar o chat, definir um nome e começar a interagir com os demais participantes:

![Chat Interface](https://github.com/user-attachments/assets/86af5047-2a3d-400f-ab5e-afb183600867)

---

### 🔒 Filtro de Mensagens Ofensivas

Mensagens com conteúdo inadequado são automaticamente censuradas pelo front-end:

![Mensagem Censurada](https://github.com/user-attachments/assets/3edcc8a4-db45-4d17-957f-7456b99a66e2)

---

### 🔄 Comunicação via WebSocket

O front-end envia a seguinte estrutura de mensagem para o `ChatApp` via WebSocket:

![Mensagem WebSocket](https://github.com/user-attachments/assets/0a4b1013-aea5-4588-a65c-30c6241fe8d8)

> A flag `offensive` é definida como `true` quando o conteúdo contém palavras inapropriadas.

---

### 📢 Publicação de Eventos no Redis

O `ChatApp` publica eventos nos canais do Redis com base no conteúdo da mensagem:

#### Caso 1: Mensagem apropriada  
- O evento é publicado no canal de **Log**
- O `LogWorker` registra o seguinte log:

![Log de Mensagem Apropriada](https://github.com/user-attachments/assets/0a7bc768-78ac-466c-a053-db182d411917)

---

#### Caso 2: Mensagem ofensiva  
- Eventos são publicados em **dois canais**: Log e Email
- O `LogWorker` registra:

![Log de Mensagem Ofensiva](https://github.com/user-attachments/assets/8e764adb-1775-45f1-994c-7c9816bcb695)

- O `EmailWorker`, ao detectar o evento no canal de **Email**, simula o envio de uma notificação:

![Simulação de Envio de Email](https://github.com/user-attachments/assets/2fd7fbce-cbfb-4095-abc2-62e02340cbaf)

- Em seguida, um novo evento é publicado no canal de **Log**, e o `LogWorker` registra:

![Log de Envio de Email](https://github.com/user-attachments/assets/14bfdb36-f5ca-4242-bd55-b1b1b28484ee)

---

## 🧠 Conclusão

Esse sistema demonstra, de forma prática, como funciona uma **arquitetura orientada a eventos**, utilizando:

- WebSocket para comunicação em tempo real
- Redis para publicação e consumo de eventos
- Workers especializados para tratamento e registro de logs e notificações

Ideal para quem deseja entender e aplicar conceitos modernos de sistemas assíncronos e desacoplados.

---
