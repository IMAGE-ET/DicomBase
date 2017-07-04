/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: QueryIodBase.cs
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

namespace UIH.RT.TMS.Dicom.Iod.Iods
{
	public abstract class QueryIodBase : IodBase
	{
		#region Constructors
        /// <summary>
		/// Initializes a new instance of the <see cref="QueryIodBase"/> class.
        /// </summary>
        public QueryIodBase() : base()
        {
            SetAttributeFromEnum(DicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Series);
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="QueryIodBase"/> class.
        /// </summary>
		public QueryIodBase(IDicomElementProvider dicomElementProvider) : base(dicomElementProvider)
        {
            SetAttributeFromEnum(DicomElementProvider[DicomTags.QueryRetrieveLevel], QueryRetrieveLevel.Series);
        }
        #endregion

		/// <summary>
		/// Gets or sets the specific character set.
		/// </summary>
		/// <value>The specific character set.</value>
		public string SpecificCharacterSet
		{
			get { return DicomElementProvider[DicomTags.SpecificCharacterSet].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.SpecificCharacterSet].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the Retrieve AE Title.
		/// </summary>
		/// <value>The Retrieve AE Title.</value>
		public string RetrieveAeTitle
		{
			get { return DicomElementProvider[DicomTags.RetrieveAeTitle].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.RetrieveAeTitle].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the Storage Media Fileset Id.
		/// </summary>
		/// <value>The media Fileset Id.</value>
		public string StorageMediaFileSetId
		{
			get { return DicomElementProvider[DicomTags.StorageMediaFileSetId].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.StorageMediaFileSetId].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the Storage Media Fileset Uid.
		/// </summary>
		/// <value>The media Fileset Uid.</value>
		public string StorageMediaFileSetUid
		{
			get { return DicomElementProvider[DicomTags.StorageMediaFileSetUid].GetString(0, String.Empty); }
			set { DicomElementProvider[DicomTags.StorageMediaFileSetUid].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the query retrieve level.
		/// </summary>
		/// <value>The query retrieve level.</value>
		public QueryRetrieveLevel QueryRetrieveLevel
		{
			get
			{
				if (!DicomElementProvider[DicomTags.QueryRetrieveLevel].IsEmpty)
				{
					try
					{
						return (QueryRetrieveLevel)Enum.Parse(typeof(QueryRetrieveLevel), DicomElementProvider[DicomTags.QueryRetrieveLevel].GetString(0, QueryRetrieveLevel.None.ToString()), true);
					}
					catch (Exception)
					{
						return QueryRetrieveLevel.None;
					}
				}
				return QueryRetrieveLevel.None;

			}
			set
			{
				SetAttributeFromEnum(DicomElementProvider[DicomTags.QueryRetrieveLevel], value);
			}
		}

		/// <summary>
		/// Gets or sets the Instance Availability
		/// </summary>
		public InstanceAvailability InstanceAvailability
		{
			get
			{
				if (!DicomElementProvider[DicomTags.InstanceAvailability].IsEmpty)
				{
					try
					{
						return (InstanceAvailability)Enum.Parse(typeof(InstanceAvailability), DicomElementProvider[DicomTags.InstanceAvailability].GetString(0, InstanceAvailability.Unknown.ToString()), true);
					}
					catch (Exception)
					{
						return InstanceAvailability.Unknown;
					}
				}
				return InstanceAvailability.Unknown;
			}
			set { SetAttributeFromEnum(DicomElementProvider[DicomTags.InstanceAvailability], value); }
		}
	}

	#region InstanceAvailability Enum
	/// <summary>
	/// <see cref="DicomTags.InstanceAvailability"/>
	/// </summary>
	public enum InstanceAvailability
	{
		/// <summary>
		/// The instances are immediately available
		/// </summary>
		Online,
		/// <summary>
		/// The instances need to be retrieved from relatively slow media such as optical disk or tape
		/// </summary>
		Nearline,
		/// <summary>
		/// The instances need to be retrieved by manual intervention
		/// </summary>
		Offline,
		/// <summary>
		/// The instances cannot be retrieved. Note that SOP Instances that are unavailable may have an 
		/// alternate representation that is available (see section C.6.1.1.5.1).
		/// </summary>
		Unknown
	}

	#endregion

	#region QueryRetrieveLevel Enum
	/// <summary>
	/// 
	/// </summary>
	public enum QueryRetrieveLevel
	{
		/// <summary>
		/// 
		/// </summary>
		None,
		/// <summary>
		/// 
		/// </summary>
		Patient,
		/// <summary>
		/// 
		/// </summary>
		Study,
		/// <summary>
		/// 
		/// </summary>
		Series,
		/// <summary>
		/// 
		/// </summary>
		Image
	}

	#endregion
}
