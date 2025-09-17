from src.No.no import No

class Fila():
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
        no.imprimir()            
        if no.temProximo():
            self._listarTodos(no.proximo)

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
        self.base = atual.proximo
        return atual

    def obterPosicao(self, id: int) -> int:
        try:
            posicao = self._posicao_do_no(self.base, id)
            if not posicao:
                posicao = 0
            
            return posicao
        except:
            return 0
    
    def _posicao_do_no(self, no: No, id: int) -> int:
        try:
            if no.valor.id == id:
                return 1
            else:
                if no.temProximo():            
                    return 1 + self._posicao_do_no(no.proximo, id)
        except:
            raise "Id não encontrado"

