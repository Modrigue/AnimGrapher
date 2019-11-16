using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AleProjects.AleLexer.AleParser;
using AleProjects.AleLexer;

namespace AnimGrapher
{
    public partial class Expression
    {
        private AleExpressionParser AP = new AleExpressionParser();

        Random rnd = new Random();

        //variables for calculations

        //strictly typed variables
        double X = 2.7182818284590452353602874713527;
        decimal Money = 100M;

        //all other dynamic variables
        Dictionary<string, object> Variables;

        public Expression()
        {
            AP.IgnoreCase = true;

            AP.InitOperations(AleExpressionParser.OPERATIONS_STANDARDSET);
            //AP.InitOperations(AleExpressionParser.OPERATIONS_CLIKESET);

            InitMyVariables(AP.IgnoreCase);
            InitMyOperations();
        }

        private void InitMyVariables(bool ignoreCase)
        {
            Variables = new Dictionary<string, object>(ignoreCase ? StringComparer.CurrentCultureIgnoreCase : StringComparer.CurrentCulture);

            Variables.Add("S", "AleExpressionParser 1.0 beta");
            Variables.Add("i", (int)1);
            Variables.Add("i64", (UInt64)1);
            Variables.Add("b", false);
            Variables.Add("φ", Math.PI / 3);
            Variables.Add("Date", new DateTime(2012, 12, 21));

            Variables.Add("Matrix1", new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            Variables.Add("Matrix2", new int[,] { { 10, 11, 12, 13 }, { 14, 15, 16, 20 }, { 27, 28, 29, 40 } });

            Dictionary<string, ChemicalElement> elements = new Dictionary<string, ChemicalElement>();
            elements.Add("H", new ChemicalElement("H", "Hydrogen", 1.008, "1s1"));
            elements.Add("He", new ChemicalElement("He", "Helium", 4.002602, "1s2"));
            elements.Add("Li", new ChemicalElement("Li", "Lithium", 6.94, "2s1"));
            elements.Add("C", new ChemicalElement("C", "Carbon", 14.007, "2s2 2p3"));
            elements.Add("N", new ChemicalElement("N", "Nitrogen", 14.007, "2s2 2p3"));
            elements.Add("O", new ChemicalElement("O", "Oxygen", 15.99, "2s2 2p4"));

            Variables.Add("Elements", elements);
        }

        private void InitMyOperations()
        {

            AP.AddOperation(new AleOperation(AP.IgnoreCase ? "ITEM" : "Item")
            {
                /* 'Parameters' property describes arguments of operation (function).
                 * Number of Tuples in list defines number of arguments of function.
                 * Each Tuple describes each argument of function.
                 * First item of Tuple describes type of argument for "run-time" checking.
                 * TypeCode.Object value of this item means that any object can be passed as argument and it is concern of 'Evaluator' delegate to check type of passed object and correctly use it.
                 * Second item defines default value for argument. When it is null - argument is required.
                 * When it is not null - argument is not required and second item is a default value passed instead.  */ 
                Parameters = new List<Tuple<TypeCode, object>>() { new Tuple<TypeCode, object>(TypeCode.Int32, null) },

                /* 'ObjectTypeCode' defines if operation (function) is a class method. 
                 * Default value for 'ObjectTypeCode' set by constructor is TypeCode.Empty. It means that function is not a class method.
                 * 'ObjectTypeCode' is used by parser to correctly select operation and during evaluation for "run-time" checking of class instance passed to delegate in 'parameters' argument.
                 * 'ObjectTypeCode' can be TypeCode.Object. It means that class instance of any type can be passed to delegate. */
                InstanceTypeCode = TypeCode.Object,

                /* delegate which is called to evaluate operation.
                 * 'term' is a element of a parsed expression tree.
                 * 'parameters' is a structure with operation arguments. When number of arguments is 3 or less, they are passed in 'FirstParam', 'SecondParam' and 'ThirdParam' fields.
                 * When number of arguments is 4 or more, they are passed in 'Parameters' field (List<object>)
                 * Store result of evaluation or information about error in 'result' structure */
                Evaluator = delegate(AleTerm term, ref OperationEvalParameters parameters, ref AleTermResult result)
                {
                    Dictionary<string, ChemicalElement> elements = parameters.ClassInstance as Dictionary<string, ChemicalElement>;

                    if (elements != null)
                    {
                        int i = 0;
                        int k = Convert.ToInt32(parameters.FirstParam);

                        foreach (KeyValuePair<string, ChemicalElement> element in elements)
                        {
                            if (i == k)
                            {
                                result.Value = element.Value;
                                return true;
                            }
                            i++;
                        }

                        result.SetError(AleTermResult.ERROR_KEYNOTFOUND, term.Token.StartInOrigin);
                    }
                    else result.SetError(AleTermResult.ERROR_UNKNOWNMETHOD, term.Token.StartInOrigin);

                    return false;
                }
            });

            AP.AddOperation(new AleOperation(AP.IgnoreCase ? "RAND" : "Rand")
            {
                Parameters = new List<Tuple<TypeCode, object>>() { new Tuple<TypeCode, object>(TypeCode.Double, Double.MinValue), new Tuple<TypeCode, object>(TypeCode.Double, Double.MaxValue) },
                Evaluator = delegate(AleTerm term, ref OperationEvalParameters parameters, ref AleTermResult result)
                {
                    //if (parameters.ActualParamsCount == 1) result.Value = rnd.Next(Convert.ToInt32(parameters.FirstParam));
                    //else if (parameters.ActualParamsCount == 2) result.Value = rnd.Next(Convert.ToInt32(parameters.FirstParam), Convert.ToInt32(parameters.SecondParam));
                    //else result.Value = rnd.Next();

                    if (parameters.ActualParamsCount == 1) result.Value = Convert.ToDouble(parameters.FirstParam) * rnd.NextDouble();
                    else if (parameters.ActualParamsCount == 2)
                    {
                        double a = Convert.ToDouble(parameters.FirstParam);
                        double b = Convert.ToDouble(parameters.SecondParam);
                        result.Value = (a - b) * rnd.NextDouble() + b;
                    }
                    else result.Value = rnd.NextDouble();

                    return true;
                }
            });

            AP.AddOperation(new AleOperation(AP.IgnoreCase ? "IIF" : "Iif")
            {
                Parameters = new List<Tuple<TypeCode, object>>() { new Tuple<TypeCode, object>(TypeCode.Boolean, null), new Tuple<TypeCode, object>(TypeCode.Object, null), 
                                                                   new Tuple<TypeCode, object>(TypeCode.Object, null) },
                Evaluator = delegate(AleTerm term, ref OperationEvalParameters parameters, ref AleTermResult result)
                {
                    if ((bool)parameters.FirstParam) result.Value = parameters.SecondParam; else result.Value = parameters.ThirdParam;
                    return true;
                }
            });

            // the "↑" power operator. x↑y means Math.Pow(x,y), x↑ means Math.Pow(x,2)
            // this overloaded constructor creates operator (not function) description. First argument is a operator literal, second is a precedence of operator, third is an associativity
            AP.AddOperation(new AleOperation("↑", 2000, AleOperation.OPERATOR_YFX + AleOperation.OPERATOR_YF)
            {
                // there is no need to initialize Parameters field for operators. It is not used for operators evaluation. Only for functions.

                Evaluator = delegate(AleTerm term, ref OperationEvalParameters parameters, ref AleTermResult result)
                {
                    AleTerm t1 = term[0];

                    AleTermResult a;
                    if (!t1.Evaluate(out a, OnEvaluate, OnAssign) && result.SetError(a.ErrorCode, a.ErrorPos)) return false;
                    
                    AleTermResult b = new AleTermResult();
                    if (term.Count == 2)
                    {
                        AleTerm t2 = term[1];
                        if (!t2.Evaluate(out b, OnEvaluate, OnAssign) && result.SetError(b.ErrorCode, b.ErrorPos)) return false;
                    }
                    else b.Value = 2.0;

                    TypeCode optype = AleTerm.OperationType(a.Value, b.Value);

                    switch (optype)
                    {
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                        case TypeCode.UInt64:
                        case TypeCode.Int64:
                        case TypeCode.UInt32:
                        case TypeCode.Int32:
                            result.Value = Math.Pow(Convert.ToDouble(a.Value), Convert.ToDouble(b.Value));
                            return true;
                        default:
                            result.SetError(AleTermResult.ERROR_INCOMPATIBLETYPES, term.Token.StartInOrigin);
                            return false;
                    }
                }
            });

            //// "❗" factorial operator (\u2757 char). 
            //AP.AddOperation(new AleOperation("❗", 1800, AleOperation.OPERATOR_YF)
            // "!" factorial operator
            AP.AddOperation(new AleOperation("!", 1800, AleOperation.OPERATOR_YF)
            {
                Evaluator = delegate(AleTerm term, ref OperationEvalParameters parameters, ref AleTermResult result)
                {
                    AleTerm t1 = term[0];

                    AleTermResult a;
                    if (!t1.Evaluate(out a, OnEvaluate, OnAssign) && result.SetError(a.ErrorCode, a.ErrorPos)) return false;

                    // check if integer value
                    //if (!AleTerm.ValidForOperationType(a.Value, TypeCode.Int32) && result.SetError(AleTermResult.ERROR_INCOMPATIBLETYPES, term.Token.StartInOrigin)) return false;
                    double nDouble = Convert.ToDouble(a.Value);
                    if (nDouble != Math.Floor(nDouble))
                    {
                        result.SetError(AleTermResult.ERROR_INCOMPATIBLETYPES, term.Token.StartInOrigin);
                        return false;
                    }

                    // check if allowed value
                    int n = Convert.ToInt32(a.Value);
                    if (n < 1 && result.SetError(AleTermResult.ERROR_EVALUATION, term.Token.StartInOrigin)) return false;

                    double fac_n = 1;

                    while (n > 1)
                    {
                        fac_n *= n;
                        n--;
                    }

                    result.Value = fac_n;
                    return true;
                }
            });

            // disabled: not functional
            //// "²" square operator
            //AP.AddOperation(new AleOperation("²", 1800, AleOperation.OPERATOR_YF)
            //{
            //    Evaluator = delegate(AleTerm term, ref OperationEvalParameters parameters, ref AleTermResult result)
            //    {
            //        AleTerm t1 = term[0];

            //        AleTermResult a;
            //        if (!t1.Evaluate(out a, OnEvaluate, OnAssign) && result.SetError(a.ErrorCode, a.ErrorPos)) return false;

            //        TypeCode optype = AleTerm.OperationType(a.Value, null);

            //        switch (optype)
            //        {
            //            case TypeCode.Double:
            //                result.Value = Math.Pow(Convert.ToDouble(a.Value), 2);
            //                return true;
            //            case TypeCode.Decimal:
            //                result.Value = Convert.ToDecimal(Math.Pow(Convert.ToDouble(a.Value), 2));
            //                return true;
            //            case TypeCode.UInt64:
            //                result.Value = Convert.ToUInt64(Math.Pow(Convert.ToUInt64(a.Value), 2));
            //                return true;
            //            case TypeCode.Int64:
            //                result.Value = Convert.ToInt64(Math.Pow(Convert.ToInt64(a.Value), 2));
            //                return true;
            //            case TypeCode.UInt32:
            //                result.Value = Convert.ToUInt32(Math.Pow(Convert.ToUInt32(a.Value), 2));
            //                return true;
            //            case TypeCode.Int32:
            //                result.Value = Convert.ToInt32(Math.Pow(Convert.ToInt32(a.Value), 2));
            //                return true;
            //            default:
            //                result.SetError(AleTermResult.ERROR_INCOMPATIBLETYPES, term.Token.StartInOrigin);
            //                return false;
            //        }
            //    }
            //});

            AP.AddOperation(new AleOperation(AP.IgnoreCase ? "REVERSE" : "Reverse")
           {
               // Parameters = null - method has no parameters
               InstanceTypeCode = TypeCode.String,
               Evaluator = delegate(AleTerm term, ref OperationEvalParameters parameters, ref AleTermResult result)
               {
                   StringBuilder s = new StringBuilder(parameters.ClassInstance.ToString());
                   int n = s.Length;
                   int m = (n--) / 2;
                   char c;

                   for (int i = 0; i < m; i++)
                   {
                       c = s[i];
                       s[i] = s[n - i];
                       s[n - i] = c;
                   }

                   result.Value = s;
                   return true;
               }
           });
        }

        private void OnSemanticsValidate(object sender, SemanticsValidateEventArgs e)
        {
            bool ignoreCase = AP.IgnoreCase;
            AleExpressionParser P = sender as AleExpressionParser;

            // We want for some reasons that "Rnd" can't be a variable name
            if ((e.Term.HashCode == -1028829980 && !ignoreCase && e.Term.Value.ToString() == "Rnd") ||
                (e.Term.HashCode == 1843313028 && ignoreCase && e.Term.Value.ToString() == "RND"))
            {
                AleTerm parent = e.Term.Parent;
                if (e.Term.TypeOfTerm == AleTermType.Variable && (parent == null || parent.Operation == null || !parent.Operation.IsClassOperator || e.Term != parent[1]))
                    P.SetError(e.Term.Token.StartInOrigin, AleExpressionParser.ERROR_INVALIDVARIABLE);
            }
        }

        // evaluation
        private void OnEvaluate(AleTerm term, AleTermEvaluateArgs e)
        {
            bool ignoreCase = AP.IgnoreCase;
            object res;

            /* VARIABLES
             * e.Instance is null
             * e.Name - name of variable (in uppercase when we ignore case)
             * e.NameHash - hash of e.Name (obtained by GetHashCode())
             * e.Indexes is null when we evaluate variable
             * e.Result - stores value of variable
            */
            if (e.Instance == null)
            {
                // variables stored in X and Money
                if (e.NameHash == -842352648 && e.Name == "X") e.Result = X;
                else if ((e.NameHash == 1602047654 && !ignoreCase && e.Name == "Money") || (e.NameHash == 2068757350 && ignoreCase && e.Name == "MONEY")) e.Result = Money;
                else
                {
                    // variables stored in Variables collection
                    if (Variables.TryGetValue(e.Name, out res)) e.Result = res;
                    else
                    {
                        res = (int)0;
                        Variables.Add(e.Name, res);
                        e.Result = res;
                    }
                }

                return;
            }

            /* MEMBERS OF CLASSES (class operator '.')
             * e.Instance - instance of class.
             * e.Name - name of class property (in uppercase when we ignore case)
             * e.NameHash - hash of e.Name (obtained by GetHashCode())
             * e.Indexes is null when we evaluate class property
             * e.Result - stores value of property or result of method
            */
            if (!String.IsNullOrEmpty(e.Name))
            {

                if (e.Instance is Dictionary<string, ChemicalElement>)  // Count of ChemicalElements in collection (and other properties here)
                {
                    Dictionary<string, ChemicalElement> elements = e.Instance as Dictionary<string, ChemicalElement>;

                    if ((e.NameHash == 2002596872 && !ignoreCase && e.Name == "Count") || (e.NameHash == -1825660792 && ignoreCase && e.Name == "COUNT")) e.Result = elements.Count;
                    else e.SetError(AleTermResult.ERROR_UNKNOWNPROPERTY, term.Token.StartInOrigin);
                }
                else if (e.Instance is ChemicalElement)  // ChemicalElement properties
                {
                    ChemicalElement element = e.Instance as ChemicalElement;

                    if ((e.NameHash == 1644092087 && !ignoreCase && e.Name == "Symbol") || (e.NameHash == 2104510327 && ignoreCase && e.Name == "SYMBOL")) e.Result = element.Symbol;
                    else if ((e.NameHash == 452724692 && !ignoreCase && e.Name == "Weight") || (e.NameHash == 917337236 && ignoreCase && e.Name == "WEIGHT")) e.Result = element.Weight;
                    else if ((e.NameHash == 62725275 && !ignoreCase && e.Name == "Name") || (e.NameHash == 462326075 && ignoreCase && e.Name == "NAME")) e.Result = element.Name;
                    else if ((e.NameHash == 1405338047 && !ignoreCase && e.Name == "ElectronConfig") || (e.NameHash == -641811116 && ignoreCase && e.Name == "ELECTRONCONFIG")) e.Result = element.ElectronConfig;
                    else e.SetError(AleTermResult.ERROR_UNKNOWNPROPERTY, term.Token.StartInOrigin);
                }

                return;
            }


            /* ELEMENTS OF ARRAYS AND COLLECTIONS (index operator)
             * e.Instance - instance of array or collection.
             * e.Name == ""
             * e.NameHash == 0
             * e.Indexes is List<object> with indexes of array or keys of collection
             * e.Result - stores value of member
            */

            // elements of dictionary <string, ChemicalElement>
            if (e.Instance is Dictionary<string, ChemicalElement>)
            {
                if (e.Indexes.Count != 1 && e.SetError(AleTermResult.ERROR_INVALIDKEYS, term.Token.StartInOrigin)) return;

                Dictionary<string, ChemicalElement> elements = e.Instance as Dictionary<string, ChemicalElement>;
                ChemicalElement elem;
                if (elements.TryGetValue(e.Indexes[0].ToString(), out elem)) e.Result = elem;
                else e.SetError(AleTermResult.ERROR_KEYNOTFOUND, term.Token.StartInOrigin);

                return;
            }

            Type typ = e.Instance.GetType();

            // elements of int[,] (matrix1, matrix2)
            if (typ.IsArray && typ.FullName == "System.Int32[,]")
            {
                if (e.Indexes.Count != 2 && e.SetError(AleTermResult.ERROR_INVALIDINDEXES, term.Token.StartInOrigin)) return;

                int[,] Matrix = e.Instance as int[,];
                try
                {
                    e.Result = Matrix[Convert.ToInt32(e.Indexes[0]), Convert.ToInt32(e.Indexes[1])];
                }
                catch
                {
                    e.SetError(AleTermResult.ERROR_INVALIDINDEXES, term.Token.StartInOrigin);
                }

                return;
            }

        }

        // assignment
        private void OnAssign(AleTerm term, AleTermAssignArgs e)
        {
            bool ignoreCase = AP.IgnoreCase;

            /* VARIABLES
             * e.Instance is null.
             * e.Name - name of variable (in uppercase when we ignore case)
             * e.NameHash - hash of e.Name (obtained by GetHashCode())
             * e.Indexes is null
             * e.Value - value that we assign
            */
            if (e.Instance == null)
            {
                // variables X and Money
                if (e.NameHash == -842352648 && e.Name == "X") X = Convert.ToDouble(e.Value);
                else if ((e.NameHash == 1602047654 && !ignoreCase && e.Name == "Money") || (e.NameHash == 2068757350 && ignoreCase && e.Name == "MONEY")) Money = Convert.ToDecimal(e.Value);
                else Variables[e.Name] = e.Value; // variables stored in Variables collection

                return;
            }

            /* MEMBERS OF CLASSES (class operator '.')
             * e.Instance - instance of class.
             * e.Name - name of class property (in uppercase when we ignore case)
             * e.NameHash - hash of e.Name (obtained by GetHashCode())
             * e.Indexes is null
             * e.Value - value that we assign
            */
            if (!String.IsNullOrEmpty(e.Name))
            {
                if (e.Instance is ChemicalElement)
                {
                    ChemicalElement element = e.Instance as ChemicalElement;

                    if ((e.NameHash == 1644092087 && !ignoreCase && e.Name == "Symbol") || (e.NameHash == 2104510327 && ignoreCase && e.Name == "SYMBOL")) element.Symbol = e.Value.ToString();
                    else if ((e.NameHash == 452724692 && !ignoreCase && e.Name == "Weight") || (e.NameHash == 917337236 && ignoreCase && e.Name == "WEIGHT")) element.Weight = Convert.ToDouble(e.Value);
                    else if ((e.NameHash == 62725275 && !ignoreCase && e.Name == "Name") || (e.NameHash == 462326075 && ignoreCase && e.Name == "NAME")) element.Name = e.Value.ToString();
                    else if ((e.NameHash == 1405338047 && !ignoreCase && e.Name == "ElectronConfig") || (e.NameHash == -641811116 && ignoreCase && e.Name == "ELECTRONCONFIG")) element.ElectronConfig = e.Value.ToString();
                    else e.SetError(AleTermResult.ERROR_UNKNOWNPROPERTY, term.Token.StartInOrigin);

                    return;
                }

                // elements of dictionary <string, object> (json-like object constant assigned to some variable before)
                if (e.Instance is Dictionary<string, object>)
                {
                    Dictionary<string, object> dict = e.Instance as Dictionary<string, object>;
                    dict[e.Name] = e.Value; // this may add new properties to object

                    return;
                }

                e.SetError(AleTermResult.ERROR_UNKNOWNELEMENT, term.Token.StartInOrigin);
                return;
            }


            /* ELEMENTS OF ARRAYS AND COLLECTIONS (index operator)
             * e.Instance - instance of array or collection.
             * e.Name == ""
             * e.NameHash == 0
             * e.Indexes - list<object> with indexes or keys
             * e.Value - value that we assign
            */

            Type typ = e.Instance.GetType();

            // elements of dictionary <string, ChemicalElement> (Variables["Elements"] member)
            if (e.Instance is Dictionary<string, ChemicalElement>)
            {
               if (e.Indexes.Count != 1 && e.SetError(AleTermResult.ERROR_INVALIDKEYS, term.Token.StartInOrigin)) return;

                Dictionary<string, ChemicalElement> elements = e.Instance as Dictionary<string, ChemicalElement>;
                elements[e.Indexes[0].ToString()] = e.Value as ChemicalElement;

                return;
            }

            // elements of array constant (initialization list) assigned to some variable before
            if (e.Instance is Dictionary<object, object>)
            {
                Dictionary<object, object> list = e.Instance as Dictionary<object, object>;
                if (e.Indexes.Count == 1)
                {
                    // set value to some element of array
                    if (AleTerm.ValidForOperationType(e.Indexes[0], TypeCode.Int32)) list[Convert.ToInt32(e.Indexes[0])] = e.Value; else list[e.Indexes[0]] = e.Value;
                }
                else if (e.Indexes.Count == 0 && (term.Parser.Options & AleExpressionParser.OPTION_ALLOWEMPTYINDEX) != 0)
                {
                    // add new element to array (MyArray[] = newValue)
                    int MaxKey = Int32.MinValue;

                    foreach (KeyValuePair<object, object> kv in list)
                        if (kv.Key is Int32 && (int)kv.Key > MaxKey) MaxKey = (int)kv.Key;

                    if (MaxKey < Int32.MaxValue) list[MaxKey + 1] = e.Value;
                    else e.SetError(AleTermResult.ERROR_INVALIDINDEXES, term.Token.StartInOrigin);
                }
                else e.SetError(AleTermResult.ERROR_INVALIDINDEXES, term.Token.StartInOrigin);

                return;
            }

            // elements of int[,] (Matrix1, Matrix2)
            if (typ.IsArray && typ.FullName == "System.Int32[,]")
            {
                if (e.Indexes.Count != 2 && e.SetError(AleTermResult.ERROR_INVALIDINDEXES, term.Token.StartInOrigin)) return;
                if (!AleTerm.ValidForOperationType(e.Value, TypeCode.Int32) && 
                    e.SetError(AleTermResult.ERROR_INCOMPATIBLETYPES, term.Token.StartInOrigin)) return;

                int[,] Matrix = e.Instance as int[,];

                try
                {
                    Matrix[Convert.ToInt32(e.Indexes[0]), Convert.ToInt32(e.Indexes[1])] = Convert.ToInt32(e.Value);
                }
                catch
                {
                    e.SetError(AleTermResult.ERROR_INVALIDINDEXES, term.Token.StartInOrigin);
                }

                return;
            }

            e.SetError(AleTermResult.ERROR_UNKNOWNELEMENT, term.Token.StartInOrigin);
        }

        private void ShowTokens(List<AleToken> list)
        {
            if (list == null) return;

            string s = "";
            int n = list.Count;

            for (int i = 0; i < n; i++) 
                if (list[i] != null) s += list[i].DebugPrint() + "\u000d\u000a"; else s += "<Null>\u000d\u000a";

            //txtResult.Text += "--> List of tokens\u000d\u000a" + s + "--> End of list of tokens\u000d\u000a\u000d\u000a";
        }

        private void ShowRPNList(List<AleToken> list)
        {
            if (list == null) return;

            string s = "";
            int n = list.Count;

            for (int i = 0; i < n; i++)
                if (list[i] != null) 
                    s += "Name=" + list[i].Name() + ": Start=" + list[i].StartInOrigin.ToString() + " : Length=" + list[i].LengthInOrigin.ToString() +
                         " : Elements=" + list[i].SubElementsCount.ToString() + " : Value=" + (list[i].Value == null ? "<null>" : list[i].Value) +
                         " : SubElements=" + (list[i].SubElements != null ? "<object>" : "<null>") + "\u000d\u000a";
                else s += "<Null>\u000d\u000a";

            //txtResult.Text += "--> Reverse Polish Notation\u000d\u000a" + s + "--> End of Reverse Polish Notation\u000d\u000a\u000d\u000a";
        }

        private void ShowError(AleExpressionParser AP)
        {
            //string err = AP.ErrorMessage() + " in line:" + AP.ErrorLine + " col:" + AP.ErrorCol + "\u000d\u000a";
            string err = AP.ErrorMessage() + " at position " + AP.ErrorCol + ".";
            throw new Exception(err);

            //txtResult.Text = "Error '" + AP.ErrorMessage() + "' in line:" + AP.ErrorLine + " col:" + AP.ErrorCol + "\u000d\u000a";
            //txtExpression.SelectionStart = AP.ErrorPosition;
            //txtExpression.SelectionLength = 1;
            //txtExpression.Focus();
        }

        public void Parse(string expr, string param)
        {
            //string S;
            List<AleToken> list, rpn_list;
            AleTerm T;

            //lblInfo.Text = "";

            AP.Options = (AP.IgnoreCase ? AleExpressionParser.OPTION_IGNORECASE : 0) +
                (/*chkStrictSyntax.Checked ?*/ AleExpressionParser.OPTION_STRICTSYNTAX /*: 0*/) +
                AleExpressionParser.OPTION_ALLOWEMPTYPARAMS +
                AleExpressionParser.OPTION_ALLOWEMPTYINDEX +
                AleExpressionParser.OPTION_ALLOWMULTIDIMINDEXES +
                AleExpressionParser.OPTION_STRICTINDEXES;

            AP.Text = expr;
            AP.VarPrefix = /*cboPrefix.Text == "" ?*/ '\0' /*: cboPrefix.Text[0]*/;
            AP.EndOfExpression = "; 'end of expression' 'stoṕ herẽ'";

            AP.Constants = new Dictionary<string, object>(AP.IgnoreCase ? StringComparer.CurrentCultureIgnoreCase : StringComparer.CurrentCulture);
            AP.Constants.Add("true", true);
            AP.Constants.Add("false", false);
            AP.Constants.Add("pi", Math.PI);
            AP.Constants.Add("π", Math.PI);
            //AP.Constants.Add("2pi", 2 * Math.PI);
            //AP.Constants.Add("2π", 2 * Math.PI);
            AP.Constants.Add("e", Math.E);
            AP.Constants.Add("null", null);

            AP.SemanticsValidate += OnSemanticsValidate;

            int res = 0;

            while (res < AP.Text.Length)
            {
                res = AP.Tokenize(out list, res);

                if (AP.ErrorCode == AleExpressionParser.ERROR_OK)
                {
                    T = AP.Parse(list);

                    if (AP.ErrorCode == AleExpressionParser.ERROR_OK)
                    {
                        ShowTokens(list);

                        rpn_list = AP.ToReversePolishNotation(list); // this is for demonstation only. there is no need to call AP.ToReversePolishNotation for parsing and evaluation
                        ShowRPNList(rpn_list);

                        if (T != null)
                        {
                            //txtResult.Text += "--> Prefix form \u000d\u000a" + T.DebugPrint() + "\u000d\u000a--> End of prefix form\u000d\u000a\u000d\u000a";
                            //S = "";

                            List<Tuple<int, string>> vars = T.Variables;
                            
                            // list unknown variables if existing
                            List<string> unknownVars = new List<string>();
                            foreach (Tuple<int, string> hashVar in vars)
                            {
                                string variable = hashVar.Item2;

                                // fetch expected parameter
                                if (variable.ToUpper() == param.ToUpper())
                                    continue;

                                // unknown variable
                                unknownVars.Add(variable);
                            }

                            if (unknownVars.Count > 0)
                                throw new Exception("Unknown variables");

                            //foreach (Tuple<int, string> variable in vars) S += "Hash=" + variable.Item1 + "; Name=" + variable.Item2 + "\u000d\u000a";
                            //txtResult.Text += "--> Variables list\u000d\u000a" + S + "--> End of variables list\u000d\u000a\u000d\u000a";
                        }
                    }
                    else
                        throw new Exception("Invalid expression");
                }
                else
                {
                    ShowError(AP);
                    break;
                }
            }
        }

        public double Evaluate(string expr)
        {
            List<AleToken> list;
            AleTerm T = null;
            
            //lblInfo.Text = "";

            AP.Options = (AP.IgnoreCase ? AleExpressionParser.OPTION_IGNORECASE : 0) +
                AleExpressionParser.OPTION_ALLOWEMPTYLISTMEMBER +
                (/*chkStrictSyntax.Checked ?*/ AleExpressionParser.OPTION_STRICTSYNTAX /*: 0*/) +
                AleExpressionParser.OPTION_ALLOWEMPTYPARAMS +
                AleExpressionParser.OPTION_ALLOWEMPTYINDEX +
                AleExpressionParser.OPTION_ALLOWMULTIDIMINDEXES +
                AleExpressionParser.OPTION_STRICTINDEXES;

            AP.Text = expr;
            AP.VarPrefix = /*cboPrefix.Text == "" ?*/ '\0' /*: cboPrefix.Text[0]*/;
            AP.EndOfExpression = "; 'end of expression' 'stoṕ herẽ'";

            AP.Constants = new Dictionary<string, object>(AP.IgnoreCase ? StringComparer.CurrentCultureIgnoreCase : StringComparer.CurrentCulture);
            AP.Constants.Add("true", true);
            AP.Constants.Add("false", false);
            AP.Constants.Add("pi", Math.PI);
            AP.Constants.Add("π", Math.PI);
            //AP.Constants.Add("2pi", 2 * Math.PI);
            //AP.Constants.Add("2π", 2 * Math.PI);
            AP.Constants.Add("e", Math.E);
            AP.Constants.Add("null", null);

            AP.SemanticsValidate += OnSemanticsValidate;

            int res = 0;
            AleTermResult val;

            while (res < AP.Text.Length)
            {
                res = AP.Tokenize(out list, res);

                if (AP.ErrorCode == AleExpressionParser.ERROR_OK) T = AP.Parse(list);

                if (AP.ErrorCode != AleExpressionParser.ERROR_OK)
                {
                    // invalid expression
                    ShowError(AP);
                }

                if (T != null)
                {
                    if (!T.Evaluate(out val, OnEvaluate, OnAssign))
                    {
                        // undefined value
                        throw new Exception("undefined");

                        //string err = "Error \"" + val.ErrorMessage() + "\" in line:" + AP.ErrorPosToLine(val.ErrorPos).ToString() + " col:" + AP.ErrorPosToCol(val.ErrorPos).ToString() + "\u000d\u000a";
                        //txtExpression.SelectionStart = val.ErrorPos;
                        //txtExpression.SelectionLength = 1;
                        //txtExpression.Focus();
                    }
                    //else if (val.Value != null) txtResult.Text += val.Value.ToString() + " : expression result type = " + val.Value.GetType().ToString() + "\u000d\u000a";
                    //else txtResult.Text += "<null>\u000d\u000a";

                    //TypeCode vType = Type.GetTypeCode(val.Value.GetType());
                    //if (vType == TypeCode.Boolean)
                    //    continue;

                    if (res == AP.Text.Length) // end of expression reached
                        return Convert.ToDouble(val.Value.ToString());
                }
            }

            return 0;
        }

        private void btnPerfomance_Click(object sender, EventArgs e)
        {
            List<AleToken> list;
            List<AleTerm> Terms = new List<AleTerm>();
            AleTerm T = null;

            /*
            lblInfo.Text = "";
            txtResult.Text = "";

            AP.Options = (AP.IgnoreCase ? AleSimpleLexer.OPTION_IGNORECASE : 0) +
                AleExpressionParser.OPTION_ALLOWEMPTYLISTMEMBER +
                (chkStrictSyntax.Checked ? AleExpressionParser.OPTION_STRICTSYNTAX : 0) +
                AleExpressionParser.OPTION_ALLOWEMPTYPARAMS +
                AleExpressionParser.OPTION_ALLOWEMPTYINDEX +
                AleExpressionParser.OPTION_ALLOWMULTIDIMINDEXES +
                AleExpressionParser.OPTION_STRICTINDEXES;

            AP.Text = expr_;
            AP.VarPrefix = cboPrefix.Text == "" ? '\0' : cboPrefix.Text[0];
            AP.EndOfExpression = "; 'end of expression' 'stoṕ herẽ'";
            */

            AP.Constants = new Dictionary<string, object>(AP.IgnoreCase ? StringComparer.CurrentCultureIgnoreCase : StringComparer.CurrentCulture);
            AP.Constants.Add("true", true);
            AP.Constants.Add("false", false);
            AP.Constants.Add("pi", Math.PI);
            AP.Constants.Add("π", Math.PI);
            //AP.Constants.Add("2pi", 2 * Math.PI);
            //AP.Constants.Add("2π", 2 * Math.PI);
            AP.Constants.Add("e", Math.E);
            AP.Constants.Add("null", null);

            AP.SemanticsValidate += OnSemanticsValidate;

            int res = 0;

            while (res < AP.Text.Length)
            {
                res = AP.Tokenize(out list, res);

                if (AP.ErrorCode == AleExpressionParser.ERROR_OK) T = AP.Parse(list);

                if (AP.ErrorCode != AleExpressionParser.ERROR_OK)
                {
                    ShowError(AP);
                    return;
                }

                if (T != null) Terms.Add(T);
            }

            if (Terms.Count == 0) return;

            //btnParse.Enabled = false;
            //btnEval.Enabled = false;
            //btnPerfomance.Enabled = false;

            string S;
            AleTermResult val = new AleTermResult();
            DateTime t1 = DateTime.Now;
            int N = 1000000;

            for (int i = N; i > 0 && val.ErrorCode == AleTermResult.ERROR_OK; i--)
            {
                if (i % 10000 == 0)
                {
                    //lblInfo.Text = i.ToString();
                    Application.DoEvents();
                }

                foreach (AleTerm t in Terms)
                {
                    t.Evaluate(out val, OnEvaluate, OnAssign);
                    if (val.ErrorCode != AleTermResult.ERROR_OK) break;
                }
            }

            DateTime t2 = DateTime.Now;

            if (val.ErrorCode == AleTermResult.ERROR_OK)
            {
                TimeSpan dt = t2.Subtract(t1);
                S = "Evaluation speed: " + (N / dt.TotalSeconds).ToString() + " expressions per second";
                //txtResult.Text = S;
            }
            //else txtResult.Text = "Error \"" + val.ErrorMessage() + "\" in line:" + AP.ErrorPosToLine(val.ErrorPos).ToString() + " col:" + AP.ErrorPosToCol(val.ErrorPos).ToString() + "\u000d\u000a";

            //btnParse.Enabled = true;
            //btnEval.Enabled = true;
            //btnPerfomance.Enabled = true;
        }

        /*
        private void btnClear_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "";
            txtResult.Text = "";
        }

        private void comboOperators_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboOperators.SelectedIndex == 1) AP.InitOperations(AleExpressionParser.OPERATIONS_STANDARDSET);
            else AP.InitOperations(AleExpressionParser.OPERATIONS_CLIKESET);

            InitMyOperations();
        }

        private void chkIgnoreCase_CheckedChanged(object sender, EventArgs e)
        {
            AP.IgnoreCase = AP.IgnoreCase;

            if (comboOperators.SelectedIndex == 1) AP.InitOperations(AleExpressionParser.OPERATIONS_STANDARDSET);
            else AP.InitOperations(AleExpressionParser.OPERATIONS_CLIKESET);
            
            InitMyVariables(AP.IgnoreCase);
            InitMyOperations();
        }

        private void cboExample_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cboExample.SelectedIndex;
            if (i < 0) return;

            string name = "example_" + i.ToString();
            string S = AleProjects.AleParser_Test.examples.ResourceManager.GetString(name);
            expr_ = S;
        }
        */
    }

    public class ChemicalElement
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public string ElectronConfig { get; set; }

        public ChemicalElement(string symbol, string name, double weight, string econfig)
        {
            Symbol = symbol;
            Name = name;
            Weight = weight;
            ElectronConfig = econfig;
        }
    }
}
