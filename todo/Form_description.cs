using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace todo
{
    public partial class Form_description : Form
    {
        Form_edit formedit;
        public Form_description(Form_edit form_Edit)
        {
            InitializeComponent();
            formedit = form_Edit;

        }

    }
}
