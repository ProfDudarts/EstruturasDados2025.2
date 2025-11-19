from src.models.produtos import Produto

def imprimir_produtos_por_categoria(categoria_alvo):
    produtos_encontrados = []
    
    for produto in Produto._todos_produtos:
        if produto.categoria.lower() == categoria_alvo.lower():
            produtos_encontrados.append(produto)
    
    if produtos_encontrados:
        print(f"--- Produtos na Categoria '{categoria_alvo.capitalize()}' ---")
        for produto in produtos_encontrados:
            print(produto)
        print("-------------------------------------------------")
    else:
        print(f"Não foram encontrados produtos na categoria '{categoria_alvo.capitalize()}'.")