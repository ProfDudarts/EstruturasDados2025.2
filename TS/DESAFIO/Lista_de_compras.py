import datetime
from no import ItemNode 
class ListaDeCompras:

    def __init__(self):
        self.head = None
        self.tail = None
        self.tamanho = 0

    def _encontrar_item(self, nome):

        atual = self.head
        while atual:
            if atual.nome.lower() == nome.lower():
                return atual
            atual = atual.next
        return None
        
    def _remover_node(self, item_node):

        if item_node.prev:
            item_node.prev.next = item_node.next
        else: 
            self.head = item_node.next
            
        if item_node.next:
            item_node.next.prev = item_node.prev
        else:
            self.tail = item_node.prev
            
        self.tamanho -= 1
        item_node.prev = None
        item_node.next = None
    
    def _adicionar_node(self, item_node, posicao='inicio'):

        if not self.head:
            self.head = item_node
            self.tail = item_node
        elif posicao.lower() == 'inicio':
            item_node.next = self.head
            self.head.prev = item_node
            self.head = item_node
        elif posicao.lower() == 'fim':
            self.tail.next = item_node
            item_node.prev = self.tail
            self.tail = item_node
        self.tamanho += 1

    def _mover_para_inicio_por_prioridade(self, item_node):

        if item_node == self.head or item_node.prioridade != 'Alta':
            return
        self._remover_node(item_node)
        atual = self.head
        ultimo_alta = None

        while atual and atual.prioridade == 'Alta':
            ultimo_alta = atual
            atual = atual.next
        if not ultimo_alta:
            self._adicionar_node(item_node, 'inicio')
            return
        item_node.next = ultimo_alta.next
        item_node.prev = ultimo_alta
        ultimo_alta.next = item_node

        if item_node.next:
            item_node.next.prev = item_node
        else:
            self.tail = item_node 

        self.tamanho += 1

        print(f"🔄 Reordenado: '{item_node.nome}' movido para a posição correta de prioridade.")



    def adicionar_item(self, nome, quantidade, categoria, prioridade='Média', posicao='fim'):

        if self._encontrar_item(nome):
            print(f"⚠️ Item '{nome}' já existe na lista.")
            return

        novo_item = ItemNode(nome, quantidade, categoria, prioridade)

        if not self.head:
            self.head = novo_item
            self.tail = novo_item
            self.tamanho += 1
        else:
            if posicao.lower() == 'meio' and self.head.next:
                proximo = self.head.next
                self.head.next = novo_item
                novo_item.prev = self.head
                novo_item.next = proximo
                proximo.prev = novo_item
                self.tamanho += 1
            else: 
                self._adicionar_node(novo_item, posicao)
        if novo_item.prioridade == 'Alta':
             self._mover_para_inicio_por_prioridade(novo_item)
             
        print(f"✅ Item '{nome}' adicionado (Prioridade: {novo_item.prioridade}).")


    def remover_item(self, nome):
        """Remove um item da lista buscando pelo nome."""
        item_para_remover = self._encontrar_item(nome)
        
        if not item_para_remover:
            print(f"Item '{nome}' não encontrado.")
            return

        self._remover_node(item_para_remover)
        print(f"Item '{nome}' removido com sucesso.")

    def buscar_item(self, palavra_chave):
        """Busca itens cujo nome ou categoria contenha a palavra-chave."""
        palavra_chave = palavra_chave.lower()
        resultados = []
        atual = self.head
        
        while atual:
            if palavra_chave in atual.nome.lower() or palavra_chave in atual.categoria.lower():
                resultados.append(atual)
            atual = atual.next
            
        if not resultados:
            print(f" Nenhum item encontrado com a palavra-chave '{palavra_chave}'.")
        else:
            print(f" {len(resultados)} item(s) encontrado(s) para '{palavra_chave}':")
            for item in resultados:
                print(f"   - **{item.nome}** ({item.quantidade}) - P: {item.prioridade} | S: {item.status}")
        
        return resultados

    def alterar_prioridade_e_reordenar(self, nome, nova_prioridade):
        item_node = self._encontrar_item(nome)
        nova_prioridade = nova_prioridade.capitalize()
        
        if not item_node:
            print(f"Item '{nome}' não encontrado para alteração.")
            return
            
        if nova_prioridade not in ['Alta', 'Média', 'Baixa']:
            print("Prioridade inválida. Use 'Alta', 'Média' ou 'Baixa'.")
            return
            
        antiga_prioridade = item_node.prioridade
        item_node.registrar_alteracao('prioridade', antiga_prioridade, nova_prioridade)


        item_node.prioridade = nova_prioridade
        
        if nova_prioridade == 'Alta' or item_node.get_prioridade_valor() > ItemNode(nome, 0, "", antiga_prioridade).get_prioridade_valor():
            self._mover_para_inicio_por_prioridade(item_node)
            print(f"⬆️ Prioridade de '{nome}' alterada para **{nova_prioridade}** e lista reordenada.")
        else:
             print(f"📝 Prioridade de '{nome}' alterada para **{nova_prioridade}**.")



    def marcar_comprado(self, nome):
        item_node = self._encontrar_item(nome)
        
        if not item_node:
            print(f"Item '{nome}' não encontrado.")
            return

        if item_node.status == 'Comprado':
             print(f"Item '{nome}' já está marcado como Comprado.")
             return
            
        item_node.registrar_alteracao('status', item_node.status, 'Comprado')

        item_node.status = 'Comprado'
        
        print(f"Item '{nome}' marcado como **Comprado**.")
        
    def exibir_lista(self):
        if not self.head:
            print("A lista de compras está vazia.")
            return

        print("\n=======================================================")
        print(f"  Lista de Compras Atual (Total de Itens: {self.tamanho})")
        print("=======================================================")
        
        atual = self.head
        posicao = 1
        
        while atual:
            status_simbolo = "✅" if atual.status == "Comprado" else "⏳"
            prioridade_simbolo = "🔥" if atual.prioridade == "Alta" else "🟡" if atual.prioridade == "Média" else "🔵"
            
            print(f"{posicao}. {status_simbolo} {prioridade_simbolo} **{atual.nome}** - Qtd: {atual.quantidade} | Prioridade: {atual.prioridade} | Categoria: {atual.categoria} | Status: {atual.status}")
            print(f"   (Incluído em: {atual.data_inclusao})")
            
            if atual.historico_head:
                print(f"   [Última Alteração: {atual.historico_head.tipo_alteracao} para '{atual.historico_head.valor_novo}' em {atual.historico_head.data_registro}]")
            
            atual = atual.next
            posicao += 1
        print("=======================================================\n")