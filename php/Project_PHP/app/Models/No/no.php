<?php
namespace Estruturas;

class No {
    private mixed $valor;      // ->Armazena o valor do nó
    private ?self $proximo;    // ->Referência para o próximo nó da lista (ou null se for o último)

    // --Construtor: inicializa o nó com um valor
    public function __construct(mixed $valor) {
        $this->valor = $valor;
        $this->proximo = null; // ->Por padrão, não aponta para nenhum próximo nó
    }

    // --Retorna o valor armazenado no nó
    public function getValor() : mixed {
        return $this->valor;
    }

    // --Retorna o próximo nó ou null se não houver
    public function getProximo() : ?self {
        return $this->proximo;
    }

    // --Define qual será o próximo nó
    public function setProximo(?self $no) : void {
        $this->proximo = $no;
    }

    // --Imprime o valor do nó
    public function imprimir() : void {
        if (is_scalar($this->valor)) {       // ->Se for um tipo simples (int, string, etc.)
            echo $this->valor . PHP_EOL;    // ->Exibe o valor
        } else {
            echo "[Objeto No]" . PHP_EOL;   // ->Caso seja um objeto ou array, exibe texto genérico
        }
    }
}
