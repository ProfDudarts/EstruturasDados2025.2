#include <stdio.h>
#include <stdlib.h>
#include <locale.h>
#ifdef _WIN32
#include <windows.h>
#endif
#include "Pilha/pilha.h"
#include "Fila/fila.h"
#include "ListaEncadeada/lista_encadeada.h"
#include "ListaDuplamenteEncadeada/lista_duplamente_encadeada.h"
#include "ArvoreBinaria/arvore_binaria.h"
#include "Grafo/Grafo.h"

// Utilitarios de I/O
static void print_divider(const char* titulo) {
 printf("\n==============================================================\n");
 if (titulo && *titulo) {
 printf("%s\n", titulo);
 printf("--------------------------------------------------------------\n");
 }
}

static int ler_int(const char* prompt, int* out) {
 char buf[128];
 for (;;) {
 if (prompt) printf("%s", prompt);
 if (!fgets(buf, sizeof buf, stdin)) return 0;
 char* end = NULL;
 long v = strtol(buf, &end,10);
 if (end != buf) { *out = (int)v; return 1; }
 printf("Entrada invalida. Digite um numero inteiro.\n");
 }
}

static void esperar_enter(void) {
 printf("\nPressione Enter para continuar...");
 int c; while ((c = getchar()) != '\n' && c != EOF) {}
}

// Impressoes auxiliares (para estruturas que nao possuem funcao de imprimir)
static void imprimir_pilha(Pilha* p) {
 printf("Pilha (topo -> base): [");
 NoPilha* n = p ? p->topo : NULL;
 while (n) {
 printf("%d", n->dado);
 n = n->proximo; if (n) printf(", ");
 }
 printf("]\n");
}

static void imprimir_fila(Fila* f) {
 printf("Fila (inicio -> fim): [");
 NoFila* n = f ? f->inicio : NULL;
 while (n) {
 printf("%d", n->dado);
 n = n->proximo; if (n) printf(", ");
 }
 printf("]\n");
}

// Submenus interativos
static void submenu_pilha(void) {
 print_divider("Pilha (LIFO) - Menu");
 Pilha* p = criar_pilha();
 for (;;) {
 printf("\n1) Empilhar\n2) Desempilhar\n3) Ver pilha\n4) Esvaziar\n0) Voltar\nEscolha: ");
 int op; if (!ler_int("", &op)) break;
 if (op ==0) { deletar_pilha(p); return; }
 switch (op) {
 case 1: { int v; ler_int("Valor: ", &v); empilhar(p, v); imprimir_pilha(p); break; }
 case 2: {
 if (pilha_vazia(p)) { printf("Pilha vazia.\n"); break; }
 int v = desempilhar(p); printf("Desempilhado: %d\n", v); imprimir_pilha(p); break; }
 case 3: imprimir_pilha(p); break;
 case 4: while (!pilha_vazia(p)) (void)desempilhar(p); printf("Pilha esvaziada.\n"); break;
 default: printf("Opcao invalida.\n");
 }
 }
}

static void submenu_fila(void) {
 print_divider("Fila (FIFO) - Menu");
 Fila* f = criar_fila();
 for (;;) {
 printf("\n1) Enfileirar\n2) Desenfileirar\n3) Ver fila\n4) Esvaziar\n0) Voltar\nEscolha: ");
 int op; if (!ler_int("", &op)) break;
 if (op ==0) { deletar_fila(f); return; }
 switch (op) {
 case 1: { int v; ler_int("Valor: ", &v); enfileirar(f, v); imprimir_fila(f); break; }
 case 2: {
 if (fila_vazia(f)) { printf("Fila vazia.\n"); break; }
 int v = desenfileirar(f); printf("Desenfileirado: %d\n", v); imprimir_fila(f); break; }
 case 3: imprimir_fila(f); break;
 case 4: while (!fila_vazia(f)) (void)desenfileirar(f); printf("Fila esvaziada.\n"); break;
 default: printf("Opcao invalida.\n");
 }
 }
}

static void submenu_lista_encadeada(void) {
 print_divider("Lista Encadeada - Menu");
 ListaEncadeada* lst = criar_lista();
 for (;;) {
 printf("\n1) Inserir inicio\n2) Inserir fim\n3) Inserir na posicao\n4) Remover inicio\n5) Remover fim\n6) Remover na posicao\n7) Buscar valor\n8) Imprimir\n9) Tamanho\n0) Voltar\nEscolha: ");
 int op; if (!ler_int("", &op)) break;
 if (op ==0) { deletar_lista(lst); return; }
 switch (op) {
 case 1: { int v; ler_int("Valor: ", &v); inserir_inicio(lst, v); imprimir_lista(lst); break; }
 case 2: { int v; ler_int("Valor: ", &v); inserir_fim(lst, v); imprimir_lista(lst); break; }
 case 3: { int v, pos; ler_int("Valor: ", &v); ler_int("Posicao (0..n): ", &pos); if (pos <0 || pos > obter_tamanho(lst)) { printf("Posicao invalida.\n"); break; } inserir_posicao(lst, v, pos); imprimir_lista(lst); break; }
 case 4: { if (lista_vazia(lst)) { printf("Lista vazia.\n"); break; } int v = remover_inicio(lst); printf("Removido: %d\n", v); imprimir_lista(lst); break; }
 case 5: { if (lista_vazia(lst)) { printf("Lista vazia.\n"); break; } int v = remover_fim(lst); printf("Removido: %d\n", v); imprimir_lista(lst); break; }
 case 6: { if (lista_vazia(lst)) { printf("Lista vazia.\n"); break; } int pos; ler_int("Posicao (0..n-1): ", &pos); if (pos <0 || pos >= obter_tamanho(lst)) { printf("Posicao invalida.\n"); break; } int v = remover_posicao(lst, pos); printf("Removido: %d\n", v); imprimir_lista(lst); break; }
 case 7: { int v; ler_int("Valor: ", &v); int i = buscar(lst, v); if (i >=0) printf("Encontrado na posicao %d\n", i); else printf("Nao encontrado.\n"); break; }
 case 8: imprimir_lista(lst); break;
 case 9: printf("Tamanho: %d\n", obter_tamanho(lst)); break;
 default: printf("Opcao invalida.\n");
 }
 }
}

static void submenu_lista_dupla(void) {
 print_divider("Lista Duplamente Encadeada - Menu");
 ListaDuplamenteEncadeada* l2 = criar_lista_dupla();
 for (;;) {
 printf("\n1) Inserir inicio\n2) Inserir fim\n3) Inserir na posicao\n4) Remover inicio\n5) Remover fim\n6) Remover na posicao\n7) Buscar valor\n8) Imprimir\n9) Imprimir reverso\n10) Tamanho\n0) Voltar\nEscolha: ");
 int op; if (!ler_int("", &op)) break;
 if (op ==0) { deletar_lista_dupla(l2); return; }
 switch (op) {
 case 1: { int v; ler_int("Valor: ", &v); inserir_inicio_dupla(l2, v); imprimir_lista_dupla(l2); break; }
 case 2: { int v; ler_int("Valor: ", &v); inserir_fim_dupla(l2, v); imprimir_lista_dupla(l2); break; }
 case 3: { int v, pos; ler_int("Valor: ", &v); ler_int("Posicao (0..n): ", &pos); if (pos <0 || pos > obter_tamanho_dupla(l2)) { printf("Posicao invalida.\n"); break; } inserir_posicao_dupla(l2, v, pos); imprimir_lista_dupla(l2); break; }
 case 4: { if (lista_dupla_vazia(l2)) { printf("Lista vazia.\n"); break; } int v = remover_inicio_dupla(l2); printf("Removido: %d\n", v); imprimir_lista_dupla(l2); break; }
 case 5: { if (lista_dupla_vazia(l2)) { printf("Lista vazia.\n"); break; } int v = remover_fim_dupla(l2); printf("Removido: %d\n", v); imprimir_lista_dupla(l2); break; }
 case 6: { if (lista_dupla_vazia(l2)) { printf("Lista vazia.\n");
 break; } int pos; ler_int("Posicao (0..n-1): ", &pos); if (pos <0 || pos >= obter_tamanho_dupla(l2)) { printf("Posicao invalida.\n"); break; } int v = remover_posicao_dupla(l2, pos); printf("Removido: %d\n", v); imprimir_lista_dupla(l2); break; }
 case 7: { int v; ler_int("Valor: ", &v); int i = buscar_dupla(l2, v); if (i >=0) printf("Encontrado na posicao %d\n", i); else printf("Nao encontrado.\n"); break; }
 case 8: imprimir_lista_dupla(l2); break;
 case 9: imprimir_lista_dupla_reversa(l2); break;
 case 10: printf("Tamanho: %d\n", obter_tamanho_dupla(l2)); break;
 default: printf("Opcao invalida.\n");
 }
 }
}

static void submenu_arvore(void) {
 print_divider("Arvore Binaria de Busca - Menu");
 ArvoreBinaria* arv = criar_arvore();
 for (;;) {
 printf("\n1) Inserir valor\n2) Buscar valor\n3) Imprimir em ordem\n4) Resetar arvore\n0) Voltar\nEscolha: ");
 int op; if (!ler_int("", &op)) break;
 if (op ==0) { deletar_arvore(arv); return; }
 switch (op) {
 case 1: { int v; ler_int("Valor: ", &v); inserir_valor(arv, v); printf("Inserido.\n"); break; }
 case 2: { int v; ler_int("Valor: ", &v); printf("%s\n", buscar_valor(arv, v) ? "Encontrado" : "Nao encontrado"); break; }
 case 3: imprimir_em_ordem(arv); break;
 case 4: deletar_arvore(arv); arv = criar_arvore(); printf("Arvore resetada.\n"); break;
 default: printf("Opcao invalida.\n");
 }
 }
}

static void submenu_grafo(void) {
 print_divider("Grafo - Menu");
 Grafo* g = NULL; int numV =0;
 for (;;) {
 printf("\n1) Criar/Recriar grafo\n2) Adicionar aresta (nao direcionado)\n3) Imprimir grafo\n4) BFS\n5) DFS\n0) Voltar\nEscolha: ");
 int op; if (!ler_int("", &op)) break;
 if (op ==0) { if (g) deletar_grafo(g); return; }
 switch (op) {
 case 1: {
 if (g) { deletar_grafo(g); g = NULL; }
 ler_int("Numero de vertices: ", &numV);
 if (numV <=0) { printf("Valor invalido.\n"); numV =0; break; }
 g = criar_grafo(numV); printf("Grafo criado com %d vertices.\n", numV); break; }
 case 2: {
 if (!g) { printf("Crie o grafo primeiro (opcao1).\n"); break; }
 int u, v; ler_int("Origem: ", &u); ler_int("Destino: ", &v);
 if (u <0 || v <0 || u >= numV || v >= numV) { printf("Vertices invalidos.\n"); break; }
 adicionar_aresta(g, u, v); printf("Aresta adicionada.\n"); break; }
 case 3: { if (!g) { printf("Crie o grafo primeiro.\n"); break; } imprimir_grafo(g); break; }
 case 4: { if (!g) { printf("Crie o grafo primeiro.\n"); break; } int s; ler_int("Vertice inicial: ", &s); if (s <0 || s >= numV) { printf("Invalido.\n"); break; } BFS(g, s); break; }
 case 5: { if (!g) { printf("Crie o grafo primeiro.\n"); break; } int s; ler_int("Vertice inicial: ", &s); if (s <0 || s >= numV) { printf("Invalido.\n"); break; } DFS(g, s); break; }
 default: printf("Opcao invalida.\n");
 }
 }
}

// Menu principal
static void mostrar_menu_principal(void) {
 print_divider("MENU PRINCIPAL - Estruturas de Dados");
 printf("1) Pilha (LIFO)\n");
 printf("2) Fila (FIFO)\n");
 printf("3) Lista Encadeada\n");
 printf("4) Lista Duplamente Encadeada\n");
 printf("5) Arvore Binaria de Busca\n");
 printf("6) Grafo (lista de adjacencia)\n");
 printf("0) Sair\n\n");
 printf("Digite a opcao e pressione Enter: ");
}

int main(void) {
 setlocale(LC_ALL, "");
#ifdef _WIN32
 SetConsoleOutputCP(CP_UTF8);
 SetConsoleCP(CP_UTF8);
#endif
 for (;;) {
 mostrar_menu_principal();
 int op; if (!ler_int("", &op)) break;
 switch (op) {
 case 1: submenu_pilha(); break;
 case 2: submenu_fila(); break;
 case 3: submenu_lista_encadeada(); break;
 case 4: submenu_lista_dupla(); break;
 case 5: submenu_arvore(); break;
 case 6: submenu_grafo(); break;
 case 0: print_divider("Saindo..."); return 0;
 default: printf("Opcao invalida.\n");
 }
 esperar_enter();
 }
 return 0;
}