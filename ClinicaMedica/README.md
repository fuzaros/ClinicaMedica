# 🏥 Sistema de Gestão de Clínica Médica

![.NET](https://img.shields.io/badge/.NET%208.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC292B?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

Um sistema robusto e moderno para gerenciamento de clínicas médicas, desenvolvido em ASP.NET Core MVC com Entity Framework Core. O sistema permite o controle completo do fluxo de atendimento, desde o cadastro detalhado de pacientes até o agendamento estruturado de consultas médicas, garantindo a integridade dos dados por meio de chaves estrangeiras e regras de negócio direto no banco de dados SQL Server.

---

## 🚀 Recursos Principais

* **👥 Gestão de Pacientes:** Cadastro completo contendo Nome, CPF, Data de Nascimento, Telefone, E-mail e data de registro automatizada.
* **📅 Agendamento de Consultas:** Vínculo relacional seguro entre a consulta e o paciente. Controle de Especialidade Médica, Nome do Médico, Data/Horário e espaço dedicado para Observações e Sintomas clínicos.
* **🛡️ Segurança de Dados:** Implementação de proteção contra ataques de *Overposting* através do mapeamento explícito com atributos `[Bind]` nos controladores.
* **🎨 Interface Responsiva:** Visual limpo e simétrico construído com Bootstrap, adaptado para quebras de linha e leitura vertical confortável em qualquer tamanho de tela (Desktop ou Mobile).

---

## 🛠️ Tecnologias e Arquitetura

O projeto foi edificado sobre as melhores práticas do ecossistema Microsoft:

* **Linguagem:** C# (Object-Oriented Programming)
* **Framework Web:** ASP.NET Core MVC (Model-View-Controller)
* **Mapeamento Otimizado (ORM):** Entity Framework Core
* **Banco de Dados:** Microsoft SQL Server
* **Renderização Dinâmica:** Razor Engine (HTML5/CSS3)

---

## ⚙️ Como Executar o Projeto

### Pré-requisitos
* Visual Studio (18.7.0) ou VS Code.
* SDK do .NET (10.0) Core instalado.
* Instância do SQL Server ativa.

### Passo a Passo

1. **Clonar o Repositório:**
   ```bash
   git clone [https://github.com/fuzaros/ClinicaMedica](https://github.com/fuzaros/ClinicaMedica.git)

2. **Configurar a String de Conexão:**
   O projeto já vem configurado de fábrica para usar o LocalDB do Visual Studio. Caso utilize outra instância do SQL Server, certifique-se de ajustar a linha `ClinicaMedicaContext` no seu arquivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "ClinicaMedicaContext": "Server=(localdb)\\mssqllocaldb;Database=ClinicaMedicaContext;Trusted_Connection=True;MultipleActiveResultSets=true"
   }