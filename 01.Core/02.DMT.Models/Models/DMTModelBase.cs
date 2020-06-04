#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using NLib;
using DMT;

// required for JsonIgnore attribute.
using Newtonsoft.Json;

#endregion

namespace DMT.Models
{
    #region DMTModelBase (abstract)

    /// <summary>
    /// The DMTModelBase abstract class.
    /// Provide basic implementation of INotifyPropertyChanged interface.
    /// </summary>
    public abstract class DMTModelBase : INotifyPropertyChanged
    {
        #region Private Methods

        /// <summary>
        /// Raise Property Changed Event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected void RaiseChanged(string propertyName)
        {
            PropertyChanged.Call(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    #endregion
}
