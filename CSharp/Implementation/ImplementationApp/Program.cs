using DataStructures;
using System;
using Lib.Enums;
using System.Reflection;
using DataStructures.Nodes;

class Program
{
    private static MyList<Item> lista = new MyList<Item>();

    static void Main(string[] args)
    {
        int op = -1;

        // lista.Add(new Item("Leite", 2, DateTime.Now.AddDays(-1), TagTypes.Alimentos, PriorityTypes.Media, StatusTypes.Pendente));
        // lista.Add(new Item("Sabão em Pó", 1, DateTime.Now, TagTypes.Limpeza, PriorityTypes.Alta, StatusTypes.Pendente));


        while (op != 0)
        {
            Console.WriteLine("=== LISTA DE COMPRAS ===");
            Console.WriteLine("1 - Adicionar item");
            Console.WriteLine("2 - Remover item por nome");
            Console.WriteLine("3 - Buscar item por palavra-chave");
            Console.WriteLine("4 - Alterar prioridade e reordenar");
            Console.WriteLine("5 - Marcar item como Comprado");
            Console.WriteLine("6 - Exibir lista completa");
            Console.WriteLine("7 - Exibir Histórico de Alterações de um item");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");

            if (int.TryParse(Console.ReadLine(), out op))
            {
                Console.Clear();
                try
                {
                    switch (op)
                    {
                        case 1: AdicionarItem(); break;
                        case 2: RemoverItemPorNome(); break;
                        case 3: BuscarItemPorPalavraChave(); break;
                        case 4: AlterarPrioridade(); break;
                        case 5: MarcarItemComoComprado(); break;
                        case 6: ExibirListaCompleta(); break;
                        case 7: ExibirHistoricoItem(); break;
                        case 0: Console.WriteLine("Saindo..."); break;
                        default: Console.WriteLine("Opção inválida."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro durante a operação: {ex.Message}");
                }
                if (op != 0)
                {
                    Console.WriteLine("\nPressione Enter para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Entrada inválida. Tente novamente.");
            }
        }
    }

    static void AdicionarItem()
    {
        Console.WriteLine("--- ADICIONAR ITEM ---");
        Console.Write("Nome do produto: ");
        string nome = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Quantidade: ");
        if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade <= 0)
        {
            Console.WriteLine("Quantidade inválida.");
            return;
        }

        TagTypes tag = EscolherEnum<TagTypes>("Categoria");
        PriorityTypes prioridade = EscolherEnum<PriorityTypes>("Prioridade");
        StatusTypes status = StatusTypes.Pendente; 

        var novoItem = new Item(nome, quantidade, DateTime.Now, tag, prioridade, status);

        Console.WriteLine("\nOnde adicionar?");
        Console.WriteLine("1 - Início");
        Console.WriteLine("2 - Fim (Padrão)");
        Console.WriteLine("3 - Posição Específica (0 a {0})", lista.Count());
        Console.Write("Escolha: ");
        string pos = Console.ReadLine()?.Trim() ?? "2";

        switch (pos)
        {
            case "1":
                lista.AddFront(novoItem);
                break;
            case "3":
                Console.Write("Índice de inserção: ");
                if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index <= lista.Count())
                {
                    lista.InsertAt(novoItem, index);
                }
                else
                {
                    Console.WriteLine("Índice inválido. Adicionando ao final.");
                    lista.AddEnd(novoItem);
                }
                break;
            case "2":
            default:
                lista.AddEnd(novoItem);
                break;
        }

        Console.WriteLine($"Item '{nome}' adicionado com sucesso.");
    }

    static void RemoverItemPorNome()
    {
        Console.WriteLine("--- REMOVER ITEM ---");
        Console.Write("Nome do item a remover: ");
        string nome = Console.ReadLine()?.Trim() ?? string.Empty;

        bool removido = lista.Remove(item => item.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

        if (removido)
        {
            Console.WriteLine($"Item '{nome}' removido com sucesso.");
        }
        else
        {
            Console.WriteLine($"Item '{nome}' não encontrado.");
        }
    }

    static void BuscarItemPorPalavraChave()
    {
        Console.WriteLine("--- BUSCAR ITEM ---");
        Console.Write("Palavra-chave: ");
        string palavraChave = Console.ReadLine()?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(palavraChave))
        {
            Console.WriteLine("Palavra-chave não pode ser vazia.");
            return;
        }

        var resultados = new MyList<Item>();
        foreach (var item in lista)
        {
            // Busca por palavra-chave no Nome ou Tag
            if (item.Nome.IndexOf(palavraChave, StringComparison.OrdinalIgnoreCase) >= 0 ||
                item.Tag.ToString().IndexOf(palavraChave, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                resultados.Add(item);
            }
        }

        if (resultados.IsEmpty)
        {
            Console.WriteLine("Nenhum item encontrado com a palavra-chave.");
        }
        else
        {
            Console.WriteLine("\n--- Resultados da Busca ---");
            ExibirItens(resultados);
        }
    }

    static void AlterarPrioridade()
    {
        Console.WriteLine("--- ALTERAR PRIORIDADE E REORDENAR ---");
        Console.Write("Nome do item: ");
        string nome = Console.ReadLine()?.Trim() ?? string.Empty;

        DiKnot<Item>? nodeToUpdate = null;
        int index = 0;
        foreach (var node in lista.Nodes()) 
        {
            if (node.Value.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
            {
                nodeToUpdate = node;
                break;
            }
            index++;
        }

        if (nodeToUpdate == null)
        {
            Console.WriteLine($"Item '{nome}' não encontrado.");
            return;
        }

        PriorityTypes novaPrioridade = EscolherEnum<PriorityTypes>("Nova Prioridade");
        
        var item = nodeToUpdate.Value;
        
        string valorAntigo = item.Prioridade.ToString();
        string valorNovo = novaPrioridade.ToString();
        item.RegistrarAlteracao("Prioridade", DateTime.Now, valorAntigo, valorNovo);

        item.Prioridade = novaPrioridade;

        lista.ReordenarPorPrioridade(nodeToUpdate);

        Console.WriteLine($"Prioridade do item '{nome}' alterada para **{novaPrioridade}** e lista reordenada (Alta vai para o topo).");
    }

    static void MarcarItemComoComprado()
    {
        Console.WriteLine("--- MARCAR COMO COMPRADO ---");
        Console.Write("Nome do item: ");
        string nome = Console.ReadLine()?.Trim() ?? string.Empty;
        
        Item? itemToUpdate = null;
        foreach (var item in lista)
        {
            if (item.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
            {
                itemToUpdate = item;
                break;
            }
        }
        
        if (itemToUpdate == null)
        {
            Console.WriteLine($"Item '{nome}' não encontrado.");
            return;
        }

        if (itemToUpdate.Status == StatusTypes.Comprado)
        {
            Console.WriteLine($"Item '{nome}' já está marcado como Comprado.");
            return;
        }
        
        // Registrar alteração
        string valorAntigo = itemToUpdate.Status.ToString();
        itemToUpdate.Status = StatusTypes.Comprado;
        itemToUpdate.RegistrarAlteracao("Status", DateTime.Now, valorAntigo, itemToUpdate.Status.ToString());
        
        Console.WriteLine($"Item '{nome}' marcado como **Comprado**.");
    }
    
    static void ExibirListaCompleta()
    {
        Console.WriteLine("--- LISTA COMPLETA (Ordenada por Prioridade) ---");
        
        if (lista.IsEmpty)
        {
            Console.WriteLine("(Lista de compras vazia)");
            return;
        }

        var alta = new MyList<Item>();
        var media = new MyList<Item>();
        var baixa = new MyList<Item>();
        
        foreach(var item in lista)
        {
            if (item.Prioridade == PriorityTypes.Alta) alta.Add(item);
            else if (item.Prioridade == PriorityTypes.Media) media.Add(item);
            else baixa.Add(item);
        }
        
        ExibirItens(alta, "PRIORIDADE ALTA");
        ExibirItens(media, "PRIORIDADE MÉDIA");
        ExibirItens(baixa, "PRIORIDADE BAIXA");
    }
    
    static void ExibirHistoricoItem()
    {
        Console.WriteLine("--- HISTÓRICO DE ALTERAÇÕES ---");
        Console.Write("Nome do item: ");
        string nome = Console.ReadLine()?.Trim() ?? string.Empty;
        
        Item? itemToView = null;
        foreach (var item in lista)
        {
            if (item.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
            {
                itemToView = item;
                break;
            }
        }
        
        if (itemToView == null)
        {
            Console.WriteLine($"Item '{nome}' não encontrado.");
            return;
        }

        Console.WriteLine($"\nHistórico para: {itemToView.Nome}");
        if (itemToView.HistoricoAlteracoes.IsEmpty)
        {
            Console.WriteLine("(Nenhuma alteração registrada além da criação.)");
        }
        else
        {
            foreach (var log in itemToView.HistoricoAlteracoes.ReadTail())
            {
                Console.WriteLine($"- {log}");
            }
        }
    }

    static void ExibirItens(MyList<Item> lista, string? titulo = null)
    {
        if (lista.IsEmpty) return;
        
        if (!string.IsNullOrEmpty(titulo))
        {
            Console.WriteLine($"\n-- {titulo} ({lista.Count()} itens) --");
        }
        
        int i = 0;
        foreach (var item in lista)
        {
            string status = item.Status == StatusTypes.Comprado ? "**[COMPRADO]**" : "[PENDENTE]";
            string corPrioridade = item.Prioridade == PriorityTypes.Alta ? "!!!" : (item.Prioridade == PriorityTypes.Media ? "!!" : "!");
            
            Console.WriteLine($"{i,2}. {item.Nome,-20} | Qtd: {item.Quantidade,3} | {item.Tag,-10} | Prioridade: {item.Prioridade,-6}{corPrioridade} | Status: {status}");
            i++;
        }
    }

    static T EscolherEnum<T>(string nomeCampo) where T : Enum
    {
        Console.WriteLine($"\nSelecione {nomeCampo}:");
        var values = Enum.GetValues(typeof(T));
        int i = 0;
        foreach (var value in values)
        {
            Console.WriteLine($"{i} - {value}");
            i++;
        }

        Console.Write("Escolha (número): ");
        if (int.TryParse(Console.ReadLine(), out int escolha) && escolha >= 0 && escolha < values.Length)
        {
            return (T)values.GetValue(escolha)!;
        }
        
        Console.WriteLine("Escolha inválida. Usando o primeiro valor (default).");
        return (T)values.GetValue(0)!;
    }
}