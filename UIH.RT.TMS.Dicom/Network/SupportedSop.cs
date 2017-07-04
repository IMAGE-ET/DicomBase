/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SupportedSop.cs
////
//// Summary:
////
////
//// Date: 2014/08/19
//////////////////////////////////////////////////////////////////////////
#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

using System.Collections.Generic;

namespace UIH.RT.TMS.Dicom.Network
{
    /// <summary>
    /// Structure used to represent the supported SOP Classes for Scu/Scp operations.
    /// </summary>
    public struct SupportedSop
    {
        private SopClass _sopClass;
        private IList<TransferSyntax> _syntaxList;

        /// <summary>
        /// The <see cref="UIH.RT.TMS.Dicom.SopClass"/> instance supported.
        /// </summary>
        public SopClass SopClass
        {
            get { return _sopClass; }
            set { _sopClass = value; }
        }

        /// <summary>
        /// A list of transfer syntaxes supported by the <see cref="SopClass"/>.
        /// </summary>
        public IList<TransferSyntax> SyntaxList
        {
            get {
                if (_syntaxList == null)
                    _syntaxList = new List<TransferSyntax>();
                return _syntaxList; 
            }
        }

        /// <summary>
        /// Used to add a supported transfer syntax.
        /// </summary>
        /// <param name="syntax">The transfer syntax supproted by the SOP Class.</param>
        public void AddSyntax(TransferSyntax syntax)
        {
            SyntaxList.Add(syntax);
        }
    }
}
