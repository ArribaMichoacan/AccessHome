using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AcessHome.Data
{
    /// <summary>
    /// Class that Provides a lazy way to start the database initialization
    /// and avoid blocking the app execution.
    /// </summary>
    /// <typeparam name="T">Lazy task initialization represented by a Task<T> </typeparam>
    public class AsyncLazy<T>
    {
        private readonly Lazy<Task<T>> instance;

        /// <summary>
        /// Creates a new lazy instance initialization using a synchronous delegate
        /// </summary>
        /// <param name="factory">Delegate synchronous</param>
        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        /// <summary>
        /// creates a new lazy instance initialization using an asynchronous delegate
        /// </summary>
        /// <param name="factory">Delegate Asynchronous</param>
        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        /// <summary>
        /// Gets an awaiter for the task that represents an asynchronous value.
        /// </summary>
        /// <returns>The awaiter for the task</returns>
        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }

        /// <summary>
        /// Creates the value in an asynchronously if it has not already been created.
        /// </summary>
        public void Start()
        {
            var unused = instance.Value;
        }
    }
}