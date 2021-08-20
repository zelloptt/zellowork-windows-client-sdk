using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiClientSample
{
    public partial class InstancesForm : Form
    {
        System.Collections.Generic.Dictionary<UInt16,MainForm> clients = new Dictionary<ushort,MainForm>();
        UInt16 clientId = 1; 
        public InstancesForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm(tbNetworkName.Text, clientId);
            mf.Show(this);
            listBox1.Items.Add("Zello client #" + clientId.ToString());
            clients.Add(clientId,mf);
            ++clientId;
       }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (clients.Count > 0)
            {
                List<MainForm> lst = new List<MainForm>();
                foreach (KeyValuePair<UInt16, MainForm> kvp in clients)
                {
                    lst.Add(kvp.Value);
                }
                clients.Clear();
                listBox1.Items.Clear();
                foreach (MainForm mf in lst)
                {
                    mf.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                String str = listBox1.Items[listBox1.SelectedIndex].ToString();
                UInt16 idx = Convert.ToUInt16(str.Substring(str.IndexOf('#') + 1));
                if (idx != 0 && clients.ContainsKey(idx))
                {
                    MainForm mf = clients[idx];
                    clients.Remove(idx);
                    mf.Close();
                }
            }
        }

        public void OnNormalZClientClose(UInt16 idx)
        {
            clients.Remove(idx);
            listBox1.Items.Remove("Zello client #" + idx.ToString());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnStop.Enabled = listBox1.SelectedIndex >= 0;
        }

    }
}
