using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proyecto_LFA
{
    public partial class Form1 : Form
    {
        public string inicial;
        public string[] finales;
        public Stack Pila = new Stack();
        public string estadoActual;
        public List<List<string>> estados;
        public string transicionHecha;
        public string pilaResultante;
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Proceso de la carag de archivo
                string ruta;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                string acepto = Convert.ToString(openFileDialog.ShowDialog());
                ruta = openFileDialog.FileName;

                if (acepto == "OK") { } else { ruta = "";}
            
                TextReader Leer_archivo = new StreamReader(ruta);
               
                richTextBox1.Text = Leer_archivo.ReadToEnd();
    
                //Variables para particionar el archivo de texto
                string archivo = richTextBox1.Text;
                string[] separador = archivo.Split('\n');
                string aux;
                string total;
                string recorridos = string.Empty;
                estados = new List<List<string>>();
                string[] transicion;

                txtRecorridos.Text = Convert.ToString(separador);
                total = separador[0];
                inicial = separador[1];
                aux = separador[2];
                finales = aux.Split(",");

                // Creacion de estados con base al archivo de texto
                for (int i = 0; i < int.Parse(total); i++)
                {
                    List<string> nuevoEstado = new List<string>();
                    estados.Add(nuevoEstado);
                }
                // Insercion de los recorridos que puede tener el estado
                for (int i = 3; i < separador.Length; i++)
                {
                    recorridos = recorridos + separador[i] + "\n";
                    transicion = separador[i].Split(",");
                    estados[int.Parse(transicion[0])].Add(separador[i]);
                }

                txtEstados.Text = total;
                txtInicial.Text = inicial;
                txtFinales.Text = aux;
                txtRecorridos.Text = recorridos;
                txtTransiciones.Text = "";
                txtPila.Text = "";

                button2.Enabled = true;
                textBox1.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Ruta o Archivo no valido.", "Error");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            textBox1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Variables para evaluar la cadena entrante
            Pila = new Stack();
            int tamanioCadena = 0;
            int cantidadTransiciones = 0;
            bool cadenaValida = true;
            bool encontrado = false;
            string[] transicion;

            // Se empieza a evaluar la cadena
            try
            {
                if (textBox1.Text != "")
                {
                    transicionHecha = "";
                    pilaResultante = "";
                    estadoActual = inicial;
                    while (tamanioCadena < textBox1.Text.Length && cadenaValida == true)
                    {
                        encontrado = false;
                        cantidadTransiciones = 0;
                        while (encontrado == false && estados[int.Parse(estadoActual)].Count > cantidadTransiciones)
                        {
                            transicion = estados[int.Parse(estadoActual)][cantidadTransiciones].Split(",");
                            if (textBox1.Text[tamanioCadena].ToString() == transicion[1])
                            {
                                if (transicion[2] != "")
                                {
                                    Pila.Pop();
                                }
                                if (transicion[3] != "")
                                {
                                    if (transicion[3].ToString().Length > 1)
                                    {
                                        for (int i = 0; i < transicion[3].ToString().Length; i++)
                                        {
                                            char[] separador = transicion[3].ToCharArray();
                                            Pila.Push(separador[i].ToString());
                                        }
                                    }
                                    else
                                    {
                                        Pila.Push(transicion[3].ToString());
                                    }
                                }
                                estadoActual = transicion[4];
                                encontrado = true;
                                for (int i = 0; i < transicion.Length; i++)
                                {
                                    if (transicion[i] == "")
                                    {
                                        transicion[i] = "ε";
                                    }
                                }
                                transicionHecha = transicionHecha + "→ ( ( " + transicion[0]+", "+transicion[1]+", "+transicion[2]+" ), ( "+transicion[4]+", "+transicion[3]+" ) )" + "\n";
                            }
                            cantidadTransiciones++;

                            // No encontro el caracter a leer en los posbiles caracteres del estado
                            if (encontrado == false && estados[int.Parse(estadoActual)].Count == cantidadTransiciones)
                            {
                                cadenaValida = false;
                            }
                        }
                        tamanioCadena++;

                    }
                    var aux = Pila.ToArray();
                    pilaResultante = String.Join("\n", aux);
                    if (pilaResultante == "")
                    {
                        pilaResultante = pilaResultante + "ε";
                    }
                    foreach (var estadoFinal in finales)
                    {
                        if (estadoActual == estadoFinal)
                        {
                            if (cadenaValida == true && Pila.Count == 0)
                            {
                                txtPila.Text = pilaResultante;
                                txtTransiciones.Text = transicionHecha;
                                MessageBox.Show("La cadena es valida.");
                            }
                            else
                            {
                                txtPila.Text = pilaResultante;
                                txtTransiciones.Text = transicionHecha;
                                MessageBox.Show("La cadena no es valida.");
                            }
                        }
                        else
                        {
                            txtPila.Text = pilaResultante;
                            txtTransiciones.Text = transicionHecha;
                            MessageBox.Show("La cadena no es valida.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresarse texto para poder validar.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("La cadena no es valida, pila vacía.");
            }   
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void label10_Click(object sender, EventArgs e)
        {
        }
    }
}
