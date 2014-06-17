using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Commons.AST;
using Commons.Exceptions;

namespace CodeGeneration
{
    public class TypeGenerator : CodeGenerator
    {
        private TypeBuilder _typeBuilder;
        private MethodBuilder _main;
        private Dictionary<string, FieldBuilder> _fieldTable;
        private Dictionary<string, MethodBuilder> _methodsTable;

        public TypeGenerator(ModuleBuilder moduleBuilder, string name, IEnumerable<Expression> expressions)
        {
            _typeBuilder = moduleBuilder.DefineType(name);
            _fieldTable = new Dictionary<string, FieldBuilder>();
            _methodsTable = new Dictionary<string, MethodBuilder>();

            CreateType(expressions);
            _typeBuilder.CreateType();
        }

        public MethodBuilder GetEntryPoint()
        {
            if (_main == null)
            {
                throw new CodeGenerationException("No entry point defined.");
            }

            return _main;
        }

        private void CreateType(IEnumerable<Expression> expressions)
        {
            foreach (var expression in expressions)
            {
                CompileStatement(expression);
            }
        }

        private void CompileStatement(Expression statement)
        {
            if (statement is FieldDeclarationExpression)
            {
                var fieldDeclarationExpression = (FieldDeclarationExpression)statement;
                DefineField(fieldDeclarationExpression.Identifier, fieldDeclarationExpression.Type);
            }
            else if (statement is FuncDeclarationExpression)
            {
                var funcDeclarationExpression = (FuncDeclarationExpression) statement;
                var methodGenerator = new MethodGenerator(funcDeclarationExpression, funcDeclarationExpression.Name, funcDeclarationExpression.IsMain, _typeBuilder, _fieldTable, _methodsTable);

                if (funcDeclarationExpression.IsMain)
                {
                    if (_main != null)
                    {
                        throw new CodeGenerationException("Entry point already defined.");
                    }

                    _main = methodGenerator.GetMethod();
                }

                _methodsTable[funcDeclarationExpression.Name] = methodGenerator.GetMethod();
            }
            else
            {
                throw new CodeGenerationException(string.Format("Statement type not yet implemented: {0}", statement.GetType().Name));
            }
        }

        private void DefineField(string identifier, Type type)
        {
            if (_fieldTable.ContainsKey(identifier))
            {
                throw new CodeGenerationException("A field with the same name has already been defined");
            }

            var fieldBuilder = _typeBuilder.DefineField(identifier, type, FieldAttributes.Public | FieldAttributes.Static);

            _fieldTable[identifier] = fieldBuilder;
        }

        private Type TypeOfFieldExpression(Expression expression)
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
                if (_fieldTable.ContainsKey(identifierExpression.Identifier))
                {
                    var field = _fieldTable[identifierExpression.Identifier];
                    return field.FieldType;
                }
                else
                {
                    throw new CodeGenerationException(string.Format("Undeclared field: {0}", identifierExpression.Identifier));
                }
            }
            else
            {
                throw new CodeGenerationException(string.Format("Unknown type: {0}", expression.GetType().Name));
            }
        }

        private object GetExpressionValue(Expression expression)
        {
            if (expression is StringLiteralExpression)
            {
                var stringLiteralExpression = (StringLiteralExpression) expression;
                return stringLiteralExpression.Value;
            }
            else if (expression is IntegerLiteralExpression)
            {
                var integerLiteralExpression = (IntegerLiteralExpression)expression;
                return integerLiteralExpression.Value;
            }
            else
            {
                throw new CodeGenerationException(string.Format("Unknown type: {0}", expression.GetType().Name));
            }
        }
    }
}
