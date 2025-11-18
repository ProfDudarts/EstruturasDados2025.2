<?php

require __DIR__ . '';
require __DIR__ . '';

function testeListaEncadeada() {
    $lista = new Estruturas\ListaEncadeada();
    assert($lista->taVazia() === true, "A lista deve começar vazia");
    assert($lista->obterTamanho() === 0, "O tamanho inicial deve ser 0");

    
    $no1 = new Estruturas\No("A");
    $no2 = new Estruturas\No("B");
    $no3 = new Estruturas\No("C");

    // Inserir no inicio
    $lista->inserirNoInicio($no1);
    assert($lista->taVazia() === false, "A lista não deve estar vazia após inserir");
    assert($lista->obterTamanho() === 1, "O tamanho deve ser 1 após inserir um nó no início");

    // Inserir no fim
    $lista->inserirNoFim($no2);
    assert($lista->obterTamanho() === 2, "O tamanho deve ser 2 após inserir no fim");

    // Inserir outro no inicio
    $lista->inserirNoInicio($no3);
    assert($lista->obterTamanho() === 3, "O tamanho deve ser 3 após inserir outro nó no início");

    // Buscar 
    $busca = $lista->buscar("B");
    assert($busca === $no2, "O nó buscado deve ser o correto");

    $buscaInexistente = $lista->buscar("X");
    assert($buscaInexistente === null, "Buscar valor inexistente deve retornar null");

    // Remover do inicio
    $removidoInicio = $lista->removerDoInicio();
    assert($removidoInicio === $no3, "O nó removido do início deve ser o último inserido no início");
    assert($lista->obterTamanho() === 2, "Tamanho deve ser 2 após remover do início");

    // Remover por valor
    $removidoValor = $lista->removerPorValor("B");
    assert($removidoValor === $no2, "O nó removido por valor deve ser correto");
    assert($lista->obterTamanho() === 1, "Tamanho deve ser 1 após remover por valor");

    // Remover valor inexistente
    $removidoInexistente = $lista->removerPorValor("X");
    assert($removidoInexistente === null, "Remover valor inexistente deve retornar null");

    echo "Todos os testes da lista encadeada passaram\n";
}


