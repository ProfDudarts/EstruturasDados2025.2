<?php
namespace Estruturas;

class No {
    private mixed $valor;
    private ?self $proximo;

    public function __construct(mixed $valor) {
        $this->valor = $valor;
        $this->proximo = null;
    }

    public function getValor() : mixed {
        return $this->valor;
    }

    public function getProximo() : ?self {
        return $this->proximo;
    }

    public function setProximo(?self $no) : void {
        $this->proximo = $no;
        return $this;
    }

    public function imprimir() : void {
        if (is_scalar($this->valor)) {
            echo $this->valor . PHP_EOL;
        } else {
            echo "[Objeto No]" . PHP_EOL;
        }
    }
}