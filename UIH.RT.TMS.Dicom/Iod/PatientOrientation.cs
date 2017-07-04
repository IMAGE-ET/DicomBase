/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: PatientOrientation.cs
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
using UIH.RT.TMS.Dicom.Utilities;

namespace UIH.RT.TMS.Dicom.Iod
{
	/// <summary>
	/// Represents the orientation of the image in the patient using dicom enumerated values
	/// to indicate the direction of the first row and column in the image.
	/// </summary>
	public class PatientOrientation : IEquatable<PatientOrientation>
	{
		public static PatientOrientation Empty = new PatientOrientation(String.Empty, String.Empty);
		public static PatientOrientation AxialRight = new PatientOrientation(PatientDirection.Right, PatientDirection.Posterior);
		public static PatientOrientation AxialLeft = new PatientOrientation(PatientDirection.Left, PatientDirection.Posterior);
		public static PatientOrientation SaggittalPosterior = new PatientOrientation(PatientDirection.Posterior, PatientDirection.Foot);
		public static PatientOrientation SaggittalAnterior = new PatientOrientation(PatientDirection.Anterior, PatientDirection.Foot);
		public static PatientOrientation CoronalRight = new PatientOrientation(PatientDirection.Right, PatientDirection.Foot);
		public static PatientOrientation CoronalLeft = new PatientOrientation(PatientDirection.Left, PatientDirection.Foot);

		public PatientOrientation(PatientDirection row, PatientDirection column)
		{
			Row = new PatientDirection(row);
			Column = new PatientDirection(column);
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public PatientOrientation(string row, string column)
			: this(row, column, AnatomicalOrientationType.Biped) {}

		/// <summary>
		/// Constructor.
		/// </summary>
		public PatientOrientation(string row, string column, AnatomicalOrientationType anatomicalOrientationType)
			: this(new PatientDirection(row, anatomicalOrientationType), new PatientDirection(column, anatomicalOrientationType)) {}

		/// <summary>
		/// Constructor.
		/// </summary>
		public PatientOrientation(string row, string column, string anatomicalOrientationType)
			: this(new PatientDirection(row, ParseAnatomicalOrientationType(anatomicalOrientationType)), new PatientDirection(column, ParseAnatomicalOrientationType(anatomicalOrientationType))) {}

		private static AnatomicalOrientationType ParseAnatomicalOrientationType(string anatomicalOrientationType)
		{
			return IodBase.ParseEnum(anatomicalOrientationType, AnatomicalOrientationType.None);
		}

		#region Public Properties

		/// <summary>
		/// Gets whether or not this <see cref="PatientOrientation"/> object is empty.
		/// </summary>
		public bool IsEmpty
		{
			get { return Row.IsEmpty && Column.IsEmpty; }
		}

		public bool IsValid
		{
			get
			{
				//Both unspecified is not valid.
				if (Row.Equals(Column))
					return false;

				if (!Row.IsValid || !Column.IsValid)
					return false;

				//We could do something with vectors and check they are "orthogonal", but this is pretty good.
				for (int i = 0; i < Math.Max(Row.ComponentCount, Column.ComponentCount); ++i)
				{
					//They can both point to the same direction in the patient, but
					//they can't both have "primary component == Foot", for example
					//or event have primary components along the same axis (e.g. Right and Left).
					var component = (PatientDirection.Component) i;
					var row = Row[component];
					var column = Column[component];
					if (row.Equals(column))
						return false;

					if (row.Equals(column.OpposingDirection))
						return false;
				}

				return true;
			}
		}

		/// <summary>
		/// Gets the direction of the first row in the image.
		/// </summary>
		public virtual PatientDirection Row { get; private set; }

		/// <summary>
		/// Gets the direction of the first column in the image.
		/// </summary>
		public virtual PatientDirection Column { get; private set; }

		/// <summary>
		/// Gets the primary direction of the first row in the image.
		/// </summary>
		public PatientDirection PrimaryRow
		{
			get { return Row.Primary; }
		}

		/// <summary>
		/// Gets the primary direction of the first column in the image.
		/// </summary>
		public PatientDirection PrimaryColumn
		{
			get { return Column.Primary; }
		}

		/// <summary>
		/// Gets the secondary direction of the first row in the image.
		/// </summary>
		public PatientDirection SecondaryRow
		{
			get { return Row.Secondary; }
		}

		/// <summary>
		/// Gets the secondary direction of the first column in the image.
		/// </summary>
		public PatientDirection SecondaryColumn
		{
			get { return Column.Secondary; }
		}

		/// <summary>
		/// Gets the tertiary direction of the first row in the image.
		/// </summary>
		public PatientDirection TertiaryRow
		{
			get { return Row.Tertiary; }
		}

		/// <summary>
		/// Gets the tertiary direction of the first column in the image.
		/// </summary>
		public PatientDirection TertiaryColumn
		{
			get { return Column.Tertiary; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets a string suitable for direct insertion into a <see cref="DicomElementMultiValueText"/> element.
		/// </summary>
		public override string ToString()
		{
			if (Row.IsEmpty && Column.IsEmpty)
				return String.Empty;

			return String.Format(@"{0}\{1}", Row, Column);
		}

		/// <summary>
		/// Creates a <see cref="PatientOrientation"/> object from a dicom multi-valued string.
		/// </summary>
		/// <returns>
		/// Null if there are not exactly 2 parsed values in the input string.
		/// </returns>
		public static PatientOrientation FromString(string multiValuedString)
		{
			string[] values = DicomStringHelper.GetStringArray(multiValuedString);
			if (values != null && values.Length == 2)
				return new PatientOrientation(values[0], values[1]);

			return null;
		}

		#region IEquatable<PatientOrientation> Members

		public bool Equals(PatientOrientation other)
		{
			if (other == null)
				return false;

			return other.Row == Row && other.Column == Column;
		}

		#endregion

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			return Equals(obj as PatientOrientation);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#endregion
	}
}
