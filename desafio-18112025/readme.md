# Desafio Final – Sistema Inteligente de Lista de Compras
## Situação-Problema
Imagine que você e sua equipe foram contratados para desenvolver um sistema colaborativo de lista de compras para um grupo de pessoas que compartilham uma casa. O objetivo é permitir que todos adicionem, removam e organizem itens de forma dinâmica, garantindo que a lista seja eficiente e reflita as prioridades do grupo.

O sistema deve ser implementado utilizando Lista Encadeada (ou Lista Duplamente Encadeada) já implementada como estrutura principal, explorando suas vantagens para inserções e remoções dinâmicas.

## Requisitos do Sistema
Estrutura da Lista:
- Cada nó representa um item da lista com:
- Nome do produto
- Quantidade
- Categoria (alimentos, limpeza, higiene, etc.)
- Prioridade (alta, média, baixa)
- Status (pendente, comprado)
- Data de inclusão

Funcionalidades obrigatórias:
- Adicionar item no início, meio ou fim da lista.
- Remover item por nome.
- Buscar item por palavra-chave.
-Alterar prioridade e reordenar dinamicamente (itens com prioridade alta devem ir para o topo).
- Marcar item como comprado (sem removê-lo, mas alterando status).
- Exibir lista completa com ordenação por prioridade.
- Histórico de alterações: cada modificação (quantidade, prioridade, status) deve ser registrada em uma lista encadeada secundária ligada ao item.

Demais Requisitos
- Interface via terminal (criar interface é opcional)
- Linguagem: Python ou a linguagem sorteada para o time
- Usar a implementação já feita no projeto, alterando apenas o tipo do valor.
- Pode usar IA como auxílio
- Criar um PR para a branch DEV no repositório