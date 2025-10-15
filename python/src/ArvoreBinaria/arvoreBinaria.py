from src.No.node import No


class ArvoreBinaria:
    def __init__(self):
        self.root: No = None
    
    def inserir(self, valor: No) -> None:
        if not self.root:
            self.root = valor
        else:
            self._inserir(self.root, valor)
    
    def _inserir(self, pai: No, valor: No) -> None:
        if not pai.esquerdo:
            pai.esquerdo = valor
            return
        elif not pai.direito:
            pai.direito = valor
            return
        else:
            if valor.valor < pai.valor:
                return self._inserir(pai.esquerdo, valor)
            else:
                return self._inserir(pai.direito, valor)

    def taCompleto(self, no: No) -> bool:
        return no.esquerdo and no.direito
    
    def exibir(self):
        self._exibir(self.root, 0, "R: ")

    def _exibir(self, atual: No, nivel: int, prefixo: str) -> None:
        print(" " * nivel, prefixo, atual.valor)
        if atual.esquerdo:
            self._exibir(atual.esquerdo, nivel + 2, "E: ")
        if atual.direito:
            self._exibir(atual.direito, nivel + 2, "D: ")
    
    def altura(self,):
        pass


