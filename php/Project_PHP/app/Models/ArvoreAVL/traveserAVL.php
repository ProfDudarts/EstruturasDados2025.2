<?php
namespace Estruturas;


// ArvoreAVL (A classe principal, deve ser importada ou definida)
if (!class_exists('Estruturas\ArvoreAVL')) {
    class ArvoreAVL {
        public $root = null; 
        public $cbc = null;
        // Adicionar um placeholder de método 'balance' se usado
        public function balance() { return 0; }
    }
}

// RbppavlNode (A classe do nó, deve ser importada ou definida)
if (!class_exists('Estruturas\RbppavlNode') && !class_exists('Estruturas\NoArvoreAVL')) {
    // Usando NoArvoreAVL como placeholder, pois o RbppavlCommon usa NoArvoreAVL
    class NoArvoreAVL {
        public $data;
        public $link = [null, null];
        public function balance() { return 0; }
    }
    // Adicionando um alias se o docblock realmente usar RbppavlNode
    class_alias('Estruturas\NoArvoreAVL', 'Estruturas\RbppavlNode');
}

// RbppavlCommon (A classe base que TraveserAVL herda)
// Assumimos que o RbppavlCommon foi colocado no namespace Estruturas
if (!class_exists('Estruturas\RbppavlCommon')) {
    abstract class RbppavlCommon {
        // Propriedades e métodos mínimos usados por TraveserAVL
        public $cbc = null;
        protected $debugMode = false;
        protected function resetStatus() {}
        protected function nodeFirst($tree) {}
        protected function nodeLast($tree) {}
        protected function checkData($data, $method) {}
        protected function setStatus($id, $params = null, $reset = false) {}
        protected function nodeFind($tree, $data, &$y, &$q, &$dir, $mode) {}
        protected function nodeNext($tree, $node) {}
        protected function nodePrev($tree, $node) {}
    }
}


// Constantes (Para resolver RBPPAVL_FIND_EXACT_MATCH)
if (!defined('RBPPAVL_FIND_EXACT_MATCH')) {
    define('RBPPAVL_FIND_EXACT_MATCH', 1);
    define('RBPPAVL_FIND_PREV_MATCH', 2);
    define('RBPPAVL_FIND_NEXT_MATCH', 3);
}
if (!defined('RBPPAVL_WARNING')) {
    define('RBPPAVL_WARNING', 40); // para setStatus(102)
}


/**
 * PHP version 5
 *
 * @category Estruturas
 * @package  Arvores
 * @author   mondrake <mondrake@mondrake.org>
 * @license  http://www.gnu.org/licenses/gpl.html GNU GPLv3
 */

// CORREÇÃO A: Extends RbppavlCommon
class TraveserAVL extends RbppavlCommon
{
    /**
     * A arvore a ser percorrida
     *
     * @type ArvoreAVL 
     */
    private $_tree;

    /**
     * Cursor para o nó atual
     *
     * @type RbppavlNode 
     */
    private $_node;

    /**
     * Constrói uma instância de TraveserAVL.
     *
     * @param ArvoreAVL $tree      a árvore chamadora
     * @param bool        $debugMode modo de depuração (padrão: false)
     *
     * @access public
     */
    public function __construct(ArvoreAVL $tree = null, $debugMode = false)
    {
        $this->_tree = $tree;
        // Assumindo que $this->cbc foi resolvido como public em RbppavlCommon
        $this->cbc = $tree->cbc; 
        $this->_node = null;
        $this->debugMode = $debugMode;
    }

    /**
     * Retorna o objeto de dados com o menor valor (mais à esquerda).
     * * O cursor é definido para o nó identificado.
     * * @return object o objeto de dados mais à esquerda na árvore ou null se a árvore estiver vazia.
     * @api
     * */
    public function first()
    {
        $this->resetStatus();
        $this->_node = $this->nodeFirst($this->_tree);
        return ($this->_node) ? $this->_node->data : null;
    }

    /**
     * Retorna o objeto de dados com o maior valor (mais à direita).
     * * O cursor é definido para o nó identificado.
     * * @return object o objeto de dados mais à direita na árvore ou null se a árvore estiver vazia.
     * @api
     */
    public function last()
    {
        $this->resetStatus();
        $this->_node = $this->nodeLast($this->_tree);
        return ($this->_node) ? $this->_node->data : null;
    }
    
    /**
     * Procura um objeto de dados na árvore.
     *
     * O cursor é definido para o nó identificado.
     *
     * @param object  $data o objeto de dados a ser procurado
     * @param integer $mode o modo de busca (padrão: RBPPAVL_FIND_EXACT_MATCH)
     *
     * @return object o objeto de dados encontrado ou null se não encontrado
     *
     * @api
     */
    public function find($data, $mode = RBPPAVL_FIND_EXACT_MATCH)
    {
        $this->resetStatus();
        if (!$this->checkData($data, __FUNCTION__)) {
            return null;
        }
        // Arvore vazia
        if (is_null($this->_tree) or is_null($this->_tree->root)) {
            $this->_node = null;
            $this->setStatus(102);
            return null;
        }
        $y = $this->_tree->root;
        $q = null;
        $dir = null;
        $this->_node = $this->nodeFind($this->_tree, $data, $y, $q, $dir, $mode);
        return ($this->_node) ? $this->_node->data : null;
    }

    /**
     * Retorna o próximo objeto de dados em sequência in-order.
     * * Atualiza o cursor. Se não houver mais objetos de dados, retorna null.
     * Se o cursor ainda não estiver definido, retorna o primeiro objeto de dados em sequência in
     * -order.
     * * @return object próximo objeto de dados em sequência in-order ou null se o cursor estiver no nó mais à direita
     * @api
     */
    public function next()
    {
        $this->resetStatus();
        $this->_node = $this->nodeNext($this->_tree, $this->_node);
        return ($this->_node) ? $this->_node->data : null;
    }

    /**
     * Retorna o objeto de dados anterior em sequência in-order.
     * * Atualiza o cursor. Se não houver mais objetos de dados, retorna null.
     * Se o cursor ainda não estiver definido, retorna o último objeto de dados em sequência in
     * -order.
     * * @return object objeto de dados anterior em sequência in-order ou null se o cursor estiver no nó mais à esquerda
     * @api
     */
    public function prev()
    {
        $this->resetStatus();
        $this->_node = $this->nodePrev($this->_tree, $this->_node);
        return ($this->_node) ? $this->_node->data : null;
    }
    
    /**
     * Retorna o objeto de dados no nó atual do cursor.
     * * @return object objeto de dados no nó atual ou null se o cursor não estiver definido
     * @api
     */
    public function curr()
    {
        $this->resetStatus();
        return ($this->_node) ? $this->_node->data : null;
    }
}