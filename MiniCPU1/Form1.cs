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
        // ===== Estado de la CPU =====
        int[] memoria = new int[1000];
        int PC = 300, IR = 0, AC = 0;
        int paso = 1;
        bool halt = false;

        public MiniCPU()
        {
            InitializeComponent();

            // Programa ejemplo
            memoria[300] = 1940;
            memoria[301] = 5941;
            memoria[302] = 2941;
            memoria[940] = 3;
            memoria[941] = 2;

            Dump("Inicio del programa");
        }

        // ===== Evento del botón =====
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (halt) return;

            // FETCH
            IR = memoria[PC];
            PC++;
            Dump($"Paso {paso++} - FETCH");

            // EXECUTE
            int opcode = IR / 1000;
            int addr = IR % 1000;

            switch (opcode)
            {
                case 1: AC = memoria[addr]; break;         // LOAD
                case 2: memoria[addr] = AC; break;         // STORE
                case 5: AC += memoria[addr]; break;        // ADD
                default:
                    Dump($"ERROR: opcode {opcode} no válido en instrucción {Fmt(IR)}");
                    halt = true;
                    return;
            }

            Dump($"paso {paso++} - EXECUTE");

            if (PC > 302) // fin del programa en este ejemplo
            {
                Dump("✅ Programa terminado");
                halt = true;
            }
        }

        // ===== Utilidades =====
        private void Dump(string stage)
        {
            txtOutput.AppendText($"\r\n--- {stage} ---\r\n");
            txtOutput.AppendText($"PC = {PC:0000}, IR = {Fmt(IR)}, AC = {Fmt(AC)}\r\n");
            txtOutput.AppendText($"Mem[300] = {Fmt(memoria[300])}\r\n");
            txtOutput.AppendText($"Mem[301] = {Fmt(memoria[301])}\r\n");
            txtOutput.AppendText($"Mem[302] = {Fmt(memoria[302])}\r\n");

            txtOutput.AppendText($"Mem[940] = {Fmt(memoria[940])}\r\n");
            txtOutput.AppendText($"Mem[941] = {Fmt(memoria[941])}\r\n");
        }

        private string Fmt(int v) => $"{v:0000}";
    }
}

