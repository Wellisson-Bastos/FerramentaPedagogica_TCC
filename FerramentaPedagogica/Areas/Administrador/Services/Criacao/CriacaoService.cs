using FerramentaPedagogica.Areas.Administrador.Models;
using FerramentaPedagogica.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FerramentaPedagogica.Areas.Administrador.Services.Criacao
{
    public class CriacaoService
    {
        public DTOPerguntaLista[] ObterPerguntas(int piJogo)
        {
            using (var db = new studiesContext())
            {
                var lRetorno = (from PER in db.Pergunta
                                where
                                  PER.CodigoJogo == piJogo
                                orderby
                                  PER.Ordem
                                select new DTOPerguntaLista
                                {
                                    Codigo = PER.Codigo,
                                    Texto = PER.Texto
                                }).ToArray();

                for (int i = 0; i < lRetorno.Length; i++)
                {
                    lRetorno[i].Texto = (i + 1) + " - " + lRetorno[i].Texto;
                }

                return lRetorno;
            }
        }

        public DTOPergunta ObterDadosPergunta(int piJogo, int piPergunta, int piUsuario)
        {
            using (var db = new studiesContext())
            {
                var lRetorno = (from PER in db.Pergunta
                                where
                                  PER.CodigoJogo == piJogo &&
                                  PER.Codigo == piPergunta
                                select new DTOPergunta
                                {
                                    CodigoUsuario = PER.CodigoJogoNavigation.PkUsuario,
                                    Codigo = PER.Codigo,
                                    Ordem = PER.Ordem.Value,
                                    Descricao = PER.Texto,
                                    Pontuacao = PER.Pontuacao.Value,
                                    Tempo = PER.Tempo,
                                    ImagemByte = PER.Imagem,
                                    URLYoutube = PER.LinkVideo,
                                    CodigoJogo = PER.CodigoJogo.Value
                                }).FirstOrDefault();

                var lRespostas = db.Resposta.Where(p => p.CodigoPergunta == lRetorno.Codigo).ToList();

                foreach (var lResposta in lRespostas)
                {
                    switch (lResposta.Opcao)
                    {
                        case "A":
                            lRetorno.RespostaA = lResposta.Texto;
                            lRetorno.RespostaACorreta = lResposta.RespostaCorreta;
                            break;
                        case "B":
                            lRetorno.RespostaB = lResposta.Texto;
                            lRetorno.RespostaBCorreta = lResposta.RespostaCorreta;
                            break;
                        case "C":
                            lRetorno.RespostaC = lResposta.Texto;
                            lRetorno.RespostaCCorreta = lResposta.RespostaCorreta;
                            break;
                        case "D":
                            lRetorno.RespostaD = lResposta.Texto;
                            lRetorno.RespostaDCorreta = lResposta.RespostaCorreta;
                            break;
                    }
                }

                lRetorno.UsuarioCriador = UsuarioCriador(piUsuario, piJogo);

                return lRetorno;
            }
        }

        public DTOPergunta AdicionarPergunta(DTOPergunta pPergunta)
        {
            Pergunta lPergunta = new Pergunta()
            {
                Texto = pPergunta.Descricao,
                Imagem = pPergunta.ImagemByte != null ? pPergunta.ImagemByte : string.IsNullOrEmpty(pPergunta.Imagem) ? null : TrataBase64String(pPergunta.Imagem),
                LinkVideo = string.IsNullOrEmpty(pPergunta.URLYoutube) ? string.Empty : pPergunta.URLYoutube,
                Tempo = pPergunta.Tempo,
                Ordem = pPergunta.Ordem,
                CodigoJogo = pPergunta.CodigoJogo,
                Pontuacao = pPergunta.Pontuacao
            };

            using (var db = new studiesContext())
            {
                db.Pergunta.Add(lPergunta);
                db.SaveChanges();

                pPergunta.Codigo = lPergunta.Codigo;

                AdicionarRespostas(pPergunta);

                return pPergunta;
            }
        }

        public void EditarPergunta(DTOPergunta pPergunta)
        {
            using (var db = new studiesContext())
            {
                var lPergunta = db.Pergunta.FirstOrDefault(p => p.Codigo == pPergunta.Codigo);

                lPergunta.Imagem = pPergunta.ImagemByte != null ? pPergunta.ImagemByte : string.IsNullOrEmpty(pPergunta.Imagem) ? null : TrataBase64String(pPergunta.Imagem);
                lPergunta.LinkVideo = string.IsNullOrEmpty(pPergunta.URLYoutube) ? string.Empty : pPergunta.URLYoutube;
                lPergunta.Texto = pPergunta.Descricao;
                lPergunta.Tempo = pPergunta.Tempo;
                lPergunta.Ordem = pPergunta.Ordem;
                lPergunta.Pontuacao = pPergunta.Pontuacao;

                db.SaveChanges();

                EditarRespostas(pPergunta);
            }
        }

        public string ValidarPergunta(DTOPergunta pPergunta)
        {
            string lRetorno = string.Empty;

            if (string.IsNullOrEmpty(pPergunta.Descricao))
            {
                lRetorno = "A pergunta deve ser preenchida" + Environment.NewLine;
            }

            string lsValidacaoAlternativas = ValidarAlternativaCorreta(pPergunta);

            if (!string.IsNullOrEmpty(lsValidacaoAlternativas))
            {
                lRetorno = lsValidacaoAlternativas;

            }
            else if (string.IsNullOrEmpty(pPergunta.RespostaA) || string.IsNullOrEmpty(pPergunta.RespostaB))
            {
                lRetorno = "As alternativas A e B devem ser preenchidas";
            }
            else if (string.IsNullOrEmpty(pPergunta.RespostaC) && !string.IsNullOrEmpty(pPergunta.RespostaD))
            {
                lRetorno = "Preencha a alternativa C para utilizar a D";
            }
            else if ((string.IsNullOrEmpty(pPergunta.RespostaC) && pPergunta.RespostaCCorreta) || (string.IsNullOrEmpty(pPergunta.RespostaD) && pPergunta.RespostaDCorreta))
            {
                lRetorno = "A alternativa correta deve ser preenchida";
            }

            return lRetorno;
        }

        private byte[] TrataBase64String(string psBase64)
        {
            var lRetorno = new byte[] { };

            if (!string.IsNullOrEmpty(psBase64))
            {
                int liPosInicial = psBase64.IndexOf(",") + 1;

                if (liPosInicial > -1)
                {
                    psBase64 = psBase64.Substring(liPosInicial, psBase64.Length - liPosInicial);
                }
                lRetorno = Convert.FromBase64String(psBase64);
            }

            return lRetorno;
        }

        private void AdicionarRespostas(DTOPergunta pPergunta)
        {
            AdicionarResposta(pPergunta.RespostaA, pPergunta.RespostaACorreta, "A", pPergunta.Codigo.Value);

            AdicionarResposta(pPergunta.RespostaB, pPergunta.RespostaBCorreta, "B", pPergunta.Codigo.Value);

            if (!string.IsNullOrEmpty(pPergunta.RespostaC))
            {
                AdicionarResposta(pPergunta.RespostaC, pPergunta.RespostaCCorreta, "C", pPergunta.Codigo.Value);
            }

            if (!string.IsNullOrEmpty(pPergunta.RespostaD))
            {
                AdicionarResposta(pPergunta.RespostaD, pPergunta.RespostaDCorreta, "D", pPergunta.Codigo.Value);
            }
        }

        private void EditarRespostas(DTOPergunta pPergunta)
        {
            EditarResposta(pPergunta.RespostaA, pPergunta.RespostaACorreta, "A", pPergunta.Codigo.Value);

            EditarResposta(pPergunta.RespostaB, pPergunta.RespostaBCorreta, "B", pPergunta.Codigo.Value);

            if (!string.IsNullOrEmpty(pPergunta.RespostaC))
            {
                EditarResposta(pPergunta.RespostaC, pPergunta.RespostaCCorreta, "C", pPergunta.Codigo.Value);
            }
            else if (ExisteAlternativa("C", pPergunta.Codigo.Value))
            {
                ExcluirResposta("C", pPergunta.Codigo.Value);
            }

            if (!string.IsNullOrEmpty(pPergunta.RespostaD))
            {
                EditarResposta(pPergunta.RespostaD, pPergunta.RespostaDCorreta, "D", pPergunta.Codigo.Value);
            }
            else if (ExisteAlternativa("D", pPergunta.Codigo.Value))
            {

            }
        }

        private void AdicionarResposta(string psTexto, bool pbRespostaCorreta, string psOpcao, int piPergunta)
        {
            Resposta lResposta = new Resposta()
            {
                Texto = psTexto,
                RespostaCorreta = pbRespostaCorreta,
                Opcao = psOpcao,
                CodigoPergunta = piPergunta
            };

            using (var db = new studiesContext())
            {
                db.Resposta.Add(lResposta);
                db.SaveChanges();
            }
        }

        private void EditarResposta(string psTexto, bool pbRespostaCorreta, string psOpcao, int piPergunta)
        {
            using (var db = new studiesContext())
            {
                var lResposta = db.Resposta.FirstOrDefault(p => p.CodigoPergunta == piPergunta && p.Opcao.Equals(psOpcao));

                if (lResposta == null)
                {
                    var lRespostaNova = new Resposta()
                    {
                        Texto = psTexto,
                        RespostaCorreta = pbRespostaCorreta,
                        Opcao = psOpcao,
                        CodigoPergunta = piPergunta
                    };

                    db.Resposta.Add(lRespostaNova);
                    db.SaveChanges();
                }
                else
                {
                    lResposta.Texto = psTexto;
                    lResposta.RespostaCorreta = pbRespostaCorreta;

                    db.SaveChanges();
                }
            }
        }

        private bool ExisteAlternativa(string psOpcao, int piPergunta)
        {
            using (var db = new studiesContext())
            {
                return db.Resposta.Any(p => p.CodigoPergunta == piPergunta && p.Opcao.Equals(psOpcao));
            }
        }

        public string ValidarAlternativaCorreta(DTOPergunta pPergunta)
        {
            string lRetorno = string.Empty;

            int liCountAlternativasCorretas = 0;

            if (pPergunta.RespostaACorreta)
            {
                liCountAlternativasCorretas++;
            }

            if (pPergunta.RespostaBCorreta)
            {
                liCountAlternativasCorretas++;
            }

            if (pPergunta.RespostaCCorreta)
            {
                liCountAlternativasCorretas++;
            }

            if (pPergunta.RespostaDCorreta)
            {
                liCountAlternativasCorretas++;
            }

            if (liCountAlternativasCorretas == 0)
            {
                lRetorno = "Marque a alternativa correta.";
            }
            else if (liCountAlternativasCorretas > 1)
            {
                lRetorno = "Marque apenas uma alternativa correta.";
            }

            return lRetorno;
        }

        public async Task<bool> ExcluirPergunta(int piCodigo)
        {
            try
            {
                using (var db = new studiesContext())
                {
                    var lPergunta = db.Pergunta.FirstOrDefault(p => p.Codigo == piCodigo);

                    db.Pergunta.Remove(lPergunta);

                    await db.SaveChangesAsync();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void ExcluirResposta(string psOpcao, int piPergunta)
        {
            using (var db = new studiesContext())
            {
                var lResposta = db.Resposta.FirstOrDefault(p => p.CodigoPergunta == piPergunta && p.Opcao.Equals(psOpcao));

                db.Resposta.Remove(lResposta);

                db.SaveChanges();
            }
        }

        public bool UsuarioCriador(int piUsuario, int piJogo)
        {
            using (var db = new studiesContext())
            {
                return db.Jogo.Any(p => p.Codigo == piJogo && p.PkUsuario == piUsuario);
            }
        }

        public bool JogoFavoritado(int piUsuario, int piJogo)
        {
            using (var db = new studiesContext())
            {
                return db.Favorito.Any(p => p.CodigoJogo == piJogo && p.CodigoUsuario == piUsuario);
            }
        }

        public void FavoritarJogo(bool Favoritar, int piUsuario, int piJogo)
        {
            using (var db = new studiesContext())
            {
                if (Favoritar)
                {
                    Favorito lFavorito = new Favorito()
                    {
                        CodigoJogo = piJogo,
                        CodigoUsuario = piUsuario,
                        DataInsercao = DateTime.Now
                    };

                    db.Favorito.Add(lFavorito);
                    db.SaveChanges();
                }
                else
                {
                    var lFavorito = db.Favorito.FirstOrDefault(p => p.CodigoUsuario == piUsuario && p.CodigoJogo == piJogo);

                    db.Favorito.Remove(lFavorito);

                    db.SaveChanges();
                }
            }
        }
    }
}
