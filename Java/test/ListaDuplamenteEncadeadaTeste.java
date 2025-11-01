import Models.ListaDuplamenteEncadeada;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class ListaDuplamenteEncadeadaTeste {

    @Test
    public void testAdicionarFimEConter() {
        ListaDuplamenteEncadeada<Integer> lista = new ListaDuplamenteEncadeada<>();
        lista.inserir(1);
        lista.inserir(2);
        lista.inserir(3);

        assertTrue(lista.contem(1));
        assertTrue(lista.contem(2));
        assertTrue(lista.contem(3));
        assertFalse(lista.contem(4));
        assertEquals(3, lista.tamanho());
    }

    @Test
    public void testRemoverMeioInicioEFim() {
        ListaDuplamenteEncadeada<String> lista = new ListaDuplamenteEncadeada<>();
        lista.inserir("A");
        lista.inserir("B");
        lista.inserir("C");
        lista.inserir("D");

        assertTrue(lista.remover("B")); // meio
        assertFalse(lista.contem("B"));

        assertTrue(lista.remover("A")); // início
        assertFalse(lista.contem("A"));

        assertTrue(lista.remover("D")); // fim
        assertFalse(lista.contem("D"));

        assertTrue(lista.contem("C"));
        assertEquals(1, lista.tamanho());

        assertTrue(lista.remover("C"));
        assertThrows(RuntimeException.class, () -> lista.remover("VAZIO"));
    }

    @Test
    public void testRemoverElementoInexistente() {
        ListaDuplamenteEncadeada<Integer> lista = new ListaDuplamenteEncadeada<>();
        lista.inserir(10);
        lista.inserir(20);

        assertFalse(lista.remover(30));
        assertEquals(2, lista.tamanho());
    }

    @Test
    public void testListaVazia() {
        ListaDuplamenteEncadeada<Double> lista = new ListaDuplamenteEncadeada<>();
        assertTrue(lista.estaVazia());

        lista.inserir(1.1);
        assertFalse(lista.estaVazia());

        lista.remover(1.1);
        assertTrue(lista.estaVazia());
    }
}
