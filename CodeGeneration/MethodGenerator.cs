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
    public sealed class MethodGenerator : CodeGenerator
    {
        //.NET Methods
        private const string SystemConsoleWriteLine = "WriteLine";
        private const string SystemConsoleReadLine = "ReadLine";

        private ILGenerator _ilGenerator;
        private Dictionary<string, LocalBuilder> _localTable;
        private Dictionary<string, FieldBuilder> _fieldTable;
        private Dictionary<string, MethodBuilder> _methodsTable;
        private MethodBuilder _methodBuilder;

        public MethodGenerator(IEnumerable<Expression> expressions, string name, bool isMain, TypeBuilder typeBuilder, Dictionary<string, FieldBuilder> fieldTable, Dictionary<string, MethodBuilder> methodsTable)
        {
            _methodBuilder = typeBuilder.DefineMethod(name, MethodAttributes.Static, typeof(void), Type.EmptyTypes);

            _ilGenerator = _methodBuilder.GetILGenerator();
            _localTable = new Dictionary<string, LocalBuilder>();
            _fieldTable = fieldTable;
            _methodsTable = methodsTable;

            CompileStatements(expressions);

            if (isMain)
            {
                _ilGenerator.Emit(OpCodes.Call, typeof (Console).GetMethod(SystemConsoleReadLine, BindingFlags.Public | BindingFlags.Static, null, new Type[] {}, null));
            }

            _ilGenerator.Emit(OpCodes.Ret);
        }

        public MethodBuilder GetMethod()
        {
            return _methodBuilder;
        }

        private void CompileStatements(IEnumerable<Expression> statements)
        {
            foreach (var statement in statements)
            {
                CompileStatement(statement);
            }
        }

        private void CompileStatement(Expression statement)
        {
            if (statement is VariableDeclarationExpression)
            {
                var variableDeclarationExpression = (VariableDeclarationExpression)statement;
                _localTable[variableDeclarationExpression.Identifier] = _ilGenerator.DeclareLocal(TypeOfExpression(variableDeclarationExpression.Expression));

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

                if (_localTable.ContainsKey(assignmentExpression.Identifier))
                {
                    StoreLocal(assignmentExpression.Identifier, typeOfExpression);
                }
                else if (_fieldTable.ContainsKey(assignmentExpression.Identifier))
                {
                    SetField(assignmentExpression.Identifier, typeOfExpression);
                }
                else
                {
                    throw new CodeGenerationException(string.Format("Undeclared variable: {0}", assignmentExpression.Identifier));
                }
            }
            else if (statement is PrintLineExpression)
            {
                var printLineExpression = (PrintLineExpression)statement;
                CompileExpression(printLineExpression.Expression, typeof(string));
                _ilGenerator.Emit(OpCodes.Call, typeof(System.Console).GetMethod(SystemConsoleWriteLine, new Type[] { typeof(string) }));
            }
            else if (statement is LoopExpression)
            {
                var loopExpression = (LoopExpression)statement;

                //Create an index variable
                var loopIndexExpression = GenerateLoopIndexExpression();
                CompileStatement(loopIndexExpression);

                var test = _ilGenerator.DefineLabel();
                _ilGenerator.Emit(OpCodes.Br, test);

                var loopBody = _ilGenerator.DefineLabel();
                _ilGenerator.MarkLabel(loopBody);
                CompileStatements(loopExpression.Body);

                //Increment the index variable by 1
                _ilGenerator.Emit(OpCodes.Ldloc, _localTable[loopIndexExpression.Identifier]);
                _ilGenerator.Emit(OpCodes.Ldc_I4, 1);
                _ilGenerator.Emit(OpCodes.Add);
                StoreLocal(loopIndexExpression.Identifier, typeof(int));

                _ilGenerator.MarkLabel(test);
                _ilGenerator.Emit(OpCodes.Ldloc, _localTable[loopIndexExpression.Identifier]);
                CompileExpression(loopExpression.IterationExpression, typeof(int));

                //Jump to the loopBody label if the loopIndex is less then the iteration expression.
                _ilGenerator.Emit(OpCodes.Blt, loopBody);
            }
            else if (statement is FuncInvocationExpression)
            {
                var funcInvocationExpression = (FuncInvocationExpression)statement;

                if (!_methodsTable.ContainsKey(funcInvocationExpression.Name))
                {
                    throw new CodeGenerationException(string.Format("Can not call undeclared function: {0}", funcInvocationExpression.Name));
                }

                _ilGenerator.Emit(OpCodes.Call, _methodsTable[funcInvocationExpression.Name]);
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

                if (_localTable.ContainsKey(identifier))
                {
                    _ilGenerator.Emit(OpCodes.Ldloc, _localTable[identifier]);
                }
                else if (_fieldTable.ContainsKey(identifier))
                {
                    _ilGenerator.Emit(OpCodes.Ldsfld, _fieldTable[identifier]);
                }
                else
                {
                    throw new CodeGenerationException(string.Format("Undeclared variable: {0}", identifier));
                }
            }
            else
            {
                throw new CodeGenerationException(string.Format("Expression type not yet implemented: {0}", expression.GetType().Name));
            }
        }

        private void StoreLocal(string identifier, Type type)
        {
            var localBuilder = _localTable[identifier];

            if (localBuilder.LocalType == type)
            {
                _ilGenerator.Emit(OpCodes.Stloc, _localTable[identifier]);
            }
            else
            {
                throw new CodeGenerationException(string.Format("{0} is of type {1}, can not store {2}", identifier, localBuilder.LocalType.Name, type.Name));
            }
        }

        private void SetField(string identifier, Type type)
        {
            var fieldBuilder = _fieldTable[identifier];

            if (fieldBuilder.FieldType == type)
            {
                _ilGenerator.Emit(OpCodes.Stsfld, fieldBuilder);
            }
            else
            {
                throw new CodeGenerationException(string.Format("{0} is of type {1}, can not store {2}", identifier, fieldBuilder.FieldType.Name, type.Name));
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
                if (_localTable.ContainsKey(identifierExpression.Identifier))
                {
                    var local = _localTable[identifierExpression.Identifier];
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

        private VariableDeclarationExpression GenerateLoopIndexExpression()
        {
            var variableDeclarationExpression = new VariableDeclarationExpression();
            variableDeclarationExpression.Identifier = "index";
            variableDeclarationExpression.Expression = new IntegerLiteralExpression { Value = 0 };
            return variableDeclarationExpression;
        }
    }
}
