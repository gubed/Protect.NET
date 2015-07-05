using dnlib.DotNet;

namespace Protect.NET
{
    interface IProtection
    {
        string name { get; }
        string description { get; }
        void Protect();
    }
}
