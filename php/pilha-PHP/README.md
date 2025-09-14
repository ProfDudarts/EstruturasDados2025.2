# Pilha PHP

Este projeto implementa uma estrutura de dados de pilha em PHP. A pilha é uma estrutura de dados que segue o princípio LIFO (Last In, First Out), onde o último elemento inserido é o primeiro a ser removido.

## Estrutura do Projeto

O projeto contém os seguintes arquivos:

- **src/No/No.php**: Define a classe `No`, que representa um nó na pilha. A classe possui propriedades para armazenar o valor do nó e um ponteiro para o próximo nó, além de métodos para inicializar e imprimir o valor do nó.

- **src/Pilha/Pilha.php**: Define a classe `Pilha`, que implementa a estrutura de dados de pilha. A classe possui métodos para inserir (push), remover (pop), listar (listar) e verificar se a pilha está vazia. O método listar exibe todos os elementos da pilha.

- **src/index.php**: Ponto de entrada do aplicativo. Cria uma instância da classe `Pilha`, insere alguns nós, remove um nó, imprime o nó removido e lista o estado atual da pilha após cada operação.

- **composer.json**: Arquivo de configuração para o Composer, que lista as dependências do projeto e as configurações necessárias para autoloading.

## Instalação

1. Clone o repositório:
   ```
   git clone <URL do repositório>
   ```

2. Navegue até o diretório do projeto:
   ```
   cd pilha-php
   ```

3. Instale as dependências usando o Composer:
   ```
   composer install
   ```

## Execução

Para executar o projeto, utilize o seguinte comando:
```
php src/index.php
```

Isso irá executar o script que demonstra a funcionalidade da pilha, incluindo a inserção e remoção de nós, bem como a listagem do estado atual da pilha.