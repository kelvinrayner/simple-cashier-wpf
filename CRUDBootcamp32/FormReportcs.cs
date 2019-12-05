using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDBootcamp32
{
    public partial class FormReportcs : Form
    {
        int DocKey;
        public FormReportcs(int dockey)
        {
            InitializeComponent();
            this.DocKey = dockey;
        }

        private void FormReportcs_Load(object sender, EventArgs e)
        {
            TransactionReport1.SetParameterValue("@DocKey", DocKey);
        }
    }
}
