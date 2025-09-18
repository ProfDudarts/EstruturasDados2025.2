# Projetos de Estrutura de Dados II
Projetos de Estrutura de Dados II no 4º período de Engenharia de Computação do ano de 2025.2

## Objetivos:
O objetivo deste projeto é **implementar, aplicar e analisar diferentes estruturas de dados** estudadas ao longo da disciplina, utilizando **problemas práticos** como contexto para testar sua eficiência e funcionamento.

Mais do que desenvolver um sistema completo, o foco é que os alunos:
- Implementem corretamente as estruturas de dados (até árvores binárias como obrigatórias e outras definidas por sorteio).
- Demonstrem a aplicação prática das estruturas em cenários que evidenciem suas vantagens e limitações.
- Exercitem a lógica e o raciocínio computacional, destacando como o uso adequado de cada estrutura impacta na resolução de problemas.
- Apresentem resultados claros, comparando a utilização das diferentes estruturas e discutindo sua adequação.

## Requisitos:
- Atividade em **grupos de até 4 pessoas.**
- Cada grupo deve ter um líder.
- O versionamento deve ser feito com **Git**, no repositório https://github.com/ProfDudarts/EstruturasDados2025.2.
- Fluxo de versionamento:
  - A **branch dev** será a base para novas branches.
  - As entregas devem ser feitas via **Pull Request para dev.**
  - Só serão consideradas entregues as implementações que forem **mergeadas na main a partir da dev.**
    - O líder deve ser o aprovador destes **Pull Requests**
- A linguagem será definida por **sorteio** entre as seguintes opções:
  - [C](c/)
  - [C++](Grupos/Grupo_2/)
  - [Java](Java/)
  - [TypeScript](TS/)
  - [PHP](php/)
- Criar **documentação simples e objetiva**, incluindo:
  - Fluxos de uso incluindo notação **Big O**
  - Exemplos de execução
  - Breve explicação de cada estrutura implementada
- Considera-se que o código está implementado se:
  - Tiver todos os métodos necessários para o uso genérico da estrutura de dados;
  - Não conter bugs de implementação;
  - Implementar Execeções;
  - Implementar testes unitários com 100% de cobertura.
- A nota individual será baseada em:
  - **Participação registrada no repositório** (commits, pull requests, issues).
  - **Pergunta técnica** respondida durante a apresentação.

# Estruturas de Dados 
## Estruturas obrigatórias (todas as equipes devem implementar):
- Fila
- Pilha
- Lista Encadeada
- Lista Duplamente Encadeada
- Árvore Binária

## Estruturas sorteadas (cada equipe implementará uma):
- Árvore AVL
- Árvore B
- Árvore Vermelho-Preto
- Heap (Min-Heap ou Max-Heap)
- Tabela Hash
- Grafo
- Algoritmos de Ordenação (Bubble Sort, Selection Sort, Insertion Sort, Merge Sort, Quick Sort e Heap Sort)

# Apresentação
- Cada equipe terá 20 minutos:
  - **10 minutos** para apresentar a **estrutura sorteada**, explicando implementação e uso.
  - **10 minutos** para responder às **perguntas da banca** sobre o código.
- A banca será composta pelo professor e, se possível, professores convidados.

# Cronograma
| Data | Atividade |
| :--- | :--- |
| 26/08 | Definição das equipes, sorteio das linguagens e das estruturas exclusivas |
| 14/09 | **Entrega 01** – Implementação de Fila e Pilha |
| 28/09 | **Entrega 02** – Implementação de Lista Encadeada e Lista Duplamente Encadeada |
| 12/10 | **Entrega 03** – Implementação da Árvore Binária |
| 02/11 | **Entrega 04** – Implementação da Estrutura Sorteada |
| 04/11 | Apresentação do Projeto |
| 18/11 e 25/11 | Desafio final de implementação prática |

# Gabarito e Avaliação
| Item | Peso | Nota |
| :--- | :---: | :---: |
| Estruturas obrigatórias – Entrega 01 (Fila e Pilha) | 10% | 10,0 |
| Estruturas obrigatórias – Entrega 02 (Listas) | 10% | 10,0 |
| Estruturas obrigatórias – Entrega 03 (Árvore Binária) | 10% | 10,0 |
| Estrutura/algoritmo sorteado - Entrega 04 | 20% | 10,0 |
| Código limpo, organizado e documentado | 5% | 10,0 |
| Demonstração prática e apresentação em sala | 10% | 10,0 |
| Participação individual (estatísticas do GitHub) | 15% | 10,0 |
| Arguição individual (pergunta técnica) | 10% | 10,0 |
| Desafio de Aplicação da implementação* | 10% | 10,0 |

---
*Se houver alguma necessidade de ajuste de cronograma e não for possível fazer o desafio em sala, os 10% serão divididos entre a "Apresentaçao" (em grupo - 5%) e a "Arguição" (individual - 5%).*