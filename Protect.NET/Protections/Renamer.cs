using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Protect.NET.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Protect.NET.Protections
{
    public class Renamer : IProtection
    {
        string IProtection.name
        {
            get
            {
                return "Renamer";
            }
        }
        string IProtection.description
        {
            get
            {
                return "Renames all objects within the assembly.";
            }
        }

        private static Dictionary<string, string> TypeDefNames = new Dictionary<string, string>();
        private static Collection<TypeDef> namespaces = new Collection<TypeDef>();
        private static List<string> NamespaceNames = new List<string>();

        public void Protect()
        {

            for (int mDef = 0; mDef < Globals.asm.Modules.Count; mDef++)
            {
                ModuleDef moduleDef = Globals.asm.Modules[mDef];
                for (int j = 0; j < moduleDef.Types.Count; j++)
                {
                    TypeDef td = moduleDef.Types[j];
                    IterateType(td, Globals.asm);
                }
                moduleDef.EntryPoint.Name = Generator.getMainName();
            }
        }
        private void IterateType(TypeDef td, AssemblyDef asm)
        {
            if (td.HasNestedTypes)
            {
                for (int i = 0; i < td.NestedTypes.Count; i++)
                    IterateType(td.NestedTypes[i], asm);
            }

            if (!NamespaceNames.Contains(td.Namespace))
            {
                NamespaceNames.Add(td.Namespace);
                namespaces.Add(td);
            }
            else
                namespaces.Add(td);
            if (Checker.checkType(td))
            {
                string text;
                if (td.Name != "<Module>")
                    text = Generator.getName();
                else
                    text = Generator.getModuleName();

                if (!TypeDefNames.ContainsKey(td.Name))
                    TypeDefNames.Add(td.Name, text);

                //if (td.BaseType == null) return;
                //if (td.BaseType.Name.Contains("Form"))
                //{
                //    string tmpp = "";
                //    foreach (Resource res in asm.ManifestModule.Resources)
                //    {
                //        if (res.Name.Contains(td.Name))
                //        {
                //            tmpp = td.Name;
                //            string tmpN = res.Name.Replace(".resources", "");
                //            res.Name = tmpN.Replace(td.Name, text) + ".resources";
                //        }

                //    }
                //foreach (MethodDef mDef in td.Methods)
                //{
                //    //if (!mDef.HasBody && !mDef.FullName.Contains("getRes")) continue;
                //    foreach (Instruction instr in mDef.Body.Instructions)
                //    {
                //        if (instr.OpCode == OpCodes.Ldstr)
                //        {
                //            if (instr.Operand.ToString().Contains(tmpp))
                //                instr.Operand = instr.Operand.ToString().Replace(td.Name, text);
                //        }
                //    }

                //}
                //}
                td.Name = text;
                for (int i = 0; i < td.Methods.Count; i++)
                {
                    MethodDef md = td.Methods[i];
                    ChangeMethod(md);
                }
                for (int i = 0; i < td.Fields.Count; i++)
                {
                    FieldDef fd = td.Fields[i];
                    ChangeField(fd);
                }
                for (int i = 0; i < td.Events.Count; i++)
                {
                    EventDef ed = td.Events[i];
                    ChangeEvent(ed);
                }
                for (int i = 0; i < td.Properties.Count; i++)
                {
                    PropertyDef pd = td.Properties[i];
                    ChangeProperty(pd);
                }
            }

        }
        private void ChangeMethod(MethodDef md)
        {

            if (Checker.checkMethod(md))
            {
                md.Name = Generator.getName();
                foreach (ParamDef current in md.ParamDefs)
                {
                    current.Name = Generator.getName();
                }
                if (md.HasBody && md.Body.HasVariables)
                {
                    foreach (var current2 in md.Body.Variables)
                    {
                        current2.Name = Generator.getName();
                    }
                }
                md.Attributes = (MethodAttributes.Public | md.Attributes);
            }
        }
        private void ChangeField(FieldDef fd)
        {
            if (Checker.checkField(fd))
            {
                fd.Name = Generator.getName();
            }
        }
        private void ChangeEvent(EventDef ed)
        {
            if (Checker.checkEvent(ed))
            {
                ed.Name = Generator.getName();
            }
        }
        private void ChangeProperty(PropertyDef pd)
        {
            if (Checker.checkProperty(pd))
            {
                pd.Name = Generator.getName();
            }
        }
    }
}
