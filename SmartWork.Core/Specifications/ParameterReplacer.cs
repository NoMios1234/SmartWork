using System.Linq.Expressions;

namespace SmartWork.Core.Specifications
{
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression parameter;
        private readonly ParameterExpression replacement;

        public ParameterReplacer(ParameterExpression parameter, ParameterExpression replacement)
        {
            this.parameter = parameter;
            this.replacement = replacement;
        }
        protected override Expression VisitParameter(ParameterExpression node) => base.VisitParameter(this.parameter == node ? this.replacement : node);
    }
}