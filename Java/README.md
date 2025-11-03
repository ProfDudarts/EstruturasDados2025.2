=====================================================
           DOCUMENTAÇÃO – ESTRUTURAS DE DADOS
=====================================================

Projeto: Implementações de Estruturas de Dados em Java
Autores: [Lucas Kather Dziaduk, Pedros, Rafel Lindquist]
Linguagem: Java
Descrição: Implementação prática de diversas estruturas de dados fundamentais:
Pilha, Fila, Lista Encadeada Simples, Lista Duplamente Encadeada, Árvore Binária e Tabela Hash.

-----------------------------------------------------
1. FLUXO DE USO E COMPLEXIDADE (BIG-O)
-----------------------------------------------------

A seguir, o fluxo típico de uso de cada estrutura com suas operações
principais e complexidades de tempo (Big-O).

-----------------------------------------------------
1.1 PILHA<T>
-----------------------------------------------------
Tipo: LIFO (Last In, First Out)

Fluxo de uso:
1. Criar pilha → Pilha<Integer> pilha = new Pilha<>();
2. Adicionar elemento → pilha.push(valor);
3. Ver topo → pilha.peek();
4. Remover topo → pilha.pop();
5. Verificar se está vazia → pilha.isEmpty();

Operações e complexidades:
- push(valor): O(1)
- pop(): O(1)
- peek(): O(1)
- isEmpty(): O(1)
- size(): O(1)

-----------------------------------------------------
1.2 FILA<T>
-----------------------------------------------------
Tipo: FIFO (First In, First Out)

Fluxo de uso:
1. Criar fila → Fila<String> fila = new Fila<>();
2. Enfileirar → fila.enqueue(valor);
3. Ver primeiro → fila.peek();
4. Desenfileirar → fila.dequeue();
5. Verificar se está vazia → fila.isEmpty();

Operações e complexidades:
- enqueue(valor): O(1)
- dequeue(): O(1)
- peek(): O(1)
- isEmpty(): O(1)
- size(): O(1)

-----------------------------------------------------
1.3 LISTA ENCADEADA (Simples)
-----------------------------------------------------
Tipo: Lista Linear com ponteiros (cada nó aponta para o próximo)

Fluxo de uso:
1. Criar lista → ListaEncadeada lista = new ListaEncadeada();
2. Inserir → lista.inserir(valor);
3. Remover → lista.remover(valor);
4. Listar → lista.listar();

Operações e complexidades:
- inserir(valor): O(n)
- remover(valor): O(n)
- listar(): O(n)

-----------------------------------------------------
1.4 LISTA DUPLAMENTE ENCADEADA
-----------------------------------------------------
Tipo: Lista Linear com ponteiros duplos (cada nó aponta para o anterior e o próximo)

Fluxo de uso:
1. Criar lista → ListaDuplamenteEncadeada lista = new ListaDuplamenteEncadeada();
2. Inserir → lista.inserir(valor);
3. Remover → lista.remover(valor);
4. Listar na ordem → lista.listarFrente();
5. Listar reverso → lista.listarTras();

Operações e complexidades:
- inserir(valor): O(1)
- remover(valor): O(n)
- listarFrente(): O(n)
- listarTras(): O(n)

-----------------------------------------------------
1.5 ÁRVORE BINÁRIA DE BUSCA (BST)
-----------------------------------------------------
Tipo: Estrutura hierárquica onde cada nó possui até dois filhos
(esquerda: menores, direita: maiores).

Fluxo de uso:
1. Criar árvore → ArvoreBinaria arvore = new ArvoreBinaria();
2. Inserir → arvore.inserir(valor);
3. Buscar → arvore.buscar(valor);
4. Percorrer em ordem → arvore.emOrdem();

Operações e complexidades:
- inserir(valor): O(log n) média / O(n) pior caso
- buscar(valor): O(log n) média / O(n) pior caso
- emOrdem(): O(n)

-----------------------------------------------------
1.6 TABELA HASH<K,V>
-----------------------------------------------------
Tipo: Estrutura de mapeamento chave-valor com acesso rápido

Fluxo de uso:
1. Criar tabela → TabelaHash<String, Integer> tabela = new TabelaHash<>(capacidade);
2. Inserir → tabela.inserir(chave, valor);
3. Buscar → tabela.buscar(chave);
4. Remover → tabela.remover(chave);
5. Exibir → tabela.mostrar();

Operações e complexidades:
- inserir(chave, valor): O(1) média / O(n) colisões
- buscar(chave): O(1) média / O(n) colisões
- remover(chave): O(1) média / O(n) colisões
- mostrar(): O(n)

-----------------------------------------------------
2. EXEMPLO DE EXECUÇÃO (Main.java)
-----------------------------------------------------

Saída esperada ao executar o método main:

Fila → Primeiro que entra, primeiro que sai (FIFO).
Primeiro da fila: 1
Desenfileirando...
Novo primeiro: 2

Pilha → Último que entra, primeiro que sai (LIFO).
Topo da pilha: C
Pop: C
Novo topo: B

Lista Encadeada → Nós ligados por ponteiros simples.
10 -> 20 -> 30 -> null

Lista Duplamente Encadeada → Cada nó aponta para o anterior e o próximo.
A <-> B <-> C
Resultado reverso:
C <-> B <-> A

Árvore Binária de Busca → organiza dados hierarquicamente.
Percorrendo em ordem:
1 2 3 5 8

Tabela Hash → acessa valores rapidamente por chave.
0: [Daniel → 22] 
1: 
2: [Alice → 25] 
3: [Bruno → 30] 
4: [Carla → 28] 

-----------------------------------------------------
3. EXPLICAÇÃO DE CADA ESTRUTURA
-----------------------------------------------------

-----------------------------------------------------
3.1 PILHA<T>
-----------------------------------------------------
Descrição:
Estrutura de dados baseada no princípio LIFO (Last In, First Out),
onde o último elemento inserido é o primeiro a ser removido.

Implementação:
Utiliza LinkedList internamente e os métodos addFirst(), removeFirst() e getFirst().

Aplicações:
- Controle de chamadas de funções (pilha de execução)
- Desfazer/refazer (Ctrl+Z)
- Conversão de expressões matemáticas e verificação de parênteses

-----------------------------------------------------
3.2 FILA<T>
-----------------------------------------------------
Descrição:
Estrutura FIFO (First In, First Out). O primeiro elemento inserido é o primeiro a sair.

Implementação:
Usa LinkedList internamente com addLast(), removeFirst() e getFirst().

Aplicações:
- Filas de impressão, processos e atendimento
- Algoritmos de busca em largura (BFS)
- Controle de eventos em sistemas

-----------------------------------------------------
3.3 LISTA ENCADEADA (Simples)
-----------------------------------------------------
Descrição:
Estrutura linear em que cada nó contém um valor e uma referência para o próximo.
Permite inserção e remoção sem realocação de memória.

Implementação:
Classe No (valor + ponteiro para o próximo). 
O método inserir percorre até o fim para adicionar o novo nó.

Aplicações:
- Estruturas dinâmicas (sem tamanho fixo)
- Implementação de filas e pilhas dinâmicas
- Manipulação de listas de dados não contínuos na memória

-----------------------------------------------------
3.4 LISTA DUPLAMENTE ENCADEADA
-----------------------------------------------------
Descrição:
Semelhante à lista simples, mas cada nó tem referência para o anterior e o próximo.
Isso permite percorrer a lista nos dois sentidos.

Implementação:
Classe NoDuplo (valor + anterior + próximo).
Mantém ponteiros para o início e fim, permitindo inserção em O(1).

Aplicações:
- Navegação bidirecional (ex: histórico de navegador)
- Implementação de deque (fila dupla)
- Estruturas que exigem percorrer em ambas as direções

-----------------------------------------------------
3.5 ÁRVORE BINÁRIA DE BUSCA (BST)
-----------------------------------------------------
Descrição:
Estrutura hierárquica em que cada nó possui até dois filhos. 
Valores menores ficam à esquerda; valores maiores, à direita.

Implementação:
Inserção e busca recursivas.
O método emOrdem() percorre a árvore e imprime os elementos em ordem crescente.

Aplicações:
- Indexação e busca eficiente de dados
- Estruturas avançadas como AVL, Red-Black e Heaps
- Operações de ordenação e filtragem hierárquica

-----------------------------------------------------
3.6 TABELA HASH<K,V>
-----------------------------------------------------
Descrição:
Estrutura de mapeamento chave-valor com acesso rápido.
Cada índice da tabela contém uma lista encadeada para tratar colisões.

Implementação:
Classe interna Par<K,V> (chave + valor).
Métodos de inserção, busca, remoção e exibição usando LinkedList para colisões.

Aplicações:
- Armazenamento rápido de dados por chave
- Implementação de dicionários e caches
- Busca eficiente em grandes volumes de dados

-----------------------------------------------------
FIM DA DOCUMENTAÇÃO
=====================================================
