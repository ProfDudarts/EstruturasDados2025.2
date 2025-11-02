package Models;
import java.util.LinkedList;

public class TabelaHash<K, V> {
    private LinkedList<Par<K, V>>[] tabela;
    private int capacidade;

    private static class Par<K, V> {
        K chave;
        V valor;

        Par(K chave, V valor) {
            this.chave = chave;
            this.valor = valor;
        }
    }

    public TabelaHash(int capacidade) {
        this.capacidade = capacidade;
        tabela = new LinkedList[capacidade];
    }

    // transforma a chave em um índice
    private int hash(K chave) {
        return Math.abs(chave.hashCode()) % capacidade;
    }

    public void inserir(K chave, V valor) {
        if (chave == null)
            throw new IllegalArgumentException("Chave não pode ser nula.");

        int indice = hash(chave);

        if (tabela[indice] == null) {
            tabela[indice] = new LinkedList<>();
        }

        for (Par<K, V> par : tabela[indice]) {
            if (par.chave.equals(chave)) {
                par.valor = valor; // Atualiza o valor existente
                return;
            }
        }

        tabela[indice].add(new Par<>(chave, valor));
    }

    public V buscar(K chave) {
        if (chave == null)
            throw new IllegalArgumentException("Chave não pode ser nula.");

        int indice = hash(chave);
        if (tabela[indice] != null) {
            for (Par<K, V> par : tabela[indice]) {
                if (par.chave.equals(chave)) {
                    return par.valor;
                }
            }
        }
        return null;
    }

    public void remover(K chave) {
        if (chave == null)
            throw new IllegalArgumentException("Chave não pode ser nula.");

        int indice = hash(chave);
        if (tabela[indice] != null) {
            tabela[indice].removeIf(par -> par.chave.equals(chave));
        }
    }

    public void mostrar() {
        for (int i = 0; i < capacidade; i++) {
            System.out.print(i + ": ");
            if (tabela[i] != null) {
                for (Par<K, V> par : tabela[i]) {
                    System.out.print("[" + par.chave + " → " + par.valor + "] ");
                }
            }
            System.out.println();
        }
    }

    public static void info() {
        System.out.println("Tabela Hash → acessa valores rapidamente por chave.");
        System.out.println("Vantagem: busca e inserção muito rápidas.");
        System.out.println("Limitação: quando muitas chaves caem no mesmo índice (colisão), as operações ficam mais lentas.\n");
    }
}