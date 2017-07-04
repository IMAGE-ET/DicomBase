/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomVM.cs
////
//// Summary:
////
////
//// Date: 2014/08/18
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace UIH.RT.TMS.Dicom
{
    public class DicomVM
    {
        private static IDictionary<string, DicomVM> _vm = new Dictionary<string, DicomVM>();

        // ReSharper disable InconsistentNaming
        public readonly static DicomVM VM_1 = DicomVM.Parse("1");
        public readonly static DicomVM VM_1_2 = DicomVM.Parse("1-2");
        public readonly static DicomVM VM_1_3 = DicomVM.Parse("1-3");
        public readonly static DicomVM VM_1_8 = DicomVM.Parse("1-8");
        public readonly static DicomVM VM_1_32 = DicomVM.Parse("1-32");
        public readonly static DicomVM VM_1_99 = DicomVM.Parse("1-99");
        public readonly static DicomVM VM_1_n = DicomVM.Parse("1-n");
        public readonly static DicomVM VM_2 = DicomVM.Parse("2");
        public readonly static DicomVM VM_2_n = DicomVM.Parse("2-n");
        public readonly static DicomVM VM_2_2n = DicomVM.Parse("2-2n");
        public readonly static DicomVM VM_3 = DicomVM.Parse("3");
        public readonly static DicomVM VM_3_n = DicomVM.Parse("3-n");
        public readonly static DicomVM VM_3_3n = DicomVM.Parse("3-3n");
        public readonly static DicomVM VM_4 = DicomVM.Parse("4");
        public readonly static DicomVM VM_6 = DicomVM.Parse("6");
        public readonly static DicomVM VM_16 = DicomVM.Parse("16");
        // ReSharper restore InconsistentNaming

        public DicomVM(int minimum, int maximum, int multiplicity)
        {
            Minimum = minimum;
            Maximum = maximum;
            Multiplicity = multiplicity;
        }

        private DicomVM()
            : this(1, 1, 1)
        {
            
        }

        #region Public Properties 

        public int Minimum { get; private set; }

        public int Maximum { get; private set; }

        public int Multiplicity { get; private set; }

        #endregion

        public override string ToString()
        {
            if (Minimum == Maximum)
                return Minimum.ToString(CultureInfo.InvariantCulture);

            if (Maximum == int.MaxValue)
            {
                if (Multiplicity > 1)
                    return string.Format(@"{0}-{1}", Minimum, Multiplicity);
                else
                {
                    return string.Format(@"{0}-n", Minimum);
                }
            }

            return string.Format(@"{0}-{1}", Minimum, Maximum);
        }

        public static DicomVM Parse(string s)
        {
            try
            {
                DicomVM vm = null;

                if (_vm.TryGetValue(s, out vm))
                    return vm;

                string[] parts = s.Split('-');
                if (parts.Length == 1)
                {
                    vm = new DicomVM {Minimum = int.Parse(parts[0])};
                    vm.Maximum = vm.Minimum;
                    vm.Multiplicity = vm.Minimum;
                }
                else
                {
                    vm = new DicomVM {Minimum = int.Parse(parts[0]), Multiplicity = 1};

                    if (parts[1].EndsWith("n", StringComparison.OrdinalIgnoreCase))
                    {
                        vm.Maximum = int.MaxValue;
                        parts[1] = parts[1].TrimEnd('n', 'N');

                        if (parts[1].Length > 0)
                            vm.Multiplicity = int.Parse(parts[1]);
                    }
                    else
                    {
                        vm.Maximum = int.Parse(parts[1]);
                    }
                }

                _vm.Add(s, vm);

                return vm;
            }
            catch (Exception e)
            {
                throw new DicomException("Error parsing value multiplicty ['" + s + "']", e);
            }
        }
        
    }
}
