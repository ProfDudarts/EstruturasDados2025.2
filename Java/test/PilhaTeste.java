import Models.Pilha;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class PilhaTeste {

    @Test
    public void testEmpilharEDesempilhar() {
        Pilha<Integer> pilha = new Pilha<>();
        pilha.push(10);
        pilha.push(20);
        pilha.push(30);

        assertEquals(30, pilha.pop());
        assertEquals(20, pilha.pop());
        assertEquals(10, pilha.pop());
        assertThrows(RuntimeException.class, pilha::pop); // pilha vazia
    }

    @Test
    public void testTopoNaoRemoveElemento() {
        Pilha<String> pilha = new Pilha<>();
        pilha.push("A");
        pilha.push("B");

        assertEquals("B", pilha.peek());
        assertEquals("B", pilha.peek()); // ainda o mesmo
        assertEquals("B", pilha.pop());
        assertEquals("A", pilha.peek()); // agora A é o topo
    }

    @Test
    public void testPilhaVazia() {
        Pilha<Double> pilha = new Pilha<>();
        assertTrue(pilha.isEmpty());

        pilha.push(3.14);
        assertFalse(pilha.isEmpty());

        pilha.pop();
        assertTrue(pilha.isEmpty());
    }

    @Test
    public void testOrdemInversa() {
        Pilha<String> pilha = new Pilha<>();
        pilha.push("primeiro");
        pilha.push("segundo");
        pilha.push("terceiro");

        assertEquals("terceiro", pilha.pop());
        assertEquals("segundo", pilha.pop());
        assertEquals("primeiro", pilha.pop());
    }

    @Test
    public void testDesempilharPilhaVazia() {
        Pilha<Integer> pilha = new Pilha<>();
        assertThrows(RuntimeException.class, pilha::pop);
    }
}