/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: DicomCodecRegistry.cs
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
using System.Linq;
using System.Collections.Generic;
using UIH.RT.Framework.Utility;
using UIH.RT.TMS.Common;

namespace UIH.RT.TMS.Dicom.Codec
{
    /// <summary>
    /// Registry of <see cref="IDicomCodecFactory"/> implementations that extend <see cref="DicomCodecFactoryExtensionPoint"/>.
    /// </summary>
    public static class DicomCodecRegistry
    {
        #region Private Members

        private static readonly List<IDicomCodecFactory> Codecs;
    	private static readonly Dictionary<TransferSyntax, IDicomCodecFactory> Dictionary;

		#endregion

        #region Static Constructor
        
		static DicomCodecRegistry()
        {
			Dictionary = new Dictionary<TransferSyntax, IDicomCodecFactory>();
            Codecs = new List<IDicomCodecFactory>();
			try
			{
                var codecFactories = Platform.Instance.CompositionContainer.GetExportedValues<IDicomCodecFactory>();

                foreach (IDicomCodecFactory codecFactory in codecFactories)
                {
                    Codecs.Add(codecFactory);
                    Dictionary[codecFactory.CodecTransferSyntax] = codecFactory;
                }
			}
			catch(NotSupportedException)
			{
				LogAdapter.Logger.Info("No dicom codec extension(s) exist.");
			}
			catch(Exception e)
			{
                LogAdapter.Logger.TraceException(e);
			}
        }

		#endregion

		#region Public Static Methods

		/// <summary>
		/// Gets the <see cref="TransferSyntax"/>es of the available <see cref="IDicomCodecFactory"/> implementations.
		/// </summary>
		public static TransferSyntax[] GetCodecTransferSyntaxes()
		{
			return Dictionary.Where(kvp => kvp.Value.Enabled).Select(kvp => kvp.Key).ToArray();
		}

    	/// <summary>
    	/// Gets an array of <see cref="IDicomCodec"/>s (one from each available <see cref="IDicomCodecFactory"/>).
    	/// </summary>
		public static IDicomCodec[] GetCodecs()
    	{
    		return Codecs.Where(c => c.Enabled).Select(c => c.GetDicomCodec()).ToArray();
		}

		/// <summary>
		/// Gets an array <see cref="IDicomCodecFactory"/> instances.
		/// </summary>
		/// <remarks>
		/// Extensions are loaded for the codec factories.  If more than one codec support a <see cref="TransferSyntax"/>,
		/// both codecs are returned in this list, although only one would be used.
		/// </remarks>
		/// <returns>An array of codec factories.</returns>
		public static IDicomCodecFactory[] GetCodecFactories()
		{
			return Codecs.Where(c => c.Enabled).ToArray();
		}
		
		/// <summary>
        /// Get a codec instance from the registry.
        /// </summary>
        /// <param name="syntax">The transfer syntax to get a codec for.</param>
        /// <returns>null if a codec has not been registered, an <see cref="IDicomCodec"/> instance otherwise.</returns>
        public static IDicomCodec GetCodec(TransferSyntax syntax)
        {
			IDicomCodecFactory factory;
            if (!Dictionary.TryGetValue(syntax, out factory))
                return null;

            return factory.Enabled ? factory.GetDicomCodec() : null;
        }

        /// <summary>
        /// Set an <see cref="IDicomCodecFactory"/> for a transfer syntax, overriding the current value.
        /// </summary>
        /// <param name="syntax">The transfer syntax of the codec.</param>
        /// <param name="factory">The factory for the codec.</param>
        public static void SetCodec(TransferSyntax syntax, IDicomCodecFactory factory)
        {
            Dictionary[syntax] = factory;
        }

        /// <summary>
        /// Get default parameters for the codec.
        /// </summary>
        /// <param name="syntax">The transfer syntax to get the parameters for.</param>
        /// <param name="collection">The <see cref="DicomDataset"/> that the codec will work on.</param>
        /// <returns>null if no codec is registered, the parameters otherwise.</returns>
        public static DicomCodecParameters GetCodecParameters(TransferSyntax syntax, DicomDataset collection)
        {
			IDicomCodecFactory factory;
			if (!Dictionary.TryGetValue(syntax, out factory))
				return null;

            return factory.Enabled ? factory.GetCodecParameters(collection) : null;
        }
        #endregion
    }
}
