<?php

require __DIR__ . '';
require __DIR__ . ''; 

function testePilha() {
    $pilha = new Estruturas\_pilha();
    assert($pilha->estaVazia() === true, "A pilha deve começar vazia");

    $no1 = new Estruturas\No("A");
    $pilha->inserir($no1);
    assert($pilha->estaVazia() === false, "A pilha não deve estar vazia após inserir");

    $topo = $pilha->topo();
    assert($topo === $no1, "O topo deve ser o último elemento inserido");

    $removido = $pilha->remover();
    assert($removido === $no1, "O elemento removido deve ser o mesmo que entrou");

    assert($pilha->estaVazia() === true, "A pilha deve estar vazia após remover o único elemento");

    echo "Todos os testes passaram";
}
