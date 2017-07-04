/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: IApplicationEntity.cs
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

namespace UIH.RT.TMS.Dicom.Iod
{
	public interface IApplicationEntity
	{
        string Name { get; }
        string AETitle { get; }
		string Description { get; }
		string Location { get; }

	    // TODO (CR Mar 2012): Unsure about this
        IScpParameters ScpParameters { get; }
        IStreamingParameters StreamingParameters { get; }
    }

    public interface IScpParameters
    {
        string HostName { get; }
        int Port { get; }
    }

    public interface IStreamingParameters
    {
        int HeaderServicePort { get; }
        int WadoServicePort { get; }
    }
}
