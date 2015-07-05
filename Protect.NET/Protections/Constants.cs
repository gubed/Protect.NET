using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Windows.Forms;
using Protect.NET.Helpers;
using System.Collections.Generic;

namespace Protect.NET.Protections
{
    class Constants : IProtection
    {
        string IProtection.name
        {
            get
            {
                return "Constants";
            }
        }
        string IProtection.description
        {
            get
            {
                return "Renames all constants within the assembly.";
            }
        }

        public void Protect()
        {

            TypeDef type = typeDef("Protect.NET.Runtime.Constants");
            ModuleDef mainModule = Globals.asm.ManifestModule.Types[0].Module;
            MethodDef mDefEncrypt = methodDef(type, "EncryptOrDecrypt");
            mDefEncrypt.DeclaringType = Globals.asm.ManifestModule.Types[0];
            mDefEncrypt.Name = Generator.getName();
            InjectHelper.Inject(mDefEncrypt, mainModule);


            TypeDef type2 = typeDef("Protect.NET.Runtime.Constants");
            ModuleDef mainModule2 = Globals.asm.ManifestModule.Types[0].Module;
            MethodDef mDefEncrypt2 = methodDef(type2, "EncryptOrDecrypt");
            mDefEncrypt2.DeclaringType = mDefEncrypt.DeclaringType;
            mDefEncrypt2.Name = Generator.getName();
            InjectHelper.Inject(mDefEncrypt2, mainModule2);

            List<string> keys = new List<string>();

            for (int mDef = 0; mDef < Globals.asm.Modules.Count; mDef++)
            {
                ModuleDef moduleDef = Globals.asm.Modules[mDef];
                for (int j = 0; j < moduleDef.Types.Count; j++)
                {
                    TypeDef td = moduleDef.Types[j];
                    for (int qq = 0; qq < td.Methods.Count; qq++)
                    {
                        MethodDef mmDef = td.Methods[qq];
                        if (!mmDef.HasBody) return;
                        int instrCount = mmDef.Body.Instructions.Count;
                        for (int i = 0; i < instrCount; i++)
                        {
                            Instruction cur = mmDef.Body.Instructions[i];
                            string key = Utils.GenerateKey(15);
                            
                            if (cur.OpCode == OpCodes.Ldstr && !keys.Contains(cur.Operand.ToString()))
                            {
                                int rand = Utils.randomInt(0, 2);
                                Console.WriteLine(rand.ToString());
                                cur.Operand = Runtime.Constants.EncryptOrDecrypt(cur.Operand.ToString(), key);

                                mmDef.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Ldstr, key));
                                mmDef.Body.Instructions.Insert(i + 2, new Instruction(OpCodes.Call, (rand == 0) ? mDefEncrypt : mDefEncrypt2));

                                mmDef.Body.OptimizeBranches();
                                mmDef.Body.SimplifyBranches();

                                keys.Add(key);
                            }

                        }
                    }
                }
            }
        }
        private TypeDef typeDef(string name)
        {
            var tmpAsm = AssemblyDef.Load(Application.ExecutablePath);
            var importmeType = tmpAsm.ManifestModule.Find(name, false);
            return importmeType;
        }
        private MethodDef methodDef(TypeDef tDef, string name)
        {
            var importmeType = tDef.FindMethod(name);
            return importmeType;
        }
    }
}
