using System;
using System.Runtime.Remoting.Messaging;

namespace Zek.Extensions
{
    public static class EventHandlerExtensions
    {
        ///// <summary>
        ///// Invoke an event asynchronously. Each subscriber to the event will be invoked on a separate thread.
        ///// </summary>
        ///// <param name="someEvent">The event to be invoked asynchronously.</param>
        ///// <param name="sender">The sender of the event.</param>
        ///// <param name="args">The args of the event.</param>
        ///// <typeparam name="TEventArgs">The type of <see cref="EventArgs"/> to be used with the event.</typeparam>
        //public static void InvokeAsync<TEventArgs>(this EventHandler<TEventArgs> someEvent, object sender, TEventArgs args)
        //    where TEventArgs : EventArgs
        //{
        //    if (someEvent == null)
        //    {
        //        return;
        //    }

        //    var eventListeners = someEvent.GetInvocationList();

        //    AsyncCallback endAsyncCallback = delegate(IAsyncResult iar)
        //                                         {
        //                                             var ar = iar as AsyncResult;
        //                                             if (ar == null)
        //                                             {
        //                                                 return;
        //                                             }

        //                                             var invokedMethod = ar.AsyncDelegate as EventHandler<TEventArgs>;
        //                                             if (invokedMethod != null)
        //                                             {
        //                                                 invokedMethod.EndInvoke(iar);
        //                                             }
        //                                         };

        //    foreach (EventHandler<TEventArgs> methodToInvoke in eventListeners)
        //    {
        //        methodToInvoke.BeginInvoke(sender, args, endAsyncCallback, null);
        //    }
        //}

        ///// <summary>
        ///// Invoke an event asynchronously. Each subscriber to the event will be invoked on a separate thread.
        ///// </summary>
        ///// <param name="someEvent">The event to be invoked asynchronously.</param>
        ///// <param name="sender">The sender of the event.</param>
        ///// <param name="args">The args of the event.</param>
        //public static void InvokeAsync(this EventHandler someEvent, object sender, EventArgs args)
        //{
        //    if (someEvent == null)
        //    {
        //        return;
        //    }

        //    var eventListeners = someEvent.GetInvocationList();

        //    AsyncCallback endAsyncCallback = delegate(IAsyncResult iar)
        //                                         {
        //                                             var ar = iar as AsyncResult;
        //                                             if (ar == null)
        //                                             {
        //                                                 return;
        //                                             }

        //                                             var invokedMethod = ar.AsyncDelegate as EventHandler;
        //                                             if (invokedMethod != null)
        //                                             {
        //                                                 invokedMethod.EndInvoke(iar);
        //                                             }
        //                                         };

        //    foreach (EventHandler methodToInvoke in eventListeners)
        //    {
        //        methodToInvoke.BeginInvoke(sender, args, endAsyncCallback, null);
        //    }
        //}


        /// <summary>
        /// Method for invoking/raising events.
        /// </summary>
        /// <param name="handler">Required. The EventHandler to Invoke.</param>
        /// <param name="sender">Required.</param>
        public static void Raise(this EventHandler handler, object sender)
        {
            handler.Raise(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Method for invoking/raising events.
        /// </summary>
        /// <param name="handler">Required. The EventHandler to Invoke.</param>
        /// <param name="sender">Required.</param>
        /// <param name="e">Required.</param>
        public static void Raise(this EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
                handler(sender, e);
        }

        /// <summary>
        /// Method for invoking/raising events.
        /// </summary>
        /// <param name="handler">Required. The EventHandler to Invoke.</param>
        /// <param name="sender">Required.</param>
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender) where TEventArgs : EventArgs
        {
            handler.Raise(sender, Activator.CreateInstance<TEventArgs>());
        }

        /// <summary>
        /// Method for invoking/raising events.
        /// </summary>
        /// <param name="handler">Required. The EventHandler to Invoke.</param>
        /// <param name="sender">Required.</param>
        /// <param name="e">Required.</param>
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            if (handler != null)
                handler(sender, e);
        }

        /// <summary>
        /// Invoke an event asynchronously. Each subscriber to the event will be invoked on a separate thread.
        /// </summary>
        /// <param name="someEvent">The event to be invoked asynchronously.</param>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The args of the event.</param>
        /// <typeparam name="TEventArgs">The type of <see cref="EventArgs"/> to be used with the event.</typeparam>
        public static void InvokeAsync<TEventArgs>(this EventHandler<TEventArgs> someEvent, object sender, TEventArgs args)
            where TEventArgs : EventArgs
        {
            if (someEvent == null)
            {
                return;
            }

            var eventListeners = someEvent.GetInvocationList();

            AsyncCallback endAsyncCallback = delegate(IAsyncResult iar)
            {
                var ar = iar as AsyncResult;
                if (ar == null)
                {
                    return;
                }

                var invokedMethod = ar.AsyncDelegate as EventHandler<TEventArgs>;
                if (invokedMethod != null)
                {
                    invokedMethod.EndInvoke(iar);
                }
            };

            foreach (EventHandler<TEventArgs> methodToInvoke in eventListeners)
            {
                methodToInvoke.BeginInvoke(sender, args, endAsyncCallback, null);
            }
        }

        /// <summary>
        /// Invoke an event asynchronously. Each subscriber to the event will be invoked on a separate thread.
        /// </summary>
        /// <param name="someEvent">The event to be invoked asynchronously.</param>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The args of the event.</param>
        public static void InvokeAsync(this EventHandler someEvent, object sender, EventArgs args)
        {
            if (someEvent == null)
            {
                return;
            }

            var eventListeners = someEvent.GetInvocationList();

            AsyncCallback endAsyncCallback = delegate(IAsyncResult iar)
            {
                var ar = iar as AsyncResult;
                if (ar == null)
                {
                    return;
                }

                var invokedMethod = ar.AsyncDelegate as EventHandler;
                if (invokedMethod != null)
                {
                    invokedMethod.EndInvoke(iar);
                }
            };

            foreach (EventHandler methodToInvoke in eventListeners)
            {
                methodToInvoke.BeginInvoke(sender, args, endAsyncCallback, null);
            }
        }
    }
}