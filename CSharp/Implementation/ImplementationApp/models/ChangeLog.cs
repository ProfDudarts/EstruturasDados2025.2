namespace DataStructures;
using System;

public class ChangeLog : IComparable<ChangeLog>
{
    public string CampoAlterado { get; set; }
    public DateTime DataAlteracao { get; set; }
    public string ValorAntigo { get; set; }
    public string ValorNovo { get; set; }

    public ChangeLog(string campo, DateTime data, string valorAntigo = "", string valorNovo = "")
    {
        CampoAlterado = campo;
        DataAlteracao = data;
        ValorAntigo = valorAntigo;
        ValorNovo = valorNovo;
    }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(ValorAntigo) && string.IsNullOrEmpty(ValorNovo))
        {
             return $"[{DataAlteracao:dd/MM/yyyy HH:mm:ss}] {CampoAlterado}";
        }
        return $"[{DataAlteracao:dd/MM/yyyy HH:mm:ss}] {CampoAlterado}: De '{ValorAntigo}' para '{ValorNovo}'";
    }

    public int CompareTo(ChangeLog? other)
    {
        if (other == null) return 1;

        return DataAlteracao.CompareTo(other.DataAlteracao);
    }
}