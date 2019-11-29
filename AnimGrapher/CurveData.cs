using System;
using System.Globalization;

namespace AnimGrapher
{
    class CurveData
    {
        private String name_ = "";

        private CurveType curveType_ = CurveType.PARAMETRIC;

        private string xtEquation_ = "";
        private string ytEquation_ = "";
        private string rtEquation_ = "";
        private string yxEquation_ = "";

        public bool Isometric = true;

        public double XVmin = -2;
        public double XVmax =  2;
        public double YVmin = -2;
        public double YVmax =  2;

        public double Pmin = 0;
        public double Pmax = 6.283;     // 2*Math.PI;
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

        public string XtEquation
        {
            get { return xtEquation_; }
            set { xtEquation_ = value; }
        }

        public string YtEquation
        {
            get { return ytEquation_; }
            set { ytEquation_ = value; }
        }

        public string RtEquation
        {
            get { return rtEquation_; }
            set { rtEquation_ = value; }
        }

        public string YxEquation
        {
            get { return yxEquation_; }
            set { yxEquation_ = value; }
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
                    xtEquation_ = equation1;
                    ytEquation_ = equation2;
                    break;

                case CurveType.POLAR:
                    rtEquation_ = equation1;
                    break;

                case CurveType.CARTESIAN:
                    yxEquation_ = equation1;
                    break;
            }
        }

        public void Clear()
        {
            CurveData cdDefault = new CurveData();

            name_ = cdDefault.name_;
            curveType_ = cdDefault.curveType_;

            xtEquation_ = cdDefault.xtEquation_;
            ytEquation_ = cdDefault.ytEquation_;
            rtEquation_ = cdDefault.rtEquation_;
            yxEquation_ = cdDefault.yxEquation_;
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
            CurveData cdCopy = new CurveData();

            cdCopy.Name = this.Name;
            cdCopy.Type = this.Type;

            cdCopy.XtEquation = this.XtEquation;
            cdCopy.YtEquation = this.YtEquation;
            cdCopy.RtEquation = this.RtEquation;
            cdCopy.YxEquation = this.YxEquation;

            cdCopy.Isometric = this.Isometric;
            cdCopy.XVmin = this.XVmin;
            cdCopy.XVmax = this.XVmax;
            cdCopy.YVmin = this.YVmin;
            cdCopy.YVmax = this.YVmax;

            cdCopy.Pmin = this.Pmin;
            cdCopy.Pmax = this.Pmax;
            cdCopy.Pstep = this.Pstep;

            cdCopy.Thickness = this.Thickness;

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
                    desc += "x(t)=" + xtEquation_ + Environment.NewLine;
                    desc += "y(t)=" + ytEquation_ + Environment.NewLine;
                    break;

                case CurveType.POLAR:
                    desc += "r(t)=" + rtEquation_ + Environment.NewLine;
                    break;

                case CurveType.CARTESIAN:
                    desc += "y(x)=" + yxEquation_ + Environment.NewLine;
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
