using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductManagement
{
    public partial class StockMain : Form
    {
        public StockMain()
        {
            InitializeComponent();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products product = new Products();
            product.MdiParent = this;
            product.Show();
        }

        private void StockMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program is made by Emir Misic. @2017.", "About");
        }
    }    
}
