using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Protect.NET.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protect.NET.Protections
{
    class Proxy : IProtection
    {
        string IProtection.name
        {
            get
            {
                return "Proxy";
            }
        }
        string IProtection.description
        {
            get
            {
                return "Hides method calls within the assembly.";
            }
        }

        public void Protect()
        {
            for(int tDef = 0; tDef < Globals.asm.ManifestModule.Types.Count; tDef++)
            {
                TypeDef typeDef = Globals.asm.ManifestModule.Types[tDef];
                for(int mDef = 0; mDef < typeDef.Methods.Count; mDef++)
                {
                    MethodDef methodDef = typeDef.Methods[mDef];
                    if (!methodDef.HasBody) return;

                    if(methodDef.Name == "Main")
                    {
                        int instrCount = methodDef.Body.Instructions.Count;
                        for (int i = 0; i < instrCount; i++)
                        {
                            Instruction cur = methodDef.Body.Instructions[i];

                            if (cur.OpCode != OpCodes.Call) return;


                            MethodDef m = null;
                            MemberRef r = null;
                            try
                            {
                                m = (MethodDef)cur.Operand;
                            }
                            catch (InvalidCastException)
                            {
                                r = (MemberRef)cur.Operand;
                            }
                            TypeSig tRef = (r==null) ? r.ReturnType : m.ReturnType;
                            MethodDefUser callMethod = new MethodDefUser(Generator.getName(), MethodSig.CreateInstance(tRef), MethodAttributes.FamANDAssem | MethodAttributes.Family |  MethodAttributes.Static);

                            callMethod.Body = new CilBody();

                            //if (m != null)
                            //{
                            //    if (m.HasThis)
                            //    {
                            //        ParamDefUser param = new ParamDefUser(methodDef.DeclaringType.Name.ToLower());
                            //        param.MarshalType = methodDef.DeclaringType.mar
                            //        callMethod.Parameters.Add(param);
                            //        methodDef.Body.Instructions.Insert(i+1, Instruction.Create(OpCodes.Ldarg, jeeps));
                            //    }
                            //    if (m.HasParameters)
                            //    {

                            //        foreach (ParameterDef p in m.Parameters)
                            //        {
                            //            ParameterDef newP = new ParameterDef(Generator.getName(),
                            //                p.Attributes, p.ParameterType);
                            //            asd.Parameters.Add(newP);
                            //            mEdit.Append(Instruction.Create(OpCodes.Ldarg, newP));
                            //        }
                            //    }
                            //}
                        }

                    }
                }
            }
        }
    }
}
