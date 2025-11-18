<?php

require __DIR__ . '';
require __DIR__ . '';
//
function testeFila() {
    $fila = new Estruturas\_fila();


    assert($fila->taVazio() === true, "A fila deve começar vazia");


    $no1 = new Estruturas\No("A");
    $no2 = new Estruturas\No("B");


    $fila->enfileirar($no1);
    assert($fila->taVazio() === false, "A fila não deve estar vazia após enfileirar");

    $fila->enfileirar($no2);
    

    $removido1 = $fila->desenfileirar();
    assert($removido1 === $no1, "O primeiro nó removido deve ser o no1");

    $removido2 = $fila->desenfileirar();
    assert($removido2 === $no2, "O segundo nó removido deve ser o no2");


    assert($fila->taVazio() === true, "A fila deve estar vazia após remover todos os nós");


    $removidoVazio = $fila->desenfileirar();
    assert($removidoVazio === null, "Remover de uma fila vazia deve retornar null");

    echo "Todos os testes da fila passaram\n";
}


