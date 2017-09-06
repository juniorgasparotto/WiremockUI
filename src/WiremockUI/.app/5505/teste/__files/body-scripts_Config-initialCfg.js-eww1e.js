var initialCfg = function(){
	return {
		urlRedirect: "", // url a ser carregada após o logout, ou erro no login. Em branco ("") não redireciona, volta para o index.html
		mainAsPopup: false, // utilizado para após o login carregar o main.html em outra janela do browser.
		userComplementaryStyles: [],	// utilizado para acrescentar arquivos de CSS personalizados do usuário.
		enableOverhaul: false
	};
};
