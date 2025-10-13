import { Fila } from './Models/fila.js';
import {No} from './Models/no.js';
import {Pilha} from './Models/pilha.js';
import { ArvoreBinaria } from './Models/ArvoreBinaria.js';

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