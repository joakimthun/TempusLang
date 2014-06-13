using Commons.AST;
using Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration
{
    public sealed class AssemblyGenerator : CodeGenerator
    {
        public AssemblyGenerator(IEnumerable<Expression> expressions, string moduleName)
        {
            if (Path.GetFileName(moduleName) != moduleName)
            {
                throw new Exception("Can only output into the current directory!");
            }

            var name = new AssemblyName(Path.GetFileNameWithoutExtension(moduleName));
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Save);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName);

            var typeGenerator = new TypeGenerator(moduleBuilder, "Program", expressions);

            moduleBuilder.CreateGlobalFunctions();
            assemblyBuilder.SetEntryPoint(typeGenerator.GetEntryPoint());
            assemblyBuilder.Save(moduleName);
        }

        
    }
}
