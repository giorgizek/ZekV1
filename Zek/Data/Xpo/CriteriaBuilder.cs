using DevExpress.Data.Filtering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zek.Data.Xpo
{
    public class CriteriaBuilder
    {
        private static CriteriaOperator getBinaryCriteria(string propertyName, object requiredValue, BinaryOperatorType operatorType)
        {
            return new BinaryOperator(propertyName, requiredValue, operatorType);
        }

        public static CriteriaOperator GetEqualCriteria(string propertyName, object requiredValue)
        {
            return getBinaryCriteria(propertyName, requiredValue, BinaryOperatorType.Equal);
        }

        public static CriteriaOperator GetNotEqualCriteria(string propertyName, object requiredValue)
        {
            return getBinaryCriteria(propertyName, requiredValue, BinaryOperatorType.NotEqual);
        }

        public static CriteriaOperator GetInCriteria(string propertyName, ICollection values)
        {
            return new InOperator(propertyName, values);
        }

        public static CriteriaOperator GetGreaterCriteria(string propertyName, object requiredValue, bool orEqual)
        {
            if (orEqual)
            {
                return getBinaryCriteria(propertyName, requiredValue, BinaryOperatorType.GreaterOrEqual);
            }
            return getBinaryCriteria(propertyName, requiredValue, BinaryOperatorType.Greater);
        }

        public static CriteriaOperator GetLessCriteria(string propertyName, object requiredValue, bool orEqual)
        {
            if (orEqual)
            {
                return getBinaryCriteria(propertyName, requiredValue, BinaryOperatorType.LessOrEqual);
            }
            return getBinaryCriteria(propertyName, requiredValue, BinaryOperatorType.Less);
        }

        public static CriteriaOperator GetLikeUpperCriteria(string propertyName, object requiredValue)
        {
            FunctionOperator operandProperty = createOperandProperty(propertyName, FunctionOperatorType.Upper);
            FunctionOperator operandValue = createOperandValue(requiredValue, FunctionOperatorType.Upper);
            return new BinaryOperator(operandProperty, operandValue, BinaryOperatorType.Like);
        }

        public static CriteriaOperator GetORCriteria(CriteriaOperatorCollection operatorCollection)
        {
            return new GroupOperator(GroupOperatorType.Or, operatorCollection);
        }

        public static CriteriaOperator GetOrCriteria(params CriteriaOperator[] operators)
        {
            CriteriaOperatorCollection operatorCollection = new CriteriaOperatorCollection();
            operatorCollection.AddRange(operators.Where(op => !Equals(op, null)));
            if (operatorCollection.Count == 0)
                return null;

            if (operatorCollection.Count == 1)
                return operatorCollection[0];

            return GetORCriteria(operatorCollection);
        }

        public static CriteriaOperator GetANDCriteria(CriteriaOperatorCollection operatorCollection)
        {
            return new GroupOperator(GroupOperatorType.And, operatorCollection);
        }

        public static CriteriaOperator GetAndCriteria(params CriteriaOperator[] operators)
        {
            CriteriaOperatorCollection operatorCollection = new CriteriaOperatorCollection();
            foreach (CriteriaOperator op in operators)
            {
                if (!Equals(op, null))
                    operatorCollection.Add(op);
            }
            if (operatorCollection.Count == 0)
                return null;

            if (operatorCollection.Count == 1)
                return operatorCollection[0];

            return GetANDCriteria(operatorCollection);
        }

        public static CriteriaOperator GetIsNullCriteria(string propertyName)
        {
            return new NullOperator(propertyName);
        }

        public static CriteriaOperator GetNotNullCriteria(string propertyName)
        {
            return new NotOperator(new NullOperator(propertyName));
        }

        private static FunctionOperator createOperandValue(object requiredValue, FunctionOperatorType operatorType)
        {
            CriteriaOperatorCollection operands = new CriteriaOperatorCollection();
            OperandValue operandValue = new OperandValue(requiredValue);
            operands.Add(operandValue);
            return new FunctionOperator(operatorType, operands);
        }

        private static FunctionOperator createOperandProperty(string property, FunctionOperatorType operatorType)
        {
            CriteriaOperatorCollection operands = new CriteriaOperatorCollection();
            OperandProperty propertyOperand = new OperandProperty(property);
            operands.Add(propertyOperand);
            return new FunctionOperator(operatorType, operands);
        }
    }
}
