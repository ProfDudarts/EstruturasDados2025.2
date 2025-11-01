import Models.ListaEncadeada;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class ListaEncadeadaTeste {

    @Test
    public void testAdicionarEConter() {
        ListaEncadeada<Integer> lista = new ListaEncadeada<>();
        lista.inserir(10);
        lista.inserir(20);
        lista.inserir(30);

        assertTrue(lista.contem(10));
        assertTrue(lista.contem(20));
        assertTrue(lista.contem(30));
        assertFalse(lista.contem(40));
    }

    @Test
    public void testRemover() {
        ListaEncadeada<String> lista = new ListaEncadeada<>();
        lista.inserir("A");
        lista.inserir("B");
        lista.inserir("C");

        assertTrue(lista.remover("B"));
        assertFalse(lista.contem("B"));
        assertEquals(2, lista.tamanho());

        assertTrue(lista.remover("A")); // remove o primeiro
        assertTrue(lista.remover("C")); // remove o último
        assertTrue(lista.estaVazia());

        assertThrows(RuntimeException.class, () -> lista.remover("VAZIO"));
    }

    @Test
    public void testRemoverElementoInexistente() {
        ListaEncadeada<Integer> lista = new ListaEncadeada<>();
        lista.inserir(1);
        lista.inserir(2);

        assertFalse(lista.remover(3));
        assertEquals(2, lista.tamanho());
    }

    @Test
    public void testListaVazia() {
        ListaEncadeada<Double> lista = new ListaEncadeada<>();
        assertTrue(lista.estaVazia());

        lista.inserir(5.5);
        assertFalse(lista.estaVazia());

        lista.remover(5.5);
        assertTrue(lista.estaVazia());
    }

    @Test
    public void testTamanho() {
        ListaEncadeada<Integer> lista = new ListaEncadeada<>();
        assertEquals(0, lista.tamanho());

        lista.inserir(1);
        lista.inserir(2);
        assertEquals(2, lista.tamanho());

        lista.remover(1);
        assertEquals(1, lista.tamanho());
    }
}
