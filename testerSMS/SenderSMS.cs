using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testerSMS
{
    public partial class SenderSMS : Form
    {
        public SenderSMS()
        {
            InitializeComponent();
            llenartree();
        }
        public string mensaje = string.Empty;
        public string users = string.Empty;
        public string nodosstring = string.Empty;

        
        public string sms()
        {

           // mensaje = "Mensaje de pruena";
           // users = "3213123,6548947,654651,524654,65464,846541";


            string querry = " INSERT INTO dbo.[MMSEND] ( [ORIGINATOR],[DESTINATION],[MSGTEXT], [SERVICEFROM],[SERVICETO],[USERNAME], [USERPWD])"  
                     +"SELECT '7246','506'+U.UserName"
                     +",'"+ mensaje+"'"
                     +",'LeeSMPPServer','7246',NULL,NULL FROM ePines_Development.dbo.[User] U (nolock)"
                     +"WHERE UserID IN ("+users+")";


            return querry;
        }



        public void llenartree()
        {
            trv.Nodes.Add("*", "Todos");
            trv.Nodes["*"].Nodes.Add("31321", "user1");
            trv.Nodes["*"].Nodes.Add("3756", "user2");
            trv.Nodes["*"].Nodes.Add("2341", "user3");
            trv.Nodes["*"].Nodes.Add("55", "agenteZ");

        }


        private void button1_Click(object sender, EventArgs e)
        {

            users = string.Empty;
            nodosstring = string.Empty;
            mensaje = string.Empty;


            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("inserte un mensaje...");
            }
            else
            {
                mensaje = textBox1.Text;
            }
            
            //////////////////////

            


            ////////////////////

           
                foreach (TreeNode Nodos in trv.Nodes["*"].Nodes)
                {
                    if (Nodos.Checked)
                    {
                        nodosstring += Nodos.Name + ",";
                    }
                }

                if (nodosstring == string.Empty)
                {
                    MessageBox.Show("seleccione un usuario");

                }
                else
                {
                    users = nodosstring.Substring(0, (nodosstring.Length - 1));
                }

            
                textBox2.Text = sms();


        }

        private void trv_AfterCheck(object sender, TreeViewEventArgs e)
        {


            // Se remueve el evento para evitar que se ejecute nuevamente por accion de cambio de estado 
            // en esta operacion
            trv.AfterCheck -= trv_AfterCheck;

            // Se valida si el nodo marcado tiene un nodo padre
            // en caso de tenerlo se debe evaluar los nodos al mismo nivel para determinar si todos estan marcados, 
            // si lo estan se marca tambien el nodo padre

            if (e.Node.Parent != null)
            {
                bool result = true;
                foreach (TreeNode node in e.Node.Parent.Nodes)
                {
                    if (!node.Checked)
                    {
                        result = false;
                        break;
                    }
                }

                e.Node.Parent.Checked = result;
            }


            // Se valida si el nodo tiene hijos
            // si los tiene se recorren y asignan el estado del nodo que se esta evaluando
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }

            trv.AfterCheck += trv_AfterCheck;
        }
    
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
           else if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }



        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            label1.Text = textBox1.Text.Length.ToString();
            if (textBox1.Text.Length > 159)
            {
                label1.BackColor = System.Drawing.Color.Red;
            }
            else if (textBox1.Text.Length < 159)
            {
                label1.BackColor = System.Drawing.Color.Transparent;
            }
        }

    }
}
