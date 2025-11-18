<?php
declare(strict_types=1);

namespace App\Estruturas;

use App\Estruturas\ConstantesAVL;

class NoArvoreAVL 
{
    /**
     * Ponteiros para as subárvores esquerda e direita do nó.
     *
     * @var array<int, NoArvoreAVL|null> 
     */
    public array $link = [ null, null ];
    
    /**
     * Ponteiro para o nó pai.
     *
     * @var NoArvoreAVL|null
     */
    public ?NoArvoreAVL $parent = null;
    
    /**
     * Ponteiro para o objeto de dados (carga útil) do nó.
     *
     * @var mixed
     */
    public $data;
    
    /**
     * Altura do nó.
     *
     * @var int
     */
    public int $height;

    /**
     * Constrói uma instância de NoArvoreAVL.
     *
     * @param mixed             $data   o objeto de dados do nó
     * @param NoArvoreAVL|null  $parent o NoArvoreAVL pai
     *
     * @access protected
     */
    public function __construct($data, ?NoArvoreAVL $parent)
    {
        // Atributos do Nó
        $this->link[0] = $this->link[1] = null;
        $this->parent = $parent;
        $this->data = $data;
        $this->height = 0;
    }

    /**
     * Retorna o valor atual do fator de balanceamento do nó (Altura Direita - Altura Esquerda).
     *
     * @return int valor do fator de balanceamento
     *
     * @access protected
     */
    public function balance(): int
    {
        // Uso de type-hinting e operador null coalesce
        $lHeight = $this->link[0]->height ?? -1; 
        $rHeight = $this->link[1]->height ?? -1;
        return $rHeight - $lHeight; // Nota: AVL tradicional usa L-R, mas mantive R-L como estava no seu código.
    }

    /**
     * Redefine a altura do nó com base nos filhos.
     *
     * @return void
     *
     * @access protected
     */
    public function heightReset(): void
    {
        $lHeight = $this->link[0] ? $this->link[0]->height : -1;
        $rHeight = $this->link[1] ? $this->link[1]->height : -1;
        $this->height = max($lHeight, $rHeight) + 1;
    }

    /**
     * Força a remoção do nó e dos dados associados da árvore.
     *
     * @param int &$ctr contador de nós removidos da árvore 
     *
     * @return int número de nós removidos da árvore
     *
     * @access protected
     */
    public function wipe(int &$ctr): int
    {
        if ($this->link[0]) {
            $this->link[0]->wipe($ctr);
            $this->link[0] = null;
        }
        if ($this->link[1]) {
            $this->link[1]->wipe($ctr);
            $this->link[1] = null;
        }
        // Limpar nó
        $this->data = null;
        $this->link[0] = $this->link[1] = null;
        if ($this->parent) {
            $dir = ($this === $this->parent->link[0] ? 0 : 1);
            $this->parent->link[$dir] = null;
        }
        $this->parent = null;
        $ctr++;
        return $ctr; 
    }

    /**
     * Valida a conformidade do nó com as regras AVL.
     *
     * @param int     $balanceFactor fator de balanceamento
     * @param int     &$status       status de falha
     * @param boolean $recurse       se verdadeiro, valida recursivamente
     *
     * @return NoArvoreAVL|null ponteiro para o nó que falha nas regras AVL, ou nulo se OK
     *
     * @access protected
     */
    public function debugNodeValidate(int $balanceFactor, int &$status, bool $recurse = true): ?NoArvoreAVL
    {
        if ($recurse) {
            if ($this->link[0]) {
                $n = $this->link[0]->debugNodeValidate($balanceFactor, $status);
                if ($status > 0) {
                    return $n;
                }
            }
            if ($this->link[1]) {
                $n = $this->link[1]->debugNodeValidate($balanceFactor, $status);
                if ($status > 0) {
                    return $n;
                }
            }
        }
        
        // Validação do nó atual
        if ($this->height !== $this->_debugHeightCalculate()) {
            // Erro de altura
            $status = ConstantesAVL::VALIDATION_HEIGHT_FAILURE;
            return $this;
        }
        
        if (abs($this->balance()) > $balanceFactor) {
            // Erro de balanceamento
            $status = ConstantesAVL::VALIDATION_BALANCE_FAILURE;
            return $this;
        }
        
        // validação bem-sucedida do nó
        $status = ConstantesAVL::VALIDATION_OK;
        return null;
    }

    /**
     * Calcula a altura do nó recursivamente (apenas para depuração).
     *
     * @return int a altura do nó
     *
     * @access private
     */
    private function _debugHeightCalculate(): int
    {
        if (!$this->link[0] and !$this->link[1]) {
            return 0;
        } else {
            $lHeight = $this->link[0] ?
                       $this->link[0]->_debugHeightCalculate() : -1;
            $rHeight = $this->link[1] ?
                       $this->link[1]->_debugHeightCalculate() : -1;
            return (max($lHeight, $rHeight) + 1);
        }
    }
}
