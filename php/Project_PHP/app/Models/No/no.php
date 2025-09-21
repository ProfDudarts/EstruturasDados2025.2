<?php
namespace Estruturas;

class No {
    public mixed $valor;
    public ?self $proximo;

    public function __construct(mixed $valor) {
        $this->valor = $valor;
        $this->proximo = null;
    }

    public function imprimir() : void {
        echo $this->valor . PHP_EOL;
    }
}