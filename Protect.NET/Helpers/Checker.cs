using dnlib.DotNet;

namespace Protect.NET.Helpers
{
    public static class Checker
    {
        public static bool checkType(TypeDef td)
        {
            return !td.IsRuntimeSpecialName && !td.IsSpecialName && !td.IsNestedFamilyOrAssembly && !td.IsNestedFamilyAndAssembly;
        }
        public static bool checkMethod(MethodDef md)
        {
            if (!md.IsConstructor && !md.IsFamilyAndAssembly && !md.IsSpecialName && !md.IsRuntimeSpecialName && !md.IsRuntime && !md.IsFamily)
            {
                if (md.DeclaringType.BaseType != null)
                {
                    if (!md.DeclaringType.BaseType.Name.Contains("Delegate"))
                        return true;
                }
                return false;

            }
            return false;
        }
        public static bool checkField(FieldDef fd)
        {
            return !fd.IsFamilyOrAssembly && !fd.IsSpecialName && !fd.IsRuntimeSpecialName && !fd.IsFamily && !fd.DeclaringType.IsEnum && !fd.DeclaringType.BaseType.Name.Contains("Delegate");
        }
        public static bool checkProperty(PropertyDef pd)
        {
            return !pd.IsSpecialName && !pd.IsRuntimeSpecialName && !pd.DeclaringType.Name.Contains("AnonymousType");
        }
        public static bool checkEvent(EventDef ed)
        {
            return !ed.IsSpecialName && !ed.IsRuntimeSpecialName;
        }
        public static bool isForm(AssemblyDef asm)
        {
            bool result = false;
            foreach (ModuleDef current in asm.Modules)
            {
                foreach (TypeDef current2 in current.Types)
                {
                    foreach (TypeDef current3 in current2.NestedTypes)
                    {
                        foreach (MethodDef current4 in current3.Methods)
                        {
                            if (current4.Name == "InitializeComponent")
                            {
                                result = true;
                            }
                        }
                    }
                    foreach (MethodDef current5 in current2.Methods)
                    {
                        if (current5.Name == "InitializeComponent")
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
    }
}
