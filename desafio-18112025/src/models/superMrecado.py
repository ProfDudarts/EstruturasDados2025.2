from src.models.produtos import Produto
import datetime

def gerar_produtos(produto : Produto):
    categoria = ["Alimentos", "Limpeza", "Higiene", "Utensilhos"]
    batata = Produto("Batata", 0, categoria[0], "alta", "Pendente", datetime.datetime.now())
    smartphone = Produto("Smartphone XZ-500", 0, categoria[1], "alta", "Pendente", datetime.datetime.now())
    camiseta = Produto("Camiseta", 0, categoria[2], "media", "Pendente", datetime.datetime.now())
    livro_misterio = Produto("Livro", 0, categoria[3], "baixa", "Pendente", datetime.datetime.now())
    pasta_dente = Produto("Pasta de Dente", 0, categoria[4], "alta", "Pendente", datetime.datetime.now())
    fone_ouvido = Produto("Fone de Ouvido", 0, categoria[1], "media", "Pendente", datetime.datetime.now())
    pao_integral = Produto("Pão", 0, categoria[0], "media", "Pendente", datetime.datetime.now())