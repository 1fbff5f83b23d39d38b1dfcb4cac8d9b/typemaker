﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Ast.Expressions
{
	public interface INameofExpression : IExpression
	{
		IExpression Target { get; }
	}
}
