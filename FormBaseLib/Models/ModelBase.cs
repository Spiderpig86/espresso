using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FormBaseLib.Models {

    /// <summary>
    ///     Base view for all forms of the app. Comes with notification hook and disposable handlers
    /// </summary>
    public abstract class ModelBase: INotifyPropertyChanged, IDisposable {

        protected bool _disposed; // Form disposal status

        /**
         * PROPERTIES
         */

        /// <summary>
        ///     Property value for if exception should be thrown when given the wrong name
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName {
            get;
            private set;
        }

        /// <summary>
        ///     Title of the associated view
        /// </summary>
        public virtual String DisplayName {
            get;
            protected set;
        }

        /// <summary>
        ///     Exception for models
        /// </summary>
        /// <param name="exc">
        ///     The exception to throw
        /// </param>
        /// <param name="method">
        ///     Affected method name
        /// </param>
        protected void ReportException(Exception exc, String method) {
            System.Diagnostics.Trace.WriteLine("Error in class " + this.GetType().Name + " method " + method + " => " + exc.ToString());
        }

        protected ModelBase() {
            ThrowOnInvalidPropertyName = true;
        }

        /// <summary>
        ///     Retrieve attributes of this model and verify that attribute exists
        /// </summary>
        /// <param name="propertyName">
        ///     Name of property or attribute we want to verify
        /// </param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName) {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null) {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /**
         * EVENT HANDLERS
         */

        /// <summary>
        ///     Event handlers used to indicate any change in any property
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName) {
            this.VerifyPropertyName(propertyName); // Make sure that the given property name is valid

            PropertyChangedEventHandler handler = this.PropertyChanged; // Get a reference to the current handler
            if (handler != null) {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                handler(this, e); // Call the handler
            }
        }

        #region Disposable Methods
        // Methods needed for the IDisposable interface

        /// <summary>
        ///     Dispose of the current model. This is not virtual
        /// </summary>
        public void Dispose() {
            Dispose(true);

            // Object is cleaned up by the Dispoes method
            // Take the object off of the finalization queue and prevent finalization code for the object from executing a second time
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Actual call to disposing the object. The boolean is
        ///     an indiactor for if the code directly calls it (true)
        ///     or it is implicitly called (false)
        /// </summary>
        /// <param name="disposing">
        ///     Boolean indicating what is triggering the method
        /// </param>
        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                // If the object is not already disposed
                if (disposing) {
                    // Dispose all managed and unmanaged resources
                    // TODO
                }

                // Call other cleanup methods
                
                // Disposing is now done
                _disposed = true;
            }
        }

        #endregion

    }
}
