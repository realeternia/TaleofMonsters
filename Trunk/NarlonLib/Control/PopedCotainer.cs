﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace NarlonLib.Control
{
    public partial class PopedCotainer : UserControl
    {
        public PopedCotainer()
        {
            InitializeComponent();
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {// Alt+F4 is to closing
            if ((keyData & Keys.Alt) == Keys.Alt)
                if ((keyData & Keys.F4) == Keys.F4)
                {
                    this.Parent.Hide();
                    return true;
                }
            
            if ((keyData & Keys.Enter) == Keys.Enter)
            {
                if (this.ActiveControl is Button)
                {
                    (this.ActiveControl as Button).PerformClick();
                    return true;
                }
            }
            
            return base.ProcessDialogKey(keyData);
        }

  

    }
}
