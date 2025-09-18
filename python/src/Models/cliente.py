class Cliente():
    def __init__(self, id: int, nome: str):
        self.id = id
        self.nome = nome

    def exibir(self):
        print(f"ID: {self.id}\tNome: {self.nome.title()}")