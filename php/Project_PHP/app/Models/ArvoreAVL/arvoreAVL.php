<?php
declare (strict_types=1);
namespace Estruturas;

/**
 * PHP versão 5
 *
 * @category Estruturas
 * @package  Rbppavl
 * @author   mondrake <mondrake@mondrake.org>
 * @license  http://www.gnu.org/licenses/gpl.html GNU GPLv3
 */

class ArvoreAVL extends AVLCommon
{
    /**
     * Ponteiro para o nó raiz da árvore.
     *
     * @type NoArvoreAVL 
     */
    public $root;
    
    /**
     * Desequilíbrio máximo permitido para um nó.
     *
     * @type integer 
     */
    public $balanceFactor;

    /**
     * Número de nós na árvore.
     *
     * @type integer 
     */
    private $_count;

    /**
     * Estatísticas internas da árvore.
     *
     * @type array 
     */
    private $_statistics = array( 'att_ins'  => 0,
                                  'att_repl' => 0,
                                  'att_del'  => 0,
                                  'ins'      => 0,
                                  'repl'     => 0,
                                  'del'      => 0,
                                  'self'     => 0,
                                  'll'       => 0,
                                  'lr'       => 0,
                                  'rr'       => 0,
                                  'rl'       => 0);

    /**
     * Cria uma nova árvore.
     *
     * @param string g $callbackClass classe da interface de callback a ser instanciada
     * @param int     $balanceFactor fator de balanceamento AVL da árvore
     * @param boolean $debugMode     se verdadeiro, produz depuração detalhada (verbose) das operações da árvore
     * @param mixed   $memThreshold  se não for nulo, ativa a verificação de memória disponível, com o mínimo de memória disponível
     * g                               conforme o valor especificado
     *
     * @api
     */
    public function __construct($callbackClass, $balanceFactor = 1, $debugMode = false, $memThreshold = null)
    {
        if (empty($callbackClass)) {
            //nenhuma classe de callback especificada
            $this->setStatus(106);
            return null;
        }
        $this->cbc = new $callbackClass();
        $this->balanceFactor = $balanceFactor;
        $this->root = null;
        $this->_count = 0;
        $this->debugMode = $debugMode;
        if ($memThreshold) {
            $this->memoryLimit = $this->returnBytes(ini_get('memory_limit'));
            $this->memoryThreshold = $this->returnBytes($memThreshold);
        }
        return $this;
    }

    /**
     * Destrói a árvore liberando todos os nós.
     *
     * @access private
     */
    public function __destruct()
    {
        if ($this->root) {
            $ctr = 0;
            $this->root->wipe($ctr);
            // debug diagnostic - wiped nodes
            if ($this->debugMode) {
                $this->setStatus(12, array('%ctr' => $ctr,));
            }
        }
    }

    /**
     * Insere um objeto de dados na estrutura da árvore.
     *
     * Normalmente retorna nulo se for bem-sucedido.
     * Use getStatusLevel() e getStatusCode() para verificar o status de erro interno.
     *
     * @param object $data objeto de dados a ser inserido
     *
     * @api
     *
     * @return object nulo se o nó foi criado ou se ocorreu um erro interno;
     * ponteiro para o objeto de dados existente se o nó já existir
      */
    public function insert($data)
    {
        $this->resetStatus();
        if (!$this->checkData($data, __FUNCTION__)) {
            return null;
        }
        $this->_statistics['att_ins']++;
        $p = $this->_nodeProbe($data);
        if ($p == null || $p->data === $data) {
            $this->_statistics['ins']++;
            return null;
        } else {
            return $p->data;
        }
    }

    /**
     * Substitui um objeto de dados na estrutura da árvore.
     *
     * Normalmente retorna nulo se for bem-sucedido, ou um ponteiro para o objeto
     * de dados substituído. Nota: o objeto de dados substituído NÃO é destruído, o código
     * chamador deve lidar com o objeto de dados duplicado.
     * Use getStatusLevel() e getStatusCode() para verificar o status de erro interno.
     *
     * @param object $data objeto de dados
     *
     * @return object nulo se o nó foi criado ou se ocorreu um erro interno;
     * set          ponteiro para o objeto de dados substituído se o nó já existia
     *
     * @api
      */
    public function replace($data)
    {
        $this->resetStatus();
        if (!$this->checkData($data, __FUNCTION__)) {
            return null;
        }
        $this->_statistics['att_repl']++;
        $p = $this->_nodeProbe($data);
        if ($p == null || $p->data === $data) {
            $this->_statistics['repl']++;
            return null;
        } else {
            $this->_statistics['repl']++;
            $r = $p;
            $p->data = $data;
            return $r->data;
        }
    }

    /**
     * Encontra um objeto de dados na estrutura da árvore.
     *
     * Normalmente retorna um ponteiro para o objeto de dados encontrado,
     * ou nulo se o objeto de dados não for encontrado.
     * Use getStatusLevel() e getStatusCode() para verificar o status de erro interno.
     *
     * @param object  $data objeto de dados a ser encontrado
     * @param integer $mode modo de busca (padrão: RBPPAVL_FIND_EXACT_MATCH)
     *
     * @return object ponteiro para o objeto de dados encontrado ou nulo se o nó não for encontrado
     *
     * @api
      */
    public function find($data, $mode = RBPPAVL_FIND_EXACT_MATCH)
    {
        $this->resetStatus();
        if (!$this->checkData($data, __FUNCTION__)) {
            return null;
        }
        // Árvore vazia
        if (is_null($this->root)) {
            $this->setStatus(102);
            return null;
        }
        $y = $this->root;
        $q = null;
        $dir = null;
        $p = $this->nodeFind($this, $data, $y, $q, $dir, $mode);
        return $p ? $p->data : null;
    }

    /**
     * Remove um objeto de dados da estrutura da árvore.
     *
     * Normalmente retorna um ponteiro para o objeto de dados removido,
     * ou nulo se o objeto de dados não for encontrado.
     * Use getStatusLevel() e getStatusCode() para verificar o status de erro interno.
     *
     * @param object $data objeto de dados a ser removido
     *
     * @return object ponteiro para o objeto de dados removido ou nulo se o nó não for encontrado
     *
     * @api
      */
    public function delete($data)
    {
        $this->resetStatus();
        if (!$this->checkData($data, __FUNCTION__)) {
            return null;
        }
        if ($this->root == null) {
            // árvore vazia
            $this->setStatus(102);
            return null;
        }

        $this->_statistics['att_del']++;
        // <Passo 1: Localiza nó a ser removido>
        $y = $this->root;
        $q = null;
        $dir = null;
        $p = $this->nodeFind($this, $data, $y, $q, $dir, RBPPAVL_FIND_EXACT_MATCH);
        if (!$p) {
            return null;
        }
        $data = $p->data;

        $nodeType = null;
        // <Passo 2: Remove no>
        //$q = $p->parent;
        if (!$q) {
            // debug diagnostic - root node
            if ($this->debugMode) {
                $nodeType = ' ' . $this->txt('root');
            }
            $isRoot = true;
        } else {
            $isRoot = false;
        }
        if ($p->link[0] == null) {
            // sem subárvore esquerda
            if ($p->link[1] == null) {
                // debug diagnostic - leaf
                if ($this->debugMode) {
                    $nodeType .= ' ' . $this->txt('leaf');
                }
                $w = null;
                // leaf
                if ($q) {
                    $q->link[$dir] = null;
                }
            } else {
                // diagnostico de depuração - p com subárvore direita
                if ($this->debugMode) {
                    $nodeType .= ' ' . $this->txt('p-noleft');
                }
                $w = $p->link[1];
                if ($q) {
                    $q->link[$dir] = $p->link[1];
                    $q->link[$dir]->parent = $p->parent;
                }
            }
        } else {
            if ($p->link[1] == null) {
                // diagnostico de depuração - p com subárvore esquerda
                if ($this->debugMode) {
                    $nodeType .= ' ' . $this->txt('p-noright');
                }
                $w = $p->link[0];
                if ($q) {
                    $q->link[$dir] = $p->link[0];
                    $q->link[$dir]->parent = $p->parent;
                }
            } else {
                // diagnostico de depuração - p com subárvores esquerda e direita
                if ($this->debugMode) {
                    $nodeType .= ' ' . $this->txt('internal');
                }
                $r = $p->link[1];
                if ($r->link[0] == null) {
                    // debug diagnostic - r without left subtree
                    if ($this->debugMode) {
                        $nodeType .= ' ' . $this->txt('r-noleft', array('%node' => $this->cbc->dump($r->data),));
                    }
                    $r->link[0] = $p->link[0];
                    $w = $r;
                    if ($q) {
                        $q->link[$dir] = $r;
                    }
                    $r->parent = $p->parent;
                    if ($r->link[0]) {
                        $r->link[0]->parent = $r;
                    }
                    $r->height = $p->height;
                    $q = $r;
                    $dir = 1;
                } else {
                    // debug diagnostic - r with left subtree
                    if ($this->debugMode) {
                        $nodeType .= ' ' . $this->txt('r-left', array('%node' => $this->cbc->dump($r->data),));
                    }
                    $s = $r->link[0];
                    while ($s->link[0] != null) {
                        $s = $s->link[0];
                    }
                    $r = $s->parent;
                    $r->link[0] = $s->link[1];
                    $s->link[0] = $p->link[0];
                    $s->link[1] = $p->link[1];
                    $w = $s;
                    if ($q) {
                        $q->link[$dir] = $s;
                    }
                    if ($s->link[1]) {
                        $s->link[1]->parent = $s;
                    }
                    $s->link[0]->parent = $s;
                    $s->parent = $p->parent;
                    if ($r->link[0]) {
                        $r->link[0]->parent = $r;
                    }
                    $s->height = $p->height;
                    $q = $r;
                    $dir = 0;
                }
            }
        }
        if ($isRoot) {
            $this->root = $w;
            if ($this->root) {
                $this->root->parent = null;
            }
        }

        $this->_count--;
        $this->_statistics['del']++;
        // debug diagnostic    - deleted
        if ($this->debugMode) {
            $this->setStatus(
                10, array('%nodeType'     => $nodeType,
                          '%node'         => $this->cbc->dump($p->data),
                          '%replaceBy'    => ($w ? "'" . $this->cbc->dump($w->data) . "'" : $this->txt('none')),
                          '%count'        => $this->_count,)
            );
        }
        unset($p);

        // <Passo 3: Atualiza alturas>
        while ($q !== null) {
            $y = $q;
            $q = $y->parent;
            $h = $y->height;
            if (abs($y->balance()) > $this->balanceFactor) {
                $this->_rotationRebalance($y);
                if ($h == $y->height) {
                    break;
                }
            } else {
                $y->heightReset();
                if ($h == $y->height) {
                    // debug diagnostic    - self balancing
                    $this->_statistics['self']++;
                    if ($this->debugMode) {
                        $this->setStatus(
                            6, array('%node'         => $this->cbc->dump($y->data),
                                     '%balance'      => $y->balance(),)
                        );
                    }
                    break;
                } else {
                    // debug diagnostic    - decrease height
                    if ($this->debugMode) {
                        $this->setStatus(
                            11, array('%node'         => $this->cbc->dump($y->data),
                                      '%height'       => $y->height,
                                      '%balance'      => $y->balance(),)
                        );
                    }
                }
            }
            if ($q) {
                $dir = ($q->link[0] !== $y) ? 1 : 0;
            } else {
                $dir = 0;
            }
        }
        return $data;
    }

    /**
     * Retorna o número de nós na árvore.
     *
     * @return integer número de nós na árvore
     *
     * @api
      */
    public function getCount()
    {
        $this->resetStatus();
        return $this->_count;
    }

    /**
     * Localiza o ponto de inserção para um novo nó e insere o nó na árvore.
     *
     * @param object $data objeto de dados a ser inserido
     *
     * @return object ponteiro para o nó inserido ou para o nó existente
     *
     * @access private
      */
    private function _nodeProbe($data)
    {
        // <Passo 1: Search node tree for insertion point>
        $y = $this->root;
        $q = null;
        $dir = null;
        $p = $this->nodeFind($this, $data, $y, $q, $dir, RBPPAVL_FIND_EXACT_MATCH);
        if ($p) {
            // debug - node exists already
            if ($this->debugMode) {
                $this->setStatus(2, array('%node'   => $this->cbc->dump($data), ));
            }
            return $p;
        }

        // <Passo 2: Insert new node at located position>
        if ($this->memoryLimit) {
            if (memory_get_usage() >= $this->memoryLimit - $this->memoryThreshold) {
                $this->setStatus(103, array('%node'   => $this->cbc->dump($data), ));
                return null;
            }
        }
        $n = new NoArvoreAVL($this, $data, $q);

        $this->_count++;
        if ($q) {
            $q->link[$dir] = $n;
            if ($this->debugMode) {
                $this->setStatus(
                    4, array('%node'         => $this->cbc->dump($n->data),
                             '%direction'    => ($dir == 0 ? $this->txt('left') : $this->txt('right')),
                             '%parent'       => $this->cbc->dump($q->data),
                             '%count'        => $this->_count,)
                );
            }
        } else {
            $this->root = $n;

            if ($this->debugMode) {
                $this->setStatus(
                    3, array('%node'         => $this->cbc->dump($n->data),
                             '%count'        => $this->_count,)
                );
            }
            return $n;
        }

        // <Passo 3: Atualiza alturas>
        // Atualiza alturas a partir do nó pai do nó inserido
        // até a raiz ou até que o fator de balanceamento indique
        for ($p = $n; $p !== $y; $p = $q) {
            $q = $p->parent;
            $dir = ($q->link[0] === $p) ? 0 : 1;
            if (   ($dir == 0 and $q->balance() >= 0)
                or ($dir == 1 and $q->balance() <= 0)
            ) {
                $this->_statistics['self']++;
        
                if ($this->debugMode) {
                    $this->setStatus(
                        6, array('%node'       => $this->cbc->dump($q->data),
                                 '%balance'    => $q->balance(),)
                    );
                }
                return $n;
            } else {
                $q->height++;
            
                if ($this->debugMode) {
                    $this->setStatus(
                        5, array('%node'         => $this->cbc->dump($q->data),
                                 '%height'       => $q->height,
                                 '%balance'      => $q->balance(),)
                    );
                }
            }
        }

        //  <Passo 4: Realiza rotações se necessário>
        if (abs($y->balance()) > $this->balanceFactor) {
            $this->_rotationRebalance($y);
        }

        return $n;
    }

    /**
      * Performs rotation on the node to restore AVL compliance.
      *
      * @param object $y node to be rotated
      *
      * @return null
      */
    private function _rotationRebalance($y)
    {
        if ($y->balance() < 0) {
            $x = $y->link[0];
            if ($x->balance() <= 0) {                
                $this->_statistics['ll']++;
                if ($this->debugMode) {
                    $this->setStatus(
                        7, array('%node'         => $this->cbc->dump($y->data),
                                 '%rotationType' => 'LL',
                                 '%balance'      => $y->balance(),)
                    );
                }
                // rotação
                $w = $x;
                $y->link[0] = $x->link[1];
                $x->link[1] = $y;
                // reinicia ponteiros para pais
                $x->parent = $y->parent;
                $y->parent = $x;
                if ($y->link[0] != null) {
                    $y->link[0]->parent = $y;
                }
 
                $y->heightReset();
                $x->heightReset();
            } else {                          

                $this->_statistics['lr']++;
                if ($this->debugMode) {
                    $this->setStatus(
                        7, array('%node'         => $this->cbc->dump($y->data),
                                 '%rotationType' => 'LR',
                                 '%balance'      => $y->balance(),)
                    );
                }
                // rotação
                $w = $x->link[1];
                $x->link[1] = $w->link[0];
                $w->link[0] = $x;
                $y->link[0] = $w->link[1];
                $w->link[1] = $y;

                $w->parent = $y->parent;
                $x->parent = $y->parent = $w;
                if ($x->link[1] != null) {
                    $x->link[1]->parent = $x;
                }
                if ($y->link[0] != null) {
                    $y->link[0]->parent = $y;
                }
   
                $y->heightReset();
                $x->heightReset();
                $w->heightReset();
            }
        } else {       
            $x = $y->link[1];
            if ($x->balance() >= 0) {    

                $this->_statistics['rr']++;
                if ($this->debugMode) {
                    $this->setStatus(
                        7, array('%node'         => $this->cbc->dump($y->data),
                                 '%rotationType' => 'RR',
                                 '%balance'      => $y->balance(),)
                    );
                }

                $w = $x;
                $y->link[1] = $x->link[0];
                $x->link[0] = $y;
    
                $x->parent = $y->parent;
                $y->parent = $x;
                if ($y->link[1] != null) {
                    $y->link[1]->parent = $y;
                }
 
                $y->heightReset();
                $x->heightReset();
            } else {              

                $this->_statistics['rl']++;
                if ($this->debugMode) {
                    $this->setStatus(
                        7, array('%node'         => $this->cbc->dump($y->data),
                                 '%rotationType' => 'RL',
                                 '%balance'      => $y->balance(),)
                    );
                }

                $w = $x->link[0];
                $x->link[0] = $w->link[1];
                $w->link[1] = $x;
                $y->link[1] = $w->link[0];
                $w->link[0] = $y;
           
                $w->parent = $y->parent;
                $x->parent = $y->parent = $w;
                if ($x->link[0] != null) {
                    $x->link[0]->parent = $x;
                }
                if ($y->link[1] != null) {
                    $y->link[1]->parent = $y;
                }
       
                $y->heightReset();
                $x->heightReset();
                $w->heightReset();
            }
        }
        if ($w->parent) {
            $w->parent->link[($y !== $w->parent->link[0]) ? 1 : 0] = $w;
        } else {
            $this->root = $w;
        }
    }

    /**
     * Valida a estrutura da árvore.
     *
     * Esta função deve ser usada apenas para fins de depuração.
     *
     * A validação percorre todos os nós da árvore, verificando se os
     * fatores de balanceamento e alturas estão corretos.
     *
     * @param boolean $setStatusOnSuccess se verdadeiro, define o status interno em caso de sucesso
     *
     * @return object nulo se a árvore for válida; ponteiro para o objeto de dados do nó que falhou na validação
     *
     * @api
     */
    public function debugValidate($setStatusOnSuccess = false)
    {
        $this->resetStatus();
        if ($this->root == null) {
            // Empty tree
            $this->setStatus(102);
            return null;
        }
        $status = 0;
        $failingNode = $this->root->debugNodeValidate($this->balanceFactor, $status);
        if ($failingNode) {
            // validation failure
            $this->setStatus(
                1001, array('%node'          => $this->cbc->dump($failingNode->data),
                            '%failureType'   => ($status == RBPPAVL_VALIDATION_HEIGHT_FAILURE ?
                                                $this->txt('height') :
                                                $this->txt('balance')),
                            '%height'        => $failingNode->height,
                            '%balance'       => $failingNode->balance(),)
            );
            return $failingNode->data;
        } else {
            // validation ok
            if ($setStatusOnSuccess) {
                $this->setStatus(1000, array('%count' => $this->_count,));
            }
            return null;
        }
    }
    /**
     * Gera uma representação em array da árvore em ordem de nível.
     *
     * Esta função deve ser usada apenas para fins de depuração.
     *
     * @param int $maxLevel nível máximo a ser incluído na representação
     *
     * @return array representação em array da árvore em ordem de nível
     *
     * @api
     */
    public function debugLevelOrderToArray($maxLevel = 5)
    {
        $arr = array();
        $this->_debugNodeToArray($this->root, 0, "0", $maxLevel, $arr);
        return $arr;
    }

    /**
     * Insere um nó na representação em array da árvore em ordem de nível.
     *
     * Essa função deve ser usada apenas para fins de depuração.
     *
     * Essea é uma função recursiva que percorre a árvore em pré-ordem,
     *
     * @param object $node     o nó a ser inserido na representação em array
     * @param int    $level    o nível do nó na árvore
     * @param string $pos      a posição do nó no nível (em binário)
     * @param int    $maxLevel nível máximo a ser incluído na representação
     * @param array  &$arr  referência para a representação em array da árvore
     *
     * @return null
     */
    private function _debugNodeToArray($node, $level, $pos, $maxLevel, &$arr)
    {
        if ($node == null or $level > $maxLevel) {
            return;
        }
        $posDec = bindec($pos);
        $xa = $node->data;
        $bal = $node->balance();
        $h = $node->height;
        $entry = array( $node->data,
                        $node->height,
                        $node->balance() );
        $arr[$level][$posDec] = $entry;
        if ($node->link[0] != null or $node->link[1] != null) {
            $pos .= "0";
            $this->_debugNodeToArray($node->link[0], $level + 1, $pos, $maxLevel, $arr);
            if ($node->link[1] != null) {
                $pos = substr($pos, 0, strlen($pos) - 1);
                $pos .= "1";
                $this->_debugNodeToArray($node->link[1], $level + 1, $pos, $maxLevel, $arr);
            }
        }
    }

    /**
     * Retorna estatísticas internas da árvore.
     *
     * @param string  $stat      estatística específica a ser retornada (padrão: nulo, retorna todas as estatísticas)
     * @param boolean $setStatus se verdadeiro, define o status interno com as estatísticas
     *
     * @return mixed array de todas as estatísticas se $stat for nulo;
     * valor específico da estatística se $stat for fornecido;
     * nulo se a estatística específica não existir
     *
     * @api
      */
    public function getStatistics($stat = null, $setStatus = false)
    {
        $this->_statistics['balance_factor'] =  $this->balanceFactor;
        $this->_statistics['height'] = ($this->root ? $this->root->height : -1);
        $this->_statistics['count'] = $this->_count;
        if ($setStatus) {
            // diagnose statistics
            $this->setStatus(
                1002, array('%balance'       => $this->_statistics['balance_factor'],
                            '%count'         => $this->_statistics['count'],
                            '%height'        => $this->_statistics['height'],)
            );
            $this->setStatus(
                1003, array('%ins'           => $this->_statistics['ins'],
                            '%att_ins'       => $this->_statistics['att_ins'],
                            '%repl'          => $this->_statistics['repl'],
                            '%att_repl'      => $this->_statistics['att_repl'],
                            '%del'           => $this->_statistics['del'],
                            '%att_del'       => $this->_statistics['att_del'],)
            );
            $this->setStatus(
                1004, array('%self'          => $this->_statistics['self'],
                            '%rotations'     => $this->_statistics['ll'] +
                                                $this->_statistics['lr'] +
                                                $this->_statistics['rr'] +
                                                $this->_statistics['rl'],
                            '%ll'            => $this->_statistics['ll'],
                            '%lr'            => $this->_statistics['lr'],
                            '%rr'            => $this->_statistics['rr'],
                            '%rl'            => $this->_statistics['rl'],)
            );
        }
        if (!$stat) {
            return $this->_statistics;
        } else {
            return (isset($this->_statistics[$stat]) ? $this->_statistics[$stat] : null);
        }
    }
}