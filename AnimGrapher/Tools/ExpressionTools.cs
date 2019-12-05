using System;
using System.Globalization;

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

        public static double Evaluate(string expr, string variable, double value)
        {
            string valueString = value.ToString(CultureInfo.GetCultureInfo("en-GB"));
            string prefix = variable + ":=" + valueString;

            return expression_.Evaluate(prefix + "; " + expr);
        }

        public static double Evaluate(string expr, string[] variables, double[] values)
        {
            string prefix = "";
            int index = -1;
            foreach (string variable in variables)
            {
                index++;
                double value = (double)values.GetValue(index);
                prefix += variable + ":=" + value.ToString(CultureInfo.GetCultureInfo("en-GB")) + ";";
            }

            return expression_.Evaluate(prefix + expr);
        }

        public static bool IsExpressionValid(string expr, string[] variables)
        {
            bool isValid = true;

            // invalid if null or empty
            if (String.IsNullOrEmpty(expr))
                return false;

            try
            {
                expression_.Parse(expr, variables);
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
