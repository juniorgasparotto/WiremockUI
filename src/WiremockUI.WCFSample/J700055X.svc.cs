using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class J700055X : IJ700055X
    {
        public DadosResposta Processar(DadosEnvio dadosEnvio)
        {
            var respo = new DadosResposta();
            if (dadosEnvio.Agencia == "1500" && dadosEnvio.Conta == "01010" && dadosEnvio.Dac == "1")
            {
                respo.Produtos = new List<string>
                {
                    "TitulosPublicos",
                };
            }
            else if (dadosEnvio.Agencia == "1500" && dadosEnvio.Conta == "24530" && dadosEnvio.Dac == "1")
            {
                respo.Produtos = new List<string>
                {
                    "Previdencia",
                };
            }
            else if (dadosEnvio.Agencia == "1500" && dadosEnvio.Conta == "31530" && dadosEnvio.Dac == "1")
            {
                respo.Produtos = new List<string>
                {
                    "Fundos",
                };
            }

            return respo;
        }
    }
}
