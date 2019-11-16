using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AnimGrapher
{
    class ExpressionTools
    {
        private static Expression expression_;

        #region Public functions

        public static void Init()
        {
            expression_ = new Expression();
        }

        public static double Evaluate(string expr, string param, double value)
        {
            string valueString = value.ToString(CultureInfo.GetCultureInfo("en-GB"));
            string prefix = param + ":=" + valueString;

            return expression_.Evaluate(prefix + "; " + expr);
        }

        public static bool IsExpressionValid(string expr, string var)
        {
            bool isValid = true;

            // invalid if null or empty
            if (String.IsNullOrEmpty(expr))
                return false;

            try
            {
                expression_.Parse(expr, var);
            }
            catch (Exception /*ex*/)
            {
                isValid = false;
            }

            return isValid;
        }

        #endregion
    }
}
