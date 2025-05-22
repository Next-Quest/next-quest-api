📘 Next Quest
Next Quest é um projeto desenvolvido com arquitetura em camadas e princípios de design orientados a objetos, aplicando os princípios SOLID e o padrão de projeto Strategy. O sistema organiza sua lógica em torno de entidades como User, Company e Post, com uma estrutura modular e de fácil manutenção.

🧱 Camadas da Arquitetura
O projeto está dividido nas seguintes camadas, com responsabilidades bem definidas:

Controller: Camada de entrada responsável por lidar com as requisições HTTP e orquestrar a comunicação entre as outras camadas.

Interface: Define os contratos que as implementações devem seguir, garantindo baixo acoplamento.

Service: Contém a lógica de negócio, sendo a principal responsável pelo fluxo das regras do sistema.

Model: Representa as entidades do domínio, como User, Company e Post.

DTO (Data Transfer Object): Objetos usados para transportar dados entre camadas, sem expor diretamente as entidades de domínio.

🧠 Padrões e Princípios Aplicados
✅ SOLID
S - Single Responsibility Principle (Princípio da Responsabilidade Única):
Cada classe do sistema tem uma única responsabilidade. Por exemplo, os services lidam apenas com regras de negócio e os DTOs com transporte de dados.

O - Open/Closed Principle (Aberto/Fechado):
As interfaces e serviços são projetados para permitir extensão sem modificação. Novas estratégias ou tipos de autenticação podem ser adicionados sem alterar o código existente.

L - Liskov Substitution Principle (Princípio da Substituição de Liskov):
Classes derivadas podem ser utilizadas no lugar de suas classes base sem afetar a funcionalidade. Isso é garantido pela separação clara de contratos nas interfaces.

I - Interface Segregation Principle (Princípio da Segregação de Interface):
As interfaces são específicas e enxutas, evitando obrigar classes a implementarem métodos que não utilizam.

D - Dependency Inversion Principle (Princípio da Inversão de Dependência):
As classes de alto nível não dependem diretamente de classes de baixo nível, mas sim de abstrações (interfaces), permitindo fácil substituição e injeção de dependências.

♟️ Strategy Pattern
O padrão Strategy é utilizado para lidar com diferentes métodos de autenticação no sistema, como demonstrado no diagrama de sequência do login. A lógica de autenticação é desacoplada e pode ser facilmente substituída ou expandida com novas estratégias (ex: OAuth, senha, biometria, etc.).

🧩 Classes Principais
User
Responsável por representar usuários do sistema.

Possui estados como "Administrador" ou "Usuário".

Company
Representa empresas publicadoras ou desenvolvedorsa às quais jogos podem estar vinculados.

Pode ter múltiplos jogos associados.

Post
Representa publicações feitas por usuários.

Pode conter comentários, curtidas e imagem associada.

🔄 Exemplos de Uso
Criação de Post: O controller chama o serviço por meio da interface, que utiliza os DTOs para validar os dados e cria a entidade Post.

Login: O controller seleciona a estratégia de autenticação adequada (implementação de AuthStrategy) e executa a lógica sem depender diretamente do tipo.
