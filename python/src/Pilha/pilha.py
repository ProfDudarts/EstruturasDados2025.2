from src.No.no import No

class Pilha():
    def __init__(self):
        self.base: No = None
        self.tamanho = 0
    
    def inserir(self, no: No):        
        if self.taVazio():
            self.base = no
        else:
            self.obterTopo().proximo = no
        self.tamanho += 1
    
    def _listarTodos(self, no: No):        
        if no.temProximo():
            self._listarTodos(no.proximo)
        no.imprimir()            

    def listar(self):
        self._listarTodos(self.base)
    
    def obterTopo(self):
        atual = self.base
        while True:
            if not atual.temProximo():
                return atual 
            else:
                atual = atual.proximo

    def tamanho(self):
        return self.tamanho
    
    def taVazio(self):
        return self.tamanho == 0

    def remover(self) -> No:
        atual = self.base

        while True:
            if not atual.proximo.temProximo():
                ultimo = atual.proximo
                atual.proximo = None
                self.tamanho -= 1
                return ultimo 
            else:
                atual = atual.proximo
