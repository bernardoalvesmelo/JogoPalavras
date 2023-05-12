namespace JogoDasPalavras
{
    public partial class TelaJogoPalavrasForm : Form
    {
        private JogoPalavras jogoPalavras;
        private int palavraCount;
        private int letraCount;
        private Button[] botoesDigitados;

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
            Label letra = (Label)panPalavras.Controls[24 - (palavraCount + letraCount)];
            letra.Text = botao.Text;
            letraCount = letraCount < 5 ? ++letraCount : 5;
            botoesDigitados[letraCount - 1] = botao;
        }

        public void EnviarPalpite()
        {
            if (letraCount < 5)
            {
                return;
            }
            string palavra = VerificarPalpite();
            MostrarFeedback(palavra);
            AtualizarPalavra();
            VerificarResultado();
        }

        public string VerificarPalpite()
        {
            string palpite = "";
            for (int i = 0; i < 5; i++)
            {
                Label letra = (Label)panPalavras.Controls[24 - (palavraCount + i)];
                palpite += letra.Text;
            }
            return jogoPalavras.VerificacaoPalavra(palpite);
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
                        cor = Color.White;
                        botoesDigitados[i].Enabled = false;
                        botoesDigitados[i].BackColor = this.BackColor;
                        break;
                }
                Label letra = (Label)panPalavras.Controls[24 - (palavraCount + i)];
                letra.BackColor = cor;
            }
        }

        public void VerificarResultado()
        {
            string resultado = jogoPalavras.ObterResultado();
            if (resultado != "")
            {
                panBotoes.Enabled = false;
                lblResultado.Text = resultado;
                MessageBox.Show(resultado);
            }
        }

        private void AtualizarPalavra()
        {
            palavraCount += 5;
            letraCount = 0;
            botoesDigitados = new Button[5];
        }

        private void Resetar()
        {
            this.jogoPalavras.IniciarValores();
            this.botoesDigitados = new Button[5];
            panBotoes.Enabled = true;
            lblResultado.Text = "Digite uma letra";
            foreach (Button botao in panBotoes.Controls)
            {
                botao.Enabled = true;
                botao.BackColor = this.btnEnter.BackColor;
            }
            foreach (Label letra in this.panPalavras.Controls)
            {
                letra.Text = "";
                letra.BackColor = this.BackColor;
            }
            this.palavraCount = 0;
            this.letraCount = 0;
        }

        private void ResetarEvento(object? sender, EventArgs e)
        {
            Resetar();
        }
    }
}