import Models.TabelaHash;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class TabelaHashTeste {
    @Test
    public void testInserirEBuscar() {
        TabelaHash<String, Integer> tabela = new TabelaHash<>(5);
        tabela.inserir("Alice", 25);
        tabela.inserir("Bruno", 30);

        assertEquals(25, tabela.buscar("Alice"));
        assertEquals(30, tabela.buscar("Bruno"));

        assertThrows(IllegalArgumentException.class, () -> tabela.inserir(null, 1));
        assertThrows(IllegalArgumentException.class, () -> tabela.buscar(null));
    }

    @Test
    public void testAtualizarValorExistente() {
        TabelaHash<String, Integer> tabela = new TabelaHash<>(5);
        tabela.inserir("Daniel", 10);
        tabela.inserir("Daniel", 20); // deve atualizar valor
        assertEquals(20, tabela.buscar("Daniel"));
    }

    @Test
    public void testRemoverElemento() {
        TabelaHash<String, Integer> tabela = new TabelaHash<>(5);
        tabela.inserir("Carla", 40);
        assertEquals(40, tabela.buscar("Carla"));

        tabela.remover("Carla");
        assertNull(tabela.buscar("Carla"));
    }

    @Test
    public void testBuscarElementoInexistente() {
        TabelaHash<String, Integer> tabela = new TabelaHash<>(5);
        tabela.inserir("Eva", 50);
        assertNull(tabela.buscar("Zara")); // não existe
    }

    @Test
    public void testColisoes() {
        // Criar uma tabela pequena pra forçar colisões
        TabelaHash<Integer, String> tabela = new TabelaHash<>(2);
        tabela.inserir(1, "um");
        tabela.inserir(3, "três"); // 1 e 3 têm o mesmo hash mod 2
        tabela.inserir(5, "cinco");

        assertEquals("um", tabela.buscar(1));
        assertEquals("três", tabela.buscar(3));
        assertEquals("cinco", tabela.buscar(5));
    }

    @Test
    public void testRemoverElementoEmColisao() {
        TabelaHash<Integer, String> tabela = new TabelaHash<>(2);
        tabela.inserir(1, "um");
        tabela.inserir(3, "três");
        tabela.remover(1);

        assertNull(tabela.buscar(1));  // removido
        assertEquals("três", tabela.buscar(3)); // ainda existe
    }

    @Test
    public void testSubstituirValorEmColisao() {
        TabelaHash<Integer, String> tabela = new TabelaHash<>(2);
        tabela.inserir(1, "um");
        tabela.inserir(3, "três");
        tabela.inserir(1, "novo um"); // substitui mesmo com colisão

        assertEquals("novo um", tabela.buscar(1));
        assertEquals("três", tabela.buscar(3));
    }
}