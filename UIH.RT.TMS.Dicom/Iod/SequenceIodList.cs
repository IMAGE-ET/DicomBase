/////////////////////////////////////////////////////////////////////////
//// Copyright, (c) Shanghai United Imaging Healthcare Inc
//// All rights reserved. 
//// 
//// author: qiuyang.cao@united-imaging.com
////
//// File: SequenceIodList.cs
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
using System.Collections.Generic;

namespace UIH.RT.TMS.Dicom.Iod
{
    /// <summary>
    /// Generic class to get/add strongly typed Sequence Iods.
    /// <example>
    /// <code><![CDATA[
    /// public SequenceIodList<ScheduledProcedureStepSequenceIod> ScheduledProcedureStepSequenceList
    /// {
    ///     get 
    ///     {
    ///         return new SequenceIodList<ScheduledProcedureStepSequenceIod>(base.DicomDataset[DicomTags.ScheduledProcedureStepSequence] as DicomElementSq); 
    ///     }
    ///  }]]>
    /// </code></example>
    /// </summary>
    /// <typeparam name="T">Type of SequenceIod</typeparam>
    public class SequenceIodList<T> : IList<T>
        where T:SequenceIodBase, new()
    {
        #region Private Variables
        /// <summary>
        /// 
        /// </summary>
        DicomElementSq _dicomElementSq;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceIodList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="dicomTag">The dicom tag.</param>
        public SequenceIodList(DicomTag dicomTag)
        {
            _dicomElementSq = new DicomElementSq(dicomTag);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceIodList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public SequenceIodList(uint tag)
        {
            _dicomElementSq = new DicomElementSq(tag);
        }

        /// <summary>
        /// Initializes a new instance of the SequenceIodList class.
        /// </summary>
        public SequenceIodList(DicomElementSq dicomElementSq)
        {
            if (dicomElementSq == null)
                throw new ArgumentNullException("dicomElementSq");

            _dicomElementSq = dicomElementSq;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the DicomElementSq.
        /// </summary>
        /// <value>The dicom element SQ.</value>
        public DicomElementSq dicomElementSq
        {
            get { return _dicomElementSq; }
            set { _dicomElementSq = value; }
        }

        /// <summary>
        /// Gets the first sequence item, for binding purposes.
        /// </summary>
        /// <value>The first sequence item.</value>
        public T FirstSequenceItem
        {
            get { return this[0]; }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the generic sequence iod from dicom sequence item.
        /// </summary>
        /// <param name="dicomSequenceItem">The dicom sequence item.</param>
        /// <returns></returns>
        /// <remarks>Can't specify a constructor parameter for a generic type so need to do it like this.</remarks>
        private T GetSequenceIodFromDicomSequenceItem(DicomSequenceItem dicomSequenceItem)
        {
            if (dicomSequenceItem == null)
                return null;
            else
            {
                T newT = new T();
                newT.DicomSequenceItem = dicomSequenceItem;
                return newT;
            }
        }
        #endregion

        #region IList<T> Members

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(T item)
        {
            IEnumerator<T> e = this.GetEnumerator();
            int i = 0;
            while (e.MoveNext())
            {
                T obj = e.Current;
                if (obj.Equals(item))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Note: Not Yet Implemented.  Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.</exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.</exception>
        public void Insert(int index, T item)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
            //TODO: to implement, would have to implement these methods in the DicomElementSq class, or perhaps turn DicomElementSq to use a a List instead of an array?
        }

        /// <summary>
        /// Not Yet Implemented.  Removes the <see cref="T:System.Collections.Generic.IList`1"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.</exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.</exception>
        public void RemoveAt(int index)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
            //TODO: to implement, would have to implement these methods in the DicomElementSq class, or perhaps turn DicomElementSq to use a a List instead of an array?
        }

        /// <summary>
        /// Getsthe <see cref="T"/> at the specified index.  Set is Not Yet Implemented.  
        /// </summary>
        /// <value></value>
        public T this[int index]
        {
            get
            {
                if (_dicomElementSq.Count > index)
                {
                    return GetSequenceIodFromDicomSequenceItem(_dicomElementSq[index]);
                }
                return null;
            }
            set
            {
                throw new NotImplementedException("The method or operation is not implemented.");
                //TODO: to implement, would have to implement these methods in the DicomElementSq class, or perhaps turn DicomElementSq to use a a List instead of an array?

            }
        }

        #endregion

        #region ICollection<T> Members

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
        public void Add(T item)
        {
            _dicomElementSq.AddSequenceItem(item.DicomSequenceItem);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. </exception>
        public void Clear()
        {
            _dicomElementSq.ClearSequenceItems();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            return (this.IndexOf(item) > -1);
        }

        /// <summary>
        /// Not Yet Implemented.  Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="array"/> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// 	<paramref name="array"/> is multidimensional.-or-<paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.-or-Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.</exception>
        /// 	<exception cref="NotImplementedException"/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <value></value>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</returns>
        public int Count
        {
            get { return (int) _dicomElementSq.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.</returns>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Not Yet Implemented.  Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
        /// <exception cref="NotImplementedException"/>
        public bool Remove(T item)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _dicomElementSq.Count; i++)
            {
                yield return GetSequenceIodFromDicomSequenceItem(_dicomElementSq[i]);
            }
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
