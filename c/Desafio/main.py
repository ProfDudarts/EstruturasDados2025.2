from shopping_system import ShoppingSystem
from product import Product
import os


def limpar_tela():
    """Limpa a tela do terminal"""
    os.system('cls' if os.name == 'nt' else 'clear')


def mostrar_menu():
    """Exibe o menu principal"""
    print("\n" + "="*50)
    print(" 🛒 SISTEMA DE LISTA DE COMPRAS 🛒 ".center(50, "="))
    print("="*50)
    print("\n[1] ➕ Adicionar Produto")
    print("[2] ➖ Remover Produto")
    print("[3] 🔍 Buscar Produto")
    print("[4] ✏️  Alterar Prioridade")
    print("[5] ✅ Marcar como Comprado")
    print("[6] 📋 Listar Todos os Produtos")
    print("[7] 📜 Ver Histórico de Alterações")
    print("[0] 🚪 Sair")
    print("\n" + "="*50)


def exibir_produto(produto):
    """Exibe um produto de forma formatada"""
    status_icon = "✓" if produto.status else "○"
    print(f"\n{status_icon} {produto.name}")
    print(f"  │ Quantidade: {produto.quantity}")
    print(f"  │ Categoria: {produto.category}")
    print(f"  │ Prioridade: {produto.priority.upper()}")
    print(f"  │ Status: {'COMPRADO' if produto.status else 'PENDENTE'}")
    print(f"  └ Adicionado em: {produto.date_added.strftime('%d/%m/%Y %H:%M')}")


def adicionar_produto(sistema):
    """Adiciona um novo produto"""
    limpar_tela()
    print("\n=== ADICIONAR PRODUTO ===\n")
    
    nome = input("Nome do produto: ").strip()
    if not nome:
        print("\n❌ Nome não pode ser vazio!")
        input("\nPressione ENTER para continuar...")
        return
    
    try:
        quantidade = int(input("Quantidade: "))
        if quantidade <= 0:
            print("\n❌ Quantidade deve ser maior que zero!")
            input("\nPressione ENTER para continuar...")
            return
    except ValueError:
        print("\n❌ Quantidade inválida!")
        input("\nPressione ENTER para continuar...")
        return
    
    categoria = input("Categoria: ").strip()
    
    print("\nPrioridade:")
    print("  [1] Alta")
    print("  [2] Média")
    print("  [3] Baixa")
    prio_opcao = input("Escolha (1-3): ").strip()
    
    prioridades = {'1': 'alta', '2': 'media', '3': 'baixa'}
    prioridade = prioridades.get(prio_opcao, 'media')
    
    produto = Product(nome, quantidade, categoria, prioridade)
    sistema.add_item_end(produto)
    
    print(f"\n✓ Produto '{nome}' adicionado com sucesso!")
    input("\nPressione ENTER para continuar...")


def remover_produto(sistema):
    """Remove um produto pelo nome"""
    limpar_tela()
    print("\n=== REMOVER PRODUTO ===\n")
    
    nome = input("Nome do produto a remover: ").strip()
    if not nome:
        print("\n❌ Nome não pode ser vazio!")
        input("\nPressione ENTER para continuar...")
        return
    
    if sistema.remove_item_by_name(nome):
        print(f"\n✓ Produto '{nome}' removido com sucesso!")
    else:
        print(f"\n❌ Produto '{nome}' não encontrado!")
    
    input("\nPressione ENTER para continuar...")


def buscar_produto(sistema):
    """Busca produtos por palavra-chave"""
    limpar_tela()
    print("\n=== BUSCAR PRODUTO ===\n")
    
    keyword = input("Digite o nome ou parte do nome: ").strip()
    if not keyword:
        print("\n❌ Digite algo para buscar!")
        input("\nPressione ENTER para continuar...")
        return
    
    resultados = sistema.find_items(keyword)
    
    if resultados:
        print(f"\n✓ Encontrado(s) {len(resultados)} produto(s):")
        for produto in resultados:
            exibir_produto(produto)
    else:
        print(f"\n❌ Nenhum produto encontrado com '{keyword}'")
    
    input("\nPressione ENTER para continuar...")


def alterar_prioridade(sistema):
    """Altera a prioridade de um produto"""
    limpar_tela()
    print("\n=== ALTERAR PRIORIDADE ===\n")
    
    nome = input("Nome do produto: ").strip()
    if not nome:
        print("\n❌ Nome não pode ser vazio!")
        input("\nPressione ENTER para continuar...")
        return
    
    print("\nNova Prioridade:")
    print("  [1] Alta")
    print("  [2] Média")
    print("  [3] Baixa")
    prio_opcao = input("Escolha (1-3): ").strip()
    
    prioridades = {'1': 'alta', '2': 'media', '3': 'baixa'}
    nova_prioridade = prioridades.get(prio_opcao)
    
    if not nova_prioridade:
        print("\n❌ Opção inválida!")
        input("\nPressione ENTER para continuar...")
        return
    
    if sistema.change_priority(nome, nova_prioridade):
        print(f"\n✓ Prioridade de '{nome}' alterada para '{nova_prioridade.upper()}'!")
    else:
        print(f"\n❌ Produto '{nome}' não encontrado!")
    
    input("\nPressione ENTER para continuar...")


def marcar_comprado(sistema):
    """Marca um produto como comprado"""
    limpar_tela()
    print("\n=== MARCAR COMO COMPRADO ===\n")
    
    nome = input("Nome do produto: ").strip()
    if not nome:
        print("\n❌ Nome não pode ser vazio!")
        input("\nPressione ENTER para continuar...")
        return
    
    if sistema.mark_as_bought(nome):
        print(f"\n✓ Produto '{nome}' marcado como comprado!")
    else:
        print(f"\n❌ Produto '{nome}' não encontrado!")
    
    input("\nPressione ENTER para continuar...")


def listar_produtos(sistema):
    """Lista todos os produtos"""
    limpar_tela()
    print("\n=== LISTA DE PRODUTOS ===")
    
    produtos = sistema.list_all()
    
    if not produtos:
        print("\n❌ Nenhum produto na lista!")
    else:
        print(f"\nTotal: {len(produtos)} produto(s)")
        print("\n" + "-"*50)
        for produto in produtos:
            exibir_produto(produto)
            print("-"*50)
    
    input("\nPressione ENTER para continuar...")


def ver_historico_completo(sistema):
    """Exibe todo o histórico de alterações"""
    limpar_tela()
    print("\n=== HISTÓRICO DE ALTERAÇÕES ===")
    
    historico = sistema.history.get_all_entries()
    
    if not historico:
        print("\n❌ Nenhuma alteração registrada!")
    else:
        print(f"\nTotal: {len(historico)} entrada(s)")
        print("\n" + "-"*50)
        for entry in historico:
            icone_acao = {
                'adicionado': '➕',
                'removido': '➖',
                'alterado': '✏️',
                'comprado': '✓'
            }.get(entry.action, '•')
            
            print(f"\n{icone_acao} {entry.product_name}")
            print(f"  │ Ação: {entry.action.upper()}")
            print(f"  │ Detalhes: {entry.details}")
            print(f"  └ Data: {entry.timestamp.strftime('%d/%m/%Y %H:%M:%S')}")
            print("-"*50)
    
    input("\nPressione ENTER para continuar...")


def ver_historico_produto(sistema):
    """Exibe o histórico de um produto específico"""
    limpar_tela()
    print("\n=== HISTÓRICO DE PRODUTO ===\n")
    
    nome = input("Nome do produto: ").strip()
    if not nome:
        print("\n❌ Nome não pode ser vazio!")
        input("\nPressione ENTER para continuar...")
        return
    
    historico = sistema.history.get_product_history(nome)
    
    if not historico:
        print(f"\n❌ Nenhuma alteração encontrada para '{nome}'")
    else:
        print(f"\n✓ Histórico de '{nome}' ({len(historico)} entrada(s)):")
        print("\n" + "-"*50)
        for entry in historico:
            icone_acao = {
                'adicionado': '➕',
                'removido': '➖',
                'alterado': '✏️',
                'comprado': '✓'
            }.get(entry.action, '•')
            
            print(f"\n{icone_acao} Ação: {entry.action.upper()}")
            print(f"  │ Detalhes: {entry.details}")
            print(f"  └ Data: {entry.timestamp.strftime('%d/%m/%Y %H:%M:%S')}")
            print("-"*50)
    
    input("\nPressione ENTER para continuar...")


def main():
    sistema = ShoppingSystem()
    
    while True:
        limpar_tela()
        mostrar_menu()
        
        opcao = input("\nEscolha uma opção: ").strip()
        
        if opcao == '1':
            adicionar_produto(sistema)
        elif opcao == '2':
            remover_produto(sistema)
        elif opcao == '3':
            buscar_produto(sistema)
        elif opcao == '4':
            alterar_prioridade(sistema)
        elif opcao == '5':
            marcar_comprado(sistema)
        elif opcao == '6':
            listar_produtos(sistema)
        elif opcao == '7':
            ver_historico_completo(sistema)
        elif opcao == '0':
            limpar_tela()
            print("\n👋 Até logo!\n")
            break
        else:
            print("\n❌ Opção inválida!")
            input("\nPressione ENTER para continuar...")


if __name__ == '__main__':
    main()
