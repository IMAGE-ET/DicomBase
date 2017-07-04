/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: IResourceResolver.cs
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

using System.IO;
using System.Resources;
using System.Text.RegularExpressions;

namespace UIH.RT.TMS.Common.Utilities
{
    /// <summary>
    /// Defines an interface that provides resource resolution services.
    /// </summary>
    /// <remarks>
	/// Resource resolution in this context involves accepting an unqualified or 
	/// partially qualified resource name as input and attempting to fully
	/// qualify the name so as to resolve the resource.
	/// </remarks>
    public interface IResourceResolver
    {
        /// <summary>
        /// Attempts to localize the specified unqualified string resource key.
        /// </summary>
        /// <remarks>
        /// Searches for a string resource entry that matches the specified key.
        /// </remarks>
        /// <param name="unqualifiedStringKey">The string resource key to search for.  Must not be qualified.</param>
        /// <returns>The localized string, or the argument unchanged if the key could not be found.</returns>
        string LocalizeString(string unqualifiedStringKey);

        /// <summary>
        /// Attempts to return a fully qualified resource name from the specified name, which may be partially
        /// qualified or entirely unqualified.
        /// </summary>
        /// <param name="resourceName">A partially qualified or unqualified resource name.</param>
        /// <returns>A qualified resource name, if found, otherwise an exception is thrown.</returns>
        /// <exception cref="MissingManifestResourceException">if the resource name could not be resolved.</exception>
        Stream OpenResource(string resourceName);

        /// <summary>
        /// Attempts to resolve and open a resource from the specified name, which may be partially
        /// qualified or entirely unqualified.
        /// </summary>
        /// <param name="resourceName">A partially qualified or unqualified resource name.</param>
        /// <returns>A qualified resource name, if found, otherwise an exception is thrown.</returns>
        /// <exception cref="MissingManifestResourceException">if the resource name could not be resolved.</exception>
        string ResolveResource(string resourceName);

        /// <summary>
        /// Returns the set of resources whose name matches the specified regular expression.
        /// </summary>
        /// <param name="regex"></param>
        /// <returns></returns>
        string[] FindResources(Regex regex);
    }
}
