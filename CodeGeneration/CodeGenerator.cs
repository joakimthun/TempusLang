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
    public sealed class CodeGenerator
    {
        //.NET Methods
        private const string SystemConsoleWriteLine = "WriteLine";
        private const string SystemConsoleReadLine = "ReadLine";

        private ILGenerator _ilGenerator;
        private Dictionary<string, LocalBuilder> _symbolTable;

        public CodeGenerator(IEnumerable<Expression> program, string moduleName)
        {
            if (Path.GetFileName(moduleName) != moduleName)
            {
                throw new System.Exception("Can only output into the current directory!");
            }

            var name = new AssemblyName(Path.GetFileNameWithoutExtension(moduleName));
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Save);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName);
            var typeBuilder = moduleBuilder.DefineType("Program");

            var methodBuilder = typeBuilder.DefineMethod("Main", MethodAttributes.Static, typeof(void), System.Type.EmptyTypes);

            _ilGenerator = methodBuilder.GetILGenerator();
            _symbolTable = new Dictionary<string, LocalBuilder>();

            GenerateIL(program);

            _ilGenerator.Emit(OpCodes.Call, typeof(System.Console).GetMethod(SystemConsoleReadLine, BindingFlags.Public | BindingFlags.Static, null, new Type[] { }, null));
            _ilGenerator.Emit(OpCodes.Ret);
            typeBuilder.CreateType();
            moduleBuilder.CreateGlobalFunctions();
            assemblyBuilder.SetEntryPoint(methodBuilder);
            assemblyBuilder.Save(moduleName);
        }

        private void GenerateIL(IEnumerable<Expression> program)
        {
            foreach (var statement in program)
            {
                CompileStatement(statement);
            }
        }

        private void CompileStatement(Expression statement)
        {
            if (statement is VariableDeclarationExpression)
            {
                var variableDeclarationExpression = (VariableDeclarationExpression)statement;
                _symbolTable[variableDeclarationExpression.Identifier] = _ilGenerator.DeclareLocal(TypeOfExpression(variableDeclarationExpression.Expression));

                var assignmentExpression = new AssignmentExpression();
                assignmentExpression.Identifier = variableDeclarationExpression.Identifier;
                assignmentExpression.Expression = variableDeclarationExpression.Expression;

                CompileStatement(assignmentExpression);
            }
            else if (statement is AssignmentExpression)
            {
                var assignmentExpression = (AssignmentExpression)statement;
                var typeOfExpression = TypeOfExpression(assignmentExpression.Expression);
                CompileExpression(assignmentExpression.Expression, typeOfExpression);
                StoreLocal(assignmentExpression.Identifier, typeOfExpression);
            }
            else if (statement is PrintLineExpression)
            {
                var printLineExpression = (PrintLineExpression)statement;
                CompileExpression(printLineExpression.Expression, typeof(string));
                _ilGenerator.Emit(OpCodes.Call, typeof(System.Console).GetMethod(SystemConsoleWriteLine, new Type[] { typeof(string) }));
            }
            else
            {
                throw new CodeGenerationException(string.Format("Statement type not yet implemented: {0}", statement.GetType().Name));
            }
        }

        private void CompileExpression(Expression expression, Type expectedType)
        {
            if (expression is StringLiteralExpression)
            {
                _ilGenerator.Emit(OpCodes.Ldstr, ((StringLiteralExpression)expression).Value);
            }
            else if (expression is IntegerLiteralExpression)
            {
                _ilGenerator.Emit(OpCodes.Ldc_I4, ((IntegerLiteralExpression)expression).Value);
            }
            else if (expression is IdentifierExpression)
            {
                var identifier = ((IdentifierExpression)expression).Identifier;

                if (!_symbolTable.ContainsKey(identifier))
                {
                    throw new CodeGenerationException(string.Format("Undeclared local variable: {0}", identifier));
                }

                _ilGenerator.Emit(OpCodes.Ldloc, _symbolTable[identifier]);
            }
            else
            {
                throw new CodeGenerationException(string.Format("Expression type not yet implemented: {0}", expression.GetType().Name));
            }
        }

        private void StoreLocal(string identifier, Type type)
        {
            if (_symbolTable.ContainsKey(identifier))
            {
                var localBuilder = _symbolTable[identifier];

                if (localBuilder.LocalType == type)
                {
                    _ilGenerator.Emit(OpCodes.Stloc, _symbolTable[identifier]);
                }
                else
                {
                    throw new CodeGenerationException(string.Format("{0} is of type {1}, can not store {2}", identifier, localBuilder.LocalType.Name, type.Name));
                }
            }
            else
            {
                throw new CodeGenerationException(string.Format("Undeclared local variable: {0}", identifier));
            }
        }

        private Type TypeOfExpression(Expression expression)
        {
            if (expression is StringLiteralExpression)
            {
                return typeof(string);
            }
            else if (expression is IntegerLiteralExpression)
            {
                return typeof(int);
            }
            else if (expression is IdentifierExpression)
            {
                var identifierExpression = (IdentifierExpression)expression;
                if (_symbolTable.ContainsKey(identifierExpression.Identifier))
                {
                    var local = _symbolTable[identifierExpression.Identifier];
                    return local.LocalType;
                }
                else
                {
                    throw new CodeGenerationException(string.Format("Undeclared local variable: {0}", identifierExpression.Identifier));
                }
            }
            else
            {
                throw new CodeGenerationException(string.Format("Unknown type: {0}", expression.GetType().Name));
            }
        }
    }
}
