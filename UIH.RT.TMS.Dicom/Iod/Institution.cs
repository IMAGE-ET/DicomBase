/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: Institution.cs
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

using System;
using UIH.RT.TMS.Dicom.Iod.Modules;

namespace UIH.RT.TMS.Dicom.Iod
{
	/// <summary>
	/// Represents the institution where the equipment that produced the composite instance is located.
	/// </summary>
	public struct Institution : IEquatable<Institution>
	{
		private string _name;
		private string _address;
		private string _departmentName;

		/// <summary>
		/// Initializes a new instance of <see cref="Institution"/>.
		/// </summary>
		/// <param name="name">The name of the institution.</param>
		/// <param name="address">The mailing address of the institution.</param>
		public Institution(string name, string address)
		{
			_name = name ?? string.Empty;
			_address = address ?? string.Empty;
			_departmentName = string.Empty;
		}

		/// <summary>
		/// Initializes a new instance of <see cref="Institution"/>.
		/// </summary>
		/// <param name="name">The name of the institution.</param>
		/// <param name="address">The mailing address of the institution.</param>
		/// <param name="departmentName">The name of the department in the institution.</param>
		public Institution(string name, string address, string departmentName)
		{
			_name = name ?? string.Empty;
			_address = address ?? string.Empty;
			_departmentName = departmentName ?? string.Empty;
		}

		/// <summary>
		/// Gets or sets the name of the institution.
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { _name = value ?? string.Empty; }
		}

		/// <summary>
		/// Gets or sets the mailing address of the institution.
		/// </summary>
		public string Address
		{
			get { return _address; }
			set { _address = value ?? string.Empty; }
		}

		/// <summary>
		/// Gets or sets the name of the department in the institution.
		/// </summary>
		public string DepartmentName
		{
			get { return _departmentName; }
			set { _departmentName = value ?? string.Empty; }
		}

		/// <summary>
		/// Gets a value indicating whether or not this <see cref="Institution"/> is the empty value.
		/// </summary>
		public bool IsEmpty
		{
			get { return string.IsNullOrEmpty(Name) & string.IsNullOrEmpty(Address) && string.IsNullOrEmpty(DepartmentName); }
		}

		public bool Equals(Institution other)
		{
			return Name == other.Name && Address == other.Address && DepartmentName == other.DepartmentName;
		}

		public override bool Equals(object obj)
		{
			return obj is Institution && Equals((Institution) obj);
		}

		public override int GetHashCode()
		{
			return -0x502B77AC ^ Name.GetHashCode() ^ Address.GetHashCode() ^ DepartmentName.GetHashCode();
		}

		/// <summary>
		/// Gets the empty institution.
		/// </summary>
		public static readonly Institution Empty = new Institution();

		/// <summary>
		/// Gets the institution defined in the General Equipment Module of the given <paramref name="dcm">DICOM file</paramref>.
		/// </summary>
		public static Institution GetInstitution(DicomFile dcm)
		{
			return GetInstitution(dcm.DataSet);
		}

		/// <summary>
		/// Gets the institution defined in the General Equipment Module of the given <paramref name="dicomElementProvider">dataset</paramref>.
		/// </summary>
		public static Institution GetInstitution(IDicomElementProvider dicomElementProvider)
		{
			var institution = new Institution();
			var iod = new GeneralEquipmentModuleIod(dicomElementProvider);
			institution.Name = iod.InstitutionName ?? string.Empty;
			institution.Address = iod.InstitutionAddress ?? string.Empty;
			institution.DepartmentName = iod.InstitutionalDepartmentName ?? string.Empty;
			return institution;
		}
	}
}
