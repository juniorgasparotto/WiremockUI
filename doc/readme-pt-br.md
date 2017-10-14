# Wiremock UI

É um software desenvolvido em ".NET Framework 4.5" (paradigma: "Windows Forms") que tenta representar de forma gráfica os recursos `standalone` do wiremock. Por ser uma interface gráfica, alguns recursos foram potencializados:

* Facilidade para criar e executar um servidor Wiremock
* Criar e gerenciar mais de um servidor Wiremock em apenas um lugar
* Criar múltiplos cenários para a mesma API ou Site com a intenção de alterna-los conforme a necessidade de uso.
* Visualização dos mapas com sua respectiva resposta em forma de `TreeView`
* Gerenciar os mapas do Wiremock com as opções: criar, editar, remover, duplicar, desabilitar e visualização em forma de JSON
* Editor avançado de texto com os seguintes suportes:
  * Highlight para as linguages: `JSON`, `XML`, `HTML`, `JavaScript`, `C#`, `PHP`, `LUA`, `VB.NET`
  * Formatores de XML e JSON
  * Quebra de linha automática
  * Busca e substituição
  * Recurso de "Ir para a linha"
* O gerenciamento dos mapas (dentro da ferramenta) dispensa o reinicio do servidor.
* Logs em forma de texto e tabela com as opções:
  * Análise de tempo
  * Re-executar a requisição com a ferramenta interna `Web Request`
  * Comparar requisições que não deram correspondência com qualquer mapa da `TreeView`.
* Ferramentas internas:
  * `Web Request`: É um simples executador de chamadas HTTP que pode ajudar a depurar
  * `Text Compare`: É um simples comparador de texto
  * `Text editor`: Editor de texto com as opções de formatação para JSON ou XML
  * `JSON Viewer`: Visualizador de JSON com opções de formatação e visualização em forma de árvore

## Instalação

Ele não precisa de instalação, basta apenas:

1. Fazer o download do .zip
2. Descompactar o .zip em qualquer local
2. Abrir o arquivo `Wiremock.exe`

```
https://github.com/juniorgasparotto/WiremockUI/blob/master/download/release-2.0.0.zip
```

**Requisitos - Windows**

1. Instale o ".NET Framework 4.5"
  * chocolatey: `choco install dotnet4.5`
  * microsoft: https://www.microsoft.com/en-us/download/details.aspx?id=30653
2. Instale o "Java"
  * chocolatey: `choco install jre8`
  * oracle: http://www.oracle.com/technetwork/pt/indexes/downloads/index.html

**Requisitos - Linux**

Utilize o projeto "Mono.Forms" para executar no linux, contudo, não é garantido que todas as funcionalidades funcionem da mesma maneira que ocorre no Windows.

# Wiremock - Overview

É um projeto feito em java que simula um serviço web. Tecnicamente ele foi projetado para trabalhar de de duas formas:
  * **Modo Standalone**: É quando ele é executado no prompt de comando com a finalidade de criar servidores web armazenando os request e responses em forma de arquivos. Ele pode trabalhar com 3 tipos de servidores.
    * Executar como servidor de mock
    * Executar como proxy, mas gravando os dados que são trafegados (útil para a carga inicial)
    * Executar apenas como proxy
  * **Framework de testes**: Está fora do nosso escopo, mas ele pode ser usado como framework de mock de API em forma de código. Em .NET temos o `mock4net` que tem como inspiração o Wiremock.

Para mais informações, acesse o site oficial da ferramenta: http://wiremock.org/

## Tipos de servidores - Executar como servidor de mock

Dentro do contexto de testes ele é útil para mocar APIs ou qualquer coisa sobre o protocolo HTTP. O servidor utiliza, basicamente, de duas pastas para trabalhar: 
  * **mappings**: Essa pasta contém os arquivos `.json`, onde cada arquivo representa uma rota com sua respectiva resposta. Existe uma porção de configurações dentro de cada mapa, todas estão disponíveis na documentação do wiremock.
  * **__files**: Nessa pasta ficam os arquivos de resposta que são configurados no mapa.

**Exemplo mapa - GET**

Esse mapa cria uma rota que ficará ouvindo a rota `http://[SERVER]:5500/user/all` utilizando o verbo `GET`. Quando uma requisição estiver dentro dessas regras o conteúdo do arquivo `response.txt` será retornado:

```json
{
  "request": {
    "url": "/user/all",
    "method": "GET"
  },
  "response": {
    "status": 200,
    "bodyFileName": "response.txt",
    "headers": {
      "Content-Type": "application/json"
    }
  }
}
```

**Exemplo mapa - POST**

Esse mapa cria uma rota que ficará ouvindo a rota `http://[SERVER]:5500/user/insert”all` utilizando o verbo `POST` e quando o corpo da requisição for igual á `{\"Name\":\"User3\",\"Age\":100}`. Quando uma requisição estiver dentro dessas regras o conteúdo do arquivo `response.txt` será retornado:

```json
{
  "request": {
    "url": "/user/insert",
    "method": "POST",
    "bodyPatterns" : [
      {
        "equalToJson" : "{\"Name\":\"User3\",\"Age\":100}"
      }
    ]
  },
  "response": {
    "status": 200,
    "bodyFileName": "response.txt",
    "headers": {
      "Content-Type": "application/json"
    }
  }
}
```

**Resposta:** `response.txt`:

A resposta é sempre "crua", sem nenhum encapsulamento. Como nos mapas anteriores, vimos que o arquivo de resposta é um `application/json`, então esse arquivo terá o conteúdo JSON, se fosse uma imagem, esse arquivo de resposta teria a extensão da imagem, exemplo: `response.jpg` e seu conteúdo seria um binário.

{
  "key1": "value"
}

**Testando:**

* Subir o servidor
  * `java -jar "D:\wm\wiremock-standalone-2.8.0.jar" --port 5500 --root-dir "D:\wm\server1" --verbose`
* Cenário de GET:
  * **Url**: `http://localhost:5500/user/all`
  * **Method**: `GET`
* Cenário de POST:
*   **Url**: `http://localhost:5500/user/insert`
  * **Method**: `POST`
  * **Body**: `{ "Name": "User3", "Age": 100}`

## Tipos de servidores - Executar como proxy, mas gravando os dados que são trafegados

É muito útil para dar a primeira carga de arquivos de mapas e respostas, depois disso, você pode editar os arquivos gerados podendo criar diversos cenários. Para usar os arquivos gerados é preciso mudar a forma de execução para: Servidor de arquivos estáticos.

**Executando:**

* Subir o servidor em modo de gravação:
  * `java -jar "D:\wm\wiremock-standalone-2.8.0.jar" --port 5502 --proxy-all "https://www.w3schools.com/" --record-mappings --root-dir "D:\wm\server2" --verbose --match-headers Content-Type`
* Abrir a URL no browser: `http://localhost:5502`
* Parar o servidor
* Editar qualquer arquivo
* Reiniciar o servidor para utilizar os arquivos salvos
  * `java -jar "D:\wm\wiremock-standalone-2.8.0.jar" --port 5502 --root-dir "D:\wm\server2" --verbose`
* Limpar o cache do browser
* Executar novamente: `http://localhost:5502`

## Tipos de servidores - Executar apenas como proxy

Dentro do nosso contexto de testes, ele pode ser útil quando precisamos utilizar o serviço original sem precisar alterar a URL no cliente.

**Executando:**

  * Subir o servidor em modo de proxy (ignorando os mapas salvos):
    * `java -jar "D:\wm\wiremock-standalone-2.8.0.jar" --port 5502 --proxy-all "https://www.w3schools.com/" --verbose`
  * Abrir a URL no browser: `http://localhost:5502`

# WiremockUI - Tutorial

##  Como funciona?

O .JAR do Wiremock não é executado usando processos, o .JAR da última versão do Wiremock foi convertido em .NET usando a ferramenta "IKVM". Com isso, foi possível potencializar o uso da ferramenta, tendo acesso direto às principais classes.

* Ele utiliza WindowsForms como paradigma, então é preciso ter o .NET Framework 4.5 instalado.
* Todos os arquivos salvos serão salvos em uma pasta chamada `.app` que fica na raiz de onde está o `.exe`.

## Criando um servidor de mock

Ao criar um novo servidor, um cenário também será criado, você pode ter mais de um cenário para um mesmo servidor, alternando-os quando necessário usando a opção `Set as Default` que existe nas opções dos cenários.

* Clique com o lado direito do mouse sobre o item `Servers` e clique em `Add Server`
* A porta será gerada automaticamente, mas pode ser alterada a qualquer momento.
* Você não precisa preencher o campo `Target URL`, pois a ideia é criar um servidor do zero. Se você quiser gerar uma massa inicial com uma API existente, utilize esse campo e execute o servidor em modo de gravação `Start and Record`.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-server.png" />

**Configurações avançadas**

* Na criação do servidor, é possível configurar a execução do wiremock. Clique na aba `Advanced`
* Para obter mais informações sobre cada uma: http://wiremock.org/docs/running-standalone/

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-server-advanced.png" />

**Menu de Opções**

* `Add scenario`: Adiciona um novo cenário, apenas um cenário pode estar ativo
* `Start`: Inicia um servidor usando os dados físicos das pastas `mappings` e `__files`
* `Start (Only Proxy)`: Inicia um servidor apenas como proxy ignorando os arquivos salvos se existerem.
* `Start and Record`: Inicia um servidor como proxy em modo de gravação
* `Restart`: Reinicia o servidor mantendo o tipo de execução que foi iniciado
* `Stop`: Para o servidor
* `Open Server folder`: Abre a pasta onde estão todos os cenários
* `Open Targer URL in browser`: Abre a URL original no Browser
* `Open Server URL in browser`: Abre a URL do servidor wiremock no Browser
* `Duplicate`: Duplica todo o servidor, incluindo os cenários e todos os arquivos
* `Edit`: Edita os dados do servidor
* `Remove`: Remove o servidor

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-menu.png" />


## Criando um novo cenário

* Clique com o lado direito do mouse sobre o servidor desejado e clique em `Add scenario`
* Você pode ter mais de um cenário para um mesmo servidor, isso é útil para situações onde você não quer perder tempo de criar correspondências avançadas usando as opções de match do wiremock.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-scenario.png" />


**Menu de Opções**

* `Add map`: Adiciona um novo arquivo de mapa, esse mapa será o básico de um mapa do wiremock.
* `Set as Default`: Indica que os arquivos deste cenário serão utilizados quando o servidor for iniciado.
* `Open scenario folder`: Abre a pasta que contém os arquivos desse cenário
* `Duplicate`: Duplica esse cenário e todos seus arquivos
* `Edit`: Edita o cenário
* `Remove`: Remove o cenário
* `Show URL`: Quando ativo, exibe a URL dos mapas na árvore
* `Show Name`: Quando ativo, exibe o nome do arquivo na árvore

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/scenario-menu.png" />

# Criando um novo mapa

* Para adicionar um novo mapa, clique com o lado direto do mouse sobre o item `Scenario1`.
* Será criado um arquivo de mapa com o básico das configurações do Wiremock. Para obter mais informações sobre como configurar um mapa acesse: http://wiremock.org/docs/request-matching/

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-map.png" />

**Menu de Opções do mapa**

* `Rename`: Renomeia o arquivo, quando isso ocorre, o arquivo de resposta também será renomeado e ficará com o mesmo nome, porém mantendo a sua extensão original.
* `Duplicate`: Duplica esse mapa
* `Remove`: Remove o mapa
* `Enable`: Quando desativado, esse mapa será ignorado
* `View in Web Request`: Abre o mapa no `WebRequest` permitindo executa-lo.
* `View in explorer`: Abre o gerenciador de arquivos do sistema operacional com o arquivo selecionado.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/map-menu.png" />

**Arquivo de Mapa no editor**

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/map-editor.png" />

**Arquivo de Mapa no JSON Viewer**

* Ao abrir um arquivo de mapa ou qualquer outro arquivo JSON, será possível ter uma visão melhorada do JSON clicando na aba `JSON Viewer`. 
* Clique com o lado direito do mouse sobre o atributo desejado para obter mais opções: 
  * `View text editor`: Visualiza o conteúdo em uma nova janela
  * `View as Json`: Visualiza o conteúdo em uma nova janela JSON Viewer
  * `Expand all`: Abre todos os filhos do nó
  * `Close all`: Fecha todos os filhos do nó
* Esse visualizador também está disponível em `Tools -> JSON Viewer`

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/map-json.png" />

**Menu de Opções da resposta**

* `View in explorer`: Abre o gerenciador de arquivos do sistema operacional com o arquivo selecionado.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/response-menu.png" />

**Arquivo de resposta no editor**

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/response-editor.png" />

## Edição de texto

Para abrir as opções de edição de texto, clique com o lado direito do mouse sobre o campo desejado. As seguintes opções serão exibidas:

* `Undo`: Desfaz uma alteração
* `Redo`: Refaz uma alteração
* `Edit`
  * `Word Wrap`: Liga ou desliga a quebra de linha automática
  * `Select all`: Seleciona todo o texto
  * `Copy`: Copia o texto selecionado
  * `Cut`: Recorta o texto selecionado
  * `Paste`: Copia o que estiver na área de transferência para o campo de texto
  * `Remove`: Remove o texto selecionado
* `Find`:  Localiza um texto. Abre uma janela para busca ou troca de texto.
* `Json`: Formata o texto considerando que o mesmo seja um JSON
  * `Format`: Deixa o JSON em uma forma mais legível
  * `Escape`: "Escapa" o JSON para ele poder ser usado como valor de outro JSON
  * `Unescape`: Volta o JSON para o estado normal quando ele esta "escapado"
  * `Minify`: Remove os espaços desnecessários do JSON
  * `Edit value`: Essa opção só aparece quando um texto estiver selecionado, ele é usada para editar (em outra janela) um valor de um atributo que contenha um "JSON escapado".
* `XML`: Tem as mesmas opções do JSON, porém para o formato XML
* `Languages`: Altera o `Highlight` do arquivo que está sendo editado de acordo com a linguagem selecionada.
* Um novo editor de texto também está disponível em `Tools -> Text Editor`

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/editor-edit-value.png" />

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/editor-edit-value-window.png" />

## Iniciando o servidor

* Clique com o botão direito do mouse sobre o servidor desejado 
* Clique em `Start`

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/start-server.png" />

* Ao iniciar o servidor, uma janela contendo os logs em forma de texto e tabela serão exibidos. 
* No log de texto é exibido um texto contento (em verde) da linha de comando que seria equivalente ao usar via console.
* Umas das vantagens de usar o WiremockUI é que você pode editar os arquivos de mapa e suas respostas sem a necessidade de reiniciar o serviço.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-started.png" />

**Abrir o servidor no browser**

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/open-server-in-browser.png" />

## Logs/Debugging

* O log em forma de grid é mais completo que o log em forma de texto, além de mostrar as chamadas de uma forma mais fácil, ainda existe algumas opções de debug, como:
  * Re-executar as chamadas
  * Comparar chamadas com mapas existentes.
* Essas opções de debug só funcionam com o tipo `LISTENER`, os tipos `NET.IN` e `NET.OUT` são chamadas de baixo nível feitas pelo wiremock e que também são exibidas aqui.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid.png" />

**Re-executar chamadas**

* Ao clicar com o botão direito sobre o tipo `LISTENER`, clique na opção `Open in WebRequest`.
* A ferramenta permite editar os dados de request e exibe na barra de status o código de retorno com o tempo que a chamada demorou. Essa ferramenta também está disponível pelo menu `Tools -> Web Request`

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid-webrequest.png" />

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid-webrequest-window.png" />

**Comparar chamadas com mapas existentes**

* Ao clicar com o botão direito sobre o tipo `LISTENER`, clique na opção `Compare`.
* Na lado esquerdo será aberto o conteúdo da chamada do log. Selecione o arquivo que deseja comparar usando o botão com a seta para baixo no lado direito do comparador.
* Essa ferramenta também está disponível no menu `Tools -> Text Compare`

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid-compare.png" />

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid-compare-window.png" />

**Verificar o tempo de uma chamada**

* Para ter uma precisão maior sobre o tempo da chamada, compare os tempos do tipo `NET.IN` (coluna `RequestTime`) com o tipo `NET.OUT` (Coluna `ResponseTime`) da URL desejada. Infelizmente, não existe uma maneira mais precisa para obter essa informação, seria um desejo para as próximas versões do Wiremock.
* O tipo `NET.OUT` não retorna a URL no campo esperado, sendo assim, a localização dessa linha deve ser manual, ou seja, limpe os logs e faça a chamada apenas da URL que deseja medir o tempo.
* Essa opção só faz sentido quando esta sendo executado como proxy, não faz sentido medir o tempo de um servidor mocado.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid-time.png" />

## Criando um servidor para o modo de gravação

Adicione um novo servidor preenchendo a opção `Target URL`, assim as opções de execução de gravação e proxy serão exibidas no menu do servidor.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-server-record.png" />

**Iniciando o servidor**

Ao executar em modo de gravação, você verá no log as opções `match-headers`, isso significa que ao gerar o mapa da rota, os headers `Content-Type` e `SOAPAction` devem fazer parte do filtro se existirem, ou seja, a `URL`, o `BODY` e esses `headers` devem ser iguais para obter a resposta.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/start-server-record.png" />

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-started-record.png" />

* Após a gravação, os mapas serão exibidos na árvore e suas respostas estarão disponíveis ao clicar no `+` de cada mapa.
* Para usar os arquivos salvos, pare o servidor com a opção `Stop` e inicie com a opção `Start`.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-recorded-files.png" />

## Executar apenas como proxy

Adicione um novo servidor preenchendo a opção `Target URL`, assim as opções de execução de gravação e proxy serão exibidas no menu do servidor.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-server-proxy.png" />

**Iniciando o servidor**

* Clique com o lado direito do mouse sobre o servidor
* Clique na opção `Start (Only Proxy)`

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-proxy-menu.png" />

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-started-proxy.png" />

## Menu

* `File` 
  * `Refresh`: Atualiza a tela para voltar ao estado de inicio.
  * `Open files folder`: Abre a pasta onde estão todos os arquivos.
  * `Find in Files`: Abre a ferramenta de pesquisa em arquivos.
  * `Languages`: Suporta duas línguas: inglês e português
  * `Quit`: Sai da aplicação
* `Execute`: 
  * `Add Server`: Adiciona um novo servidor
  * `Start All`: Inicia todos os servidores
  * `Start and record all`: Inicia todos os servidores em modo de gravação
  * `Stop all`: Para todos os servidores
* `Tools`: 
  * `Web Request`: Abre a ferramenta `WebRequest` que faz requisições web. . Essa ferramenta é muito simples, diversas configurações de chamadas HTTP não foram implementadas, ela foi criada para re-executar requisições ou mapas.
  * `Text Compare`: Abre a ferramenta que compara texto. Essa ferramenta é muito simples, é apenas para ajudar na comparação básica de requisições com mapas que não deram match.
  * `Text Editor`: Abre a ferramenta de edição de texto. A ferramenta é muito simples e foi desenvolvida para ajudar a formatar algum valor no formato JSON ou XML.
  * `JSON Viewer`: Abre a ferramenta JSON Viewer que ajuda na visualização do JSON exibindo-o em forma de TreeView
* `About`: Abre a tela de sobre.

<img alt="Inglês" src="https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/menus.png" />

# Como contribuir

No momento, não vou adicionar novas features devido a falta de tempo, ficarei a disposição apenas para bugs e pequenas melhorias. Caso queiram contribuir com novas ideias ou correções, basta apenas entrar em contato ou acessar o board do projeto.
  1. Vejo que o principal ponto de melhoria seria no formulário "FormMaster" que está com muitas linhas e pouco componentizado.
  2. Um outro ponto que considero importante é melhorar a camada de persistência, no momento, as chamadas não estão centralizadas deixando a situação perigosa para futuras melhorias e isso agrava por ser tratar de um banco de dados em forma de um único arquivo.
2. Links importantes para o projeto:
  * **IKVM**: Ferramenta que converte o Wiremock em Java para .NET.
    * https://www.ikvm.net/
  **PocDatabase**: Framework para facilitar a persistência dos dados
    * https://github.com/juniorgasparotto/PocDatabase
  **Board**: 