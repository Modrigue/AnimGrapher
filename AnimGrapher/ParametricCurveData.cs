using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AnimGrapher
{
    class ParametricCurveData
    {
        String name = "";

        string xEquation = "";
        string yEquation = "";

        public double XMin = -2;
        public double XMax =  2;
        public double YMin = -2;
        public double YMax =  2;

        public double TMin = 0;
        public double TMax = 6.283;     //2*Math.PI;
        public double TStep = 0.01;

        public double Thickness = 1;

        #region Accessors

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string XEquation
        {
            get { return xEquation; }
            set { xEquation = value; }
        }

        public string YEquation
        {
            get { return yEquation; }
            set { yEquation = value; }
        }

        #endregion

        public ParametricCurveData()
        {
            // create default parametric curve data
        }

        public ParametricCurveData(string name)
        {
            this.name = name;
        }

        public ParametricCurveData(string name, string xEquation, string yEquation)
        {
            this.name = name;

            this.xEquation = xEquation;
            this.yEquation = yEquation;
        }

        public void Clear()
        {
            ParametricCurveData pcdDefault = new ParametricCurveData();

            name = pcdDefault.name;

            xEquation = pcdDefault.xEquation;
            yEquation = pcdDefault.yEquation;

            XMin = pcdDefault.XMin;
            XMax = pcdDefault.XMax;
            YMin = pcdDefault.YMin;
            YMax = pcdDefault.YMax;

            TMin = pcdDefault.TMin;
            TMax = pcdDefault.TMax;
            TStep = pcdDefault.TStep;

            Thickness = pcdDefault.Thickness;
        }

        public ParametricCurveData Copy()
        {
            ParametricCurveData pcdCopy = new ParametricCurveData();

            pcdCopy.Name = this.Name;

            pcdCopy.XEquation = this.XEquation;
            pcdCopy.YEquation = this.YEquation;

            pcdCopy.XMin = this.XMin;
            pcdCopy.XMax = this.XMax;
            pcdCopy.YMin = this.YMin;
            pcdCopy.YMax = this.YMax;

            pcdCopy.TMin = this.TMin;
            pcdCopy.TMax = this.TMax;
            pcdCopy.TStep = this.TStep;

            pcdCopy.Thickness = this.Thickness;

            return pcdCopy;
        }

        public string GetDescription()
        {
            string desc = "";
            ParametricCurveData pcdDefault = new ParametricCurveData();

            // name mandatory
            if (String.IsNullOrEmpty(name))
                return "";
            desc += "[" + name + "]" + Environment.NewLine;

            // equations
            desc += "x(t)=" + xEquation + Environment.NewLine;
            desc += "y(t)=" + yEquation + Environment.NewLine;

            // write parameters values if different from default values

            CultureInfo culture = new CultureInfo("en-US");

            if (XMin != pcdDefault.XMin)
                desc += "x_min=" + XMin.ToString(culture) + Environment.NewLine;
            if (XMax != pcdDefault.XMax)
                desc += "x_max=" + XMax.ToString(culture) + Environment.NewLine;
            if (YMin != pcdDefault.YMin)
                desc += "y_min=" + YMin.ToString(culture) + Environment.NewLine;
            if (YMax != pcdDefault.YMax)
                desc += "y_max=" + YMax.ToString(culture) + Environment.NewLine;

            if (TMin != pcdDefault.TMin)
                desc += "t_min=" + TMin.ToString(culture) + Environment.NewLine;
            if (TMax != pcdDefault.TMax)
                desc += "t_max=" + TMax.ToString(culture) + Environment.NewLine;
            if (TStep != pcdDefault.TStep)
                desc += "t_step=" + TStep.ToString(culture) + Environment.NewLine;

            if (Thickness != pcdDefault.Thickness)
                desc += "thickness=" + Thickness + Environment.NewLine;

            desc += Environment.NewLine;

            return desc;
        }
    }
}
