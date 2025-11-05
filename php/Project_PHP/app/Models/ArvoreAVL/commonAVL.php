<?php

if (!class_exists('ArvoreAVL')) {
    class ArvoreAVL {
        public $root = null;
        public $balanceFactor = 1;
        public $cbc = null;
    }
}

if (!defined('RBPPAVL_DEBUG')) {
    define('RBPPAVL_DEBUG', 10);
    define('RBPPAVL_INFO', 20);
    define('RBPPAVL_NOTICE', 30);
    define('RBPPAVL_WARNING', 40);
    define('RBPPAVL_ERROR', 50);
    define('RBPPAVL_TEXT', 60);
}

if (!defined('RBPPAVL_VERSION_NUMBER')) {
    define('RBPPAVL_VERSION_NUMBER', '1.0');
    define('RBPPAVL_VERSION_STATE', 'STABLE');
}

if (!defined('RBPPAVL_FIND_EXACT_MATCH')) {
    define('RBPPAVL_FIND_EXACT_MATCH', 1);
    define('RBPPAVL_FIND_PREV_MATCH', 2);
    define('RBPPAVL_FIND_NEXT_MATCH', 3);
}

/**
 *
 * @category Estruturas
 * @package  ArvoreAVL
 * @authormondrake <mondrake@mondrake.org>
 * @license  http://www.gnu.org/licenses/gpl.html GNU GPLv3
 */

abstract class RbppavlCommon extends ArvoreAVL
{
    /**
     * Interface de comparação e callback
     *
     * @type InterfaceCBC 
     */
    public $cbc = null;

    /**
     * Modo de depuração ativado
     *
     * @type boolean 
     */
    protected $debugMode = false;

    /**
     * Limite de memória para operações de inserção na árvore
     *
     * @type integer
     */
    protected $memoryLimit = 0;

    /**
     * Limiar de memória para operações de inserção na árvore
     *
     * @type integer
     */
    protected $memoryThreshold = 0;
    /**
     * Status interno
     *
     * @type array 
     */
    protected $status = array(
        'level' => RBPPAVL_NOTICE,
        'code' => 100,
        'messageParams' => null);

                             
    /**
     * Chama métodos indefinidos
     * * @param string $name the method invoked
     * @param array  $args the arguments passed
     * @return null
     * @internal
     */

    public function __call($name, $args)
    {
        // CORREÇÃO: Os métodos mágicos devem retornar o valor do método chamado, 
        // ou null/lançar exceção. Deixamos como null, conforme o PHPDoc, mas a 
        // chamada de setStatus está correta para logar o erro.
        $this->setStatus(104, array('%method' => $name,));
        return null;
    }


    public function __get($name)
    {
        // inaccessible property 
        $this->setStatus(105, array('%property' => $name,));
        return null; // Adicionando retorno explícito
    }


    public function __set($name, $value)
    {
        // inaccessible property 
        $this->setStatus(105, array('%property' => $name,));
        return null; // Adicionando retorno explícito
    }


    public function getVersion($setStatus = false)
    {
        $this->resetStatus();
        if ($setStatus) {
            // ArvoreAVL version and state
            $this->setStatus(
                101, array(
                    '%version' => RBPPAVL_VERSION_NUMBER . RBPPAVL_VERSION_STATE,)
            );
        }
        return array(RBPPAVL_VERSION_NUMBER, RBPPAVL_VERSION_STATE);
    }

    /**
     * Procura um nó na árvore.
     *
     * @param ArvoreAVL $tree the tree to be searched
     * @param object   $data the data object to be found
     * @param NoArvoreAVL|null &$y reference to the top node to be rebalanced // CORREÇÃO PHPDOC
     * @param NoArvoreAVL|null &$q    reference to the parent of the found node // CORREÇÃO PHPDOC
     * @param integer     $dir  reference to the direction of the found node
     * @param integer     $mode the search mode
     *
     * @return NoArvoreAVL|null the found node or null if not found // CORREÇÃO PHPDOC
     *
     * @access protected
     */
    protected function nodeFind($tree, $data, &$y, &$q, &$dir, $mode)
    {
        for ($q = null, $p = $y; $p; $q = $p, $p = $p->link[$dir]) {
            $cmp = $tree->cbc->compare($data, $p->data);
    
            if ($cmp == 0) {
    
                if ($this->debugMode) {
                    $this->setStatus(9, array('%node' => $this->cbc->dump($p->data),));
                }
                return $p;
            }

            $dir = $cmp > 0 ? 1 : 0;

            if (abs($p->balance()) == $tree->balanceFactor) {
                $y = $p;
            }
        }
        switch ($mode) {
        case RBPPAVL_FIND_EXACT_MATCH:

            if ($this->debugMode) {
                $this->setStatus(8, array('%node' => $this->cbc->dump($data),));
            }
            return null;
        case RBPPAVL_FIND_PREV_MATCH:
            if ($dir == 0) {
                $p = $this->nodePrev($tree, $q);
            } else {
                $p = $q;
            }
            if ($this->debugMode) {
                if ($p) {
            
                    $this->setStatus(
                        13, array('%node' => $this->cbc->dump($data),
                                 '%prev' => $this->cbc->dump($p->data),)
                    );
                } else {
    
                    $this->setStatus(14, array('%node' => $this->cbc->dump($data),));
                }
            }
            return $p;
        case RBPPAVL_FIND_NEXT_MATCH:
            if ($dir == 1) {
                $p = $this->nodeNext($tree, $q);
            } else {
                $p = $q;
            }
            if ($this->debugMode) {
                if ($p) {
        
                    $this->setStatus(
                        15, array('%node' => $this->cbc->dump($data),
                                 '%prev' => $this->cbc->dump($p->data),)
                    );
                } else {
              
                    $this->setStatus(16, array('%node' => $this->cbc->dump($data),));
                }
            }
            return $p;
        }
        // Garantindo que a função sempre retorne algo fora do switch
        return null; 
    }

    /**
     * Retorna o nó anterior em sequência in-order. 
     * * Se não houver mais nós, retorna null.
     * @param ArvoreAVL $tree a árvore a ser pesquisada // CORREÇÃO PHPDOC
     * @param NoArvoreAVL|null      $node nó para iniciar a pesquisa // CORREÇÃO PHPDOC
     * @return NoArvoreAVL|null nó anterior em sequência in-order ou null se o nó mais à esquerda for a entrada // CORREÇÃO PHPDOC
     * @access protected
     * */
    protected function nodePrev($tree, $node)
    {
        if ($node == null) {
            return $this->nodeLast($tree);
        } elseif ($node->link[0] == null) {
            for ($p = $node, $q = $p->parent; ; $p = $q, $q = $q->parent) {
                if ($q == null or $p === $q->link[1]) {
                    return $q;
                }
            }
        } else {
            $q = $node->link[0];
            while ($q->link[1] != null) {
                $q = $q->link[1];
            }
            return $q;
        }
    }

    /**
     * Retorna o próximo nó em sequência in-order. 
     * * Se não houver mais nós, retorna null.
     *
     * @param ArvoreAVL $tree a árvore a ser pesquisada // CORREÇÃO PHPDOC
     * @param NoArvoreAVL|null      $node nó para iniciar a pesquisa // CORREÇÃO PHPDOC
     *
     * @return NoArvoreAVL|null próximo nó em sequência in-order ou null se o nó mais à direita for a entrada // CORREÇÃO PHPDOC
     *
     * @access protected
     */
    protected function nodeNext($tree, $node)
    {
        if ($node == null) {
            return $this->nodeFirst($tree);
        } elseif ($node->link[1] == null) {
            for ($p = $node, $q = $p->parent; ; $p = $q, $q = $q->parent) {
                if ($q == null or $p === $q->link[0]) {
                    return $q;
                }
            }
        } else {
            $q = $node->link[1];
            while ($q->link[0] != null) {
                $q = $q->link[0];
            }
            return $q;
        }
    }

    /**
     * Retorna o nó com o menor valor (mais à esquerda).
     *
     * @param ArvoreAVL $tree a árvore a ser pesquisada // CORREÇÃO PHPDOC
     *
     * @return NoArvoreAVL|null o nó mais à esquerda na árvore ou null se a árvore estiver vazia. // CORREÇÃO PHPDOC
     */
    protected function nodeFirst($tree)
    {
        // empty tree
        if (is_null($tree) or is_null($tree->root)) {
            $this->setStatus(102);
            return null;
        }
        $p = $tree->root;
        while ($p->link[0] != null) {
            $p = $p->link[0];
        }
        return $p;
    }

    /**
     * Retorna o nó com o maior valor (mais à direita).
     *
     * @param ArvoreAVL $tree a árvore a ser pesquisada // CORREÇÃO PHPDOC
     *
     * @return NoArvoreAVL|null o nó mais à direita na árvore ou null se a árvore estiver vazia. // CORREÇÃO PHPDOC
     */
    protected function nodeLast($tree)
    {
        // empty tree
        if (is_null($tree) or is_null($tree->root)) {
            $this->setStatus(102);
            return null;
        }
        $p = $tree->root;
        while ($p->link[1] != null) {
            $p = $p->link[1];
        }
        return $p;
    }

    /**
     * Verifica o objeto de dados passado.
     *
     * @param object  $data   o objeto de dados a ser verificado
     * @param string  $method o método chamador
     *
     * @return boolean true se o objeto de dados for válido, false caso contrário
     *
     * @access protected
     */
    protected function checkData($data, $method)
    {
        if (empty($data) or !is_object($data)) {
            // incorrect data input
            $this->setStatus(107, array('%method' => $method,));
            return false;
        } else {
            // debug log method called
            if ($this->debugMode) {
                $this->setStatus(
                    1, array(
                        '%method' => $method,
                        '%node' => $this->cbc->dump($data), )
                );
            }
            return true;
        }
    }

    /**
     * Define o status interno.
     *
     * @param int     $id     id da mensagem de status
     * @param array|null   $params parâmetros para qualificar a mensagem // CORREÇÃO PHPDOC
     * @param boolean $reset  se verdadeiro, reseta o status interno
     *
     * @return null
     */
    protected function setStatus($id, $params = null, $reset = false)
    {
        // get severity and text
        list($severity, $text) = $this->_message($id);
        // update internal status
        if ($severity <= $this->status['level'] or $reset) {
            $this->status['level'] = $severity;
            $this->status['code'] = $id;
            $this->status['messageParams'] = $params;
        }

        $className = get_class($this);
        $params['%class'] = $className;
        $qText = $this->txt($id, $params);
        if (isset($this->cbc)) {
            if (!$reset) {
                $this->cbc->diagnosticMessage($severity, $id, $text, $params, $qText, $className);
            }
            if ($severity <= RBPPAVL_ERROR) {
                $this->cbc->errorHandler($id, $text, $params, $qText, $className);
            }
        } else {
            if ($severity <= RBPPAVL_ERROR) {
                // CORREÇÃO: Passando o código de erro no construtor da Exception
                throw new \Exception($qText, $id); 
            }
        }
        return null;
    }


    protected function resetStatus()
    {
        $this->setStatus(100, null, true);
        return null;
    }


    public function getStatusLevel()
    {
        return $this->status['level'];
    }

    public function getStatusCode()
    {
        return $this->status['code'];
    }

    public function getStatusMessage()
    {
        return $this->txt($this->status['code'], $this->status['messageParams']);
    }


    public function getMessages()
    {
        return $this->_message();
    }

    public function setMessages($table)
    {
        return $this->_message($table);
    }


    protected function returnBytes($size)
    {
        switch (substr($size, -1)) {
        case 'K':
        case 'k':
            return (int)$size * 1024;
        case 'M':
        case 'm':
            return (int)$size * 1048576;
        case 'G':
        case 'g':
            return (int)$size * 1073741824;
        default:
            return $size;
        }
    }

    /**
     * Retorna a mensagem de status qualificada.
     * * @param mixed $id     id da mensagem de status
     * @param array|null $params parâmetros para qualificar a mensagem // CORREÇÃO PHPDOC
     * @return string mensagem de status qualificada
     * @access protected
     * */
    protected function txt($id, $params = null)
    {
        list($severity, $text) = $this->_message($id);
        if ($params) {
            foreach ($params as $a => $b) {
                // CORREÇÃO: str_replace espera string ou array, mas $a é a chave ('%method') e $b é o valor.
                // O código está invertido para o uso de str_replace com arrays.
                // Usaremos str_replace com string única, o que está correto aqui.
                $text = str_replace($a, $b, $text);
            }
        }
        return $text;
    }

    /**
     * Retorna a tabela de mensagens de status ou uma mensagem específica.
     * * @param mixed $id id da mensagem de status
     * @return mixed tabela de mensagens ou mensagem específica
     * @access protected
     * */
    private function _message($id = null)
    {
        static $t;
        if (!isset($t)) {
            $t = array(
                1 => array(RBPPAVL_DEBUG,'%method \'%node\''),
                2 => array(RBPPAVL_DEBUG,'node \'%node\' exists already'),
                3 => array(RBPPAVL_DEBUG,'inserted *root* node \'%node\'; count: %count'),
                4 => array(RBPPAVL_DEBUG,'inserted node \'%node\' %direction of node \'%parent\'; count: %count'),
                5 => array(RBPPAVL_DEBUG,'height increase in node \'%node\'; new height: %height new balance: %balance'),
                6 => array(RBPPAVL_DEBUG,'self-balancing in node \'%node\'; new balance: %balance'),
                7 => array(RBPPAVL_DEBUG,'%rotationType rotation on node \'%node\' (balance: %balance)'),
                8 => array(RBPPAVL_DEBUG,'node \'%node\' not found'),
                9 => array(RBPPAVL_DEBUG,'node \'%node\' found'),
                10 => array(RBPPAVL_DEBUG,'deleted node \'%node\',%nodeType; replacing node: %replaceBy; count: %count'),
                11 => array(RBPPAVL_DEBUG,'height decrease in node \'%node\'; new height: %height new balance: %balance'),
                12 => array(RBPPAVL_DEBUG,'Wiped %ctr nodes while destroying tree object'),
                13 => array(RBPPAVL_DEBUG,'node \'%node\' not found, closest previous \'%prev\''),
                14 => array(RBPPAVL_DEBUG,'node \'%node\' not found, no closest previous'),
                15 => array(RBPPAVL_DEBUG,'node \'%node\' not found, closest next \'%prev\''),
                16 => array(RBPPAVL_DEBUG,'node \'%node\' not found, no closest next'),
                100 => array(RBPPAVL_INFO, 'OK'),
                101 => array(RBPPAVL_INFO, '%class - Version %version'),
                102 => array(RBPPAVL_WARNING, 'Empty tree.'),
                103 => array(RBPPAVL_WARNING, 'Not enough memory while inserting node \'%node\'.'),
                104 => array(RBPPAVL_ERROR,'Undefined or inaccessible method %class::%method invoked'),
                105 => array(RBPPAVL_ERROR,'Undefined or inaccessible property %class::%property invoked'),
                106 => array(RBPPAVL_ERROR,'No callback class specified when instantiating %class'),
                107 => array(RBPPAVL_WARNING, 'Wrong or undefined data object passed to %class::%method. Only non-null objects accepted.'),
                1000 => array(RBPPAVL_NOTICE, 'Tree validation OK; nodes count: %count'),
                1001 => array(RBPPAVL_ERROR,'Tree validation *failed* on node: \'%node\' (%failureType failure; height: %height balance: %balance)'),
                1002 => array(RBPPAVL_INFO, 'Tree statistics: Balance factor %balance; Node count: %count; Tree height: %height'),
                1003 => array(RBPPAVL_INFO, 'Tree statistics: Inserts: (%ins/%att_ins) Replaces: (%repl/%att_repl) Deletes: (%del/%att_del)'),
                1004 => array(RBPPAVL_INFO, 'Tree statistics: Self-balances: %self; Rotations: %rotations (RR: %rr, RL: %rl, LL: %ll, LR: %lr)'),

                'none' => array(RBPPAVL_TEXT,'*none*'),
                'right' => array(RBPPAVL_TEXT,'right'),
                'left' => array(RBPPAVL_TEXT,'left'),
                'root' => array(RBPPAVL_TEXT,'*root*'),
                'leaf' => array(RBPPAVL_TEXT,'leaf'),
                'height' => array(RBPPAVL_TEXT,'height'),
                'balance' => array(RBPPAVL_TEXT,'balance'),
                'p-noleft'=> array(RBPPAVL_TEXT,'no left subtree'),
                'p-noright' => array(RBPPAVL_TEXT,'no right subtree'),
                'r-noleft'=> array(RBPPAVL_TEXT,'no left child on right subtree \'%node\''),
                'r-left' => array(RBPPAVL_TEXT,'left child on right subtree \'%node\''),
                'internal'=> array(RBPPAVL_TEXT,'internal,'),
            );
        }
    
        if (is_array($id)) {
            foreach ($id as $k => $e) {
                if (isset($t[$k])) {
                    $t[$k] = $e;
                }
            }
            return $t;
        }
        if (is_null($id)) {
            return $t;
        }

        return isset($t[$id]) ? $t[$id] : null;
    }
}