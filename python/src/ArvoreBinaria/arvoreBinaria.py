from src.No.node import No


class ArvoreBinaria:
    def __init__(self):
        self.raiz: No = None
    
    def inserir(self, valor: No) -> None:
        if not self.raiz:
            self.raiz = valor
        else:
            self._inserir(self.raiz, valor)
    
    def _inserir(self, pai: No, valor: No) -> None:
        if not pai.esquerdo:
            pai.esquerdo = valor
            return
        elif not pai.direito:
            pai.direito = valor
            return
        else:
            if self._altura(pai.esquerdo) <= self._altura(pai.direito):
                return self._inserir(pai.esquerdo, valor)
            else:
                return self._inserir(pai.direito, valor)

    def taCompleto(self, no: No) -> bool:
        return no.esquerdo and no.direito
    
    def exibir(self):
        self._exibir(self.raiz, 0, "R: ")

    def _exibir(self, atual: No, nivel: int, prefixo: str) -> None:
        print(" " * nivel, prefixo, atual.valor)
        if atual.esquerdo:
            self._exibir(atual.esquerdo, nivel + 2, "E: ")
        if atual.direito:
            self._exibir(atual.direito, nivel + 2, "D: ")
    
    def altura(self):
        return self._altura(self.raiz)

    def _altura(self, no: No):
        if no is None:
            return 0
        else: 
            return 1 + max(self._altura(no.esquerdo), self._altura(no.direito))
        


