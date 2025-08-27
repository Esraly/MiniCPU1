using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniCPU1
{
    public partial class MiniCPU : Form
    {
        int[] memoria = new int[1000];
        int PC = 300, IR = 0, AC = 0;
        int paso = 1;
        bool stop = false;

        public MiniCPU()
        {
            InitializeComponent();

            memoria[300] = 1940;
            memoria[301] = 5941;
            memoria[302] = 2941;
            memoria[303] = 6940;
            memoria[940] = 3;
            memoria[941] = 2;

            mensaje("Inicio del programa");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (stop) return;
            txtOutput.Clear();
            
            // FETCH
            IR = memoria[PC];
            PC++;
            mensaje($"Paso {paso++} - FETCH");

            // EXECUTE
            int op = IR / 1000; //1940 / 1000 = 1
            int addr = IR % 1000;

            switch (op)
            {
                case 1: AC = memoria[addr]; break;         // LOAD
                case 2: memoria[addr] = AC; break;         // STORE
                case 5: AC += memoria[addr]; break;        // ADD
                default:
                    txtOutput.AppendText($"\r\nERROR: {op} no válido en instrucción {Fmt(IR)}");               
                    txtOutput.AppendText($"\r\nPrograma terminado");
                    stop = true;
                    return;
            }

            mensaje($"paso {paso++} - EXECUTE");

            if (PC > 303) // fin del programa en este ejemplo
            {
                mensaje("Programa terminado");
                stop = true;
            }
        }

        private void btn2_exit(object sender, EventArgs e) //Boton para salir jaja
        {
            Application.Exit();
        }

        private void mensaje(string str)
        {
            txtOutput.AppendText($"\r\n{str}\r\n");
            txtOutput.AppendText($"\r\nPC = {PC:0000}, IR = {Fmt(IR)}, AC = {Fmt(AC)}\r\n\n");
            txtOutput.AppendText($"Mem[300] = {Fmt(memoria[300])}\r\n");
            txtOutput.AppendText($"Mem[301] = {Fmt(memoria[301])}\r\n");
            txtOutput.AppendText($"Mem[302] = {Fmt(memoria[302])}\r\n");
            txtOutput.AppendText($"Mem[303] = {Fmt(memoria[303])}\r\n");

            txtOutput.AppendText($"Mem[940] = {Fmt(memoria[940])}\r\n");
            txtOutput.AppendText($"Mem[941] = {Fmt(memoria[941])}\r\n");
        }

        private string Fmt(int v) => $"{v:0000}"; 
    }
}

