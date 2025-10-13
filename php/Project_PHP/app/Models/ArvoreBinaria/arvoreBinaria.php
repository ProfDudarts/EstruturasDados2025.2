<?php
namespace Estruturas;

use Estruturas\NoArvore as No;

class ArvoreBinaria
{
    private ?No $root = null;

    public function __construct()
    {
    }
    
    public function taCompleto(No $no): bool
    {
        return $no->getEsquerdo() !== null && $no->getDireito() !== null;
    }

    public function inserir(No $valor): void
    {
        if ($this->root === null) {
            $this->root = $valor;
        } else {
            $this->_inserir($this->root, $valor);
        }
    }

    private function _inserir(No $pai, No $valor): void
    {
        if ($pai->getEsquerdo() === null) {
            $pai->setEsquerdo($valor);
            return;
        }
        
        elseif ($pai->getDireito() === null) {
            $pai->setDireito($valor);
            return;
        }
        
        else {
            $this->_inserir($pai->getEsquerdo(), $valor);

            if ($this->taCompleto($pai->getEsquerdo())) {
                $this->_inserir($pai->getDireito(), $valor);
                return;
            } else {
                $this->_inserir($pai->getEsquerdo(), $valor);
                return;
            }
        }
    }

    public function exibir(): void
    {
        if ($this->root !== null) {
            $this->_exibir($this->root, 0, "R: ");
        }
    }

    private function _exibir(No $atual, int $nivel, string $prefixo): void
    {
        $indentacao = str_repeat(" ", $nivel);
        echo $indentacao . $prefixo . $atual->getValor() . "\n";

        if ($atual->getEsquerdo() !== null) {
            $this->_exibir($atual->getEsquerdo(), $nivel + 2, "E: ");
        }
        
        if ($atual->getDireito() !== null) {
            $this->_exibir($atual->getDireito(), $nivel + 2, "D: ");
        }
    }
}