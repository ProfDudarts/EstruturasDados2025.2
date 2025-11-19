using System.Formats.Asn1;
using DataStructures;
using Lib.Enums;

public class Item : IComparable<Item>
{
    public string Nome = string.Empty;
    public int Quantidade;
    public DateTime Data;

    public TagTypes Tag;
    public PriorityTypes Prioridade;
    public StatusTypes Status;

    public Item(string nome, int quantidade, DateTime data, TagTypes tag, PriorityTypes prioridade, StatusTypes status)
    {
        Nome = nome;
        Quantidade = quantidade;
        Data = data;
        Tag = tag;
        Prioridade = prioridade;
        Status = status;

        HistoricoAlteracoes = new MyList<ChangeLog>();
        RegistrarAlteracao("Criação do Item", DateTime.Now, "Item criado na lista de compras");
    }

    public MyList<ChangeLog> HistoricoAlteracoes { get; private set; } 

    public void RegistrarAlteracao(string campo, DateTime data, string valorAntigo = "", string valorNovo = "")
    {
        HistoricoAlteracoes.Add(new ChangeLog(campo, data, valorAntigo, valorNovo));
    }

    public int CompareTo(Item? other)
    {
        if (other == null) return 1;

        return Prioridade.CompareTo(other.Prioridade);
    }
}


    