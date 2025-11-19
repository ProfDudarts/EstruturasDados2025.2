import datetime


class HistoricoNode:

    def __init__(self, tipo_alteracao, valor_antigo, valor_novo, data_registro=None):
        self.tipo_alteracao = tipo_alteracao
        self.valor_antigo = valor_antigo
        self.valor_novo = valor_novo
        self.data_registro = data_registro if data_registro else datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        self.next = None

    def __str__(self):
        return (f"[{self.data_registro}] {self.tipo_alteracao} mudou de "
                f"'{self.valor_antigo}' para '{self.valor_novo}'")


class ItemNode:

    def __init__(self, nome, quantidade, categoria, prioridade='Média', status='Pendente', data_inclusao=None):
        self.nome = nome
        self.quantidade = quantidade
        self.categoria = categoria
        self.prioridade = prioridade.capitalize() 
        self.status = status 
        self.data_inclusao = data_inclusao if data_inclusao else datetime.datetime.now().strftime("%Y-%m-%d")

        self.prev = None
        self.next = None

        self.historico_head = None
    
    def get_prioridade_valor(self):
        prioridades = {'Alta': 3, 'Média': 2, 'Baixa': 1}
        return prioridades.get(self.prioridade, 1)

    def registrar_alteracao(self, tipo, antigo, novo):

        novo_registro = HistoricoNode(tipo, antigo, novo)
        novo_registro.next = self.historico_head
        self.historico_head = novo_registro
    
    def exibir_historico(self):

        if not self.historico_head:
            return "Nenhuma alteração registrada."
        
        atual = self.historico_head
        historico_str = ""
        while atual:
            historico_str += f"\n  - {atual}"
            atual = atual.next
        return historico_str