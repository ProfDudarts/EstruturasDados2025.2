import { Fila } from './Models/Fila';
import {No} from './Models/no';
import {Pilha} from './Models/pilha';
import { ArvoreBinaria } from './Models/ArvoreBinaria';
import { ListaDuplamenteEncadeada} from './Models/ListaDuplamenteEncadeada'

const minhaFila = new Fila<number>();

console.log('A fila está vazia?', minhaFila.vazio());

console.log('Inserindo os elementos 10, 20 e 30...');
minhaFila.inserir(10);
minhaFila.inserir(20);
minhaFila.inserir(30);

console.log('--- Conteúdo da fila ---');
console.log(minhaFila.listar());

console.log('Tamanho atual da fila:', minhaFila.tamanho_fila());

console.log('Elemento no topo (primeiro):', minhaFila.frente());

console.log('Removendo o primeiro elemento:', minhaFila.apagar());
console.log('Novo primeiro elemento:', minhaFila.frente());

console.log('--- Conteúdo da fila após remover ---');
console.log(minhaFila.listar());

console.log('Tamanho atual da fila:', minhaFila.tamanho_fila());

const minhaPilha = new Pilha<string>();

minhaPilha.inserir('cleive');
minhaPilha.inserir('rafael');
minhaPilha.inserir('lucas');

console.log('Tamanho da pilha:', minhaPilha.tamanho_pilha());
console.log('Elemento no topo:', minhaPilha.topo_valor());

console.log('--- Conteúdo da pilha ---');
console.log(minhaPilha.listar());

console.log('Apagando o topo:', minhaPilha.apagar());
console.log('Novo topo:', minhaPilha.topo_valor());

console.log('A pilha está vazia?', minhaPilha.empty());

///////////////////////////////////////////////////////
// Árvore Binária de Busca
///////////////////////////////////////////////////////

const minhaArvore = new ArvoreBinaria<number>();
console.log('A árvore está vazia?', minhaArvore.vazio());

console.log('Inserindo os elementos 20, 10 e 30...');
minhaArvore.inserir(20);
minhaArvore.inserir(10);
minhaArvore.inserir(30);

console.log('--- Conteúdo da árvore (In-Order) ---');
console.log(minhaArvore.listarInOrder());

console.log('Removendo 20:', minhaArvore.remover(20));
console.log('Novo conteúdo:', minhaArvore.listarInOrder());

console.log('Tentando remover 50 (não existe):', minhaArvore.remover(50));

///////////////////////////////////////////////////////
// Testes Lista Duplamente Emcadeada
///////////////////////////////////////////////////////

console.log("--- Teste de Funcionalidades Básicas e Ordenação (Merge Sort) ---");

const lista = new ListaDuplamenteEncadeada<number>();

lista.adicionar(50);
lista.adicionar(20);
lista.adicionar_inicio(80); // [80, 50, 20]
lista.inserir_na_posicao(10, 3); // [80, 50, 20, 10]
lista.inserir_na_posicao(90, 2); // [80, 50, 90, 20, 10]

lista.imprimir_frente();

console.log("\n--- Teste de Quick Sort ---");
lista.ordenar_por_quick_sort();
lista.imprimir_frente(); // Deve ser: [10, 20, 50, 80, 90]

console.log("\n--- Teste de Bubble Sort ---");
lista.adicionar(5);
lista.adicionar_inicio(100); // [100, 10, 20, 50, 80, 90, 5]
lista.imprimir_frente();
lista.ordenar_por_bubble_sort();
lista.imprimir_frente(); // Deve ser: [5, 10, 20, 50, 80, 90, 100]

console.log("\n--- Teste de Insertion Sort ---");
lista.adicionar(1);
lista.adicionar(75);
lista.imprimir_frente(); // [5, 10, 20, 50, 80, 90, 100, 1, 75]
lista.ordenar_por_insertion_sort();
lista.imprimir_frente(); // Deve ser: [1, 5, 10, 20, 50, 75, 80, 90, 100]

console.log("\n--- Teste de Merge Sort ---");
lista.adicionar(45);
lista.adicionar_inicio(99);
lista.remover(1); // Remove 1
lista.remover_na_posicao(5); // Remove 75
lista.imprimir_frente();
lista.ordenar_por_merge_sort();
lista.imprimir_frente(); // Deve ser: [5, 10, 20, 45, 50, 80, 90, 99, 100]

lista.imprimir_tras();

console.log("\n--- Teste de Selection Sort ---");
lista.adicionar_inicio(1000); // Adiciona um elemento grande
lista.imprimir_frente();
lista.ordenar_por_selection_sort();
lista.imprimir_frente();
// Deve ser: [5, 10, 20, 45, 50, 80, 90, 99, 100, 1000]
