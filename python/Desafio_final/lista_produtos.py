from datetime import datetime

class NoHistorico:
    def __init__(self, acao):
        self.acao = acao
        self.data = datetime.now().strftime("%d/%m/%Y %H:%M")
        self.prox = None

class ListaHistorico:
    def __init__(self):
        self.inicio = None

    def registrar(self, acao):
        novo = NoHistorico(acao)
        if self.inicio is None:
            self.inicio = novo
        else:
            atual = self.inicio
            while atual.prox:
                atual = atual.prox
            atual.prox = novo

    def exibir_historico(self):
        atual = self.inicio
        log = []
        while atual:
            log.append(f"[{atual.data}] {atual.acao}")
            atual = atual.prox
        return "\n      ".join(log)


class NoItem:
    def __init__(self, nome, qtd, categoria, prioridade):
        self.nome = nome
        self.qtd = qtd
        self.categoria = categoria
        self.prioridade = prioridade  
        self.status = "Pendente"      
        self.data_inclusao = datetime.now().strftime("%d/%m/%Y")
        
       
        self.historico = ListaHistorico()
        self.historico.registrar(f"Item criado: {nome}, Qtd: {qtd}, Prio: {prioridade}")
        
        self.proximo = None


    def valor_prioridade(self):
        mapa = {"Alta": 3, "Média": 2, "Media": 2, "Baixa": 1}
        return mapa.get(self.prioridade, 0)

class ListaCompras:
    def __init__(self):
        self.inicio = None

    def adicionar(self, nome, qtd, categoria, prioridade):
        novo = NoItem(nome, qtd, categoria, prioridade)
        

        if self.inicio is None:
            self.inicio = novo
        else:
            atual = self.inicio
            while atual.proximo:
                atual = atual.proximo
            atual.proximo = novo
        
        
        self.reordenar_por_prioridade()
        print(f"✅ Item '{nome}' adicionado com sucesso!")

    def buscar(self, termo):
        atual = self.inicio
        encontrados = []
        termo = termo.lower()
        while atual:
            if termo in atual.nome.lower():
                encontrados.append(atual)
            atual = atual.proximo
        return encontrados

    def marcar_comprado(self, nome):
        atual = self.inicio
        while atual:
            if atual.nome.lower() == nome.lower():
                if atual.status == "Comprado":
                    print("⚠️ Item já estava marcado como comprado.")
                    return
                
                atual.status = "Comprado"
                atual.historico.registrar("Status alterado para: Comprado")
                print(f"🛒 Item '{atual.nome}' marcado como COMPRADO!")
                return
            atual = atual.proximo
        print("❌ Item não encontrado.")

    def alterar_prioridade(self, nome, nova_prioridade):
        atual = self.inicio
        while atual:
            if atual.nome.lower() == nome.lower():
                antiga = atual.prioridade
                atual.prioridade = nova_prioridade
                atual.historico.registrar(f"Prioridade alterada: {antiga} -> {nova_prioridade}")
                print(f"🔄 Prioridade de '{atual.nome}' atualizada.")
                self.reordenar_por_prioridade() 
                return
            atual = atual.proximo
        print("❌ Item não encontrado.")

    def remover(self, nome):
        atual = self.inicio
        anterior = None

        while atual and atual.nome.lower() != nome.lower():
            anterior = atual
            atual = atual.proximo

        if atual is None:
            print("❌ Item não encontrado para remoção.")
            return

        if anterior is None:
            self.inicio = atual.proximo
        else:
            anterior.proximo = atual.proximo
        
        print(f"🗑️ Item '{atual.nome}' removido da lista.")

    def reordenar_por_prioridade(self):
        """
        Algoritmo Bubble Sort adaptado para Lista Encadeada.
        Move itens de maior prioridade (3) para o topo.
        """
        if self.inicio is None or self.inicio.proximo is None:
            return

        trocou = True
        while trocou:
            trocou = False
            atual = self.inicio
            while atual.proximo:
                
                if atual.valor_prioridade() < atual.proximo.valor_prioridade():
                    
                    atual.nome, atual.proximo.nome = atual.proximo.nome, atual.nome
                    atual.qtd, atual.proximo.qtd = atual.proximo.qtd, atual.qtd
                    atual.categoria, atual.proximo.categoria = atual.proximo.categoria, atual.categoria
                    atual.prioridade, atual.proximo.prioridade = atual.proximo.prioridade, atual.prioridade
                    atual.status, atual.proximo.status = atual.proximo.status, atual.status
                    atual.data_inclusao, atual.proximo.data_inclusao = atual.proximo.data_inclusao, atual.data_inclusao
                    
                    
                    atual.historico, atual.proximo.historico = atual.proximo.historico, atual.historico
                    
                    trocou = True
                atual = atual.proximo

    def listar(self):
        if self.inicio is None:
            print("📝 Lista de compras vazia.")
            return

        atual = self.inicio
        print("\n" + "="*60)
        print(f"{'PRIORIDADE':<10} | {'STATUS':<10} | {'NOME':<20} | {'QTD':<5} | {'CATEGORIA'}")
        print("-" * 60)
        while atual:
            prio_icon = "🔴" if atual.prioridade == "Alta" else ("🟡" if atual.prioridade == "Média" else "🟢")
            status_icon = "✅" if atual.status == "Comprado" else "⏳"
            
            print(f"{prio_icon} {atual.prioridade:<8} | {status_icon} {atual.status:<8} | {atual.nome:<20} | {atual.qtd:<5} | {atual.categoria}")
            
            
            atual = atual.proximo
        print("="*60 + "\n")