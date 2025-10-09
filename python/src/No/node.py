
class No:
    def __init__(self, valor: int):
        self.valor: int = valor
        self.esquerdo: No = None
        self.direito: No = None
    
    def __str__(self):
        return str(self.valor)