using System;
using System.Globalization;

namespace AnimGrapher
{
    class CurveData
    {
        private String name_ = "";

        private CurveType curveType_ = CurveType.PARAMETRIC;

        private string equation1_ = "";
        private string equation2_ = "";
        
        public bool Isometric = true;

        public double XVmin = -2;
        public double XVmax =  2;
        public double YVmin = -2;
        public double YVmax =  2;

        public double Pmin = 0;
        public double Pmax = 6.283; // 2*Math.PI;
        public double Pstep = 0.01;

        public double Thickness = 1;

        #region Accessors

        public string Name
        {
            get { return name_; }
            set { name_ = value; }
        }

        public CurveType Type
        {
            get { return curveType_; }
            set { curveType_ = value; }
        }

        public string Equation1
        {
            get { return equation1_; }
            set { equation1_ = value; }
        }

        public string Equation2
        {
            get { return equation2_; }
            set { equation2_ = value; }
        }

        #endregion

        public CurveData()
        {
            // create default parametric curve data
        }

        public CurveData(string name, CurveType curveType, bool isometric)
        {
            name_ = name;
            curveType_ = curveType;
            Isometric = isometric;
        }

        public CurveData(string name, CurveType curveType, string equation1, string equation2)
        {
            name_ = name;
            curveType_ = curveType;

            switch (curveType)
            {
                case CurveType.PARAMETRIC:
                    equation1_ = equation1;
                    equation2_ = equation2;
                    break;

                case CurveType.POLAR:
                case CurveType.CARTESIAN:
                case CurveType.EQUALITY:
                case CurveType.INEQUALITY:
                    equation1_ = equation1;
                    equation2_ = "";
                    break;
            }
        }

        public void Clear()
        {
            CurveData cdDefault = new CurveData();

            name_ = cdDefault.name_;
            curveType_ = cdDefault.curveType_;

            equation1_ = cdDefault.equation1_;
            equation2_ = cdDefault.equation2_;

            Isometric = cdDefault.Isometric;

            XVmin = cdDefault.XVmin;
            XVmax = cdDefault.XVmax;
            YVmin = cdDefault.YVmin;
            YVmax = cdDefault.YVmax;

            Pmin = cdDefault.Pmin;
            Pmax = cdDefault.Pmax;
            Pstep = cdDefault.Pstep;

            Thickness = cdDefault.Thickness;
        }

        public CurveData Copy()
        {
            CurveData cdCopy = new CurveData
            {
                Name = this.Name,
                Type = this.Type,

                Equation1 = this.Equation1,
                Equation2 = this.Equation2,

                Isometric = this.Isometric,
                XVmin = this.XVmin,
                XVmax = this.XVmax,
                YVmin = this.YVmin,
                YVmax = this.YVmax,

                Pmin = this.Pmin,
                Pmax = this.Pmax,
                Pstep = this.Pstep,

                Thickness = this.Thickness
            };

            return cdCopy;
        }

        public string GetDescription()
        {
            string desc = "";
            CurveData cdDefault = new CurveData();

            // name mandatory
            if (String.IsNullOrEmpty(name_))
                return "";
            desc += "[" + name_ + "]" + Environment.NewLine;

            desc += "type=" + curveType_.ToString().ToLower() + Environment.NewLine;

            // equations
            switch (curveType_)
            {
                case CurveType.PARAMETRIC:
                    desc += "x(t)=" + equation1_ + Environment.NewLine;
                    desc += "y(t)=" + equation2_ + Environment.NewLine;
                    break;

                case CurveType.POLAR:
                    desc += "r(t)=" + equation1_ + Environment.NewLine;
                    break;

                case CurveType.CARTESIAN:
                    desc += "y(x)=" + equation1_ + Environment.NewLine;
                    break;

                case CurveType.EQUALITY:
                    desc += "0=" + equation1_ + Environment.NewLine;
                    break;

                case CurveType.INEQUALITY:
                    desc += "0<" + equation1_ + Environment.NewLine;
                    break;
            }

            // write parameters values if different from default values

            CultureInfo culture = new CultureInfo("en-US");

            if (!Isometric)
                desc += "isometric=" + Isometric.ToString(culture) + Environment.NewLine;

            // if isometric, do not save x min and max
            if (!Isometric)
            {
                if (XVmin != cdDefault.XVmin)
                    desc += "xv_min=" + XVmin.ToString(culture) + Environment.NewLine;
                if (XVmax != cdDefault.XVmax)
                    desc += "xv_max=" + XVmax.ToString(culture) + Environment.NewLine;
            }

            if (YVmin != cdDefault.YVmin)
                desc += "yv_min=" + YVmin.ToString(culture) + Environment.NewLine;
            if (YVmax != cdDefault.YVmax)
                desc += "yv_max=" + YVmax.ToString(culture) + Environment.NewLine;

            switch (curveType_)
            {
                case CurveType.PARAMETRIC:
                case CurveType.POLAR:
                    if (Pmin != cdDefault.Pmin)
                        desc += "t_min=" + Pmin.ToString(culture) + Environment.NewLine;
                    if (Pmax != cdDefault.Pmax)
                        desc += "t_max=" + Pmax.ToString(culture) + Environment.NewLine;
                    if (Pstep != cdDefault.Pstep)
                        desc += "t_step=" + Pstep.ToString(culture) + Environment.NewLine;
                    break;

                case CurveType.CARTESIAN:
                case CurveType.EQUALITY:
                case CurveType.INEQUALITY:
                    if (Pmin != cdDefault.Pmin)
                        desc += "x_min=" + Pmin.ToString(culture) + Environment.NewLine;
                    if (Pmax != cdDefault.Pmax)
                        desc += "x_max=" + Pmax.ToString(culture) + Environment.NewLine;
                    if (Pstep != cdDefault.Pstep)
                        desc += "x_step=" + Pstep.ToString(culture) + Environment.NewLine;
                    break;
            }

            if (Thickness != cdDefault.Thickness)
                desc += "thickness=" + Thickness + Environment.NewLine;

            desc += Environment.NewLine;

            return desc;
        }

        // NOTE: duplicated code
        public void UpdateGivenY(int width, int height)
        {
            if (!Isometric)
                return;

            // maintain x center
            double xCenter = 0.5 * (XVmin + XVmax);

            // compute new x range
            double yRange = YVmax - YVmin;
            double xRangeNew = yRange * (double)width / (double)height;

            // compute new x min and max
            XVmin = xCenter - 0.5 * xRangeNew;
            XVmax = xCenter + 0.5 * xRangeNew;
        }
    }
}
