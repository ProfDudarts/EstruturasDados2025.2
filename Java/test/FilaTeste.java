import Models.Fila;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class FilaTeste {

    @Test
    public void testEnfileirarEDesenfileirar() {
        Fila<Integer> fila = new Fila<>();
        fila.enqueue(10);
        fila.enqueue(20);
        fila.enqueue(30);

        assertEquals(10, fila.dequeue());
        assertEquals(20, fila.dequeue());
        assertEquals(30, fila.dequeue());
        assertThrows(RuntimeException.class, fila::dequeue); // fila vazia
    }

    @Test
    public void testFrenteNaoRemoveElemento() {
        Fila<String> fila = new Fila<>();
        fila.enqueue("A");
        fila.enqueue("B");

        assertEquals("A", fila.peek());
        assertEquals("A", fila.peek()); // ainda o mesmo
        assertEquals("A", fila.dequeue()); // agora sai
    }

    @Test
    public void testFilaVazia() {
        Fila<Double> fila = new Fila<>();
        assertTrue(fila.isEmpty());

        fila.enqueue(5.5);
        assertFalse(fila.isEmpty());

        fila.dequeue();
        assertTrue(fila.isEmpty());
    }

    @Test
    public void testOrdemMantida() {
        Fila<String> fila = new Fila<>();
        fila.enqueue("um");
        fila.enqueue("dois");
        fila.enqueue("três");

        assertEquals("um", fila.dequeue());
        assertEquals("dois", fila.dequeue());
        assertEquals("três", fila.dequeue());
    }

    @Test
    public void testDesenfileirarFilaVazia() {
        Fila<Integer> fila = new Fila<>();
        assertThrows(RuntimeException.class, fila::dequeue); // deve retornar null
    }
}