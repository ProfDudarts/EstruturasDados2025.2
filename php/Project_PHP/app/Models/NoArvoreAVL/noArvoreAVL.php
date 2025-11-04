<?php
namespace Estruturas;

if (!defined('RBPPAVL_DEBUG')) {
    define('RBPPAVL_DEBUG', 10);
    define('RBPPAVL_INFO', 20);
    define('RBPPAVL_NOTICE', 30);
    define('RBPPAVL_WARNING', 40);
    define('RBPPAVL_ERROR', 50);
    define('RBPPAVL_TEXT', 60);
}

if (!defined('RBPPAVL_VALIDATION_OK')) {
    define('RBPPAVL_VALIDATION_OK', 0);
    define('RBPPAVL_VALIDATION_HEIGHT_FAILURE', 1);
    define('RBPPAVL_VALIDATION_BALANCE_FAILURE', 2);
}

/**
 * PHP versão 5
 *
 * @category Estruturas
 * @package  Rbppavl
 * @author   mondrake <mondrake@mondrake.org>
 * @license  http://www.gnu.org/licenses/gpl.html GNU GPLv3
 */

// CORREÇÃO: Removida a herança de 'extends ArvoreAVL'. Um nó não deve ser a árvore.
class NoArvoreAVL 
{
    /**
     * Ponteiros para as subárvores esquerda e direita do nó.
     *
     * @type array 
     */
    public $link = array( null, null );
    
    /**
     * Ponteiro para o nó pai.
     *
     * @type NoArvoreAVL 
     */
    public $parent = null;
    
    /**
     * Ponteiro para o objeto de dados (carga útil) do nó.
     *
     * @type object
     */
    public $data;
    
    /**
     * Altura do nó.
     *
     * @type integer
     */
    public $height;

    /**
     * Constrói uma instância de NoArvoreAVL.
     *
     * @param object $tree   a árvore chamadora // CORREÇÃO: O parâmetro $tree foi removido do método, mas mantido no PHPDoc original para referência.
     * @param object $data   o objeto do nó
     * @param object $parent o NoArvoreAVL pai
     *
     * @access private
     */
    // CORREÇÃO: Visibilidade alterada para 'protected' (ou 'private') para que apenas a árvore possa criar nós.
    protected function __construct($data, $parent)
    {
        // Atributos do Nó
        $this->link[0] = $this->link[1] = null;
        $this->parent = $parent;
        $this->data = $data;
        $this->height = 0;
    }

    /**
     * Retorna o valor atual do fator de balanceamento do nó.
     *
     * O balanceamento é determinado pela diferença das alturas das
     * subárvores do nó.
     *
     * @return int valor do fator de balanceamento
     *
     * @access private
     */
    // CORREÇÃO: Visibilidade alterada para 'protected' para encapsulamento.
    protected function balance()
    {
        // MELHORIA: Uso do operador null coalesce '??' para sintaxe mais limpa (assumindo PHP >= 7.0).
        // Se a sintaxe não for suportada, o código original era mais seguro para versões antigas.
        $lHeight = $this->link[0]->height ?? -1; 
        $rHeight = $this->link[1]->height ?? -1;
        return $rHeight - $lHeight;
    }

    /**
     * Redefine a altura do nó.
     *
     * A altura é determinada a partir do valor da propriedade 'height'
     * das subárvores.
     *
     * @return null
     *
     * @access private
     */
    // CORREÇÃO: Visibilidade alterada para 'protected' para encapsulamento.
    protected function heightReset()
    {
        $lHeight = $this->link[0] ? $this->link[0]->height : -1;
        $rHeight = $this->link[1] ? $this->link[1]->height : -1;
        $this->height = max($lHeight, $rHeight) + 1;
        return null;
    }

    /**
     * Força a remoção do nó e dos dados associados da árvore.
     *
     * Remove recursivamente o nó, todos os nós em suas subárvores, e todos
     * os objetos de dados associados sem recalcular alturas e fatores
     * de balanceamento do restante da árvore.
     * Chamado por NoArvoreAVL::_destruct() para liberar memória ordenadamente ao
     * destruir um objeto de árvore.
     *
     * @param int &$ctr contador de nós removidos da árvore // CORREÇÃO: Tipo de 'object' para 'int'.
     *
     * @return int número de nós removidos da árvore
     *
     * @access private
     */
    // CORREÇÃO: Visibilidade alterada para 'protected' para encapsulamento.
    protected function wipe(&$ctr)
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
        // MELHORIA: Garantindo que a referência ao pai também seja removida.
        $this->parent = null;
        $ctr++;
        // CORREÇÃO: Return ajustado para usar apenas $ctr.
        return $ctr; 
    }

    /**
     * Valida a conformidade do nó com as regras AVL.
     *
     * Este método deve ser usado apenas para fins de depuração.
     *
     * Chamado por NoArvoreAVL::debugValidate().
     *
     * @param int     $balanceFactor fator de balanceamento
     * @param int     &$status       status de falha
     * @param boolean $recurse       se verdadeiro, valida recursivamente todos os nós nas subárvores do nó
     *
     * @return NoArvoreAVL|null ponteiro para o nó que falha nas regras AVL, ou nulo se o nó estiver em conformidade // CORREÇÃO: Tipagem mais específica do retorno.
     *
     * @access private
     */
    // CORREÇÃO: Visibilidade alterada para 'protected' para encapsulamento.
    protected function debugNodeValidate($balanceFactor, &$status, $recurse = true)
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
        if ($this->height <> $this->_debugHeightCalculate()) {
            // Erro de altura
            $status = RBPPAVL_VALIDATION_HEIGHT_FAILURE;
            return $this;
        }
        if (abs($this->balance()) > $balanceFactor) {
            // Erro de balanceamento
            $status = RBPPAVL_VALIDATION_BALANCE_FAILURE;
            return $this;
        }
        // validação bem-sucedida do nó
        $status = RBPPAVL_VALIDATION_OK;
        return null;
    }

    /**
     * Calcula a altura do nó.
     *
     * Este método deve ser usado apenas para fins de depuração.
     *
     * Diferente de heightReset(), a altura é calculada recursivamente
     * calculando a altura das subárvores do nó a partir do nó
     * folha para cima. Este método é usado por debugNodeValidate() para garantir
     * que a propriedade 'height' de um nó corresponde à altura real.
     * Em modo normal, a propriedade 'height' armazena a altura do nó atual,
     * e é atualizada pelos métodos de ArvoreAVL para mantê-la consistente
     * durante as operações de balanceamento da árvore.
     *
     * @return int a altura do nó
     *
     * @access private
     */
    private function _debugHeightCalculate()
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