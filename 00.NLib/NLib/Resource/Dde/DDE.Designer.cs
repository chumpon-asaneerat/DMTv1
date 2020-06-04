﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NLib.Resource.Dde {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DDE {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DDE() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NLib.Resource.Dde.DDE", typeof(DDE).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The server failed to advise &quot;${service}|${topic}!${item}&quot;..
        /// </summary>
        internal static string AdviseFailedMessage {
            get {
                return ResourceManager.GetString("AdviseFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An advise loop for &quot;${service}|${topic}!${item}&quot; already exists..
        /// </summary>
        internal static string AlreadyBeingAdvisedMessage {
            get {
                return ResourceManager.GetString("AlreadyBeingAdvisedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client is already connected..
        /// </summary>
        internal static string AlreadyConnectedMessage {
            get {
                return ResourceManager.GetString("AlreadyConnectedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The context is already intialized..
        /// </summary>
        internal static string AlreadyInitializedMessage {
            get {
                return ResourceManager.GetString("AlreadyInitializedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified conversation is already paused..
        /// </summary>
        internal static string AlreadyPausedMessage {
            get {
                return ResourceManager.GetString("AlreadyPausedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The service is already registered..
        /// </summary>
        internal static string AlreadyRegisteredMessage {
            get {
                return ResourceManager.GetString("AlreadyRegisteredMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The IAsyncResult must have been returned by a call to ${method}..
        /// </summary>
        internal static string AsyncResultParameterInvalidMessage {
            get {
                return ResourceManager.GetString("AsyncResultParameterInvalidMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client failed to pause the conversation..
        /// </summary>
        internal static string ClientPauseFailedMessage {
            get {
                return ResourceManager.GetString("ClientPauseFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client failed to resume the conversation..
        /// </summary>
        internal static string ClientResumeFailedMessage {
            get {
                return ResourceManager.GetString("ClientResumeFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client failed to connect to &quot;${service}|${topic}&quot;.  Make sure the server application is running and that it supports the specified service name and topic name pair..
        /// </summary>
        internal static string ConnectFailedMessage {
            get {
                return ResourceManager.GetString("ConnectFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client failed to execute &quot;${command}&quot;..
        /// </summary>
        internal static string ExecuteFailedMessage {
            get {
                return ResourceManager.GetString("ExecuteFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The transaction filter has already been added..
        /// </summary>
        internal static string FilterAlreadyAddedMessage {
            get {
                return ResourceManager.GetString("FilterAlreadyAddedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The transaction filter has not been added..
        /// </summary>
        internal static string FilterNotAddedMessage {
            get {
                return ResourceManager.GetString("FilterNotAddedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The context failed to initialize..
        /// </summary>
        internal static string InitializeFailedMessage {
            get {
                return ResourceManager.GetString("InitializeFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The context timed out attempting to marshal the operation..
        /// </summary>
        internal static string MarshalTimeoutMessage {
            get {
                return ResourceManager.GetString("MarshalTimeoutMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The context is not hosted on a thread with a message loop..
        /// </summary>
        internal static string NoMessageLoopMessage {
            get {
                return ResourceManager.GetString("NoMessageLoopMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An advise loop for &quot;${service}|${topic}!${item}&quot; does not exist..
        /// </summary>
        internal static string NotBeingAdvisedMessage {
            get {
                return ResourceManager.GetString("NotBeingAdvisedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client is not connected..
        /// </summary>
        internal static string NotConnectedMessage {
            get {
                return ResourceManager.GetString("NotConnectedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The context is not initialized..
        /// </summary>
        internal static string NotInitializedMessage {
            get {
                return ResourceManager.GetString("NotInitializedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified conversation is not paused..
        /// </summary>
        internal static string NotPausedMessage {
            get {
                return ResourceManager.GetString("NotPausedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The service is not registered..
        /// </summary>
        internal static string NotRegisteredMessage {
            get {
                return ResourceManager.GetString("NotRegisteredMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client failed to poke &quot;${service}|${topic}!${item}&quot;..
        /// </summary>
        internal static string PokeFailedMessage {
            get {
                return ResourceManager.GetString("PokeFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The server failed to register &quot;${service}&quot;..
        /// </summary>
        internal static string RegisterFailedMessage {
            get {
                return ResourceManager.GetString("RegisterFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client failed to request &quot;${service}|${topic}!${item}&quot;..
        /// </summary>
        internal static string RequestFailedMessage {
            get {
                return ResourceManager.GetString("RequestFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The server failed to pause all conversations..
        /// </summary>
        internal static string ServerPauseAllFailedMessage {
            get {
                return ResourceManager.GetString("ServerPauseAllFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The server failed to pause the specified conversation..
        /// </summary>
        internal static string ServerPauseFailedMessage {
            get {
                return ResourceManager.GetString("ServerPauseFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The server failed to resume all conversations..
        /// </summary>
        internal static string ServerResumeAllFailedMessage {
            get {
                return ResourceManager.GetString("ServerResumeAllFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The server failed to resume the specified conversation..
        /// </summary>
        internal static string ServerResumeFailedMessage {
            get {
                return ResourceManager.GetString("ServerResumeFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client failed to initiate an advise loop for &quot;${service}|${topic}!${item}&quot;..
        /// </summary>
        internal static string StartAdviseFailedMessage {
            get {
                return ResourceManager.GetString("StartAdviseFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client failed to terminate the advise loop for &quot;${service}|${topic}!${item}&quot;..
        /// </summary>
        internal static string StopAdviseFailedMessage {
            get {
                return ResourceManager.GetString("StopAdviseFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The parameter must be &lt;= 255 characters..
        /// </summary>
        internal static string StringParameterInvalidMessage {
            get {
                return ResourceManager.GetString("StringParameterInvalidMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The parameter must be &gt; 0..
        /// </summary>
        internal static string TimeoutParameterInvalidMessage {
            get {
                return ResourceManager.GetString("TimeoutParameterInvalidMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unknown error occurred..
        /// </summary>
        internal static string UnknownErrorMessage {
            get {
                return ResourceManager.GetString("UnknownErrorMessage", resourceCulture);
            }
        }
    }
}
