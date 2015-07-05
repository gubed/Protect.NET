using dnlib.DotNet;
using Protect.NET.Helpers;
using Protect.NET.Protections;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Protect.NET.Forms
{
    public partial class frmMain : Form
    {
        // hello
        List<IProtection> protections = new List<IProtection>();
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Globals.asm = AssemblyDef.Load(@"C:\Users\Admin\Desktop\obf test.exe");


            Generator.type = Generator.sType.Unreadable;


            loadProtections();
            runProtections();

            Globals.asm.Write(@"C:\Users\Admin\Desktop\out.exe");
            Environment.Exit(0);
        }
        private void runProtections()
        {
            foreach (IProtection protection in protections)
                protection.Protect();
        }
        private void loadProtections()
        {
            //protections.Add(new Proxy());
            protections.Add(new Constants());
            protections.Add(new Renamer());
        }

    }
}
