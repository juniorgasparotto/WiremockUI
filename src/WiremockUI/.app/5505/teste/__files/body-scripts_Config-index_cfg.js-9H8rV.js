var index = function(){
	return {
		bemvindo: Ext.apdata.lang.userTexts.bemvindo,
		saudacao: Ext.apdata.lang.userTexts.saudacao,
		canChangePassword: false,
		canForgotPassword: false,
		canLinks:true,
		forgotPassScreenSize: {
			height: 350, 
			width: 350
		},//Parametros de tamanho de tela "esqueci minha senha"
		assuntosGerais: [{
			habilitado: false,
			titulo: Ext.apdata.lang.news,
			assuntoGeral: 5,
			template: publicIssueTpl, //ou publicIssueKnowMoreTpl com opção de "saiba mais..."
									  // ou publicIssueKnowMorePopupTpl com opcao de "saiba mais..." com popup 
			ordem: 0
		},{
			habilitado: false,
			titulo: Ext.apdata.lang.userTexts.quemSomosTitulo,
			template: quemSomosTpl.apply({texto: Ext.apdata.lang.userTexts.quemSomos}),
			ordem: 1
		},{
			habilitado: false,
			titulo: Ext.apdata.lang.userTexts.empresasGrupo,
			template: empresasGrupoTpl.apply({image: "images/empresasGrupo.png", href: ""}),
			ordem: 2
		}],
		
		relogioVirtual: [{
			habilitado: true,
			somenteMarcaoBtn: true,
			ordem: 3,
			dispositivo: 2,
			titulo: Ext.apdata.lang.userTexts.relogioVirtual,
                        tempoMensagem: 15
		}, {
			habilitado: false,
			somenteMarcaoBtn: true,
			ordem: 4,
			dispositivo: 5,
			titulo: Ext.apdata.lang.userTexts.relogioVirtual,
                        tempoMensagem: 15
		}, {
			habilitado: false,
			somenteMarcaoBtn: true,
			ordem: 5,
			dispositivo: 6,
			titulo: Ext.apdata.lang.userTexts.relogioVirtual,
                        tempoMensagem: 15
		}, {
			habilitado: false,
			somenteMarcaoBtn: true,
			ordem: 6,
			dispositivo: 7,
			titulo: Ext.apdata.lang.userTexts.relogioVirtual,
                        tempoMensagem: 15
		}, {
			habilitado: false,
			somenteMarcaoBtn: true,
			ordem: 7,
			dispositivo: 8,
			titulo: Ext.apdata.lang.userTexts.relogioVirtual,
                        tempoMensagem: 15
		}, {
			habilitado: false,
			somenteMarcaoBtn: true,
			ordem: 8,
			dispositivo: 9,
			titulo: Ext.apdata.lang.userTexts.relogioVirtual,
                        tempoMensagem: 15
		}, {
			habilitado: false,
			somenteMarcaoBtn: true,
			ordem: 9,
			dispositivo: 10,
			titulo: Ext.apdata.lang.userTexts.relogioVirtual,
                        tempoMensagem: 15
		}, {
			habilitado: false,
			somenteMarcaoBtn: true,
			ordem: 10,
			dispositivo: 11,
			titulo: Ext.apdata.lang.userTexts.relogioVirtual,
                        tempoMensagem: 15
		}],
		
		consultas: [{
			habilitado: false,
			titulo: Ext.apdata.lang.birthList,
			consulta: 2810,
			template: birthDayListTpl,
			image: "images/imgAniversario.gif",// pt 164988 - Deixar escolher a imagem do portlet
			ordem: 4
		}],
		forecastSlider: [{
			habilitado: false,
			titulo: Ext.apdata.lang.forecast,
			images:[
/*SP*/					"http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_bluestripes_metric&airportcode=SBSP&ForcedCity=São Paulo&ForcedState=Brasil&wmo=83780&language=BR",
/*RJ*/					"http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_bluestripes_metric&airportcode=SBJR&ForcedCity=Rio&ForcedState=Brasil&wmo=83111&language=BR",
/*Cps*/					"http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_bluestripes_metric&airportcode=SBKP&ForcedCity=Campinas&ForcedState=Brasil&wmo=83721&language=BR",
/*Miami*/				"http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_bluestripes&airportcode=KIWA&ForcedCity=Miami&ForcedState=AZ&zip=85539&language=US",
/*Ang*/					"http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_bluestripes_metric&airportcode=FNLU&ForcedCity=Luanda&ForcedState=Angola&wmo=66160&language=BR"
					],
			template: forecastSliderTpl,
			ordem: 7
		}],
		dadosExternos: [{
			habilitado: false,
			titulo: Ext.apdata.lang.forecast,
			//altura: 460, //para as cidades alternativas, a altura é 170
/*Default*/ url: "http://selos.climatempo.com.br/selos/MostraSelo.php?CODCIDADE=558&SKIN=verde",
/*SP*/		//url: "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_bluestripes_metric&airportcode=SBSP&ForcedCity=São Paulo&ForcedState=Brasil&wmo=83780&language=BR",
/*Rio*/		//url: "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_bluestripes_metric&airportcode=SBJR&ForcedCity=Rio&ForcedState=Brasil&wmo=83111&language=BR",
/*Cps*/		//url: "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_bluestripes_metric&airportcode=SBKP&ForcedCity=Campinas&ForcedState=Brasil&wmo=83721&language=BR",
/*Miami*/	//url: "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_bluestripes&airportcode=KIWA&ForcedCity=Miami&ForcedState=AZ&zip=85539&language=US ",
/*Ang*/		//url: "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_bluestripes_metric&airportcode=FNLU&ForcedCity=Luanda&ForcedState=Angola&wmo=66160&language=BR",
			ordem: 5
		}],
		pesquisaClima: [{
			habilitado: false,
			titulo: Ext.apdata.lang.portlletPesquisaPublica,
			objectId: 1832,
			template: pesquisaClimaTpl,
			ordem: 6
		}]		
	};
};
