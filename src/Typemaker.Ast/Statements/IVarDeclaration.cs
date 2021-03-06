﻿using System;
using System.Collections.Generic;
using System.Text;
using Typemaker.Ast.Statements;

namespace Typemaker.Ast
{
	public interface IVarDeclaration : IStatement, ITypedIdentifier
	{
		bool IsConst { get; }
	}
}
