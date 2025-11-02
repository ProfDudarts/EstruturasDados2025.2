<?php
namespace Estruturas;
/**
 * PHP version 5
 *
 * @category Estruturas
 * @package  Arvores
 * @author   mondrake <mondrake@mondrake.org>
 * @license  http://www.gnu.org/licenses/gpl.html GNU GPLv3
 */

class TraveserAVL extends AVLCommon
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
     * @param ArvoreAVL $tree      a árvore chamadora
     * @param bool        $debugMode modo de depuração (padrão: false)
     *
     * @access public
     */
    public function __construct(ArvoreAVL $tree = null, $debugMode = false)
    {
        $this->_tree = $tree;
        $this->cbc = $tree->cbc;
        $this->_node = null;
        $this->debugMode = $debugMode;
    }

    /**
     * Retorna o objeto de dados com o menor valor (mais à esquerda).
     * 
     * O cursor é definido para o nó identificado.
     * 
     * @return object o objeto de dados mais à esquerda na árvore ou null se a árvore estiver vazia.
     * @api
    
     */
    public function first()
    {
        $this->resetStatus();
        $this->_node = $this->nodeFirst($this->_tree);
        return ($this->_node) ? $this->_node->data : null;
    }

    /**
     * Retorna o objeto de dados com o maior valor (mais à direita).
     * 
     * O cursor é definido para o nó identificado.
     * 
     * @return object o objeto de dados mais à direita na árvore ou null se a árvore estiver vazia.
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
     * @param object  $data o objeto de dados a ser procurado
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
     * 
     * Atualiza o cursor. Se não houver mais objetos de dados, retorna null.
     * Se o cursor ainda não estiver definido, retorna o primeiro objeto de dados em sequência in
     * -order.
     * 
     * @return object próximo objeto de dados em sequência in-order ou null se o cursor estiver no nó mais à direita
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
     * 
     * Atualiza o cursor. Se não houver mais objetos de dados, retorna null.
     * Se o cursor ainda não estiver definido, retorna o último objeto de dados em sequência in
     * -order.
     * 
     * @return object objeto de dados anterior em sequência in-order ou null se o cursor estiver no nó mais à esquerda
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
     * 
     * @return object objeto de dados no nó atual ou null se o cursor não estiver definido
     * @api
     */
    public function curr()
    {
        $this->resetStatus();
        return ($this->_node) ? $this->_node->data : null;
    }
}