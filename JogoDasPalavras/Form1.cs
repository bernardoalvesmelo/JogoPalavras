namespace JogoDasPalavras
{
    public partial class TelaJogoPalavrasForm : Form
    {
        private JogoPalavras jogoPalavras;
        private int palavraCount;
        private int letraCount;

        public TelaJogoPalavrasForm()
        {
            InitializeComponent();
            this.jogoPalavras = new JogoPalavras();
            Resetar();
            ConfigurarEventos();
        }

        public void ConfigurarEventos()
        {
            foreach (Button botao in panBotoes.Controls)
            {
                botao.Click += EscolherLetra;
            }
            btnResetar.Click += ResetarEvento;
        }

        public void EscolherLetra(object? sender, EventArgs e)
        {
            Button botao = (Button)sender;
            if (botao.Text == "Enter")
            {
                EnviarPalpite();
                return;
            }
            letraCount = letraCount < 5 ? letraCount : 4;
            panPalavras.Controls[24 - (palavraCount + letraCount)].Text = botao.Text;
            letraCount++;
        }

        public void EnviarPalpite()
        {
            if (letraCount < 5)
            {
                return;
            }
            string palpite = "";
            for (int i = 0; i < 5; i++)
            {
                palpite += panPalavras.Controls[24 - (palavraCount + i)].Text;
            }
            string palavra = jogoPalavras.VerificacaoPalavra(palpite);
            MostrarFeedback(palavra);
            string resultado = jogoPalavras.ObterResultado();
            if (resultado != "")
            {
                panBotoes.Enabled = false;
                lblResultado.Text = resultado;
                MessageBox.Show(resultado);
            }
        }

        public void MostrarFeedback(string palavra)
        {
            for (int i = 0; i < 5; i++)
            {
                Color cor;
                switch (palavra[i])
                {
                    case '!':
                        cor = Color.Green;
                        break;
                    case '@':
                        cor = Color.Yellow;
                        break;
                    default:
                        cor = this.BackColor;
                        break;
                }
                panPalavras.Controls[24 - (palavraCount + i)].BackColor = cor;
            }
            palavraCount += 5;
            letraCount = 0;
        }

        public void Resetar()
        {
            panBotoes.Enabled = true;
            lblResultado.Text = "Digite uma letra";
            jogoPalavras.IniciarValores();
            foreach (Label letra in this.panPalavras.Controls)
            {
                letra.Text = "";
                letra.BackColor = this.BackColor;
            }
            this.palavraCount = 0;
            this.letraCount = 0;
        }

        public void ResetarEvento(object? sender, EventArgs e)
        {
            Resetar();
        }
    }
}