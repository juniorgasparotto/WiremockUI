# Wiremock

O WireMock é um poderoso simulador de APIs desenvolvido em Java. Com ele é possível:

1. Criar UnitTests de API usando a linguagem Java (está fora do escopo desse projeto)
2. Criar uma API (ou SITE) completa que retorne dados fixos (fakes) usando padrões de correspondência de URL ou de conteúdo (body). 
  * Basta baixar a última versão do .JAR no site do Wiremock e executa-lo indicando a pasta onde estão todos os mapas com suas rotas.
    * `java -jar "[path-to-jar].jar" --port [number] --root-dir "[folder-with-maps]" --verbose`
3. Criação de um proxy que consegue gravar as requisições e suas respectivas respostas para que sejam utilizadas posteriormente quando o "site/api destino" estiver off-line ou incompleto. 
  * Basta informar os argumentos `--proxy-all` e `--record-mappings` para que ele seja capaz de interceptar as requisições e gravar os mapas com as requisições e respostas.
    * `java -jar "[path-to-jar].jar" --port [number] --proxy-all "[URL]" --record-mappings --root-dir "[folder-to-save]" --verbose`
  * Para usar o dados gravados, basta parar a gravação e iniciar da mesma forma que é mostrado no passo 2.

Para mais informações, acesse o site oficial da ferramenta: http://wiremock.org/

# Wiremock UI

É um software desenvolvido em ".NET Framework 4.5" (paradigma: "Windows Forms") que tenta representar de forma gráfica os recursos 2 e 3 descritos acima. Por ser uma interface gráfica, alguns recursos foram potencializados, como por exemplo:

* Facilidade para criar e executar um servidor Wiremock
* Criar e gerenciar mais de um servidor Wiremock em apenas um lugar
* Criar multiplos cenários para a mesma API ou Site com a intenção de alterna-los conforme a necessidade de uso.
* Visualização dos mapas com sua respectiva resposta em forma de TreeView
* Gerenciar os mapas do Wiremock com as opções: criar, editar, remover, duplicar, desabilitar e visualização em forma de JSON
* O gerenciamento dos mapas (dentro da ferramenta) dispensa o restart do servidor.
* Logs em forma de texto e tabela com as opções:
  * Análise de tempo
  * Re-executar a requisição com a ferramenta interna "Web Request"
  * Comparar requisições que não deram correspondência com qualquer mapa da TreeView.
* Ferramentas internas:
  * Web Request: É um simples executador de chamadas HTTP que pode ajudar a depurar
  * Text Compare: É um simples comparador de texto
  * Text editor: Editor de texto com as opções de formatação para JSON ou XML
  * JSON Viwer: Visualizador de JSON com opções de formatação e visualização em forma de árvore

# Download

LINK

1. Descompacte em qualquer local
2. Abra o arquivo "Wiremock.exe"

# Requisitos

## Windows

1. Instale o ".NET Framework 4.5"
  * chocolatey: `choco install dotnet4.5`
  * microsoft: https://www.microsoft.com/en-us/download/details.aspx?id=30653
2. Instale o "Java"
  * chocolatey: `choco install jre8`
  * oracle: http://www.oracle.com/technetwork/pt/indexes/downloads/index.html

## Linux

Utilize o projeto "Mono.Forms" para executar no linux, contudo, não é garantido que todas as funcionalidades funcionem da mesma maneira que ocorre no Windows.

# Tutorial

## Criando um servidor do wiremock

Para criar um novo servidor:

1. Clique com o lado direito do mouse sobre o item "Servidores"
2. Clique em "Adicionar servidor"
3. Preencha as informações do servidor
  * Nome do servidor: Indica um nome que represente o seu servidor
  * Url destino: Utilizado para fazer proxy de um serviço existente
  * Porta do servidor: Indica qual porta usar, não pode existir dois servidores configurados com a mesma porta
  * Aba "Avançado": Contém as configurações que um servidor wiremock pode ter, leia a documentação oficial da ferramenta para obter informações sobre cada uma delas: http://wiremock.org/docs/running-standalone/

Para poder iniciar um servidor, é preciso que tenha ao menos um cenário cadastrado:

1. Clique com o lado direito do mouse no servidor
2. Clique em "Adicionar cenário"
3. Preencha as informações do cenário
  * Nome: Nome do cenário
  * Descrição: Informações detalhadas

Está quase pronto, agora você precisa criar seus "mapas" com suas respectivas requisições e respostas - ou apenas "mocks":

1. Clique com o lado direito do mouse no cenário desejado
2. Clique em "Adicionar mock"
3. Clique no arquivo que foi gerado dentro do cenário "Mock1.json"
4. No item "request"
  * Defina o "url" da rota desejada, por padrão o uso da "/" significa a raiz do servidor web.
  * Defina o "method" da rota desejada, por padrão é utilizado o método "GET"
  * Para mais informações sobre como montar um "mock" mais complexo acesso: http://wiremock.org/docs/request-matching/
5. No item "response"
  * Defina o "status", por padrão é 200.
  * O atributo "bodyFileName" é apenas um indicativo do arquivo que contém a resposta dessa rota
  * No atributo "headers" está os cabeçalhos que serão usados na resposta, caso você altere o tipo da resposta para XML por exemplo, lembre-se de alterar o "content-type" também.
6. Expanda na árvore o arquivo "Mock1.json" para visualizar o arquivo de resposta
7. Clique sobre o arquivo "Mock.txt" para edita-lo

Agora basta iniciar o servidor:

1. Clique com o lado direito do mouse no servidor desejado
2. Clique em "Iniciar"
3. Clique no link "View" no campo "Server URL"

GIF AQUI

cena1, cena2 e cena3, cena5

Quando o seu servidor tem o campo "Url destino" configurado, duas novas opções de inicio vão aparecer para o servidor:

* Inicio (Somente Proxy): Utilizado quando você deseja ignorar os mocks e usar a sua API existente.
* Iniciar e Gravar: Utilizado para interceptar e gravar as requisições e respostas de uma API existente. Com isso você ganha tempo para gerar seus mocks e pode usar/editar/duplicar esses dados conforme a necessidade.

cena4

# Como funciona e como contribuir

1. O Wiremock não é executado usando processos, o .JAR da última versão do Wiremock foi convertido em .NET usando a ferramenta "IKVM". Com isso, foi possível potencializar o uso da ferramenta, tendo acesso direto as principais classes.

2. O projeto usa como camada de persistência o framework "PocDataBase" que salva todas as configurações em apenas um arquivo JSON. Isso ajuda a não gastar tempo com soluções complexas.

3. No momento, não vou adicionar novas features devido a falta de tempo, ficarei a disposição apenas para bugs e pequenas melhorias. Caso queiram contribuir com novas ideias ou correções, basta apenas entrar em contato ou acessar o board do projeto.
  1. Vejo que o principal ponto de melhoria seria no formulário "FormMaster" que está com muitas linhas e pouco componentizado.
  2. Um outro ponto que considero importante é melhorar a camada de persistência, no momento, as chamadas não estão centralizadas deixando a situação perigosa para futuras melhorias e isso agrava por ser tratar de um banco de dados em forma de um único arquivo.

4. Links

IKVM: https://www.ikvm.net/
PocDatabase: https://github.com/juniorgasparotto/PocDatabase
Board: 


Adicionar proxy
--
tutorial-02.png
tutorial-03.png
tutorial-04.png
tutorial-05.png
tutorial-06.png
tutorial-07.png
tutorial-08.png

## Criar um cenário
--

tutorial-09.png
tutorial-10.png
tutorial-11.png
tutorial-12.png
tutorial-13.png

Iniciar cenário em modo de gravação
--

tutorial-14.png
tutorial-15.png
tutorial-16.png
tutorial-17.png
tutorial-18.png
tutorial-19.png
tutorial-20.png
tutorial-21.png
tutorial-22.png

Iniciar cenário usando os dados gravados
--

tutorial-23.png
tutorial-24.png
tutorial-25.png
tutorial-26.png
tutorial-27.png
tutorial-28.png
tutorial-29.png
tutorial-30.png

Usar outro cenário
--

tutorial-31.png

Iniciar apenas como proxy
--

tutorial-32.png
tutorial-33.png
tutorial-34.png

Calcular o tempo de uma requisição
--

tutorial-35.png

Menus
-- 

tutorial-36.png
tutorial-37.png

Criar API ou SITE que ainda não existe
--

tutorial-38.png
tutorial-39.png
tutorial-40.png
tutorial-41.png
tutorial-42.png
tutorial-43.png
tutorial-44.png
tutorial-45.png
tutorial-46.png
tutorial-47.png
tutorial-48.png
tutorial-49.png
tutorial-50.png
tutorial-51.png
tutorial-52.png

Controlar a exibição das informações dos mapas
--

tutorial-53.png

Duplicar mapa
--

tutorial-54.png
tutorial-55.png
tutorial-56.png
tutorial-57.png
tutorial-58.png

Desabilitar mapa
--

tutorial-59.png
tutorial-60.png
tutorial-61.png

Opções de debug
--

tutorial-62.png
tutorial-63.png
tutorial-64.png
tutorial-65.png
tutorial-66.png
tutorial-67.png
tutorial-68.png

Web Request
--

tutorial-69.png
tutorial-70.png

Comparar
--

tutorial-71.png
tutorial-72.png
tutorial-73.png
tutorial-74.png
tutorial-75.png

Editor de texto simples
--

tutorial-76.png
tutorial-77.png
tutorial-78.png

Visualizador de JSON
--

tutorial-79.png
tutorial-80.png
tutorial-81.png
tutorial-82.png
tutorial-83.png
tutorial-84.png
tutorial-85.png
tutorial-86.png

--

https://convertio.co/pt/mp4-gif/

-- cena1

cria servidor
cria cenario
cria mocks
  - renomeia
  GetAllUser
  - duplica
  user/1
  user/2
  - desativar
  user/2 (outro)
  user/insert
  mostrar json view
  esconde o nome e deixa a url
inicia no chrome
mostra log de texto e tabela
calcular o tempo de uma requisição
**** END

-- cena2

mostrar opções de debug do log 
mostrar web request e fechar
abrir novamente o web request com os dados para o comparador e mostrar comparador

-- cena3

duplicar cenário
duplicar servidor

-- cena4

criar nova api com base em uma existente
executar via CURL para ver os watchers
pausar
formatar json de respostas
executar iniciando e mostrar editando um dado mocado
executar apenas como proxy e mostrar que o dado editado não voltou e esta indo direto no site oficial
mostrar views de imagens e texto

-- cena5

menus
mostrar estrutura de arquivos e pastas .app/db.json
